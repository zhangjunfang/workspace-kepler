//package com.caits.analysisserver.database;
//
//import com.ctfo.redis.JedisDataSource;
//import com.ctfo.redis.pool.JedisSerPool;
//import com.ctfo.redis.service.JedisService;
//import com.ctfo.redis.service.impl.JedisServiceImpl;
//
///*****
// * Redis服务
// * @author hushuang
// *
// */
//public class RedisServer {
//	
//	private static JedisSerPool jedisSerPool = null;
//	private static JedisService jedisService = null;
//	
//	/****
//	 * 初始化连接池
//	 * @param host
//	 * @param port
//	 * @param pwd
//	 * @param timeout
//	 * @param maxActive
//	 * @param maxIdle
//	 * @param maxWait
//	 */
//	public static void initRedisService(final String host,final int port,final String pwd,final int maxActive,final int maxIdle,final long maxWait,final int redisTimeout){
//		jedisSerPool = RedisServer.getPool(RedisServer.getSource( host, port, pwd, maxActive, maxIdle, maxWait, redisTimeout));
//		jedisService = new JedisServiceImpl(jedisSerPool, jedisSerPool.getJedisPool());
//	}
//	
//	
//	/****
//	 * 获取JedisService服务
//	 * @return
//	 */
//	public static JedisService getJedisServiceInstance(){
//		if(jedisService ==null){
//			return null;
//		}else{
//			return jedisService;
//		}
//	}
//	
//	public static JedisDataSource getSource( String host, int port, String pwd, int maxActive, int maxIdle, long maxWait, int redisTimeout) {
//		JedisDataSource jedisDataSource = new JedisDataSource();
//		jedisDataSource.setHost(host);
//		jedisDataSource.setPort(port);
//		jedisDataSource.setPass(pwd);
//		jedisDataSource.setMaxActive(maxActive);
//		jedisDataSource.setMaxIdle(maxIdle);
//		jedisDataSource.setMaxWait(maxWait);
//		return jedisDataSource;
//	}
//
//	public static JedisSerPool getPool(JedisDataSource jedisDataSource) {
//		JedisSerPool jedisSerPool = new JedisSerPool(jedisDataSource);
//		return jedisSerPool;
//	}
//
//
//
//}
