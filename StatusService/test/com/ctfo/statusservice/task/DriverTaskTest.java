package com.ctfo.statusservice.task;

import org.junit.Test;

import com.ctfo.statusservice.dao.RedisConnectionPool;
import com.ctfo.statusservice.model.RedisProperties;

public class DriverTaskTest {
	RedisConnectionPool pool;
	
	public DriverTaskTest(){
		RedisProperties  pro = new RedisProperties();
		pro.setHost("10.8.3.163");
		pro.setPort(6379);
		pro.setPwd("kcpt");
		pro.setMaxActive(10);
		pro.setMaxIdle(1);
		pro.setMaxWait(3000l);
		pro.setRedisTimeout(3000);
		pool = new RedisConnectionPool(pro); 
	} 
	
	@Test
	public void testInit() {
		DriverTask task = new DriverTask();
		task.init(); 
	}

	@Test
	public void testExecute() {
		DriverTask task = new DriverTask();
		task.execute();
	}

}
