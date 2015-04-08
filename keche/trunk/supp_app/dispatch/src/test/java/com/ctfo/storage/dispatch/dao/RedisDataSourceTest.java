/**
 * 
 */
package com.ctfo.storage.dispatch.dao;

import static org.junit.Assert.*;

import org.junit.Test;

import com.ctfo.storage.dispatch.dao.RedisDataSource;

import redis.clients.jedis.Jedis;

/**
 * @author zjhl
 *
 */
public class RedisDataSourceTest {

	/**
	 * 测试	初始化
	 */
	@Test
	public void testInit() {
		RedisDataSource rds = new RedisDataSource();
		rds.setHost("192.168.100.52");
		rds.setPort(6709);
		rds.setPass("auth");
		rds.init();
		assertNotNull(rds); 
	}

	/**
	 * 测试	获取连接实例
	 */
	@Test
	public void testGetJedisConnection() {
		RedisDataSource rds = new RedisDataSource();
		rds.setHost("192.168.2.111");
		rds.setPort(6379);
		rds.setPass("kcpt");
		rds.init();
		Jedis j = rds.getJedisConnection();
		assertNotNull(j); 
	}

}
