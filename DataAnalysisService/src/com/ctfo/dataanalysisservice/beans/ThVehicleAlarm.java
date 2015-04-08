package com.ctfo.dataanalysisservice.beans;

public class ThVehicleAlarm {

	// 报警附加信息开始点
	private String alarmAddInfoStart;

	// 是否为更新对象 (true为更新对象 false为插入对象)
	private boolean isUpdate;

	public boolean getIsUpdate() {
		return isUpdate;
	}

	public void setIsUpdate(boolean isUpdate) {
		this.isUpdate = isUpdate;
	}
	
	/** 主键，由VID_ALARMCODE_GPSUTC */
	private String alarmId;

	/** 车辆ID */
	private Long vid;

	/** GPS报警时间UTC */
	private Long utc;

	/** 报警位置纬度（单位：1/600000度） */
	private Long lat;

	/** 报警位置经度（单位：1/600000度） */
	private Long lon;

	/** 地图偏移后GPS经度（单位：1/600000度） */
	private Long maplat;

	/** 地图偏移后GPS纬度（单位：1/600000度） */
	private Long maplon;

	/** 海拔(m) */
	private Long elevation;

	/** GPS方向单位：0--359度,正北为0 顺时针 */
	private Long direction;

	/** 报警时刻速度 (单位：1/10千米/小时) */
	private Long gpsSpeed;

	/** 报警代码 */
	private String alarmCode;

	/** 系统时间utc */
	private Long sysutc;

	/** 报警处理状态 0：未处理 1：已处理 */
	private String alarmStatus;

	/** 报警处理人登陆名 */
	private Long alarmHperson;

	/** 报警处理时间utc */
	private Long alarmHtime;

	/** 备注 */
	private String alarmMem;

	/** 报警开始时间UTC */
	private Long alarmStartUtc;

	/** 报警结束时间UTC */
	private Long alarmEndUtc;

	/** 报警处理状态0：未处理1：正在处理2：处理成功3：处理失败 */
	private Long alarmHandlerStatus;

	/** 当班司机 */
	private Long alarmDriver;

	/** 里程(单位：千米) */
	private Long mileage;

	/** 累计油耗 单位：1bit=0.5L 0=0L */
	private Long oilTotal;

	/** 报警位置纬度 */
	private Long endLat;

	/** 报警位置经度 */
	private Long endLon;

	/** 地图偏移后GPS经度 */
	private Long endMaplon;

	/** 地图偏移后GPS纬度 */
	private Long endMaplat;

	/** 海拔(m) */
	private Long endElevation;

	/** GPS方向单位：度,正北为0 */
	private Long endDirection;

	/** 报警时刻速度cm/s->km/h*36/1000 */
	private Long endGpsSpeed;

	/** 里程(单位：千米) */
	private Long endMileage;

	/** 累计油耗 单位：1bit=0.5L 0=0L */
	private Long endOilTotal;

	/** 车牌号 */
	private String vehicleNo;

	/** 报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它） */
	private Short alarmSrc;

	/** 报警处理成功状态0：不作处理1：将来处理2：处理完毕 */
	private Integer alarmHandlerStatusType;

	/** 报警开始时间(查询条件) */
	private Long startUtc;

	/** 报警结束时间(查询条件) */
	private Long endUtc;

	/** 报警级别ID */
	private Integer levelId;

	/** 报警级别名称 */
	private String levelName;

	/** 企业ID */
	private Long pentId;

	/** 企业名称 */
	private String pentName;

	/** 车队ID */
	private Long entId;

	/** 车队名称 */
	private String entName;

	/** 驾驶员ID */
	private Long staffId;

	/** 驾驶员名称 */
	private String staffName;

	/** 报警地点 */
	private String alarmPlace;

	//报警所属大类编码
	private String bgLevel;
	
	public String getBgLevel() {
		return bgLevel;
	}

	public void setBgLevel(String bgLevel) {
		this.bgLevel = bgLevel;
	}
	
	/**
	 * @return the alarmId
	 */
	public String getAlarmId() {
		if(vid!=null && alarmCode!=null && alarmStartUtc!=null)
		return vid+alarmCode+alarmStartUtc;
		else
		return null;
	}

	/**
	 * @param alarmId
	 *            the alarmId to set
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}

	/**
	 * @return the vid
	 */
	public Long getVid() {
		return vid;
	}

	/**
	 * @param vid
	 *            the vid to set
	 */
	public void setVid(Long vid) {
		this.vid = vid;
	}

	/**
	 * @return the utc
	 */
	public Long getUtc() {
		return utc;
	}

	/**
	 * @param utc
	 *            the utc to set
	 */
	public void setUtc(Long utc) {
		this.utc = utc;
	}

	/**
	 * @return the lat
	 */
	public Long getLat() {
		return lat;
	}

	/**
	 * @param lat
	 *            the lat to set
	 */
	public void setLat(Long lat) {
		this.lat = lat;
	}

	/**
	 * @return the lon
	 */
	public Long getLon() {
		return lon;
	}

	/**
	 * @param lon
	 *            the lon to set
	 */
	public void setLon(Long lon) {
		this.lon = lon;
	}

	/**
	 * @return the maplat
	 */
	public Long getMaplat() {
		return maplat;
	}

	/**
	 * @param maplat
	 *            the maplat to set
	 */
	public void setMaplat(Long maplat) {
		this.maplat = maplat;
	}

	/**
	 * @return the maplon
	 */
	public Long getMaplon() {
		return maplon;
	}

	/**
	 * @param maplon
	 *            the maplon to set
	 */
	public void setMaplon(Long maplon) {
		this.maplon = maplon;
	}

	/**
	 * @return the elevation
	 */
	public Long getElevation() {
		return elevation;
	}

	/**
	 * @param elevation
	 *            the elevation to set
	 */
	public void setElevation(Long elevation) {
		this.elevation = elevation;
	}

	/**
	 * @return the direction
	 */
	public Long getDirection() {
		return direction;
	}

	/**
	 * @param direction
	 *            the direction to set
	 */
	public void setDirection(Long direction) {
		this.direction = direction;
	}

	/**
	 * @return the gpsSpeed
	 */
	public Long getGpsSpeed() {
		return gpsSpeed;
	}

	/**
	 * @param gpsSpeed
	 *            the gpsSpeed to set
	 */
	public void setGpsSpeed(Long gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	/**
	 * @return the alarmCode
	 */
	public String getAlarmCode() {
		return alarmCode;
	}

	/**
	 * @param alarmCode
	 *            the alarmCode to set
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}

	/**
	 * @return the sysutc
	 */
	public Long getSysutc() {
		return sysutc;
	}

	/**
	 * @param sysutc
	 *            the sysutc to set
	 */
	public void setSysutc(Long sysutc) {
		this.sysutc = sysutc;
	}

	/**
	 * @return the alarmStatus
	 */
	public String getAlarmStatus() {
		return alarmStatus;
	}

	/**
	 * @param alarmStatus
	 *            the alarmStatus to set
	 */
	public void setAlarmStatus(String alarmStatus) {
		this.alarmStatus = alarmStatus;
	}

	/**
	 * @return the alarmHperson
	 */
	public Long getAlarmHperson() {
		return alarmHperson;
	}

	/**
	 * @param alarmHperson
	 *            the alarmHperson to set
	 */
	public void setAlarmHperson(Long alarmHperson) {
		this.alarmHperson = alarmHperson;
	}

	/**
	 * @return the alarmHtime
	 */
	public Long getAlarmHtime() {
		return alarmHtime;
	}

	/**
	 * @param alarmHtime
	 *            the alarmHtime to set
	 */
	public void setAlarmHtime(Long alarmHtime) {
		this.alarmHtime = alarmHtime;
	}

	/**
	 * @return the alarmMem
	 */
	public String getAlarmMem() {
		return alarmMem;
	}

	/**
	 * @param alarmMem
	 *            the alarmMem to set
	 */
	public void setAlarmMem(String alarmMem) {
		this.alarmMem = alarmMem;
	}

	/**
	 * @return the alarmStartUtc
	 */
	public Long getAlarmStartUtc() {
		return alarmStartUtc;
	}

	/**
	 * @param alarmStartUtc
	 *            the alarmStartUtc to set
	 */
	public void setAlarmStartUtc(Long alarmStartUtc) {
		this.alarmStartUtc = alarmStartUtc;
	}

	/**
	 * @return the alarmEndUtc
	 */
	public Long getAlarmEndUtc() {
		return alarmEndUtc;
	}

	/**
	 * @param alarmEndUtc
	 *            the alarmEndUtc to set
	 */
	public void setAlarmEndUtc(Long alarmEndUtc) {
		this.alarmEndUtc = alarmEndUtc;
	}

	/**
	 * @return the alarmHandlerStatus
	 */
	public Long getAlarmHandlerStatus() {
		return alarmHandlerStatus;
	}

	/**
	 * @param alarmHandlerStatus
	 *            the alarmHandlerStatus to set
	 */
	public void setAlarmHandlerStatus(Long alarmHandlerStatus) {
		this.alarmHandlerStatus = alarmHandlerStatus;
	}

	/**
	 * @return the alarmDriver
	 */
	public Long getAlarmDriver() {
		return alarmDriver;
	}

	/**
	 * @param alarmDriver
	 *            the alarmDriver to set
	 */
	public void setAlarmDriver(Long alarmDriver) {
		this.alarmDriver = alarmDriver;
	}

	/**
	 * @return the mileage
	 */
	public Long getMileage() {
		return mileage;
	}

	/**
	 * @param mileage
	 *            the mileage to set
	 */
	public void setMileage(Long mileage) {
		this.mileage = mileage;
	}

	/**
	 * @return the oilTotal
	 */
	public Long getOilTotal() {
		return oilTotal;
	}

	/**
	 * @param oilTotal
	 *            the oilTotal to set
	 */
	public void setOilTotal(Long oilTotal) {
		this.oilTotal = oilTotal;
	}

	/**
	 * @return the endLat
	 */
	public Long getEndLat() {
		return endLat;
	}

	/**
	 * @param endLat
	 *            the endLat to set
	 */
	public void setEndLat(Long endLat) {
		this.endLat = endLat;
	}

	/**
	 * @return the endLon
	 */
	public Long getEndLon() {
		return endLon;
	}

	/**
	 * @param endLon
	 *            the endLon to set
	 */
	public void setEndLon(Long endLon) {
		this.endLon = endLon;
	}

	/**
	 * @return the endMaplon
	 */
	public Long getEndMaplon() {
		return endMaplon;
	}

	/**
	 * @param endMaplon
	 *            the endMaplon to set
	 */
	public void setEndMaplon(Long endMaplon) {
		this.endMaplon = endMaplon;
	}

	/**
	 * @return the endMaplat
	 */
	public Long getEndMaplat() {
		return endMaplat;
	}

	/**
	 * @param endMaplat
	 *            the endMaplat to set
	 */
	public void setEndMaplat(Long endMaplat) {
		this.endMaplat = endMaplat;
	}

	/**
	 * @return the endElevation
	 */
	public Long getEndElevation() {
		return endElevation;
	}

	/**
	 * @param endElevation
	 *            the endElevation to set
	 */
	public void setEndElevation(Long endElevation) {
		this.endElevation = endElevation;
	}

	/**
	 * @return the endDirection
	 */
	public Long getEndDirection() {
		return endDirection;
	}

	/**
	 * @param endDirection
	 *            the endDirection to set
	 */
	public void setEndDirection(Long endDirection) {
		this.endDirection = endDirection;
	}

	/**
	 * @return the endGpsSpeed
	 */
	public Long getEndGpsSpeed() {
		return endGpsSpeed;
	}

	/**
	 * @param endGpsSpeed
	 *            the endGpsSpeed to set
	 */
	public void setEndGpsSpeed(Long endGpsSpeed) {
		this.endGpsSpeed = endGpsSpeed;
	}

	/**
	 * @return the endMileage
	 */
	public Long getEndMileage() {
		return endMileage;
	}

	/**
	 * @param endMileage
	 *            the endMileage to set
	 */
	public void setEndMileage(Long endMileage) {
		this.endMileage = endMileage;
	}

	/**
	 * @return the endOilTotal
	 */
	public Long getEndOilTotal() {
		return endOilTotal;
	}

	/**
	 * @param endOilTotal
	 *            the endOilTotal to set
	 */
	public void setEndOilTotal(Long endOilTotal) {
		this.endOilTotal = endOilTotal;
	}

	/**
	 * @return the vehicleNo
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}

	/**
	 * @param vehicleNo
	 *            the vehicleNo to set
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	/**
	 * @return the alarmSrc
	 */
	public Short getAlarmSrc() {
		return alarmSrc;
	}

	/**
	 * @param alarmSrc
	 *            the alarmSrc to set
	 */
	public void setAlarmSrc(Short alarmSrc) {
		this.alarmSrc = alarmSrc;
	}

	/**
	 * @return the alarmHandlerStatusType
	 */
	public Integer getAlarmHandlerStatusType() {
		return alarmHandlerStatusType;
	}

	/**
	 * @param alarmHandlerStatusType
	 *            the alarmHandlerStatusType to set
	 */
	public void setAlarmHandlerStatusType(Integer alarmHandlerStatusType) {
		this.alarmHandlerStatusType = alarmHandlerStatusType;
	}

	/**
	 * @return the startUtc
	 */
	public Long getStartUtc() {
		return startUtc;
	}

	/**
	 * @param startUtc
	 *            the startUtc to set
	 */
	public void setStartUtc(Long startUtc) {
		this.startUtc = startUtc;
	}

	/**
	 * @return the endUtc
	 */
	public Long getEndUtc() {
		return endUtc;
	}

	/**
	 * @param endUtc
	 *            the endUtc to set
	 */
	public void setEndUtc(Long endUtc) {
		this.endUtc = endUtc;
	}

	/**
	 * @return the levelId
	 */
	public Integer getLevelId() {
		return levelId;
	}

	/**
	 * @param levelId
	 *            the levelId to set
	 */
	public void setLevelId(Integer levelId) {
		this.levelId = levelId;
	}

	/**
	 * @return the levelName
	 */
	public String getLevelName() {
		return levelName;
	}

	/**
	 * @param levelName
	 *            the levelName to set
	 */
	public void setLevelName(String levelName) {
		this.levelName = levelName;
	}

	/**
	 * @return the pentId
	 */
	public Long getPentId() {
		return pentId;
	}

	/**
	 * @param pentId
	 *            the pentId to set
	 */
	public void setPentId(Long pentId) {
		this.pentId = pentId;
	}

	/**
	 * @return the pentName
	 */
	public String getPentName() {
		return pentName;
	}

	/**
	 * @param pentName
	 *            the pentName to set
	 */
	public void setPentName(String pentName) {
		this.pentName = pentName;
	}

	/**
	 * @return the entId
	 */
	public Long getEntId() {
		return entId;
	}

	/**
	 * @param entId
	 *            the entId to set
	 */
	public void setEntId(Long entId) {
		this.entId = entId;
	}

	/**
	 * @return the entName
	 */
	public String getEntName() {
		return entName;
	}

	/**
	 * @param entName
	 *            the entName to set
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}

	/**
	 * @return the staffId
	 */
	public Long getStaffId() {
		return staffId;
	}

	/**
	 * @param staffId
	 *            the staffId to set
	 */
	public void setStaffId(Long staffId) {
		this.staffId = staffId;
	}

	/**
	 * @return the staffName
	 */
	public String getStaffName() {
		return staffName;
	}

	/**
	 * @param staffName
	 *            the staffName to set
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}

	/**
	 * @return the alarmPlace
	 */
	public String getAlarmPlace() {
		return alarmPlace;
	}

	/**
	 * @param alarmPlace
	 *            the alarmPlace to set
	 */
	public void setAlarmPlace(String alarmPlace) {
		this.alarmPlace = alarmPlace;
	}

	public String getAlarmAddInfoStart() {
		return alarmAddInfoStart;
	}

	public void setAlarmAddInfoStart(String alarmAddInfoStart) {
		this.alarmAddInfoStart = alarmAddInfoStart;
	}

}
