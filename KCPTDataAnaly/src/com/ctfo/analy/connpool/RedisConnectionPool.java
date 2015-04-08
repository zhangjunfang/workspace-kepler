package com.ctfo.analy.connpool;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.JedisPoolConfig;

/*****
 * 初始化连接Redis服务
 * @author robin
 *
 */
public class RedisConnectionPool {
	
	private static JedisPool jedisPool = null;
	
	/***
	 * 初始化配置连接
	 * @param maxActive
	 * @param maxIdle
	 * @param maxWait
	 * @return
	 */
	private static JedisPoolConfig initJedisConfig(int maxActive,int maxIdle,long maxWait){
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
        return jedisPoolConfig;  
	}
	
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
	public static void initRedisConnectionPool(final String host,final int port,final String pwd,final int maxActive,final int maxIdle,final long maxWait,final int redisTimeout){
		JedisPoolConfig jedisPoolConfig = initJedisConfig(maxActive,maxIdle,maxWait);
		//JedisPool
		jedisPool = new JedisPool(jedisPoolConfig, host, port,redisTimeout,pwd);
		
	}
	
	/****
	 * 获取Redis连接
	 * @return
	 */
	public static Jedis getJedisConnection(){
		return jedisPool.getResource();
	}
	
	/****
	 * 放回连接池
	 * @param jedis
	 */
	public static void returnJedisConnection(Jedis jedis){
		jedisPool.returnResource(jedis);
	}
}
