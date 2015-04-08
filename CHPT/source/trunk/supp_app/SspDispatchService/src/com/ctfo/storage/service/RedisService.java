package com.ctfo.storage.service;

import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.storage.dao.RedisDataSource;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 缓存服务器接口<br>
 * 描述： 缓存服务器接口<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-28</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class RedisService {

	/** 日志 */
	private static final Logger logger = LoggerFactory.getLogger(RedisService.class);

	public RedisService() {
	}

	/**
	 * 根据车厂id获取服务站id
	 * 
	 * @param sapId
	 * @return
	 * @throws Exception
	 */
	public String getStationId(String sapId) throws Exception {
		Jedis jedis = null;
		String stationId = null;
		try {
			jedis = RedisDataSource.getInstance().getJedisConnection();
			jedis.select(1);
			stationId = jedis.hget("HS_STATIONID", sapId);
		} catch (Exception e) {
			logger.error("获取服务站id异常:" + e.getMessage(), e);
		} finally {
			RedisDataSource.getInstance().returnJedisConnection(jedis);
		}
		return stationId;
	}

	/**
	 * 获取登录用户密码
	 * 
	 * @param username
	 * @return
	 */
	public String getLoginInfo(String username) {
		Jedis jedis = null;
		String password = null;
		try {
			jedis = RedisDataSource.getInstance().getJedisConnection();
			jedis.select(1);
			password = jedis.hget("HS_LOGIN", username);
		} catch (Exception e) {
			logger.error("获取登录密码异常:" + e.getMessage(), e);
		} finally {
			RedisDataSource.getInstance().returnJedisConnection(jedis);
		}
		return password;
	}

	/**
	 * 模糊查询key
	 * 
	 * @param likeKey
	 * @return
	 */
	public Set<String> getkeysLike(String likeKey) {
		Jedis jedis = null;
		Set<String> keys = null;
		try {
			jedis = RedisDataSource.getInstance().getJedisConnection();
			jedis.select(1);
			keys = jedis.keys(likeKey + "*");
		} catch (Exception e) {
			logger.error("模糊查询key异常:" + e.getMessage(), e);
		} finally {
			RedisDataSource.getInstance().returnJedisConnection(jedis);
		}
		return keys;
	}

	/**
	 * 根据服务站id获取车厂转发消息数据
	 * 
	 * @param key
	 * @return
	 */
	public Set<String> getStationList(String key) {
		Jedis jedis = null;
		Set<String> messageForward = null;
		try {
			jedis = RedisDataSource.getInstance().getJedisConnection();
			jedis.select(1);
			messageForward = jedis.smembers(key);
		} catch (Exception e) {
			logger.error("根据服务站id获取车厂转发消息数据异常:" + e.getMessage(), e);
		} finally {
			RedisDataSource.getInstance().returnJedisConnection(jedis);
		}
		return messageForward;
	}

	/**
	 * 根据key删除
	 * 
	 * @param key
	 */
	public void delKey(String key) {
		Jedis jedis = null;
		try {
			jedis = RedisDataSource.getInstance().getJedisConnection();
			jedis.select(1);
			jedis.del(key);
		} catch (Exception e) {
			logger.error("删除key异常:" + e.getMessage(), e);
		} finally {
			RedisDataSource.getInstance().returnJedisConnection(jedis);
		}
	}
}
