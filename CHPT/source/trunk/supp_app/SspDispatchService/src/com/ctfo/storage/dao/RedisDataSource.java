package com.ctfo.storage.dao;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.JedisPoolConfig;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 初始化连接Redis服务<br>
 * 描述： 初始化连接Redis服务<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-27</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class RedisDataSource {

	/** redis数据源单例模式 */
	private static RedisDataSource redisDataSource = new RedisDataSource();

	/** 连接池 */
	private static JedisPool jedisPool = null;

	/** 主机地址 */
	private String host;

	/** 端口号 */
	private Integer port;

	/** 密码 */
	private String pass;

	/** 超时时间 */
	private Integer timeOut = 3000;

	/** 最大连接数 */
	private int maxActive = 1000;

	/** 最大空闲的连接数 */
	private int maxIdle = 20;

	/** 最大等待时间 */
	private int maxWait = 3000;

	public static RedisDataSource getInstance() {
		if (redisDataSource == null) {
			redisDataSource = new RedisDataSource();
		}
		return redisDataSource;
	}

	public void init() {
		JedisPoolConfig jedisPoolConfig = new JedisPoolConfig();
		jedisPoolConfig.setMaxTotal(maxActive); // 控制一个pool最多有多少个状态为idle的jedis实例
		jedisPoolConfig.setMaxIdle(maxIdle); // 最大能够保持空闲状态的对象数
		jedisPoolConfig.setMaxWaitMillis(maxWait);// 超时时间 单位:毫秒
		jedisPoolConfig.setTestOnBorrow(true); // 如果为true，则得到的jedis实例均是可用的；
		jedisPoolConfig.setTestOnReturn(true); // 在还会给pool时，是否提前进行validate操作

		jedisPool = new JedisPool(jedisPoolConfig, host, port, timeOut, pass);
	}

	/**
	 * 获取Redis连接
	 * 
	 * @return
	 */
	public Jedis getJedisConnection() {
		return jedisPool.getResource();
	}

	/**
	 * 放回连接池
	 * 
	 * @param jedis
	 */
	public void returnJedisConnection(Jedis jedis) {
		jedisPool.returnResource(jedis);
	}

	/**
	 * 销毁redis连接
	 * 
	 * @param jedis
	 */
	public void returnBrokenResource(Jedis jedis) {
		jedisPool.returnBrokenResource(jedis);
	}

	/**
	 * 关闭接口
	 */
	public void destroy() {
		jedisPool.destroy();
	}

	/***************** GET AND SET *****************/

	public String getHost() {
		return host;
	}

	public void setHost(String host) {
		this.host = host;
	}

	public Integer getPort() {
		return port;
	}

	public void setPort(Integer port) {
		this.port = port;
	}

	public String getPass() {
		return pass;
	}

	public void setPass(String pass) {
		this.pass = pass;
	}

	public Integer getTimeOut() {
		return timeOut;
	}

	public void setTimeOut(Integer timeOut) {
		this.timeOut = timeOut;
	}

	public int getMaxActive() {
		return maxActive;
	}

	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}

	public int getMaxIdle() {
		return maxIdle;
	}

	public void setMaxIdle(int maxIdle) {
		this.maxIdle = maxIdle;
	}

	public int getMaxWait() {
		return maxWait;
	}

	public void setMaxWait(int maxWait) {
		this.maxWait = maxWait;
	}
}
