package com.ctfo.statistics.alarm.model;

public class Track implements Comparable<Track>{
	/**	车辆编号	*/
	private String vid;
	/**	偏移经度	*/
	private long mapLon;
	/**	偏移纬度	*/
	private long mapLat;
	/**	GPS时间	*/
	private String gpsTime;
	/**	GPS UTC时间	*/
	private long gpsUtc;
	/**	GPS 时分秒	*/
	private int gpsDateUtc;
	/**	GPS速度	*/
	private int gpsSpeed;
	/**	方向(度) 	5	*/
	private int direction; 
	/**	基本状态(经度、纬度异常:1; 速度异常:2; 时间异常:4; 方向异常:8;  )	*/
	private int status = 0;
	/**	报警编码	*/
	private String alarmCode;
	/**	告警来源(1:终端上报; 2:平台分析)	*/
	private int alarmSource;
	/**	原始经度	*/
	private long lon;
	/**	原始纬度	*/
	private long lat;
	/**	海拔(米)		6	*/
	private int elevation; 
	/**	里程，1/10km	 	9	*/
	private int mileage;
	/**	累计油耗	*/
	private long cumulativeFuel;
	/**	发动机运行总时长	*/
	private long engineRunTotal;
	/**	发动机转速	*/
	private int engineSpeed;
	/**	基本信息状态位	*/
	private String baseStatus;
	/**	区域/线路报警附加信息 (32)	*/
	private String alarmAdded;
	/**	冷却液温度	509	*/
	private long eWaterTemp;
	/**	蓄电池电压	507	*/
	private long batteryVoltage;
	/**	瞬时油耗	*/
	private long oilInstant;
	/**	行驶记录仪速度(km/h)	*/
	private int vssSpeed; 
	/**	机油压力	*/
	private long oilPressure;
	/**	大气压力	*/
	private long atmosphericPressure;
	/**	发动机扭矩百分比，1bit=1%，0=-125%	503	*/
	private long engineTorque;
	/**	车辆信号状态  */
	private String signalStatus;
	/**	速度来源( 0:来自VSS ;1:来自GPS)  */
	private int speedSource; 
	/**	油量（单位：L）（对应仪表盘读数）		*/
	private long oilMeasure; 
	/**	超速告警附加信息	*/
	private String overspeedAlarmAdded;
	/**	路线行驶时间不足/过长	*/
	private String lineError;
	/**	油门踏板位置(1bit=0.4%，0=0%)	*/
	private long throttlePedalPosition;
	/**	终端内置电池电压 	*/
	private long builtBatteryVoltage;
	/**	发动机水温	*/
	private long engineWaterTemperature;
	/**	机油温度	*/
	private long oilTemperature;
	/**	进气温度	*/
	private long inletTemperature;
	/**	开门状态	*/
	private String openState;
	/**	需要人工确认报警事件的ID	*/
	private String manualAlarmId;
	/**	计量仪油耗，1bit=0.01L,0=0L	*/
	private long preciseFuel;
	/**	驾驶员编号	*/
	private String driverId;
	/**	驾驶员信息来源（0:平台绑定； 1:驾驶员卡；2:数据库记录）	*/
	private int driverSource;
	/**	系统时间	*/
	private long sysUtc;
	/**	限速阀值	*/
	private int speedLimit;
	/**	平台超速阀值	*/
	private int platformSpeedThreshold;
	
	/**
	 * @return the 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * @param 车辆编号 the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return the 偏移经度
	 */
	public long getMapLon() {
		return mapLon;
	}
	/**
	 * @param 偏移经度 the mapLon to set
	 */
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}
	/**
	 * @return the 偏移纬度
	 */
	public long getMapLat() {
		return mapLat;
	}
	/**
	 * @param 偏移纬度 the mapLat to set
	 */
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}
	/**
	 * @return the GPS时间
	 */
	public String getGpsTime() {
		return gpsTime;
	}
	/**
	 * @param GPS时间 the gpsTime to set
	 */
	public void setGpsTime(String gpsTime) {
		this.gpsTime = gpsTime;
	}
	/**
	 * @return the GPSUTC时间
	 */
	public long getGpsUtc() {
		return gpsUtc;
	}
	/**
	 * @param GPSUTC时间 the gpsUtc to set
	 */
	public void setGpsUtc(long gpsUtc) {
		this.gpsUtc = gpsUtc;
	}
	/**
	 * @return the GPS时分秒
	 */
	public int getGpsDateUtc() {
		return gpsDateUtc;
	}
	/**
	 * @param GPS时分秒 the gpsDateUtc to set
	 */
	public void setGpsDateUtc(int gpsDateUtc) {
		this.gpsDateUtc = gpsDateUtc;
	}
	/**
	 * @return the GPS速度
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}
	/**
	 * @param GPS速度 the gpsSpeed to set
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}
	/**
	 * @return the 方向(度)5
	 */
	public int getDirection() {
		return direction;
	}
	/**
	 * @param 方向(度)5 the direction to set
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	/**
	 * @return the 基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;)
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * @param 基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;) the status to set
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * @return the 报警编码
	 */
	public String getAlarmCode() {
		return alarmCode;
	}
	/**
	 * @param 报警编码 the alarmCode to set
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	/**
	 * @return the 原始经度
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * @param 原始经度 the lon to set
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * @return the 原始纬度
	 */
	public long getLat() {
		return lat;
	}
	/**
	 * @param 原始纬度 the lat to set
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}
	/**
	 * @return the 海拔(米)6
	 */
	public int getElevation() {
		return elevation;
	}
	/**
	 * @param 海拔(米)6 the elevation to set
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}
	/**
	 * @return the 里程，110km9
	 */
	public int getMileage() {
		return mileage;
	}
	/**
	 * @param 里程，110km9 the mileage to set
	 */
	public void setMileage(int mileage) {
		this.mileage = mileage;
	}
	/**
	 * @return the 累计油耗
	 */
	public long getCumulativeFuel() {
		return cumulativeFuel;
	}
	/**
	 * @param 累计油耗 the cumulativeFuel to set
	 */
	public void setCumulativeFuel(long cumulativeFuel) {
		this.cumulativeFuel = cumulativeFuel;
	}
	/**
	 * @return the 发动机运行总时长
	 */
	public long getEngineRunTotal() {
		return engineRunTotal;
	}
	/**
	 * @param 发动机运行总时长 the engineRunTotal to set
	 */
	public void setEngineRunTotal(long engineRunTotal) {
		this.engineRunTotal = engineRunTotal;
	}
	/**
	 * @return the 发动机转速
	 */
	public int getEngineSpeed() {
		return engineSpeed;
	}
	/**
	 * @param 发动机转速 the engineSpeed to set
	 */
	public void setEngineSpeed(int engineSpeed) {
		this.engineSpeed = engineSpeed;
	}
	/**
	 * @return the 基本信息状态位
	 */
	public String getBaseStatus() {
		return baseStatus;
	}
	/**
	 * @param 基本信息状态位 the baseStatus to set
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}
	/**
	 * @return the 区域线路报警附加信息(32)
	 */
	public String getAlarmAdded() {
		return alarmAdded;
	}
	/**
	 * @param 区域线路报警附加信息(32) the alarmAdded to set
	 */
	public void setAlarmAdded(String alarmAdded) {
		this.alarmAdded = alarmAdded;
	}
	/**
	 * @return the 冷却液温度509
	 */
	public long geteWaterTemp() {
		return eWaterTemp;
	}
	/**
	 * @param 冷却液温度509 the eWaterTemp to set
	 */
	public void seteWaterTemp(long eWaterTemp) {
		this.eWaterTemp = eWaterTemp;
	}
	/**
	 * @return the 蓄电池电压507
	 */
	public long getBatteryVoltage() {
		return batteryVoltage;
	}
	/**
	 * @param 蓄电池电压507 the batteryVoltage to set
	 */
	public void setBatteryVoltage(long batteryVoltage) {
		this.batteryVoltage = batteryVoltage;
	}
	/**
	 * @return the 瞬时油耗
	 */
	public long getOilInstant() {
		return oilInstant;
	}
	/**
	 * @param 瞬时油耗 the oilInstant to set
	 */
	public void setOilInstant(long oilInstant) {
		this.oilInstant = oilInstant;
	}
	/**
	 * @return the 行驶记录仪速度(kmh)
	 */
	public int getVssSpeed() {
		return vssSpeed;
	}
	/**
	 * @param 行驶记录仪速度(kmh) the vssSpeed to set
	 */
	public void setVssSpeed(int vssSpeed) {
		this.vssSpeed = vssSpeed;
	}
	/**
	 * @return the 机油压力
	 */
	public long getOilPressure() {
		return oilPressure;
	}
	/**
	 * @param 机油压力 the oilPressure to set
	 */
	public void setOilPressure(long oilPressure) {
		this.oilPressure = oilPressure;
	}
	/**
	 * @return the 大气压力
	 */
	public long getAtmosphericPressure() {
		return atmosphericPressure;
	}
	/**
	 * @param 大气压力 the atmosphericPressure to set
	 */
	public void setAtmosphericPressure(long atmosphericPressure) {
		this.atmosphericPressure = atmosphericPressure;
	}
	/**
	 * @return the 发动机扭矩百分比，1bit=1%，0=-125%503
	 */
	public long getEngineTorque() {
		return engineTorque;
	}
	/**
	 * @param 发动机扭矩百分比，1bit=1%，0=-125%503 the engineTorque to set
	 */
	public void setEngineTorque(long engineTorque) {
		this.engineTorque = engineTorque;
	}
	/**
	 * @return the 车辆信号状态
	 */
	public String getSignalStatus() {
		return signalStatus;
	}
	/**
	 * @param 车辆信号状态 the signalStatus to set
	 */
	public void setSignalStatus(String signalStatus) {
		this.signalStatus = signalStatus;
	}
	/**
	 * @return the 速度来源(0:来自VSS;1:来自GPS)
	 */
	public int getSpeedSource() {
		return speedSource;
	}
	/**
	 * @param 速度来源(0:来自VSS;1:来自GPS) the speedSource to set
	 */
	public void setSpeedSource(int speedSource) {
		this.speedSource = speedSource;
	}
	/**
	 * @return the 油量（单位：L）（对应仪表盘读数）
	 */
	public long getOilMeasure() {
		return oilMeasure;
	}
	/**
	 * @param 油量（单位：L）（对应仪表盘读数） the oilMeasure to set
	 */
	public void setOilMeasure(long oilMeasure) {
		this.oilMeasure = oilMeasure;
	}
	/**
	 * @return the 超速告警附加信息
	 */
	public String getOverspeedAlarmAdded() {
		return overspeedAlarmAdded;
	}
	/**
	 * @param 超速告警附加信息 the overspeedAlarmAdded to set
	 */
	public void setOverspeedAlarmAdded(String overspeedAlarmAdded) {
		this.overspeedAlarmAdded = overspeedAlarmAdded;
	}
	/**
	 * @return the 路线行驶时间不足过长
	 */
	public String getLineError() {
		return lineError;
	}
	/**
	 * @param 路线行驶时间不足过长 the lineError to set
	 */
	public void setLineError(String lineError) {
		this.lineError = lineError;
	}
	/**
	 * @return the 油门踏板位置(1bit=0.4%，0=0%)
	 */
	public long getThrottlePedalPosition() {
		return throttlePedalPosition;
	}
	/**
	 * @param 油门踏板位置(1bit=0.4%，0=0%) the throttlePedalPosition to set
	 */
	public void setThrottlePedalPosition(long throttlePedalPosition) {
		this.throttlePedalPosition = throttlePedalPosition;
	}
	/**
	 * @return the 终端内置电池电压
	 */
	public long getBuiltBatteryVoltage() {
		return builtBatteryVoltage;
	}
	/**
	 * @param 终端内置电池电压 the builtBatteryVoltage to set
	 */
	public void setBuiltBatteryVoltage(long builtBatteryVoltage) {
		this.builtBatteryVoltage = builtBatteryVoltage;
	}
	/**
	 * @return the 发动机水温
	 */
	public long getEngineWaterTemperature() {
		return engineWaterTemperature;
	}
	/**
	 * @param 发动机水温 the engineWaterTemperature to set
	 */
	public void setEngineWaterTemperature(long engineWaterTemperature) {
		this.engineWaterTemperature = engineWaterTemperature;
	}
	/**
	 * @return the 机油温度
	 */
	public long getOilTemperature() {
		return oilTemperature;
	}
	/**
	 * @param 机油温度 the oilTemperature to set
	 */
	public void setOilTemperature(long oilTemperature) {
		this.oilTemperature = oilTemperature;
	}
	/**
	 * @return the 进气温度
	 */
	public long getInletTemperature() {
		return inletTemperature;
	}
	/**
	 * @param 进气温度 the inletTemperature to set
	 */
	public void setInletTemperature(long inletTemperature) {
		this.inletTemperature = inletTemperature;
	}
	/**
	 * @return the 开门状态
	 */
	public String getOpenState() {
		return openState;
	}
	/**
	 * @param 开门状态 the openState to set
	 */
	public void setOpenState(String openState) {
		this.openState = openState;
	}
	/**
	 * @return the 需要人工确认报警事件的ID
	 */
	public String getManualAlarmId() {
		return manualAlarmId;
	}
	/**
	 * @param 需要人工确认报警事件的ID the manualAlarmId to set
	 */
	public void setManualAlarmId(String manualAlarmId) {
		this.manualAlarmId = manualAlarmId;
	}
	/**
	 * @return the 计量仪油耗，1bit=0.01L0=0L
	 */
	public long getPreciseFuel() {
		return preciseFuel;
	}
	/**
	 * @param 计量仪油耗，1bit=0.01L0=0L the preciseFuel to set
	 */
	public void setPreciseFuel(long preciseFuel) {
		this.preciseFuel = preciseFuel;
	}

	/**
	 * @return the 驾驶员编号
	 */
	public String getDriverId() {
		return driverId;
	}
	/**
	 * @param 驾驶员编号 the driverId to set
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}
	/**
	 * @return the 驾驶员信息来源（0:平台绑定；1:驾驶员卡）
	 */
	public int getDriverSource() {
		return driverSource;
	}
	/**
	 * @param 驾驶员信息来源（0:平台绑定；1:驾驶员卡） the driverSource to set
	 */
	public void setDriverSource(int driverSource) {
		this.driverSource = driverSource;
	}
	/**
	 * @return the 系统时间
	 */
	public long getSysUtc() {
		return sysUtc;
	}
	/**
	 * @param 系统时间 the sysUtc to set
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	
	@Override
	public int compareTo(Track track) { 
		return this.gpsDateUtc - track.gpsDateUtc;
	}
	/**
	 * 获取[告警来源(1:终端上报;2:平台分析)]值
	 */
	public int getAlarmSource() {
		return alarmSource;
	}
	/**
	 * 设置[告警来源(1:终端上报;2:平台分析)] 值
	 */
	public void setAlarmSource(int alarmSource) {
		this.alarmSource = alarmSource;
	}
	/**
	 * 获取[限速阀值]值
	 */
	public int getSpeedLimit() {
		return speedLimit;
	}
	/**
	 * 设置[限速阀值] 值
	 */
	public void setSpeedLimit(int speedLimit) {
		this.speedLimit = speedLimit;
	}
	/**
	 * 获取[平台超速阀值]值
	 */
	public int getPlatformSpeedThreshold() {
		return platformSpeedThreshold;
	}
	/**
	 * 设置[平台超速阀值] 值
	 */
	public void setPlatformSpeedThreshold(int platformSpeedThreshold) {
		this.platformSpeedThreshold = platformSpeedThreshold;
	}
	
}
