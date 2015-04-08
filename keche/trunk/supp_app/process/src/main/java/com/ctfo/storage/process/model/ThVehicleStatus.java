package com.ctfo.storage.process.model;

/**
 * ThVehicleStatus 车辆状态表
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:45:48
 * 
 */
public class ThVehicleStatus {
	/** 车辆ID*/
	private String vid ;
	/** 车牌号码*/
	private String vehicleNo ;
	/** 车架号(vin)*/
	private String vinCode ;
	/** 车辆内部编码,保证企业内唯一*/
	private String innerCode ;
	/** 企业ID*/
	private String entId ;
	/** 企业名称*/
	private String entName ;
	/** 车队ID*/
	private String teamId ;
	/** 车队名称*/
	private String teamName ;
	/** 驾驶员ID*/
	private String staffId ;
	/** 驾驶员名称*/
	private String staffName ;
	/** 状态采集时间*/
	private long gatherTime ;
	/** 创建时间*/
	private long createTime ;
	/** 终端状态(0 绿灯 1红灯 2 灰灯)*/
	private int terminalStatus ;
	/** 终端状态值*/
	private int terminalStatusValue ;
	/** GPS状态*/
	private int gpsStatus ;
	/** GPS状态值*/
	private int gpsStatusValue ;
	/** 冷却液温度状态*/
	private int eWaterTempStatus ;
	/** 冷却液温度值*/
	private int eWaterTemp ;
	/** 蓄电池电压状态*/
	private int extVoltageStatus ;
	/** 蓄电池电压值 */
	private int extVoltage ;
	/** 油压状态*/
	private int oilPressureStatus ;
	/** 油压值*/
	private int oilPressure ;
	/** 制动气压状态*/
	private int breakPressureStatus ;
	/** 制动气压值*/
	private int breakPressure ;
	/** 制动蹄片磨损状态*/
	private int breakpadFrayStatus ;
	/** 制动蹄片磨损值*/
	private int breakpadFray ;
	/** 燃油告警状态*/
	private int oilAlarmStatus ;
	/** 燃油告警值*/
	private int oilAlarm ;
	/** ABS故障状态*/
	private int absBugStatus ;
	/** ABS故障值*/
	private int absBug ;
	/** 水位低状态*/
	private int coolantLevel ;
	/** 水位值*/
	private int coolant ;
	/** 空滤堵塞*/
	private int airFilterClogStatus ;
	/** 空滤堵塞值*/
	private int airFilterClog ;
	/** 机虑堵塞*/
	private int mWereBlockingStatus ;
	/** 机虑堵塞值*/
	private int mWereBlocking ;
	/** 燃油堵塞*/
	private int fuelBlockingStatus ;
	/** 燃油堵塞值*/
	private int fuelBlocking ;
	/** 机油温度*/
	private int eOilTemperatureAlarmStatus ;
	/** 机油温度值*/
	private int eOilTemperatureAlarm ;
	/** 缓速器高温*/
	private int retarderHtAlarmStatus ;
	/** 缓速器温度值*/
	private int retarderHtAlarm ;
	/** 仓温高状态*/
	private int eHousingHtAlarmStatus ;
	/** 仓温值*/
	private int eHousingHtAlarm ;
	/** 整车状态：根据所有状态进行判断，只要有一个状态为红色，此处就标记1，全部为绿色时此处标记为0，其他状态全部为灰色是标记为2*/
	private int vehicleStatus ;
	/** 大气压力状态*/
	private int airPressureStatus ;
	/** 大气压力值*/
	private int airPressure ;
	/** 线路id*/
	private int lineId ;
	/** 导航系统故障报警状态*/
	private int gpsFaultStatus ;
	/** 导航系统故障报警值*/
	private int gpsFault ;
	/** 导航系统天线未接报警状态*/
	private int gpsOpenCircuitStatus ;
	/** 导航系统天线未接报警值*/
	private int gpsOpenCircuit ;
	/** 导航系统天线短路报警状态*/
	private int gpsShortCircuitStatus ;
	/** 导航系统天线短路报警值*/
	private int gpsShortCircuit ;
	/** 车机主电源欠压报警状态*/
	private int terminalUnderVoltageStatus ;
	/** 车机主电源欠压报警值*/
	private int terminalUnderVoltage ;
	/** 车机主电源掉电报警状态*/
	private int terminalPowerDownStatus ;
	/** 车机主电源掉电报警值*/
	private int terminalPowerDown ;
	/** 终端显示屏故障报警状态*/
	private int terminalScreenFaultStatus ;
	/** 终端显示屏故障报警值*/
	private int terminalScreenFault ;
	/** 语音模块故障报警状态*/
	private int ttsFaultStatus ;
	/** 语音模块故障报警值*/
	private int ttsFault ;
	/** 摄像头故障报警状态*/
	private int cameraFaultStatus ;
	/** 摄像头故障报警值 */
	private int cameraFault ;
	/**
	 * @return 获取 车辆ID
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆ID
	 * @param vid 车辆ID 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 车牌号码
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}
	/**
	 * 设置车牌号码
	 * @param vehicleNo 车牌号码 
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}
	/**
	 * @return 获取 车架号(vin)
	 */
	public String getVinCode() {
		return vinCode;
	}
	/**
	 * 设置车架号(vin)
	 * @param vinCode 车架号(vin) 
	 */
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	/**
	 * @return 获取 车辆内部编码保证企业内唯一
	 */
	public String getInnerCode() {
		return innerCode;
	}
	/**
	 * 设置车辆内部编码保证企业内唯一
	 * @param innerCode 车辆内部编码保证企业内唯一 
	 */
	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}
	/**
	 * @return 获取 企业ID
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置企业ID
	 * @param entId 企业ID 
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * @return 获取 企业名称
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置企业名称
	 * @param entName 企业名称 
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * @return 获取 车队ID
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置车队ID
	 * @param teamId 车队ID 
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
	 * @return 获取 状态采集时间
	 */
	public long getGatherTime() {
		return gatherTime;
	}
	/**
	 * 设置状态采集时间
	 * @param gatherTime 状态采集时间 
	 */
	public void setGatherTime(long gatherTime) {
		this.gatherTime = gatherTime;
	}
	/**
	 * @return 获取 创建时间
	 */
	public long getCreateTime() {
		return createTime;
	}
	/**
	 * 设置创建时间
	 * @param createTime 创建时间 
	 */
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	/**
	 * @return 获取 终端状态(0绿灯1红灯2灰灯)
	 */
	public int getTerminalStatus() {
		return terminalStatus;
	}
	/**
	 * 设置终端状态(0绿灯1红灯2灰灯)
	 * @param terminalStatus 终端状态(0绿灯1红灯2灰灯) 
	 */
	public void setTerminalStatus(int terminalStatus) {
		this.terminalStatus = terminalStatus;
	}
	/**
	 * @return 获取 终端状态值
	 */
	public int getTerminalStatusValue() {
		return terminalStatusValue;
	}
	/**
	 * 设置终端状态值
	 * @param terminalStatusValue 终端状态值 
	 */
	public void setTerminalStatusValue(int terminalStatusValue) {
		this.terminalStatusValue = terminalStatusValue;
	}
	/**
	 * @return 获取 GPS状态
	 */
	public int getGpsStatus() {
		return gpsStatus;
	}
	/**
	 * 设置GPS状态
	 * @param gpsStatus GPS状态 
	 */
	public void setGpsStatus(int gpsStatus) {
		this.gpsStatus = gpsStatus;
	}
	/**
	 * @return 获取 GPS状态值
	 */
	public int getGpsStatusValue() {
		return gpsStatusValue;
	}
	/**
	 * 设置GPS状态值
	 * @param gpsStatusValue GPS状态值 
	 */
	public void setGpsStatusValue(int gpsStatusValue) {
		this.gpsStatusValue = gpsStatusValue;
	}
	/**
	 * @return 获取 冷却液温度状态
	 */
	public int geteWaterTempStatus() {
		return eWaterTempStatus;
	}
	/**
	 * 设置冷却液温度状态
	 * @param eWaterTempStatus 冷却液温度状态 
	 */
	public void seteWaterTempStatus(int eWaterTempStatus) {
		this.eWaterTempStatus = eWaterTempStatus;
	}
	/**
	 * @return 获取 冷却液温度值
	 */
	public int geteWaterTemp() {
		return eWaterTemp;
	}
	/**
	 * 设置冷却液温度值
	 * @param eWaterTemp 冷却液温度值 
	 */
	public void seteWaterTemp(int eWaterTemp) {
		this.eWaterTemp = eWaterTemp;
	}
	/**
	 * @return 获取 蓄电池电压状态
	 */
	public int getExtVoltageStatus() {
		return extVoltageStatus;
	}
	/**
	 * 设置蓄电池电压状态
	 * @param extVoltageStatus 蓄电池电压状态 
	 */
	public void setExtVoltageStatus(int extVoltageStatus) {
		this.extVoltageStatus = extVoltageStatus;
	}
	/**
	 * @return 获取 蓄电池电压值
	 */
	public int getExtVoltage() {
		return extVoltage;
	}
	/**
	 * 设置蓄电池电压值
	 * @param extVoltage 蓄电池电压值 
	 */
	public void setExtVoltage(int extVoltage) {
		this.extVoltage = extVoltage;
	}
	/**
	 * @return 获取 油压状态
	 */
	public int getOilPressureStatus() {
		return oilPressureStatus;
	}
	/**
	 * 设置油压状态
	 * @param oilPressureStatus 油压状态 
	 */
	public void setOilPressureStatus(int oilPressureStatus) {
		this.oilPressureStatus = oilPressureStatus;
	}
	/**
	 * @return 获取 油压值
	 */
	public int getOilPressure() {
		return oilPressure;
	}
	/**
	 * 设置油压值
	 * @param oilPressure 油压值 
	 */
	public void setOilPressure(int oilPressure) {
		this.oilPressure = oilPressure;
	}
	/**
	 * @return 获取 制动气压状态
	 */
	public int getBreakPressureStatus() {
		return breakPressureStatus;
	}
	/**
	 * 设置制动气压状态
	 * @param breakPressureStatus 制动气压状态 
	 */
	public void setBreakPressureStatus(int breakPressureStatus) {
		this.breakPressureStatus = breakPressureStatus;
	}
	/**
	 * @return 获取 制动气压值
	 */
	public int getBreakPressure() {
		return breakPressure;
	}
	/**
	 * 设置制动气压值
	 * @param breakPressure 制动气压值 
	 */
	public void setBreakPressure(int breakPressure) {
		this.breakPressure = breakPressure;
	}
	/**
	 * @return 获取 制动蹄片磨损状态
	 */
	public int getBreakpadFrayStatus() {
		return breakpadFrayStatus;
	}
	/**
	 * 设置制动蹄片磨损状态
	 * @param breakpadFrayStatus 制动蹄片磨损状态 
	 */
	public void setBreakpadFrayStatus(int breakpadFrayStatus) {
		this.breakpadFrayStatus = breakpadFrayStatus;
	}
	/**
	 * @return 获取 制动蹄片磨损值
	 */
	public int getBreakpadFray() {
		return breakpadFray;
	}
	/**
	 * 设置制动蹄片磨损值
	 * @param breakpadFray 制动蹄片磨损值 
	 */
	public void setBreakpadFray(int breakpadFray) {
		this.breakpadFray = breakpadFray;
	}
	/**
	 * @return 获取 燃油告警状态
	 */
	public int getOilAlarmStatus() {
		return oilAlarmStatus;
	}
	/**
	 * 设置燃油告警状态
	 * @param oilAlarmStatus 燃油告警状态 
	 */
	public void setOilAlarmStatus(int oilAlarmStatus) {
		this.oilAlarmStatus = oilAlarmStatus;
	}
	/**
	 * @return 获取 燃油告警值
	 */
	public int getOilAlarm() {
		return oilAlarm;
	}
	/**
	 * 设置燃油告警值
	 * @param oilAlarm 燃油告警值 
	 */
	public void setOilAlarm(int oilAlarm) {
		this.oilAlarm = oilAlarm;
	}
	/**
	 * @return 获取 ABS故障状态
	 */
	public int getAbsBugStatus() {
		return absBugStatus;
	}
	/**
	 * 设置ABS故障状态
	 * @param absBugStatus ABS故障状态 
	 */
	public void setAbsBugStatus(int absBugStatus) {
		this.absBugStatus = absBugStatus;
	}
	/**
	 * @return 获取 ABS故障值
	 */
	public int getAbsBug() {
		return absBug;
	}
	/**
	 * 设置ABS故障值
	 * @param absBug ABS故障值 
	 */
	public void setAbsBug(int absBug) {
		this.absBug = absBug;
	}
	/**
	 * @return 获取 水位低状态
	 */
	public int getCoolantLevel() {
		return coolantLevel;
	}
	/**
	 * 设置水位低状态
	 * @param coolantLevel 水位低状态 
	 */
	public void setCoolantLevel(int coolantLevel) {
		this.coolantLevel = coolantLevel;
	}
	/**
	 * @return 获取 水位值
	 */
	public int getCoolant() {
		return coolant;
	}
	/**
	 * 设置水位值
	 * @param coolant 水位值 
	 */
	public void setCoolant(int coolant) {
		this.coolant = coolant;
	}
	/**
	 * @return 获取 空滤堵塞
	 */
	public int getAirFilterClogStatus() {
		return airFilterClogStatus;
	}
	/**
	 * 设置空滤堵塞
	 * @param airFilterClogStatus 空滤堵塞 
	 */
	public void setAirFilterClogStatus(int airFilterClogStatus) {
		this.airFilterClogStatus = airFilterClogStatus;
	}
	/**
	 * @return 获取 空滤堵塞值
	 */
	public int getAirFilterClog() {
		return airFilterClog;
	}
	/**
	 * 设置空滤堵塞值
	 * @param airFilterClog 空滤堵塞值 
	 */
	public void setAirFilterClog(int airFilterClog) {
		this.airFilterClog = airFilterClog;
	}
	/**
	 * @return 获取 机虑堵塞
	 */
	public int getmWereBlockingStatus() {
		return mWereBlockingStatus;
	}
	/**
	 * 设置机虑堵塞
	 * @param mWereBlockingStatus 机虑堵塞 
	 */
	public void setmWereBlockingStatus(int mWereBlockingStatus) {
		this.mWereBlockingStatus = mWereBlockingStatus;
	}
	/**
	 * @return 获取 机虑堵塞值
	 */
	public int getmWereBlocking() {
		return mWereBlocking;
	}
	/**
	 * 设置机虑堵塞值
	 * @param mWereBlocking 机虑堵塞值 
	 */
	public void setmWereBlocking(int mWereBlocking) {
		this.mWereBlocking = mWereBlocking;
	}
	/**
	 * @return 获取 燃油堵塞
	 */
	public int getFuelBlockingStatus() {
		return fuelBlockingStatus;
	}
	/**
	 * 设置燃油堵塞
	 * @param fuelBlockingStatus 燃油堵塞 
	 */
	public void setFuelBlockingStatus(int fuelBlockingStatus) {
		this.fuelBlockingStatus = fuelBlockingStatus;
	}
	/**
	 * @return 获取 燃油堵塞值
	 */
	public int getFuelBlocking() {
		return fuelBlocking;
	}
	/**
	 * 设置燃油堵塞值
	 * @param fuelBlocking 燃油堵塞值 
	 */
	public void setFuelBlocking(int fuelBlocking) {
		this.fuelBlocking = fuelBlocking;
	}
	/**
	 * @return 获取 机油温度
	 */
	public int geteOilTemperatureAlarmStatus() {
		return eOilTemperatureAlarmStatus;
	}
	/**
	 * 设置机油温度
	 * @param eOilTemperatureAlarmStatus 机油温度 
	 */
	public void seteOilTemperatureAlarmStatus(int eOilTemperatureAlarmStatus) {
		this.eOilTemperatureAlarmStatus = eOilTemperatureAlarmStatus;
	}
	/**
	 * @return 获取 机油温度值
	 */
	public int geteOilTemperatureAlarm() {
		return eOilTemperatureAlarm;
	}
	/**
	 * 设置机油温度值
	 * @param eOilTemperatureAlarm 机油温度值 
	 */
	public void seteOilTemperatureAlarm(int eOilTemperatureAlarm) {
		this.eOilTemperatureAlarm = eOilTemperatureAlarm;
	}
	/**
	 * @return 获取 缓速器高温
	 */
	public int getRetarderHtAlarmStatus() {
		return retarderHtAlarmStatus;
	}
	/**
	 * 设置缓速器高温
	 * @param retarderHtAlarmStatus 缓速器高温 
	 */
	public void setRetarderHtAlarmStatus(int retarderHtAlarmStatus) {
		this.retarderHtAlarmStatus = retarderHtAlarmStatus;
	}
	/**
	 * @return 获取 缓速器温度值
	 */
	public int getRetarderHtAlarm() {
		return retarderHtAlarm;
	}
	/**
	 * 设置缓速器温度值
	 * @param retarderHtAlarm 缓速器温度值 
	 */
	public void setRetarderHtAlarm(int retarderHtAlarm) {
		this.retarderHtAlarm = retarderHtAlarm;
	}
	/**
	 * @return 获取 仓温高状态
	 */
	public int geteHousingHtAlarmStatus() {
		return eHousingHtAlarmStatus;
	}
	/**
	 * 设置仓温高状态
	 * @param eHousingHtAlarmStatus 仓温高状态 
	 */
	public void seteHousingHtAlarmStatus(int eHousingHtAlarmStatus) {
		this.eHousingHtAlarmStatus = eHousingHtAlarmStatus;
	}
	/**
	 * @return 获取 仓温值
	 */
	public int geteHousingHtAlarm() {
		return eHousingHtAlarm;
	}
	/**
	 * 设置仓温值
	 * @param eHousingHtAlarm 仓温值 
	 */
	public void seteHousingHtAlarm(int eHousingHtAlarm) {
		this.eHousingHtAlarm = eHousingHtAlarm;
	}
	/**
	 * @return 获取 整车状态：根据所有状态进行判断，只要有一个状态为红色，此处就标记1，全部为绿色时此处标记为0，其他状态全部为灰色是标记为2
	 */
	public int getVehicleStatus() {
		return vehicleStatus;
	}
	/**
	 * 设置整车状态：根据所有状态进行判断，只要有一个状态为红色，此处就标记1，全部为绿色时此处标记为0，其他状态全部为灰色是标记为2
	 * @param vehicleStatus 整车状态：根据所有状态进行判断，只要有一个状态为红色，此处就标记1，全部为绿色时此处标记为0，其他状态全部为灰色是标记为2 
	 */
	public void setVehicleStatus(int vehicleStatus) {
		this.vehicleStatus = vehicleStatus;
	}
	/**
	 * @return 获取 大气压力状态
	 */
	public int getAirPressureStatus() {
		return airPressureStatus;
	}
	/**
	 * 设置大气压力状态
	 * @param airPressureStatus 大气压力状态 
	 */
	public void setAirPressureStatus(int airPressureStatus) {
		this.airPressureStatus = airPressureStatus;
	}
	/**
	 * @return 获取 大气压力值
	 */
	public int getAirPressure() {
		return airPressure;
	}
	/**
	 * 设置大气压力值
	 * @param airPressure 大气压力值 
	 */
	public void setAirPressure(int airPressure) {
		this.airPressure = airPressure;
	}
	/**
	 * @return 获取 线路id
	 */
	public int getLineId() {
		return lineId;
	}
	/**
	 * 设置线路id
	 * @param lineId 线路id 
	 */
	public void setLineId(int lineId) {
		this.lineId = lineId;
	}
	/**
	 * @return 获取 导航系统故障报警状态
	 */
	public int getGpsFaultStatus() {
		return gpsFaultStatus;
	}
	/**
	 * 设置导航系统故障报警状态
	 * @param gpsFaultStatus 导航系统故障报警状态 
	 */
	public void setGpsFaultStatus(int gpsFaultStatus) {
		this.gpsFaultStatus = gpsFaultStatus;
	}
	/**
	 * @return 获取 导航系统故障报警值
	 */
	public int getGpsFault() {
		return gpsFault;
	}
	/**
	 * 设置导航系统故障报警值
	 * @param gpsFault 导航系统故障报警值 
	 */
	public void setGpsFault(int gpsFault) {
		this.gpsFault = gpsFault;
	}
	/**
	 * @return 获取 导航系统天线未接报警状态
	 */
	public int getGpsOpenCircuitStatus() {
		return gpsOpenCircuitStatus;
	}
	/**
	 * 设置导航系统天线未接报警状态
	 * @param gpsOpenCircuitStatus 导航系统天线未接报警状态 
	 */
	public void setGpsOpenCircuitStatus(int gpsOpenCircuitStatus) {
		this.gpsOpenCircuitStatus = gpsOpenCircuitStatus;
	}
	/**
	 * @return 获取 导航系统天线未接报警值
	 */
	public int getGpsOpenCircuit() {
		return gpsOpenCircuit;
	}
	/**
	 * 设置导航系统天线未接报警值
	 * @param gpsOpenCircuit 导航系统天线未接报警值 
	 */
	public void setGpsOpenCircuit(int gpsOpenCircuit) {
		this.gpsOpenCircuit = gpsOpenCircuit;
	}
	/**
	 * @return 获取 导航系统天线短路报警状态
	 */
	public int getGpsShortCircuitStatus() {
		return gpsShortCircuitStatus;
	}
	/**
	 * 设置导航系统天线短路报警状态
	 * @param gpsShortCircuitStatus 导航系统天线短路报警状态 
	 */
	public void setGpsShortCircuitStatus(int gpsShortCircuitStatus) {
		this.gpsShortCircuitStatus = gpsShortCircuitStatus;
	}
	/**
	 * @return 获取 导航系统天线短路报警值
	 */
	public int getGpsShortCircuit() {
		return gpsShortCircuit;
	}
	/**
	 * 设置导航系统天线短路报警值
	 * @param gpsShortCircuit 导航系统天线短路报警值 
	 */
	public void setGpsShortCircuit(int gpsShortCircuit) {
		this.gpsShortCircuit = gpsShortCircuit;
	}
	/**
	 * @return 获取 车机主电源欠压报警状态
	 */
	public int getTerminalUnderVoltageStatus() {
		return terminalUnderVoltageStatus;
	}
	/**
	 * 设置车机主电源欠压报警状态
	 * @param terminalUnderVoltageStatus 车机主电源欠压报警状态 
	 */
	public void setTerminalUnderVoltageStatus(int terminalUnderVoltageStatus) {
		this.terminalUnderVoltageStatus = terminalUnderVoltageStatus;
	}
	/**
	 * @return 获取 车机主电源欠压报警值
	 */
	public int getTerminalUnderVoltage() {
		return terminalUnderVoltage;
	}
	/**
	 * 设置车机主电源欠压报警值
	 * @param terminalUnderVoltage 车机主电源欠压报警值 
	 */
	public void setTerminalUnderVoltage(int terminalUnderVoltage) {
		this.terminalUnderVoltage = terminalUnderVoltage;
	}
	/**
	 * @return 获取 车机主电源掉电报警状态
	 */
	public int getTerminalPowerDownStatus() {
		return terminalPowerDownStatus;
	}
	/**
	 * 设置车机主电源掉电报警状态
	 * @param terminalPowerDownStatus 车机主电源掉电报警状态 
	 */
	public void setTerminalPowerDownStatus(int terminalPowerDownStatus) {
		this.terminalPowerDownStatus = terminalPowerDownStatus;
	}
	/**
	 * @return 获取 车机主电源掉电报警值
	 */
	public int getTerminalPowerDown() {
		return terminalPowerDown;
	}
	/**
	 * 设置车机主电源掉电报警值
	 * @param terminalPowerDown 车机主电源掉电报警值 
	 */
	public void setTerminalPowerDown(int terminalPowerDown) {
		this.terminalPowerDown = terminalPowerDown;
	}
	/**
	 * @return 获取 终端显示屏故障报警状态
	 */
	public int getTerminalScreenFaultStatus() {
		return terminalScreenFaultStatus;
	}
	/**
	 * 设置终端显示屏故障报警状态
	 * @param terminalScreenFaultStatus 终端显示屏故障报警状态 
	 */
	public void setTerminalScreenFaultStatus(int terminalScreenFaultStatus) {
		this.terminalScreenFaultStatus = terminalScreenFaultStatus;
	}
	/**
	 * @return 获取 终端显示屏故障报警值
	 */
	public int getTerminalScreenFault() {
		return terminalScreenFault;
	}
	/**
	 * 设置终端显示屏故障报警值
	 * @param terminalScreenFault 终端显示屏故障报警值 
	 */
	public void setTerminalScreenFault(int terminalScreenFault) {
		this.terminalScreenFault = terminalScreenFault;
	}
	/**
	 * @return 获取 语音模块故障报警状态
	 */
	public int getTtsFaultStatus() {
		return ttsFaultStatus;
	}
	/**
	 * 设置语音模块故障报警状态
	 * @param ttsFaultStatus 语音模块故障报警状态 
	 */
	public void setTtsFaultStatus(int ttsFaultStatus) {
		this.ttsFaultStatus = ttsFaultStatus;
	}
	/**
	 * @return 获取 语音模块故障报警值
	 */
	public int getTtsFault() {
		return ttsFault;
	}
	/**
	 * 设置语音模块故障报警值
	 * @param ttsFault 语音模块故障报警值 
	 */
	public void setTtsFault(int ttsFault) {
		this.ttsFault = ttsFault;
	}
	/**
	 * @return 获取 摄像头故障报警状态
	 */
	public int getCameraFaultStatus() {
		return cameraFaultStatus;
	}
	/**
	 * 设置摄像头故障报警状态
	 * @param cameraFaultStatus 摄像头故障报警状态 
	 */
	public void setCameraFaultStatus(int cameraFaultStatus) {
		this.cameraFaultStatus = cameraFaultStatus;
	}
	/**
	 * @return 获取 摄像头故障报警值
	 */
	public int getCameraFault() {
		return cameraFault;
	}
	/**
	 * 设置摄像头故障报警值
	 * @param cameraFault 摄像头故障报警值 
	 */
	public void setCameraFault(int cameraFault) {
		this.cameraFault = cameraFault;
	}
	
}
