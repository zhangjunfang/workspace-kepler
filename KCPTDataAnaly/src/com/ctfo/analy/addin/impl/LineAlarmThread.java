package com.ctfo.analy.addin.impl;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.LineAlarmBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.MysqlDBAdapter;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.protocal.CommonAnalyseService;
import com.ctfo.analy.util.Base64_URl;
import com.ctfo.analy.util.CDate;
import com.ctfo.statement.AlarmMark;
import com.lingtu.xmlconf.XmlConf;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;

 /**
  * 线路报警处理
  * @author yangyi
  *
  */
public class LineAlarmThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger.getLogger(LineAlarmThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_pId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();
 
	// 是否运行标志
	public boolean isRunning = true;
	
	private boolean isoverspeedalarm = false;// 高速限制
	private boolean isoutareaalarm = true;// 出报警判断
	private boolean issendmessage=false;//是否发送给终端
	private boolean isalarmtoplat=false;//是否发送给平台
	private String alarmadd;//报警附加信息
	private String sendcontent;//线路报警发送内容

	public LineAlarmThread() {
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
		mysqlDBAdapter = new MysqlDBAdapter();
		mysqlDBAdapter.initDBAdapter(config, nodeName);
		
		redisDBAdapter = new RedisDBAdapter();
	}

	@Override
	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void endAnalyser() {
		// TODO Auto-generated method stub

	}

	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	public void run() {
		logger.debug("【线路报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
				int isPValid = CommonAnalyseService.isPValid(vehicleMessage.getLon(), vehicleMessage.getLat(), vehicleMessage.getUtc(), vehicleMessage.getSpeed(), vehicleMessage.getDir());
				boolean isAllowAnaly = isAllowAnaly(vehicleMessage.getCommanddr());
				if(isPValid==0&&isAllowAnaly){
					
						logger.debug("线程[" + nId + "]【线路】【收到数据】>线路报警数据["+ vehicleMessage.getCommanddr() + "]nodeName:"+nodeName+";当前车速："+vehicleMessage.getSpeed());
						// 判断并记录报警信息
						checkAlarm(vehicleMessage);
					
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
				logger.error(e);
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
	
	/**
	 * 设置用户业务类型
	 * @param usetypes
	 */
	private void setUsetype(String [] usetypes){
	// 业务类型,1-限时,2-超速限速,3-进报警判断,4-进报警给终端,5-出报警判断,6-出报警给终端,7进报警给平台,8出报警给平台,【9低速限速】
		//AREA_USETYPE VARCHAR2(50) FALSE FALSE 业务类型,1-限时,2-限速,3-进报警给平台,4-进报警给驾驶员,5-出报警给平台,6-出报警给驾驶员（多个以逗号分隔）
		
		  isoverspeedalarm = false;// 高限速
		  isoutareaalarm = true;// 出报警判断
		  issendmessage=false;//是否发送给终端
		  isalarmtoplat=false;//是否发送给平台
		  alarmadd="";//报警附加信息
		  sendcontent="";//线路报警发送内容
		  
		for(String usetype:usetypes){
			if(usetype.equals("2")){
				isoverspeedalarm=true;
			}else if(usetype.equals("3")){
				alarmadd="4||0";
				sendcontent="进线路报警";
				isoutareaalarm=false;
			}else if(usetype.equals("5")){
				alarmadd="4||1";
				sendcontent="出线路报警";
				isoutareaalarm=true;
			}  else if(usetype.equals("4")){
				issendmessage=true;
			} else if(usetype.equals("6")){
				issendmessage=true;
			} else if(usetype.equals("7")){
				isalarmtoplat=true;
			} else if(usetype.equals("8")){
				isalarmtoplat=true;
			} 
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
	 * 存储进出线路报警
	 * @param vehicleMessage
	 * @
	 */
	private void saveLineAlarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean) {
		if(isalarmtoplat){//进出线路报警给平台
			vehicleMessage.setAlarmid(AlarmMark.XLJC+vehicleMessage.getVid()+lineAlarmBean.getPid()+ "21"+ vehicleMessage.getUtc());
			vehicleMessage.setAlarmcode("21");
			vehicleMessage.setBglevel("A005");
			vehicleMessage.setAlarmSrc(2);
			vehicleMessage.setAlarmadd(alarmadd);
			vehicleMessage.setAlarmAddInfo("线路:"+lineAlarmBean.getLineName()+"-线段："+lineAlarmBean.getPid()+"-"+sendcontent);//“线路名称-线段ID”-进出线段报警
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				logger.debug("线程:["+nId+"]【线路进出】报警【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid()+",AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-【线路进出】报警存储---【数据库异常】",e);
			}
		}
		
		if(issendmessage){//进出线路报警给终端
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_INTOLINE);
//			logger.debug("线程:["+nId+"]线路进出报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
		}

	}
 
	/**
	 * 更新进出线路报警
	 * @param vehicleMessage
	 * @param lineAlarmCache
	 */
	private void updateLineAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean){
		if(isalarmtoplat){//进出线路报警给平台
			String alarmId = AlarmMark.XLJC+vehicleMessage.getVid()+lineAlarmBean.getPid()+ "21"+ lineAlarmCache.getAlarmbegintime();
			vehicleMessage.setAlarmid(alarmId);
			try{
				
				vehicleMessage.setAlarmcode("21");
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);

				//存储报警事件数据
				lineAlarmCache.setAlarmId(alarmId);
				lineAlarmCache.setAlarmcode("21");
				lineAlarmCache.setAlarmlevel("A005");
				lineAlarmCache.setAlarmadd(alarmadd);
				lineAlarmCache.setAreaId(lineAlarmBean.getLineid());
				lineAlarmCache.setEndTime(vehicleMessage.getUtc());
				//lineAlarmCache.setBegintime(areaAlarmCache.getOverspeedbegintime());
				//areaAlarmCache.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
				saveAlarmEvent(vehicleMessage,lineAlarmCache);
				
				logger.debug("线程:["+nId+"]【线路进出】报警【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid());
			}catch(Exception e){
				logger.error("Alarmid:"+vehicleMessage.getAlarmid()+"---更新进出线路报警-【数据库异常】",e);
			}
		}
	}
	
	/**
	 * 存储超速报警
	 * @param vehicleMessage
	 */
	private void saveOverspeed(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean){
		if(isalarmtoplat){//进出线路报警给平台
			vehicleMessage.setAlarmid(AlarmMark.XLCS+vehicleMessage.getVid()+lineAlarmBean.getPid()+ "1"+lineAlarmCache.getOverspeedbegintime());
			vehicleMessage.setUtc(vehicleMessage.getUtc());
			vehicleMessage.setAlarmcode("1");
			vehicleMessage.setAlarmSrc(5);
			vehicleMessage.setAlarmadd("4|"+lineAlarmBean.getLineid());
			vehicleMessage.setBglevel("A001");
			vehicleMessage.setAlarmAddInfo("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-线段超速超速报警");
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				logger.debug("线程:["+nId+"]【线路超速】【存储】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+vehicleMessage.getAlarmid()+";AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.debug("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-线段超速超速报警-存储---【数据库异常】",e);
			}
		}
		if(issendmessage){
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_OVERSPEED);
//			logger.debug("线程:["+nId+"]线路超速报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
			}
	}
	
	/**
	 * 更新超速报警
	 * @param vehicleMessage
	 * @
	 */
	private void updateOverspeed(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean) {
		String alarmId = AlarmMark.XLCS+vehicleMessage.getVid()+lineAlarmBean.getPid()+ "1"+ lineAlarmCache.getOverspeedbegintime();
		vehicleMessage.setAlarmid(alarmId);
		try{
			vehicleMessage.setAlarmcode("1");
			oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
			mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
			redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);

			//存储报警事件数据
			lineAlarmCache.setAlarmId(alarmId);
			lineAlarmCache.setAlarmcode("1");
			lineAlarmCache.setAlarmlevel("A001");
			lineAlarmCache.setAlarmadd("4|"+lineAlarmBean.getLineid());
			lineAlarmCache.setAreaId(lineAlarmBean.getLineid());
			lineAlarmCache.setBegintime(lineAlarmCache.getOverspeedbegintime());
			lineAlarmCache.setEndTime(vehicleMessage.getUtc());
			//areaAlarmCache.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
			saveAlarmEvent(vehicleMessage,lineAlarmCache);
			
			logger.debug("线程:["+nId+"]【线路超速】报警【更新】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+vehicleMessage.getAlarmid());
		}catch(Exception e){
			logger.error("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-【线路超速】报警更新---【数据库异常】",e);
		}
	}

	/**
	 * 存在缓存报警判断
	 * @param vehicleMessage
	 * @param lineAlarmBean
	 * @param lineAlarmCacheBean
	 * @
	 */
	private void dealCacheAlarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean,AlarmCacheBean lineAlarmCacheBean) {

		//设置线路超速阀值
		OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
		double speedScale = 1.0;
		String currTime = CDate.getTimeShort();
		String vd = vehicleMessage.getVid();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			speedScale = tmpBean.getSpeedScale()/100.0;
		}
		double speedThreshold = lineAlarmBean.getSpeedthreshold()*speedScale;
		
		if(isoverspeedalarm){//是否判断超速
			if(vehicleMessage.getSpeed() >speedThreshold){//是否超速
				vehicleMessage.setSpeedThreshold(speedThreshold);
				if(lineAlarmCacheBean.getOverspeedbegintime()==0){//首次超速
					lineAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
					lineAlarmCacheBean.setBeginVmb(vehicleMessage);
					lineAlarmCacheBean.setSpeedThreshold(speedThreshold);
				}else{//持续超速
					 if(!lineAlarmCacheBean.getIssaveorverspeed()&&vehicleMessage.getUtc()-lineAlarmCacheBean.getOverspeedbegintime()>lineAlarmBean.getSpeedtimethreshold()*1000){//大于超速持续时间
						 saveOverspeed(vehicleMessage,lineAlarmCacheBean,lineAlarmBean) ;
						 lineAlarmCacheBean.setIssaveorverspeed(true);
					 }
				} 
			}else{//不超速
				if(lineAlarmCacheBean.getOverspeedbegintime()!=0){//有超速
					dealUpdateOverspeedalarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有超速报警，更新结束
				}
			}
		}//是否判断超速结束
		
	}
	
	/** 
	 * 不存在缓存报警判断
	 * @param vehicleMessage
	 * @param lineAlarmBean
	 * @param lineAlarmCacheBean
	 * @param vid
	 * @param type
	 * @
	 */
	private void dealNoCacheAlarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean,AlarmCacheBean lineAlarmCacheBean,String vid,Integer type) {
		//设置围栏超速阀值
		OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
		double speedScale = 1.0;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			speedScale = tmpBean.getSpeedScale()/100.0;
		}
		double speedThreshold = lineAlarmBean.getSpeedthreshold()*speedScale;
		
		//1判断超速，2判断线路报警，3判断超速【低速】、线路报警
		if(type==1){
			if(isoverspeedalarm&&vehicleMessage.getSpeed() >speedThreshold){//判断超速								 
				AlarmCacheBean tempLineAlarmCacheBean=new AlarmCacheBean();
				tempLineAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
				tempLineAlarmCacheBean.setSpeedThreshold(speedThreshold);
				tempLineAlarmCacheBean.setUtc(vehicleMessage.getUtc());
				tempLineAlarmCacheBean.setBegintime(vehicleMessage.getUtc());
				vehicleMessage.setSpeedThreshold(speedThreshold);
				tempLineAlarmCacheBean.setBeginVmb(vehicleMessage);
				alarmMap.put(String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid()), tempLineAlarmCacheBean);										 
			}
		}else{
			
			AlarmCacheBean tempLineAlarmCacheBean=new AlarmCacheBean();
			tempLineAlarmCacheBean.setAlarmbegintime(vehicleMessage.getUtc());
			tempLineAlarmCacheBean.setUtc(vehicleMessage.getUtc());
			tempLineAlarmCacheBean.setBegintime(vehicleMessage.getUtc());
			tempLineAlarmCacheBean.setBeginVmb(vehicleMessage);
			if(type==3&&isoverspeedalarm&&vehicleMessage.getSpeed() >speedThreshold){//判断超速
				tempLineAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
				tempLineAlarmCacheBean.setSpeedThreshold(speedThreshold);
				tempLineAlarmCacheBean.getBeginVmb().setSpeedThreshold(speedThreshold);
			}
			alarmMap.put(String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid()), tempLineAlarmCacheBean);
			
			dealSaveLinealarm(vehicleMessage,lineAlarmBean,tempLineAlarmCacheBean);
		}
	}
	
	/**
	 * 判断存储线段报警
	 * @param vehicleMessage
	 * @param lineAlarmBean
	 * @param lineAlarmCacheBean
	 * @
	 */
	public void dealSaveLinealarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean,AlarmCacheBean lineAlarmCacheBean) {
		if(lineAlarmCacheBean.getAlarmbegintime()==0){
			saveLineAlarm(vehicleMessage,lineAlarmBean);
			lineAlarmCacheBean.setAlarmbegintime(vehicleMessage.getUtc());
		}
	}
	/**
	 * 判断更新线段报警
	 * @param vehicleMessage
	 * @param lineAlarmBean
	 * @param lineAlarmCacheBean
	 * @
	 */
	public void dealUpdateLinealarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean,AlarmCacheBean lineAlarmCacheBean) {
		if(lineAlarmCacheBean.getAlarmbegintime()!=0){
			updateLineAlarm(vehicleMessage,lineAlarmCacheBean,lineAlarmBean);
			lineAlarmCacheBean.setAlarmbegintime(0l);
		}
	}
	/**
	 * 判断更新超速报警
	 * @param vehicleMessage
	 * @param lineAlarmBean
	 * @param lineAlarmCacheBean
	 * @
	 */
	public void dealUpdateOverspeedalarm(VehicleMessageBean vehicleMessage,LineAlarmBean lineAlarmBean,AlarmCacheBean lineAlarmCacheBean) {
		if(isoverspeedalarm&&lineAlarmCacheBean.getIssaveorverspeed()){
			updateOverspeed(vehicleMessage,  lineAlarmCacheBean,lineAlarmBean);
			lineAlarmCacheBean.setIssaveorverspeed(false);
			lineAlarmCacheBean.setOverspeedbegintime(0l);
		}
	}
	
	/** 
	 * 报警处理
	 * @param vehicleMessage
	 * @
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage){

		AlarmCacheBean lineAlarmCacheBean = null;// 车线路缓存
		String vid = vehicleMessage.getVid();
		List<LineAlarmBean> areaList = TempMemory.getLineAlarmMap(vid);// 根据vid查询车辆的线路信息集合
		if (areaList != null) {
			logger.debug("[" + vehicleMessage.getCommanddr() + "] vid:"+vid+"所绑定的线段有："+areaList.size()+"个;");
			areaList = new ArrayList<LineAlarmBean>(areaList);
			Long mapLon = vehicleMessage.getMaplon();//偏移后经度
			Long mapLat = vehicleMessage.getMaplat();//偏移后纬度
			List<String> inarealist =queryLineList(mapLon,mapLat);//查询点所在线路
			for (LineAlarmBean lineAlarmBean : areaList) {//判断用户设置
				String mapkey=String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid());
				lineAlarmCacheBean = alarmMap.get(mapkey);// 取得本车本线路缓存数据 vid+pid
				logger.debug("[" + vehicleMessage.getCommanddr() + "] 开始处理线段："+lineAlarmBean.getPid()+";mapkey:"+mapkey+";inarealist:+inarealist+;lineAlarmCacheBean:"+lineAlarmCacheBean+";"+(inarealist.contains(mapkey)?"当前坐标【在】线路内":"当前坐标【不在】线路内"));
				//顺序点判断
				if(lineAlarmCacheBean!=null){
					if(lineAlarmCacheBean.getUtc()>=vehicleMessage.getUtc()){
						break;
					}else{
						lineAlarmCacheBean.setUtc(vehicleMessage.getUtc());
					}
				}
				setUsetype(lineAlarmBean.getUsetype());//设置用户业务类型
				//车辆判断超速时获取最大车速
				if (isoverspeedalarm&&lineAlarmCacheBean!=null){
					if (lineAlarmCacheBean.getMaxSpeed()>0){
						if (vehicleMessage.getSpeed()!=null&&vehicleMessage.getSpeed()>lineAlarmCacheBean.getMaxSpeed()){
							lineAlarmCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
						}
					}else{
						lineAlarmCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
					}
				}
				
                if (vehicleMessage.getUtc() >= lineAlarmBean.getBeginTime()&& vehicleMessage.getUtc() <= lineAlarmBean.getEndTime()) {// 在线路判定周期内
						//判断当前是否在线路内
						boolean iscurrentinarea=false;
						for(String inarea:inarealist){
							if(inarea.equals(mapkey)){
								iscurrentinarea=true;
								break;
							}
						}
						if(isoutareaalarm){//出线路报警判断
							if(iscurrentinarea){//当前在线路内，不产生线路报警，判断超速
								if(lineAlarmCacheBean!=null){//存在缓存
									dealUpdateLinealarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有线路报警，更新结束
									dealCacheAlarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//超速判断【低速判断】
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean,vid,1);//判断超速【低速】，增加缓存 
								}
							}else{//当前在线路外，产生线路报警
								if(lineAlarmCacheBean!=null){//存在缓存
									dealSaveLinealarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有线路报警， 存储
									dealUpdateOverspeedalarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有超速报警，更新结束
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean,vid,2);//存储线路报警，增加缓存 
								}
								
							}
						}else{//进线路报警判断
							if(iscurrentinarea){
								if(lineAlarmCacheBean!=null){//存在缓存，产生线路报警
									dealSaveLinealarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有线路报警，存储
									dealCacheAlarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//超速判断【低速判断】
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean,vid,3);//存储线路报警，判断超速【低速】，增加缓存 
								}
							}else{//当前在线路外
								if(lineAlarmCacheBean!=null){//存在缓存
									dealUpdateLinealarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有线路报警，更新结束
									dealUpdateOverspeedalarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有超速报警，更新结束
								}
							}
						}
					}else{//判断周期外
						if(lineAlarmCacheBean!=null){//存在缓存
							dealUpdateLinealarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有线路报警，更新结束
							dealUpdateOverspeedalarm(vehicleMessage,  lineAlarmBean,  lineAlarmCacheBean);//有超速报警，更新结束
							alarmMap.remove(mapkey);//删除缓存
						}
					}
				}
		}else{
			logger.debug("[" + vehicleMessage.getCommanddr() + "] vid:"+vid+"所绑定的线段有：0个");
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
				vehicleMessage.setAlarmid(alarmCacheBean.getAlarmId());
				vehicleMessage.setUtc(vehicleMessage.getUtc());
				alarmCacheBean.setEndTime(vehicleMessage.getUtc());
				alarmCacheBean.setEndVmb(vehicleMessage);
				oracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,alarmCacheBean);
				logger.debug("线程:[" + nId + "]【围栏软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid());
			} catch (Exception e) {
				logger.error("Alarmid:" + alarmCacheBean.getAlarmId()
						+ "---添加围栏软报警驾驶行为事件-数据库异常", e);
			}
	}
	
	/**
	 * 判断该车辆所在企业是否允许进行线路相关告警分析
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
				if (alarmCode.startsWith(Constant.ALARMCODE_OVERSPEED+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OVERSPEED)||alarmCode.indexOf(","+Constant.ALARMCODE_OVERSPEED+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_INTOLINE+",")||alarmCode.endsWith(","+Constant.ALARMCODE_INTOLINE)||alarmCode.indexOf(","+Constant.ALARMCODE_INTOLINE+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}
}
