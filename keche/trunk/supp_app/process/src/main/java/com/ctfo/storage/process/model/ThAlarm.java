package com.ctfo.storage.process.model;

/**
 * ThAlarm 报警文件记录
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:46:23
 * 
 */
public class ThAlarm {
	/**	车辆编号	*/
	private String vid;
	/**	组织编号	*/
	private String entId;
	/** 报警编码*/
	private String alarmCode ;
	/** 经度*/
	private long mapLon;
	/** 纬度*/
	private long mapLat;
	/** 原始经度*/
	private long lon ;
	/** 原始纬度*/
	private long lat ;
	/** GPS时间*/
	private long gpsTime ;
	/** GPS 速度*/
	private long gpsSpeed ;
	/** 正北方向夹角*/
	private long direction ;
	/** 累计油耗*/
	private long oilWear ;
	/** 里程*/
	private long mileage ;
	/** 报区域/线路报警*/
	private String lineAlarm ;
	/** 海拔*/
	private long elevation ;
	/** 车速来源*/
	private int speedSource ;
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
	 * 获取原始经度的值
	 * @return lon  
	 */
	public long getLon() {
		return lon;
	}

	/**
	 * 设置原始经度的值
	 * @param lon
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
	 * 获取报区域线路报警的值
	 * @return lineAlarm  
	 */
	public String getLineAlarm() {
		return lineAlarm;
	}

	/**
	 * 设置报区域线路报警的值
	 * @param lineAlarm
	 */
	public void setLineAlarm(String lineAlarm) {
		this.lineAlarm = lineAlarm;
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
	
	
}
