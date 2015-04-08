package com.ctfo.syn.dao;

import com.ctfo.redis.pool.JedisSerPool;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;
import com.ctfo.unifiedstorage.service.impl.JedisUnifiedStorageServiceImpl;

/*****
 * Redis服务
 * @author hushuang
 *
 */
public class RedisServer {
	
	private static JedisSerPool jedisSerPool = null;
	private static JedisUnifiedStorageService jedisService = null;
	
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
	public static void initRedisService(final String host,final int port,final String password,final int maxActive,final int maxIdle,final long maxWait,final int redisTimeout){
		jedisSerPool = new JedisSerPool(host, port, password, maxActive, maxIdle, maxWait, redisTimeout);
		jedisService = new JedisUnifiedStorageServiceImpl(jedisSerPool, jedisSerPool.getJedisPool());
	}
	
	/****
	 * 获取JedisService服务
	 * @return
	 */
	public static JedisUnifiedStorageService getJedisServiceInstance(){ 
		if(jedisService ==null){
			return null;
		}else{
			return jedisService;
		}
	}
}
