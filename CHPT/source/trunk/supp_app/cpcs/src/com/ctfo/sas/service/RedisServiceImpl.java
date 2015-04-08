package com.ctfo.sas.service;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

public class RedisServiceImpl implements RedisService {
	
	private static final Logger logger = LoggerFactory.getLogger(RedisServiceImpl.class);

	//redis主连接池 
	private JedisPool writeJedisPool;
	
	//redis从连接池 
	private JedisPool readJedisPool;
	
	//redis 默认库
	String redisDefaultDb = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("REDIS.default.db");
	int redisDefaultDb_int = new Integer(redisDefaultDb).intValue();

	/**
	 * 根据车厂id获取服务站id
	 * 
	 * @param sapId
	 * @return
	 * @throws Exception
	 */
	public String getStationId(String sapId) {
		Jedis jedis = readJedisPool.getResource();
		jedis.select(redisDefaultDb_int);
		String stationId = jedis.hget("HS_STATIONID", sapId);
		logger.info("根据服务站SAP编码："+sapId+" 获取服务站ID："+stationId);
		return stationId;
	}
	
	/**
	 * @description:把报文塞到redis 里
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年12月4日下午6:18:30
	 * @modifyInformation：
	 */
	public void storageRedis(String key,String members){
		Jedis jedis = writeJedisPool.getResource();
		jedis.select(redisDefaultDb_int);
		jedis.sadd(key, members);
		logger.info("塞到redis里的key为："+key+" 报文为："+members);
	}

	public JedisPool getReadJedisPool() {
		return readJedisPool;
	}

	public void setReadJedisPool(JedisPool readJedisPool) {
		this.readJedisPool = readJedisPool;
	}

	public JedisPool getWriteJedisPool() {
		return writeJedisPool;
	}

	public void setWriteJedisPool(JedisPool writeJedisPool) {
		this.writeJedisPool = writeJedisPool;
	}

}
