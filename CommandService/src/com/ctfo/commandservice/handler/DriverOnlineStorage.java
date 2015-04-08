package com.ctfo.commandservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.Driver;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleJdbcService;
import com.ctfo.commandservice.util.ConfigLoader;

/**
 *	驾驶员上班信息存储
 */
public class DriverOnlineStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(DriverOnlineStorage.class);
	/**	驾驶员上班信息存储队列	*/
	private static ArrayBlockingQueue<Driver> queue = new ArrayBlockingQueue<Driver>(50000);
	/**	Oracle接口	*/
	private OracleJdbcService oracleJdbcService;
	/**	批量提交数	*/
	private int batchSize = 3000; // 默认每次批量提交3000条
	/**	计数器	*/
	private int size = 0;
	/**	批量提交间隔（单位：秒）	*/
	private long batchTime = 3000; // 默认每3秒提交一次
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	批量提交缓存	*/
	private List<Driver> list = new ArrayList<Driver>();
	
	public DriverOnlineStorage(OracleProperties oracleProperties) throws Exception{ 
		setName("DriverOnlineStorage");
		oracleJdbcService = new OracleJdbcService(oracleProperties);
		batchSize = Integer.parseInt(ConfigLoader.fileParamMap.get("onlineBatchSize"));
		batchTime = Integer.parseInt(ConfigLoader.fileParamMap.get("onlineBatchTime"));
		log.info("驾驶员上班信息存储线程初始化完成，批量提交数[{}], 批量提交间隔[{}]ms", batchSize, batchTime); 
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
						oracleJdbcService.saveDriverOnlineList(list);
						list.clear();
						size = 0;
					}
					log.info("DriverOnlineStorage--提交:[{}]条, 耗时:[{}]ms" , temp, (System.currentTimeMillis() - curTime));
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("DriverOnlineStorage异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(Driver driver){
		return queue.offer(driver);
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
