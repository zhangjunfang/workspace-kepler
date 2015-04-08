package com.ctfo.storage.command.service;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.storage.command.dao.RedisDataSource;



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
	 * 存储组织树
	 * @param list
	 */
	public void saveOrgTreeList(Map<String, String> map) {
		try {
			if(jedis != null){
				jedis.hmset("HS_ENT_NODE", map);
			} else {
				jedis = RedisDataSource.getInstance().getJedisConnection();
				jedis.hmset("HS_ENT_NODE", map);
			}
		} catch (Exception e) {
			log.error("存储组织树异常:" + e.getMessage(), e);
		}  
	}
	
	/**
	 * 存储码表
	 * @param list
	 */
	public void saveGeneralCode(String jsonResult) {
		try {
			if(jedis != null){
				jedis.set("MEMCACHE_GENERALCODE",jsonResult);
			} else {
				jedis = RedisDataSource.getInstance().getJedisConnection();
				jedis.set("MEMCACHE_GENERALCODE",jsonResult);
			}
		} catch (Exception e) {
			log.error("存储码表异常:" + e.getMessage(), e);
		}  
	}
}
