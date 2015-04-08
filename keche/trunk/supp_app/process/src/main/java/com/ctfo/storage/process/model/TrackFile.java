package com.ctfo.storage.process.model;

/**
 * ThTrack 轨迹文件记录
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:46:02
 * 
 */
public class TrackFile {
	/**	车辆编号	*/
	private String vid;
	/**	组织编号	*/
	private String entId;
	/** 经度*/
	private long mapLon ;
	/** 纬度*/
	private long mapLat ;
	/** GPS时间*/
	private long gpsTime ;
	/** GPS 速度*/
	private long gpsSpeed ;
	/** 正北方向夹角*/
	private long direction ;
	/** 车辆状态*/
	private String status ;
	/** 报警编码*/
	private String alarmCode ;
	/** 原始经度*/
	private long lon ;
	/** 原始纬度*/
	private long lat ;
	/** 海拔*/
	private long elevation ;
	/** 里程*/
	private long mileage ;
	/** 累计油耗*/
	private long oilWear ;
	/** 发动机运行总时长*/
	private long engineWorkinglong ;
	/** 引擎转速（发动机转速）*/
	private long engineRotateSpeed ;
	/** 位置基本信息状态位*/
	private String baseStatus ;
	/** 区域/线路报警附加信息*/
	private String addStatus ;
	/** 冷却液温度*/
	private long eWater;
	/** 蓄电池电压*/
	private int batteryVoltage ;
	/** 瞬时油耗*/
	private long oilInstant ;
	/** 行驶记录仪速度(km/h)*/
	private int vssSpeed ;
	/** 机油压力 (20 COL),*/
	private long oilPressure ;
	/** 大气压力*/
	private long airPressure ;
	/** 发动机扭矩百分比，1bit=1%，0=-125%*/
	private long eTorque ;
	/** 车辆信号状态*/
	private String signStatus ;
	/** 车速来源*/
	private int speedSource ;
	/** 油量*/
	private long oilMass ;
	/** 超速报警附加信息*/
	private String addOverSpeed ;
	/** 路线行驶时间不足/过长*/
	private String outRouteST ;
	/** 油门踏板位置*/
	private int eecApp;
	/** 终端内置电池电压*/
	private int innerBatteryVoltage ;
	/** 发动机水温*/
	private long engineTemperature ;
	/** 机油温度*/
	private long oilTemperature ;
	/** 进气温度*/
	private long airInflowTpr ;
	/** 开门状态*/
	private int openStatus ;
	/** 需要人工确认报警事件的ID*/
	private int alarmConfirmId ;
	/** 计量仪油耗，1bit=0.01L,0=0L */
	private long fuelMeter ;
	/**	扩展车辆信号状态	*/
	private String expandStatusFlag;
	/** 系统时间*/
	private long sysUtc ;

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
	 * 获取经度的值
	 * @return mapLon  
	 */
	public long getMapLon() {
		return mapLon;
	}
	/**
	 * 设置经度的值
	 * @param mapLon
	 */
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}

	/**
	 * 获取纬度的值
	 * @return mapLat  
	 */
	public long getMapLat() {
		return mapLat;
	}

	/**
	 * 设置纬度的值
	 * @param mapLat
	 */
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}

	/**
	 * 获取GPS时间的值
	 * @return gpsTime  
	 */
	public long getGpsTime() {
		return gpsTime;
	}

	/**
	 * 设置GPS时间的值
	 * @param gpsTime
	 */
	public void setGpsTime(long gpsTime) {
		this.gpsTime = gpsTime;
	}

	/**
	 * 获取GPS速度的值
	 * @return gpsSpeed  
	 */
	public long getGpsSpeed() {
		return gpsSpeed;
	}

	/**
	 * 设置GPS速度的值
	 * @param gpsSpeed
	 */
	public void setGpsSpeed(long gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	/**
	 * 获取正北方向夹角的值
	 * @return direction  
	 */
	public long getDirection() {
		return direction;
	}

	/**
	 * 设置正北方向夹角的值
	 * @param direction
	 */
	public void setDirection(long direction) {
		this.direction = direction;
	}

	/**
	 * 获取车辆状态的值
	 * @return status  
	 */
	public String getStatus() {
		return status;
	}

	/**
	 * 设置车辆状态的值
	 * @param status
	 */
	public void setStatus(String status) {
		this.status = status;
	}

	/**
	 * 获取报警编码的值
	 * @return alarmCode  
	 */
	public String getAlarmCode() {
		return alarmCode;
	}

	/**
	 * 设置报警编码的值
	 * @param alarmCode
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	/**
	 * @return 获取 原始经度
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * 设置原始经度
	 * @param lon 原始经度 
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * 获取原始纬度的值
	 * @return lat  
	 */
	public long getLat() {
		return lat;
	}

	/**
	 * 设置原始纬度的值
	 * @param lat
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}

	/**
	 * 获取海拔的值
	 * @return elevation  
	 */
	public long getElevation() {
		return elevation;
	}

	/**
	 * 设置海拔的值
	 * @param elevation
	 */
	public void setElevation(long elevation) {
		this.elevation = elevation;
	}

	/**
	 * 获取里程的值
	 * @return mileage  
	 */
	public long getMileage() {
		return mileage;
	}

	/**
	 * 设置里程的值
	 * @param mileage
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	/**
	 * 获取累计油耗的值
	 * @return oilWear  
	 */
	public long getOilWear() {
		return oilWear;
	}

	/**
	 * 设置累计油耗的值
	 * @param oilWear
	 */
	public void setOilWear(long oilWear) {
		this.oilWear = oilWear;
	}

	/**
	 * 获取发动机运行总时长的值
	 * @return engineWorkinglong  
	 */
	public long getEngineWorkinglong() {
		return engineWorkinglong;
	}

	/**
	 * 设置发动机运行总时长的值
	 * @param engineWorkinglong
	 */
	public void setEngineWorkinglong(long engineWorkinglong) {
		this.engineWorkinglong = engineWorkinglong;
	}

	/**
	 * 获取引擎转速（发动机转速）的值
	 * @return engineRotateSpeed  
	 */
	public long getEngineRotateSpeed() {
		return engineRotateSpeed;
	}

	/**
	 * 设置引擎转速（发动机转速）的值
	 * @param engineRotateSpeed
	 */
	public void setEngineRotateSpeed(long engineRotateSpeed) {
		this.engineRotateSpeed = engineRotateSpeed;
	}

	/**
	 * 获取位置基本信息状态位的值
	 * @return baseStatus  
	 */
	public String getBaseStatus() {
		return baseStatus;
	}

	/**
	 * 设置位置基本信息状态位的值
	 * @param baseStatus
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	/**
	 * 获取区域线路报警附加信息的值
	 * @return addStatus  
	 */
	public String getAddStatus() {
		return addStatus;
	}

	/**
	 * 设置区域线路报警附加信息的值
	 * @param addStatus
	 */
	public void setAddStatus(String addStatus) {
		this.addStatus = addStatus;
	}

	/**
	 * 获取冷却液温度的值
	 * @return eWater  
	 */
	public long geteWater() {
		return eWater;
	}

	/**
	 * 设置冷却液温度的值
	 * @param eWater
	 */
	public void seteWater(long eWater) {
		this.eWater = eWater;
	}

	/**
	 * 获取蓄电池电压的值
	 * @return batteryVoltage  
	 */
	public int getBatteryVoltage() {
		return batteryVoltage;
	}

	/**
	 * 设置蓄电池电压的值
	 * @param batteryVoltage
	 */
	public void setBatteryVoltage(int batteryVoltage) {
		this.batteryVoltage = batteryVoltage;
	}

	/**
	 * 获取瞬时油耗的值
	 * @return oilInstant  
	 */
	public long getOilInstant() {
		return oilInstant;
	}

	/**
	 * 设置瞬时油耗的值
	 * @param oilInstant
	 */
	public void setOilInstant(long oilInstant) {
		this.oilInstant = oilInstant;
	}

	/**
	 * 获取行驶记录仪速度(kmh)的值
	 * @return vssSpeed  
	 */
	public int getVssSpeed() {
		return vssSpeed;
	}

	/**
	 * 设置行驶记录仪速度(kmh)的值
	 * @param vssSpeed
	 */
	public void setVssSpeed(int vssSpeed) {
		this.vssSpeed = vssSpeed;
	}

	/**
	 * 获取机油压力(20COL)的值
	 * @return oilPressure  
	 */
	public long getOilPressure() {
		return oilPressure;
	}

	/**
	 * 设置机油压力(20COL)的值
	 * @param oilPressure
	 */
	public void setOilPressure(long oilPressure) {
		this.oilPressure = oilPressure;
	}

	/**
	 * 获取大气压力的值
	 * @return airPressure  
	 */
	public long getAirPressure() {
		return airPressure;
	}

	/**
	 * 设置大气压力的值
	 * @param airPressure
	 */
	public void setAirPressure(long airPressure) {
		this.airPressure = airPressure;
	}

	/**
	 * 获取发动机扭矩百分比，1bit=1%，0=-125%的值
	 * @return eTorque  
	 */
	public long geteTorque() {
		return eTorque;
	}

	/**
	 * 设置发动机扭矩百分比，1bit=1%，0=-125%的值
	 * @param eTorque
	 */
	public void seteTorque(long eTorque) {
		this.eTorque = eTorque;
	}

	/**
	 * 获取车辆信号状态的值
	 * @return signStatus  
	 */
	public String getSignStatus() {
		return signStatus;
	}

	/**
	 * 设置车辆信号状态的值
	 * @param signStatus
	 */
	public void setSignStatus(String signStatus) {
		this.signStatus = signStatus;
	}

	/**
	 * 获取车速来源的值
	 * @return speedSource  
	 */
	public int getSpeedSource() {
		return speedSource;
	}

	/**
	 * 设置车速来源的值
	 * @param speedSource
	 */
	public void setSpeedSource(int speedSource) {
		this.speedSource = speedSource;
	}

	/**
	 * 获取油量的值
	 * @return oilMass  
	 */
	public long getOilMass() {
		return oilMass;
	}

	/**
	 * 设置油量的值
	 * @param oilMass
	 */
	public void setOilMass(long oilMass) {
		this.oilMass = oilMass;
	}

	/**
	 * 获取超速报警附加信息的值
	 * @return addOverSpeed  
	 */
	public String getAddOverSpeed() {
		return addOverSpeed;
	}

	/**
	 * 设置超速报警附加信息的值
	 * @param addOverSpeed
	 */
	public void setAddOverSpeed(String addOverSpeed) {
		this.addOverSpeed = addOverSpeed;
	}

	/**
	 * 获取路线行驶时间不足过长的值
	 * @return outRouteST  
	 */
	public String getOutRouteST() {
		return outRouteST;
	}

	/**
	 * 设置路线行驶时间不足过长的值
	 * @param outRouteST
	 */
	public void setOutRouteST(String outRouteST) {
		this.outRouteST = outRouteST;
	}

	/**
	 * 获取油门踏板位置的值
	 * @return eecApp  
	 */
	public int getEecApp() {
		return eecApp;
	}

	/**
	 * 设置油门踏板位置的值
	 * @param eecApp
	 */
	public void setEecApp(int eecApp) {
		this.eecApp = eecApp;
	}

	/**
	 * 获取终端内置电池电压的值
	 * @return innerBatteryVoltage  
	 */
	public int getInnerBatteryVoltage() {
		return innerBatteryVoltage;
	}

	/**
	 * 设置终端内置电池电压的值
	 * @param innerBatteryVoltage
	 */
	public void setInnerBatteryVoltage(int innerBatteryVoltage) {
		this.innerBatteryVoltage = innerBatteryVoltage;
	}

	/**
	 * 获取发动机水温的值
	 * @return engineTemperature  
	 */
	public long getEngineTemperature() {
		return engineTemperature;
	}

	/**
	 * 设置发动机水温的值
	 * @param engineTemperature
	 */
	public void setEngineTemperature(long engineTemperature) {
		this.engineTemperature = engineTemperature;
	}

	/**
	 * 获取机油温度的值
	 * @return oilTemperature  
	 */
	public long getOilTemperature() {
		return oilTemperature;
	}

	/**
	 * 设置机油温度的值
	 * @param oilTemperature
	 */
	public void setOilTemperature(long oilTemperature) {
		this.oilTemperature = oilTemperature;
	}

	/**
	 * 获取进气温度的值
	 * @return airInflowTpr  
	 */
	public long getAirInflowTpr() {
		return airInflowTpr;
	}

	/**
	 * 设置进气温度的值
	 * @param airInflowTpr
	 */
	public void setAirInflowTpr(long airInflowTpr) {
		this.airInflowTpr = airInflowTpr;
	}

	/**
	 * 获取开门状态的值
	 * @return openStatus  
	 */
	public int getOpenStatus() {
		return openStatus;
	}

	/**
	 * 设置开门状态的值
	 * @param openStatus
	 */
	public void setOpenStatus(int openStatus) {
		this.openStatus = openStatus;
	}

	/**
	 * 获取需要人工确认报警事件的ID的值
	 * @return alarmConfirmId  
	 */
	public int getAlarmConfirmId() {
		return alarmConfirmId;
	}

	/**
	 * 设置需要人工确认报警事件的ID的值
	 * @param alarmConfirmId
	 */
	public void setAlarmConfirmId(int alarmConfirmId) {
		this.alarmConfirmId = alarmConfirmId;
	}

	/**
	 * 获取计量仪油耗，1bit=0.01L0=0L的值
	 * @return fuelMeter  
	 */
	public long getFuelMeter() {
		return fuelMeter;
	}

	/**
	 * 设置计量仪油耗，1bit=0.01L0=0L的值
	 * @param fuelMeter
	 */
	public void setFuelMeter(long fuelMeter) {
		this.fuelMeter = fuelMeter;
	}

	/**
	 * 获取系统时间的值
	 * @return sysUtc  
	 */
	public long getSysUtc() {
		return sysUtc;
	}

	/**
	 * 设置系统时间的值
	 * @param sysUtc
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
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
	
	
	
}
