/**
 * 
 */
package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.ThEvent;

/**
 * 驾驶事件数据处理
 */
public class EventProcess extends Thread {
	private static Logger log = LoggerFactory.getLogger(EventProcess.class);
	/**	驾驶事件数据队列	*/
	private static ArrayBlockingQueue<ThEvent> queue = new ArrayBlockingQueue<ThEvent>(100000);
	/**	计数器	*/
	private int index = 0;
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	
	private EventStorage eventStorage;
	
	public EventProcess() throws Exception{
		setName("EventProcess");
		eventStorage = new EventStorage();
		eventStorage.start();
	}
	
	public void run(){
		while(true){
			try {
				ThEvent event = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				index++;
				process(event); 
				long current = System.currentTimeMillis();
				if ((current - lastTime) > 10000) {
					int queueSize = getQueueSize();
					log.info("EventProcess-10秒处理[{}]条, 排队[{}]条", index, queueSize);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("处理驾驶事件数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取队列大小
	 * @return
	 */
	private int getQueueSize() {
		return queue.size();
	}
	/**
	 * 处理驾驶事件数据
	 * @param MediaEvent
	 * @throws  InterruptedException 
	 */
	private void process(ThEvent event)  {
		eventStorage.put(event);
	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(ThEvent event){
		return queue.offer(event);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(ThEvent event){
		try {
			queue.put(event);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(ThEvent event){
		return queue.add(event);
	}
}
