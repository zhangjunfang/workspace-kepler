package com.ctfo.analy.addin.impl;


import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.FatigueAlarmCfgBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.MathUtils;
import com.lingtu.xmlconf.XmlConf;

/**
 * 疲劳驾驶报警分析线程,对所有车生效
 * 
 * @author yujch
 * 
 */
public class FatigueAnalyserThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger
			.getLogger(FatigueAnalyserThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(
			100000);

	int nId;
	XmlConf config;
	String nodeName;
	OracleDBAdapter oracleDBAdapter;
//	MysqlDBAdapter mysqlDBAdapter;
	RedisDBAdapter redisDBAdapter;

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmCacheBean> fatigueAlarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();

	// 是否运行标志
	public boolean isRunning = true;
	
	//private int count=0;//因状态不符合产生的非法运营数据计数，当连续两条数据均为因状态不符合产生的非法运营数据时，保存非法运营结束记录

	public FatigueAnalyserThread() {
		
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
		logger.debug("【疲劳驾驶软报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
//					logger.debug("非法运营软报警线程[" + nId + "]【收到数据】>实时数据["
//							+ vehicleMessage.getCommanddr() + "] nodeName:"
//							+ nodeName + ";当前车速：" + vehicleMessage.getSpeed());
				
					checkAlarmnew(vehicleMessage);

					logger.debug("FatigueAnalyserThread主线程" + vPacket.size());
				}
			} catch (Exception e) {
				e.printStackTrace();
				logger.error(e);
			}
		}
	}

	/**
	 * 疲劳驾驶软报警分析处理
	 * 
	 * @param vehicleMessage
	 * 
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage) {
		long sysTime = System.currentTimeMillis();
		
		
		//车辆行驶   车速大于5km/h 点火状态为开 
		String vid = vehicleMessage.getVid();
		Long utc = vehicleMessage.getUtc();
		int speed = vehicleMessage.getSpeed();
		
		//补传数据时间和当前服务器时间相差小于2分钟，则认为此数据为非补传数据，补传数据不进行判断
		if (Math.abs((sysTime - vehicleMessage.getUtc()))<=2*60*1000) {

		//设置疲劳驾驶判定时长
		FatigueAlarmCfgBean tmpBean = TempMemory.getFatigueAlarmCfgMap(vid);
		Long deferred = 4L;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			deferred = tmpBean.getDeferred();
		}
		
		Long fatigueDeferred = deferred*60*60*1000;
		
		
		String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
		//判断车辆是否行使
		boolean isRun = false;
		if (speed>=50&&MathUtils.check("0", binaryStr)){
			isRun = true;
		}

		String key = "fatigue_"+vid;
		//从缓存中取出位置对象
		AlarmCacheBean fatigueAlarmBean = fatigueAlarmMap.get(key);
		//if (isRun&&){
			if (fatigueAlarmBean != null){
				//缓存中已有记录，表明已开始疲劳驾驶判定
				
				//判断是否为连续记录：本次时间 - 上次时间 < 20 *60 *60*1000
				boolean isSerial = false;
				if ((utc - fatigueAlarmBean.getUtc()) < 20*60*1000 ){
					isSerial = true;
				}
				
				//如果记录不连续，则结束上次疲劳驾驶报警
				
				fatigueAlarmBean.setUtc(utc);
				
				if (isSerial){
					
					fatigueAlarmBean.setEndTime(utc);
					fatigueAlarmBean.setEndVmb(vehicleMessage);
					
					if (isRun){
						//连续记录,车辆正在运行
						if ((utc-fatigueAlarmBean.getBegintime())>=fatigueAlarmBean.getBufferTime()&&!fatigueAlarmBean.isSaved()){
							//达到疲劳驾驶报警条件，存储疲劳驾驶报警信息
							String alarmId = UUID.randomUUID().toString().replace("-", "");
							fatigueAlarmBean.setAlarmId(alarmId);
							fatigueAlarmBean.setBegintime(utc);
							fatigueAlarmBean.setAlarmSrc(2);
							fatigueAlarmBean.setAlarmcode("2");
							fatigueAlarmBean.setAlarmlevel("A001");
							fatigueAlarmBean.setAlarmadd("2");
							fatigueAlarmBean.setAlarmaddInfo("疲劳驾驶报警");
							fatigueAlarmBean.setBeginVmb(vehicleMessage);
							
							dealSaveFatiguealarm(vehicleMessage,fatigueAlarmBean);
							fatigueAlarmBean.setSaved(true);
						}
						fatigueAlarmBean.setStopVehicleTime(0L);
					}else{
						//车辆停止时，结束本次疲劳驾驶
						if (fatigueAlarmBean.isSaved()){
							if (fatigueAlarmBean.getStopVehicleTime()==null||fatigueAlarmBean.getStopVehicleTime()==0L){
								fatigueAlarmBean.setStopVehicleTime(utc);
							}
							
							//疲劳驾驶停车20分钟后认为本次疲劳驾驶结束
							if ((utc-fatigueAlarmBean.getStopVehicleTime())>=20*60*1000){
								dealUpdateFatiguealarm(vehicleMessage,fatigueAlarmBean);
								
								//从缓存中移除记录
								fatigueAlarmMap.remove(key);
							}
							
						}else{
							//从缓存中移除记录
							fatigueAlarmMap.remove(key);
						}
					}
				}else{
					//结束本次疲劳驾驶报警
					if (fatigueAlarmBean.isSaved()){
						fatigueAlarmBean.setEndTime(utc);
						fatigueAlarmBean.setEndVmb(vehicleMessage);
						dealUpdateFatiguealarm(fatigueAlarmBean.getEndVmb(),fatigueAlarmBean);
					}
					//从缓存中移除记录
					fatigueAlarmMap.remove(key);
					
					if (isRun){
						//添加本次疲劳驾驶告警
						AlarmCacheBean bean = new AlarmCacheBean();
						bean.setVid(vid);
						bean.setUtc(vehicleMessage.getUtc());
						bean.setBegintime(vehicleMessage.getUtc());
						bean.setBufferTime(fatigueDeferred);//单位ms
			
						fatigueAlarmMap.put(key, bean);
					}
				}

			}else{
				//缓存中没有此车疲劳驾驶信息，则存入当前对象，即为疲劳驾驶判断开时时间，并缓存记录判定时长
				if (isRun) {
					AlarmCacheBean bean = new AlarmCacheBean();
					bean.setVid(vid);
					bean.setUtc(vehicleMessage.getUtc());
					bean.setBegintime(vehicleMessage.getUtc());
					bean.setBufferTime(fatigueDeferred);//单位ms
		
					fatigueAlarmMap.put(key, bean);
				}
			}

			}else{
				logger.info("实时数据时间和当前服务器时间相差较大："+sysTime +"-"+vehicleMessage.getUtc()+" 大于 "+2*60*1000+"ms");
			}
		} 
	
	/**
	 * 判断当前时间是否在非法运营时间段内
	 * 时间段肯定在一天之内，不存在跨天情况
	 * @param currentTime
	 * @param beginTime
	 * @param endTime
	 * @return
	 */
	 private boolean checkTime(long currentTime,String beginTime,String endTime){
			boolean flag = false;
			String currDay = CDate.getStringDateShort();
			
			long fromTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(beginTime)*1000;
			long toTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(endTime)*1000;
			
			if (fromTime<currentTime&&currentTime<=toTime){
				flag=true;
			}

			return flag;
		}
	
	private long getEndTime(long currentTime,String beginTime,String endTime){
		
		String currDay = CDate.getStringDateShort();
		
		long fromTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(beginTime)*1000;
		long toTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(endTime)*1000;

		return toTime;
	}
	
	/**
	 * 判断存储疲劳驾驶报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealSaveFatiguealarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean fatigueAlarmCacheBean) {
			logger.debug("【疲劳驾驶】保存实时告警信息:"+vehicleMessage.getCommanddr());
			saveFatigueAlarm(vehicleMessage, fatigueAlarmCacheBean);
	}

	/**
	 * 判断更新非法运营报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateFatiguealarm(VehicleMessageBean vehicleMessage,
			 AlarmCacheBean fatigueAlarmCacheBean) {
			logger.debug("【疲劳驾驶】更新实时告警信息并保存告警事件:"+vehicleMessage.getCommanddr()+" 结束时间："+fatigueAlarmCacheBean.getEndTime());
			updateFatigueAlarm(vehicleMessage, fatigueAlarmCacheBean);
			if (fatigueAlarmCacheBean.getAlarmSrc()==2){
				//平台判断的疲劳驾驶保存事件记录
				logger.debug("【疲劳驾驶】驾驶行为事件开始保存:"+vehicleMessage.getCommanddr()+" alarmSRC："+fatigueAlarmCacheBean.getAlarmSrc()+" bufferTime:"+fatigueAlarmCacheBean.getBufferTime());
				saveFatigueAlarmEvent(vehicleMessage, fatigueAlarmCacheBean);
			}
			
	}
	
	/**
	 * 存储非法运营软报警
	 * 
	 * @param vehicleMessage
	 * 
	 */
	private void saveFatigueAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean fatigueAlarmBean) {
		//if (isalarmtoplat) { 非法运营报警给平台
			try {
				//查询当前驾驶员信息
				String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
				if (driverinfoStr!=null&&driverinfoStr.length()>0){
					String driverInfo[]= driverinfoStr.split(":");
					vehicleMessage.setDriverId(driverInfo[1]);
					vehicleMessage.setDriverName(driverInfo[2]);
					vehicleMessage.setDriverSrc(driverInfo[10]);
				}
				
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage,fatigueAlarmBean);
//				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,fatigueAlarmBean);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,fatigueAlarmBean);
				logger.debug("线程:[" + nId + "]【疲劳驾驶软报警】【存储】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ fatigueAlarmBean.getAlarmId() + ";AlarmAddInfo:"
						+ fatigueAlarmBean.getAlarmaddInfo());
			} catch (Exception e) {
				logger.error("名称：疲劳驾驶软报警---数据库异常", e);
			}
		//}

			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_FATIGUE);
			

	}

	/**
	 * 更新非法运营软报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateFatigueAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean fatigueAlarmCache) {
		//if (isalarmtoplat) {// 进出围栏报警给平台
			try {
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage,fatigueAlarmCache);
//				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,fatigueAlarmCache);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,fatigueAlarmCache);
				logger.debug("线程:[" + nId + "]【疲劳驾驶软报警】【更新】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid());
			} catch (Exception e) {
				logger.error("Alarmid:" + fatigueAlarmCache.getAlarmId()
						+ "---更新疲劳驾驶软报警-数据库异常", e);
			}
		}
	
	/**
	 * 存储驾驶行为事件数据
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void saveFatigueAlarmEvent(VehicleMessageBean vehicleMessage,
			AlarmCacheBean fatigueAlarmCache) {
			try {
				oracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,fatigueAlarmCache);
				logger.debug("线程:[" + nId + "]【疲劳驾驶软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid());
			} catch (Exception e) {
				logger.error("Alarmid:" + fatigueAlarmCache.getAlarmId()
						+ "---添加疲劳驾驶软报警驾驶行为事件-数据库异常", e);
			}
		}

	/**
	 * 疲劳驾驶软报警分析处理
	 * 
	 * 当不在车辆判断时间范围之内时 按终端报警标记位来判断，结束时也按报警标记位来判定；
	 * 当车辆在判断时间范围之内时 按平台设定的判断标准来判断，结束时也按平台标准来判断；
	 * 
	 * 
	 * @param vehicleMessage
	 * 
	 */
	public void checkAlarmnew(VehicleMessageBean vehicleMessage) {
		long sysTime = System.currentTimeMillis();
		
		
		//车辆行驶   车速大于5km/h 点火状态为开 
		String vid = vehicleMessage.getVid();
		Long utc = vehicleMessage.getUtc();
		int speed = vehicleMessage.getSpeed();
		
		boolean inJudgeArea = false; 
		
		//补传数据时间和当前服务器时间相差小于2分钟，则认为此数据为非补传数据，补传数据不进行判断
		if (Math.abs((sysTime - vehicleMessage.getUtc()))<=2*60*1000) {

		//设置疲劳驾驶判定时长
		FatigueAlarmCfgBean tmpBean = TempMemory.getFatigueAlarmCfgMap(vid);
		Long deferred = 4L;
		String currTime = CDate.getTimeShort();
		
		if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
			deferred = tmpBean.getDeferred();
			inJudgeArea = true;
		}
		
		Long fatigueDeferred = deferred*60*60*1000;
		
		
		String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
		String standAlarmCode = MathUtils.getBinaryString(vehicleMessage.getBaseAlarmStatus());
		//判断车辆是否行使
		boolean isRun = false;
		if (speed>=50&&MathUtils.check("0", binaryStr)){
			isRun = true;
		}
		
		//判断疲劳驾驶标记是否有效
		boolean fatigueFlag = false;
		if(MathUtils.check("2", standAlarmCode)){
			fatigueFlag = true;
		}

		String key = "fatigue_"+vid;
		//从缓存中取出位置对象
		AlarmCacheBean fatigueAlarmBean = fatigueAlarmMap.get(key);
		//if (isRun&&){
			if (fatigueAlarmBean != null){
				//缓存中已有记录，表明已开始疲劳驾驶判定
				if (fatigueAlarmBean.getBufferTime()==null){
					if (!fatigueFlag){
						//使用终端标准，如果此时车辆告警标记位切换，则结束报警
						fatigueAlarmBean.setEndTime(utc);
						fatigueAlarmBean.setEndVmb(vehicleMessage);
						dealUpdateFatiguealarm(vehicleMessage,fatigueAlarmBean);
						
						//从缓存中移除记录
						fatigueAlarmMap.remove(key);
					}
				}else{
					//判断是否为连续记录：本次时间 - 上次时间 < 20 *60 *60*1000
					boolean isSerial = false;
					if ((utc - fatigueAlarmBean.getUtc()) < 20*60*1000 ){
						isSerial = true;
					}
					
					//如果记录不连续，则结束上次疲劳驾驶报警
					
					fatigueAlarmBean.setUtc(utc);
					
					if (isSerial){
						
						fatigueAlarmBean.setEndTime(utc);
						fatigueAlarmBean.setEndVmb(vehicleMessage);
						
						if (isRun){
							//连续记录,车辆正在运行
							if ((utc-fatigueAlarmBean.getBegintime())>=fatigueAlarmBean.getBufferTime()&&!fatigueAlarmBean.isSaved()){
								//达到疲劳驾驶报警条件，存储疲劳驾驶报警信息
								String alarmId = UUID.randomUUID().toString().replace("-", "");
								fatigueAlarmBean.setAlarmId(alarmId);
								fatigueAlarmBean.setBegintime(utc);
								fatigueAlarmBean.setAlarmSrc(2);
								fatigueAlarmBean.setAlarmcode("2");
								fatigueAlarmBean.setAlarmlevel("A001");
								fatigueAlarmBean.setAlarmadd("2");
								fatigueAlarmBean.setAlarmaddInfo("疲劳驾驶报警");
								fatigueAlarmBean.setBeginVmb(vehicleMessage);
								
								dealSaveFatiguealarm(vehicleMessage,fatigueAlarmBean);
								fatigueAlarmBean.setSaved(true);
							}
							fatigueAlarmBean.setStopVehicleTime(0L);
						}else{
							//车辆停止时，结束本次疲劳驾驶
							if (fatigueAlarmBean.isSaved()){
								if (fatigueAlarmBean.getStopVehicleTime()==null||fatigueAlarmBean.getStopVehicleTime()==0L){
									fatigueAlarmBean.setStopVehicleTime(utc);
								}
								
								//疲劳驾驶停车20分钟后认为本次疲劳驾驶结束
								if ((utc-fatigueAlarmBean.getStopVehicleTime())>=20*60*1000){
									dealUpdateFatiguealarm(vehicleMessage,fatigueAlarmBean);
									
									//从缓存中移除记录
									fatigueAlarmMap.remove(key);
								}
								
							}else{
								//从缓存中移除记录
								fatigueAlarmMap.remove(key);
							}
						}
					}else{
						//结束本次疲劳驾驶报警
						if (fatigueAlarmBean.isSaved()){
							fatigueAlarmBean.setEndTime(utc);
							fatigueAlarmBean.setEndVmb(vehicleMessage);
							dealUpdateFatiguealarm(fatigueAlarmBean.getEndVmb(),fatigueAlarmBean);
						}
						//从缓存中移除记录
						fatigueAlarmMap.remove(key);
						
						if (isRun){
							//添加本次疲劳驾驶告警
							AlarmCacheBean bean = new AlarmCacheBean();
							bean.setVid(vid);
							bean.setUtc(vehicleMessage.getUtc());
							bean.setBegintime(vehicleMessage.getUtc());
							bean.setBufferTime(fatigueDeferred);//单位ms
				
							fatigueAlarmMap.put(key, bean);
						}
					}
				}

			}else{
				//缓存中没有此车疲劳驾驶信息，则存入当前对象，即为疲劳驾驶判断开时时间，并缓存记录判定时长
				if (inJudgeArea){
					if (isRun) {
						AlarmCacheBean bean = new AlarmCacheBean();
						bean.setVid(vid);
						bean.setUtc(vehicleMessage.getUtc());
						bean.setBegintime(vehicleMessage.getUtc());
						bean.setBufferTime(fatigueDeferred);//单位ms
			
						fatigueAlarmMap.put(key, bean);
					}
				}else{
					//基本报警位中包含疲劳驾驶报警
					if (fatigueFlag){
						AlarmCacheBean bean = new AlarmCacheBean();
						
						String alarmId = UUID.randomUUID().toString().replace("-", "");
						bean.setVid(vid);
						bean.setUtc(vehicleMessage.getUtc());
						bean.setBegintime(vehicleMessage.getUtc());
						bean.setAlarmId(alarmId);
						bean.setAlarmSrc(1);
						bean.setAlarmcode("2");
						bean.setAlarmlevel("A001");
						bean.setAlarmadd("2");
						bean.setAlarmaddInfo("疲劳驾驶报警");
						bean.setBeginVmb(vehicleMessage);
						
						//bean.setBufferTime(fatigueDeferred);单位ms
						//用bufferTime 是否为空来判断疲劳驾驶应用哪个标准
						
						dealSaveFatiguealarm(vehicleMessage,bean);
						bean.setSaved(true);
			
						fatigueAlarmMap.put(key, bean);
					}
				}
			}

			}else{
				logger.info("实时数据时间和当前服务器时间相差较大："+sysTime +"-"+vehicleMessage.getUtc()+" 大于 "+2*60*1000+"ms");
			}
		} 
}
