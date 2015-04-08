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
import com.ctfo.storage.process.model.AlarmStart;
import com.ctfo.storage.process.model.ThEvent;
import com.ctfo.storage.process.service.HBaseService;
import com.ctfo.storage.process.util.Cache;
import com.ctfo.storage.process.util.ConfigLoader;
/**
 *	存储报警开始线程
 */
public class AlarmStartStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(AlarmStartStorage.class);
	/**	报警开始队列	*/
	private static ArrayBlockingQueue<AlarmStart> queue = new ArrayBlockingQueue<AlarmStart>(500000);
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
	private List<AlarmStart> list = new ArrayList<AlarmStart>();
	
	public AlarmStartStorage() throws Exception{
		setName("AlarmStartStorage");
		hbaseService = new HBaseService("TH_VEHICLE_ALARM");
		batchSize = Integer.parseInt(ConfigLoader.storageParamMap.get("alarmBatchSize"));
		batchTime = Integer.parseInt(ConfigLoader.storageParamMap.get("alarmBatchTime"));
		log.info("报警开始存储线程初始化完成，批量提交数[{}], 批量提交间隔[{}]ms", batchSize, batchTime);
	}
	
	public void run(){
		while(true){
			try {
				AlarmStart alarmStart = queue.take();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				size++;
				process(alarmStart);
				long curTime = System.currentTimeMillis();
				if(size == batchSize || ((curTime - lastTime) > batchTime)){
					int temp = size;
					if(temp > 0){
						size = 0;
						if(flag){
							ScheduledExecutorService service = 	Executors.newScheduledThreadPool(1);
							Runnable hbase = new HbaseStorageTask(hbaseService.getTable(),"alarmStart");
							//hbase.run(); // 首次启动初始化缓存
							service.scheduleWithFixedDelay(hbase, 1, 15, TimeUnit.SECONDS);
							flag = false;
						}		
					}
					log.info("批量存储报警开始数据信息----提交:[{}]条, 耗时:[{}]ms-------------报警开始队列[{}]" , temp, (System.currentTimeMillis() - lastTime),queue.size());
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("处理报警开始数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(AlarmStart alarmStart){
		return queue.offer(alarmStart);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(AlarmStart alarmStart){
		try {
			queue.put(alarmStart);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(AlarmStart alarmStart){
		return queue.add(alarmStart);
	}
	
	public void process(AlarmStart alarm) throws IOException{
		String rowKey = alarm.getAlarmId()+ ":" + UUID.randomUUID().toString();
		Put put = new Put(Bytes.toBytes(rowKey));
		put.add(Bytes.toBytes("info"), Bytes.toBytes("alarmStart"), Bytes.toBytes(JSON.toJSONString(alarm)));
		hbaseService.getTable().put(put);
		count++;
		Cache.getTableMap().put("alarmStart", count);

	}
}
