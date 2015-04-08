package com.ctfo.filesaveservice.task;

import org.junit.Test;

import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.model.RedisProperties;

public class DriverTaskTest {
	RedisConnectionPool pool = null;
	public DriverTaskTest(){
		RedisProperties redisProperties = new RedisProperties();
		redisProperties.setHost("192.168.200.115");  
		redisProperties.setPort(6379); 
		redisProperties.setPwd("kcpt"); 
		pool = new RedisConnectionPool(redisProperties);
	}
	@Test
	public void testInit() { 
		DriverTask dt = new DriverTask();
		dt.init();
	}

	@Test
	public void testExecute() {
		DriverTask dt = new DriverTask();
		dt.execute(); 
	}

}
