package com.ctfo.mcc.service;

import java.util.Iterator;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.Transaction;
import redis.clients.jedis.Tuple;

import com.ctfo.mcc.dao.RedisConnectionPool;

public class RedisAreaUpdateStatus{
	protected static Logger logger = LoggerFactory.getLogger(RedisAreaUpdateStatus.class);
	
//	public ConnectRedis RedisConnectionPool = null;

	/****
	 * 获取失效信息
	 */
	public static String[] getAreaLastInvalidTime() {
		Jedis jedis = null;
		String info[] = null;
		
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3); // 库3，存储电子围栏更新状态信息
			
			Set<Tuple> st = jedis.zrangeWithScores("areaStatus", 0, 1);
			Iterator<Tuple> it  = st.iterator();
			if(it.hasNext()){
				Tuple t = it.next();
				double time = t.getScore();
				String seq = t.getElement();
				info = new String[2];
				info[0] = String.valueOf(new Double(time).longValue());
				info[1] = seq;
			}
		} catch (Exception e) {
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis); 
			}
			logger.info("获取缓存信息异常:" + e.getMessage(), e);
		}finally{
			RedisConnectionPool.returnJedisConnection(jedis);
		}
		return info;
	}

	/*****
	 * 设置缓存信息
	 */
	public static void setAreaCache(String seq,String time,String command) {
		Jedis jedis = null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3); // 库3，存储电子围栏更新状态信息
			Transaction t = jedis.multi();
			t.zadd("areaStatus", Double.parseDouble(time), seq); // 缓存失效时间,并以失效时间排序
			t.set(seq, command);
			t.exec();
			logger.debug("Setting area information '" + seq + "," + time + "," + command + "' successfully.");
		} catch (Exception e) {
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis); 
			}
			logger.info("缓存围栏信息异常:" + e.getMessage(), e);
		}finally{
			if(null != jedis){
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}

	/*****
	 * 删除缓存信息
	 */
	public static boolean delAreaSeq(String key) {
		Jedis jedis = null;
		Transaction t = null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3); // 库3，存储电子围栏更新状态信息
			t = jedis.multi();
			t.zrem("areaStatus", key);
			t.del(key);
			t.exec();
		}catch(Exception ex){
			if(null != t){
				t.discard(); // 回滚事务
			}
			logger.error("Removing key=" + key + " from redis for fail.");
			return false;
		}finally{
			if(null != jedis){
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		return true;
	}
	
}
