package com.ctfo.dataanalysisservice.beans;

public class AlarmDataCache {

	private Long vid;

	private Long areaId;

	private Long endSpeed;

	private Long lengthOfTime; // 超速持续时间

	// 超速开始速度
	private Long beginOverSpeed;
	// 超速结束速度
	private Long endOverSpeed;

	// 超速开始时间
	private Long beginOverSpeedTime;
	// 超速结束时间
	private Long endOverSpeedTime;

	private boolean inOutAreaAlarm = false; // 进区域报警 true在区域内，false在区域外

	private boolean speedingAlarm = false; // 超速

	private boolean speedAndTimeAlarm = false; // 超速报警（满足超速的速度和持续时间）

	private Long lon;

	private Long lat;

	private Long gpsUtc;

	public Long getBeginOverSpeed() {
		return beginOverSpeed;
	}

	public void setBeginOverSpeed(Long beginOverSpeed) {
		this.beginOverSpeed = beginOverSpeed;
	}

	public Long getEndOverSpeed() {
		return endOverSpeed;
	}

	public void setEndOverSpeed(Long endOverSpeed) {
		this.endOverSpeed = endOverSpeed;
	}

	public Long getBeginOverSpeedTime() {
		return beginOverSpeedTime;
	}

	public void setBeginOverSpeedTime(Long beginOverSpeedTime) {
		this.beginOverSpeedTime = beginOverSpeedTime;
	}

	public Long getEndOverSpeedTime() {
		return endOverSpeedTime;
	}

	public void setEndOverSpeedTime(Long endOverSpeedTime) {
		this.endOverSpeedTime = endOverSpeedTime;
	}

	public Long getGpsUtc() {
		return gpsUtc;
	}

	public void setGpsUtc(Long gpsUtc) {
		this.gpsUtc = gpsUtc;
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

	public Long getVid() {
		return vid;
	}

	public void setVid(Long vid) {
		this.vid = vid;
	}

	public Long getAreaId() {
		return areaId;
	}

	public void setAreaId(Long areaId) {
		this.areaId = areaId;
	}

	public Long getEndSpeed() {
		return endSpeed;
	}

	public void setEndSpeed(Long endSpeed) {
		this.endSpeed = endSpeed;
	}

	public Long getLengthOfTime() {
		return lengthOfTime;
	}

	public void setLengthOfTime(Long lengthOfTime) {
		this.lengthOfTime = lengthOfTime;
	}

	/**
	 * 进区域报警 true在区域内，false在区域外
	 * 
	 * @return
	 */
	public boolean isInOutAreaAlarm() {
		return inOutAreaAlarm;
	}

	public void setInOutAreaAlarm(boolean inOutAreaAlarm) {
		this.inOutAreaAlarm = inOutAreaAlarm;
	}

	/**
	 * 超速 true 未超速 false
	 * 
	 * @return
	 */
	public boolean isSpeedingAlarm() {
		return speedingAlarm;
	}

	public void setSpeedingAlarm(boolean speedingAlarm) {
		this.speedingAlarm = speedingAlarm;
	}

	public boolean isSpeedAndTimeAlarm() {
		return speedAndTimeAlarm;
	}

	public void setSpeedAndTimeAlarm(boolean speedAndTimeAlarm) {
		this.speedAndTimeAlarm = speedAndTimeAlarm;
	}

}
