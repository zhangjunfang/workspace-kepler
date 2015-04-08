package com.ctfo.analy.addin.impl;

import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Iterator;
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
import com.ctfo.analy.beans.LineAlarmBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.protocal.CommonAnalyseService;
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
public class LineAlarmThread2 extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger.getLogger(LineAlarmThread2.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
//	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_pId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();
 
	// 是否运行标志
	public boolean isRunning = true;
	
	private boolean isoverspeedalarm = false;// 高速限制
	private boolean isintoareaalarm = true;// 入报警判断
	private boolean isoutareaalarm = true;// 出报警判断
	private boolean issendmessage=false;//是否发送给终端
	private boolean isalarmtoplat=false;//是否发送给平台
	private String alarmadd;//报警附加信息
	private String sendcontent;//线路报警发送内容

	public LineAlarmThread2() {
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
		/*mysqlDBAdapter = new MysqlDBAdapter();
		mysqlDBAdapter.initDBAdapter(config, nodeName);*/
		
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
				String msgType = vehicleMessage.getMsgType();
				if ("0".equals(msgType)|| "1".equals(msgType)) {
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
		  isintoareaalarm = false;// 进报警判断
		  isoutareaalarm = false;// 出报警判断
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
				isintoareaalarm=true;
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
	private void saveLineAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean) {
			try{
				lineAlarmCache.setAreaId(lineAlarmBean.getLineid());
				lineAlarmCache.setLineName(lineAlarmBean.getLineName());
				
				//查询当前驾驶员信息
				String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
				if (driverinfoStr!=null&&driverinfoStr.length()>0){
					String driverInfo[]= driverinfoStr.split(":");
					vehicleMessage.setDriverId(driverInfo[1]);
					vehicleMessage.setDriverName(driverInfo[2]);
					vehicleMessage.setDriverSrc(driverInfo[10]);
				}
				
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage,lineAlarmCache);
//				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,lineAlarmCache);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,lineAlarmCache);
				logger.debug("线程:["+nId+"]【线路进出】报警【存储】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid()+",AlarmAddInfo:"+vehicleMessage.getAlarmAddInfo());
			}catch(Exception e){
				logger.error("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-【线路进出】报警存储---【数据库异常】",e);
			}
		
		if(issendmessage){//进出线路报警给终端
			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,vehicleMessage.getAlarmcode());
//			logger.debug("线程:["+nId+"]线路进出报警发送终端成功[" + vehicleMessage.getCommanddr() + "]");
		}

	}
 
	/**
	 * 更新进出线路报警
	 * @param vehicleMessage
	 * @param lineAlarmCache
	 */
	private void updateLineAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean){
			try{
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage,lineAlarmCache);
//				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,lineAlarmCache);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,lineAlarmCache);

				//存储报警事件数据

				saveAlarmEvent(vehicleMessage,lineAlarmCache);
				
				logger.debug("线程:["+nId+"]【线路进出】报警【更新】成功[" + vehicleMessage.getCommanddr() + "]Alarmid:"+vehicleMessage.getAlarmid());
			}catch(Exception e){
				logger.error("Alarmid:"+vehicleMessage.getAlarmid()+"---更新进出线路报警-【数据库异常】",e);
			}
	}
	
	/**
	 * 存储超速报警
	 * @param vehicleMessage
	 */
	private void saveOverspeed(VehicleMessageBean vehicleMessage,AlarmCacheBean lineAlarmCache,LineAlarmBean lineAlarmBean){
			String alarmId = UUID.randomUUID().toString().replace("-", "");
			lineAlarmCache.setAlarmId(alarmId);
			
			lineAlarmCache.setAlarmcode("1");
			lineAlarmCache.setAlarmSrc(5);
			lineAlarmCache.setAreaId(lineAlarmBean.getLineid());
			lineAlarmCache.setLineName(lineAlarmBean.getLineName());
			lineAlarmCache.setAlarmadd("4|"+lineAlarmBean.getLineid());
			lineAlarmCache.setAlarmlevel("A001");
			lineAlarmCache.setAlarmaddInfo("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-线段超速超速报警");
			try{
				//查询当前驾驶员信息
				String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
				if (driverinfoStr!=null&&driverinfoStr.length()>0){
					String driverInfo[]= driverinfoStr.split(":");
					vehicleMessage.setDriverId(driverInfo[1]);
					vehicleMessage.setDriverName(driverInfo[2]);
					vehicleMessage.setDriverSrc(driverInfo[10]);
				}
				
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage,lineAlarmCache);
//				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,lineAlarmCache);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,lineAlarmCache);
				logger.debug("线程:["+nId+"]【线路超速】【存储】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+lineAlarmCache.getAlarmId()+";AlarmAddInfo:"+lineAlarmCache.getAlarmaddInfo());
			}catch(Exception e){
				logger.debug("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-线段超速超速报警-存储---【数据库异常】",e);
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
		try{
			oracleDBAdapter.updateVehicleAlarm(vehicleMessage,lineAlarmCache);
//			mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,lineAlarmCache);
			redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,lineAlarmCache);

			//存储报警事件数据
			//areaAlarmCache.setAlarmAddInfo("名称："+areaAlarmBean.getAreaName()+"-类型："+sendcontent);//“围栏名称”进围栏报警
			saveAlarmEvent(vehicleMessage,lineAlarmCache);
			
			logger.debug("线程:["+nId+"]【线路超速】报警【更新】成功[" + vehicleMessage.getCommanddr() + "]车速："+vehicleMessage.getSpeed()+";Alarmid:"+lineAlarmCache.getAlarmId());
		}catch(Exception e){
			logger.error("线路:"+lineAlarmBean.getLineName()+"-线段:"+lineAlarmBean.getPid()+"-【线路超速】报警更新---【数据库异常】",e);
		}
	}
	
	/** 
	 * 报警处理
	 * @param vehicleMessage
	 * @throws NoSuchMethodException 
	 * @throws InvocationTargetException 
	 * @throws InstantiationException 
	 * @throws IllegalAccessException 
	 * @
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage) throws IllegalAccessException, InstantiationException, InvocationTargetException, NoSuchMethodException{

		String vid = vehicleMessage.getVid();
		Long currUtc = vehicleMessage.getUtc();
		String commaddr = vehicleMessage.getCommanddr();
		
		List<LineAlarmBean> areaList = TempMemory.getLineAlarmMap(vid);// 根据vid查询车辆的线路信息集合
		if (areaList != null) {
			logger.debug("[" + commaddr + "] vid:"+vid+"所绑定的线段有："+areaList.size()+"个;");
			areaList = new ArrayList<LineAlarmBean>(areaList);
			Long mapLon = vehicleMessage.getMaplon();//偏移后经度
			Long mapLat = vehicleMessage.getMaplat();//偏移后纬度
			List<String> inarealist =queryLineList(mapLon,mapLat);//查询点所在线路
			//车辆线路关系缓存
			Map<String, LineAlarmBean> reVehilceLineStatusMap = new ConcurrentHashMap<String, LineAlarmBean>();
			//线路和线段的关系需要重新考虑
			for (LineAlarmBean lineAlarmBean : areaList) {//判断用户设置
				
				lineAlarmBean.setOnline(false);
				reVehilceLineStatusMap.put(lineAlarmBean.getLineid(), lineAlarmBean);
				
				String mapkey=String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid());

				logger.debug("[" + commaddr + "] 开始处理线段：(pid:"+lineAlarmBean.getPid()+";lineId:"+lineAlarmBean.getLineid()+";lineName:"+lineAlarmBean.getLineName()+") "+(inarealist.contains(mapkey)?"当前坐标【在】线路内":"当前坐标【不在】线路内"));

				setUsetype(lineAlarmBean.getUsetype());//设置用户业务类型
				
                if (currUtc >= lineAlarmBean.getBeginTime()&& vehicleMessage.getUtc() <= lineAlarmBean.getEndTime()) {// 在线路判定周期内
						//判断当前是否在线路内
						boolean iscurrentinarea=false;
						for(String inarea:inarealist){
							if(inarea.equals(mapkey)){
								iscurrentinarea=true;
								//当车辆在线路的任一条线段上时，认为车辆在线路上
								if (reVehilceLineStatusMap.containsKey(lineAlarmBean.getLineid())&&!reVehilceLineStatusMap.get(lineAlarmBean.getLineid()).isOnline()){
									lineAlarmBean.setOnline(true);
									reVehilceLineStatusMap.put(lineAlarmBean.getLineid(), lineAlarmBean);
								}
								break;
							}
						}

						if(isoverspeedalarm){
							//围栏内超速判断
							String csLineKey=AlarmMark.XLCS+ String.valueOf(vid)+ "_"+ String.valueOf(lineAlarmBean.getPid());
							AlarmCacheBean csLineCacheBean = alarmMap.get(csLineKey);
							if (iscurrentinarea){
								//车辆判断超速时获取最大车速
								if (csLineCacheBean!=null){
									if (csLineCacheBean.getMaxSpeed()>0){
										if (vehicleMessage.getSpeed()!=null&&vehicleMessage.getSpeed()>csLineCacheBean.getMaxSpeed()){
											csLineCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
										}
									}else{
										csLineCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
									}
								}
								
								//设置线路超速阀值
								OverspeedAlarmCfgBean tmpBean = TempMemory.getOverspeedAlarmCfgMap(vehicleMessage.getVid());
								double speedScale = 1.0;
								String currTime = CDate.getTimeShort();
								
								if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
									speedScale = tmpBean.getSpeedScale()/100.0;
								}
								double speedThreshold = lineAlarmBean.getSpeedthreshold()*speedScale;
								
								vehicleMessage.setSpeedThreshold(speedThreshold);
								//判断是否超速
								if (csLineCacheBean!=null){//缓存中以有超速记录
									if(vehicleMessage.getSpeed() > speedThreshold){//持续超速
										if(!csLineCacheBean.isSaved()&&vehicleMessage.getUtc()-csLineCacheBean.getBeginVmb().getUtc()>lineAlarmBean.getSpeedtimethreshold()*1000){//大于超速持续时间

											 saveOverspeed(vehicleMessage,csLineCacheBean,lineAlarmBean) ;
											 csLineCacheBean.setBeginVmb(vehicleMessage);
											 csLineCacheBean.setSaved(true);
											 //areaAlarmCacheBean.setIssaveorverspeed(true);
										 }
										//计算平均车速
										csLineCacheBean.setAvgSpeed(vehicleMessage.getSpeed());

									}else{
										//更新超速结果
										if (csLineCacheBean.isSaved()){
											csLineCacheBean.setEndVmb(vehicleMessage);
											updateOverspeed(vehicleMessage,csLineCacheBean,lineAlarmBean);
										}
										alarmMap.remove(csLineKey);
									}
								}else{
									//添加缓存
									if(vehicleMessage.getSpeed() >speedThreshold){//判断超速	
										
										AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
										
										tmpcacheBean.setOverspeedbegintime(vehicleMessage.getUtc());
										tmpcacheBean.setSpeedThreshold(speedThreshold);
										tmpcacheBean.setUtc(vehicleMessage.getUtc());

										tmpcacheBean.setBegintime(vehicleMessage.getUtc());
										tmpcacheBean.setBeginVmb(vehicleMessage);
										tmpcacheBean.setAlarmSrc(3);
										
										tmpcacheBean.setSaved(false);
										
										//计算平均车速
										tmpcacheBean.setAvgSpeed(vehicleMessage.getSpeed());

										alarmMap.put(csLineKey, tmpcacheBean);										 
									}
								}
							}else{
								//车辆驶出线路时，结束未完成超速
								if (csLineCacheBean!=null&&csLineCacheBean.isSaved()){
									csLineCacheBean.setEndVmb(vehicleMessage);
									updateOverspeed(vehicleMessage,csLineCacheBean,lineAlarmBean);
								}
								alarmMap.remove(csLineKey);
							}
						}
					
					}else{//判断周期外
						//结束所有未结束的告警
						/*String intoLineKey = AlarmMark.XLJC +"01"+ String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid());
						AlarmCacheBean intoLineCacheBean = alarmMap.get(intoLineKey);
						if (intoLineCacheBean!=null){
							alarmMap.remove(intoLineKey);
						}
						
						String outLineKey = AlarmMark.XLJC +"02"+ String.valueOf(vid) + "_"	+ String.valueOf(lineAlarmBean.getPid());
						AlarmCacheBean outLineCacheBean = alarmMap.get(outLineKey);
						if (outLineCacheBean!=null){
							alarmMap.remove(outLineKey);
						}*/
						
						String csLineKey=AlarmMark.XLCS+ String.valueOf(vid)+ "_"+ String.valueOf(lineAlarmBean.getPid());
						AlarmCacheBean csLineCacheBean = alarmMap.get(csLineKey);
						if (csLineCacheBean!=null){
							if (csLineCacheBean.isSaved()){
								csLineCacheBean.setEndVmb(vehicleMessage);
								updateOverspeed(vehicleMessage,csLineCacheBean,lineAlarmBean);
							}
							alarmMap.remove(csLineKey);
						}
					}
				}
			
			//处理车辆进出线路报警
			Iterator it = reVehilceLineStatusMap.keySet().iterator();
			while(it.hasNext()){
				String lineId = (String) it.next();
				LineAlarmBean lineAlarmBean = reVehilceLineStatusMap.get(lineId);
				
				if (isintoareaalarm){
					String intoLineKey = AlarmMark.XLJC + String.valueOf(vid) + "_"	+ String.valueOf(lineId);
					AlarmCacheBean intoLineCacheBean = alarmMap.get(intoLineKey);
					if (lineAlarmBean.isOnline()){
						if (intoLineCacheBean!=null){
							if (!intoLineCacheBean.isSaved()){
								//保存进围栏告警
			
								VehicleMessageBean beginBean = intoLineCacheBean.getBeginVmb();
								String alarmId = UUID.randomUUID().toString().replace("-", "");
								intoLineCacheBean.setAlarmId(alarmId);
								intoLineCacheBean.setAlarmcode(Constant.ALARMCODE_INTOLINE);
								intoLineCacheBean.setAlarmlevel("A005");
								intoLineCacheBean.setAlarmSrc(2);
								intoLineCacheBean.setAlarmadd("4||0");
								intoLineCacheBean.setAreaId(lineAlarmBean.getLineid());
								intoLineCacheBean.setLineName(lineAlarmBean.getLineName());
								intoLineCacheBean.setAlarmaddInfo("线路:"+lineAlarmBean.getLineName()+"-线段："+lineAlarmBean.getPid()+"-进线路报警");//“线路名称-线段ID”-进出线段报警
								
								saveLineAlarm(beginBean,intoLineCacheBean,lineAlarmBean);
								intoLineCacheBean.setSaved(true);
								
								//更新出围栏
								intoLineCacheBean.setEndVmb(vehicleMessage);
								
								updateLineAlarm(vehicleMessage,intoLineCacheBean,lineAlarmBean);
								
								alarmMap.remove(intoLineKey);
							}
						}
					}else{
						//更新缓存状态
						AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
						tmpcacheBean.setBegintime(currUtc);
						tmpcacheBean.setBeginVmb(vehicleMessage);
						tmpcacheBean.setAlarmSrc(2);
						tmpcacheBean.setSaved(false);
						alarmMap.put(intoLineKey, tmpcacheBean);
					}
				}
				
				if (isoutareaalarm){
					String outLineKey = AlarmMark.XLSC + String.valueOf(vid) + "_"	+ String.valueOf(lineId);
					AlarmCacheBean outLineCacheBean = alarmMap.get(outLineKey);
					if (!lineAlarmBean.isOnline()){
						if (outLineCacheBean!=null){
							if (!outLineCacheBean.isSaved()){
								//保存进围栏告警
							
								VehicleMessageBean beginBean = outLineCacheBean.getBeginVmb();
								String alarmId = UUID.randomUUID().toString().replace("-", "");
								outLineCacheBean.setAlarmId(alarmId);
								outLineCacheBean.setAlarmcode(Constant.ALARMCODE_OUTLINE);
								outLineCacheBean.setAlarmlevel("A005");
								outLineCacheBean.setAlarmSrc(2);
								outLineCacheBean.setAlarmadd("4||1");
								outLineCacheBean.setAreaId(lineAlarmBean.getLineid());
								outLineCacheBean.setLineName(lineAlarmBean.getLineName());
								outLineCacheBean.setAlarmaddInfo("线路:"+lineAlarmBean.getLineName()+"-线段："+lineAlarmBean.getPid()+"-出线路报警");//“线路名称-线段ID”-进出线段报警
								
								saveLineAlarm(beginBean,outLineCacheBean,lineAlarmBean);
								outLineCacheBean.setSaved(true);
								
								//更新出围栏
								outLineCacheBean.setEndVmb(vehicleMessage);
								
								updateLineAlarm(vehicleMessage,outLineCacheBean,lineAlarmBean);
								
								alarmMap.remove(outLineKey);
							}
						}
					}else{
						//更新缓存状态
						AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
						tmpcacheBean.setBegintime(currUtc);
						tmpcacheBean.setBeginVmb(vehicleMessage);
						tmpcacheBean.setAlarmSrc(2);
						tmpcacheBean.setSaved(false);
						alarmMap.put(outLineKey, tmpcacheBean);
					}
				}
			}
		}else{
			logger.debug("[commaddr:+"+commaddr+"] vid:"+vid+" 所绑定的线段有：0个");
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
				logger.debug("线程:[" + nId + "]【围栏软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ alarmCacheBean.getAlarmId());
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
						||alarmCode.startsWith(Constant.ALARMCODE_INTOLINE+",")||alarmCode.endsWith(","+Constant.ALARMCODE_INTOLINE)||alarmCode.indexOf(","+Constant.ALARMCODE_INTOLINE+",")>-1
						||alarmCode.startsWith(Constant.ALARMCODE_OUTLINE+",")||alarmCode.endsWith(","+Constant.ALARMCODE_OUTLINE)||alarmCode.indexOf(","+Constant.ALARMCODE_OUTLINE+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}
}
