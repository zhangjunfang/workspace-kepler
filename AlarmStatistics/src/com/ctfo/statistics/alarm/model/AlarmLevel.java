package com.ctfo.statistics.alarm.model;

public class AlarmLevel {
	/**	告警级别	*/
	private int level;
	/**	告警总计	*/
	private int total;
	public AlarmLevel(int alarmLevel, int alarmTotal){
		this.level = alarmLevel;
		this.total = alarmTotal;
	}
	/**
	 * 获取[告警级别]值
	 */
	public int getLevel() {
		return level;
	}
	/**
	 * 设置[告警级别] 值
	 */
	public void setLevel(int level) {
		this.level = level;
	}
	/**
	 * 获取[告警总计]值
	 */
	public int getTotal() {
		return total;
	}
	/**
	 * 设置[告警总计] 值
	 */
	public void setTotal(int total) {
		this.total = total;
	}
	
}
