package com.ctfo.storage.process.parse;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.apache.hadoop.hbase.client.Put;
import org.apache.hadoop.hbase.util.Bytes;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.process.model.ThOil;
import com.ctfo.storage.process.model.ThVehicleStatus;
import com.ctfo.storage.process.service.HBaseService;
import com.ctfo.storage.process.util.Cache;
import com.ctfo.storage.process.util.ConfigLoader;
/**
 *	油量数据存储
 */
public class OilStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(OilStorage.class);
	/**	油量数据队列	*/
	private static ArrayBlockingQueue<ThOil> queue = new ArrayBlockingQueue<ThOil>(100000);
	/**	HBase接口	*/
	private HBaseService hbaseService;
	/**	批量提交数	*/
	private int batchSize = 3000; // 默认每次批量提交3000条
	/**	计数器	*/
	private int size = 0;
	/**	计数器	*/
	private int count = 0;
	/**	定时任务标示	*/
	private boolean flag = true;
	/**	批量提交间隔（单位：秒）	*/
	private long batchTime = 3000; // 默认每3秒提交一次
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	批量提交缓存	*/
	private List<ThOil> list = new ArrayList<ThOil>();
	
	public OilStorage() throws Exception{ 
		setName("OilStorage");
		hbaseService = new HBaseService("TH_OIL");
		batchSize = Integer.parseInt(ConfigLoader.storageParamMap.get("oilBatchSize"));
		batchTime = Integer.parseInt(ConfigLoader.storageParamMap.get("oilBatchTime"));
		log.info("油量数据存储线程初始化完成，批量提交数[{}], 批量提交间隔[{}]ms", batchSize, batchTime); 
	}
	
	public void run(){
		while(true){
			try {
				ThOil oil = queue.take();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				size++;
				process(oil);
				long curTime = System.currentTimeMillis();
				if(size == batchSize || ((curTime - lastTime) > batchTime)){
					int temp = size;
					if(temp > 0){
						size = 0;
						if(flag){
							ScheduledExecutorService service = 	Executors.newScheduledThreadPool(1);
							Runnable hbase = new HbaseStorageTask(hbaseService.getTable(),"oil");
							service.scheduleWithFixedDelay(hbase, 1, 30, TimeUnit.SECONDS);
							flag = false;
						}		
					}
					log.info("批量存储油量数据信息----提交:[{}]条, 耗时:[{}]ms" , temp, (System.currentTimeMillis() - lastTime));
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("存储油量数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(ThOil oil){
		return queue.offer(oil);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(ThOil oil){
		try {
			queue.put(oil);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(ThOil oil){
		return queue.add(oil);
	}
	
	public void process(ThOil oil) throws IOException{
		String rowKey = oil.getUpTime() + UUID.randomUUID().toString();
		Put put = new Put(Bytes.toBytes(rowKey));
		put.add(Bytes.toBytes("info"), Bytes.toBytes(oil.getVid()), Bytes.toBytes(JSON.toJSONString(oil)));
		hbaseService.getTable().put(put);
		count++;
		Cache.getTableMap().put("oil", count);
	}
}
