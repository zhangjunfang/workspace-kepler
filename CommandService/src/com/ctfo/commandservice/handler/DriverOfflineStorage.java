package com.ctfo.commandservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.Driver;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleJdbcService;
import com.ctfo.commandservice.util.ConfigLoader;

/**
 *	驾驶员下班信息更新
 */
public class DriverOfflineStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(DriverOfflineStorage.class);
	/**	驾驶员下班信息更新队列	*/
	private ArrayBlockingQueue<Driver> queue = new ArrayBlockingQueue<Driver>(50000);
	/**	Oracle接口	*/
	private OracleJdbcService oracleJdbcService;
	/**	批量提交数	*/
	private int batchSize = 1000; // 默认每次批量提交3000条
	/**	计数器	*/
	private int size = 0;
	/**	批量提交间隔（单位：秒）	*/
	private long batchTime = 5000; // 默认每5秒提交一次
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	批量提交缓存	*/
	private List<Driver> list = new ArrayList<Driver>();
	/** 驾驶员插拔卡状态接口	 */
	private Jedis driverRedis = null;
	
	public DriverOfflineStorage(OracleProperties oracleProperties) throws Exception{ 
		setName("DriverOfflineStorage");
		oracleJdbcService = new OracleJdbcService(oracleProperties);
		batchSize = Integer.parseInt(ConfigLoader.fileParamMap.get("offlineBatchSize"));
		batchTime = Integer.parseInt(ConfigLoader.fileParamMap.get("offlineBatchTime"));
		driverRedis =  RedisConnectionPool.getJedisConnection();
		driverRedis.select(7);
		log.info("驾驶员下班信息更新线程初始化完成，批量提交数[{}], 批量提交间隔[{}]ms", batchSize, batchTime); 
	}
	
	public void run(){
		while(true){
			try {
				Driver driver = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				if(driver != null){
					size++;
					list.add(driver);
				} else {
					Thread.sleep(1); 
				}
				long curTime = System.currentTimeMillis();
				if(size == batchSize || ((curTime - lastTime) > batchTime)){
					int temp = size;
					if(temp > 0){
						oracleJdbcService.updateDriverOfflineList(list);
						list.clear();
						size = 0;
					}
					log.info("DriverOfflineStorage--提交:[{}]条, 耗时:[{}]ms" , temp, (System.currentTimeMillis() - curTime));
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("DriverOfflineStorage-异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(Driver driver){
		try {
			queue.put(driver);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
}
