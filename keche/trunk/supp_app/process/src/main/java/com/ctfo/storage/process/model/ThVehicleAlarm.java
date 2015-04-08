package com.ctfo.storage.process.model;

/**
 * ThVehicleAlarm 车辆报警历史表
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:45:17
 * 
 */
public class ThVehicleAlarm {
	/** 告警编号*/
	private String alarmId ;
	/** 车辆ID*/
	private String vid ;
	
	/** GPS报警时间*/
	private long utc ;
	
	/** 报警位置纬度（单位：1/600000度）*/
	private long lat ;
	
	/** 报警位置经度（单位：1/600000度）*/
	private long lon ;
	
	/** 地图偏移后GPS经度*/
	private long mapLat ;
	
	/** 地图偏移后GPS纬度*/
	private long mapLon ;
	
	/** 海拔(m)*/
	private int elevation ;
	
	/** GPS方向*/
	private int direction ;
	
	/** 报警时刻速度*/
	private int gpsSpeed ;
	
	/** 报警代码*/
	private String alarmCode ;
	
	/** 系统时间utc*/
	private long sysUtc ;
	
	/** 报警处理状态0：未处理1：已处理*/
	private int alarmStatus ;
	
	/** 报警处理人登陆名*/
	private String alarmHperson  ;
	
	/** 报警处理时间utc*/
	private long alarmHtime ;
	
	/** 备注*/
	private long alarmMem ;
	
	/** 报警开始时间UTC*/
	private long alarmStartUtc ;
	
	/** 报警结束时间UTC*/
	private long alarmEndUtc ;
	
	/** 报警处理状态0：未处理1：正在处理2：处理成功3：处理失败*/
	private int alarmHandlerStatus ;
	
	/** 当班司机*/
	private String alarmDriver ;
	
	/** 里程(单位：千米)*/
	private long mileage ;
	
	/** 累计油耗(1bit=0.5L 0=0L)*/
	private long oilTotal;
	
	/** 报警位置纬度*/
	private long endLat ;
	
	/** 报警位置经度*/
	private long endLon ;
	
	/** 地图偏移后GPS经度，*/
	private long endMaplon ;
	
	/** 地图偏移后GPS纬度*/
	private long endMaplat ;
	
	/** 海拔(m)*/
	private long endElevation ;
	
	/** GPS方向*/
	private long endDirection ;
	
	/** 报警时刻速度*/
	private long endGpsSpeed ;
	
	/** 里程(单位：千米)*/
	private long endMileage ;
	
	/** 累计油耗*/
	private long endOilTotal ;
	
	/** 车牌号*/
	private String vehicleNo ;
	
	/** 督办状态：0：未督办 1：内部督办 2：监管平台督办*/
	private int alarmTodo ;
	
	/** 报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）*/
	private int alarmSrc ;
	
	/** 报警处理成功状态-1:未处理0：不作处理1：将来处理2：处理完毕*/
	private int alarmHandlerStatusType ;
	
	/** 报警所属大类编码*/
	private String bgLevel ;
	
	/** 信号状态*/
	private int signStatus ;
	
	/** 扩展状态*/
	private String extendStatus ;
	
	/** 基本状态*/
	private String baseStatus ;
	
	/** 报警开始附加信息*/
	private String alarmAddInfoStart ;
	
	/** 报警结束附加信息*/
	private String alarmAddInfoEnd ;
	
	/** 超速报警开始点附加信息*/
	private String overspeedInfoST ;
	
	/** 进出区域/路段报警开始点附加信息*/
	private String enteringAreaST ;
	
	/** 路线行驶时间不足/过长开始点附加信息*/
	private String outRouteST ;
	
	/** 超速报警结束点附加信息*/
	private String overspeedInfoED ;
	
	/** 进出区域/路段报警结束点附加信息*/
	private String enteringAreaED ;
	
	/** 路线行驶时间不足/过长结束点附加信息*/
	private String outRouteED ;
	
	/** 报警具体描述*/
	private String alarmAddInfo ;
	
	/** 围栏或线路ID*/
	private String areaId ;
	
	/** 最大转速*/
	private String maxRpm ;
	
	/** 速度阀值，超速告警使用*/
	private String speedThreshOld ;
	
	/** 告警处理方式 1:平台下发信息 2:平台拍照 3:平台解除*/
	private int handlerMethods ;
	
	/** 最大速度 */
	private long maxSpeed ;
	
	/** 平均速度*/
	private long avgSpeed;
	
	/** 车队ID*/
	private String teamId ;
	
	/** 车队名称*/
	private String teamName ;
	
	/** 组织ID*/
	private String corpId ;
	
	/** 组织名称*/
	private String corpName ;

	/**
	 * @return 获取 告警编号
	 */
	public String getAlarmId() {
		return alarmId;
	}

	/**
	 * 设置告警编号
	 * @param alarmId 告警编号 
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}

	/**
	 * 获取车辆ID的值
	 * @return vid  
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置车辆ID的值
	 * @param vid
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获取GPS报警时间的值
	 * @return utc  
	 */
	public long getUtc() {
		return utc;
	}

	/**
	 * 设置GPS报警时间的值
	 * @param utc
	 */
	public void setUtc(long utc) {
		this.utc = utc;
	}

	/**
	 * 获取报警位置纬度（单位：1600000度）的值
	 * @return lat  
	 */
	public long getLat() {
		return lat;
	}

	/**
	 * 设置报警位置纬度（单位：1600000度）的值
	 * @param lat
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}

	/**
	 * 获取报警位置经度（单位：1600000度）的值
	 * @return lon  
	 */
	public long getLon() {
		return lon;
	}

	/**
	 * 设置报警位置经度（单位：1600000度）的值
	 * @param lon
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}

	/**
	 * 获取地图偏移后GPS经度的值
	 * @return mapLat  
	 */
	public long getMapLat() {
		return mapLat;
	}

	/**
	 * 设置地图偏移后GPS经度的值
	 * @param mapLat
	 */
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}

	/**
	 * 获取地图偏移后GPS纬度的值
	 * @return mapLon  
	 */
	public long getMapLon() {
		return mapLon;
	}

	/**
	 * 设置地图偏移后GPS纬度的值
	 * @param mapLon
	 */
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}

	/**
	 * 获取海拔(m)的值
	 * @return elevation  
	 */
	public int getElevation() {
		return elevation;
	}

	/**
	 * 设置海拔(m)的值
	 * @param elevation
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}

	/**
	 * 获取GPS方向的值
	 * @return direction  
	 */
	public int getDirection() {
		return direction;
	}

	/**
	 * 设置GPS方向的值
	 * @param direction
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}

	/**
	 * 获取报警时刻速度的值
	 * @return gpsSpeed  
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}

	/**
	 * 设置报警时刻速度的值
	 * @param gpsSpeed
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	/**
	 * 获取报警代码的值
	 * @return alarmCode  
	 */
	public String getAlarmCode() {
		return alarmCode;
	}

	/**
	 * 设置报警代码的值
	 * @param alarmCode
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}

	/**
	 * 获取系统时间utc的值
	 * @return sysUtc  
	 */
	public long getSysUtc() {
		return sysUtc;
	}

	/**
	 * 设置系统时间utc的值
	 * @param sysUtc
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}

	/**
	 * 获取报警处理状态0：未处理1：已处理的值
	 * @return alarmStatus  
	 */
	public int getAlarmStatus() {
		return alarmStatus;
	}

	/**
	 * 设置报警处理状态0：未处理1：已处理的值
	 * @param alarmStatus
	 */
	public void setAlarmStatus(int alarmStatus) {
		this.alarmStatus = alarmStatus;
	}

	/**
	 * 获取报警处理人登陆名的值
	 * @return alarmHperson  
	 */
	public String getAlarmHperson() {
		return alarmHperson;
	}

	/**
	 * 设置报警处理人登陆名的值
	 * @param alarmHperson
	 */
	public void setAlarmHperson(String alarmHperson) {
		this.alarmHperson = alarmHperson;
	}

	/**
	 * 获取报警处理时间utc的值
	 * @return alarmHtime  
	 */
	public long getAlarmHtime() {
		return alarmHtime;
	}

	/**
	 * 设置报警处理时间utc的值
	 * @param alarmHtime
	 */
	public void setAlarmHtime(long alarmHtime) {
		this.alarmHtime = alarmHtime;
	}

	/**
	 * 获取备注的值
	 * @return alarmMem  
	 */
	public long getAlarmMem() {
		return alarmMem;
	}

	/**
	 * 设置备注的值
	 * @param alarmMem
	 */
	public void setAlarmMem(long alarmMem) {
		this.alarmMem = alarmMem;
	}

	/**
	 * 获取报警开始时间UTC的值
	 * @return alarmStartUtc  
	 */
	public long getAlarmStartUtc() {
		return alarmStartUtc;
	}

	/**
	 * 设置报警开始时间UTC的值
	 * @param alarmStartUtc
	 */
	public void setAlarmStartUtc(long alarmStartUtc) {
		this.alarmStartUtc = alarmStartUtc;
	}

	/**
	 * 获取报警结束时间UTC的值
	 * @return alarmEndUtc  
	 */
	public long getAlarmEndUtc() {
		return alarmEndUtc;
	}

	/**
	 * 设置报警结束时间UTC的值
	 * @param alarmEndUtc
	 */
	public void setAlarmEndUtc(long alarmEndUtc) {
		this.alarmEndUtc = alarmEndUtc;
	}

	/**
	 * 获取报警处理状态0：未处理1：正在处理2：处理成功3：处理失败的值
	 * @return alarmHandlerStatus  
	 */
	public int getAlarmHandlerStatus() {
		return alarmHandlerStatus;
	}

	/**
	 * 设置报警处理状态0：未处理1：正在处理2：处理成功3：处理失败的值
	 * @param alarmHandlerStatus
	 */
	public void setAlarmHandlerStatus(int alarmHandlerStatus) {
		this.alarmHandlerStatus = alarmHandlerStatus;
	}

	/**
	 * 获取当班司机的值
	 * @return alarmDriver  
	 */
	public String getAlarmDriver() {
		return alarmDriver;
	}

	/**
	 * 设置当班司机的值
	 * @param alarmDriver
	 */
	public void setAlarmDriver(String alarmDriver) {
		this.alarmDriver = alarmDriver;
	}

	/**
	 * 获取里程(单位：千米)的值
	 * @return mileage  
	 */
	public long getMileage() {
		return mileage;
	}

	/**
	 * 设置里程(单位：千米)的值
	 * @param mileage
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	/**
	 * 获取累计油耗(1bit=0.5L0=0L)的值
	 * @return oilTotal  
	 */
	public long getOilTotal() {
		return oilTotal;
	}

	/**
	 * 设置累计油耗(1bit=0.5L0=0L)的值
	 * @param oilTotal
	 */
	public void setOilTotal(long oilTotal) {
		this.oilTotal = oilTotal;
	}

	/**
	 * 获取报警位置纬度的值
	 * @return endLat  
	 */
	public long getEndLat() {
		return endLat;
	}

	/**
	 * 设置报警位置纬度的值
	 * @param endLat
	 */
	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}

	/**
	 * 获取报警位置经度的值
	 * @return endLon  
	 */
	public long getEndLon() {
		return endLon;
	}

	/**
	 * 设置报警位置经度的值
	 * @param endLon
	 */
	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}

	/**
	 * 获取地图偏移后GPS经度，的值
	 * @return endMaplon  
	 */
	public long getEndMaplon() {
		return endMaplon;
	}

	/**
	 * 设置地图偏移后GPS经度，的值
	 * @param endMaplon
	 */
	public void setEndMaplon(long endMaplon) {
		this.endMaplon = endMaplon;
	}

	/**
	 * 获取地图偏移后GPS纬度的值
	 * @return endMaplat  
	 */
	public long getEndMaplat() {
		return endMaplat;
	}

	/**
	 * 设置地图偏移后GPS纬度的值
	 * @param endMaplat
	 */
	public void setEndMaplat(long endMaplat) {
		this.endMaplat = endMaplat;
	}

	/**
	 * 获取海拔(m)的值
	 * @return endElevation  
	 */
	public long getEndElevation() {
		return endElevation;
	}

	/**
	 * 设置海拔(m)的值
	 * @param endElevation
	 */
	public void setEndElevation(long endElevation) {
		this.endElevation = endElevation;
	}

	/**
	 * 获取GPS方向的值
	 * @return endDirection  
	 */
	public long getEndDirection() {
		return endDirection;
	}

	/**
	 * 设置GPS方向的值
	 * @param endDirection
	 */
	public void setEndDirection(long endDirection) {
		this.endDirection = endDirection;
	}

	/**
	 * 获取报警时刻速度的值
	 * @return endGpsSpeed  
	 */
	public long getEndGpsSpeed() {
		return endGpsSpeed;
	}

	/**
	 * 设置报警时刻速度的值
	 * @param endGpsSpeed
	 */
	public void setEndGpsSpeed(long endGpsSpeed) {
		this.endGpsSpeed = endGpsSpeed;
	}

	/**
	 * 获取里程(单位：千米)的值
	 * @return endMileage  
	 */
	public long getEndMileage() {
		return endMileage;
	}

	/**
	 * 设置里程(单位：千米)的值
	 * @param endMileage
	 */
	public void setEndMileage(long endMileage) {
		this.endMileage = endMileage;
	}

	/**
	 * 获取累计油耗的值
	 * @return endOilTotal  
	 */
	public long getEndOilTotal() {
		return endOilTotal;
	}

	/**
	 * 设置累计油耗的值
	 * @param endOilTotal
	 */
	public void setEndOilTotal(long endOilTotal) {
		this.endOilTotal = endOilTotal;
	}

	/**
	 * 获取车牌号的值
	 * @return vehicleNo  
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}

	/**
	 * 设置车牌号的值
	 * @param vehicleNo
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	/**
	 * 获取督办状态：0：未督办1：内部督办2：监管平台督办的值
	 * @return alarmTodo  
	 */
	public int getAlarmTodo() {
		return alarmTodo;
	}

	/**
	 * 设置督办状态：0：未督办1：内部督办2：监管平台督办的值
	 * @param alarmTodo
	 */
	public void setAlarmTodo(int alarmTodo) {
		this.alarmTodo = alarmTodo;
	}

	/**
	 * 获取报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）的值
	 * @return alarmSrc  
	 */
	public int getAlarmSrc() {
		return alarmSrc;
	}

	/**
	 * 设置报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）的值
	 * @param alarmSrc
	 */
	public void setAlarmSrc(int alarmSrc) {
		this.alarmSrc = alarmSrc;
	}

	/**
	 * 获取报警处理成功状态-1:未处理0：不作处理1：将来处理2：处理完毕的值
	 * @return alarmHandlerStatusType  
	 */
	public int getAlarmHandlerStatusType() {
		return alarmHandlerStatusType;
	}

	/**
	 * 设置报警处理成功状态-1:未处理0：不作处理1：将来处理2：处理完毕的值
	 * @param alarmHandlerStatusType
	 */
	public void setAlarmHandlerStatusType(int alarmHandlerStatusType) {
		this.alarmHandlerStatusType = alarmHandlerStatusType;
	}

	/**
	 * 获取报警所属大类编码的值
	 * @return bgLevel  
	 */
	public String getBgLevel() {
		return bgLevel;
	}

	/**
	 * 设置报警所属大类编码的值
	 * @param bgLevel
	 */
	public void setBgLevel(String bgLevel) {
		this.bgLevel = bgLevel;
	}

	/**
	 * 获取信号状态的值
	 * @return signStatus  
	 */
	public int getSignStatus() {
		return signStatus;
	}

	/**
	 * 设置信号状态的值
	 * @param signStatus
	 */
	public void setSignStatus(int signStatus) {
		this.signStatus = signStatus;
	}

	/**
	 * 获取扩展状态的值
	 * @return extendStatus  
	 */
	public String getExtendStatus() {
		return extendStatus;
	}

	/**
	 * 设置扩展状态的值
	 * @param extendStatus
	 */
	public void setExtendStatus(String extendStatus) {
		this.extendStatus = extendStatus;
	}

	/**
	 * 获取基本状态的值
	 * @return baseStatus  
	 */
	public String getBaseStatus() {
		return baseStatus;
	}

	/**
	 * 设置基本状态的值
	 * @param baseStatus
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	/**
	 * 获取报警开始附加信息的值
	 * @return alarmAddInfoStart  
	 */
	public String getAlarmAddInfoStart() {
		return alarmAddInfoStart;
	}

	/**
	 * 设置报警开始附加信息的值
	 * @param alarmAddInfoStart
	 */
	public void setAlarmAddInfoStart(String alarmAddInfoStart) {
		this.alarmAddInfoStart = alarmAddInfoStart;
	}

	/**
	 * 获取报警结束附加信息的值
	 * @return alarmAddInfoEnd  
	 */
	public String getAlarmAddInfoEnd() {
		return alarmAddInfoEnd;
	}

	/**
	 * 设置报警结束附加信息的值
	 * @param alarmAddInfoEnd
	 */
	public void setAlarmAddInfoEnd(String alarmAddInfoEnd) {
		this.alarmAddInfoEnd = alarmAddInfoEnd;
	}

	/**
	 * 获取超速报警开始点附加信息的值
	 * @return overspeedInfoST  
	 */
	public String getOverspeedInfoST() {
		return overspeedInfoST;
	}

	/**
	 * 设置超速报警开始点附加信息的值
	 * @param overspeedInfoST
	 */
	public void setOverspeedInfoST(String overspeedInfoST) {
		this.overspeedInfoST = overspeedInfoST;
	}

	/**
	 * 获取进出区域路段报警开始点附加信息的值
	 * @return enteringAreaST  
	 */
	public String getEnteringAreaST() {
		return enteringAreaST;
	}

	/**
	 * 设置进出区域路段报警开始点附加信息的值
	 * @param enteringAreaST
	 */
	public void setEnteringAreaST(String enteringAreaST) {
		this.enteringAreaST = enteringAreaST;
	}

	/**
	 * 获取路线行驶时间不足过长开始点附加信息的值
	 * @return outRouteST  
	 */
	public String getOutRouteST() {
		return outRouteST;
	}

	/**
	 * 设置路线行驶时间不足过长开始点附加信息的值
	 * @param outRouteST
	 */
	public void setOutRouteST(String outRouteST) {
		this.outRouteST = outRouteST;
	}

	/**
	 * 获取超速报警结束点附加信息的值
	 * @return overspeedInfoED  
	 */
	public String getOverspeedInfoED() {
		return overspeedInfoED;
	}

	/**
	 * 设置超速报警结束点附加信息的值
	 * @param overspeedInfoED
	 */
	public void setOverspeedInfoED(String overspeedInfoED) {
		this.overspeedInfoED = overspeedInfoED;
	}

	/**
	 * 获取进出区域路段报警结束点附加信息的值
	 * @return enteringAreaED  
	 */
	public String getEnteringAreaED() {
		return enteringAreaED;
	}

	/**
	 * 设置进出区域路段报警结束点附加信息的值
	 * @param enteringAreaED
	 */
	public void setEnteringAreaED(String enteringAreaED) {
		this.enteringAreaED = enteringAreaED;
	}

	/**
	 * 获取路线行驶时间不足过长结束点附加信息的值
	 * @return outRouteED  
	 */
	public String getOutRouteED() {
		return outRouteED;
	}

	/**
	 * 设置路线行驶时间不足过长结束点附加信息的值
	 * @param outRouteED
	 */
	public void setOutRouteED(String outRouteED) {
		this.outRouteED = outRouteED;
	}

	/**
	 * 获取报警具体描述的值
	 * @return alarmAddInfo  
	 */
	public String getAlarmAddInfo() {
		return alarmAddInfo;
	}

	/**
	 * 设置报警具体描述的值
	 * @param alarmAddInfo
	 */
	public void setAlarmAddInfo(String alarmAddInfo) {
		this.alarmAddInfo = alarmAddInfo;
	}

	/**
	 * 获取围栏或线路ID的值
	 * @return areaId  
	 */
	public String getAreaId() {
		return areaId;
	}

	/**
	 * 设置围栏或线路ID的值
	 * @param areaId
	 */
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}

	/**
	 * 获取最大转速的值
	 * @return maxRpm  
	 */
	public String getMaxRpm() {
		return maxRpm;
	}

	/**
	 * 设置最大转速的值
	 * @param maxRpm
	 */
	public void setMaxRpm(String maxRpm) {
		this.maxRpm = maxRpm;
	}

	/**
	 * 获取速度阀值，超速告警使用的值
	 * @return speedThreshOld  
	 */
	public String getSpeedThreshOld() {
		return speedThreshOld;
	}

	/**
	 * 设置速度阀值，超速告警使用的值
	 * @param speedThreshOld
	 */
	public void setSpeedThreshOld(String speedThreshOld) {
		this.speedThreshOld = speedThreshOld;
	}

	/**
	 * 获取告警处理方式1:平台下发信息2:平台拍照3:平台解除的值
	 * @return handlerMethods  
	 */
	public int getHandlerMethods() {
		return handlerMethods;
	}

	/**
	 * 设置告警处理方式1:平台下发信息2:平台拍照3:平台解除的值
	 * @param handlerMethods
	 */
	public void setHandlerMethods(int handlerMethods) {
		this.handlerMethods = handlerMethods;
	}

	/**
	 * 获取最大速度的值
	 * @return maxSpeed  
	 */
	public long getMaxSpeed() {
		return maxSpeed;
	}

	/**
	 * 设置最大速度的值
	 * @param maxSpeed
	 */
	public void setMaxSpeed(long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	/**
	 * 获取平均速度的值
	 * @return avgSpeed  
	 */
	public long getAvgSpeed() {
		return avgSpeed;
	}

	/**
	 * 设置平均速度的值
	 * @param avgSpeed
	 */
	public void setAvgSpeed(long avgSpeed) {
		this.avgSpeed = avgSpeed;
	}

	/**
	 * 获取车队ID的值
	 * @return teamId  
	 */
	public String getTeamId() {
		return teamId;
	}

	/**
	 * 设置车队ID的值
	 * @param teamId
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}

	/**
	 * 获取车队名称的值
	 * @return teamName  
	 */
	public String getTeamName() {
		return teamName;
	}

	/**
	 * 设置车队名称的值
	 * @param teamName
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	/**
	 * 获取组织ID的值
	 * @return corpId  
	 */
	public String getCorpId() {
		return corpId;
	}

	/**
	 * 设置组织ID的值
	 * @param corpId
	 */
	public void setCorpId(String corpId) {
		this.corpId = corpId;
	}

	/**
	 * 获取组织名称的值
	 * @return corpName  
	 */
	public String getCorpName() {
		return corpName;
	}

	/**
	 * 设置组织名称的值
	 * @param corpName
	 */
	public void setCorpName(String corpName) {
		this.corpName = corpName;
	}
	
	
	
}
