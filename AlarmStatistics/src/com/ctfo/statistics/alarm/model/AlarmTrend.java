package com.ctfo.statistics.alarm.model;
/**
 * 告警趋势
 *
 */
public class AlarmTrend {
	/**	告警日期	*/
	private long date;
	/**	严重告警（1）	*/
	private int serious;
	/**	中度告警（2）	*/
	private int urgent;
	/**	一般告警（3）	*/
	private int general;
	/**	告警日期字符串	*/
	private String dateStr;
	/**
	 * 初始化告警趋势
	 * @param dateStr
	 */
	public AlarmTrend(long dateUtc){
		this.date = dateUtc;
	}
	/**
	 * 设置告警级别
	 * @param level
	 * @param total
	 */
	public void setAlarmLevel(int level, int total) {
		if(level == 1){
			this.serious = total;
		}else if(level == 2){
			this.urgent = total;
		}else{
			this.general = total;
		}
	}
	/**
	 * 获取[告警日期]值
	 */
	public long getDate() {
		return date;
	}
	/**
	 * 设置[告警日期] 值
	 */
	public void setDate(long date) {
		this.date = date;
	}
	/**
	 * 获取[严重告警（1）]值
	 */
	public int getSerious() {
		return serious;
	}
	/**
	 * 设置[严重告警（1）] 值
	 */
	public void setSerious(int serious) {
		this.serious = serious;
	}
	/**
	 * 获取[中度告警（2）]值
	 */
	public int getUrgent() {
		return urgent;
	}
	/**
	 * 设置[中度告警（2）] 值
	 */
	public void setUrgent(int urgent) {
		this.urgent = urgent;
	}
	/**
	 * 获取[一般告警（3）]值
	 */
	public int getGeneral() {
		return general;
	}
	/**
	 * 设置[一般告警（3）] 值
	 */
	public void setGeneral(int general) {
		this.general = general;
	}
	/**
	 * 获取[告警日期字符串]值
	 */
	public String getDateStr() {
		return dateStr;
	}
	/**
	 * 设置[告警日期字符串] 值
	 */
	public void setDateStr(String dateStr) {
		this.dateStr = dateStr;
	}
	
}
