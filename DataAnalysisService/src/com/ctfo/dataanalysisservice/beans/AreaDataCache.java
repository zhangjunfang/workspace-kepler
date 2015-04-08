package com.ctfo.dataanalysisservice.beans;

/**
 * 围栏属性bean
 */
public class AreaDataCache {

	// 围栏判定开始时间
	private Long beginTime;
	// 围栏判定结束时间
	private Long endTime;

	private Long areaId; // 区域ID

	private Long llon; // 经度

	private Long llat; // 维度

	private Long rlon;

	private Long rlat;

	private Long areaMaxSpeed; // 最高速度(km/h)

	private Long superSpeedTimes; // 超速持续时间(s)

	public Long getBeginTime() {
		return beginTime;
	}

	public void setBeginTime(Long beginTime) {
		this.beginTime = beginTime;
	}

	public Long getEndTime() {
		return endTime;
	}

	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}

	public Long getAreaId() {
		return areaId;
	}

	public void setAreaId(Long areaId) {
		this.areaId = areaId;
	}

	public Long getLlon() {
		return llon;
	}

	public void setLlon(Long llon) {
		this.llon = llon;
	}

	public Long getLlat() {
		return llat;
	}

	public void setLlat(Long llat) {
		this.llat = llat;
	}

	public Long getRlon() {
		return rlon;
	}

	public void setRlon(Long rlon) {
		this.rlon = rlon;
	}

	public Long getRlat() {
		return rlat;
	}

	public void setRlat(Long rlat) {
		this.rlat = rlat;
	}

	public Long getAreaMaxSpeed() {
		return areaMaxSpeed;
	}

	public void setAreaMaxSpeed(Long areaMaxSpeed) {
		this.areaMaxSpeed = areaMaxSpeed;
	}

	public Long getSuperSpeedTimes() {
		return superSpeedTimes;
	}

	public void setSuperSpeedTimes(Long superSpeedTimes) {
		this.superSpeedTimes = superSpeedTimes;
	}

}
