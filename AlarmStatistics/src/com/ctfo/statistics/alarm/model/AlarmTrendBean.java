package com.ctfo.statistics.alarm.model;
/**
 * 告警趋势
 *
 */
public class AlarmTrendBean implements Comparable<AlarmTrendBean>{
	/**	告警日期	*/
	private int date;
	/**	趋势	*/
	private String trend;
	/**
	 * 获取[告警日期]值
	 */
	public int getDate() {
		return date;
	}

	/**
	 * 设置[告警日期] 值
	 */
	public void setDate(int date) {
		this.date = date;
	}

	/**
	 * 获取[趋势]值
	 */
	public String getTrend() {
		return trend;
	}

	/**
	 * 设置[趋势] 值
	 */
	public void setTrend(String trend) {
		this.trend = trend;
	}

	@Override
	public int compareTo(AlarmTrendBean bean) {
		return this.date - bean.date;
	}
}
