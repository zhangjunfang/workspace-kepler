package com.ruishenh.utils;

import redis.clients.jedis.JedisPool;

public class JedisUtil {

	private static JedisPool pool;

	static JedisPool getPool() {
		if (pool == null) {
			pool = new JedisPool(getRedisAddress(), getRedisPort());
		}
		return pool;
	}

	static String getRedisAddress() {
		return "10.57.41.19";
	}

	static int getRedisPort() {
		return 6379;
	}
}
