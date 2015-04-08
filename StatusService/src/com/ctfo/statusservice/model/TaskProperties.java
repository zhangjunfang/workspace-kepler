package com.ctfo.statusservice.model;

import java.util.concurrent.TimeUnit;

public class TaskProperties {
	/**	启动延迟时间 		*/
	private long delay;
	/**	任务运行间隔		*/
	private long period;
	/**	时间单位（day:天; hour:小时; minute:分钟 ; second:秒）	*/
	private String unit;
	/**	时间单位（day:天; hour:小时; minute:分钟 ; second:秒）	*/
	private TimeUnit timeUnit;
	/**
	 * 获得启动延迟时间的值
	 * @return the delay 启动延迟时间  
	 */
	public long getDelay() {
		return delay;
	}
	/**
	 * 设置启动延迟时间的值
	 * @param delay 启动延迟时间  
	 */
	public void setDelay(long delay) {
		this.delay = delay;
	}
	/**
	 * 获得任务运行间隔的值
	 * @return the period 任务运行间隔  
	 */
	public long getPeriod() {
		return period;
	}
	/**
	 * 设置任务运行间隔的值
	 * @param period 任务运行间隔  
	 */
	public void setPeriod(long period) {
		this.period = period;
	}
	/**
	 * 获得时间单位（day:天;hour:小时;minute:分钟;second:秒）的值
	 * @return the unit 时间单位（day:天;hour:小时;minute:分钟;second:秒）  
	 */
	public String getUnit() {
		return unit;
	}
	/**
	 * 设置时间单位（day:天;hour:小时;minute:分钟;second:秒）的值
	 * @param unit 时间单位（day:天;hour:小时;minute:分钟;second:秒）  
	 */
	public void setUnit(String unit) {
		if ("day".equals(unit)){
			this.timeUnit = TimeUnit.DAYS;
		}else if ("hour".equals(unit)){
			this.timeUnit = TimeUnit.HOURS;
		}else if ("minute".equals(unit)){
			this.timeUnit = TimeUnit.MINUTES;
		}else if ("second".equals(unit)){
			this.timeUnit = TimeUnit.SECONDS;
		}else{
			this.timeUnit = TimeUnit.MINUTES;
		}
		this.unit = unit;
	}
	/**
	 * 获得时间单位（day:天;hour:小时;minute:分钟;second:秒）的值
	 * @return the timeUnit 时间单位（day:天;hour:小时;minute:分钟;second:秒）  
	 */
	public TimeUnit getTimeUnit() {
		return timeUnit;
	}
	/**
	 * 设置时间单位（day:天;hour:小时;minute:分钟;second:秒）的值
	 * @param timeUnit 时间单位（day:天;hour:小时;minute:分钟;second:秒）  
	 */
	public void setTimeUnit(TimeUnit timeUnit) {
		this.timeUnit = timeUnit;
	}
	
}
