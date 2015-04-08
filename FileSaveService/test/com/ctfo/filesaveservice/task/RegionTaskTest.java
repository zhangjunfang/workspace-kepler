package com.ctfo.filesaveservice.task;

import org.junit.Test;

import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.model.RedisProperties;
import com.ctfo.filesaveservice.util.GridUtil;

public class RegionTaskTest {
	RedisConnectionPool pool = null;
	public RegionTaskTest(){
		RedisProperties redisProperties = new RedisProperties();
		redisProperties.setHost("192.168.200.115");  
		redisProperties.setPort(6379); 
		redisProperties.setPwd("kcpt"); 
		pool = new RedisConnectionPool(redisProperties);
		GridUtil gu = new GridUtil();
		gu.setLen("100000");
		gu.setStartx("44095967");
		gu.setStarty("32137832");
	}
	@Test
	public void testInit() {
		RegionTask rt = new RegionTask();
		rt.init();
	}

	@Test
	public void testExecute() {
		RegionTask rt = new RegionTask();
		rt.setConfig("regionFilePath", "d:/test/region"); 
		rt.init();  
		rt.execute();  
	} 
}
