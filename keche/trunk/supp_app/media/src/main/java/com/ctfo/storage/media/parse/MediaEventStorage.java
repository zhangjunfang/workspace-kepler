package com.ctfo.storage.media.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.model.MediaEvent;
import com.ctfo.storage.media.service.HBaseService;
/**
 *	多媒体事件存储
 */
public class MediaEventStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(MediaEventStorage.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<MediaEvent> queue = new ArrayBlockingQueue<MediaEvent>(50000);
	/**	MongoDB接口	*/
	private HBaseService hbaseService;
	/**	批量提交数	*/
	private int batchSize = 3000; // 默认每次批量提交3000条
	/**	计数器	*/
	private int size = 0;
	/**	批量提交间隔（单位：秒）	*/
	private long batchTime = 3000; // 默认每3秒提交一次
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	批量提交缓存	*/
	private List<MediaEvent> list = new ArrayList<MediaEvent>();
	
	public MediaEventStorage(){
		hbaseService = new HBaseService("TH_VEHICLE_MULTI_EVENT");
		
	}
	
	public void run(){
		while(true){
			try {
//				Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				MediaEvent mediaEvent = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				size++;
				
				process(mediaEvent); 
			} catch (Exception e) {
				log.error("处理队列数据异常:" + e.getMessage());
			}
		}
	}
	/**
	 * 处理多媒体文件
	 * @param MediaEvent
	 * @throws  InterruptedException 
	 */
	private void process(MediaEvent mediaEvent)  {
//		long s = System.currentTimeMillis();
		long curTime = System.currentTimeMillis();
		if(size == batchSize || ((curTime - lastTime) > batchTime)){
			list.add(mediaEvent);
			long start = System.currentTimeMillis();
			hbaseService.saveMediaEventList(list);
			log.info("处理多媒体事件----批量提交:[{}]条, 耗时:[{}]ms" , list.size(), (System.currentTimeMillis() - start));
			list.clear();
			size = 0;
			lastTime = System.currentTimeMillis();
		} else {
			list.add(mediaEvent);
		}
		
//		log.info("处理完成，耗时[{}]ms", System.currentTimeMillis() - s);
//		hbaseService.saveMediaEventList(mediaEvent, "kcpt", "fs");
	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(MediaEvent mediaEvent){
		return queue.offer(mediaEvent);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(MediaEvent mediaEvent){
		try {
			queue.put(mediaEvent);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(MediaEvent mediaEvent){
		return queue.add(mediaEvent);
	}
}
