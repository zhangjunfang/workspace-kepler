package com.ctfo.mcc.service;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.io.IoClient;
/**
 * 消息发送队列
 *
 */
public class SendQueue extends Thread {
	private static Logger logger = LoggerFactory.getLogger(SendCacheCommand.class);
	/** 连接队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(100000);
	/** 最后处理时间 */
	private long lastTime = System.currentTimeMillis();
	/** 计数器 */
	private int index = 0;
	
	public SendQueue(){
		setName("SendQueue");
		logger.info("SendQueue - 启动!");
	}
	
	public void init(){ 
		start();
	}
	
	public void run(){
		while(true){
			try {
				String message = queue.take();
				index++;
				IoClient.sendMessage(message);
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 30000){
					int size = queue.size();
					logger.info("消息发送队列30s接收数据[{}]条, 等待[{}]条", index, size); 
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) { 
				logger.error("消息发送队列异常:"+ e.getMessage(), e);
			}
		}
	}
	/**
	 * 写入队列
	 * @param message
	 */
	public static boolean offer(String message){
		return queue.offer(message);
	}
	/**
	 * 写入队列
	 * @param message
	 */
	public static void put(String message){
		try {
			queue.put(message);
		} catch (InterruptedException e) {
			logger.error("写入队列异常:" + e.getMessage(), e); 
		}
	}
}
