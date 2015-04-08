package com.ctfo.statistics.alarm.model;


public class Alarm {
	/** 编号	*/
	private String alarmId;
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车队编号	*/
	private String teamId;
	/**	车队名称	*/
	private String teamName;
	/**	组织名称	*/
	private String entId;
	/**	车牌号	*/
	private String entName;
	/**	告警编号	*/
	private String alarmCode;
	/**	告警来源(1:终端上报; 2:平台分析)	*/
	private int alarmSource;
	/**	父类型	*/
	private String parentType;
	/**	告警时长	*/
	private int duration;
	/**	最高车速	*/
	private int maxSpeed;
	/**	平均车速	*/
	private int avgSpeed;
	/**	超速阀值	*/
	private int speedThreshold;
	/**	告警开始UTC时间	*/
	private long startUtc;
	/**	告警开始位置经度	*/
	private long startLon;
	/**	告警开始位置纬度	*/
	private long startLat;
	/**	告警结束UTC时间	*/
	private long endUtc;
	/**	告警结束位置经度	*/
	private long endLon;
	/**	告警结束位置纬度	*/
	private long endLat;
	/**	告警开始系统UTC时间	*/
	private long sysUtc;
	/**	告警总里程	*/
	private int alarmTotalMileage;
	/**	告警累计里程	*/
	private int accumulatedMileage;
	/**	最近一次告警位置里程	*/
	private int lastMileage;
	/**	车速总和	*/
	private int totalSpeed;
	/**	车速计数	*/
	private int speedIndex;
	/**	告警级别	*/
	private int alarmLevel;
	/**	驾驶员编号	*/
	private String driverId;
	/**	驾驶员信息来源（0:平台绑定； 1:驾驶员卡；2:数据库记录）	*/
	private int driverSource;
	/**	告警开始速度	*/
	private int startSpeed;
	/**	告警结束速度	*/
	private int endSpeed;
	/**
	 * 创建告警
	 * @param track 轨迹对象
	 * @param vehicleInfo	
	 */
	public Alarm(Track track, VehicleInfo vehicleInfo){ 
		this.sysUtc = System.currentTimeMillis();
		this.vid = track.getVid();
		this.plate = vehicleInfo.getPlate();
		this.teamId = vehicleInfo.getTeamId();
		this.teamName = vehicleInfo.getTeamName();
		this.entId = vehicleInfo.getEntId();
		this.entName = vehicleInfo.getEntName();
		this.speedThreshold = vehicleInfo.getSpeedThreshold();
		this.startUtc = track.getGpsUtc();
		this.startLon = track.getLon();
		this.startLat = track.getLat();
	}

	/**
	 * 获取[编号]值
	 */
	public String getAlarmId() {
		return alarmId;
	}

	/**
	 * 设置[编号] 值
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}

	/**
	 * 获取[车辆编号]值
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置[车辆编号] 值
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获取[车牌号]值
	 */
	public String getPlate() {
		return plate;
	}

	/**
	 * 设置[车牌号] 值
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}

	/**
	 * 获取[车队编号]值
	 */
	public String getTeamId() {
		return teamId;
	}

	/**
	 * 设置[车队编号] 值
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}

	/**
	 * 获取[车队名称]值
	 */
	public String getTeamName() {
		return teamName;
	}

	/**
	 * 设置[车队名称] 值
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	/**
	 * 获取[组织名称]值
	 */
	public String getEntId() {
		return entId;
	}

	/**
	 * 设置[组织名称] 值
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}

	/**
	 * 获取[车牌号]值
	 */
	public String getEntName() {
		return entName;
	}

	/**
	 * 设置[车牌号] 值
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}

	/**
	 * 获取[告警编号]值
	 */
	public String getAlarmCode() {
		return alarmCode;
	}

	/**
	 * 设置[告警编号] 值
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}

	/**
	 * 获取[告警来源] 值 (1:终端上报; 2:平台分析)
	 */
	public int getAlarmSource() {
		return alarmSource;
	}

	/**
	 * 设置[告警来源] 值(1:终端上报; 2:平台分析)
	 */
	public void setAlarmSource(int alarmSource) {
		this.alarmSource = alarmSource;
	}

	/**
	 * 获取[父类型]值
	 */
	public String getParentType() {
		return parentType;
	}

	/**
	 * 设置[父类型] 值
	 */
	public void setParentType(String parentType) {
		this.parentType = parentType;
	}

	/**
	 * 获取[告警时长]值
	 */
	public int getDuration() {
		return duration;
	}

	/**
	 * 设置[告警时长] 值
	 */
	public void setDuration(int duration) {
		this.duration = duration;
	}

	/**
	 * 获取[最高车速]值
	 */
	public int getMaxSpeed() {
		return maxSpeed;
	}

	/**
	 * 设置[最高车速] 值
	 */
	public void setMaxSpeed(int maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	/**
	 * 获取[平均车速]值
	 */
	public int getAvgSpeed() {
		return avgSpeed;
	}

	/**
	 * 设置[平均车速] 值
	 */
	public void setAvgSpeed(int avgSpeed) {
		this.avgSpeed = avgSpeed;
	}

	/**
	 * 获取[超速阀值]值
	 */
	public int getSpeedThreshold() {
		return speedThreshold;
	}

	/**
	 * 设置[超速阀值] 值
	 */
	public void setSpeedThreshold(int speedThreshold) {
		this.speedThreshold = speedThreshold;
	}

	/**
	 * 获取[告警开始UTC时间]值
	 */
	public long getStartUtc() {
		return startUtc;
	}

	/**
	 * 设置[告警开始UTC时间] 值
	 */
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}

	/**
	 * 获取[告警开始位置经度]值
	 */
	public long getStartLon() {
		return startLon;
	}

	/**
	 * 设置[告警开始位置经度] 值
	 */
	public void setStartLon(long startLon) {
		this.startLon = startLon;
	}

	/**
	 * 获取[告警开始位置纬度]值
	 */
	public long getStartLat() {
		return startLat;
	}

	/**
	 * 设置[告警开始位置纬度] 值
	 */
	public void setStartLat(long startLat) {
		this.startLat = startLat;
	}

	/**
	 * 获取[告警结束UTC时间]值
	 */
	public long getEndUtc() {
		return endUtc;
	}

	/**
	 * 设置[告警结束UTC时间] 值
	 */
	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
//		计算告警时长
		this.duration = (int) (endUtc - this.startUtc);
//		计算告警里程
	}

	/**
	 * 获取[告警结束位置经度]值
	 */
	public long getEndLon() {
		return endLon;
	}

	/**
	 * 设置[告警结束位置经度] 值
	 */
	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}

	/**
	 * 获取[告警结束位置纬度]值
	 */
	public long getEndLat() {
		return endLat;
	}

	/**
	 * 设置[告警结束位置纬度] 值
	 */
	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}

	/**
	 * 获取[告警开始系统UTC时间]值
	 */
	public long getSysUtc() {
		return sysUtc;
	}

	/**
	 * 设置[告警开始系统UTC时间] 值
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	/**
	 * 获取[告警总里程]值
	 */
	public int getAlarmTotalMileage() {
		return alarmTotalMileage;
	}
	/**
	 * 设置[告警总里程] 值
	 */
	public void setAlarmTotalMileage(int alarmMileage) {
		this.alarmTotalMileage = alarmMileage;
	}

	/**
	 * 获取[最近一次告警位置里程]值
	 */
	public int getLastMileage() {
		return lastMileage;
	}

	/**
	 * 设置[最近一次告警位置里程] 值
	 */
	public void setLastMileage(int lastMileage) {
		this.lastMileage = lastMileage;
	}

	/**
	 * 获取[车速总和]值
	 */
	public int getTotalSpeed() {
		return totalSpeed;
	}

	/**
	 * 设置[车速总和] 值
	 */
	public void setTotalSpeed(int currentSpeed) {
		this.totalSpeed += currentSpeed;
		this.speedIndex++;
	}

	/**
	 * 获取[车速计数]值
	 */
	public int getSpeedIndex() {
		return speedIndex;
	}

	/**
	 * 设置[车速计数] 值
	 */
	public void setSpeedIndex(int speedIndex) {
		this.speedIndex = speedIndex;
	}

	/**
	 * 获取[告警累计里程]值
	 */
	public int getAccumulatedMileage() {
		return accumulatedMileage;
	}

	/**
	 * 设置[告警累计里程] 值
	 */
	public void setAccumulatedMileage(int currentMileage) {
		this.accumulatedMileage += currentMileage;
	}
	/**
	 * 获取[告警累计里程]值
	 */
	public int getAvgSpeedByPlatformSet() {
		if(this.speedIndex > 1){
			return this.totalSpeed / this.speedIndex;
		}
		return this.totalSpeed;
	}

	/**
	 * 获取[告警级别]值
	 */
	public int getAlarmLevel() {
		return alarmLevel;
	}

	/**
	 * 设置[告警级别] 值
	 */
	public void setAlarmLevel(int alarmLevel) {
		this.alarmLevel = alarmLevel;
	}

	/**
	 * 获取[驾驶员编号]值
	 */
	public String getDriverId() {
		return driverId;
	}

	/**
	 * 设置[驾驶员编号] 值
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}

	/**
	 * 获取[驾驶员信息来源（0:平台绑定；1:驾驶员卡；2:数据库记录）]值
	 */
	public int getDriverSource() {
		return driverSource;
	}

	/**
	 * 设置[驾驶员信息来源（0:平台绑定；1:驾驶员卡；2:数据库记录）] 值
	 */
	public void setDriverSource(int driverSource) {
		this.driverSource = driverSource;
	}
	/**
	 * 获取[告警开始速度]值
	 */
	public int getStartSpeed() {
		return startSpeed;
	}
	/**
	 * 设置[告警开始速度] 值
	 */
	public void setStartSpeed(int startSpeed) {
		this.startSpeed = startSpeed;
	}
	/**
	 * 获取[告警结束速度]值
	 */
	public int getEndSpeed() {
		return endSpeed;
	}
	/**
	 * 设置[告警结束速度] 值
	 */
	public void setEndSpeed(int endSpeed) {
		this.endSpeed = endSpeed;
	}
	
}
