package com.ctfo.statistics.alarm.model;

public class Mileage {
	/**	车辆编号	 */
	private String vid;
	/**	最小里程	 */
	private int minMileage;
	/**	最大里程	 */
	private int maxLileage;
	/**
	 * 获取[车辆编号]值
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置[车辆编号] 值
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获取[最小里程]值
	 */
	public int getMinMileage() {
		return minMileage;
	}
	/**
	 * 设置[最小里程] 值
	 */
	public void setMinMileage(int minMileage) {
		this.minMileage = minMileage;
	}
	/**
	 * 获取[最大里程]值
	 */
	public int getMaxLileage() {
		return maxLileage;
	}
	/**
	 * 设置[最大里程] 值
	 */
	public void setMaxLileage(int maxLileage) {
		this.maxLileage = maxLileage;
	}
	/**
	 * 重置里程对象
	 */
	public void reset() {
		this.maxLileage = 0;
		this.minMileage = 0;
	}
}
