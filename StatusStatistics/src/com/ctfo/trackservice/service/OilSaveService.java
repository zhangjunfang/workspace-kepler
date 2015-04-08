package com.ctfo.trackservice.service;

import com.ctfo.trackservice.model.OilSaveBean;
import com.ctfo.trackservice.model.VehicleMessageBean;
import com.ctfo.trackservice.util.ExcConstants;

/**
 * 文件名：OilSaveService.java
 * 功能：节油驾驶分析
 *
 * @author huangjincheng
 * 2014-9-25下午3:03:21
 * 
 */
public class OilSaveService {
	//private final static Logger logger = LoggerFactory.getLogger(OilSaveService.class);
	private OilSaveBean oilSaveBean = new OilSaveBean();
	private long utc;
	private String vid;
	private VehicleMessageBean lastLocBean=null;
	//private int count = 0;
	long onlineTime = 0; // 车辆平台在线时间
	
	//private static long intervalTime = 90 * 24 * 60 * 60 * 1000l;
	
	private long urgentSpeedTime = 0l;
	private long urgentLowdownTime = 0l;
	private long overSpeedTime = 0l;
	private long overRpmTime = 0l;
	private long longIdleTime = 0l;
	private long idleAirConditionTime = 0l;
	private long gearGlideTime = 0l;
	Long startGpsTime = -1L; // GPS时间
	Long gpsTime = -1L; // GPS时间

	

	public OilSaveService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
		oilSaveBean.setStatDate(this.utc+12*60*60*1000);
		oilSaveBean.setVid(this.vid);
	}
	/**
	 * 节油驾驶统计分析(部分数据取车辆运行统计)
	 * @param trackBean
	 * @param isLastRow
	 */
	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow){
		Long gpsTime = trackBean.getUtc();
		String[] alarmCode = trackBean.getAlarmcode().split(",");
		//备份值
		if (lastLocBean==null){
			lastLocBean = trackBean;
		}
		Long lastGpsTime = lastLocBean.getUtc();
		long timediff = accountOffLineTime(gpsTime,lastGpsTime);//本条记录和上条记录的时间差 ms
		
		
		if(null != alarmCode && isContainCode(alarmCode, "48")){//急加速
			trackBean.setUrgentSpeed(true);
			if(!lastLocBean.getUrgentSpeed()){
				oilSaveBean.addUrgentSpeedNuM(1);
			}else{
				urgentSpeedTime = urgentSpeedTime + timediff;
			}
		}else {
			trackBean.setUrgentSpeed(false);
			if(lastLocBean.getUrgentSpeed()){
				urgentSpeedTime = urgentSpeedTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "49")){//急减速
			trackBean.setUrgentLowdown(true);
			if(!lastLocBean.getUrgentLowdown()){
				oilSaveBean.addUrgentLowdownNum(1);
			}else{
				urgentLowdownTime = urgentLowdownTime + timediff;
			}
		}else {
			trackBean.setUrgentLowdown(false);
			if(lastLocBean.getUrgentLowdown()){
				urgentLowdownTime = urgentLowdownTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "1")){//超速
			trackBean.setOverSpeed(true);
			if(!lastLocBean.getOverSpeed()){
				oilSaveBean.addOverSpeedNum(1);
			}else{
				overSpeedTime = overSpeedTime + timediff;
			}
		}else {
			trackBean.setOverSpeed(false);
			if(lastLocBean.getOverSpeed()){
				overSpeedTime = overSpeedTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "47")){//超转
			trackBean.setOverRpm(true);
			if(!lastLocBean.getOverRpm()){
				oilSaveBean.addOverRpmNum(1);
			}else{
				overRpmTime = overRpmTime + timediff;
			}
		}else {
			trackBean.setOverRpm(false);
			if(lastLocBean.getOverRpm()){
				overRpmTime = overRpmTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "45")){//超长怠速
			trackBean.setLongIdle(true);
			if(!lastLocBean.getLongIdle()){
				oilSaveBean.addLongIdleNum(1);
			}else{
				longIdleTime = longIdleTime + timediff;
			}
		}else {
			trackBean.setLongIdle(false);
			if(lastLocBean.getLongIdle()){
				longIdleTime = longIdleTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "46")){//怠速空调
			trackBean.setIdleAirCondition(true);
			if(!lastLocBean.getIdleAirCondition()){
				oilSaveBean.addIdleAirConditionNum(1);
			}else{
				idleAirConditionTime = idleAirConditionTime + timediff;
			}
		}else {
			trackBean.setIdleAirCondition(false);
			if(lastLocBean.getIdleAirCondition()){
				idleAirConditionTime = idleAirConditionTime + timediff;
			}
		}
		
		if(null != alarmCode && isContainCode(alarmCode, "44")){//空挡滑行
			trackBean.setGearGlide(true);
			if(!lastLocBean.getGearGlide()){
				oilSaveBean.addGearGlideNum(1);
			}else{
				gearGlideTime = gearGlideTime + timediff;
			}
		}else {
			trackBean.setGearGlide(false);
			if(lastLocBean.getGearGlide()){
				gearGlideTime = gearGlideTime + timediff;
			}
		}
		
	
		if(!"".equals(trackBean.getDriverId())){
			oilSaveBean.setDriverId(trackBean.getDriverId());
		}
		if(!"".equals(trackBean.getDriverName())){
			oilSaveBean.setDriverName(trackBean.getDriverName());
		}
	
		if (isLastRow){//当到达最后一行时
			//System.out.println("日统计mile:"+vehicleStatus.getMileage());		
			oilSaveBean.setUrgentLowdownTime(urgentLowdownTime/1000);
			oilSaveBean.setUrgentSpeedTime(urgentSpeedTime/1000);
			oilSaveBean.setOverSpeedTime(overSpeedTime/1000);
			oilSaveBean.setOverRpmTime(overRpmTime/1000);
			oilSaveBean.setLongIdleTime(longIdleTime/1000);
			oilSaveBean.setIdleAirConditionTime(idleAirConditionTime/1000);
			oilSaveBean.setGearGlideTime(gearGlideTime/1000);
			
		}
		
		lastLocBean = trackBean;
		
		
	}
	
	/*****
	 * 计算车辆在线时间
	 */
	private long accountOffLineTime(Long curUtc,Long lastUtc){
		if(lastUtc<=0||curUtc<=0){
			return 0;
		}

		if((curUtc - lastUtc) <= ExcConstants.PLATFORM_REPORT_DATA_LONGEST_INTERVAL_TIME ){
			return curUtc - lastUtc;
		}
		
		return 0;
	}
	/**
	 * 判断报警编码是否含有
	 * @param arr
	 * @param code
	 * @return
	 */
	private Boolean isContainCode(String[] arr,String code){
		for(int i = 0;i<arr.length;i++){
			if(code.equals(arr[i])){
				return true ;
			}
		}
		return false;
	}
	/**
	 * @return the oilSaveBean
	 */
	public OilSaveBean getOilSaveBean() {
		return oilSaveBean;
	}
	/**
	 * @param oilSaveBean the oilSaveBean to set
	 */
	public void setOilSaveBean(OilSaveBean oilSaveBean) {
		this.oilSaveBean = oilSaveBean;
	}
	
	
}
