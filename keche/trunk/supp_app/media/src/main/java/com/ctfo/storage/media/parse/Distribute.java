package com.ctfo.storage.media.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.model.Media;

public class Distribute extends Thread{
	private static Logger log = LoggerFactory.getLogger(Distribute.class);
	
	private static ArrayBlockingQueue<Media> queue = new ArrayBlockingQueue<Media>(50000);
	
	
	public Distribute(){
		
	}
	
	
	public void run(){
		while(true){
			try {
//				Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				Media o = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				process(o); 
			} catch (Exception e) {
				log.error("处理队列数据异常:" + e.getMessage());
			}
		}
	}
	
	private void process(Media media) {
		// TODO Auto-generated method stub
		
	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(Media media){
		return queue.offer(media);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public static void put(Media media){
		try {
			queue.put(media);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(Media media){
		return queue.add(media);
	}
}
