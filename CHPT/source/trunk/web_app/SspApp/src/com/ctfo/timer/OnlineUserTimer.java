package com.ctfo.timer;

import java.util.Set;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.context.FrameworkContext;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

public class OnlineUserTimer {

	private String JedisCacheTime = (String) CustomizedPropertyPlaceholderConfigurer.getContextProperty("JedisCacheTime");
	
	public void timerOnlineUser(){
    	JedisPool jedisPool = (JedisPool)FrameworkContext.getBean("writeJedisPool"); // 获取Redis连接池
		Jedis jd = jedisPool.getResource();// 获取Redis连接
//		jd.select(2);
		try {
			Set<String> names = jd.hkeys("LOGIN");// 获取用户索引列表
			long curtime = System.currentTimeMillis()/1000;// 系统当前时间
			if(names !=null && !names.equals("")){
				for (String name : names) {// 遍历用户索引列表
					long loginTime = Long.parseLong(jd.hget("LOGIN", name))/1000;
					long cacheTime = Long.parseLong(JedisCacheTime);
					if (curtime-loginTime>cacheTime) {  // 长时间没有操作，删除在线信息
						jd.hdel("LOGIN", name); // 删除在线用户信息	
					}
				}
			}
			
		} catch(Exception e) {
			e.printStackTrace();
		} finally {
			jedisPool.returnResource(jd);// 释放连接
		}
	}
}
