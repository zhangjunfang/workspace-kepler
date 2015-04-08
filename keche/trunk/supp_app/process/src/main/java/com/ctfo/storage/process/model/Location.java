/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * @author zjhl
 *
 */
public class Location {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌号	颜色	*/
	private String plateColor;
	/**	车架号	*/
	private String vinCode;
	/**	车辆内部编码	*/
	private String innerCode;
	/**	终端号	*/
	private String tid;
	/**	终端型号	*/
	private String terminalType;
	/**	手机号	*/
	private String phoneNumber;
	/**	厂商编号	*/
	private String oemCode;
	/**	组织编号	*/
	private String entId;
	/**	组织名称	*/
	private String entName;
	/**	车队编号	*/
	private String teamId;
	/**	车队名称	*/
	private String teamName;
	/** 驾驶员ID*/
	private String staffId ;
	/** 驾驶员名称*/
	private String staffName ;
	/**	在线状态	*/
	private int online;
	/**	报警标志位	*/
	private String alarmFlag;
	/**	状态位	*/
	private String statusFlag;
	/**	状态	*/
	private int status;
	/**	经度	*/
	private int lon;
	/**	纬度	*/
	private int lat;
	/**	偏移经度	*/
	private long maplon;
	/**	偏移纬度	*/
	private long maplat;
	/**	海拔（单位：米）	*/
	private int elevation;
	/**	方向（0-359，正北为0，顺时针 单位：度）	*/
	private int direction;
	/**	速度（1/10km/h 单位：米/小时）	*/
	private int speed;
	/**	GPS速度（1/10km/h 单位：米/小时）	*/
	private int gpsSpeed;
	/**	BCD时间（格式:yyMMddHHmmss）	*/
	private String bcdTime;
	/**	UTC时间	*/
	private long utcTime;
	/**	里程	*/
	private long mileage;
	/**	油量	*/
	private int oil;
	/**	行驶记录仪速度	*/
	private int vssSpeed;
	/**	人工确认报警ID	*/
	private int acknowledgeAlarmId;
	/**	超速报警附加信息	*/
	private String  overspeedAlarmAdded;
	/**	进出区域/路线报警附加信息	*/
	private String areaAndLineAlarmAdded;
	/**	路段行驶时间不足/过长报警附加信息	*/
	private String roadTimeAlarmAdded;
	/**	发动机转速	*/
	private int engineRpm;
	/**	瞬时油耗	*/
	private int instantFuel;
	/**	发动机扭矩百分比	*/
	private long engineTorque;
	/**	油门踏板位置	*/
	private int throttlePedalPosition;
	/**	扩展车辆报警标志 	*/
	private String expandAlarmFlag;
	/**	扩展车辆信号状态	*/
	private String expandStatusFlag;
	/**	累计油耗	*/
	private long cumulativeFuel;
	/**	开门报警附加信息	*/
	private int doorAlarmAdded;
	/**	速度来源标识	*/
	private int speedSource;
	/**	计量仪油耗	*/
	private long meterOil;
	/**	IO 状态位	*/
	private int ioStatusFlag;
	/**	模拟量	*/
	private long analog;
	/**	无线通信网络信号强度	*/
	private int signalStrength;
	/**	GNSS 定位卫星数	*/
	private int satellites;
	/**	发动机运行总时长	*/
	private int engineRunTotal;
	/**	终端内置电池电压	*/
	private int terminalBatteryVoltage;
	/**	蓄电池电压	*/
	private int batteryVoltage ;
	/**	机油温度	*/
	private int oilTemperature;
	/**	发动机冷却液温度	*/
	private int coolantTemperature;
	/**	进气温度	*/
	private int inletTemperature;
	/**	机油压力	*/
	private int oilPressure;
	/**	大气压力	*/
	private int atmosphericPressure;
	/**	公共自定义信息内容	*/
	private String customContent;

	/**
	 * @return 获取 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return 获取 车架号
	 */
	public String getVinCode() {
		return vinCode;
	}
	/**
	 * 设置车架号
	 * @param vinCode 车架号 
	 */
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	/**
	 * @return 获取 手机号
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号
	 * @param phoneNumber 手机号 
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	/**
	 * @return 获取 报警标志位
	 */
	public String getAlarmFlag() {
		return alarmFlag;
	}
	/**
	 * 设置报警标志位
	 * @param alarmFlag 报警标志位 
	 */
	public void setAlarmFlag(String alarmFlag) {
		this.alarmFlag = alarmFlag;
	}
	/**
	 * @return 获取 状态位
	 */
	public String getStatusFlag() {
		return statusFlag;
	}
	/**
	 * 设置状态位
	 * @param statusFlag 状态位 
	 */
	public void setStatusFlag(String statusFlag) {
		this.statusFlag = statusFlag;
	}
	/**
	 * @return 获取 经度
	 */
	public int getLon() {
		return lon;
	}
	/**
	 * 设置经度
	 * @param lon 经度 
	 */
	public void setLon(int lon) {
		this.lon = lon;
	}
	/**
	 * @return 获取 纬度
	 */
	public int getLat() {
		return lat;
	}
	/**
	 * 设置纬度
	 * @param lat 纬度 
	 */
	public void setLat(int lat) {
		this.lat = lat;
	}
	/**
	 * @return 获取 偏移经度
	 */
	public long getMaplon() {
		return maplon;
	}
	/**
	 * 设置偏移经度
	 * @param maplon 偏移经度 
	 */
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}
	/**
	 * @return 获取 偏移纬度
	 */
	public long getMaplat() {
		return maplat;
	}
	/**
	 * 设置偏移纬度
	 * @param maplat 偏移纬度 
	 */
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}
	/**
	 * @return 获取 海拔（单位：米）
	 */
	public int getElevation() {
		return elevation;
	}
	/**
	 * 设置海拔（单位：米）
	 * @param elevation 海拔（单位：米） 
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}
	/**
	 * @return 获取 方向（0-359，正北为0，顺时针单位：度）
	 */
	public int getDirection() {
		return direction;
	}
	/**
	 * 设置方向（0-359，正北为0，顺时针单位：度）
	 * @param direction 方向（0-359，正北为0，顺时针单位：度） 
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	/**
	 * @return 获取 GPS速度（110kmh单位：米小时）
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}
	/**
	 * 设置GPS速度（110kmh单位：米小时）
	 * @param gpsSpeed GPS速度（110kmh单位：米小时） 
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}
	/**
	 * @return 获取 速度（110kmh单位：米小时）
	 */
	public int getSpeed() {
		return speed;
	}
	/**
	 * 设置速度（110kmh单位：米小时）
	 * @param speed 速度（110kmh单位：米小时） 
	 */
	public void setSpeed(int speed) {
		this.speed = speed;
	}
	/**
	 * @return 获取 BCD时间（格式:yyMMddHHmmss）
	 */
	public String getBcdTime() {
		return bcdTime;
	}
	/**
	 * 设置BCD时间（格式:yyMMddHHmmss）
	 * @param bcdTime BCD时间（格式:yyMMddHHmmss） 
	 */
	public void setBcdTime(String bcdTime) {
		this.bcdTime = bcdTime;
	}
	/**
	 * @return 获取 UTC时间
	 */
	public long getUtcTime() {
		return utcTime;
	}
	/**
	 * 设置UTC时间
	 * @param utcTime UTC时间 
	 */
	public void setUtcTime(long utcTime) {
		this.utcTime = utcTime;
	}
	/**
	 * @return 获取 里程
	 */
	public long getMileage() {
		return mileage;
	}
	/**
	 * 设置里程
	 * @param mileage 里程 
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}
	/**
	 * @return 获取 油量
	 */
	public int getOil() {
		return oil;
	}
	/**
	 * 设置油量
	 * @param oil 油量 
	 */
	public void setOil(int oil) {
		this.oil = oil;
	}
	/**
	 * @return 获取 行驶记录仪速度
	 */
	public int getVssSpeed() {
		return vssSpeed;
	}
	/**
	 * 设置行驶记录仪速度
	 * @param vssSpeed 行驶记录仪速度 
	 */
	public void setVssSpeed(int vssSpeed) {
		this.vssSpeed = vssSpeed;
	}
	/**
	 * @return 获取 人工确认报警ID
	 */
	public int getAcknowledgeAlarmId() {
		return acknowledgeAlarmId;
	}
	/**
	 * 设置人工确认报警ID
	 * @param acknowledgeAlarmId 人工确认报警ID 
	 */
	public void setAcknowledgeAlarmId(int acknowledgeAlarmId) {
		this.acknowledgeAlarmId = acknowledgeAlarmId;
	}
	/**
	 * @return 获取 超速报警附加信息
	 */
	public String getOverspeedAlarmAdded() {
		return overspeedAlarmAdded;
	}
	/**
	 * 设置超速报警附加信息
	 * @param overspeedAlarmAdded 超速报警附加信息 
	 */
	public void setOverspeedAlarmAdded(String overspeedAlarmAdded) {
		this.overspeedAlarmAdded = overspeedAlarmAdded;
	}
	/**
	 * @return 获取 进出区域路线报警附加信息
	 */
	public String getAreaAndLineAlarmAdded() {
		return areaAndLineAlarmAdded;
	}
	/**
	 * 设置进出区域路线报警附加信息
	 * @param areaAndLineAlarmAdded 进出区域路线报警附加信息 
	 */
	public void setAreaAndLineAlarmAdded(String areaAndLineAlarmAdded) {
		this.areaAndLineAlarmAdded = areaAndLineAlarmAdded;
	}
	/**
	 * @return 获取 路段行驶时间不足过长报警附加信息
	 */
	public String getRoadTimeAlarmAdded() {
		return roadTimeAlarmAdded;
	}
	/**
	 * 设置路段行驶时间不足过长报警附加信息
	 * @param roadTimeAlarmAdded 路段行驶时间不足过长报警附加信息 
	 */
	public void setRoadTimeAlarmAdded(String roadTimeAlarmAdded) {
		this.roadTimeAlarmAdded = roadTimeAlarmAdded;
	}
	/**
	 * @return 获取 发动机转速
	 */
	public int getEngineRpm() {
		return engineRpm;
	}
	/**
	 * 设置发动机转速
	 * @param engineRpm 发动机转速 
	 */
	public void setEngineRpm(int engineRpm) {
		this.engineRpm = engineRpm;
	}
	/**
	 * @return 获取 瞬时油耗
	 */
	public int getInstantFuel() {
		return instantFuel;
	}
	/**
	 * 设置瞬时油耗
	 * @param instantFuel 瞬时油耗 
	 */
	public void setInstantFuel(int instantFuel) {
		this.instantFuel = instantFuel;
	}
	/**
	 * @return 获取 发动机扭矩百分比
	 */
	public long getEngineTorque() {
		return engineTorque;
	}
	/**
	 * 设置发动机扭矩百分比
	 * @param engineTorque 发动机扭矩百分比 
	 */
	public void setEngineTorque(long engineTorque) {
		this.engineTorque = engineTorque;
	}
	/**
	 * @return 获取 油门踏板位置
	 */
	public int getThrottlePedalPosition() {
		return throttlePedalPosition;
	}
	/**
	 * 设置油门踏板位置
	 * @param throttlePedalPosition 油门踏板位置 
	 */
	public void setThrottlePedalPosition(int throttlePedalPosition) {
		this.throttlePedalPosition = throttlePedalPosition;
	}
	/**
	 * @return 获取 扩展车辆报警标志
	 */
	public String getExpandAlarmFlag() {
		return expandAlarmFlag;
	}
	/**
	 * 设置扩展车辆报警标志
	 * @param expandAlarmFlag 扩展车辆报警标志 
	 */
	public void setExpandAlarmFlag(String expandAlarmFlag) {
		this.expandAlarmFlag = expandAlarmFlag;
	}
	/**
	 * @return 获取 扩展车辆信号状态
	 */
	public String getExpandStatusFlag() {
		return expandStatusFlag;
	}
	/**
	 * 设置扩展车辆信号状态
	 * @param expandStatusFlag 扩展车辆信号状态 
	 */
	public void setExpandStatusFlag(String expandStatusFlag) {
		this.expandStatusFlag = expandStatusFlag;
	}
	/**
	 * @return 获取 累计油耗
	 */
	public long getCumulativeFuel() {
		return cumulativeFuel;
	}
	/**
	 * 设置累计油耗
	 * @param cumulativeFuel 累计油耗 
	 */
	public void setCumulativeFuel(long cumulativeFuel) {
		this.cumulativeFuel = cumulativeFuel;
	}
	/**
	 * @return 获取 开门报警附加信息
	 */
	public int getDoorAlarmAdded() {
		return doorAlarmAdded;
	}
	/**
	 * 设置开门报警附加信息
	 * @param doorAlarmAdded 开门报警附加信息 
	 */
	public void setDoorAlarmAdded(int doorAlarmAdded) {
		this.doorAlarmAdded = doorAlarmAdded;
	}
	/**
	 * @return 获取 速度来源标识
	 */
	public int getSpeedSource() {
		return speedSource;
	}
	/**
	 * 设置速度来源标识
	 * @param speedSource 速度来源标识 
	 */
	public void setSpeedSource(int speedSource) {
		this.speedSource = speedSource;
	}
	/**
	 * @return 获取 计量仪油耗
	 */
	public long getMeterOil() {
		return meterOil;
	}
	/**
	 * 设置计量仪油耗
	 * @param meterOil 计量仪油耗 
	 */
	public void setMeterOil(long meterOil) {
		this.meterOil = meterOil;
	}
	/**
	 * @return 获取 IO状态位
	 */
	public int getIoStatusFlag() {
		return ioStatusFlag;
	}
	/**
	 * 设置IO状态位
	 * @param ioStatusFlag IO状态位 
	 */
	public void setIoStatusFlag(int ioStatusFlag) {
		this.ioStatusFlag = ioStatusFlag;
	}
	/**
	 * @return 获取 模拟量
	 */
	public long getAnalog() {
		return analog;
	}
	/**
	 * 设置模拟量
	 * @param analog 模拟量 
	 */
	public void setAnalog(long analog) {
		this.analog = analog;
	}
	/**
	 * @return 获取 无线通信网络信号强度
	 */
	public int getSignalStrength() {
		return signalStrength;
	}
	/**
	 * 设置无线通信网络信号强度
	 * @param signalStrength 无线通信网络信号强度 
	 */
	public void setSignalStrength(int signalStrength) {
		this.signalStrength = signalStrength;
	}
	/**
	 * @return 获取 GNSS定位卫星数
	 */
	public int getSatellites() {
		return satellites;
	}
	/**
	 * 设置GNSS定位卫星数
	 * @param satellites GNSS定位卫星数 
	 */
	public void setSatellites(int satellites) {
		this.satellites = satellites;
	}
	/**
	 * @return 获取 发动机运行总时长
	 */
	public int getEngineRunTotal() {
		return engineRunTotal;
	}
	/**
	 * 设置发动机运行总时长
	 * @param engineRunTotal 发动机运行总时长 
	 */
	public void setEngineRunTotal(int engineRunTotal) {
		this.engineRunTotal = engineRunTotal;
	}
	/**
	 * @return 获取 终端内置电池电压
	 */
	public int getTerminalBatteryVoltage() {
		return terminalBatteryVoltage;
	}
	/**
	 * 设置终端内置电池电压
	 * @param terminalBatteryVoltage 终端内置电池电压 
	 */
	public void setTerminalBatteryVoltage(int terminalBatteryVoltage) {
		this.terminalBatteryVoltage = terminalBatteryVoltage;
	}
	/**
	 * @return 获取 蓄电池电压
	 */
	public int getBatteryVoltage() {
		return batteryVoltage;
	}
	/**
	 * 设置蓄电池电压
	 * @param batteryVoltage 蓄电池电压 
	 */
	public void setBatteryVoltage(int batteryVoltage) {
		this.batteryVoltage = batteryVoltage;
	}
	/**
	 * @return 获取 机油温度
	 */
	public int getOilTemperature() {
		return oilTemperature;
	}
	/**
	 * 设置机油温度
	 * @param oilTemperature 机油温度 
	 */
	public void setOilTemperature(int oilTemperature) {
		this.oilTemperature = oilTemperature;
	}
	/**
	 * @return 获取 发动机冷却液温度
	 */
	public int getCoolantTemperature() {
		return coolantTemperature;
	}
	/**
	 * 设置发动机冷却液温度
	 * @param coolantTemperature 发动机冷却液温度 
	 */
	public void setCoolantTemperature(int coolantTemperature) {
		this.coolantTemperature = coolantTemperature;
	}
	/**
	 * @return 获取 进气温度
	 */
	public int getInletTemperature() {
		return inletTemperature;
	}
	/**
	 * 设置进气温度
	 * @param inletTemperature 进气温度 
	 */
	public void setInletTemperature(int inletTemperature) {
		this.inletTemperature = inletTemperature;
	}
	/**
	 * @return 获取 机油压力
	 */
	public int getOilPressure() {
		return oilPressure;
	}
	/**
	 * 设置机油压力
	 * @param oilPressure 机油压力 
	 */
	public void setOilPressure(int oilPressure) {
		this.oilPressure = oilPressure;
	}
	/**
	 * @return 获取 大气压力
	 */
	public int getAtmosphericPressure() {
		return atmosphericPressure;
	}
	/**
	 * 设置大气压力
	 * @param atmosphericPressure 大气压力 
	 */
	public void setAtmosphericPressure(int atmosphericPressure) {
		this.atmosphericPressure = atmosphericPressure;
	}
	/**
	 * @return 获取 公共自定义信息内容
	 */
	public String getCustomContent() {
		return customContent;
	}
	/**
	 * 设置公共自定义信息内容
	 * @param customContent 公共自定义信息内容 
	 */
	public void setCustomContent(String customContent) {
		this.customContent = customContent;
	}
	/**
	 * @return 获取 车队编号
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置车队编号
	 * @param teamId 车队编号 
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	/**
	 * @return 获取 车队名称
	 */
	public String getTeamName() {
		return teamName;
	}
	/**
	 * 设置车队名称
	 * @param teamName 车队名称 
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	/**
	 * @return 获取 组织编号
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置组织编号
	 * @param entId 组织编号 
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * @return 获取 组织名称
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置组织名称
	 * @param entName 组织名称 
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * @return 获取 车辆内部编码
	 */
	public String getInnerCode() {
		return innerCode;
	}
	/**
	 * 设置车辆内部编码
	 * @param innerCode 车辆内部编码 
	 */
	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}
	/**
	 * @return 获取 驾驶员ID
	 */
	public String getStaffId() {
		return staffId;
	}
	/**
	 * 设置驾驶员ID
	 * @param staffId 驾驶员ID 
	 */
	public void setStaffId(String staffId) {
		this.staffId = staffId;
	}
	/**
	 * @return 获取 驾驶员名称
	 */
	public String getStaffName() {
		return staffName;
	}
	/**
	 * 设置驾驶员名称
	 * @param staffName 驾驶员名称 
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}
	/**
	 * @return 获取 车牌号颜色
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌号颜色
	 * @param plateColor 车牌号颜色 
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	/**
	 * @return 获取 终端号
	 */
	public String getTid() {
		return tid;
	}
	/**
	 * 设置终端号
	 * @param tid 终端号 
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}
	/**
	 * @return 获取 终端型号
	 */
	public String getTerminalType() {
		return terminalType;
	}
	/**
	 * 设置终端型号
	 * @param terminalType 终端型号 
	 */
	public void setTerminalType(String terminalType) {
		this.terminalType = terminalType;
	}
	/**
	 * @return 获取 状态
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * 设置状态
	 * @param status 状态 
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * @return 获取 在线状态
	 */
	public int getOnline() {
		return online;
	}
	/**
	 * 设置在线状态
	 * @param online 在线状态 
	 */
	public void setOnline(int online) {
		this.online = online;
	}
	/**
	 * @return 获取 厂商编号
	 */
	public String getOemCode() {
		return oemCode;
	}
	/**
	 * 设置厂商编号
	 * @param oemCode 厂商编号 
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
	
}
