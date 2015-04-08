package com.ctfo.trackservice.task;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.TimeUnit;

/**
 * 文件名：TaskConfiger.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-11-11上午10:31:41
 * 
 */
public class TaskConfiger {
	/** 任务名称 */
	private String name = "";
	/** 任务执行线程数*/
	private int threadNum = 1;
	
	/** 任务实现类 */
	private String impClass = "";
	
	/** 任务描述 */
	private String desc = "";
	
	/** 时间单位 */
	private TimeUnit unit = TimeUnit.MINUTES;

	/** 周期时间值 */
	private String period = "";
	
	/** 延时执行时长（单位同时间单位） */
	private String delay = "";
	
	/** 依赖 */
	private String depend = "";
	
	
	/** 用户自定义配置信息 */
	private Map<String, String> config = new HashMap<String, String>();
	
	//-------------------GETER SETER---------------------------
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getImpClass() {
		return impClass;
	}

	public void setImpClass(String impClass) {
		this.impClass = impClass;
	}

	public String getDesc() {
		return desc;
	}

	public void setDesc(String desc) {
		this.desc = desc;
	}

	public TimeUnit getUnit() {
		return unit;
	}

	public void setUnit(TimeUnit unit) {
		this.unit = unit;
	}
	
	public void setUnit(String unit) {
		if ("day".equals(unit)) this.unit = TimeUnit.DAYS;
		else if ("hour".equals(unit)) this.unit = TimeUnit.HOURS;
		else if ("minute".equals(unit)) this.unit = TimeUnit.MINUTES;
		else if ("second".equals(unit)) this.unit = TimeUnit.SECONDS;
		else this.unit = TimeUnit.MINUTES;
	}

	public String getPeriod() {
		return period;
	}

	public void setPeriod(String period) {
		this.period = period;
	}

	/**
	 * 获得用户自定义配置信息的值
	 * @return the config 用户自定义配置信息  
	 */
	public Map<String, String> getConfig() {
		return config;
	}
	/**
	 * 获得用户自定义配置信息的值
	 * @return the config 用户自定义配置信息  
	 */
	public String getConfig(String key) { 
		return config.get(key);
	}
	/**
	 * 设置用户自定义配置信息的值
	 * @param config 用户自定义配置信息   
	 */
	public void setConfig(String key, String value) {
		this.config.put(key, value);
	}

	public String getDelay() {
		return delay;
	}

	public void setDelay(String delay) {
		this.delay = delay;
	}
	
	/**
	 * @return the threadNum
	 */
	public int getThreadNum() {
		return threadNum;
	}

	/**
	 * @param threadNum the threadNum to set
	 */
	public void setThreadNum(int threadNum) {
		this.threadNum = threadNum;
	}

	/**
	 * @return the depend
	 */
	public String getDepend() {
		return depend;
	}

	/**
	 * @param depend the depend to set
	 */
	public void setDepend(String depend) {
		this.depend = depend;
	}
	
	
}
