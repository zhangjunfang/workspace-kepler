package com.ruishenh.utils;

import org.junit.Before;
import org.junit.Test;

import redis.clients.jedis.Jedis;

public class JedisUtilTest {

	Jedis jedis = new Jedis("10.57.41.19");

	@Before
	public void setUp() throws Exception {
		// jedis=JedisUtil.getPool().getResource();
	}

	@Test
	public void test() {
		System.out.println("test:"+jedis.get("test"));
	}
}
