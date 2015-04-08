/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncService		</li><br>
 * <li>文件名称：com.ctfo.syncservice.dao RedisDataSource.java	</li><br>
 * <li>时        间：2013-12-16  上午11:15:44	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.storage.process.dao;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.JedisPoolConfig;

/*****************************************
 * <li>描        述：redis数据库接口		
 * 
 *****************************************/
public class RedisDataSource {
	/** redis数据源单例模式	*/
	private static RedisDataSource redisDataSource = new RedisDataSource();
	/** 连接池  */
	private static JedisPool jedisPool = null;
	/** 主机地址  */
	private String host ;
	/** 端口号	*/
	private Integer port;
	/** 密码  */
	private String pass ;
	/** 超时时间  */
	private Integer timeOut = 3000;
	/** 最大连接数  */
	private int maxActive = 1000;
	/** 最大空闲的连接数  */
	private int maxIdle = 20;
	/** 最大等待时间  */
	private int maxWait = 3000;
	
	public static RedisDataSource getInstance(){
		if(redisDataSource == null){
			redisDataSource = new RedisDataSource();
		}
		return redisDataSource;
	}

	
	public void init() {
		JedisPoolConfig jedisPoolConfig = new JedisPoolConfig();  
		jedisPoolConfig.setMaxTotal(maxActive); // 控制一个pool最多有多少个状态为idle的jedis实例  
        jedisPoolConfig.setMaxIdle(maxIdle);  // 最大能够保持空闲状态的对象数   
        jedisPoolConfig.setMaxWaitMillis(maxWait);// 超时时间  单位:毫秒
        jedisPoolConfig.setTestOnBorrow(true); //如果为true，则得到的jedis实例均是可用的；    
        jedisPoolConfig.setTestOnReturn(true); // 在还会给pool时，是否提前进行validate操作  
		
		jedisPool = new JedisPool(jedisPoolConfig, host, port, timeOut, pass);
	}
	
	/****
	 * 获取Redis连接
	 * @return
	 */
	public Jedis getJedisConnection(){
		return jedisPool.getResource();
	}
	
	/**
	 * 放回连接池
	 * @param jedis
	 */
	public void returnJedisConnection(Jedis jedis){
		jedisPool.returnResource(jedis);
	}
	
	/*****************************************
	 * <li>描        述：销毁redis连接 		</li><br>
	 * <li>时        间：2013-8-27  下午1:52:00	</li><br>
	 * <li>参数： @param jedis			</li><br>
	 * 
	 *****************************************/
	public void returnBrokenResource(Jedis jedis){
		jedisPool.returnBrokenResource(jedis);
	}

	/*****************************************
	 * <li>描        述：关闭接口 		</li><br>
	 * <li>时        间：2013-12-16  上午11:33:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void destroy() {
		jedisPool.destroy();
	}
	/**
	 * @return the 主机地址
	 */
	public String getHost() {
		return host;
	}
	/**
	 * @param 主机地址 the host to set
	 */
	public void setHost(String host) {
		this.host = host;
	}
	/**
	 * @return the 端口号
	 */
	public Integer getPort() {
		return port;
	}
	/**
	 * @param 端口号 the port to set
	 */
	public void setPort(Integer port) {
		this.port = port;
	}
	/**
	 * @return the 密码
	 */
	public String getPass() {
		return pass;
	}
	/**
	 * @param 密码 the pass to set
	 */
	public void setPass(String pass) {
		this.pass = pass;
	}
	/**
	 * @return the 超时时间
	 */
	public Integer getTimeOut() {
		return timeOut;
	}
	/**
	 * @param 超时时间 the timeOut to set
	 */
	public void setTimeOut(Integer timeOut) {
		this.timeOut = timeOut;
	}
	/**
	 * @return the 最大连接数
	 */
	public int getMaxActive() {
		return maxActive;
	}
	/**
	 * @param 最大连接数 the maxActive to set
	 */
	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}
	/**
	 * @return the 最大空闲的连接数
	 */
	public int getMaxIdle() {
		return maxIdle;
	}
	/**
	 * @param 最大空闲的连接数 the maxIdle to set
	 */
	public void setMaxIdle(int maxIdle) {
		this.maxIdle = maxIdle;
	}
	/**
	 * @return the 最大等待时间
	 */
	public int getMaxWait() {
		return maxWait;
	}


	/**
	 * @param 最大等待时间 the maxWait to set
	 */
	public void setMaxWait(int maxWait) {
		this.maxWait = maxWait;
	}

}
