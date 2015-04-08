/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncService		</li><br>
 * <li>文件名称：com.ctfo.syncservice.dao RedisDataSource.java	</li><br>
 * <li>时        间：2013-12-16  上午11:15:44	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.dao;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.JedisPoolConfig;

/*****************************************
 * <li>描        述：redis数据库接口		
 * 
 *****************************************/
public class RedisDataSource {
	/** 连接池  */
	private JedisPool jedisPool = null;
	/** 连接REDIS IP地址  */
	private String redis_host= "";
	/** 连接REDIS端口号 (默认6379端口)  */
	private int redis_port = 6379;
	/** 连接超时时间  */
	private int timeOut = 1000;
	/** 连接密码  */
	private String redis_password = null;
	/** 最大连接数  */
	private int maxActive = 1000;
	/** 最大空闲的连接数  */
	private int maxIdle = 20;
	/** 当池内没有返回对象时，最大等待时间  */
	private int maxWait = 50;
	
	/****
	 * 初始化连接池
	 * @param host
	 * @param port
	 * @param pwd
	 * @param timeout
	 * @param maxActive
	 * @param maxIdle
	 * @param maxWait
	 */
	public void init(){
		JedisPoolConfig jedisPoolConfig = initJedisConfig(maxActive,maxIdle,maxWait);
		this.jedisPool = new JedisPool(jedisPoolConfig, redis_host, redis_port,timeOut,redis_password);
		
	}

	/***
	 * 初始化配置连接
	 * @param maxActive
	 * @param maxIdle
	 * @param maxWait
	 * @return
	 */
	private JedisPoolConfig initJedisConfig(int maxActive,int maxIdle,long maxWait){
		JedisPoolConfig jedisPoolConfig = new JedisPoolConfig();  
        // 控制一个pool最多有多少个状态为idle的jedis实例  
        jedisPoolConfig.setMaxActive(maxActive);   
        // 最大能够保持空闲状态的对象数  
        jedisPoolConfig.setMaxIdle(maxIdle);  
        // 超时时间  
        jedisPoolConfig.setMaxWait(maxWait);  
        // 在borrow一个jedis实例时，是否提前进行alidate操作；如果为true，则得到的jedis实例均是可用的；  
        jedisPoolConfig.setTestOnBorrow(true);   
        // 在还会给pool时，是否提前进行validate操作  
        jedisPoolConfig.setTestOnReturn(true);  
        jedisPoolConfig.setTestWhileIdle(true);
        jedisPoolConfig.setTimeBetweenEvictionRunsMillis(60000);
        return jedisPoolConfig;  
	}
	
	/****
	 * 获取Redis连接
	 * @return
	 */
	public Jedis getJedisConnection(){
		return this.jedisPool.getResource();
	}
	
	/****
	 * 放回连接池
	 * @param jedis
	 */
	public void returnJedisConnection(Jedis jedis){
		this.jedisPool.returnResource(jedis);
	}
	
	/*****************************************
	 * <li>描        述：消耗redis连接 		</li><br>
	 * <li>时        间：2013-8-27  下午1:52:00	</li><br>
	 * <li>参数： @param jedis			</li><br>
	 * 
	 *****************************************/
	public void returnBrokenResource(Jedis jedis){
		this.jedisPool.returnBrokenResource(jedis);
	}
	public String getRedis_host() {
		return redis_host;
	}
	public void setRedis_host(String redisHost) {
		redis_host = redisHost;
	}
	public int getRedis_port() {
		return redis_port;
	}
	public void setRedis_port(int redisPort) {
		redis_port = redisPort;
	}
	public int getTimeOut() {
		return timeOut;
	}
	public void setTimeOut(int timeOut) {
		this.timeOut = timeOut;
	}
	public String getRedis_password() {
		return redis_password;
	}
	public void setRedis_password(String redisPassword) {
		redis_password = redisPassword;
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

	/*****************************************
	 * <li>描        述：关闭接口 		</li><br>
	 * <li>时        间：2013-12-16  上午11:33:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void destroy() {
		this.jedisPool.destroy();
	}
	
//	public static void main(String[] args) {
//		NewsletterRedisDataSource ds = new NewsletterRedisDataSource();
//		ds.setRedis_host("192.168.100.50");
//		ds.setRedis_port(6379);
//		ds.init();
//		Jedis jedis = ds.getJedisConnection();
//		jedis.set("k1test", "vitest");
//		
//		
//		ds.returnJedisConnection(jedis);
//	}
}
