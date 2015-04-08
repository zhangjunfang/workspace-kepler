/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.service RedisService.java	</li><br>
 * <li>时        间：2013-10-23  下午3:30:07	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.service;

import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.CustomIssued;
import com.ctfo.redis.util.Constant;

/*****************************************
 * <li>描        述：redis服务		
 * 
 *****************************************/
public class RedisService {
	private static final Logger logger = LoggerFactory.getLogger(RedisService.class);
	
	 
	public RedisService() {
	}
	
	/*****************************************
	 * <li>描        述：存储多媒体信息 		</li><br>
	 * <li>时        间：2013-9-13  下午2:29:11	</li><br>
	 * <li>参数： @param app			</li><br>
	 * @ 
	 * 
	 *****************************************/
	public void saveRedisMultMedia(Map<String, String> app, int expireTime) {
		if (!app.containsKey(Constant.N125) || null == app.get(Constant.N123) || !Constant.N0.equals(app.get(Constant.N123)) || null == app.get(Constant.N121) || !"0".equals(app.get(Constant.N121))) { // 跳过多媒体事件和不属于平台下发拍照
			return;
		}
		String vid = app.get(Constant.VID);
		Jedis jedis = null;
		try {
			jedis =  RedisConnectionPool.getJedisConnection();
			jedis.select(0);
			StringBuffer value = new StringBuffer();
			value.append(app.get(Constant.N125));
			value.append(Constant.POUND);
			value.append(app.get(Constant.UUID));
			value.append(Constant.POUND);
			value.append(app.get(Constant.N124));
			jedis.lpush(vid + Constant.MEDIA, value.toString());
			jedis.expire(vid + Constant.MEDIA, expireTime);
		} catch (Exception ex) {
			RedisConnectionPool.returnBrokenResource(jedis);
			logger.error("存储多媒体信息:" + ex.getMessage(), ex);
		} finally {
			RedisConnectionPool.returnJedisConnection(jedis);
		}
	}
	/*****************************************
	 * <li>描        述：存储对媒体事件 		</li><br>
	 * <li>时        间：2013-9-13  下午2:17:10	</li><br>
	 * <li>参数： @param app			</li><br>
	 *  
	 * 
	 *****************************************/
	public void saveMultimediaEvent(String eventIndex, String eventTime, String type, String vid,int expireTime)  {
		if (null != eventIndex && null != eventTime && null != type && Constant.N0.equals(type)) {
			Jedis jedis = null;
			try {
				jedis =  RedisConnectionPool.getJedisConnection();
				jedis.select(0);  
//				jedis.set(vid + Constant.UNDERLINE + eventTime, eventIndex);
//				jedis.expire(vid + Constant.UNDERLINE + eventTime, expireTime);
				jedis.setex(vid + "_" + eventTime, expireTime, eventIndex);
			} catch (Exception ex) {
				RedisConnectionPool.returnBrokenResource(jedis);
				logger.error("存储对媒体事件异常:" + ex.getMessage(), ex);
			} finally {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/*****************************************
	 * <li>描        述：更新照片进度 		</li><br>
	 * <li>时        间：2013-9-13  下午4:07:13	</li><br>
	 * <li>参数： @param app			</li><br>
	 * @ 
	 * 
	 *****************************************/
	public Map<String, String> updatePicProgress(Map<String, String> app, int expireTime)  {
		String eventTime = app.get(Constant.N151);
		String stus = app.get(Constant.N150);
		if (null != stus && null != eventTime) {
			String vid = app.get(Constant.VID);
			Jedis jedis = null;
			try {
				// 从连接池获得连接
				jedis =  RedisConnectionPool.getJedisConnection();
				jedis.select(0);
				if (jedis.exists(vid + Constant.UNDERLINE + eventTime)) {
					String takingSEQ = jedis.get(vid + Constant.UNDERLINE + eventTime);
					String ky = vid + ":" + takingSEQ;
					String status = Constant.N8;
					if (Constant.N1.equals(stus)) {
						status = Constant.N6;
					} else if (Constant.N2.equals(stus)) {
						status = Constant.N7;
					}
					if (jedis.exists(ky)) { // CHECK 是否对应照片KY
						String value = jedis.get(ky);
						String[] arr = value.split(Constant.POUND);
						if (3 == arr.length) {
							jedis.set(ky, status + Constant.POUND + app.get(Constant.UUID) + Constant.POUND + arr[2]); // 状态或URL#照片主键#几路
							jedis.expire(ky, expireTime);
							app.put("RET", status);
							app.put(Constant.SEQ, takingSEQ);
							return app;
						}
					}
				}
			} catch (Exception ex) {
				RedisConnectionPool.returnBrokenResource(jedis);
				logger.error("更新照片进度异常:" + ex.getMessage(), ex);
			} finally {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		return null;
	}
	
	/*****************************************
	 * <li>描 述：更新指令状态</li><br>
	 * <li>时 间：2013-8-22 下午2:28:15</li><br>
	 * <li>参数： @param vid 车辆编号	
	 * <li>参数： @param seq 消息序列
	 * <li>参数： @param status 返回状态
	 * <li>参数： @param expireTime 失效时间(失效时间为凌晨过后)</li><br>
	 * 
	 *****************************************/
	public void updateCommand(String status, String seq, String vid,int expireTime) {
		Jedis jedis = null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(0); // Index 0 缓存回复状态
			if(!seq.matches("\\d+")){ // 只解SEQ包含'_' 
					jedis.set(seq, status);
					jedis.expire(seq, expireTime); // 失效时间为凌晨过后
			}else{ // 更新照片状态
				String ky = vid  + ":" + seq;
				if(jedis.exists(ky)){
					String value = jedis.get(ky);
					String[] arr = value.split("#");
					if(3 == arr.length){
						jedis.set(ky,  status + "#" + "0" + "#" + arr[2]); // 状态或URL#照片主键#几路
						jedis.expire(ky,expireTime);
					}
				}
			}
		}catch(Exception e){
			RedisConnectionPool.returnBrokenResource(jedis);
			logger.error("updateCommand 更新redis位置信息有效状态异常:"+ e.getMessage() , e);
		}finally{
			RedisConnectionPool.returnJedisConnection(jedis);;
		}
	}

	/**
	 * 存储自定义指令列表
	 * @param issuedList
	 */
	public static void saveCustomCommand(List<CustomIssued> issuedList, int expireSeconds) {
		Jedis jedis = null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(1); 
			for(CustomIssued issued : issuedList){
				String key = "CUSTOM_" + issued.getSeq();
				jedis.setex(key, expireSeconds, "0_" + issued.getVid());
			}
		}catch(Exception e){
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("存储自定义指令列表异常:"+ e.getMessage() , e);
		}finally{
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis);;
			}
		}
	}
}
