package com.ctfo.trackservice.model;

public class EquipmentStatus {
	
	private String vid = "1"; //车辆编码
	
	private double terminalValue = -1; // 终端状态
	
	private int terminalStatus = 2; // 终端状态
	
	private double gpsValue = -1; // GPS状态
	
	private int gpsStatusStatus = 2;
	
	private double eWaterValue = -1; // 冷却液温度  // -1：无值上报,-2：表示上报最大极限无效值
	
	private int eWaterStatus = 2;
	
	private double extVoltageValue = -1; // 蓄电池电压 // -1：无值上报,-2：表示上报最大极限无效值
	
	private int extVoltageStatus = 2;
	
	private double oilPressureValue = -1; // 油压状态
	
	private int oilPressureStatus = 2;
	
	private double brakePressureValue = -1; // 制动气压值
	
	private int brakePressureStatus = 2;
	
	private double brakepadFrayValue = -1; // 制动蹄片磨损
	
	private int brakepadFrayStatus = 2; 
	
	private double oilAramValue = -1; // 燃油告警
	
	private int oilAramStatus = 2;
	
	private double absBugValue = -1; // ABS故障状态
	
	private int absBugStatus = 2;
	
	private double coolantLevelValue = -1; // 水位低状态
	
	private int coolantLevelStatus = 2;
	
	private double airFilterValue = -1; // 空滤堵塞
	
	private int airFilterStatus = 2;
	
	private double mwereBlockingValue = -1; // 机虑堵塞
	
	private int mwereBlockingStatus = 2;
	
	private double fuelBlockingValue = -1; // 燃油堵塞
	
	private int fuelBlockingStatus = 2;
	
	private double eoilTemperatureValue = -1; // 机油温度
	
	private int eoilTemperatureStatus = 2; // 机油温度
	
	private double retarerHtValue = -1; // 缓速器高温
	
	private int retarerHtStatus = 2; // 缓速器高温
	
	private double ehousingValue = -1; // 仓温高状态
	
	private int ehousingStatus = 2; // 仓温高状态

	private double airPressureValue = -1; //大气压力  // -1：无值上报,-2：表示上报最大极限无效值
	
	private int airPressureStatus = 2; //大气压力
	
	private int gpsFaultStatus = 2; //导航系统故障报警状态
	
	private double gpsFaultValue = -1;
	
	private int gpsOpenciruitStatus = 2;// 导航系统天线未接报警状态
	
	private double gpsOpenciruitValue = -1;
	
	private int gpsShortciruitStatus = 2; //导航系统天线短路报警状态
	
	private double gpsShortciruitValue = -1;
	
	private int terminalUnderVoltageStatus = 2;//车机主电源欠压报警状态
	
	private double terminalUnderVoltageValue = -1;
	
	private int terminalPowerDownStatus = 2; // 车机主电源掉电报警状态
	
	private double terminalPowerDownValue = -1;
	
	private int terminalScreenfalutStatus = 2; //终端显示屏故障报警状态
	
	private double terminalScreenfalutValue = -1;
	
	private int ttsFaultStatus = 2; //语音模块故障报警状态
	
	private double ttsFaultValue = -1;
	
	private int cameraFaultStatus = 2; // 摄像头故障报警状态
	
	private double cameraFaultValue = -1;
	
	private String gpsTime = null;
	
	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getGpsTime() {
		return gpsTime;
	}

	public void setGpsTime(String gpsTime) {
		this.gpsTime = gpsTime;
	}

	public double getTerminalValue() {
		return terminalValue;
	}

	public void setTerminalValue(double terminalValue) {
		this.terminalValue = terminalValue;
	}

	public int getTerminalStatus() {
		return terminalStatus;
	}

	public void setTerminalStatus(int terminalStatus) {
		this.terminalStatus = terminalStatus;
	}

	public double getGpsValue() {
		return gpsValue;
	}

	public void setGpsValue(double gpsValue) {
		this.gpsValue = gpsValue;
	}

	public int getGpsStatusStatus() {
		return gpsStatusStatus;
	}

	public void setGpsStatusStatus(int gpsStatusStatus) {
		this.gpsStatusStatus = gpsStatusStatus;
	}

	public double geteWaterValue() {
		return eWaterValue;
	}

	public void seteWaterValue(double eWaterValue) {
		this.eWaterValue = eWaterValue;
	}

	public int geteWaterStatus() {
		return eWaterStatus;
	}

	public void seteWaterStatus(int eWaterStatus) {
		this.eWaterStatus = eWaterStatus;
	}

	public double getExtVoltageValue() {
		return extVoltageValue;
	}

	public void setExtVoltageValue(double extVoltageValue) {
		this.extVoltageValue = extVoltageValue;
	}

	public int getExtVoltageStatus() {
		return extVoltageStatus;
	}

	public void setExtVoltageStatus(int extVoltageStatus) {
		this.extVoltageStatus = extVoltageStatus;
	}

	public double getOilPressureValue() {
		return oilPressureValue;
	}

	public void setOilPressureValue(double oilPressureValue) {
		this.oilPressureValue = oilPressureValue;
	}

	public int getOilPressureStatus() {
		return oilPressureStatus;
	}

	public void setOilPressureStatus(int oilPressureStatus) {
		this.oilPressureStatus = oilPressureStatus;
	}

	public double getBrakePressureValue() {
		return brakePressureValue;
	}

	public void setBrakePressureValue(double brakePressureValue) {
		this.brakePressureValue = brakePressureValue;
	}

	public int getBrakePressureStatus() {
		return brakePressureStatus;
	}

	public void setBrakePressureStatus(int brakePressureStatus) {
		this.brakePressureStatus = brakePressureStatus;
	}

	public double getBrakepadFrayValue() {
		return brakepadFrayValue;
	}

	public void setBrakepadFrayValue(double brakepadFrayValue) {
		this.brakepadFrayValue = brakepadFrayValue;
	}

	public int getBrakepadFrayStatus() {
		return brakepadFrayStatus;
	}

	public void setBrakepadFrayStatus(int brakepadFrayStatus) {
		this.brakepadFrayStatus = brakepadFrayStatus;
	}

	public double getOilAramValue() {
		return oilAramValue;
	}

	public void setOilAramValue(double oilAramValue) {
		this.oilAramValue = oilAramValue;
	}

	public int getOilAramStatus() {
		return oilAramStatus;
	}

	public void setOilAramStatus(int oilAramStatus) {
		this.oilAramStatus = oilAramStatus;
	}

	public double getAbsBugValue() {
		return absBugValue;
	}

	public void setAbsBugValue(double absBugValue) {
		this.absBugValue = absBugValue;
	}

	public int getAbsBugStatus() {
		return absBugStatus;
	}

	public void setAbsBugStatus(int absBugStatus) {
		this.absBugStatus = absBugStatus;
	}

	public double getCoolantLevelValue() {
		return coolantLevelValue;
	}

	public void setCoolantLevelValue(double coolantLevelValue) {
		this.coolantLevelValue = coolantLevelValue;
	}

	public int getCoolantLevelStatus() {
		return coolantLevelStatus;
	}

	public void setCoolantLevelStatus(int coolantLevelStatus) {
		this.coolantLevelStatus = coolantLevelStatus;
	}

	public double getAirFilterValue() {
		return airFilterValue;
	}

	public void setAirFilterValue(double airFilterValue) {
		this.airFilterValue = airFilterValue;
	}

	public int getAirFilterStatus() {
		return airFilterStatus;
	}

	public void setAirFilterStatus(int airFilterStatus) {
		this.airFilterStatus = airFilterStatus;
	}

	public double getMwereBlockingValue() {
		return mwereBlockingValue;
	}

	public void setMwereBlockingValue(double mwereBlockingValue) {
		this.mwereBlockingValue = mwereBlockingValue;
	}

	public int getMwereBlockingStatus() {
		return mwereBlockingStatus;
	}

	public void setMwereBlockingStatus(int mwereBlockingStatus) {
		this.mwereBlockingStatus = mwereBlockingStatus;
	}

	public double getFuelBlockingValue() {
		return fuelBlockingValue;
	}

	public void setFuelBlockingValue(double fuelBlockingValue) {
		this.fuelBlockingValue = fuelBlockingValue;
	}

	public int getFuelBlockingStatus() {
		return fuelBlockingStatus;
	}

	public void setFuelBlockingStatus(int fuelBlockingStatus) {
		this.fuelBlockingStatus = fuelBlockingStatus;
	}

	public double getEoilTemperatureValue() {
		return eoilTemperatureValue;
	}

	public void setEoilTemperatureValue(double eoilTemperatureValue) {
		this.eoilTemperatureValue = eoilTemperatureValue;
	}

	public int getEoilTemperatureStatus() {
		return eoilTemperatureStatus;
	}

	public void setEoilTemperatureStatus(int eoilTemperatureStatus) {
		this.eoilTemperatureStatus = eoilTemperatureStatus;
	}

	public double getRetarerHtValue() {
		return retarerHtValue;
	}

	public void setRetarerHtValue(double retarerHtValue) {
		this.retarerHtValue = retarerHtValue;
	}

	public int getRetarerHtStatus() {
		return retarerHtStatus;
	}

	public void setRetarerHtStatus(int retarerHtStatus) {
		this.retarerHtStatus = retarerHtStatus;
	}

	public double getEhousingValue() {
		return ehousingValue;
	}

	public void setEhousingValue(double ehousingValue) {
		this.ehousingValue = ehousingValue;
	}

	public int getEhousingStatus() {
		return ehousingStatus;
	}

	public void setEhousingStatus(int ehousingStatus) {
		this.ehousingStatus = ehousingStatus;
	}

	public double getAirPressureValue() {
		return airPressureValue;
	}

	public void setAirPressureValue(double airPressureValue) {
		this.airPressureValue = airPressureValue;
	}

	public int getAirPressureStatus() {
		return airPressureStatus;
	}

	public void setAirPressureStatus(int airPressureStatus) {
		this.airPressureStatus = airPressureStatus;
	}

	public int getGpsFaultStatus() {
		return gpsFaultStatus;
	}

	public void setGpsFaultStatus(int gpsFaultStatus) {
		this.gpsFaultStatus = gpsFaultStatus;
	}

	public double getGpsFaultValue() {
		return gpsFaultValue;
	}

	public void setGpsFaultValue(double gpsFaultValue) {
		this.gpsFaultValue = gpsFaultValue;
	}

	public int getGpsOpenciruitStatus() {
		return gpsOpenciruitStatus;
	}

	public void setGpsOpenciruitStatus(int gpsOpenciruitStatus) {
		this.gpsOpenciruitStatus = gpsOpenciruitStatus;
	}

	public double getGpsOpenciruitValue() {
		return gpsOpenciruitValue;
	}

	public void setGpsOpenciruitValue(double gpsOpenciruitValue) {
		this.gpsOpenciruitValue = gpsOpenciruitValue;
	}

	public int getGpsShortciruitStatus() {
		return gpsShortciruitStatus;
	}

	public void setGpsShortciruitStatus(int gpsShortciruitStatus) {
		this.gpsShortciruitStatus = gpsShortciruitStatus;
	}

	public double getGpsShortciruitValue() {
		return gpsShortciruitValue;
	}

	public void setGpsShortciruitValue(double gpsShortciruitValue) {
		this.gpsShortciruitValue = gpsShortciruitValue;
	}

	public int getTerminalUnderVoltageStatus() {
		return terminalUnderVoltageStatus;
	}

	public void setTerminalUnderVoltageStatus(int terminalUnderVoltageStatus) {
		this.terminalUnderVoltageStatus = terminalUnderVoltageStatus;
	}

	public double getTerminalUnderVoltageValue() {
		return terminalUnderVoltageValue;
	}

	public void setTerminalUnderVoltageValue(double terminalUnderVoltageValue) {
		this.terminalUnderVoltageValue = terminalUnderVoltageValue;
	}

	public int getTerminalPowerDownStatus() {
		return terminalPowerDownStatus;
	}

	public void setTerminalPowerDownStatus(int terminalPowerDownStatus) {
		this.terminalPowerDownStatus = terminalPowerDownStatus;
	}

	public double getTerminalPowerDownValue() {
		return terminalPowerDownValue;
	}

	public void setTerminalPowerDownValue(double terminalPowerDownValue) {
		this.terminalPowerDownValue = terminalPowerDownValue;
	}

	public int getTerminalScreenfalutStatus() {
		return terminalScreenfalutStatus;
	}

	public void setTerminalScreenfalutStatus(int terminalScreenfalutStatus) {
		this.terminalScreenfalutStatus = terminalScreenfalutStatus;
	}

	public double getTerminalScreenfalutValue() {
		return terminalScreenfalutValue;
	}

	public void setTerminalScreenfalutValue(double terminalScreenfalutValue) {
		this.terminalScreenfalutValue = terminalScreenfalutValue;
	}

	public int getTtsFaultStatus() {
		return ttsFaultStatus;
	}

	public void setTtsFaultStatus(int ttsFaultStatus) {
		this.ttsFaultStatus = ttsFaultStatus;
	}

	public double getTtsFaultValue() {
		return ttsFaultValue;
	}

	public void setTtsFaultValue(double ttsFaultValue) {
		this.ttsFaultValue = ttsFaultValue;
	}

	public int getCameraFaultStatus() {
		return cameraFaultStatus;
	}

	public void setCameraFaultStatus(int cameraFaultStatus) {
		this.cameraFaultStatus = cameraFaultStatus;
	}

	public double getCameraFaultValue() {
		return cameraFaultValue;
	}

	public void setCameraFaultValue(double cameraFaultValue) {
		this.cameraFaultValue = cameraFaultValue;
	}
	
}
