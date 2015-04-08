package com.ctfo.statistics.alarm.model;

public class FatigueRules {
	/**	车辆编号	*/
	private String vid;
	/**	规则起始时间	*/
	private long startUtc;
	/**	规则结束时间	*/
	private long endUtc;
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
	 * 获取[规则起始时间]值
	 */
	public long getStartUtc() {
		return startUtc;
	}
	/**
	 * 设置[规则起始时间] 值
	 */
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}
	/**
	 * 获取[规则结束时间]值
	 */
	public long getEndUtc() {
		return endUtc;
	}
	/**
	 * 设置[规则结束时间] 值
	 */
	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
	}
	
}
