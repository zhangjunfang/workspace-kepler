/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.task;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.model.Driver;
import com.ctfo.filesaveservice.util.LocalDriverCacle;
import com.ctfo.filesaveservice.util.TaskAdapter;

/*****************************************
 * <li>描 述：驾驶员信息同步任务 (平台驾驶员纬度相关)
 * 
 *****************************************/
public class DriverTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(DriverTask.class);

	
	/*****************************************
	 * <li>描 述：初始化</li><br>
	 * <li>时 间：2013-12-16 上午11:35:36</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		try{
			setName("DriverTask");
			execute();
		}catch(Exception e){
			logger.error("驾驶员信息初始化异常:" + e.getMessage(), e);
		}
	}

	/*****************************************
	 * <li>描 述：同步信息</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		int bind = 0;
		int push = 0;
		Jedis jedis = null;
		try {
			Map<String, String> map = new HashMap<String, String>();
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			
			long s1 = System.currentTimeMillis();
//			获取驾驶员绑定表缓存
			map = jedis.hgetAll("HD_DRIVER_BIND");
			Map<String, Driver> driverMap = new ConcurrentHashMap<String, Driver>();
			for (Map.Entry<String, String> m : map.entrySet()) {
				String value = m.getValue();
				String[] array = StringUtils.splitByWholeSeparatorPreserveAllTokens(value, ":", 11);
				if (array != null && array.length > 10) {
					bind++;
					Driver driver = new Driver();
					driver.setDriverId(array[1]);
					driver.setDriverSource(array[10]);
					driverMap.put(array[0], driver);
				}
			}
			map.clear();
			
			long s2 = System.currentTimeMillis();
//			获取驾驶员插拔卡信息缓存
			map = jedis.hgetAll("HD_DRIVER_CARD");
			for (Map.Entry<String, String> m : map.entrySet()) {
				String value = m.getValue();
				String[] array = StringUtils.splitByWholeSeparatorPreserveAllTokens(value, ":", 11);
				if (array != null && array.length > 10) {
					push++;
					Driver driver = new Driver();
					driver.setDriverId(array[1]);
					driver.setDriverSource(array[10]);
					driverMap.put(array[0], driver);
				}
			}
			if (driverMap.size() > 0) {
//				如果当前缓存中的key在新缓存中没有就从缓存中清除
				Set<String> keySet = LocalDriverCacle.getInstance().getKeySet();
				for(String key : keySet){
					if(!driverMap.containsKey(key)){ 
						LocalDriverCacle.getInstance().remove(key);
					}
				}
				LocalDriverCacle.getInstance().putAll(driverMap);
			}
			map.clear();
			driverMap.clear();
			long end = System.currentTimeMillis();
			logger.info("DriverTask--同步数据:[{}]条, 绑定信息:[{}]条, 刷卡数据:[{}]条 , 绑定处理耗时:[{}]ms, 刷卡处理耗时:[{}]ms, 总耗时[{}]ms", (push + bind), bind, push, (s2 - s1), (end - s2), end - start);
		} catch (Exception e) {
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("DriverTask--同步驾驶员信息异常:" + e.getMessage(), e);
		} finally {
			if (jedis != null) {
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
}
