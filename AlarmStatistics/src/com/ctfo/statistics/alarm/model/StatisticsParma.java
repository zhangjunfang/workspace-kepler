package com.ctfo.statistics.alarm.model;

import com.ctfo.statistics.alarm.common.Utils;

/**
 *	统计参数
 *
 */
public class StatisticsParma {
	/**	统计日期字符串（YYYY-MM-DD）	*/
	private String statisticsDayStr;
	/**	统计正午UTC时间（12:00:00）	*/
	private long currentUtc;
	/**	统计开始UTC时间（00:00:00）	*/
	private long startUtc;
	/**	统计结束UTC时间（23:59:59）	*/
	private long endUtc;
	/**	上周三正午UTC时间（15 12:00:00）	*/
	private long lastWeekUtc;
	/**	上周（一）统计开始UTC时间（yyyyMM01 00:00:00）	*/
	private long lastWeekStartUtc;
	/**	上周（日）统计结束UTC时间（yyyyMM[28~31] 23:59:59）	*/
	private long lastWeekEndUtc;
	/**	上个月15号正午UTC时间（15 12:00:00）	*/
	private long lastMonthUtc;
	/**	上个月统计开始UTC时间（yyyyMM01 00:00:00）	*/
	private long lastMonthStartUtc;
	/**	上个月统计结束UTC时间（yyyyMM[28~31] 23:59:59）	*/
	private long lastMonthEndUtc;
	/**	轨迹文件日期路径（/yyyy/MM/dd/）	*/
	private String trackDatePath;
	/**	手动统计标记（true:false）	*/
	private boolean manualOperation;
	/**	统计日期（yyyyMMdd）	*/
	private String dateStr;
	/**	非法驾驶开始UTC时间（02:00:00）	*/
	private long illegalDrivingStartUtc;
	/**	非法驾驶U结束UTC时间（05:00:00）	*/
	private long illegalDrivingEndUtc;
	/**	是否周统计		*/
	private boolean weekStatistics;
	/**	是否月统计		*/
	private boolean monthStatistics;
	/**	统计年份（yyyy）	*/
	private int year;
	/**	统计月份（MM）	*/
	private int month;
	/**	统计周（当年第几周）	*/
	private int week;
	/**
	 * 获取[统计日期字符串（YYYY-MM-DD）]值
	 */
	public String getStatisticsDayStr() {
		return statisticsDayStr;
	}
	/**
	 * 设置[统计日期字符串（YYYY-MM-DD）] 值
	 */
	public void setStatisticsDayStr(String statisticsDayStr) {
		this.statisticsDayStr = statisticsDayStr;
	}
	/**
	 * 获取[统计正午UTC时间（12:00:00）]值
	 */
	public long getCurrentUtc() {
		return currentUtc;
	}
	/**
	 * 设置[统计正午UTC时间（12:00:00）] 值
	 */
	public void setCurrentUtc(long currentUtc) {
		this.currentUtc = currentUtc;
	}
	/**
	 * 获取[统计开始UTC时间（00:00:00）]值
	 */
	public long getStartUtc() {
		return startUtc;
	}
	/**
	 * 设置[统计开始UTC时间（00:00:00）] 值
	 */
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}
	/**
	 * 获取[统计结束UTC时间（23:59:59）]值
	 */
	public long getEndUtc() {
		return endUtc;
	}
	/**
	 * 设置[统计结束UTC时间（23:59:59）] 值
	 */
	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
	}
	/**
	 * 获取[轨迹文件日期路径（yyyyMMdd）]值
	 */
	public String getTrackDatePath() {
		return trackDatePath;
	}
	/**
	 * 设置[轨迹文件日期路径（yyyyMMdd）] 值
	 */
	public void setTrackDatePath(String trackDatePath) {
		this.trackDatePath = trackDatePath;
	}
	/**
	 * 获取[手动统计标记（true:false）]值
	 */
	public boolean isManualOperation() {
		return manualOperation;
	}
	/**
	 * 设置[手动统计标记（true:false）] 值
	 */
	public void setManualOperation(boolean manualOperation) {
		this.manualOperation = manualOperation;
	}
	/**
	 * 获取[统计日期（yyyyMMdd）]值
	 */
	public String getDateStr() {
		return dateStr;
	}
	/**
	 * 设置[统计日期（yyyyMMdd）] 值
	 */
	public void setDateStr(String dateStr) {
		this.dateStr = dateStr;
		if(dateStr.length() > 6){
			String yearStr = dateStr.substring(0, 4);
			if(Utils.isNumeric(yearStr)){
				this.year = Integer.parseInt(yearStr);
			}
			String monthStr = dateStr.substring(4, 6);
			if(Utils.isNumeric(monthStr)){
				this.month = Integer.parseInt(monthStr);
			}
		}
	}
	/**
	 * 获取[非法驾驶开始UTC时间（02:00:00）]值
	 */
	public long getIllegalDrivingStartUtc() {
		return illegalDrivingStartUtc;
	}
	/**
	 * 设置[非法驾驶开始UTC时间（02:00:00）] 值
	 */
	public void setIllegalDrivingStartUtc(long illegalDrivingStartUtc) {
		this.illegalDrivingStartUtc = illegalDrivingStartUtc;
	}
	/**
	 * 获取[非法驾驶U结束UTC时间（05:00:00）]值
	 */
	public long getIllegalDrivingEndUtc() {
		return illegalDrivingEndUtc;
	}
	/**
	 * 设置[非法驾驶U结束UTC时间（05:00:00）] 值
	 */
	public void setIllegalDrivingEndUtc(long illegalDrivingEndUtc) {
		this.illegalDrivingEndUtc = illegalDrivingEndUtc;
	}
	/**
	 * 获取[是否周统计]值
	 */
	public boolean isWeekStatistics() {
		return weekStatistics;
	}
	/**
	 * 设置[是否周统计] 值
	 */
	public void setWeekStatistics(boolean weekStatistics) {
		this.weekStatistics = weekStatistics;
	}
	/**
	 * 获取[是否月统计]值
	 */
	public boolean isMonthStatistics() {
		return monthStatistics;
	}
	/**
	 * 设置[是否月统计] 值
	 */
	public void setMonthStatistics(boolean monthStatistics) {
		this.monthStatistics = monthStatistics;
	}
	/**
	 * 获取[统计年份（yyyy）]值
	 */
	public int getYear() {
		return year;
	}
	/**
	 * 设置[统计年份（yyyy）] 值
	 */
	public void setYear(int year) {
		this.year = year;
	}
	/**
	 * 获取[统计月份（MM）]值
	 */
	public int getMonth() {
		return month;
	}
	/**
	 * 设置[统计月份（MM）] 值
	 */
	public void setMonth(int month) {
		this.month = month;
	}
	/**
	 * 获取[上个月15号正午UTC时间（1512:00:00）]值
	 */
	public long getLastMonthUtc() {
		return lastMonthUtc;
	}
	/**
	 * 设置[上个月15号正午UTC时间（1512:00:00）] 值
	 */
	public void setLastMonthUtc(long lastMonthUtc) {
		this.lastMonthUtc = lastMonthUtc;
	}
	/**
	 * 获取[上个月统计开始UTC时间（yyyyMM0100:00:00）]值
	 */
	public long getLastMonthStartUtc() {
		return lastMonthStartUtc;
	}
	/**
	 * 设置[上个月统计开始UTC时间（yyyyMM0100:00:00）] 值
	 */
	public void setLastMonthStartUtc(long lastMonthStartUtc) {
		this.lastMonthStartUtc = lastMonthStartUtc;
	}
	/**
	 * 获取[上个月统计结束UTC时间（yyyyMM[28~31]23:59:59）]值
	 */
	public long getLastMonthEndUtc() {
		return lastMonthEndUtc;
	}
	/**
	 * 设置[上个月统计结束UTC时间（yyyyMM[28~31]23:59:59）] 值
	 */
	public void setLastMonthEndUtc(long lastMonthEndUtc) {
		this.lastMonthEndUtc = lastMonthEndUtc;
	}
	/**
	 * 获取[上周三正午UTC时间（1512:00:00）]值
	 */
	public long getLastWeekUtc() {
		return lastWeekUtc;
	}
	/**
	 * 设置[上周三正午UTC时间（1512:00:00）] 值
	 */
	public void setLastWeekUtc(long lastWeekUtc) {
		this.lastWeekUtc = lastWeekUtc;
	}
	/**
	 * 获取[上周（一）统计开始UTC时间（yyyyMM0100:00:00）]值
	 */
	public long getLastWeekStartUtc() {
		return lastWeekStartUtc;
	}
	/**
	 * 设置[上周（一）统计开始UTC时间（yyyyMM0100:00:00）] 值
	 */
	public void setLastWeekStartUtc(long lastWeekStartUtc) {
		this.lastWeekStartUtc = lastWeekStartUtc;
	}
	/**
	 * 获取[上周（日）统计结束UTC时间（yyyyMM[28~31]23:59:59）]值
	 */
	public long getLastWeekEndUtc() {
		return lastWeekEndUtc;
	}
	/**
	 * 设置[上周（日）统计结束UTC时间（yyyyMM[28~31]23:59:59）] 值
	 */
	public void setLastWeekEndUtc(long lastWeekEndUtc) {
		this.lastWeekEndUtc = lastWeekEndUtc;
	}
	/**
	 * 获取[统计周（当年第几周）]值
	 */
	public int getWeek() {
		return week;
	}
	/**
	 * 设置[统计周（当年第几周）] 值
	 */
	public void setWeek(int week) {
		this.week = week;
	}
	
}
