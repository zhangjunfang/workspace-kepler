/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service RedisService.java	</li><br>
 * <li>时        间：2013-9-16  下午4:59:50	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.service;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.statusservice.dao.RedisConnectionPool;
import com.ctfo.statusservice.model.AlarmEnd;
import com.ctfo.statusservice.model.AlarmStart;


/*****************************************
 * <li>描        述：redis服务		
 * 
 *****************************************/
public class RedisService {
	private static final Logger logger = LoggerFactory.getLogger(RedisService.class);
	/**	报警开始存储连接	*/
	private Jedis alarmStartJedis = null;
	/**	报警结束存储连接	*/
	private Jedis alarmEndJedis = null;
	
	public RedisService(boolean isStart) {
		if(isStart){
			alarmStartJedis = RedisConnectionPool.getJedisConnection();
			alarmStartJedis.select(9);
		} else {
			alarmEndJedis = RedisConnectionPool.getJedisConnection();
			alarmEndJedis.select(9);
		}
	}

	/*****************************************
	 * <li>描        述：存储报警开始列表 		</li><br>
	 * <li>时        间：2013-9-16  下午5:08:08	</li><br>
	 * <li>参数： @param app			</li><br>
	 * @param list 	报警开始列表
	 * @param	expiredSeconds	过期秒数
	 *****************************************/
	public void saveAlarmStartList(List<AlarmStart> list, int expiredSeconds) {
		try{
			if(list != null && list.size() > 0){
				for(AlarmStart alarmStart : list){
					if(alarmStart.getAlarmKey() != null && alarmStart.getAlarmKey().length() > 30 && alarmStart.getAlarmValue() != null){
						alarmStartJedis.setex(alarmStart.getAlarmKey(), expiredSeconds , alarmStart.getAlarmValue());
					}
				}
			}
		}catch(Exception ex){
			if(alarmStartJedis != null ){
				RedisConnectionPool.returnBrokenResource(alarmStartJedis);
			}
			alarmStartJedis = RedisConnectionPool.getJedisConnection();
			alarmStartJedis.select(9); 
			logger.error("存储报警开始列表异常:"+ ex.getMessage(), ex);
		}
	}
	/**
	 * 存储报警结束列表
	 * @param list	报警结束列表
	 * @param expiredSeconds	过期秒数
	 */
	public void saveAlarmEndList(List<AlarmEnd> list) {
		try{
			if(list != null && list.size() > 0){
				for(AlarmEnd alarmEnd : list){
					if(alarmEnd.getAlarmKey() != null && alarmEnd.getAlarmKey().length() > 30){
						if(alarmEndJedis.exists(alarmEnd.getAlarmKey()) && alarmEnd.getEndUtc() > 0){ 
							alarmEndJedis.append(alarmEnd.getAlarmKey(), String.valueOf(alarmEnd.getEndUtc()));
						}
					}
				}
			}
		}catch(Exception ex){
			if(alarmEndJedis != null ){
				RedisConnectionPool.returnBrokenResource(alarmEndJedis);
			}
			alarmEndJedis = RedisConnectionPool.getJedisConnection();
			alarmEndJedis.select(9); 
			logger.error("存储报警结束列表异常:"+ ex.getMessage(), ex);
		}
	}
}
