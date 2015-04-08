package com.ctfo.analy.addin.impl;


import java.lang.reflect.InvocationTargetException;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.TbLineStationBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.protocal.CommonAnalyseService;
import com.ctfo.analy.util.GeometryUtil;
import com.ctfo.statement.AlarmMark;
import com.lingtu.xmlconf.XmlConf;

 /**
  * 车辆进出站分析处理
  * @author LiangJian
  */
public class StationAnalyserThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger.getLogger(StationAnalyserThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
//	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();
	
 
	// 是否运行标志
	public boolean isRunning = true;

	public StationAnalyserThread() {
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
		logger.debug("【站点分析线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				String msgType = vehicleMessage.getMsgType();
				if (("0".equals(msgType)|| "1".equals(msgType))) {
				int isPValid = CommonAnalyseService.isPValid(vehicleMessage.getLon(), vehicleMessage.getLat(), vehicleMessage.getUtc(), vehicleMessage.getSpeed(), vehicleMessage.getDir());

				if(isPValid==0){
					logger.debug("线程[" + nId + "]【站点】【收到数据】>站点分析数据["+ vehicleMessage.getCommanddr() + "]nodeName:"+nodeName+";当前车速："+vehicleMessage.getSpeed());
					// 判断并记录报警信息
					checkAlarm(vehicleMessage);
				}else{
					String msg = "";
					if (isPValid!=0){
						msg += "不合法车辆轨迹;";
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
	 * 存储进出站点
	 * @param vehicleMessage
	 * 
	 */
	private void saveOverStationAlarm(VehicleMessageBean vehicleMessage,AlarmCacheBean alarmCacheBean,TbLineStationBean tblineStationBean){
			try{
				oracleDBAdapter.saveVehicleOverStationInfo(vehicleMessage,alarmCacheBean,tblineStationBean);
				logger.debug("线程:["+nId+"]【车辆过站信息】【存储】成功[" + vehicleMessage.getCommanddr() + "]Overid:"+alarmCacheBean.getAlarmId()+";StationName:"+tblineStationBean.getStationName());
			}catch(Exception e){
				logger.error("名称："+tblineStationBean.getStationName()+"-类型："+("0".equals(alarmCacheBean.getAlarmcode())?"进站":"出站")+"---数据库异常",e);
			}
	}

	
	public void checkAlarm(VehicleMessageBean vehicleMessage) throws IllegalAccessException, InstantiationException, InvocationTargetException, NoSuchMethodException{
		String vid = ""+vehicleMessage.getVid();
		Long currUtc = vehicleMessage.getUtc();
		String commaddr = vehicleMessage.getCommanddr();
		
		List<TbLineStationBean> stationList = TempMemory.getStationMap(vid);// 根据vid查询车辆的站点信息集合
		if (stationList != null) {
			logger.debug("commaddr:"+commaddr+" vid:"+vid+" 所绑定的站点有："+stationList.size()+"个");
			
			for (TbLineStationBean tblineStationBean : stationList) {//判断用户设置
				
				logger.debug("commaddr:"+commaddr+" 开始处理站点：(stationId:"+tblineStationBean.getStationId()+";stationName:"+tblineStationBean.getStationName()+";) ");

				double distance = GeometryUtil.getDistance(tblineStationBean.getMapLon()*1.0/600000,tblineStationBean.getMapLat()*1.0/600000,vehicleMessage.getMaplon()*1.0/600000,vehicleMessage.getMaplat()*1.0/600000);
				
				boolean inStation = false;
				if (distance>=0&&distance<=tblineStationBean.getStationRadius()){
					//在站点内
					inStation = true;
				}
				
				//进站点处理
				String intoStationKey=AlarmMark.ZDJR + vid+ "_"+ String.valueOf(tblineStationBean.getLineId()) + "_"+ String.valueOf(tblineStationBean.getStationId());
				AlarmCacheBean intoStationCacheBean = alarmMap.get(intoStationKey);
				if (inStation){
					if (intoStationCacheBean!=null){
						if (!intoStationCacheBean.isSaved()){
							//保存进站信息
							
							String alarmId = UUID.randomUUID().toString().replace("-", "");
							
							intoStationCacheBean.setAlarmId(alarmId);
							intoStationCacheBean.setAlarmcode("0");
							
							saveOverStationAlarm(vehicleMessage,intoStationCacheBean,tblineStationBean);
							intoStationCacheBean.setSaved(true);
							
							alarmMap.remove(intoStationKey);
						}
					}
				}else{
					//更新缓存状态
					AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
					tmpcacheBean.setSaved(false);
					alarmMap.put(intoStationKey, tmpcacheBean);
				}
				
				//出站点处理
				String outStationKey=AlarmMark.ZDSC + vid + "_"+ String.valueOf(tblineStationBean.getLineId())+ "_"+ String.valueOf(tblineStationBean.getStationId());
				AlarmCacheBean outStationCacheBean = alarmMap.get(outStationKey);
				if (!inStation){
					if (outStationCacheBean!=null){
						if (!outStationCacheBean.isSaved()){
							//保存出站信息
							String alarmId = UUID.randomUUID().toString().replace("-", "");
							
							outStationCacheBean.setAlarmId(alarmId);
							outStationCacheBean.setAlarmcode("1");
							
							saveOverStationAlarm(vehicleMessage,outStationCacheBean,tblineStationBean);
							outStationCacheBean.setSaved(true);
							
							alarmMap.remove(outStationKey);
						}
					}
				}else{
					//更新缓存状态
					AlarmCacheBean tmpcacheBean = new AlarmCacheBean();
					tmpcacheBean.setSaved(false);
					alarmMap.put(outStationKey, tmpcacheBean);
				}
				
				}
		}else{
			logger.debug("[commaddr:+"+commaddr+"] vid:"+vid+" 所绑定的站点有：0个");
		}
	}

	
}

