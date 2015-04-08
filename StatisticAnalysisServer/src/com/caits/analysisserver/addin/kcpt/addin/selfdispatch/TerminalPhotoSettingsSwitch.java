package com.caits.analysisserver.addin.kcpt.addin.selfdispatch;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;

import org.apache.commons.beanutils.BeanUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.SelfDispatch;
import com.caits.analysisserver.bean.FailureSendCommandBean;
import com.caits.analysisserver.bean.PhotoSettingDetailBean;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;
import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.sendcommand.manager.commproxy.SendCommandBean;
import com.ctfo.sendcommand.manager.model.CommandBean;
import com.ctfo.sendcommand.manager.service.SendCommandService;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： 终端触发拍照控制开关<br>
 * 描述： 根据平台设置下发终端参数设置<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2012-10-31</td>
 * <td>yujch</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author yujch
 * @since JDK1.6
 */
@SuppressWarnings({"rawtypes","unused","static-access"})
public class TerminalPhotoSettingsSwitch extends SelfDispatch {
	private static final Logger logger = LoggerFactory.getLogger(TerminalPhotoSettingsSwitch.class);
	
	/** 指令头： CAITS */
    private final String COMMAND_HEAD_CAITS = "CAITS";
    /** 通讯方式：服务器 */
    private final String CHANNED_TYPE_SERVER = "0";
    /** 重试次数：0 */
    private final String RE_TRY_TIME = "0";
    /** 重试次数：1 */
    private final String RE_TRY_TIME_1 = "1";
    /** 指令来源类型：平台 */
    private final Integer COMMAND_FROM_TYPE_PLATFORM = 0;
    
    private final int TYPE_DRIVING_RECORD = 1;
    
    private final int TYPE_DRIVING_RECORD_3 = 3;//终端立即拍照用此类型
    
    private long sleepTime = 1*60*1000;//每1分钟生成一次任务
    
    private long preEndTime = 0L;
    
    private Timer timer=new Timer();
	/*			
	180	定时拍照控制 B0~B31				
	181	定距拍照控制 B0~B31				
	*/				

	private String sendCommandUrl;
	
	private String timeOpenControlStr;//定时开启拍照控制字符串
	
	private String distanceOpenControlStr;//定距拍照开启控制字符串
	
	// ------获得xml拼接的Sql语句
	private String queryNextJobTimeSql; // 查询下次job时间
	private String queryCurrentJobSql; // 查询当前能够执行的job
	private String queryWaitSendJobSql;//查询等待下发的任务
	private String saveSendCommandSql; // 保存命令发送记录
	private String saveSendSettingLogSql; // 保存参数设置日志
	private String updatePhotoSettingNewFlagSql;
	
	private Thread rsct=null;
	
	
	private BlockingQueue failureQueue = new ArrayBlockingQueue(1000);
	
	// 初始化方法
	public void initAnalyser(){
		preEndTime = System.currentTimeMillis();
		String sleepTimeStr = SystemBaseInfoPool.getinstance().getBaseInfoMap("photosetting_sleepTime").getValue();
		if (sleepTimeStr!=null&&!"".equals(sleepTimeStr)){
			sleepTime = Long.parseLong(sleepTimeStr)*60*1000;
		}
		//参数
		sendCommandUrl = SystemBaseInfoPool.getinstance().getBaseInfoMap("sendcommand_url").getValue();
		timeOpenControlStr = SystemBaseInfoPool.getinstance().getBaseInfoMap("time_open_control_str").getValue();
		distanceOpenControlStr = SystemBaseInfoPool.getinstance().getBaseInfoMap("distance_open_control_str").getValue();
		
		// 查询扩展表分区语句
		queryNextJobTimeSql = SQLPool.getinstance().getSql("sql_queryNextJobTimeSql");
		queryCurrentJobSql = SQLPool.getinstance().getSql("sql_queryCurrentJobSql");
		queryWaitSendJobSql = SQLPool.getinstance().getSql("sql_queryWaitSendJobSql");
		saveSendCommandSql = SQLPool.getinstance().getSql("sql_saveSendCommandSql");
		saveSendSettingLogSql = SQLPool.getinstance().getSql("sql_saveSendSettingLogSql");
		updatePhotoSettingNewFlagSql = SQLPool.getinstance().getSql("sql_updatePhotoSettingNewFlagSql");
	}

 	public void run() {
		logger.info("定时定距触发拍照设置开始发送！");
		//启动失败任务重发线程
		/*if (rsct==null){
			ReSendCommandThread reSend = new ReSendCommandThread();
			rsct=new Thread(reSend);
			rsct.start();
		}*/
		
		while (true) {
			try {
				//启动任务调度线程
				queryWaitSendJob();
				
				//休眠
				this.sleep(sleepTime);
				
			} catch (InterruptedException e) {
 				e.printStackTrace();
				this.run();
			}
		}

	}
	
	/**
	 * 查询当前时间延后10分钟的待发送任务，及当前时间之前10分钟内新添加并需要执行的记录
	 * 生成调度任务
	 */
	public void queryWaitSendJob(){
		PreparedStatement dbPstmt = null;
		PreparedStatement dbPstmt0 = null;
		Connection dbConnection = null;
		
		int count = 0;//成功下发终端个数
		
		// 结果集对象
		ResultSet dbResultSet = null;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				//查询当前要下发的终端参数信息
				long beginUTC = System.currentTimeMillis();
				
				logger.info("查询"+preEndTime+"到"+(beginUTC+sleepTime)+"之间的数据");
				dbPstmt = dbConnection.prepareStatement(queryWaitSendJobSql);
				dbPstmt.setLong(1, preEndTime);//参数以秒为单位
				dbPstmt.setLong(2, beginUTC+sleepTime);
				dbPstmt.setLong(3, preEndTime);
				dbPstmt.setLong(4, beginUTC+sleepTime);
				dbPstmt.setLong(5, preEndTime);
				dbPstmt.setLong(6, preEndTime);
				/*	dbPstmt.setInt(4, sleepSecond);*/
				dbResultSet = dbPstmt.executeQuery();
				
				//查询结束修改标记
				dbPstmt0 = dbConnection.prepareStatement(updatePhotoSettingNewFlagSql);
				preEndTime = beginUTC+sleepTime;
				while (dbResultSet.next()) {
					//如果任务日期小于当前日期，生成任务；如果任务日期大于等于当前日期，立即下发
					Date beginTime = dbResultSet.getTimestamp("BEGIN_TIME");
					Date endTime = dbResultSet.getTimestamp("END_TIME");
					String taskFlag = dbResultSet.getString("TASK_FLAG");
					
					String tprotocolType = dbResultSet.getString("TPROTOCOL_TYPE");
					
					String detailId = dbResultSet.getString("DETAIL_ID");
					
					PhotoSettingDetailBean psdb = new PhotoSettingDetailBean();
					psdb.setVid(dbResultSet.getString("VID"));
					psdb.setTimeInterval(dbResultSet.getInt("TIMING_INTERVAL"));
					psdb.setDistinceInterval(dbResultSet.getInt("DISTANCE_INTERVAL"));
					psdb.setBeginTime(beginTime);
					psdb.setEndTime(endTime);
					psdb.setUserId(dbResultSet.getString("CREATE_BY"));
					psdb.setDetailId(detailId);
					psdb.setVehicleNo(dbResultSet.getString("VEHICLE_NO"));
					psdb.setOemCode(dbResultSet.getString("OEM_CODE"));
					psdb.setCommaddr(dbResultSet.getString("COMMADDR"));
					psdb.setTerType(dbResultSet.getString("TER_TYPE"));
					psdb.setDvrNo(dbResultSet.getString("DVR_NO"));
					psdb.setIsNew(dbResultSet.getString("IS_NEW"));
					psdb.setTprotocolType(tprotocolType);
					
					dbPstmt0.setString(1, detailId);
					logger.info("detailId:" + detailId + ";flag:"+taskFlag+" beginTime:"+beginTime+" beginTimeutc:"+beginTime.getTime()+" currentTime:"+beginUTC);
					
					if ("0".equals(taskFlag)){
						if (beginTime.getTime()>beginUTC&&beginTime.getTime()<=(beginUTC+sleepTime)){
							//创建定时定距开启任务
							logger.info("执行1");
							psdb.setSwitchFlag("1");
							if (!(psdb.getTerType()!=null&&"2".equals(psdb.getTerType())&&(psdb.getDvrNo()==null||psdb.getIsNew()!=null))){
								long delay = beginTime.getTime()-beginUTC;
								if ("808B".equals(tprotocolType)){
									SendCommandTask sctask = new SendCommandTask(psdb);
									timer.schedule(sctask, delay);
								}else{
									psdb.setPid("1");
									SendCommandTask sctask1 = new SendCommandTask(psdb);
									timer.schedule(sctask1, delay);
									
									PhotoSettingDetailBean psdb2 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb2.setPid("2");
									SendCommandTask sctask2 = new SendCommandTask(psdb2);
									timer.schedule(sctask2, delay);
									
									PhotoSettingDetailBean psdb3 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb3.setPid("3");
									SendCommandTask sctask3 = new SendCommandTask(psdb3);
									timer.schedule(sctask3, delay);
									
									PhotoSettingDetailBean psdb4 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb4.setPid("4");
									SendCommandTask sctask4 = new SendCommandTask(psdb4);
									timer.schedule(sctask4, delay);
								}
							}
						}
						if (endTime.getTime()>beginUTC&&endTime.getTime()<=(beginUTC+sleepTime)){
							logger.info("执行2");
							//创建定时定距禁用任务
							if ("808B".equals(tprotocolType)){//808B终端需要到点禁用，808终端不需要
								psdb.setSwitchFlag("0");
								if (!(psdb.getTerType()!=null&&"2".equals(psdb.getTerType()))){
									SendCommandTask sctask = new SendCommandTask(psdb);
									timer.schedule(sctask, endTime.getTime()-beginUTC);
								}
							}
						}
					}
					if ("1".equals(taskFlag)){
						if (beginTime.getTime()<=beginUTC&&endTime.getTime()>beginUTC){
							logger.info("执行3");
							//下发定时定距开启指令
							psdb.setSwitchFlag("1");
							if (!(psdb.getTerType()!=null&&"2".equals(psdb.getTerType())&&(psdb.getDvrNo()==null||psdb.getIsNew()!=null))){
								if ("808B".equals(tprotocolType)){
									SendCommandThread sct = new SendCommandThread(psdb);
									Thread tt = new Thread(sct);
									tt.start();
								}else{
									psdb.setPid("1");
									SendCommandThread sct = new SendCommandThread(psdb);
									Thread tt = new Thread(sct);
									tt.start();
									
									PhotoSettingDetailBean psdb2 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb2.setPid("2");
									SendCommandThread sct2 = new SendCommandThread(psdb2);
									Thread tt2 = new Thread(sct2);
									tt2.start();
									
									PhotoSettingDetailBean psdb3 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb3.setPid("3");
									SendCommandThread sct3 = new SendCommandThread(psdb3);
									Thread tt3 = new Thread(sct3);
									tt3.start();
									
									PhotoSettingDetailBean psdb4 = (PhotoSettingDetailBean)BeanUtils.cloneBean(psdb);
									psdb4.setPid("4");
									SendCommandThread sct4 = new SendCommandThread(psdb4);
									Thread tt4 = new Thread(sct4);
									tt4.start();
								}
							}
							
						}
						if (endTime.getTime()<=beginUTC&&endTime.getTime()>=(beginUTC-15*60*1000)){
							logger.info("执行4");
							//下发定时定距禁用指令
							if ("808B".equals(tprotocolType)){//808B终端需要到点禁用，808终端不需要
								psdb.setSwitchFlag("0");
								if (!(psdb.getTerType()!=null&&"2".equals(psdb.getTerType()))){
									SendCommandThread sct = new SendCommandThread(psdb);
									Thread tt1 = new Thread(sct);
									tt1.start();
								}
							}
						}
					}
					dbPstmt0.addBatch();
					count++;
				}
				dbPstmt0.executeBatch();
			}
			logger.info(count+"个终端触发拍照设置指令下发成功");
		}catch (Exception e) {
			logger.error("触发拍照设置指令下发出错：", e);
			// flag = 0;
		} finally {
			try {
				count=0;
				if(dbResultSet != null){
					dbResultSet.close();
				}
				if(dbPstmt != null){
					dbPstmt.close();
				}
				if(dbPstmt0 != null){
					dbPstmt0.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
	}
	
	class SendCommandTask extends TimerTask{
		private PhotoSettingDetailBean psdb ;
		
		public SendCommandTask(PhotoSettingDetailBean psdb){
			this.psdb = psdb;
		}
		public void run(){
			sendCommandToSetting(psdb);
		}  
	} 

	/**
	 * 指令发送线程
	 * @author yujch
	 *
	 */
	class SendCommandThread implements Runnable {
		
		private PhotoSettingDetailBean psdb ;
		public SendCommandThread(PhotoSettingDetailBean psdb){
			this.psdb = psdb;
		}

		@Override
		public void run() {
 			sendCommandToSetting(psdb);
		}
	}

	/**
	 * 下发指令设置终端参数
	 * 
	 * 查询未解析的行驶记录命令
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
 	public int sendCommandToSetting(Date jobTime) {
		//logger.info("start parse!");
		// PreparedStatement对象
		PreparedStatement dbPstmt0 = null;
		PreparedStatement saveCommandPstmt = null;
		PreparedStatement saveSettingLogPstmt = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		
		int count = 0;//成功下发终端个数
		
		// 结果集对象
		ResultSet dbResultSet = null;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				//查询当前要下发的终端参数信息
				dbPstmt0 = dbConnection.prepareStatement(queryCurrentJobSql);
				saveCommandPstmt = dbConnection.prepareStatement(saveSendCommandSql);
				saveSettingLogPstmt = dbConnection.prepareStatement(saveSendSettingLogSql);
				dbPstmt0.setTimestamp(1,new java.sql.Timestamp(jobTime.getTime()));
				dbPstmt0.setTimestamp(2,new java.sql.Timestamp(jobTime.getTime()));
				dbResultSet = dbPstmt0.executeQuery();
				
				while (dbResultSet.next()) {
					String vid = dbResultSet.getString("VID");
					int timeInterval = dbResultSet.getInt("TIMING_INTERVAL");
					int distinceInterval = dbResultSet.getInt("DISTANCE_INTERVAL");
					Date beginTime = dbResultSet.getTimestamp("BEGIN_TIME");
					Date endTime = dbResultSet.getTimestamp("END_TIME");
					String userId = dbResultSet.getString("CREATE_BY");
					String detailId = dbResultSet.getString("DETAIL_ID");
					String vehicleNo=dbResultSet.getString("VEHICLE_NO");
					String oemCode=dbResultSet.getString("OEM_CODE");
					String commaddr=dbResultSet.getString("COMMADDR");

					Long sendTime = null;
					
					String switchFlag = "0";//禁用
					Map<String, Object> keyValueMap = new HashMap<String, Object>();
					//判断需要下发开启还是禁用指令
					if (jobTime.equals(beginTime)){
						//下发启用命令
						if (timeInterval>0){
							String timeIntervalStr = Integer.toBinaryString(timeInterval*60);
							
							int timeControl = Integer.parseInt(timeIntervalStr+timeOpenControlStr,2);
							keyValueMap.put("180", ""+timeControl);
						}
						if (distinceInterval>0){
							String distinceIntervalStr = Integer.toBinaryString(distinceInterval*1000);
							
							int distinceControl = Integer.parseInt(distinceIntervalStr+distanceOpenControlStr,2);
							keyValueMap.put("181", ""+distinceControl);
						}
						switchFlag = "1";
					}else{
						//下发禁用命令
						if (timeInterval>0){
						keyValueMap.put("180", "0");
						}
						if (distinceInterval>0){
						keyValueMap.put("181", "0");
						}
					}
					
					String seq = SendCommandService.getSeq(userId);

		            // 组装SendCommandBean
					SendCommandBean sendCommandBean = new SendCommandBean();
		            sendCommandBean.setHead(COMMAND_HEAD_CAITS);
		            sendCommandBean.setSeq(seq);
		            sendCommandBean.setOemcode(oemCode);
		            sendCommandBean.setTidentifyno(commaddr);
		            sendCommandBean.setCommtype(CHANNED_TYPE_SERVER);
		            sendCommandBean.setRetry(RE_TRY_TIME);
		            sendCommandBean.setType(TYPE_DRIVING_RECORD);
		            sendCommandBean.setKeyValueMap(keyValueMap);
		            
		            sendTime = (new Date()).getTime();
		            
		            CommandBean commandBean = SendCommandService.getCommandStr(sendCommandBean);
		            SendCommandService.msgUrl=sendCommandUrl;
		          //调用接口进行下发
					boolean resultFlag = SendCommandService.sendCommand(commandBean.getCommand()+vid);
					
					// 根据指令发送服务返回结果处理本方法返回结果和发送状态
					Integer result =-1;
					if(resultFlag){
						result=2;
					}
					logger.info(vid+"::"+vehicleNo+" send status:"+resultFlag);
					/*if (!resultFlag){
						//把指令存入发送失败队列,等待重新发送
						FailureSendCommandBean fscb = new FailureSendCommandBean();
						fscb.setScb(sendCommandBean);
						fscb.setBeginTime(beginTime);
						fscb.setEndTime(endTime);
						fscb.setUserId(userId);
						fscb.setId(detailId);
						fscb.setVid(vid);
						fscb.setVehicleNo(vehicleNo);
						fscb.setOemCode(oemCode);
						fscb.setCommaddr(commaddr);
						fscb.setSwitchFlag(switchFlag);
						
						failureQueue.offer(fscb);
					}*/
					saveCommandPstmt.setString(1, GeneratorPK.instance().getPKString());
					saveCommandPstmt.setString(2, userId);
					saveCommandPstmt.setString(3, vehicleNo);
					saveCommandPstmt.setLong(4, sendTime);
					saveCommandPstmt.setString(5,commandBean.getCoType());
					saveCommandPstmt.setInt(6, COMMAND_FROM_TYPE_PLATFORM);
					saveCommandPstmt.setString(7, seq);
					saveCommandPstmt.setString(8, commandBean.getCoChannel());
					saveCommandPstmt.setString(9, commandBean.getCoParm());
					saveCommandPstmt.setString(10, commandBean.getCommand());
					saveCommandPstmt.setInt(11, result);
					saveCommandPstmt.setNull(12, Types.VARCHAR);
					saveCommandPstmt.setNull(13, Types.INTEGER);
					saveCommandPstmt.setString(14, oemCode);
					saveCommandPstmt.setInt(15, 1);
					saveCommandPstmt.setInt(16, 1);
					saveCommandPstmt.setString(17, commandBean.getCoSubtype());
					saveCommandPstmt.setString(18, userId.toString());
					saveCommandPstmt.setString(19, vid);
					saveCommandPstmt.setNull(20, Types.VARCHAR);
					
					saveCommandPstmt.addBatch();
					
					String photoLogKey = GeneratorPK.instance().getPKString();
					logger.debug("----------------------photoLogKey:"+photoLogKey);
					//保存或更新发送日志
					saveSettingLogPstmt.setString(1, vid);
					saveSettingLogPstmt.setString(2, detailId);
					saveSettingLogPstmt.setString(3, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(4,seq);
					saveSettingLogPstmt.setString(5, "1");
					saveSettingLogPstmt.setString(6, photoLogKey);
					saveSettingLogPstmt.setString(7, vid);
					saveSettingLogPstmt.setString(8, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(9,seq);
					saveSettingLogPstmt.setString(10, userId);
					saveSettingLogPstmt.setString(11, "1");
					saveSettingLogPstmt.setString(12, "1");
					
					saveSettingLogPstmt.addBatch();
					
				}
				
				saveCommandPstmt.executeBatch();
				saveSettingLogPstmt.executeBatch();
				
			}else{
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("触发拍照设置指令下发出错：", e);
			// flag = 0;
		} finally {
			try {
				if(dbResultSet != null){
					dbResultSet.close();
				}
				if(dbPstmt0 != null){
					dbPstmt0.close();
				}
				if(saveCommandPstmt != null){
					saveCommandPstmt.close();
				}
				if(saveSettingLogPstmt != null){
					saveSettingLogPstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
		return flag;
	}
	
	public int sendCommandToSetting(PhotoSettingDetailBean psdb) {
		//logger.info("start parse!");
		// PreparedStatement对象
		PreparedStatement saveCommandPstmt = null;
		PreparedStatement saveSettingLogPstmt = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		
		int count = 0;//成功下发终端个数

		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				//查询当前要下发的终端参数信息
				
				saveCommandPstmt = dbConnection.prepareStatement(saveSendCommandSql);
				saveSettingLogPstmt = dbConnection.prepareStatement(saveSendSettingLogSql);
				if (psdb!=null){
					String vid = psdb.getVid();
					int timeInterval = psdb.getTimeInterval();
					int distinceInterval = psdb.getDistinceInterval();
					Date beginTime = psdb.getBeginTime();
					Date endTime = psdb.getEndTime();
					String userId = psdb.getUserId();
					String detailId = psdb.getDetailId();
					String vehicleNo= psdb.getVehicleNo();
					String oemCode= psdb.getOemCode();
					String commaddr=psdb.getCommaddr();
					String switchFlag = psdb.getSwitchFlag();//禁用
					String terType = psdb.getTerType();
					String dvrNo = psdb.getDvrNo();
					String tprotocolType = psdb.getTprotocolType();
					String pid = psdb.getPid();
					
					Long sendTime = null;
					
					
					Map<String, Object> keyValueMap = new HashMap<String, Object>();
					//判断需要下发开启还是禁用指令
					if ("1".equals(switchFlag)){						
						//下发启用命令
						if ("808B".equals(tprotocolType)){
							if (timeInterval>0){
								String timeIntervalStr = Integer.toBinaryString(timeInterval*60);
								
								long timeControl = Long.parseLong(timeIntervalStr+timeOpenControlStr,2);
								keyValueMap.put("180", ""+timeControl);
								
								if (terType!=null&&dvrNo!=null&&"2".equals(terType)){
									//切换2g、3g手机号
									String tmpNo = dvrNo;
									dvrNo = commaddr;
									commaddr = tmpNo;
									//此指令需要给视频服务器发
									keyValueMap.put("309", "1");
									keyValueMap.put("136", "1");
									keyValueMap.put("137", "127");
									keyValueMap.put("138", "63");
									keyValueMap.put("139", "63");
									keyValueMap.put("140", "127");
									keyValueMap.put("610", "00000000"+CDate.date2StrByFormat(beginTime, "HHmmss"));
									keyValueMap.put("611", "00000000"+CDate.date2StrByFormat(endTime, "HHmmss"));
									keyValueMap.put("BASE_TEL", dvrNo);
								}
							}
							if (distinceInterval>0){
								String distinceIntervalStr = Integer.toBinaryString(distinceInterval*1000);
								
								long distinceControl = Long.parseLong(distinceIntervalStr+distanceOpenControlStr,2);
								keyValueMap.put("181", ""+distinceControl);
							}
						}else{
							//808终端下发触发拍照要特殊处理
							if (timeInterval>0){
								//下发终端立即拍照指令
								//计算拍照张数
								int aa = (int)Math.floor(((endTime.getTime() - beginTime.getTime())/1000)/(timeInterval*60));
								//TYPE:10,RETRY:1,VALUE:2|1|1|0|0|10|126|65|65|126
								//图像立即拍摄,value格式为：摄像头通道ID|拍摄命令|录像时间|保存标志|分辨率|照片质量|亮度|对比度|饱和度|色度,191:拍摄序号				
								String value =pid+"|"+aa+"|"+(timeInterval*60)+"|0|0|10|126|65|65|126";
								keyValueMap.put("VALUE", value);
							}
						}
					}else{
						//下发禁用命令
						if (timeInterval>0){
						keyValueMap.put("180", "0");
						}
						if (distinceInterval>0){
						keyValueMap.put("181", "0");
						}
					}
					
					String seq = SendCommandService.getSeq(userId);

		            // 组装SendCommandBean
					SendCommandBean sendCommandBean = new SendCommandBean();
		            sendCommandBean.setHead(COMMAND_HEAD_CAITS);
		            sendCommandBean.setSeq(seq);
		            sendCommandBean.setOemcode(oemCode);
		            sendCommandBean.setTidentifyno(commaddr);
		            sendCommandBean.setCommtype(CHANNED_TYPE_SERVER);
		            if ("808".equals(tprotocolType)){
		            	sendCommandBean.setRetry(RE_TRY_TIME_1);
			            sendCommandBean.setType(TYPE_DRIVING_RECORD_3);
		            }else{
		            	sendCommandBean.setRetry(RE_TRY_TIME);
			            sendCommandBean.setType(TYPE_DRIVING_RECORD);
		            }
		            
		            sendCommandBean.setKeyValueMap(keyValueMap);
		            
		            sendTime = (new Date()).getTime();
		            
		            CommandBean commandBean = SendCommandService.getCommandStr(sendCommandBean);
		            SendCommandService.msgUrl=sendCommandUrl;
		          //调用接口进行下发
					boolean resultFlag = SendCommandService.sendCommand(commandBean.getCommand()+vid);
					
					// 根据指令发送服务返回结果处理本方法返回结果和发送状态
					Integer result =-1;
					if(resultFlag){
						result=2;
					}
					logger.info(vid+"::"+vehicleNo+" send status:"+resultFlag);
					/*if (!resultFlag){
						//把指令存入发送失败队列,等待重新发送
						FailureSendCommandBean fscb = new FailureSendCommandBean();
						fscb.setScb(sendCommandBean);
						fscb.setBeginTime(beginTime);
						fscb.setEndTime(endTime);
						fscb.setUserId(userId);
						fscb.setId(detailId);
						fscb.setVid(vid);
						fscb.setVehicleNo(vehicleNo);
						fscb.setOemCode(oemCode);
						fscb.setCommaddr(commaddr);
						fscb.setSwitchFlag(switchFlag);
						
						failureQueue.offer(fscb);
					}*/
					saveCommandPstmt.setString(1,GeneratorPK.instance().getPKString());
					saveCommandPstmt.setString(2, userId);
					saveCommandPstmt.setString(3, vehicleNo);
					saveCommandPstmt.setLong(4, sendTime);
					saveCommandPstmt.setString(5,commandBean.getCoType());
					saveCommandPstmt.setInt(6, COMMAND_FROM_TYPE_PLATFORM);
					saveCommandPstmt.setString(7, seq);
					saveCommandPstmt.setString(8, commandBean.getCoChannel());
					saveCommandPstmt.setString(9, commandBean.getCoParm());
					saveCommandPstmt.setString(10, commandBean.getCommand());
					saveCommandPstmt.setInt(11, result);
					saveCommandPstmt.setNull(12, Types.VARCHAR);
					saveCommandPstmt.setNull(13, Types.INTEGER);
					saveCommandPstmt.setString(14, oemCode);
					saveCommandPstmt.setInt(15, 1);
					saveCommandPstmt.setInt(16, 1);
					saveCommandPstmt.setString(17, commandBean.getCoSubtype());
					saveCommandPstmt.setString(18, userId.toString());
					saveCommandPstmt.setString(19, vid);
					saveCommandPstmt.setNull(20, Types.VARCHAR);
					
					saveCommandPstmt.addBatch();
					
					String photoLogKey = GeneratorPK.instance().getPKString();
					
					//保存或更新发送日志
					saveSettingLogPstmt.setString(1, vid);
					saveSettingLogPstmt.setString(2, detailId);
					saveSettingLogPstmt.setString(3, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(4,seq);
					saveSettingLogPstmt.setString(5, "1");
					saveSettingLogPstmt.setString(6,photoLogKey);
					saveSettingLogPstmt.setString(7, vid);
					saveSettingLogPstmt.setString(8, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(9,seq);
					saveSettingLogPstmt.setString(10, userId);
					saveSettingLogPstmt.setString(11, "1");
					saveSettingLogPstmt.setString(12, "1");
					
					saveSettingLogPstmt.addBatch();
					
				}
				
				saveCommandPstmt.executeBatch();
				saveSettingLogPstmt.executeBatch();
				
			}else{
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("触发拍照设置指令下发出错：", e);
			// flag = 0;
		} finally {
			try {
				if(saveCommandPstmt != null){
					saveCommandPstmt.close();
				}
				if(saveSettingLogPstmt != null){
					saveSettingLogPstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
		return flag;
	}
	
	public int reSendCommandToSetting(FailureSendCommandBean failureBean) {
		PreparedStatement saveCommandPstmt = null;
		PreparedStatement saveSettingLogPstmt = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		
		try {
			Date nowDate = new Date();
			//当发送时间有效时进行发送
			Date beginTime = failureBean.getBeginTime();
			Date endTime = failureBean.getEndTime();
			String switchFlag = failureBean.getSwitchFlag();
			
			if (("1".equals(switchFlag)&&nowDate.before(endTime)&&nowDate.after(beginTime))||("0".equals(switchFlag)&&nowDate.before(CDate.getNextHourDateFromParam(endTime,15))&&nowDate.after(endTime))){

			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				//查询当前要下发的终端参数信息
				saveCommandPstmt = dbConnection.prepareStatement(saveSendCommandSql);
				saveSettingLogPstmt = dbConnection.prepareStatement(saveSendSettingLogSql);
				
					String vid = failureBean.getVid();
					String userId = failureBean.getUserId();
					String detailId = failureBean.getId();
					String vehicleNo = failureBean.getVehicleNo();
					String oemCode = failureBean.getOemCode();
					String commaddr = failureBean.getCommaddr();
					
					logger.info(vehicleNo+"定时定距触发拍照参数设置重新发送");
					
					String seq = SendCommandService.getSeq(failureBean.getUserId());

		            // 组装SendCommandBean
					SendCommandBean sendCommandBean = failureBean.getScb();
		            sendCommandBean.setSeq(seq);

		            Long sendTime = (new Date()).getTime();
		            
		            CommandBean commandBean = SendCommandService.getCommandStr(sendCommandBean);
		            SendCommandService.msgUrl=sendCommandUrl;
		          //调用接口进行下发
					boolean resultFlag = SendCommandService.sendCommand(commandBean.getCommand()+vid);
					
					// 根据指令发送服务返回结果处理本方法返回结果和发送状态
					Integer result =-1;
					if(resultFlag){
						result=2;
					}
					
					/*if (!resultFlag){
						//把指令存入发送失败队列,等待重新发送
						failureQueue.offer(failureBean);
					}*/
					saveCommandPstmt.setString(1,GeneratorPK.instance().getPKString());
					saveCommandPstmt.setString(2, userId);
					saveCommandPstmt.setString(3, vehicleNo);
					saveCommandPstmt.setLong(4, sendTime);
					saveCommandPstmt.setString(5,commandBean.getCoType());
					saveCommandPstmt.setInt(6, COMMAND_FROM_TYPE_PLATFORM);
					saveCommandPstmt.setString(7, seq);
					saveCommandPstmt.setString(8, commandBean.getCoChannel());
					saveCommandPstmt.setString(9, commandBean.getCoParm());
					saveCommandPstmt.setString(10, commandBean.getCommand());
					saveCommandPstmt.setInt(11, result);
					saveCommandPstmt.setNull(12, Types.VARCHAR);
					saveCommandPstmt.setNull(13, Types.INTEGER);
					saveCommandPstmt.setString(14, oemCode);
					saveCommandPstmt.setInt(15, 1);
					saveCommandPstmt.setInt(16, 1);
					saveCommandPstmt.setString(17, commandBean.getCoSubtype());
					saveCommandPstmt.setString(18, userId.toString());
					saveCommandPstmt.setString(19, vid);
					saveCommandPstmt.setNull(20, Types.VARCHAR);
					
					saveCommandPstmt.execute();
					
					//保存或更新发送日志
					saveSettingLogPstmt.setString(1, vid);
					saveSettingLogPstmt.setString(2, detailId);
					saveSettingLogPstmt.setString(3, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(4,seq);
					saveSettingLogPstmt.setString(5, "1");
					saveSettingLogPstmt.setString(6, vid);
					saveSettingLogPstmt.setString(7,GeneratorPK.instance().getPKString());
					saveSettingLogPstmt.setString(8, getSendStatusByBool(resultFlag));
					saveSettingLogPstmt.setString(9,seq);
					saveSettingLogPstmt.setString(10, userId);
					saveSettingLogPstmt.setString(11, "1");
					saveSettingLogPstmt.setString(12, "1");
					
					saveSettingLogPstmt.execute();
				
			}else{
				logger.debug("获取数据库链接失败");
			}
			}
		} catch (Exception e) {
			logger.error("触发拍照设置指令重新下发出错：", e);
			// flag = 0;
		} finally {
			try {
				if(saveCommandPstmt != null){
					saveCommandPstmt.close();
				}
				if(saveSettingLogPstmt != null){
					saveSettingLogPstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
		return flag;
	}
	
	
	public Date queryNextJobTime() {
		PreparedStatement dbPstmt = null;
		Connection dbConnection = null;
		
		Date jobTime=null;
		// 结果集对象
		ResultSet dbResultSet = null;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				//查询当前要下发的终端参数信息
				dbPstmt = dbConnection.prepareStatement(queryNextJobTimeSql);
				dbResultSet = dbPstmt.executeQuery();
				
				while (dbResultSet.next()) {
					jobTime = dbResultSet.getTimestamp("JOBTIME");
					if (jobTime.after(new Date())){
						break;
					}
				}
				
			}else{
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("自动扩展表分区出错：", e);
			// flag = 0;
		} finally {
			try {
				if(dbResultSet != null){
					dbResultSet.close();
				}
				if(dbPstmt != null){
					dbPstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
		return jobTime;
	}
	
	/**
	 * 根据发送指令对象的字段获取指令发送状态
	 */
	private String getSendStatusByBool(boolean sendOk) {
		String statusStr = "";
		if(sendOk) {
			statusStr = "-1"; // 发送成功是等待状态
		} else {
			statusStr = "1";
		}
		return statusStr;
	}
	
	

	/**
	 * 将空值转换为空字符串
	 * 
	 * @param str
	 *            字符串
	 * @return String 返回处理后的字符串
	 */
	public static String nullToStr(String str) {
		return str == null || str.equals("null") ? "" : str.trim();
	}

	
	/**
	 * 失败命令重发线程
	 * @author yujch
	 *
	 */
	class ReSendCommandThread implements Runnable {
		
		@Override
		public void run() {
			logger.info("失败指令重发任务加载成功！");
 			while(true){
				try {
					FailureSendCommandBean fscb = (FailureSendCommandBean)failureQueue.take();
					if (fscb!=null){
						//进行重新发送
						reSendCommandToSetting(fscb);
					}
					Thread.sleep(30*1000);//每个发送间隔30秒
				} catch (InterruptedException e) {
 					e.printStackTrace();
					//当出现异常时重新启动此线程
					this.run();
				}
			}
			
			
		}
	}

	@Override
	public void costTime() {
		
	}
	
	public static void main(String args[]){
		System.out.println(CDate.getUserDate("yyyy").substring(0,2));
	}

}
