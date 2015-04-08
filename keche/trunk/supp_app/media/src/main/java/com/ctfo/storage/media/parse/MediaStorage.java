package com.ctfo.storage.media.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.model.MediaFile;
import com.ctfo.storage.media.service.MongoService;

public class MediaStorage extends Thread{
	private static Logger log = LoggerFactory.getLogger(MediaStorage.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<MediaFile> queue = new ArrayBlockingQueue<MediaFile>(50000);
	/**	MongoDB接口	*/
	private MongoService mongoService;
	
	public MediaStorage(){
		 mongoService = new MongoService();
	}
	
	
	public void run(){
		while(true){
			try {
//				Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				MediaFile mediaFile = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				process(mediaFile); 
			} catch (Exception e) {
				log.error("处理队列数据异常:" + e.getMessage());
			}
		}
	}
	/**
	 * 处理多媒体文件
	 * @param mediaFile
	 */
	private void process(MediaFile mediaFile) {
		long s = System.currentTimeMillis();
		mongoService.save(mediaFile, "kcpt", "fs");
		log.debug("图片存储完成，耗时[{}]ms", System.currentTimeMillis() - s);
	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(MediaFile mediaFile){
		return queue.offer(mediaFile);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public static void put(MediaFile mediaFile){
		try {
			queue.put(mediaFile);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(MediaFile mediaFile){
		return queue.add(mediaFile);
	}
}
