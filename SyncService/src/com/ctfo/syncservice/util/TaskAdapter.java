package com.ctfo.syncservice.util;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.syncservice.dao.OracleDataSource;
import com.ctfo.syncservice.dao.RedisDataSource;

public abstract class TaskAdapter implements Runnable {
	protected Logger log = LoggerFactory.getLogger(getClass());
	/** 任务名称 */
	protected String name = ""; 
	/** 自定义配置文件映射表 */
	protected Map<String, String> conf = null;
	/** 数据库连接管理类 */
	protected OracleDataSource oracle = null;
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
//		log.info("任务[" + this.getName() +"]开始执行...");
//		long st = System.currentTimeMillis();
		try {
			execute(); //执行任务
		} catch (Exception e) {
			log.error(e.getMessage(), e);
		} 
//		long et = System.currentTimeMillis();
//		log.info("任务[" + this.getName() +"]执行完成，"+stats+"共耗时：" + (et - st) + "ms");
	}

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public Map<String, String> getConf() {
		return conf;
	}
	public void setConf(Map<String, String> conf) {
		this.conf = conf;
	}
	public OracleDataSource getOracle() {
		return oracle;
	}
	public void setOracle(OracleDataSource oracle) {
		this.oracle = oracle;
	}
//	}
	public RedisDataSource getRedis() {
		return redis;
	}
	public void setRedis(RedisDataSource redis) {
		this.redis = redis;
	}
}
