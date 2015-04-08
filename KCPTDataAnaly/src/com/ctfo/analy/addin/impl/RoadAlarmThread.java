/***
 * 道路等级报警处理
 * LiangJian
 * 2012年12月7日10:50:12
 */
package com.ctfo.analy.addin.impl;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.AreaAlarmBean;
import com.ctfo.analy.beans.LineAlarmBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.RoadAlarmBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.CustTipConfig;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.protocal.CommonAnalyseService;
import com.ctfo.analy.util.Base64_URl;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.ExceptionUtil;
import com.ctfo.analy.util.GetAddressUtil;
import com.ctfo.analy.util.MathUtils;
import com.lingtu.xmlconf.XmlConf;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;

/**
 * 道路等级报警处理
 * @author LiangJian
 * 2012年12月10日21:38:35
 */
public class RoadAlarmThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger.getLogger(RoadAlarmThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
//	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 是否运行标志
	public boolean isRunning = true;
	
	/**
	路段进行以下定义
	高速限速对应GIS返回的“高速公路”
	国道限速对应GIS返回的“国道”、“快速路”
	省道限速对应GIS返回的“省道”
	城区限速对应GIS返回的“主要路段”、“次要路段”
	其他限速对应GIS返回的其他道路等级
	
	接口返回：管理等级 1：高速公路、2：国道、3：快速路、4：省道、5：主要道路、6：次要道路、7：一般道路、8：出入目的地道路、9：系道路、10：步行道路
 */
	private final List<String> GAOSU = Arrays.asList("1");
	private final List<String> GUODAO = Arrays.asList("2","3");
	private final List<String> SHENGDAO = Arrays.asList("4");
	private final List<String> CHENGQU = Arrays.asList("5","6");
	
	/** 缓存，为了记录每次请求时间点，及保存和更新是所用到的告警ID */
	private Map<String, String> tmpCache = new ConcurrentHashMap<String, String>();
	private Map<String, AlarmCacheBean> roadCache = new ConcurrentHashMap<String, AlarmCacheBean>();
	
	private Map<String, String> warningCache = new ConcurrentHashMap<String, String>();

	/** 高速-记录起始时间所用的Key */
	private final String _GAOSU_UTC = "_GAOSU_UTC";
	/** 国道-记录起始时间所用的Key */
	private final String _GUODAO_UTC = "_GUODAO_UTC";
	/** 省道-记录起始时间所用的Key */
	private final String _SHENGDAO_UTC = "_SHENGDAO_UTC";
	/** 城区-记录起始时间所用的Key */
	private final String _CHENGQU_UTC = "_CHENGQU_UTC";
	/** 其他-记录起始时间所用的Key */
	private final String _QITA_UTC = "_QITA_UTC";
	
	public RoadAlarmThread() {
	}

	/**
	 * 初始化方法
	 */
	public void initAnalyser(int nId, XmlConf config, String nodeName)throws Exception {
		this.nId = nId;
		this.config = config;
		this.nodeName = nodeName;

		start();

		oracleDBAdapter = new OracleDBAdapter();
		oracleDBAdapter.initDBAdapter(config, nodeName);
//		mysqlDBAdapter = new MysqlDBAdapter();
//		mysqlDBAdapter.initDBAdapter(config, nodeName);
		
		redisDBAdapter = new RedisDBAdapter();
	}

	@Override
	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void endAnalyser() {

	}

	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	public void run() {
		logger.debug("【道路等级报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
				int isPValid = CommonAnalyseService.isPValid(vehicleMessage.getLon(), vehicleMessage.getLat(), vehicleMessage.getUtc(), vehicleMessage.getSpeed(), vehicleMessage.getDir());
				boolean isAllowAnaly = isAllowAnaly(vehicleMessage.getCommanddr());
				if(isPValid==0&&isAllowAnaly){
					
						logger.debug("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],线程[" + nId + "]【道路等级】【收到数据】>道路等级报警数据[" + vehicleMessage.getCommanddr() + "]nodeName:"+nodeName+";GPS时间："+vehicleMessage.getUtc()+";当前车速："+vehicleMessage.getSpeed());
						// 判断并记录报警信息
						checkAlarmnew(vehicleMessage);
					
				}else{
					String msg = "";
					if (isPValid!=0){
						msg += "不合法车辆轨迹;";
					}
					if (!isAllowAnaly){
						msg += "企业不进行线路告警分析;";
					}
					logger.debug(msg+"[" + vehicleMessage.getCommanddr()+"]");
				}
				}
			} catch (Exception e) {
				logger.error("DLDJ-道路等级日志-分析过程中出错；"+e.getMessage(),e);
			}
		}
	}

	/**
	 * 查询点所在线路
	 * @param lon
	 * @param lat
	 * @return
	 */
	private List<String> queryLineList(Long lon,Long lat){
		Geometry point = new GeometryFactory().createPoint(new Coordinate(lon / 600000.0,	lat / 600000.0));
		return TempMemory.getLineTree(point.getEnvelopeInternal());
	}
	
	/***
	 * 查看该车是否符合在线路内
	 * @param vehicleMessage
	 * @return true:在线路内，false:不在线路内
	 */
	private boolean isCurrentinArea(VehicleMessageBean vehicleMessage){
		String vid = vehicleMessage.getVid();
		List<LineAlarmBean> lineAlarmList = TempMemory.getLineAlarmMap(vid);// 根据vid查询车辆的线路信息集合
		boolean lineAlarmBoolean = false;//标示该车是否在线路内，true:在线路内，false:不在线路内。在线路内时，道路等级不做报警验证
		if(lineAlarmList==null){
			//该车没有被绑定线路告警配置
			lineAlarmBoolean = false;//不在线路内
		}else{
			//该车被绑定了线路告警配置
			//继续判断该车是否在线路内，如果在围栏内则不做处理
			List<String> inarealist =queryLineList(vehicleMessage.getLon(),vehicleMessage.getLat());//查询点所在线路
			for (LineAlarmBean lineAlarmBean : lineAlarmList) {//判断用户设置
				String mapkey=String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid());
                if (vehicleMessage.getUtc() >= lineAlarmBean.getBeginTime()&& vehicleMessage.getUtc() <= lineAlarmBean.getEndTime()) {// 在线路判定周期内
					//判断当前是否在线路内
					boolean iscurrentinarea = false;
					for(String inarea:inarealist){
						if(inarea.equals(mapkey)){
							iscurrentinarea=true;
							break;
						}
					}
					if(iscurrentinarea){
						lineAlarmBoolean = true;//在线路内
						break;
					}
                }
             }
		}
		return lineAlarmBoolean;
	}
	
	/**
	 * 查询车辆当前是否在判断生效围栏内
	 * @param vehicleMessage
	 * @return
	 */
	private boolean isInArea(VehicleMessageBean vehicleMessage){
		AlarmCacheBean areaAlarmCacheBean = null;// 车围栏缓存
		boolean iscurrentinarea=false;
		try{
		String vid = vehicleMessage.getVid();
		List<AreaAlarmBean> areaList = TempMemory.getAreaAlarmMap(vid);// 根据vid查询车辆的围栏信息集合
		if (areaList != null) {
			areaList = new ArrayList<AreaAlarmBean>(areaList);
			List<String> inarealist =queryAreaList(vehicleMessage.getLon(),vehicleMessage.getLat());//查询点所在围栏
			for (AreaAlarmBean areaAlarmBean : areaList) {//判断用户设置
				String mapkey=String.valueOf(vid) + "_"	+ String.valueOf(areaAlarmBean.getAreaid());
				
				logger.debug("DLDJ_开始处理围栏："+areaAlarmBean.getAreaName()+";mapkey:"+mapkey+";inarealist:[+inarealist+];areaAlarmCacheBean:"+areaAlarmCacheBean+";"+(inarealist.contains(mapkey)?"当前坐标【在】围栏内":"当前坐标【不在】围栏内"));
				
                if (vehicleMessage.getUtc() >= areaAlarmBean.getBeginTime()&& vehicleMessage.getUtc() <= areaAlarmBean.getEndTime()) {// 在围栏判定周期内
						//判断当前是否在围栏内
						for(String inarea:inarealist){
							if(inarea.equals(mapkey)){
								iscurrentinarea=true;
								break;
							}
						}
                }
			}
		}
		}catch(Exception ex){
			logger.error("DLDJ 判断车辆当前是否在围栏内出错"+ex.getMessage(),ex);
		}
		return iscurrentinarea;
	}
	
	/**
	 * 查询点所在围栏
	 * @param lon
	 * @param lat
	 * @return
	 */
	private List<String> queryAreaList(Long lon,Long lat){
		Geometry point = new GeometryFactory().createPoint(new Coordinate(lon / 600000.0,lat / 600000.0));
		return TempMemory.getAreaTree(point.getEnvelopeInternal());
	}

	/**
	 * 验证是否超速
	 * @param vehicleMessage 位置信息
	 * @param roadAlarmBean 配置信息
	 * @param flag 1：高速，2：国道，3：省道，4：县道，5：其他
	 * @return true:超速，false:没超速
	 */
	private boolean isOverspeed(VehicleMessageBean vehicleMessage,RoadAlarmBean roadAlarmBean,String key,int flag){
		boolean boo = false;
		if(1==flag){//高速
//			String key = vehicleMessage.getVid()+_GAOSU_UTC;
			//高速超速没存过，且本次速度大于配置速度，否则不超速，准备更新（更新条件：超速已存且本次速度小于配置速度）
			if(vehicleMessage.getSpeed()>roadAlarmBean.getEw_speed_limit()){
				if(null!=tmpCache&&null==tmpCache.get(key)){
					tmpCache.put(key,""+vehicleMessage.getUtc());
				}
				if((vehicleMessage.getUtc()-Long.parseLong(tmpCache.get(key)))>roadAlarmBean.getEw_continue_limit()*1000){
					boo = true;
				}
			}else{
				tmpCache.remove(key);
			}
		}else
		if(2==flag){//国道
//			String key = vehicleMessage.getVid()+_GUODAO_UTC;
			if(vehicleMessage.getSpeed()>roadAlarmBean.getNr_speed_limit()){
				if(null!=tmpCache&&null==tmpCache.get(key)){
					tmpCache.put(key,""+vehicleMessage.getUtc());
				}
				if((vehicleMessage.getUtc()-Long.parseLong(tmpCache.get(key)))>roadAlarmBean.getNr_continue_limit()*1000){
					boo = true;
				}
			}else{
				tmpCache.remove(key);
			}
		}else
		if(3==flag){//省道
//			String key = vehicleMessage.getVid()+_SHENGDAO_UTC;
			if(vehicleMessage.getSpeed()>roadAlarmBean.getPr_speed_limit()){
				if(null!=tmpCache&&null==tmpCache.get(key)){
					tmpCache.put(key,""+vehicleMessage.getUtc());
				}
				if((vehicleMessage.getUtc()-Long.parseLong(tmpCache.get(key)))>roadAlarmBean.getPr_continue_limit()*1000){
					boo = true;
				}
			}else{
				tmpCache.remove(key);
			}
		}else
		if(4==flag){//城区
//			String key = vehicleMessage.getVid()+_CHENGQU_UTC;
			if(vehicleMessage.getSpeed()>roadAlarmBean.getCr_speed_limit()){
				if(null!=tmpCache&&null==tmpCache.get(key)){
					tmpCache.put(key,""+vehicleMessage.getUtc());
				}
				if((vehicleMessage.getUtc()-Long.parseLong(tmpCache.get(key)))>roadAlarmBean.getCr_continue_limit()*1000){
					boo = true;
				}
			}else{
				tmpCache.remove(key);
			}
		}else
		if(5==flag){//其他
//			String key = vehicleMessage.getVid()+_QITA_UTC;
			if(vehicleMessage.getSpeed()>roadAlarmBean.getOr_speed_limit()){
				if(null!=tmpCache&&null==tmpCache.get(key)){
					tmpCache.put(key,""+vehicleMessage.getUtc());
				}
				if((vehicleMessage.getUtc()-Long.parseLong(tmpCache.get(key)))>roadAlarmBean.getOr_continue_limit()*1000){
					boo = true;
				}
			}else{
				tmpCache.remove(key);
			}
		}
		return boo;
	}
	
	/**
	 * 存储报警
	 * @param vehicleMessage
	 * @param alarmAddInfo 描述
	 */
	private void saveAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean){
		try{
			//查询当前驾驶员信息
			String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
			if (driverinfoStr!=null&&driverinfoStr.length()>0){
				String driverInfo[]= driverinfoStr.split(":");
				vehicleMessage.setDriverId(driverInfo[1]);
				vehicleMessage.setDriverName(driverInfo[2]);
				vehicleMessage.setDriverSrc(driverInfo[10]);
			}
			
			oracleDBAdapter.saveVehicleAlarm(vehicleMessage,alarmCacheBean);
//			mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,alarmCacheBean);
			redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,alarmCacheBean);
			
			logger.debug("DLDJ-道路等级日志-线程:["+nId+"]【道路等级】【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+alarmCacheBean.getAlarmId()+";"+alarmCacheBean.getAlarmaddInfo()+"");
		}catch(Exception e){
			logger.error("DLDJ-道路等级日志-道路等级-存储-数据库异常"+e.getMessage(),e);
		}
	}

	/**
	 * 更新报警
	 * @param vehicleMessage
	 * @param alarmAddInfo 描述
	 */
	private void updateAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean){
		try{
			oracleDBAdapter.updateVehicleAlarm(vehicleMessage,alarmCacheBean);
//			mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,alarmCacheBean);
			redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,alarmCacheBean);
			logger.debug("DLDJ-道路等级日志-线程:["+nId+"]【道路等级】【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+alarmCacheBean.getAlarmId()+";"+alarmCacheBean.getAlarmaddInfo()+"");
		}catch(Exception e){
			logger.error("DLDJ-道路等级日志-道路等级-更新-数据库异常"+e.getMessage(),e);
		}
	}
	
	/**
	 * 信息下发
	 * @param msgid
	 * @param sendcommand
	 */
	private void sendMessage(String msgid,String sendcommand){
		MessageBean message = new MessageBean();
		message.setMsgid(msgid);
		message.setCommand(sendcommand);
		DataPool.setReceivePacket(message);
	}
	
	/**
	 * 道路等级-报警处理 新逻辑
	 * 
	 * @param vehicleMessage
	 * @throws SQLException
	 */
	private void checkAlarmnew(VehicleMessageBean vehicleMessage){
		try{
		String vid = vehicleMessage.getVid();
		String keyIsSave = vid+"_isSave";
		
		//由于上报的位置信息有补报的时间，这样的时间点对于的位置信息不做验证。
		String keyVidUtc = vid+"_utc";
		if(tmpCache.get(keyVidUtc)==null){
			tmpCache.put(keyVidUtc, ""+vehicleMessage.getUtc());
		}
		//判断当前点时间是否晚于上一次时间，排除补报数据
		if(vehicleMessage.getUtc()-(Long.parseLong(tmpCache.get(keyVidUtc)))>=0){
			//更新缓存最后时间
			tmpCache.put(keyVidUtc,""+vehicleMessage.getUtc());
			
			List<RoadAlarmBean> roadAlarmList = TempMemory.getRoadAlarmMap(vid);//根据vid查询车辆道路配置信息
			if(roadAlarmList!=null){//如果该车被绑定道路等级配置
				boolean isInArea =this.isInArea(vehicleMessage) ;//查看该车是否在围栏内
				boolean boo = isCurrentinArea(vehicleMessage);//查看该车是否符合在线路内
				
				//判断车辆当前定位状态，如果当前未定位则不进行道路等级超速判断,并结束之前缓存的超速告警
				String baseStatus = MathUtils.getBinaryString(vehicleMessage.getBaseStatus()==null?"0":vehicleMessage.getBaseStatus());
				
				logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"],该车:"+(boo?"【在】":"【不在】")+"线路内 ,该车:"+(isInArea?"【在】":"【不在】")+"围栏内 "+roadAlarmList.size()+" 定位状态："+check("1",baseStatus));
				int step = 1;
				if(!isInArea&&!boo&&check("1",baseStatus)){
					//判断是否满足道路等级配置
					RoadAlarmBean roadAlarmBean = roadAlarmList.get(0);
					logger.info("if语句执行到第" + (step++) + "步 isInArea");
					if (roadAlarmBean!=null){
						logger.info("if语句执行到第" + (step++) + "步 roadAlarmBean");
					//for(RoadAlarmBean roadAlarmBean : roadAlarmList){
						Long mapLon = vehicleMessage.getMaplon();//偏移后经度
						Long mapLat = vehicleMessage.getMaplat();//偏移后纬度
						int dir = vehicleMessage.getDir();//方向
						//判断该车当前所在道路等级（调用获取道路等级接口）
						logger.info("if语句执行到第" + (step++) + "步 dir");
						String tmpAddressNo = GetAddressUtil.getAddressNo(mapLon/600000.0, mapLat/600000.0,dir);
						logger.info("if语句执行到第" + (step++) + "步 tmpAddressNo");
						//道路等级转换
						String addressNo = convertRoadLevel(tmpAddressNo);
						logger.info("if语句执行到第" + (step++) + "步 addressNo");
						
						//是否超速
						boolean isOverSpeed = this.isOverspeednew(vehicleMessage, roadAlarmBean,addressNo);
						logger.info("if语句执行到第" + (step++) + "步 isOverSpeed");
						//根据车速发送预警消息
						//是否达到预警值
						if (!isOverSpeed){
							logger.info("if语句执行到第" + (step++) + "步 isOverSpeed");
							String flag = warningCache.get(keyIsSave);
							if (flag==null){
								logger.info("if语句执行到第" + (step++) + "步 flag");
								boolean isWarning = this.isOverspeednewWarning(vehicleMessage, roadAlarmBean, addressNo);
								if (isWarning){
									logger.info("if语句执行到第" + (step++) + "步 isWarning");
									//达到预警阀值，向海泰终端发送预警短信
									logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"],道路等级："+tmpAddressNo+"("+addressNo+"); 超速预警信息下发！");
									sendWarning(vehicleMessage,""+roadAlarmBean.getEnt_id());
									logger.info("if语句执行到第" + (step++) + "步 vehicleMessage");
									warningCache.put(keyIsSave,"1");
								}
							}else{
								logger.info("if语句执行到第" + (step++) + "步 else");
								boolean isWarningRelease = this.isOverspeedWarningRelease(vehicleMessage, roadAlarmBean, addressNo);
								if (isWarningRelease){
									logger.info("if语句执行到第" + (step++) + "步 isWarningRelease");
									warningCache.remove(keyIsSave);
								}
							}
						}
						logger.info("if语句执行到第" + (step++) + "步 2222");
						logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"],经纬度:["+mapLon+","+mapLat+","+dir+"]道路等级："+tmpAddressNo+"("+addressNo+"); 是否超速："+isOverSpeed+"管理等级 1：高速公路、2：国道、3：快速路、4：省道、5：主要道路、6：次要道路、7：一般道路、8：出入目的地道路、9：系道路、10：步行道路");

						AlarmCacheBean roadCacheBean = roadCache.get(keyIsSave);
						if(null!=roadCacheBean){//如果缓存中已存在报警
							//缓存非空时添加最大车速
							logger.info("if语句执行到第" + (step++) + "步 roadCacheBean");
							if (roadCacheBean.getMaxSpeed()>0){
								logger.info("if语句执行到第" + (step++) + "步 getMaxSpeed()>0");
								if (vehicleMessage.getSpeed()!=null&&vehicleMessage.getSpeed()>roadCacheBean.getMaxSpeed()){
									logger.info("if语句执行到第" + (step++) + "步 getSpeed()!=null");
									roadCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
								}
							}else{
								logger.info("if语句执行到第" + (step++) + "步 333");
								roadCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
							}
							
							//计算平均车速
							if (isOverSpeed){
								logger.info("if语句执行到第" + (step++) + "步 isOverSpeed");
								roadCacheBean.setAvgSpeed(vehicleMessage.getSpeed());
							}
							
							//缓存非空，证明之前已存在超速判断，这时如果道路等级切换或状态为不超速，则需要结束之前缓存的告警
							if (!roadCacheBean.getAddressNo().equals(addressNo)||!isOverSpeed){
								logger.info("if语句执行到第" + (step++) + "步 roadCacheBean.getAddressNo()");
								//结束告警 之前已存在的告警
								Long alarmTime = vehicleMessage.getUtc()-roadCacheBean.getBegintime();
								
								if (alarmTime>roadCacheBean.getBufferTime()*1000){
									logger.info("if语句执行到第" + (step++) + "步 alarmTime>roadCacheBean.getBufferTime()*1000");
									//对于超过报警持续时间但没有保存的报警，要先保存，后更新。
									/*if (!roadCacheBean.isSaved()){
										roadCacheBean.setBeginVmb(vehicleMessage);//告警起始时间需要去除告警缓冲时间
										dealSaveAlarm(roadCacheBean,""+roadAlarmBean.getEnt_id());
									}*/
									//只结束已经产生的告警，如果此告警刚好达到报警阀值并结束，则不进行保存
									if (roadCacheBean.isSaved()){
										logger.info("if语句执行到第" + (step++) + "步 roadCacheBean.isSaved()");
										roadCacheBean.setEndVmb(vehicleMessage);
										dealUpdateAlarm(roadCacheBean,vehicleMessage);
										logger.info("if语句执行到第" + (step++) + "步 dealUpdateAlarm");
									}
								}
								//清除之前的告警缓存
								roadCache.remove(keyIsSave);
								logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"],报警持续时间("+alarmTime+"ms) 阀值("+roadCacheBean.getBufferTime()*1000+"ms), 道路等级切换或状态为不超速，告警结束");
							}else{
								logger.info("if语句执行到第" + (step++) + "步 444");
								//继续进行告警判断
								//告警是否达到报警触发条件
								Long alarmTime = vehicleMessage.getUtc()-roadCacheBean.getBegintime();
								if (alarmTime>=roadCacheBean.getBufferTime()*1000){
									logger.info("if语句执行到第" + (step++) + "步 alarmTime>=roadCacheBean.getBufferTime()*1000");
									//达到报警条件，报警入库，并发提示给终端
									if (!roadCacheBean.isSaved()){

										roadCacheBean.setBeginVmb(vehicleMessage);
										
										dealSaveAlarm(roadCacheBean,""+roadAlarmBean.getEnt_id());
										roadCacheBean.setSaved(true);
										logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"], 报警持续时间("+alarmTime+"ms) 阀值("+roadCacheBean.getBufferTime()*1000+"ms),保存报警信息完成");
									}
								}else{
									//未达到报警条件，什么都不做
								}
							}
						}else{
							//缓存中不存在报警，对当前报警信息进行缓存
							//缓存开始点对象
							logger.info("if语句执行到第" + (step++) + "步 555");
							if (isOverSpeed){
	
								String alarmId = UUID.randomUUID().toString().replace("-", "");
								AlarmCacheBean rcb = new AlarmCacheBean();
								if("1".equals(addressNo)){
									rcb.setAddressNo("1");
									rcb.setConfigId2(roadAlarmBean.getConfig_id());
									rcb.setConfigName(roadAlarmBean.getConfig_name());
									rcb.setLimitSpeed(roadAlarmBean.getEw_continue_limit());
									rcb.setBufferTime(roadAlarmBean.getEw_continue_limit());
//									vehicleMessage.setSpeedThreshold(roadAlarmBean.getEw_continue_limit());
								}else if("2".equals(addressNo)){
									rcb.setAddressNo("2");
									rcb.setConfigId2(roadAlarmBean.getConfig_id());
									rcb.setConfigName(roadAlarmBean.getConfig_name());
									rcb.setLimitSpeed(roadAlarmBean.getNr_continue_limit());
									rcb.setBufferTime(roadAlarmBean.getNr_continue_limit());
//									vehicleMessage.setSpeedThreshold(roadAlarmBean.getNr_continue_limit());
								}else if("3".equals(addressNo)){
									rcb.setAddressNo("3");
									rcb.setConfigId2(roadAlarmBean.getConfig_id());
									rcb.setConfigName(roadAlarmBean.getConfig_name());
									rcb.setLimitSpeed(roadAlarmBean.getPr_continue_limit());
									rcb.setBufferTime(roadAlarmBean.getPr_continue_limit());
//									vehicleMessage.setSpeedThreshold(roadAlarmBean.getPr_continue_limit());
								}else if("4".equals(addressNo)){
									rcb.setAddressNo("4");
									rcb.setConfigId2(roadAlarmBean.getConfig_id());
									rcb.setConfigName(roadAlarmBean.getConfig_name());
									rcb.setLimitSpeed(roadAlarmBean.getCr_continue_limit());
									rcb.setBufferTime(roadAlarmBean.getCr_continue_limit());
//									vehicleMessage.setSpeedThreshold(roadAlarmBean.getCr_continue_limit());
								}else{
									rcb.setAddressNo("5");
									rcb.setConfigId2(roadAlarmBean.getConfig_id());
									rcb.setConfigName(roadAlarmBean.getConfig_name());
									rcb.setLimitSpeed(roadAlarmBean.getOr_continue_limit());
									rcb.setBufferTime(roadAlarmBean.getOr_continue_limit());
//									vehicleMessage.setSpeedThreshold(roadAlarmBean.getOr_continue_limit());
								}
								rcb.setBeginVmb(vehicleMessage);
								rcb.setBegintime(vehicleMessage.getUtc());
								rcb.setSpeedThreshold(vehicleMessage.getSpeedThreshold());
								rcb.setAlarmId(alarmId);
								rcb.setAlarmSrc(4);
								
								//计算平均车速
								rcb.setAvgSpeed(vehicleMessage.getSpeed());
								logger.info("if语句执行到第" + (step++) + "步 rcb.setAvgSpeed(vehicleMessage.getSpeed())");
								roadCache.put(keyIsSave, rcb);
								
								logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"], 缓存报警信息 alarmId:["+alarmId+"] 完成");
							}
						}

					}
				}else{
					logger.info("else语句执行到第" + (step++) + "步");
					//车辆行驶到线路内，结束之前产生的告警
					if(null!=roadCache.get(keyIsSave)){//如果缓存中已存在报警
						//缓存非空，证明之前已存在超速判断，这时如果道路等级切换或状态为不超速，则需要结束之前缓存的告警
						AlarmCacheBean roadCacheBean = roadCache.get(keyIsSave);
						logger.info("else语句执行到第" + (step++) + "步");
						//结束告警
						if (roadCacheBean.isSaved()){
							roadCacheBean.setEndVmb(vehicleMessage);
							logger.info("else语句执行到第" + (step++) + "步");
							dealUpdateAlarm(roadCacheBean,vehicleMessage);
						}
						//清除之前的告警缓存
						roadCache.remove(keyIsSave);
						logger.info("else语句执行到第" + (step++) + "步");
						logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"], 车辆进入线路，道路等级告警结束");
					}
				}
			}
		}else{
			logger.info("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vid+"],该车:上报的时间小于上一次上报的时间，本条位置信息丢弃。当前GPS时间："+vehicleMessage.getUtc()+",上一次GPS时间："+tmpCache.get(keyVidUtc));
		}
		}catch(Exception ex){
			logger.error("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vehicleMessage.getVid()+"],该车:道路等级超速报警分析过程中出错！"+ExceptionUtil.getErrorStack(ex, 0) + ex + ex.getMessage() + ex.getLocalizedMessage() + ex.fillInStackTrace());
		}
	}

	/**
	 * 道路等级转换 由原来的10级转换为5级
	 * @param addressNo
	 * @return
	 */
	private String convertRoadLevel(String addressNo){
		String lvl = "5";
		if(GAOSU.contains(addressNo)){
			lvl = "1";
		}else if(GUODAO.contains(addressNo)){
			lvl = "2";
		}else if(SHENGDAO.contains(addressNo)){
			lvl = "3";
		}else if(CHENGQU.contains(addressNo)){
			lvl = "4";
		}else{
			lvl = "5";
		}
		return lvl;
	}
	
	
	/**
	 * 验证是否超速
	 * @param vehicleMessage 位置信息
	 * @param roadAlarmBean 配置信息
	 * @param flag 1：高速，2：国道，3：省道，4：县道，5：其他
	 * @return true:超速，false:没超速
	 */
	private boolean isOverspeednew(VehicleMessageBean vehicleMessage,RoadAlarmBean roadAlarmBean,String addressNo){
		boolean boo = false;
		OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
		double speedScale = 1.0;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			speedScale = tmpBean.getSpeedScale()/100.0;
		}
		

		if("1".equals(addressNo)){//高速
			//高速超速没存过，且本次速度大于配置速度，否则不超速，准备更新（更新条件：超速已存且本次速度小于配置速度）
			double threshold = roadAlarmBean.getEw_speed_limit()*speedScale;
			logger.info("threshold:"+threshold +" speedScale:"+speedScale);
			if(vehicleMessage.getSpeed()>threshold){
					boo = true;
					vehicleMessage.setSpeedThreshold(threshold);
			}
		}else if("2".equals(addressNo)){//国道
			double threshold =  roadAlarmBean.getNr_speed_limit()*speedScale;
			if(vehicleMessage.getSpeed()>threshold){
					boo = true;
					vehicleMessage.setSpeedThreshold(threshold);
			}
		}else if("3".equals(addressNo)){//省道
			double threshold = roadAlarmBean.getPr_speed_limit()*speedScale;
			if(vehicleMessage.getSpeed()>threshold){
					boo = true;
					vehicleMessage.setSpeedThreshold(threshold);
				}
		}else if("4".equals(addressNo)){//城区
			double threshold = roadAlarmBean.getCr_speed_limit()*speedScale;
			if(vehicleMessage.getSpeed()>threshold){
					boo = true;
					vehicleMessage.setSpeedThreshold(threshold);
				}
		}else if("5".equals(addressNo)){//其他
			double threshold =  roadAlarmBean.getOr_speed_limit()*speedScale;
			if(vehicleMessage.getSpeed()>threshold){
					boo = true;
					vehicleMessage.setSpeedThreshold(threshold);
				}
		}
		logger.info("[commaddr:"+vehicleMessage.getCommanddr()+"]threshold:"+vehicleMessage.getSpeedThreshold() +" speedScale:"+speedScale);
		return boo;
	}
	
	private void dealSaveAlarm(AlarmCacheBean roadCacheBean,String entId){
		try{
		VehicleMessageBean vehicleMessage = roadCacheBean.getBeginVmb();
		
		roadCacheBean.setAlarmcode("1");//告警编码
		roadCacheBean.setAlarmlevel("A001");//告警级别
		roadCacheBean.setAlarmSrc(4);
		String alarmAddInfo = "名称："+roadCacheBean.getConfigName()+",类型：道路等级";
		if ("1".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【高速限速】报警";
		}else if ("2".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【国道限速】报警";
		}else if ("3".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【省道限速】报警";
		}else if ("4".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【城区限速】报警";
		}else if ("5".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【其他限速】报警";
		}
				
		roadCacheBean.setAlarmaddInfo(alarmAddInfo);
		saveAlarm(vehicleMessage,roadCacheBean);//存储
		
		if (CustTipConfig.getInstance().isSendToTerminal()&&("156044".equals(entId)||"157183".equals(entId))){//下发提示信息给终端
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_OVERSPEED);
			logger.debug("线程:["+nId+"]道路等级超速报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
		}
		}catch(Exception ex){
			logger.error("DLDJ-道路等级日志-["+roadCacheBean.getBeginVmb().getCommanddr()+"],vid:["+roadCacheBean.getBeginVmb().getVid()+"], 道路等级告警【保存】出错！"+ex.getMessage(),ex);
		}
	}
	
	private void dealSaveAlarm(AlarmCacheBean roadCacheBean){
		try{

			roadCacheBean.setAlarmcode("1");//告警编码
			roadCacheBean.setAlarmlevel("A001");//告警级别
			roadCacheBean.setAlarmSrc(4);
		String alarmAddInfo = "名称："+roadCacheBean.getConfigName()+",类型：道路等级";
		if ("1".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【高速限速】报警";
		}else if ("2".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【国道限速】报警";
		}else if ("3".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【省道限速】报警";
		}else if ("4".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【城区限速】报警";
		}else if ("5".equals(roadCacheBean.getAddressNo())){
			alarmAddInfo += "【其他限速】报警";
		}
				
		roadCacheBean.setAlarmaddInfo(alarmAddInfo);
		saveAlarm(roadCacheBean.getBeginVmb(),roadCacheBean);//存储
		
		if (CustTipConfig.getInstance().isSendToTerminal()){//下发提示信息给终端
			TerminalAlarmThread.checkIsHasALarmNotice(roadCacheBean.getBeginVmb(),Constant.ALARMCODE_OVERSPEED);
			logger.debug("线程:["+nId+"]道路等级超速报警发送终端成功[" + roadCacheBean.getBeginVmb().getCommanddr() + "]");
		}
		}catch(Exception ex){
			logger.error("DLDJ-道路等级日志-["+roadCacheBean.getBeginVmb().getCommanddr()+"],vid:["+roadCacheBean.getBeginVmb().getVid()+"], 道路等级告警【保存】出错！"+ex.getMessage(),ex);
		}
	}
	
	private void dealUpdateAlarm(AlarmCacheBean roadCacheBean,VehicleMessageBean vehicleMessage){
		try{
			updateAlarm(vehicleMessage,roadCacheBean);
			saveAlarmEvent(vehicleMessage,roadCacheBean);
			
		}catch(Exception ex){
			logger.error("DLDJ-道路等级日志-["+roadCacheBean.getBeginVmb().getCommanddr()+"],vid:["+roadCacheBean.getBeginVmb().getVid()+"], 道路等级告警【更新】出错！"+ex.getMessage(),ex);
		}
	}
	
	/**
	 * 判断二进制某位是否是1或0
	 * @param args
	 */
	private boolean check(String num, String result) {
		logger.info("vehicleMessage.getBaseStatus() : " +result );
		boolean bool = false;
		if (result.matches(".*0\\d{"+ num +"}")) { 
			bool = false;
		}
		if (result.matches(".*1\\d{"+ num +"}")) { 
			bool = true;
		}

		return bool;

	}
	
	/**
	 * 验证是否超速预警解除
	 * @param vehicleMessage 位置信息
	 * @param roadAlarmBean 配置信息
	 * @param flag 1：高速，2：国道，3：省道，4：县道，5：其他
	 * @return true:超速，false:没超速
	 */
	private boolean isOverspeedWarningRelease(VehicleMessageBean vehicleMessage,RoadAlarmBean roadAlarmBean,String addressNo){
		boolean boo = false;
		if("1".equals(addressNo)){//高速
			//高速超速没存过，且本次速度大于配置速度，否则不超速，准备更新（更新条件：超速已存且本次速度小于配置速度）
			if(vehicleMessage.getSpeed()<=(roadAlarmBean.getEw_speed_limit()-100)){	
					boo = true;
			}
		}else if("2".equals(addressNo)){//国道
			if(vehicleMessage.getSpeed()<=(roadAlarmBean.getNr_speed_limit()-100)){
					boo = true;
			}
		}else if("3".equals(addressNo)){//省道
			if(vehicleMessage.getSpeed()<=(roadAlarmBean.getPr_speed_limit()-100)){
					boo = true;
				}
		}else if("4".equals(addressNo)){//城区
			if(vehicleMessage.getSpeed()<=(roadAlarmBean.getCr_speed_limit()-100)){
					boo = true;
				}
		}else if("5".equals(addressNo)){//其他
			if(vehicleMessage.getSpeed()<=(roadAlarmBean.getOr_speed_limit()-100)){
					boo = true;
			}
		}
		return boo;
	}
	
	/**
	 * 验证是否超速
	 * @param vehicleMessage 位置信息
	 * @param roadAlarmBean 配置信息
	 * @param flag 1：高速，2：国道，3：省道，4：县道，5：其他
	 * @return true:超速，false:没超速
	 */
	private boolean isOverspeednewWarning(VehicleMessageBean vehicleMessage,RoadAlarmBean roadAlarmBean,String addressNo){
		boolean boo = false;
		if("1".equals(addressNo)){//高速
			//高速超速没存过，且本次速度大于配置速度，否则不超速，准备更新（更新条件：超速已存且本次速度小于配置速度）
			if(vehicleMessage.getSpeed()>(roadAlarmBean.getEw_speed_limit()-50)){	
					boo = true;
			}
		}else if("2".equals(addressNo)){//国道
			if(vehicleMessage.getSpeed()>(roadAlarmBean.getNr_speed_limit()-50)){
					boo = true;
			}
		}else if("3".equals(addressNo)){//省道
			if(vehicleMessage.getSpeed()>(roadAlarmBean.getPr_speed_limit()-50)){
					boo = true;
				}
		}else if("4".equals(addressNo)){//城区
			if(vehicleMessage.getSpeed()>(roadAlarmBean.getCr_speed_limit()-50)){
					boo = true;
				}
		}else if("5".equals(addressNo)){//其他
			if(vehicleMessage.getSpeed()>(roadAlarmBean.getOr_speed_limit()-50)){
					boo = true;
				}
		}
		return boo;
	}
	
	private void sendWarning(VehicleMessageBean vehicleMessage,String entId){
		try{
			logger.info("DLDJ_预警["+vehicleMessage.getCommanddr()+"]："+CustTipConfig.getInstance().isSendToTerminal()+" "+"156044".equals(entId)+" "+"157183".equals(entId));
		if (CustTipConfig.getInstance().isSendToTerminal()&&("156044".equals(entId)||"157183".equals(entId))){//下发提示信息给终端
			String sendcommand = "CAITS 0_0_0 " + vehicleMessage.getOemcode() + "_"
			+ vehicleMessage.getCommanddr()
			+ " 0 D_SNDM {TYPE:1,1:9,2:" + Base64_URl.base64Encode("你即将超速，请谨慎驾驶！") + "} \r\n"+vehicleMessage.getVid()+"";
			sendMessage(vehicleMessage.getMsgid(),sendcommand);
			logger.debug("DLDJ_线程:["+nId+"]" +
					"" +
					"[" + vehicleMessage.getCommanddr() + "]:道路等级预警信息下发成功！");
		}
		}catch(Exception ex){
			logger.error("DLDJ-道路等级日志-["+vehicleMessage.getCommanddr()+"],vid:["+vehicleMessage.getVid()+"], 道路等级预警信息下发出错！"+ex.getMessage(),ex);
		}
	}
	
	/**
	 * 存储驾驶行为事件数据
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void saveAlarmEvent(VehicleMessageBean vehicleMessage,
			AlarmCacheBean alarmCacheBean) {
			try {
				oracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,alarmCacheBean);
				logger.debug("线程:[" + nId + "]【DLDJ_道路等级软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ alarmCacheBean.getAlarmId());
			} catch (Exception e) {
				logger.error("Alarmid:" + alarmCacheBean.getAlarmId()
						+ "DLDJ_添加道路等级软报警驾驶行为事件-数据库异常", e);
			}
	}
	
	/**
	 * 判断该车辆所在企业是否允许进行道路等级相关告警分析
	 * @param commaddr
	 * @return
	 */
	private boolean isAllowAnaly(String commaddr){
		boolean flag = false;
		AlarmBaseBean bean = TempMemory.getAlarmVehicleMap(commaddr);
		if (bean!=null){
			OrgAlarmConfBean oacBean = TempMemory.getOrgAlarmConfMap(bean.getTeamId());
			if (oacBean!=null){
				String alarmCode = oacBean.getAlarmCode();
				if (alarmCode.startsWith(Constant.ALARMCODE_OVERSPEED+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OVERSPEED)||alarmCode.indexOf(","+Constant.ALARMCODE_OVERSPEED+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}
	
	public static void main(String[] args) {
		String s = "20121024/163655";
		System.out.print(s.substring(9, 13));
		System.out.println(RoadAlarmThread.class.getMethods());
		RoadAlarmThread r = new RoadAlarmThread();
		String l = r.convertRoadLevel(null);
		RoadAlarmThread a = new RoadAlarmThread();
		System.out.println(""+a.check("1","10")+"-----"+""+a.check("1","00"));
	}
}
