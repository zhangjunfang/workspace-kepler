package com.ctfo.trackservice.model;


/**
 * 文件名：DriverDetailBean.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-10-14下午6:40:40
 * 
 */
public class DriverDetailBean {
	private long id; // 一个车辆文件有多个驾驶员信息，记录该bean属于哪个文件，判断线程是否该结束
	private String detailId;
	
	private long statDate;
	
	private String vid;
	private VehicleMessageBean beginVmb;//驾驶起始点轨迹对象缓存
	private VehicleMessageBean endVmb;//驾驶结束点轨迹对象缓存
	
	private long   engineRotateTime;//发动机工作时长
	private long   runningTime;//行车时长
	private long   mileage;
	private long   runningOil;//行车总油耗
	private long   ecuOilWear;//ECU总油耗
	private long   ecuRunningOilWear;//ecu行车油耗
	private long   ecuIdlingOilwear;//ecu怠速油耗
	private long   metOilWear;//met总油耗
	private long   metRunningOilWear;//met行车油耗
	private long   metIdlingOilWear;//met怠速油耗
	private long   accCloseNum;//acc关次数
	private long   accCloseTime;//acc关时长
	
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
	
	private long heatUpTime = 0l; //加热器总时长
	
	private long heatUpNum = 0l;//加热器总次数
	
	private long gearGlideTime = 0l;//空挡滑行
	
	private long gearGlideNum = 0l;
	
	private long warmWindTime = 0l;//暖风
	
	private long economicRunTime = 0l;//超经济区运行时长（应用上超经济区运行比例 = round（economicRunTime/engineRotateTime * 100,2））
	

	
	public String getDetailId() {
		return detailId;
	}
	public void setDetailId(String detailId) {
		this.detailId = detailId;
	}
	public long getStatDate() {
		return statDate;
	}
	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public VehicleMessageBean getBeginVmb() {
		return beginVmb;
	}
	public void setBeginVmb(VehicleMessageBean beginVmb) {
		this.beginVmb = beginVmb;
	}
	public VehicleMessageBean getEndVmb() {
		return endVmb;
	}
	public void setEndVmb(VehicleMessageBean endVmb) {
		this.endVmb = endVmb;
	}
	public long getEngineRotateTime() {
		return engineRotateTime;
	}
	public void setEngineRotateTime(long engineRotateTime) {
		this.engineRotateTime = engineRotateTime;
	}
	public long getRunningTime() {
		return runningTime;
	}
	public void setRunningTime(long runningTime) {
		this.runningTime = runningTime;
	}
	public long getRunningOil() {
		return runningOil;
	}
	public void setRunningOil(long runningOil) {
		this.runningOil = runningOil;
	}
	public long getEcuOilWear() {
		return ecuOilWear;
	}
	public void setEcuOilWear(long ecuOilWear) {
		this.ecuOilWear = ecuOilWear;
	}
	public long getEcuRunningOilWear() {
		return ecuRunningOilWear;
	}
	public void setEcuRunningOilWear(long ecuRunningOilWear) {
		this.ecuRunningOilWear = ecuRunningOilWear;
	}
	public long getEcuIdlingOilwear() {
		return ecuIdlingOilwear;
	}
	public void setEcuIdlingOilwear(long ecuIdlingOilwear) {
		this.ecuIdlingOilwear = ecuIdlingOilwear;
	}
	public long getMetOilWear() {
		return metOilWear;
	}
	public void setMetOilWear(long metOilWear) {
		this.metOilWear = metOilWear;
	}
	public long getMetRunningOilWear() {
		return metRunningOilWear;
	}
	public void setMetRunningOilWear(long metRunningOilWear) {
		this.metRunningOilWear = metRunningOilWear;
	}
	public long getMetIdlingOilWear() {
		return metIdlingOilWear;
	}
	public void setMetIdlingOilWear(long metIdlingOilWear) {
		this.metIdlingOilWear = metIdlingOilWear;
	}
	public long getAccCloseNum() {
		return accCloseNum;
	}
	public void setAccCloseNum(long accCloseNum) {
		this.accCloseNum = accCloseNum;
	}
	public long getAccCloseTime() {
		return accCloseTime;
	}
	public void setAccCloseTime(long accCloseTime) {
		this.accCloseTime = accCloseTime;
	}

	public long getGearGlideNum() {
		return gearGlideNum;
	}
	public void addGearGlideNum(long gearGlideNum) {
		this.gearGlideNum = this.gearGlideNum + gearGlideNum;
	}
	public long getGearGlideTime() {
		return gearGlideTime;
	}
	public void addGearGlideTime(long gearGlideTime) {
		this.gearGlideTime = this.gearGlideTime + gearGlideTime;
	}

	
	public long getUrgentLowdownNum() {
		return urgentLowdownNum;
	}
	public void addUrgentLowdownNum(long urgentLowdownNum) {
		this.urgentLowdownNum = this.urgentLowdownNum + urgentLowdownNum;
	}
	public long getUrgentLowdownTime() {
		return urgentLowdownTime;
	}
	public void addUrgentLowdownTime(long urgentLowdownTime) {
		this.urgentLowdownTime = this.urgentLowdownTime + urgentLowdownTime;
	}
	public long getLongIdleNum() {
		return longIdleNum;
	}
	public void addLongIdleNum(long longIdleNum) {
		this.longIdleNum = this.longIdleNum + longIdleNum;
	}
	public long getLongIdleTime() {
		return longIdleTime;
	}
	public void addLongIdleTime(long longIdleTime) {
		this.longIdleTime = this.longIdleTime + longIdleTime;
	}
	public long getEconomicRunTime() {
		return economicRunTime;
	}
	public void setEconomicRunTime(long economicRunTime) {
		this.economicRunTime =  economicRunTime;
	}

	public long getMileage(){
		return mileage;
	}
	public void addMileage(long mileage){
		this.mileage += mileage;
	}
	
	public void addEcuOilWear(long ecuoil){
		this.ecuOilWear += ecuoil;
	}
	
	public void addMetOilWear(long metoil){
		this.metOilWear += metoil;
	}
	
	public void addEngineTime(long time){
		this.engineRotateTime += time;
	}
	
	public void addAccNum(int num){
		this.accCloseNum += num;
	}
	
	public void addAccTime(long time){
		this.accCloseTime += time;
	}
	
	public void addRunningTime(long time){
		this.runningTime += time;
	}
	
	public void addEcuRunningOilWear(long oil){
		this.ecuRunningOilWear += oil;
	}
	
	public void addMetRunningOilWear(long oil){
		this.metRunningOilWear += oil;
	}
	
	/**
	 * @return the id
	 */
	public long getId() {
		return id;
	}
	/**
	 * @param id the id to set
	 */
	public void setId(long id) {
		this.id = id;
	}
	/**
	 * @return the overSpeedTime
	 */
	public void addOverSpeedTime(long time) {		
		this.overSpeedTime = this.overSpeedTime + time;
	}
	/**
	 * @param overSpeedTime the overSpeedTime to set
	 */
	public long getOverSpeedTime() {
		return overSpeedTime;
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
	public void addOverSpeedNum(long overSpeedNum) {
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
	public void addOverRpmTime(long overRpmTime) {
		this.overRpmTime = this.overRpmTime + overRpmTime;
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
	public void addOverRpmNum(long overRpmNum) {
		this.overRpmNum = this.overRpmNum + overRpmNum;
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
	public void addUrgentSpeedNuM(long urgentSpeedNuM) {
		this.urgentSpeedNuM = this.urgentSpeedNuM + urgentSpeedNuM;
	}
	
	public long getUrgentSpeedTime() {
		return urgentSpeedTime;
	}
	public void addUrgentSpeedTime(long urgentSpeedTime) {
		this.urgentSpeedTime = this.urgentSpeedTime + urgentSpeedTime;
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
	public void addIdleAirConditionTime(long idleAirConditionTime) {
		this.idleAirConditionTime = this.idleAirConditionTime + idleAirConditionTime;
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
	public void addIdleAirConditionNum(long idleAirConditionNum) {
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
	public void addAirConditionTime(long airConditionTime) {
		this.airConditionTime = this.airConditionTime + airConditionTime;
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
	public void addAirConditionNum(long airConditionNum) {
		this.airConditionNum = this.airConditionNum + airConditionNum;
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
	public void addWarmWindTime(long warmWindTime) {
		this.warmWindTime = this.warmWindTime + warmWindTime;
	}
	/**
	 * @return the heatUpTime
	 */
	public long getHeatUpTime() {
		return heatUpTime;
	}
	/**
	 * @param heatUpTime the heatUpTime to set
	 */
	public void addHeatUpTime(long heatUpTime) {
		this.heatUpTime = this.heatUpTime + heatUpTime;
	}
	/**
	 * @return the heatUpNum
	 */
	public long getHeatUpNum() {
		return heatUpNum;
	}
	/**
	 * @param heatUpNum the heatUpNum to set
	 */
	public void addHeatUpNum(long heatUpNum) {
		this.heatUpNum = this.heatUpNum + heatUpNum;
	}

	
	
	
	
}
