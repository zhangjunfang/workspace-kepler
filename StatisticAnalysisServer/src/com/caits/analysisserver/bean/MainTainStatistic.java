package com.caits.analysisserver.bean;

public class MainTainStatistic {
	// vid
	String vid ;
	// VEHICLE_NO
	String vehicleNo = "";
	// 0:循环执行 1:单次执行
	String exeFrequency = "";
	// 第几维保
	int mainTainTimes = 0;
	// 当前行驶里程
	double currentMileage = 0;
	// 维保间隔里程
	double intervalMileage = 0;
	// 提醒里程
	double warnMileage = 0;
	// 维保间隔天数
	int intervalDays = 0;
	// 提醒天数
	int warnDays = 0;
	// 执行具体时间
	String exeTime = "";
	// 本次维护结束时间
	String mainTainDate = "";
	// 本次维护结束时里程
	double mainTainMileage = 0;
	//所属类别ID
	String maintainId = "";
	// planId
	String planId ;
	// 维保计划编号
	String planCode = "";
	// 维护状态 (0:未维护 1:已维护)
	String mainTainStat = "";
	// 维保类别名称
	String mainTainName = "";
	//维保状态，新增加，用于标示是否按时维护
	String maintainOntimeStat = "";
	//计划维保时间
	String planMaintainDate = "";
	//计划维保里程
	String planMaintainMileage = "";
	//按里程提醒信息
	String warnMileageMessage = "";
	//按时间提醒信息
	String warnTimeMessage = "";
	
	public String getWarnMileageMessage() {
		return warnMileageMessage;
	}
	public void setWarnMileageMessage(String warnMileageMessage) {
		this.warnMileageMessage = warnMileageMessage;
	}
	public String getWarnTimeMessage() {
		return warnTimeMessage;
	}
	public void setWarnTimeMessage(String warnTimeMessage) {
		this.warnTimeMessage = warnTimeMessage;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getVehicleNo() {
		return vehicleNo;
	}
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}
	public String getExeFrequency() {
		return exeFrequency;
	}
	public void setExeFrequency(String exeFrequency) {
		this.exeFrequency = exeFrequency;
	}
	public int getMainTainTimes() {
		return mainTainTimes;
	}
	public void setMainTainTimes(int mainTainTimes) {
		this.mainTainTimes = mainTainTimes;
	}
	public double getCurrentMileage() {
		return currentMileage;
	}
	public void setCurrentMileage(double currentMileage) {
		this.currentMileage = currentMileage;
	}
	public double getIntervalMileage() {
		return intervalMileage;
	}
	public void setIntervalMileage(double intervalMileage) {
		this.intervalMileage = intervalMileage;
	}
	public double getWarnMileage() {
		return warnMileage;
	}
	public void setWarnMileage(double warnMileage) {
		this.warnMileage = warnMileage;
	}
	public int getIntervalDays() {
		return intervalDays;
	}
	public void setIntervalDays(int intervalDays) {
		this.intervalDays = intervalDays;
	}
	public int getWarnDays() {
		return warnDays;
	}
	public void setWarnDays(int warnDays) {
		this.warnDays = warnDays;
	}
	public String getExeTime() {
		return exeTime;
	}
	public void setExeTime(String exeTime) {
		this.exeTime = exeTime;
	}
	public String getMainTainDate() {
		return mainTainDate;
	}
	public void setMainTainDate(String mainTainDate) {
		this.mainTainDate = mainTainDate;
	}
	public double getMainTainMileage() {
		return mainTainMileage;
	}
	public void setMainTainMileage(double mainTainMileage) {
		this.mainTainMileage = mainTainMileage;
	}
	public String getMaintainId() {
		return maintainId;
	}
	public void setMaintainId(String maintainId) {
		this.maintainId = maintainId;
	}
	public String getPlanId() {
		return planId;
	}
	public void setPlanId(String planId) {
		this.planId = planId;
	}
	public String getPlanCode() {
		return planCode;
	}
	public void setPlanCode(String planCode) {
		this.planCode = planCode;
	}
	public String getMainTainStat() {
		return mainTainStat;
	}
	public void setMainTainStat(String mainTainStat) {
		this.mainTainStat = mainTainStat;
	}
	public String getMainTainName() {
		return mainTainName;
	}
	public void setMainTainName(String mainTainName) {
		this.mainTainName = mainTainName;
	}
	public String getMaintainOntimeStat() {
		return maintainOntimeStat;
	}
	public void setMaintainOntimeStat(String maintainOntimeStat) {
		this.maintainOntimeStat = maintainOntimeStat;
	}
	public String getPlanMaintainDate() {
		return planMaintainDate;
	}
	public void setPlanMaintainDate(String planMaintainDate) {
		this.planMaintainDate = planMaintainDate;
	}
	public String getPlanMaintainMileage() {
		return planMaintainMileage;
	}
	public void setPlanMaintainMileage(String planMaintainMileage) {
		this.planMaintainMileage = planMaintainMileage;
	}

}
