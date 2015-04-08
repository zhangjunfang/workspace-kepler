package com.ctfo.storage.statistics.util;

import java.util.HashMap;
import java.util.Map;

import com.ctfo.storage.statistics.dao.MySqlDataSource;
import com.ctfo.storage.statistics.dao.RedisDataSource;

/**
 * 任务抽象类（所以定时任务都继承此类）
 *
 */
public abstract class TaskAdapter implements Runnable {
	/** 任务名称 */
	protected String name = ""; 
	/** 自定义配置文件映射表 */
	protected Map<String, String> config = new HashMap<String, String>();
	/** 数据库连接管理类 */
	protected MySqlDataSource mysql = null;
	/** Redis缓存服务连接(池)管理器 */
	protected RedisDataSource redis = null;
	/** 状态	 */
	protected String stats = "";
	
	/**
	 * 初始化任务
	 */
	public abstract void init();
	
	/**
	 * 执行任务
	 */
	public abstract void execute();
	
	/**
	 * 默认执行入口，调用execute方法
	 */
	public void run() {
		execute(); //执行任务
	}

	/**
	 * @return 获取 任务名称
	 */
	public String getName() {
		return name;
	}

	/**
	 * 设置任务名称
	 * @param name 任务名称 
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * @return 获取 自定义配置文件映射表
	 */
	public Map<String, String> getConfig() {
		return config;
	}

	/**
	 * 设置自定义配置文件映射表
	 * @param config 自定义配置文件映射表 
	 */
	public void setConfig(String key, String value) {
		this.config.put(key, value);
	}

	/**
	 * @return 获取 数据库连接管理类
	 */
	public MySqlDataSource getMysql() {
		return mysql;
	}

	/**
	 * 设置数据库连接管理类
	 * @param mysql 数据库连接管理类 
	 */
	public void setMysql(MySqlDataSource mysql) {
		this.mysql = mysql;
	}

	/**
	 * @return 获取 Redis缓存服务连接(池)管理器
	 */
	public RedisDataSource getRedis() {
		return redis;
	}

	/**
	 * 设置Redis缓存服务连接(池)管理器
	 * @param redis Redis缓存服务连接(池)管理器 
	 */
	public void setRedis(RedisDataSource redis) {
		this.redis = redis;
	}

	/**
	 * @return 获取 状态
	 */
	public String getStats() {
		return stats;
	}

	/**
	 * 设置状态
	 * @param stats 状态 
	 */
	public void setStats(String stats) {
		this.stats = stats;
	}
	
}
