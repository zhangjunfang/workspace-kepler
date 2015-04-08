package com.caits.analysisserver.bean;

public class VehicleAlarmEvent {
	
	private String vid; //车辆ID
	
	private String startGpsTime = null; // 报警开始GPS时间
	
	private String alarmCode = null; // 报警编码
	
	private String alarmType = null; // 报警类型
	
	private String phoneNumber = null; //手机号
	
	private long startUtc = -1; //报警开始时间
	
	private String AREA_ID ; // 电子围栏编号

	private String mtypeCode = null; // 多媒体类型
	
	private String mediaUrl = null; // 多媒体信息
	
	private long startLat = -1; // 开始纬度（单位：十万分之一度）
	
	private long startLon = -1; // 开始经度（单位：十万分之一度）
	
	private long startMapLat = -1; // 开始地图偏移后GPS经度
	
	private long startMapLon = -1; // 开始地图偏移后GPS纬度
	
	private int startElevation = -1; // 开始海拔高度（单位：米）
	
	private int startHead = -1; // 开始方向（单位：度）
	
	private Long startGpsSpeed = -1L; // 开始速度(单位：米/小时)
	
	private long endUtc = -1; //报警结束时间
	
	private long endLat = -1; // 结束纬度（单位：十万分之一度）
	
	private long endLon = -1; // 结束经度（单位：十万分之一度）
	
	private long endMapLat = -1; // 结束地图偏移后GPS经度
	
	private long endMapLon = -1; // 结束地图偏移后GPS纬度
	
	private int endElevation = -1; // 结束海拔高度（单位：米）
	
	private int endHead = -1; // 结束方向（单位：度）
	
	private Long endGpsSpeed = -1L; // 结束速度(单位：米/小时)
	
	private Long maxSpeed = -1L; // 关键点速度(单位：米/小时)（如超速周期中的最高车速）
	
	private long accountTime = 0; //报警事件时长（单位：s）
	
	private int costOil = 0; // 报警下油耗

	private int elevationUnderAlarm = 0; // 报警下行驶里程
	
	private long mileage =0; // 报警下行驶里程
	
	private String openDoorType = null; //开门类型（1正常开门 2带速开门 3区域内开门 4区域外开门）
	
	private int alarmSrc = 0;

	public long getMileage() {
		return mileage;
	}

	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	public int getCostOil() {
		return costOil;
	}

	public void setCostOil(int costOil) {
		this.costOil = costOil;
	}

	public int getElevationUnderAlarm() {
		return elevationUnderAlarm;
	}

	public void setElevationUnderAlarm(int elevationUnderAlarm) {
		this.elevationUnderAlarm = elevationUnderAlarm;
	}

	public String getStartGpsTime() {
		return startGpsTime;
	}

	public void setStartGpsTime(String startGpsTime) {
		this.startGpsTime = startGpsTime;
	}

	public String getAREA_ID() {
		return AREA_ID;
	}

	public void setAREA_ID(String aREAID) {
		AREA_ID = aREAID;
	}

	public long getAccountTime() {
		return accountTime;
	}

	public void setAccountTime(long accountTime) {
		this.accountTime = accountTime;
	}

	public String getAlarmType() {
		return alarmType;
	}

	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}

	public long getEndUtc() {
		return endUtc;
	}

	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
	}

	public String getAlarmCode() {
		return alarmCode;
	}

	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}

	public long getStartLat() {
		return startLat;
	}

	public void setStartLat(long startLat) {
		this.startLat = startLat;
	}

	public long getStartLon() {
		return startLon;
	}

	public void setStartLon(long startLon) {
		this.startLon = startLon;
	}

	public long getStartMapLat() {
		return startMapLat;
	}

	public void setStartMapLat(long startMapLat) {
		this.startMapLat = startMapLat;
	}

	public long getStartMapLon() {
		return startMapLon;
	}

	public void setStartMapLon(long startMapLon) {
		this.startMapLon = startMapLon;
	}

	public long getEndLat() {
		return endLat;
	}

	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}

	public long getEndLon() {
		return endLon;
	}

	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}

	public long getEndMapLat() {
		return endMapLat;
	}

	public void setEndMapLat(long endMapLat) {
		this.endMapLat = endMapLat;
	}

	public long getEndMapLon() {
		return endMapLon;
	}

	public void setEndMapLon(long endMapLon) {
		this.endMapLon = endMapLon;
	}

	public int getStartElevation() {
		return startElevation;
	}

	public void setStartElevation(int startElevation) {
		this.startElevation = startElevation;
	}

	public int getStartHead() {
		return startHead;
	}

	public void setStartHead(int startHead) {
		this.startHead = startHead;
	}

	public int getEndElevation() {
		return endElevation;
	}

	public void setEndElevation(int endElevation) {
		this.endElevation = endElevation;
	}

	public int getEndHead() {
		return endHead;
	}

	public void setEndHead(int endHead) {
		this.endHead = endHead;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getPhoneNumber() {
		return phoneNumber;
	}

	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}

	public long getStartUtc() {
		return startUtc;
	}

	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}

	public String getMtypeCode() {
		return mtypeCode;
	}

	public void setMtypeCode(String mtypeCode) {
		this.mtypeCode = mtypeCode;
	}

	public String getMediaUrl() {
		return mediaUrl;
	}

	public void setMediaUrl(String mediaUrl) {
		this.mediaUrl = mediaUrl;
	}

	public Long getStartGpsSpeed() {
		return startGpsSpeed;
	}

	public void setStartGpsSpeed(Long startGpsSpeed) {
		this.startGpsSpeed = startGpsSpeed;
	}

	public Long getEndGpsSpeed() {
		return endGpsSpeed;
	}

	public void setEndGpsSpeed(Long endGpsSpeed) {
		this.endGpsSpeed = endGpsSpeed;
	}

	public Long getMaxSpeed() {
		return maxSpeed;
	}

	public void setMaxSpeed(Long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	public String getOpenDoorType() {
		return openDoorType;
	}

	public void setOpenDoorType(String openDoorType) {
		this.openDoorType = openDoorType;
	}

	public int getAlarmSrc() {
		return alarmSrc;
	}

	public void setAlarmSrc(int alarmSrc) {
		this.alarmSrc = alarmSrc;
	}
}
