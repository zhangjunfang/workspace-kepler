package com.ctfo.storage.process.parse;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.process.model.RealTimeLocation;
import com.ctfo.storage.process.service.RedisService;
import com.ctfo.storage.process.util.ConfigLoader;
/**
 *	实时数据存储
 */
public class RealTimeStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(RealTimeStorage.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<RealTimeLocation> queue = new ArrayBlockingQueue<RealTimeLocation>(50000);
	/**	Redis接口	*/
	private RedisService redisService;
	/**	批量提交数	*/
	private int batchSize = 3000; // 默认每次批量提交3000条
	/**	计数器	*/
	private int size = 0;
	/**	批量提交间隔（单位：秒）	*/
	private long batchTime = 3000; // 默认每3秒提交一次
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	批量提交缓存	*/
	private Map<String, String> map = new HashMap<String, String>();
	
	public RealTimeStorage(){
		setName("RealTimeStorage");
		redisService = new RedisService();
		batchSize = Integer.parseInt(ConfigLoader.storageParamMap.get("realTimeBatchSize"));
		batchTime = Integer.parseInt(ConfigLoader.storageParamMap.get("realTimeBatchTime"));
		log.info("实时数据存储线程初始化完成，批量提交数[{}], 批量提交间隔[{}]ms", batchSize, batchTime); 
	}
	
	public void run(){
		while(true){
			try {
				RealTimeLocation realTimeLocation = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
				size++;
				map.put(realTimeLocation.getPhoneNumber(), JSON.toJSONString(realTimeLocation));
				long curTime = System.currentTimeMillis();
				if(size == batchSize || ((curTime - lastTime) > batchTime)){
					int temp = size;
					if(temp > 0){
						redisService.saveRealTimeLocationList(map);
						map.clear();
						size = 0;
					}
					log.info("批量更新实时数据信息----提交:[{}]条, 耗时:[{}]ms" , temp, (System.currentTimeMillis() - curTime));
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("存储实时数据队列数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(RealTimeLocation realTimeLocation){
		return queue.offer(realTimeLocation);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(RealTimeLocation realTimeLocation){
		try {
			queue.put(realTimeLocation);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(RealTimeLocation realTimeLocation){
		return queue.add(realTimeLocation);
	}
}
