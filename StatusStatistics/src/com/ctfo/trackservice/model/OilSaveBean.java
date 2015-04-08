package com.ctfo.trackservice.model;

/**
 * 文件名：OilSaveBean.java
 * 功能：节油驾驶
 *
 * @author huangjincheng
 * 2014-9-25下午3:05:23
 * 
 */
public class OilSaveBean {
	
	private String vid;
	
	private long statDate = 0;//统计日期
	
	private String driverId = "";//驾驶员id
	
	private String driverName = "";//驾驶员名册
	
	private long mileage = 0; // 当日行驶里程
	
	private long costOil = 0; // 当日油耗
	
	private long overSpeedTime = 0l; //超速时长
	
	private long overSpeedNum = 0l; //超速次数
	
	private long overRpmTime = 0l; //超转时长
	
	private long overRpmNum = 0l; //超转次数
	
	private long urgentSpeedTime = 0l;//急加速时长
	
	private long urgentSpeedNuM = 0l;//急加速次数
	
	private long urgentLowdownTime = 0l; //急减速时长
	
	private long urgentLowdownNum = 0l; //急减速次数
	
	private long longIdleTime = 0l;
	
	private long longIdleNum = 0l;
	
	private long idleAirConditionTime = 0l; //怠速空调
	
	private long idleAirConditionNum = 0l;
	
	private long airConditionTime = 0l; //空调总时长
	
	private long airConditionNum = 0l;//空调总次数
	
	private long gearGlideTime = 0l;//空挡滑行
	
	private long gearGlideNum = 0l;
	
	private long warmWindTime = 0l;//暖风
	
	private long economicRunTime = 0l;//超经济区运行时长（应用上超经济区运行比例 = round（economicRunTime/engineRotateTime * 100,2））
	
	private long precise_oil =0;
	
	private long ecuOil =0;
	
	private long ecuRunningOil =0;
	
	private long metRunningOil =0;
	
	private long engineTime =0;
	/**
	 * @return the vid
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * @param vid the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * @return the statDate
	 */
	public long getStatDate() {
		return statDate;
	}

	/**
	 * @param statDate the statDate to set
	 */
	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}

	/**
	 * @return the mileage
	 */
	public long getMileage() {
		return mileage;
	}

	/**
	 * @param mileage the mileage to set
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	/**
	 * @return the costOil
	 */
	public long getCostOil() {
		return costOil;
	}

	/**
	 * @param costOil the costOil to set
	 */
	public void setCostOil(long costOil) {
		this.costOil = costOil;
	}

	/**
	 * @return the overSpeedTime
	 */
	public long getOverSpeedTime() {
		return overSpeedTime;
	}

	/**
	 * @param overSpeedTime the overSpeedTime to set
	 */
	public void setOverSpeedTime(long overSpeedTime) {
		this.overSpeedTime = overSpeedTime;
	}

	/**
	 * @return the overSpeedNum
	 */
	public long getOverSpeedNum() {
		return overSpeedNum;
	}

	/**
	 * @param overSpeedNum the overSpeedNum to set
	 */
	public void setOverSpeedNum(long overSpeedNum) {
		this.overSpeedNum = overSpeedNum;
	}
	
	public void addOverSpeedNum(int overSpeedNum) {
		this.overSpeedNum = this.overSpeedNum + overSpeedNum;
	}

	/**
	 * @return the overRpmTime
	 */
	public long getOverRpmTime() {
		return overRpmTime;
	}

	/**
	 * @param overRpmTime the overRpmTime to set
	 */
	public void setOverRpmTime(long overRpmTime) {
		this.overRpmTime = overRpmTime;
	}

	/**
	 * @return the overRpmNum
	 */
	public long getOverRpmNum() {
		return overRpmNum;
	}

	/**
	 * @param overRpmNum the overRpmNum to set
	 */
	public void setOverRpmNum(long overRpmNum) {
		this.overRpmNum = overRpmNum;
	}
	
	public void addOverRpmNum(int overRpmNum) {
		this.overRpmNum = this.overRpmNum + overRpmNum;
	}

	/**
	 * @return the urgentSpeedTime
	 */
	public long getUrgentSpeedTime() {
		return urgentSpeedTime;
	}

	/**
	 * @param urgentSpeedTime the urgentSpeedTime to set
	 */
	public void setUrgentSpeedTime(long urgentSpeedTime) {
		this.urgentSpeedTime = urgentSpeedTime;
	}

	/**
	 * @return the urgentSpeedNuM
	 */
	public long getUrgentSpeedNuM() {
		return urgentSpeedNuM;
	}

	/**
	 * @param urgentSpeedNuM the urgentSpeedNuM to set
	 */
	public void setUrgentSpeedNuM(long urgentSpeedNuM) {
		this.urgentSpeedNuM = urgentSpeedNuM;
	}
	
	public void addUrgentSpeedNuM(int urgentSpeedNuM) {
		this.urgentSpeedNuM = this.urgentSpeedNuM + urgentSpeedNuM;
	}

	/**
	 * @return the urgentLowdownTime
	 */
	public long getUrgentLowdownTime() {
		return urgentLowdownTime;
	}

	/**
	 * @param urgentLowdownTime the urgentLowdownTime to set
	 */
	public void setUrgentLowdownTime(long urgentLowdownTime) {
		this.urgentLowdownTime = urgentLowdownTime;
	}

	/**
	 * @return the urgentLowdownNum
	 */
	public long getUrgentLowdownNum() {
		return urgentLowdownNum;
	}

	/**
	 * @param urgentLowdownNum the urgentLowdownNum to set
	 */
	public void setUrgentLowdownNum(long urgentLowdownNum) {
		this.urgentLowdownNum = urgentLowdownNum;
	}
	
	public void addUrgentLowdownNum(int urgentLowdownNum) {
		this.urgentLowdownNum = this.urgentLowdownNum + urgentLowdownNum;
	}


	/**
	 * @return the longIdleTime
	 */
	public long getLongIdleTime() {
		return longIdleTime;
	}

	/**
	 * @param longIdleTime the longIdleTime to set
	 */
	public void setLongIdleTime(long longIdleTime) {
		this.longIdleTime = longIdleTime;
	}

	/**
	 * @return the longIdleNum
	 */
	public long getLongIdleNum() {
		return longIdleNum;
	}

	/**
	 * @param longIdleNum the longIdleNum to set
	 */
	public void setLongIdleNum(long longIdleNum) {
		this.longIdleNum = longIdleNum;
	}
	
	public void addLongIdleNum(int longIdleNum) {
		this.longIdleNum = this.longIdleNum + longIdleNum;
	}

	/**
	 * @return the idleAirConditionTime
	 */
	public long getIdleAirConditionTime() {
		return idleAirConditionTime;
	}

	/**
	 * @param idleAirConditionTime the idleAirConditionTime to set
	 */
	public void setIdleAirConditionTime(long idleAirConditionTime) {
		this.idleAirConditionTime = idleAirConditionTime;
	}

	/**
	 * @return the idleAirConditionNum
	 */
	public long getIdleAirConditionNum() {
		return idleAirConditionNum;
	}

	/**
	 * @param idleAirConditionNum the idleAirConditionNum to set
	 */
	public void setIdleAirConditionNum(long idleAirConditionNum) {
		this.idleAirConditionNum = idleAirConditionNum;
	}
	
	public void addIdleAirConditionNum(int idleAirConditionNum) {
		this.idleAirConditionNum = this.idleAirConditionNum + idleAirConditionNum;
	}

	/**
	 * @return the airConditionTime
	 */
	public long getAirConditionTime() {
		return airConditionTime;
	}

	/**
	 * @param airConditionTime the airConditionTime to set
	 */
	public void setAirConditionTime(long airConditionTime) {
		this.airConditionTime = airConditionTime;
	}

	/**
	 * @return the airConditionNum
	 */
	public long getAirConditionNum() {
		return airConditionNum;
	}

	/**
	 * @param airConditionNum the airConditionNum to set
	 */
	public void setAirConditionNum(long airConditionNum) {
		this.airConditionNum = airConditionNum;
	}

	/**
	 * @return the gearGlideTime
	 */
	public long getGearGlideTime() {
		return gearGlideTime;
	}

	/**
	 * @param gearGlideTime the gearGlideTime to set
	 */
	public void setGearGlideTime(long gearGlideTime) {
		this.gearGlideTime = gearGlideTime;
	}

	/**
	 * @return the gearGlideNum
	 */
	public long getGearGlideNum() {
		return gearGlideNum;
	}

	/**
	 * @param gearGlideNum the gearGlideNum to set
	 */
	public void setGearGlideNum(long gearGlideNum) {
		this.gearGlideNum = gearGlideNum;
	}
	
	public void addGearGlideNum(int gearGlideNum) {
		this.gearGlideNum = this.gearGlideNum + gearGlideNum;
	}


	/**
	 * @return the warmWindTime
	 */
	public long getWarmWindTime() {
		return warmWindTime;
	}

	/**
	 * @param warmWindTime the warmWindTime to set
	 */
	public void setWarmWindTime(long warmWindTime) {
		this.warmWindTime = warmWindTime;
	}

	/**
	 * @return the economicRunTime
	 */
	public long getEconomicRunTime() {
		return economicRunTime;
	}

	/**
	 * @param economicRunTime the economicRunTime to set
	 */
	public void setEconomicRunTime(long economicRunTime) {
		this.economicRunTime = economicRunTime;
	}

	/**
	 * @return the precise_oil
	 */
	public long getPrecise_oil() {
		return precise_oil;
	}

	/**
	 * @param precise_oil the precise_oil to set
	 */
	public void setPrecise_oil(long precise_oil) {
		this.precise_oil = precise_oil;
	}

	/**
	 * @return the ecuOil
	 */
	public long getEcuOil() {
		return ecuOil;
	}

	/**
	 * @param ecuOil the ecuOil to set
	 */
	public void setEcuOil(long ecuOil) {
		this.ecuOil = ecuOil;
	}

	/**
	 * @return the ecuRunningOil
	 */
	public long getEcuRunningOil() {
		return ecuRunningOil;
	}

	/**
	 * @param ecuRunningOil the ecuRunningOil to set
	 */
	public void setEcuRunningOil(long ecuRunningOil) {
		this.ecuRunningOil = ecuRunningOil;
	}

	/**
	 * @return the metRunningOil
	 */
	public long getMetRunningOil() {
		return metRunningOil;
	}

	/**
	 * @param metRunningOil the metRunningOil to set
	 */
	public void setMetRunningOil(long metRunningOil) {
		this.metRunningOil = metRunningOil;
	}

	/**
	 * @return the driverId
	 */
	public String getDriverId() {
		return driverId;
	}

	/**
	 * @param driverId the driverId to set
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}

	/**
	 * @return the driverName
	 */
	public String getDriverName() {
		return driverName;
	}

	/**
	 * @param driverName the driverName to set
	 */
	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}

	/**
	 * @return the engineTime
	 */
	public long getEngineTime() {
		return engineTime;
	}

	/**
	 * @param engineTime the engineTime to set
	 */
	public void setEngineTime(long engineTime) {
		this.engineTime = engineTime;
	}
	
	
	
	
	
}
