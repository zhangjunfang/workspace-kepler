package com.ctfo.analy.beans;

/**
 * 命令解析后车辆上报数据
 * 
 * @author yangjian
 * 
 */
public class VehicleMessageBean {

	private String vid;//车辆唯一标识
	private String msgid;//消息服务器ID
	
	private String commanddr;//手机号
	private String oemcode;//终端识别码
	
	private String msgType;//消息类型
	
	private String onlineState;//在线状态

	private String dateString;//定位时间yyyymmdd/hhmmss
	private Long utc;//定位时间

	private Long lon;//经度
	private Long lat;//纬度
	private Long maplon=0l;//偏移经度
	private Long maplat=0l;//偏移纬度


	private Integer speed;//速度
	private Integer dir;//方向
	private String alarmtype;//设置报警类型(多个报警用逗号分割)0:围栏（进出报警和超速） 1：关键点（到达和离开 没有超速） 2：线路（偏移和分段限速）

	private String alarmid;//报警id
	private String alarmcode;//报警编码
	private Integer elevation=0;//海拔
	private Long mileage=0L;//里程
	private Long oil=0L;//油量
	
	private Long metOil=0L;//精准油耗
	
	private Long rpm;//发动机转速
	
	private String vehicleno;//车牌号
	private String alarmadd;//附加标志位
	private String bglevel;//报警级别
	
	private String AlarmAddInfo;//报警具体描述
	
	private String baseStatus;//基本状态位
	
	private String areaId;//当前所相关围栏或线路ID

	private boolean isAlarm = false;
	
	private boolean isReach = false;// 到报警是否判断结束
	
	private boolean isLeave = false;// 离开报警是否判断结束
	
	private long receiveUtc;//平台接收时间
	
	private int alarmSrc=2;
	
	private String baseAlarmStatus;//基本报警位
	
	private String extendAlarmStatus;//扩展报警位
	
	private double  speedThreshold;//速度阀值，超速报警时使用
	
	private String command;
	
	private String driverId;
	private String driverName;
	private String driverSrc;

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

	public Integer getSpeed() {
		return speed;
	}

	public void setSpeed(Integer speed) {
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

	public Long getRpm() {
		return rpm;
	}

	public void setRpm(Long rpm) {
		this.rpm = rpm;
	}

	public String getMsgType() {
		return msgType;
	}

	public void setMsgType(String msgType) {
		this.msgType = msgType;
	}

	public String getOnlineState() {
		return onlineState;
	}

	public void setOnlineState(String onlineState) {
		this.onlineState = onlineState;
	}

	public long getReceiveUtc() {
		return receiveUtc;
	}

	public void setReceiveUtc(long receiveUtc) {
		this.receiveUtc = receiveUtc;
	}
	
	public int getAlarmSrc() {
		return alarmSrc;
	}
	
	public void setAlarmSrc(int alarmSrc) {
		this.alarmSrc = alarmSrc;
	}

	public String getBaseAlarmStatus() {
		return baseAlarmStatus;
	}

	public void setBaseAlarmStatus(String baseAlarmStatus) {
		this.baseAlarmStatus = baseAlarmStatus;
	}

	public String getExtendAlarmStatus() {
		return extendAlarmStatus;
	}

	public void setExtendAlarmStatus(String extendAlarmStatus) {
		this.extendAlarmStatus = extendAlarmStatus;
	}

	public double getSpeedThreshold() {
		return speedThreshold;
	}

	public void setSpeedThreshold(double speedThreshold) {
		this.speedThreshold = speedThreshold;
	}

	public String getCommand() {
		return command;
	}

	public void setCommand(String command) {
		this.command = command;
	}

	public String getDriverId() {
		return driverId;
	}

	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}

	public String getDriverName() {
		return driverName;
	}

	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}

	public String getDriverSrc() {
		return driverSrc;
	}

	public void setDriverSrc(String driverSrc) {
		this.driverSrc = driverSrc;
	}
}
