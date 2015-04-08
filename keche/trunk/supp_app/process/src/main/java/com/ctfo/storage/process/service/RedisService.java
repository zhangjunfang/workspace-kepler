package com.ctfo.storage.process.service;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.storage.process.dao.RedisDataSource;

/**
 * RedisService
 * 缓存服务器接口
 * 
 */
public class RedisService {
	/**	日志	*/
	private static final Logger log = LoggerFactory.getLogger(RedisService.class);
	/**	连接实例	*/
	private static Jedis jedis = null;
	/**
	 * 
	 */
	public RedisService() {
		jedis = RedisDataSource.getInstance().getJedisConnection();
	}

	/**
	 * 存储实时位置
	 * @param list
	 */
	public void saveRealTimeLocationList(Map<String, String> map) {
		try {
			if(jedis != null){
				jedis.hmset("HD_VEHICLE", map);
			} else {
				jedis = RedisDataSource.getInstance().getJedisConnection();
				jedis.hmset("HD_VEHICLE", map);
			}
		} catch (Exception e) {
			log.error("存储实时位置异常:" + e.getMessage(), e);
		}  
	}
}
