package com.ctfo.savecenter.dao;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.redis.core.RedisAdapter;
import com.ctfo.redis.pool.JedisConnectionPool;
import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.util.CDate;

public class CommandManagerRedisDBAdapter {
	
	private static final Logger logger = LoggerFactory.getLogger(CommandManagerRedisDBAdapter.class);
	
	/*****
	 * 指令回复通应应答，更新REDIS缓存状态 TODO 	JedisConnectionServer.updatePhotoCommandStatus
	 * @param app
	 */
	public void updateCommand(Map<String,String> app){
		String status = app.get("RET");
		String seq = app.get(Constant.SEQ);
		String vid = app.get(Constant.VID);
		
		Jedis jedis = null;
		try{
//			jedis = RedisConnectionPool.getJedisConnection();
			jedis = JedisConnectionPool.getJedisConnection();
			jedis.select(0); // Index 0 缓存回复状态
			if(!seq.matches("\\d+")){ // 只解SEQ包含'_' 
					jedis.set(seq, status);
					jedis.expire(seq, CDate.getIntvalTime()); // 失效时间为凌晨过后
			}else{ // 更新照片状态
				String ky = vid  + ":" + seq;
				if(jedis.exists(ky)){
					String value = jedis.get(ky);
					String[] arr = value.split("#");
					if(3 == arr.length){
						jedis.set(ky,  status + "#" + "0" + "#" + arr[2]); // 状态或URL#照片主键#几路
						jedis.expire(ky,CDate.getIntvalTime());
					}
				}
			}
		}catch(Exception ex){
			JedisConnectionPool.returnBrokenResource(jedis);
			logger.error("Control command connect redis server to error: " + ex.getMessage());
		}finally{
			if(null != jedis)
//				RedisConnectionPool.returnJedisConnection(jedis);
				JedisConnectionPool.returnJedisConnection(jedis);;
		}
	}
	
	/*****
	 * 更新图片状态   JedisConnectionServer.updateCommandStatus
	 * @param app
	 */
	public void updatePicCommand(Map<String,String> app){
		String status = app.get("RET");
		String seq = app.get(Constant.SEQ);
		String vid = app.get(Constant.VID);
		RedisAdapter.updatePhotoCommandStatus(vid, seq, status, CDate.getIntvalTime());

//		Jedis jedis = null;
//		try{
////			jedis = RedisConnectionPool.getJedisConnection();
//			jedis = JedisConnectionPool.getJedisConnection();
//			jedis.select(0); // Index 0 缓存回复状态
//			if(jedis.exists(vid + ":" + seq)){
//				jedis.set(vid + ":" + seq, status);
//			}else{
//				jedis.set(vid + ":" + seq, status);
//				jedis.expire(vid + ":" + seq, CDate.getIntvalTime()); // 失效时间为凌晨过后
//			}
//		}catch(Exception ex){
//			logger.error("Control command connect redis server to error: "+ ex.getMessage());
//		}finally{
//			if(null != jedis)
//			RedisConnectionPool.returnJedisConnection(jedis);
//		}
	}
	
	
}
