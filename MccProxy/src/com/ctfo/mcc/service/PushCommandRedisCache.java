package com.ctfo.mcc.service;

import java.sql.SQLException;
import java.util.regex.Pattern;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.Transaction;

import com.ctfo.mcc.dao.RedisConnectionPool;


public class PushCommandRedisCache{
	
	protected static Logger logger = LoggerFactory.getLogger(PushCommandRedisCache.class);
	
//	public ConnectRedis connectRedis = null;
	
	public static int cache_time = 0;
	// 定时定距
	private static Pattern time_distance_ptn = Pattern.compile(".*D_SETP \\{(TYPE:0,RETRY:0,180:\\d+,181:\\d+)\\}");
	// 定时
	private static Pattern time_ptn = Pattern.compile(".*D_SETP \\{(TYPE:0,RETRY:0,180:\\d+)\\}");
	// 定距
	private static Pattern distance_ptn = Pattern.compile(".*D_SETP \\{(TYPE:0,RETRY:0,181:\\d+)\\}");
	 
	public static void pushCommandToRedisCache(String command) {
//		CAITS 156692_1370836697_34520 E001_15290424160 0 D_CTLM {TYPE:10,RETRY:1,VALUE:1|1|1|0|1|5|100|100|100|100} 
		String[] sstr = null;
		String type = null;
		sstr = command.split("\\ ");
		type = sstr[4];
		String macid = sstr[2];// 通讯码
		if(null != type){
			if( "D_CTLM".equals(type)){ // 终端控制指令
				String subType = null;
				String usecontent = sstr[5].substring(1, sstr[5].length() - 1);
				
				String[] parm = usecontent.split(",");
				
				for (int i = 0; i < parm.length; i++) {
					String[] tempKV = parm[i].split(":",2);
					if (tempKV.length == 2){
						if("TYPE".equals( tempKV[0])){
							subType = tempKV[1];
							break;
						}
					}
				}// End for
				
				if("20".equals(subType)){ // 终端升级
					saveCommandToRedis(macid,"D_CTLM_20","_D_CTLM_20",command, cache_time);
					
				}
//				远程锁车指令
				if("26".equals(subType)){
					saveCommandToRedisNotExpire(macid,"D_CTLM_26","_D_CTLM_26",command);
				}
			}else if("D_SETP".equals(type)){ //参数设置指令
				String subType = null;
				String usecontent = sstr[5].substring(1, sstr[5].length() - 1);
				
				String[] parm = usecontent.split(",");
				
				for (int i = 0; i < parm.length; i++) {
					String[] tempKV = parm[i].split(":",2);
					if (tempKV.length == 2){
						if("TYPE".equals( tempKV[0])){
							subType = tempKV[1];
							break;
						}
					}
				}// End for
				
				if("0".equals(subType)){ // 参数设置指令类型
					if(time_distance_ptn.matcher(command).find() 
							|| time_ptn.matcher(command).find() 
							|| distance_ptn.matcher(command).find() ){ // 触发拍照
						saveCommandToRedis(macid,"D_SETP_180","_D_SETP_0_180",command, cache_time);
						
					}else{ // 终端参数设置
						saveCommandToRedis(macid,"D_SETP","_D_SETP_0",command, cache_time);
					}
					
				}else if("14".equals(subType)){ // 电子围栏设置
					Jedis jedis = null;
					try {
						jedis = RedisConnectionPool.getJedisConnection();
					} catch (SQLException e) {
					}
					jedis.select(2); // 下行缓存库
					
					if(jedis.hexists(macid,"D_SETP_14")){
						// 开启一个事务
						Transaction t = jedis.multi();
						t.lpush(macid + "_D_SETP_14", command);
						t.expire(macid, cache_time);
						t.expire(macid + "_D_SETP_14", cache_time);
						t.exec();
					}else{
						// 开启一个事务
						Transaction t = jedis.multi();
						t.hset(macid, "D_SETP_14", macid + "_D_SETP_14");
						t.lpush(macid + "_D_SETP_14", command);
						t.expire(macid, cache_time);
						t.expire(macid + "_D_SETP_14", cache_time);
						//提交一个事务
						t.exec();
					}
					
					RedisConnectionPool.returnJedisConnection(jedis);
//					logger.debug("Save '" + command + "' to redis successfully.");
				}else if("15".equals(subType)){ // 线路绑定
					saveCommandToRedis(macid,"D_SETP_15","_D_SETP_15",command, cache_time);
				}
			}else if("D_GETP".equals(type)){ // 终端参数获取
				saveCommandToRedis(macid,"D_GETP","_D_GETP",command, cache_time);
			}else if("D_REQD".equals(type)){ // 提取行驶记录仪
				String subType = null;
				String usecontent = sstr[5].substring(1, sstr[5].length() - 1);
				
				String[] parm = usecontent.split(",");
				
				for (int i = 0; i < parm.length; i++) {
					String[] tempKV = parm[i].split(":",2);
					if (tempKV.length == 2){
						if("TYPE".equals( tempKV[0])){
							subType = tempKV[1];
							break;
						}
					}
				}// End for
				
				if("4".equals(subType)){
					saveCommandToRedis(macid,"D_REQD","_D_REQD_4",command, cache_time);
				}
			}else if("D_SNDM".equals(type)){ // 终端参数获取
				String[] array = command.split("CAITS");
				if(array.length == 2){
					String expire  = array[0].trim(); 
					if(StringUtils.isNumeric(expire)){
						int expireSeconds = Integer.parseInt(expire) * 60 * 60; // 缓存时间单位是小时需要改成秒
						saveCommandToRedis(macid, "D_SNDM", "_D_SNDM", "CAITS" + array[1], expireSeconds);
					}
				}
			}
		}
	}
	
	/**
	 * 
	 * @param macid
	 * @param type
	 * @param subType
	 * @param command
	 */
	private static void saveCommandToRedis(String macid, String type, String subType, String command, int expireSeconds) {
		Jedis jedis = null;
		long start = System.currentTimeMillis();
		long s1 = start;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			s1 = System.currentTimeMillis();
			jedis.select(2); // 下行缓存库
			if (jedis.hexists(macid, type)) {
				// 开启一个事务
				Transaction t = jedis.multi();
				t.set(macid + subType, command);
				t.expire(macid, expireSeconds);
				t.expire(macid + subType, expireSeconds);
				// 提交一个事务
				t.exec();
			} else {
				// 开启一个事务
				Transaction t = jedis.multi();
				t.hset(macid, type, macid + subType);
				t.set(macid + subType, command);
				t.expire(macid, expireSeconds);
				t.expire(macid + subType, expireSeconds);
				// 提交一个事务
				t.exec();
			}

		} catch (Exception e) {
			if (jedis != null) {
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("缓存指令信息异常:" + e.getMessage(), e);
		} finally {
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("缓存指令完成, 耗时:[{}], 获取连接耗时:[{}]ms, 业务处理耗时:[{}]ms, 缓存内容:[{}]", end - start, s1 - start, end - s1, command);
	}
	/*****************************************
	 * <li>描        述：缓存下发指令(无过期时间) 		</li><br>
	 * <li>时        间：2013-9-5  下午3:04:41	</li><br>
	 * <li>参数： @param macid		服务编号
	 * <li>参数： @param type		指令类型
	 * <li>参数： @param subType	子类型
	 * <li>参数： @param command	指令	</li><br>
	 * 
	 *****************************************/
	private static void saveCommandToRedisNotExpire(String macid, String type, String subType, String command) {
		Jedis jedis = null;
		long start = System.currentTimeMillis();
		long s1 = start;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			s1 = System.currentTimeMillis();
			jedis.select(2); // 下行缓存库
			if (jedis.hexists(macid, type)) {
				// 开启一个事务
				Transaction t = jedis.multi();
				t.set(macid + subType, command);
				// 提交一个事务
				t.exec();
			} else {
				// 开启一个事务
				Transaction t = jedis.multi();
				t.hset(macid, type, macid + subType);
				t.set(macid + subType, command);
				// 提交一个事务
				t.exec();
			}

			RedisConnectionPool.returnJedisConnection(jedis);
//			logger.info("Save '" + command + "' to redis successfully.");
		} catch (Exception e) {
			if (jedis != null) {
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("缓存指令异常:" + e.getMessage(), e);
		} finally {
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("缓存指令完成, 耗时:[{}], 获取连接耗时:[{}]ms, 业务处理耗时:[{}]ms, 缓存内容:[{}]", end - start, s1 - start, end - s1, command);
	}

	/**
	 * 获得cache_time的值
	 * @return the cache_time cache_time  
	 */
	public static int getCache_time() {
		return cache_time;
	}
	/**
	 * 设置cache_time的值
	 * @param cache_time cache_time  
	 */
	public static void setCache_time(int cache_time) {
		PushCommandRedisCache.cache_time = cache_time;
	}
	

}
