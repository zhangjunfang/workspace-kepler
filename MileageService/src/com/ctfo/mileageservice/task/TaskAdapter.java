package com.ctfo.mileageservice.task;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


public abstract class TaskAdapter implements Runnable {
	protected Logger log = LoggerFactory.getLogger(getClass());
	
	protected String type = "";
	/** 任务名称 */
	protected String name = ""; 
	/** 任务 线程数 */
	protected int threadNum = 0; 
	/** 自定义配置文件映射表 */
	protected Map<String, String> config = null;
	/** 状态	 */
	protected String stats = "";
	
	/**
	 * 初始化任务
	 */
	public abstract void init();
	
	/**
	 * 执行任务
	 * @throws Exception 
	 */
	public abstract void execute() throws Exception;
	
	/**
	 * 默认执行入口，调用execute方法
	 */
	public void run() {
		try {
			execute(); //执行任务
		} catch (Exception e) {
			log.error(e.getMessage(), e);
		} 
	}

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public Map<String, String> getConf() {
		return config;
	}
	public void setConfig(Map<String, String> conf) {
		this.config = conf;
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
	 * @return the type
	 */
	public String getType() {
		return type;
	}

	/**
	 * @param type the type to set
	 */
	public void setType(String type) {
		this.type = type;
	}
	
	
}
