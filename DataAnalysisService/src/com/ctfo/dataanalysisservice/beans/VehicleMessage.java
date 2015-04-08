package com.ctfo.dataanalysisservice.beans;

/**
 * 命令解析后车辆上报数据bean
 * 
 * @author yangjian
 * 
 */
public class VehicleMessage {

	private String uuid;

	private Long vid;

	private String seq;

	private String oemCode;

	private Long utc;

	private Long lon;

	private Long lat;

	private String commanddr;

	private Long speed;

	private boolean isAlarm = false;

	// 到报警是否判断结束
	private boolean isReach = false;
	
	// 离开报警是否判断结束
	private boolean isLeave = false;
	
	private Long maplon=0l;
	private Long maplat=0l;


	/**
	 * 多个报警类型 用逗号分隔
	 */
	private String alarmType;

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

	public Long getSpeed() {
		return speed;
	}

	public void setSpeed(Long speed) {
		this.speed = speed;
	}

	public String getUuid() {
		return uuid;
	}

	public void setUuid(String uuid) {
		this.uuid = uuid;
	}

	public Long getVid() {
		return vid;
	}

	public void setVid(Long vid) {
		this.vid = vid;
	}

	public String getSeq() {
		return seq;
	}

	public void setSeq(String seq) {
		this.seq = seq;
	}

	public String getOemCode() {
		return oemCode;
	}

	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
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

	public String getCommanddr() {
		return commanddr;
	}

	public void setCommanddr(String commanddr) {
		this.commanddr = commanddr;
	}

	public String getAlarmType() {
		return alarmType;
	}

	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}

	/**
	 * @return the isAlarm
	 */
	public boolean isAlarm() {
		return isAlarm;
	}

	/**
	 * @param isAlarm
	 *            the isAlarm to set
	 */
	public void setAlarm(boolean isAlarm) {
		this.isAlarm = isAlarm;
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
}
