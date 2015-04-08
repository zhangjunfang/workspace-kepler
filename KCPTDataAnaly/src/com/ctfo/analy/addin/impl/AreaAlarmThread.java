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
import com.ctfo.analy.beans.AreaAlarmBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.MysqlDBAdapter;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.protocal.CommonAnalyseService;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.MathUtils;
import com.ctfo.statement.AlarmMark;
import com.lingtu.xmlconf.XmlConf;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;

 /**
  * 围栏区域报警处理
  * @author LiangJian
  */
public class AreaAlarmThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger.getLogger(AreaAlarmThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();
	
	// 围栏内外开门或停车报警map 缓存 key=vId_AlarmMark.WLKM  || vId_AlarmMark.WLTC
	private Map<String, AlarmCacheBean> openAndStopedMap = new ConcurrentHashMap<String, AlarmCacheBean>();
 
 
	// 是否运行标志
	public boolean isRunning = true;
	
	private boolean isoverspeedalarm = false;// 高速限制
	private boolean isoutareaalarm = true;// 出报警判断
	private boolean issendmessage=false;//是否发送给终端
	private boolean isalarmtoplat=false;//是否发送给平台
	private String alarmadd;//报警附加信息
	private String sendcontent;//围栏报警发送内容

	public AreaAlarmThread() {
	}

 
	/**
	 * 初始化方法
	 */
	public void initAnalyser(int nId, XmlConf config, String nodeName)
			throws Exception {
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
		logger.debug("【围栏区域报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
				int isPValid = CommonAnalyseService.isPValid(vehicleMessage.getLon(), vehicleMessage.getLat(), vehicleMessage.getUtc(), vehicleMessage.getSpeed(), vehicleMessage.getDir());
				boolean isAllowAnaly = isAllowAnaly(vehicleMessage.getCommanddr());
				if(isPValid==0&&isAllowAnaly){
					logger.debug("线程[" + nId + "]【围栏】【收到数据】>围栏报警数据["+ vehicleMessage.getCommanddr() + "]nodeName:"+nodeName+";当前车速："+vehicleMessage.getSpeed());
					// 判断并记录报警信息
					checkAlarm(vehicleMessage);
				}else{
					String msg = "";
					if (isPValid!=0){
						msg += "不合法车辆轨迹;";
					}
					if (!isAllowAnaly){
						msg += "企业不进行围栏告警分析;";
					}
					logger.debug(msg+"[" + vehicleMessage.getCommanddr()+"]");
				}
				}
			} catch (Exception e) {
				e.printStackTrace();
				logger.error(e);
			}
		}
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
		  sendcontent="";//围栏报警发送内容
		for(String usetype:usetypes){
			if(usetype.equals("2")){
				isoverspeedalarm=true;
			}else if(usetype.equals("3")){
				alarmadd="3||0";
				sendcontent="进围栏报警";
				isoutareaalarm=false;
			}else if(usetype.equals("5")){
				alarmadd="3||1";
				sendcontent="出围栏报警";
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
	 * 设置用户业务类型
	 * @param usetypes
	 */
	private void setUsetype(String [] usetypes,String[] msg){
		// 业务类型,1-限时,2-超速限速,3-进报警判断,4-进报警给终端,5-出报警判断,6-出报警给终端,7进报警给平台,8出报警给平台,【9低速限速】
		//AREA_USETYPE VARCHAR2(50) FALSE FALSE 业务类型,1-限时,2-限速,3-进报警给平台,4-进报警给驾驶员,5-出报警给平台,6-出报警给驾驶员（多个以逗号分隔）
		
		  isoverspeedalarm = false;// 高限速
		  isoutareaalarm = true;// 出报警判断
		  issendmessage=false;//是否发送给终端
		  isalarmtoplat=false;//是否发送给平台
		  alarmadd="";//报警附加信息
		  sendcontent="";//围栏报警发送内容
		for(String usetype:usetypes){
			if(usetype.equals("2")){
				isoverspeedalarm=true;
			}else if(usetype.equals("3")){
				alarmadd="3||0";
				sendcontent="进围栏报警";
				/*if (msg!=null&&msg.length>=1&&msg[0]!=null&&!"%u6D88%u606F".equals(msg[0])){
					sendcontent=msg[0];
				}*/
				isoutareaalarm=false;
			}else if(usetype.equals("5")){
				alarmadd="3||1";
				sendcontent="出围栏报警";
				/*if (msg!=null&&msg.length>=2&&msg[1]!=null&&!"%u6D88%u606F".equals(msg[1])){
					sendcontent=msg[1];
				}*/
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
	 * 存储进出围栏报警
	 * @param vehicleMessage
	 * 
	 */
	private void saveAreaAlarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean){
		if(isalarmtoplat){//进出围栏报警给平台
			vehicleMessage.setAlarmid(AlarmMark.WLJC+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+ "20"+ vehicleMessage.getUtc());
			vehicleMessage.setAlarmcode("20");
			vehicleMessage.setBglevel("A002");
			vehicleMessage.setAlarmSrc(2);
			vehicleMessage.setAlarmadd(alarmadd);
			vehicleMessage.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				logger.debug("线程:["+nId+"]【围栏进出报警】【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid()+";AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent+"---数据库异常",e);
			}
		}
		
		if(issendmessage){//进出围栏报警给终端
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_INTOAREA);
//			logger.debug("线程:["+nId+"]围栏进出报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
		}

	}
 
	/**
	 * 更新进出围栏报警
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateAreaAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean areaAlarmCache,AreaAlarmBean areaAlarmBean){
		if(isalarmtoplat){//进出围栏报警给平台
			String alarmId = AlarmMark.WLJC+vehicleMessage.getVid() +areaAlarmBean.getAreaid()+ "20"+ areaAlarmCache.getAlarmbegintime();
			vehicleMessage.setAlarmid(alarmId);
			try{
				vehicleMessage.setAlarmcode("20");
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);
				
				//存储报警事件数据
				areaAlarmCache.setAlarmId(alarmId);
				areaAlarmCache.setAlarmcode("20");
				vehicleMessage.setAlarmSrc(2);
				areaAlarmCache.setAlarmlevel("A002");
				areaAlarmCache.setAlarmadd(alarmadd);
				areaAlarmCache.setAreaId(areaAlarmBean.getAreaid());
				areaAlarmCache.setEndTime(vehicleMessage.getUtc());
				areaAlarmCache.setEndVmb(vehicleMessage);
				//areaAlarmCache.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
				saveAlarmEvent(vehicleMessage,areaAlarmCache);
				
				logger.debug("线程:["+nId+"]【围栏进出报警】【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid());
			}catch(Exception e){
				logger.error("Alarmid:"+vehicleMessage.getAlarmid()+"---更新进出围栏报警-数据库异常",e);
			}
		}
	}
	
	/**
	 * 存储超速报警
	 * @param vehicleMessage
	 * 
	 */
	private void saveOverspeed(VehicleMessageBean vehicleMessage,AlarmCacheBean areaAlarmCache,AreaAlarmBean areaAlarmBean){
		if(isalarmtoplat){//进出围栏报警给平台
			vehicleMessage.setAlarmid(AlarmMark.WLCS+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+  "1"+ areaAlarmCache.getOverspeedbegintime());
			//vehicleMessage.setUtc(vehicleMessage.getUtc());
			vehicleMessage.setAlarmcode("1");
			vehicleMessage.setAlarmSrc(3);
			vehicleMessage.setAlarmadd("3|"+areaAlarmBean.getAreaid());
			vehicleMessage.setBglevel("A001");
			vehicleMessage.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型：围栏超速告警");//“围栏名称”超速告警
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				logger.debug("线程:["+nId+"]【围栏超速】【存储】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+vehicleMessage.getAlarmid()+";AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("名称："+areaAlarmBean.getAreaName()+"-类型：围栏超速告警"+"---存储超速报警-数据库异常",e);
			}
		}
		if(issendmessage){
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_OVERSPEED);
			logger.debug("线程:["+nId+"]围栏超速报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
		}
	}
	
	/**
	 * 更新超速报警
	 * @param vehicleMessage
	 * 
	 */
	private void updateOverspeed(VehicleMessageBean vehicleMessage,AlarmCacheBean areaAlarmCache,AreaAlarmBean areaAlarmBean){
		String alarmId = AlarmMark.WLCS+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+ "1"+ areaAlarmCache.getOverspeedbegintime();
		vehicleMessage.setAlarmid(alarmId);
		try{
			vehicleMessage.setAlarmcode("1");
			oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
			mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
			redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);
			
			//存储报警事件数据
			areaAlarmCache.setAlarmId(alarmId);
			areaAlarmCache.setAlarmcode("1");
			vehicleMessage.setAlarmSrc(3);
			areaAlarmCache.getBeginVmb().setAlarmSrc(3);
			areaAlarmCache.setAlarmlevel("A001");
			areaAlarmCache.setAlarmadd("3|"+areaAlarmBean.getAreaid());
			areaAlarmCache.setAreaId(areaAlarmBean.getAreaid());
			areaAlarmCache.setBegintime(areaAlarmCache.getOverspeedbegintime());
			areaAlarmCache.setEndTime(vehicleMessage.getUtc());
			areaAlarmCache.setEndVmb(vehicleMessage);
			//areaAlarmCache.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
			saveAlarmEvent(vehicleMessage,areaAlarmCache);
			
			logger.debug("线程:["+nId+"]【围栏超速】报警【更新】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+vehicleMessage.getAlarmid());
		}catch(Exception e){
			logger.error("车速："+vehicleMessage.getSpeed()+";Alarmid:"+vehicleMessage.getAlarmid()+"名称："+areaAlarmBean.getAreaName()+"-类型：【围栏超速】报警【更新】"+"---数据库异常",e);
		}
	}
	
	/**
	 * 存在缓存报警判断
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	private void dealCacheAlarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,AlarmCacheBean areaAlarmCacheBean){
		//设置围栏超速阀值
		OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
		double speedScale = 1.0;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			speedScale = tmpBean.getSpeedScale()/100.0;
		}
		double speedThreshold = areaAlarmBean.getAreamaxspeed()*speedScale;
		
		
		if(isoverspeedalarm){//是否判断超速
			if(vehicleMessage.getSpeed() >speedThreshold){//是否超速
				vehicleMessage.setSpeedThreshold(speedThreshold);
				if(areaAlarmCacheBean.getOverspeedbegintime()==0){//首次超速
					areaAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
					areaAlarmCacheBean.setSpeedThreshold(speedThreshold);
					areaAlarmCacheBean.setBeginVmb(vehicleMessage);
				}else{//持续超速
					 if(!areaAlarmCacheBean.getIssaveorverspeed()&&vehicleMessage.getUtc()-areaAlarmCacheBean.getOverspeedbegintime()>areaAlarmBean.getSuperspeedtimes()*1000){//大于超速持续时间
						 saveOverspeed(vehicleMessage,areaAlarmCacheBean,areaAlarmBean) ;
						 areaAlarmCacheBean.setIssaveorverspeed(true);
					 }
				} 
			}else{//不超速
				if(areaAlarmCacheBean.getOverspeedbegintime()!=0){//有超速
					dealUpdateOverspeedalarm(vehicleMessage,areaAlarmBean,areaAlarmCacheBean);//有超速报警，更新结束
					areaAlarmCacheBean.setOverspeedbegintime(0l);
				}
			}
		}//是否判断超速结束
	}
	
	/** 
	 * 不存在缓存报警判断
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * @param vid
	 * @param type
	 * 
	 */
	private void dealNoCacheAlarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,AlarmCacheBean areaAlarmCacheBean,String vid,Integer type) {
		//设置围栏超速阀值
		OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
		double speedScale = 1.0;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			speedScale = tmpBean.getSpeedScale()/100.0;
		}
		double speedThreshold = areaAlarmBean.getAreamaxspeed()*speedScale;
		
		//1判断超速和低速，2判断区域报警，3判断超速低速、区域报警
		if(type==1){
			AlarmCacheBean tempAreaAlarmCacheBean = new AlarmCacheBean();
			if(isoverspeedalarm&&vehicleMessage.getSpeed() >speedThreshold){//判断超速								 
				tempAreaAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
				tempAreaAlarmCacheBean.setSpeedThreshold(speedThreshold);
				tempAreaAlarmCacheBean.setUtc(vehicleMessage.getUtc());

				tempAreaAlarmCacheBean.setBegintime(vehicleMessage.getUtc());
				
				vehicleMessage.setSpeedThreshold(areaAlarmBean.getAreamaxspeed());
				tempAreaAlarmCacheBean.setBeginVmb(vehicleMessage);
				alarmMap.put(String.valueOf(vid) + "_"	+ String.valueOf(areaAlarmBean.getAreaid()), tempAreaAlarmCacheBean);										 
			}
		}else{
			saveAreaAlarm(vehicleMessage,areaAlarmBean);
			AlarmCacheBean tempAreaAlarmCacheBean=new AlarmCacheBean();
			tempAreaAlarmCacheBean.setAlarmbegintime(vehicleMessage.getUtc());
			tempAreaAlarmCacheBean.setUtc(vehicleMessage.getUtc());
			tempAreaAlarmCacheBean.setBegintime(vehicleMessage.getUtc());
			tempAreaAlarmCacheBean.setBeginVmb(vehicleMessage);
			if(type==3&&isoverspeedalarm&&vehicleMessage.getSpeed() > speedThreshold){//判断超速
					tempAreaAlarmCacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
					tempAreaAlarmCacheBean.setSpeedThreshold(speedThreshold);
			}
			alarmMap.put(String.valueOf(vid) + "_"	+ String.valueOf(areaAlarmBean.getAreaid()), tempAreaAlarmCacheBean);
		}
	}
	
	/**
	 * 判断存储区域报警
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealSaveAreaalarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,AlarmCacheBean areaAlarmCacheBean) {
		if(areaAlarmCacheBean.getAlarmbegintime()==0){
			saveAreaAlarm(vehicleMessage,areaAlarmBean);
			areaAlarmCacheBean.setAlarmbegintime(vehicleMessage.getUtc());
			areaAlarmCacheBean.setBeginVmb(vehicleMessage);
		}
	}
	/**
	 * 判断更新区域报警
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateAreaalarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,AlarmCacheBean areaAlarmCacheBean){
		if(areaAlarmCacheBean.getAlarmbegintime()!=0){
			updateAreaAlarm(vehicleMessage,areaAlarmCacheBean,areaAlarmBean);
			areaAlarmCacheBean.setAlarmbegintime(0l);
		}
	}
	/**
	 * 判断更新超速报警
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateOverspeedalarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,AlarmCacheBean areaAlarmCacheBean) {
		if(isoverspeedalarm&&areaAlarmCacheBean.getIssaveorverspeed()){
			updateOverspeed(vehicleMessage,  areaAlarmCacheBean,areaAlarmBean);
			areaAlarmCacheBean.setIssaveorverspeed(false);
			areaAlarmCacheBean.setOverspeedbegintime(0l);
		}
	}
	
	/** 
	 * 报警处理
	 * @param vehicleMessage
	 * 
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage){

		AlarmCacheBean areaAlarmCacheBean = null;// 车围栏缓存
		String vid = vehicleMessage.getVid();
		List<AreaAlarmBean> areaList = TempMemory.getAreaAlarmMap(vid);// 根据vid查询车辆的围栏信息集合
		if (areaList != null) {
			logger.debug("vid:"+vid+"所绑定的围栏有："+areaList.size()+"个");
			areaList = new ArrayList<AreaAlarmBean>(areaList);
			List<String> inarealist =queryAreaList(vehicleMessage.getLon(),vehicleMessage.getLat());//查询点所在围栏
			for (AreaAlarmBean areaAlarmBean : areaList) {//判断用户设置
				String mapkey=String.valueOf(vid) + "_"	+ String.valueOf(areaAlarmBean.getAreaid());
				areaAlarmCacheBean = alarmMap.get(mapkey);// 取得本车本围栏缓存数据 vid+areaid
				logger.debug("开始处理围栏："+areaAlarmBean.getAreaName()+";mapkey:"+mapkey+";inarealist:"+inarealist+";areaAlarmCacheBean:"+areaAlarmCacheBean+";"+(inarealist.contains(mapkey)?"当前坐标【在】围栏内":"当前坐标【不在】围栏内"));
				//顺序点判断
				if(areaAlarmCacheBean!=null){
					if(areaAlarmCacheBean.getUtc()>=vehicleMessage.getUtc()){
						break;
					}else{
						areaAlarmCacheBean.setUtc(vehicleMessage.getUtc());
					}
				}
				setUsetype(areaAlarmBean.getUsetype(),areaAlarmBean.getMessageValue());//设置用户业务类型
				//车辆判断超速时获取最大车速
				if (isoverspeedalarm&&areaAlarmCacheBean!=null){
					if (areaAlarmCacheBean.getMaxSpeed()>0){
						if (vehicleMessage.getSpeed()!=null&&vehicleMessage.getSpeed()>areaAlarmCacheBean.getMaxSpeed()){
							areaAlarmCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
						}
					}else{
						areaAlarmCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
					}
				}
				
                if (vehicleMessage.getUtc() >= areaAlarmBean.getBeginTime()&& vehicleMessage.getUtc() <= areaAlarmBean.getEndTime()) {// 在围栏判定周期内
						//判断当前是否在围栏内
						boolean iscurrentinarea=false;
						for(String inarea:inarealist){
							if(inarea.equals(mapkey)){
								iscurrentinarea=true;
								break;
							}
						}
						//判断是否开门或停车
						analyseOpendoorOrStopedAlarm(vehicleMessage,areaAlarmBean,iscurrentinarea,true);
						
						if(isoutareaalarm){//出围栏报警判断
							if(iscurrentinarea){//当前在围栏内，不产生围栏报警，判断超速、低速
								if(areaAlarmCacheBean!=null){//存在缓存
									dealUpdateAreaalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有围栏报警，更新结束
									dealCacheAlarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//超速判断【低速判断】								
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean,vid,1);//判断超速【低速】，增加缓存 
								}
							}else{//当前在围栏外，产生围栏报警							
								if(areaAlarmCacheBean!=null){//存在缓存
									dealSaveAreaalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有围栏报警， 存储
									dealUpdateOverspeedalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有超速报警，更新结束
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean,vid,2);//存储围栏报警，增加缓存 
								}
							}
						}else{//进围栏报警判断
							if(iscurrentinarea){//当前在围栏内
								if(areaAlarmCacheBean!=null){//存在缓存
									dealSaveAreaalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有围栏报警， 存储
									dealCacheAlarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//超速判断【低速判断】								
								}else{//不存在缓存
									dealNoCacheAlarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean,vid,3);//存储围栏报警，判断超速【低速】，增加缓存  
								}
							}else{//当前在围栏外
								if(areaAlarmCacheBean!=null){//存在缓存
									dealUpdateAreaalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有围栏报警，更新结束
									dealUpdateOverspeedalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有超速报警，更新结束
								}
							}
						}
					}else{//判断周期外
						if(areaAlarmCacheBean!=null){//存在缓存
							dealUpdateAreaalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有围栏报警，更新结束
							dealUpdateOverspeedalarm(vehicleMessage,  areaAlarmBean,  areaAlarmCacheBean);//有超速报警，更新结束
							alarmMap.remove(mapkey);//删除缓存
						}
						
						//判断是否开门或停车告警结束
						analyseOpendoorOrStopedAlarm(vehicleMessage,areaAlarmBean,false,false);
					}
				}
		}else{
			logger.debug("vid:"+vid+"所绑定的围栏有：0个");
		}
	}
	
	/**
	 * 区域内外开门或停车告警判断
	 * @param vehicleMessage 接收数据对象
	 * @param vehicleDoorType 停车或开门告警判定类型  1：区域内开门告警，2：区域外开门告警 3：区域内停车告警 4：区域外停车告警
	 * @param inArea 是否在区域内
	 */
	public void analyseOpendoorOrStopedAlarm(VehicleMessageBean vehicleMessage,AreaAlarmBean areaAlarmBean,boolean inArea,boolean inTimeJudgeRegion){
		
		boolean inAreaOpendoorAlarm = false;
		boolean outAreaOpendoorAlarm = false;
		boolean inAreaStopedAlarm = false;
		boolean outAreaStopedAlarm = false;
		
		boolean isOpenDoor = false;
		boolean isStoped = false;
		
		if (areaAlarmBean!=null){
		String[] vehicleDoorType = areaAlarmBean.getVehicleDoorType();
		
			if (vehicleDoorType!=null){
				for (int i = 0;i<vehicleDoorType.length;i++){
					String type = vehicleDoorType[i];
					if ("1".equals(type)){
						inAreaOpendoorAlarm = true;
					}else if ("2".equals(type)){
						outAreaOpendoorAlarm = true;
					}else if ("3".equals(type)){
						inAreaStopedAlarm = true;
					}else if ("4".equals(type)){
						outAreaStopedAlarm = true;
					}
				}
			}
		
		
		//判断车辆当前是否开门
		String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
		if (MathUtils.check("13", binaryStr)||MathUtils.check("14", binaryStr)||MathUtils.check("15", binaryStr)||MathUtils.check("16", binaryStr)){
			isOpenDoor = true;
		}
		
		//判断车辆当前是否停车 停车判断标准：车速小于5Km/h
		if (vehicleMessage.getSpeed()<50){
			isStoped = true;
		}
		
		//从缓存中读取报警信息
		AlarmCacheBean opendoorAlarmCacheBean = openAndStopedMap.get(vehicleMessage.getVid()+"_"+AlarmMark.WLKM+"_"+areaAlarmBean.getAreaid());
		
		//如果缓存报警中的上一点时间和本次时间相差超过10分钟,则移除此对象
		if (opendoorAlarmCacheBean!=null&&opendoorAlarmCacheBean.getUtc()>0l&&(vehicleMessage.getUtc()-opendoorAlarmCacheBean.getUtc())>10*60*1000){
			openAndStopedMap.remove(vehicleMessage.getVid()+"_"+AlarmMark.WLKM+"_"+areaAlarmBean.getAreaid());
			opendoorAlarmCacheBean = null;
		}
		
		if (opendoorAlarmCacheBean!=null){//当存在缓存时
			opendoorAlarmCacheBean.setUtc(vehicleMessage.getUtc());
			if (!isOpenDoor){
				//结束门开告警
				String alarmId = AlarmMark.WLKM+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+opendoorAlarmCacheBean.getAlarmbegintime();
				updateOpendoorAlarm(vehicleMessage,opendoorAlarmCacheBean);
				
				openAndStopedMap.remove(vehicleMessage.getVid()+"_"+AlarmMark.WLKM+"_"+areaAlarmBean.getAreaid());
			}
		}else{
			if (inTimeJudgeRegion&&isOpenDoor&&((inArea&&inAreaOpendoorAlarm)||(!inArea&&outAreaOpendoorAlarm))){
				//开始门开告警
				AlarmCacheBean cacheBean = new AlarmCacheBean();
				cacheBean.setUtc(vehicleMessage.getUtc());
				cacheBean.setAlarmbegintime(vehicleMessage.getUtc());
				cacheBean.setBegintime(vehicleMessage.getUtc());
				cacheBean.setBeginVmb(vehicleMessage);
				
				
				String alarmId = AlarmMark.WLKM+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+vehicleMessage.getUtc();
				vehicleMessage.setAreaId(areaAlarmBean.getAreaid());
				
				cacheBean.setAlarmId(alarmId);
				cacheBean.setAreaId(areaAlarmBean.getAreaid());
				String alarmCode = "";
				if (inArea){
					alarmCode = "60";
					//alarmSrc = "区域内开门";
				}else{
					alarmCode = "61";
					//alarmSrc = "区域外开门";
				}
				cacheBean.setAlarmcode(alarmCode);
				cacheBean.setAlarmlevel("A002");
				
				openAndStopedMap.put(vehicleMessage.getVid()+"_"+AlarmMark.WLKM+"_"+areaAlarmBean.getAreaid(),cacheBean);
				
				saveOpendoorAlarm(vehicleMessage,alarmId,inArea);
			}
		}
		
		
		AlarmCacheBean stopedAlarmCacheBean = openAndStopedMap.get(vehicleMessage.getVid()+"_"+AlarmMark.WLTC+"_"+areaAlarmBean.getAreaid());
		//如果缓存报警中的上一点时间和本次时间相差超过10分钟,则移除此对象
		if (stopedAlarmCacheBean!=null&&stopedAlarmCacheBean.getUtc()>0l&&(vehicleMessage.getUtc()-stopedAlarmCacheBean.getUtc())>10*60*1000){
			openAndStopedMap.remove(vehicleMessage.getVid()+"_"+AlarmMark.WLTC+"_"+areaAlarmBean.getAreaid());
			stopedAlarmCacheBean = null;
		}
		if (stopedAlarmCacheBean!=null){//当存在缓存时，如果出现状态为FALSE，则结束告警；否则继续判定
			stopedAlarmCacheBean.setUtc(vehicleMessage.getUtc());
			if (!isStoped){
				//结束停车告警
				String alarmId = AlarmMark.WLTC+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+stopedAlarmCacheBean.getAlarmbegintime();
				updateStopedAlarm(vehicleMessage,stopedAlarmCacheBean);
				
				openAndStopedMap.remove(vehicleMessage.getVid()+"_"+AlarmMark.WLTC+"_"+areaAlarmBean.getAreaid());
			}
		}else{
			if (inTimeJudgeRegion&&isStoped&&((inArea&&inAreaStopedAlarm)||(!inArea&&outAreaStopedAlarm))){
				//开始停车告警
				AlarmCacheBean cacheBean = new AlarmCacheBean();
				cacheBean.setUtc(vehicleMessage.getUtc());
				cacheBean.setAlarmbegintime(vehicleMessage.getUtc());
				cacheBean.setBegintime(vehicleMessage.getUtc());
				cacheBean.setBeginVmb(vehicleMessage);
				
				String alarmId = AlarmMark.WLTC+vehicleMessage.getVid()+areaAlarmBean.getAreaid()+vehicleMessage.getUtc();
				vehicleMessage.setAreaId(areaAlarmBean.getAreaid());
				cacheBean.setAlarmId(alarmId);
				cacheBean.setAreaId(areaAlarmBean.getAreaid());
				String alarmCode = "";
				if (inArea){
					alarmCode = "62";
					//alarmSrc = "区域内停车";
				}else{
					alarmCode = "63";
					//alarmSrc = "区域外停车";
				}
				cacheBean.setAlarmcode(alarmCode);
				cacheBean.setAlarmlevel("A002");
				openAndStopedMap.put(vehicleMessage.getVid()+"_"+AlarmMark.WLTC+"_"+areaAlarmBean.getAreaid(),cacheBean);
				saveStopedAlarm(vehicleMessage,alarmId,inArea);
			}
		}
		}
	}
	
	/**
	 * 存储区域内外开门报警
	 * @param vehicleMessage
	 * 
	 */
	private void saveOpendoorAlarm(VehicleMessageBean vehicleMessage,String alarmId,boolean inArea){
		String alarmCode = "";
		String alarmSrc = "";
		if (inArea){
			alarmCode = "60";
			alarmSrc = "区域内开门";
		}else{
			alarmCode = "61";
			alarmSrc = "区域外开门";
		}

			vehicleMessage.setAlarmid(alarmId);
			vehicleMessage.setAlarmcode(alarmCode);
			vehicleMessage.setBglevel("A002");
			vehicleMessage.setAlarmSrc(2);
			vehicleMessage.setAlarmadd("");
			vehicleMessage.setAlarmAddInfo("名称：围栏门开报警 -类型："+alarmSrc);//“围栏名称”进围栏报警
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				
				TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,alarmCode);
				
				logger.debug("线程:["+nId+"]【围栏内外开门报警】【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid()+";AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("名称：围栏门开报警 -类型："+alarmSrc+"---数据库异常",e);
			}
			
			

	}
 
	/**
	 * 更新开门报警
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateOpendoorAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean){
			try{
				vehicleMessage.setAlarmid(alarmCacheBean.getAlarmId());
				vehicleMessage.setAlarmcode(alarmCacheBean.getAlarmcode());
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);
				
				alarmCacheBean.setEndTime(vehicleMessage.getUtc());
				alarmCacheBean.setEndVmb(vehicleMessage);
				saveAlarmEvent(vehicleMessage,alarmCacheBean);
				logger.debug("线程:["+nId+"]【围栏内外开门报警】【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid());
			}catch(Exception e){
				logger.error("Alarmid:"+vehicleMessage.getAlarmid()+"---更新围栏内外开门报警-数据库异常",e);
			}
	}
	
	/**
	 * 存储区域内外开门报警
	 * @param vehicleMessage
	 * 
	 */
	private void saveStopedAlarm(VehicleMessageBean vehicleMessage,String alarmId,boolean inArea){
		String alarmCode = "";
		String alarmSrc = "";
		if (inArea){
			alarmCode = "62";
			alarmSrc = "区域内停车";
		}else{
			alarmCode = "63";
			alarmSrc = "区域外停车";
		}

			vehicleMessage.setAlarmid(alarmId);
			vehicleMessage.setAlarmcode(alarmCode);
			vehicleMessage.setBglevel("A002");
			vehicleMessage.setAlarmSrc(2);
			vehicleMessage.setAlarmadd("");
			vehicleMessage.setAlarmAddInfo("名称：围栏停车报警 -类型："+alarmSrc);//“围栏名称”进围栏报警
			try{
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage);
				
				TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,alarmCode);
				
				logger.debug("线程:["+nId+"]【围栏内外停车报警】【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid()+";AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("名称：围栏停车报警 -类型："+alarmSrc+"---数据库异常",e);
			}

	}
 
	/**
	 * 更新停车报警
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateStopedAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean){
			try{
				vehicleMessage.setAlarmid(alarmCacheBean.getAlarmId());
				vehicleMessage.setAlarmcode(alarmCacheBean.getAlarmcode());
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage);
				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage);
				
				alarmCacheBean.setEndTime(vehicleMessage.getUtc());
				alarmCacheBean.setEndVmb(vehicleMessage);
				saveAlarmEvent(vehicleMessage,alarmCacheBean);
			
				logger.debug("线程:["+nId+"]【围栏内外停车报警】【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid());
			}catch(Exception e){
				logger.error("Alarmid:"+vehicleMessage.getAlarmid()+"---更新围栏内外停车报警-数据库异常",e);
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
	 * 判断该车辆所在企业是否允许进行围栏相关告警分析
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
						||alarmCode.startsWith(Constant.ALARMCODE_INTOAREA+",")||alarmCode.endsWith(","+Constant.ALARMCODE_INTOAREA)||alarmCode.indexOf(","+Constant.ALARMCODE_INTOAREA+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_INAREAOPENDOOR+",")||alarmCode.endsWith(","+Constant.ALARMCODE_INAREAOPENDOOR)||alarmCode.indexOf(","+Constant.ALARMCODE_INAREAOPENDOOR+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_OUTAREAOPENDOOR+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OUTAREAOPENDOOR)||alarmCode.indexOf(","+Constant.ALARMCODE_OUTAREAOPENDOOR+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_INAREASTOPED+",")||alarmCode.endsWith(","+Constant.ALARMCODE_INAREASTOPED)||alarmCode.indexOf(","+Constant.ALARMCODE_INAREASTOPED+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_OUTAREASTOPED+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OUTAREASTOPED)||alarmCode.indexOf(","+Constant.ALARMCODE_OUTAREASTOPED+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}
}
