package com.caits.analysisserver.bean;

/**
 * 命令解析后车辆上报数据
 * 
 * @author yujch
 * 
 */
public class VehicleMessageBean {

	private String vid;//车辆唯一标识
	private String msgid;//消息服务器ID
	
	private String commanddr;//手机号
	private String oemcode;//终端识别码

	private String dateString;//定位时间yyyymmdd/hhmmss
	private Long utc;//定位时间

	private Long lon;//经度
	private Long lat;//纬度
	private Long maplon=0l;//偏移经度
	private Long maplat=0l;//偏移纬度


	private Long speed;//速度
	private Integer dir;//方向
	private String alarmtype;//设置报警类型(多个报警用逗号分割)0:围栏（进出报警和超速） 1：关键点（到达和离开 没有超速） 2：线路（偏移和分段限速）

	private String alarmid;//报警id
	private String alarmcode;//报警编码
	private Integer elevation=0;//海拔
	private Long mileage=0L;//里程
	private Long oil=0L;//油量
	
	private Long metOil=0L;//精准油耗
	
	private String vehicleno;//车牌号
	private String alarmadd;//附加标志位
	private String bglevel;//报警级别
	
	private String AlarmAddInfo;//报警具体描述
	
	private String baseStatus;//基本状态位
	
	private String extendStatus;//扩展状态位
	
	private String areaId;//当前所相关围栏或线路ID

	private boolean isAlarm = false;
	
	private boolean isReach = false;// 到报警是否判断结束
	
	private boolean isLeave = false;// 离开报警是否判断结束
	
	private Long gpsSpeed;
	private Long vssSpeed;
	
	private String speedFrom;
	
	private Long rpm;//发动机转速
	
	private boolean accState;//acc状态  true acc开 false acc关
	
	private boolean gpsState;//gps状态 true 已定位 false 未定位
	
	/**
	 * "位置类型｜区域或线路ID｜方向 
	类型：   0：无特定位置；1：圆型区域；2：矩形区域；3：多边形区域；4：路线
	方向：   0：进，1：出
	 */
	private String areaAlarmAdditional;//区域/线路报警附加信息 
	/**
	 * 超速报警，格式：位置类型|区域或路段ID
	 类型： 0：无特定位置；1：圆型区域；2：矩形区域；3：多边形区域；4：路段 
	 当类型为0时，无区域ID或路段ID值			
	 */
	private String overspeedAlarmAdditional;//超速报警附加信息
	
	/**
	 * "
	路线行驶时间不足/过长
	       格式： 路段ID｜路段行驶时间｜结果
	       结果： 0：不足，1：过长"				
	 */
	private String alarmAdditional;//路段行驶时间不足或过长附加信息
	
	private String opendoorState;//开门状态  0：带速开门；1区域外开门；2：区域内开门；其他值保留

	
	private String torque;//发动机扭矩百分比
	
	private Long volgate;//蓄电池电压
	
	private Long oilPres;//机油压力
	
	private Long coolLiquidtem;//冷却液温度
	
	private Long gsPres;//进气压力
	
	private Long airTemperture;//进气温度
	
	private String trackStr;
	
	private String driverId;//驾驶员编号
	
	private String driverName;//驾驶员姓名
	
	private String driverSrc;//驾驶员信息来源（0、平台绑定 1、驾驶员卡）
	
	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getMsgid() {
		return msgid;
	}

	public void setMsgid(String msgid) {
		this.msgid = msgid;
	}

	public String getCommanddr() {
		return commanddr;
	}

	public void setCommanddr(String commanddr) {
		this.commanddr = commanddr;
	}

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	public String getDateString() {
		return dateString;
	}

	public void setDateString(String dateString) {
		this.dateString = dateString;
	}

	public Long getUtc() {
		return utc;
	}

	public void setUtc(Long utc) {
		this.utc = utc;
	}

	public Long getLon() {
		return lon;
	}

	public void setLon(Long lon) {
		this.lon = lon;
	}

	public Long getLat() {
		return lat;
	}

	public void setLat(Long lat) {
		this.lat = lat;
	}

	public Long getMaplon() {
		return maplon;
	}

	public void setMaplon(Long maplon) {
		this.maplon = maplon;
	}

	public Long getMaplat() {
		return maplat;
	}

	public void setMaplat(Long maplat) {
		this.maplat = maplat;
	}

	public Long getSpeed() {
		return speed;
	}

	public void setSpeed(Long speed) {
		this.speed = speed;
	}

	public Integer getDir() {
		return dir;
	}

	public void setDir(Integer dir) {
		this.dir = dir;
	}

	public String getAlarmtype() {
		return alarmtype;
	}

	public void setAlarmtype(String alarmtype) {
		this.alarmtype = alarmtype;
	}

	public String getAlarmid() {
		return alarmid;
	}

	public void setAlarmid(String alarmid) {
		this.alarmid = alarmid;
	}

	public String getAlarmcode() {
		return alarmcode;
	}

	public void setAlarmcode(String alarmcode) {
		this.alarmcode = alarmcode;
	}

	public Integer getElevation() {
		return elevation;
	}

	public void setElevation(Integer elevation) {
		this.elevation = elevation;
	}

	public Long getMileage() {
		return mileage;
	}

	public void setMileage(Long mileage) {
		this.mileage = mileage;
	}

	public Long getOil() {
		return oil;
	}

	public void setOil(Long oil) {
		this.oil = oil;
	}

	public String getVehicleno() {
		return vehicleno;
	}

	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}

	public String getAlarmadd() {
		return alarmadd;
	}

	public void setAlarmadd(String alarmadd) {
		this.alarmadd = alarmadd;
	}

	public String getBglevel() {
		return bglevel;
	}

	public void setBglevel(String bglevel) {
		this.bglevel = bglevel;
	}

	public String getAlarmAddInfo() {
		return AlarmAddInfo;
	}

	public void setAlarmAddInfo(String alarmAddInfo) {
		AlarmAddInfo = alarmAddInfo;
	}

	public boolean isAlarm() {
		return isAlarm;
	}

	public void setAlarm(boolean isAlarm) {
		this.isAlarm = isAlarm;
	}

	public boolean isReach() {
		return isReach;
	}

	public void setReach(boolean isReach) {
		this.isReach = isReach;
	}

	public boolean isLeave() {
		return isLeave;
	}

	public void setLeave(boolean isLeave) {
		this.isLeave = isLeave;
	}

	public String getBaseStatus() {
		return baseStatus;
	}

	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	public Long getMetOil() {
		return metOil;
	}

	public void setMetOil(Long metOil) {
		this.metOil = metOil;
	}

	public String getAreaId() {
		return areaId;
	}

	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}

	public Long getGpsSpeed() {
		return gpsSpeed;
	}

	public void setGpsSpeed(Long gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	public Long getVssSpeed() {
		return vssSpeed;
	}

	public void setVssSpeed(Long vssSpeed) {
		this.vssSpeed = vssSpeed;
	}

	public String getSpeedFrom() {
		return speedFrom;
	}

	public void setSpeedFrom(String speedFrom) {
		this.speedFrom = speedFrom;
	}

	public Long getRpm() {
		return rpm;
	}

	public void setRpm(Long rpm) {
		this.rpm = rpm;
	}

	public boolean isAccState() {
		return accState;
	}

	public void setAccState(boolean accState) {
		this.accState = accState;
	}

	public boolean isGpsState() {
		return gpsState;
	}

	public void setGpsState(boolean gpsState) {
		this.gpsState = gpsState;
	}

	public String getAlarmAdditional() {
		return alarmAdditional;
	}

	public void setAlarmAdditional(String alarmAdditional) {
		this.alarmAdditional = alarmAdditional;
	}

	public String getOpendoorState() {
		return opendoorState;
	}

	public void setOpendoorState(String opendoorState) {
		this.opendoorState = opendoorState;
	}

	public String getAreaAlarmAdditional() {
		return areaAlarmAdditional;
	}

	public void setAreaAlarmAdditional(String areaAlarmAdditional) {
		this.areaAlarmAdditional = areaAlarmAdditional;
	}

	public String getOverspeedAlarmAdditional() {
		return overspeedAlarmAdditional;
	}

	public void setOverspeedAlarmAdditional(String overspeedAlarmAdditional) {
		this.overspeedAlarmAdditional = overspeedAlarmAdditional;
	}

	public String getTorque() {
		return torque;
	}

	public void setTorque(String torque) {
		this.torque = torque;
	}

	public Long getVolgate() {
		return volgate;
	}

	public void setVolgate(Long volgate) {
		this.volgate = volgate;
	}

	public Long getOilPres() {
		return oilPres;
	}

	public void setOilPres(Long oilPres) {
		this.oilPres = oilPres;
	}

	public Long getCoolLiquidtem() {
		return coolLiquidtem;
	}

	public void setCoolLiquidtem(Long coolLiquidtem) {
		this.coolLiquidtem = coolLiquidtem;
	}

	public Long getGsPres() {
		return gsPres;
	}

	public void setGsPres(Long gsPres) {
		this.gsPres = gsPres;
	}

	public Long getAirTemperture() {
		return airTemperture;
	}

	public void setAirTemperture(Long airTemperture) {
		this.airTemperture = airTemperture;
	}

	public String getExtendStatus() {
		return extendStatus;
	}

	public void setExtendStatus(String extendStatus) {
		this.extendStatus = extendStatus;
	}

	public String getTrackStr() {
		return trackStr;
	}

	public void setTrackStr(String trackStr) {
		this.trackStr = trackStr;
	}

	public String getDriverId() {
		if (driverId == null) {
			return "";
		} else {
			return driverId;
		}
		// return driverId;
	}

	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}

	public String getDriverSrc() {
		return driverSrc;
	}

	public void setDriverSrc(String driverSrc) {
		this.driverSrc = driverSrc;
	}

	public String getDriverName() {
		return driverName;
	}

	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}
}
