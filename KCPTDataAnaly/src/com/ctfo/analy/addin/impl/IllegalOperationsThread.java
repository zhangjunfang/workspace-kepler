package com.ctfo.analy.addin.impl;

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
import com.ctfo.analy.beans.IllegalOptionsAlarmBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.OracleDBAdapter;
import com.ctfo.analy.dao.RedisDBAdapter;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.MathUtils;
import com.lingtu.xmlconf.XmlConf;

/**
 * 非法运营软报警 企业设置禁止运营时间段及报警触发时长，车辆在该时段内正常行驶时长大于等于报警触发时长时即为非法运营
 * 
 * @author yujch
 * 
 */
public class IllegalOperationsThread extends Thread implements PacketAnalyser {

	private static final Logger logger = Logger
			.getLogger(IllegalOperationsThread.class);
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
	private Map<String, AlarmCacheBean> illegalOptionsAlarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();

	// 是否运行标志
	public boolean isRunning = true;
	
	//private int count=0;//因状态不符合产生的非法运营数据计数，当连续两条数据均为因状态不符合产生的非法运营数据时，保存非法运营结束记录

	public IllegalOperationsThread() {
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
		logger.debug("【非法运营软报警线程[" + nId + "]启动】");
		while (isRunning) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessageBean vehicleMessage = vPacket.take();
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
//					logger.debug("非法运营软报警线程[" + nId + "]【收到数据】>实时数据["
//							+ vehicleMessage.getCommanddr() + "] nodeName:"
//							+ nodeName + ";当前车速：" + vehicleMessage.getSpeed());
					// 判断并记录报警信息
					boolean isAllowAnaly = isAllowAnaly(vehicleMessage.getCommanddr());
					if (isAllowAnaly){
						checkAlarm(vehicleMessage);
					}else{
						logger.debug("企业不进行非法营运告警分析[" + vehicleMessage.getCommanddr()+"]");
					}
					logger.debug("IllegalOperations主线程" + vPacket.size());
				}
			} catch (Exception e) {
				e.printStackTrace();
				logger.error(e);
			}
		}
	}

	/**
	 * 非法运营软报警分析处理
	 * 
	 * @param vehicleMessage
	 * 
	 */
	public void checkAlarm(VehicleMessageBean vehicleMessage) {

		AlarmCacheBean illeOptCacheBean = null;// 非法运营缓存
		String vid = vehicleMessage.getVid();
		String cacheKey = vid + "_FFYY";
		IllegalOptionsAlarmBean ioabean = TempMemory.getIlleOptAlarmMap(vid);// 根据vid查询车辆当前的非法运营软报警配置
		if (ioabean != null) {//当此车有配置信息时才判断非法运营
			long sysTime = System.currentTimeMillis();
			//补传数据时间和当前服务器时间相差小于1分钟，则认为此数据为非补传数据，补传数据不进行判断
			//判断是否为补传数据
			logger.debug("非法运营软报警线程[" + nId + "]【数据处理开始】>实时数据["
					+ vehicleMessage.getCommanddr() + "] nodeName:"
					+ nodeName + ";车辆："+vehicleMessage.getVehicleno()+"当前服务器速度：" + vehicleMessage.getSpeed()+";当前指令中时间："+vehicleMessage.getUtc());
			if (Math.abs((sysTime - vehicleMessage.getUtc()))<=30*60*1000) {
				
				long currentTime = vehicleMessage.getUtc();
			//非法运营报警：服务器时间在判定时间范围内，点火状态为开，当前车速大于5Km/h,原始车速大于等于50
			if (checkTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime())){//符合非法运营时间区间条件
				logger.debug("符合时间区间条件 --"+vehicleMessage.getCommanddr());
					String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
					
					if (MathUtils.check("0", binaryStr)&&vehicleMessage.getSpeed()>=50){ //符合非法运营状态条件
						logger.debug("符合非法运营状态条件 --"+vehicleMessage.getCommanddr());
						illeOptCacheBean = illegalOptionsAlarmMap.get(cacheKey);//取得车辆对应缓存对象
						if (illeOptCacheBean != null){
							illeOptCacheBean.setCount(0);
							//判断是否符合非法运营时间条件，初始时间到当前时间大于等于报警判断时间
							if (!illeOptCacheBean.isSaved()&&(currentTime - illeOptCacheBean.getBegintime()) >= ioabean.getDeferred()*60*1000){
								//符合报警触发条件，保存实时报警数据
								illeOptCacheBean.setBegintime(currentTime);
								illeOptCacheBean.setBeginVmb(vehicleMessage);
								dealSaveIlleOptalarm(vehicleMessage,illeOptCacheBean);
								illeOptCacheBean.setSaved(true);
							}else{
								//不符合触发报警条件，继续进行计算
								if (currentTime > illeOptCacheBean.getUtc()){
									illeOptCacheBean.setUtc(currentTime);
								}
								logger.debug("【非法运营】缓存非法运营车辆对象--更新"+vehicleMessage.getCommanddr());
							}
							
							//缓存结束时数据对象
							setMileageOil(vehicleMessage,illeOptCacheBean);
							
							
						}else{
							illeOptCacheBean=new AlarmCacheBean();
							illeOptCacheBean.setUtc(currentTime);
							illeOptCacheBean.setBegintime(currentTime);
							illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
							illeOptCacheBean.setBeginVmb(vehicleMessage);
							illeOptCacheBean.setAlarmcode("110");
							illeOptCacheBean.setAlarmlevel("A001");
							illeOptCacheBean.setAlarmSrc(2);
							illeOptCacheBean.setEndVmb(vehicleMessage);
							illeOptCacheBean.setAlarmId(UUID.randomUUID().toString().replace("-", ""));
							illegalOptionsAlarmMap.put(cacheKey,illeOptCacheBean);
							logger.debug("【非法运营】缓存非法运营车辆对象--添加"+vehicleMessage.getCommanddr());
						}
					}else{
						//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
						illeOptCacheBean = illegalOptionsAlarmMap.get(cacheKey);//取得车辆对应缓存对象
						if (illeOptCacheBean!=null){
							if (illeOptCacheBean.getCount()==0){
								illeOptCacheBean.setUtc(currentTime);
								
								long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
								if (currentTime>endTime&&endTime>0){
									illeOptCacheBean.setEndTime(endTime);
									vehicleMessage.setUtc(endTime);
								}else{
									illeOptCacheBean.setEndTime(currentTime);
								}
								//illeOptCacheBean.setEndTime2(currentTime);
								//缓存结束时数据对象
								setMileageOil(vehicleMessage,illeOptCacheBean);
							}
							
							if (illeOptCacheBean.getCount()>=2&&illeOptCacheBean.isSaved()){
								//更新非法运营软报警时长等信息，清楚此车的非法运营信息
								dealUpdateIlleOptalarm(illeOptCacheBean.getEndVmb(),illeOptCacheBean);
								illegalOptionsAlarmMap.remove(cacheKey);
								illeOptCacheBean.setCount(0);
							}else{
								illeOptCacheBean.setCount(illeOptCacheBean.getCount()+1);
							}
						}
					}
				}else{
					logger.debug("不符合时间区间条件 --"+vehicleMessage.getCommanddr());
					//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
					illeOptCacheBean = illegalOptionsAlarmMap.get(cacheKey);//取得车辆对应缓存对象
					if (illeOptCacheBean!=null&&illeOptCacheBean.isSaved()){
						illeOptCacheBean.setUtc(currentTime);
						
						long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
						if (currentTime>endTime&&endTime>0){
							illeOptCacheBean.setEndTime(endTime);
							vehicleMessage.setUtc(endTime);
						}else{
							illeOptCacheBean.setEndTime(currentTime);
						}

						//更新非法运营软报警时长等信息，清楚此车的非法运营信息
						setMileageOil(vehicleMessage,illeOptCacheBean);
						
						dealUpdateIlleOptalarm(illeOptCacheBean.getEndVmb(),illeOptCacheBean);
					}
					
					illegalOptionsAlarmMap.remove(cacheKey);
				}
				
			}else{
				logger.info("实时数据时间和当前服务器时间相差较大："+sysTime +"-"+vehicleMessage.getUtc()+" 大于 "+30*60*1000);
			}
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
	 * 判断存储非法运营报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealSaveIlleOptalarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean illeOptAlarmCacheBean) {
			logger.debug("【非法运营】保存实时告警信息:"+vehicleMessage.getCommanddr());
			saveIlleOptAlarm(vehicleMessage, illeOptAlarmCacheBean);
	}

	/**
	 * 判断更新非法运营报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateIlleOptalarm(VehicleMessageBean vehicleMessage,
			 AlarmCacheBean illeOptAlarmCacheBean) {
			logger.debug("【非法运营】更新实时告警信息并保存告警事件:"+vehicleMessage.getCommanddr()+" 结束时间："+illeOptAlarmCacheBean.getEndTime());
			updateIlleOptAlarm(vehicleMessage, illeOptAlarmCacheBean);
			saveIlleOptAlarmEvent(vehicleMessage, illeOptAlarmCacheBean);
	}
	
	/**
	 * 存储非法运营软报警
	 * 
	 * @param vehicleMessage
	 * 
	 */
	private void saveIlleOptAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean illeOPptAlarmBean) {
		//if (isalarmtoplat) { 非法运营报警给平台
			illeOPptAlarmBean.setAlarmcode("110");
			illeOPptAlarmBean.setAlarmlevel("A001");
			illeOPptAlarmBean.setAlarmSrc(2);
			illeOPptAlarmBean.setAlarmadd("2");
			illeOPptAlarmBean.setAlarmaddInfo("非法运营报警");
			try {
				//查询当前驾驶员信息
				String driverinfoStr = redisDBAdapter.getCurrentDriverInfo(vehicleMessage.getVid());
				if (driverinfoStr!=null&&driverinfoStr.length()>0){
					String driverInfo[]= driverinfoStr.split(":");
					vehicleMessage.setDriverId(driverInfo[1]);
					vehicleMessage.setDriverName(driverInfo[2]);
					vehicleMessage.setDriverSrc(driverInfo[10]);
				}
				
				oracleDBAdapter.saveVehicleAlarm(vehicleMessage,illeOPptAlarmBean);
//				mysqlDBAdapter.saveVehicleAlarm(vehicleMessage,illeOPptAlarmBean);
				redisDBAdapter.setAnalysisAlarmInfo(vehicleMessage,illeOPptAlarmBean);
				logger.debug("线程:[" + nId + "]【非法运营软报警】【存储】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ illeOPptAlarmBean.getAlarmId() + ";AlarmAddInfo:"
						+ illeOPptAlarmBean.getAlarmaddInfo());
			} catch (Exception e) {
				logger.error("名称：非法运营软报警---数据库异常", e);
			}
		//}

			TerminalAlarmThread.checkIsHasALarmNotice(vehicleMessage,Constant.ALARMCODE_ILLEOPT);
			

	}

	/**
	 * 更新非法运营软报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateIlleOptAlarm(VehicleMessageBean vehicleMessage,
			AlarmCacheBean illeOptAlarmCache) {
		//if (isalarmtoplat) {// 进出围栏报警给平台
			try {
				oracleDBAdapter.updateVehicleAlarm(vehicleMessage,illeOptAlarmCache);
//				mysqlDBAdapter.updateVehicleAlarm(vehicleMessage,illeOptAlarmCache);
				redisDBAdapter.removeAnalysisAlarmInfo(vehicleMessage,illeOptAlarmCache);
				logger.debug("线程:[" + nId + "]【非法运营软报警】【更新】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ illeOptAlarmCache.getAlarmId());
			} catch (Exception e) {
				logger.error("Alarmid:" + illeOptAlarmCache.getAlarmId()
						+ "---更新非法运营软报警-数据库异常", e);
			}
		}
	
	/**
	 * 存储驾驶行为事件数据
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void saveIlleOptAlarmEvent(VehicleMessageBean vehicleMessage,
			AlarmCacheBean illeOptAlarmCache) {
			try {
				oracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,illeOptAlarmCache);
				logger.debug("线程:[" + nId + "]【非法运营软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ illeOptAlarmCache.getAlarmId());
			} catch (Exception e) {
				logger.error("Alarmid:" + illeOptAlarmCache.getAlarmId()
						+ "---添加非法运营软报警驾驶行为事件-数据库异常", e);
			}
		}
	
	private void setMileageOil(VehicleMessageBean vehicleMessage,AlarmCacheBean illeOptCacheBean){
		//缓存最大速度
		if (illeOptCacheBean.getMaxSpeed()<vehicleMessage.getSpeed()){
			illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
		}
		//计算里程、油耗信息
		if (vehicleMessage.getOil()>0&&illeOptCacheBean.getEndVmb().getOil()>0){
			long costOil = vehicleMessage.getOil() - illeOptCacheBean.getEndVmb().getOil();
			if (costOil>0){
				illeOptCacheBean.setOil(illeOptCacheBean.getOil() + costOil);
			}
		}
		if (vehicleMessage.getMetOil()>0&&illeOptCacheBean.getEndVmb().getMetOil()>0){
			long costMetOil = vehicleMessage.getMetOil() -illeOptCacheBean.getEndVmb().getMetOil();
			if (costMetOil>0){
				illeOptCacheBean.setMetOil(illeOptCacheBean.getMetOil()+costMetOil);
			}
		}
		
		if (vehicleMessage.getMileage()>0&&illeOptCacheBean.getEndVmb().getMileage()>0){
			long costMileage = vehicleMessage.getMileage()-illeOptCacheBean.getEndVmb().getMileage();
			if (costMileage>0){
				illeOptCacheBean.setMileage(illeOptCacheBean.getMileage()+costMileage);
			}
		}
		
		//if (illeOptCacheBean.getEndTime()>=vehicleMessage.getUtc()){
			illeOptCacheBean.setEndVmb(vehicleMessage);
		//}
		
	}
	
	/**
	 * 判断该车辆所在企业是否允许进行非法营运告警分析
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
				if (alarmCode.startsWith(Constant.ALARMCODE_ILLEOPT+",")||alarmCode.endsWith(","+Constant.ALARMCODE_ILLEOPT)||alarmCode.indexOf(","+Constant.ALARMCODE_ILLEOPT+",")>-1){
					flag = true;
				}
			}
		}
		return flag;
	}

}
