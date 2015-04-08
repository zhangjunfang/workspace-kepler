package com.ctfo.advice.thread;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.dao.MQDataSource;
import com.ctfo.advice.service.MQService;



/**
 * 文件名：MQSendThread.java
 * 功能：MQ消息发送线程
 *
 * @author hjc
 * 2014-8-12下午4:06:47
 * 
 */
public class MQSendThread extends Thread{

	private static Logger log = LoggerFactory.getLogger(MQSendThread.class);
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(5000);
	private boolean flag = true;
	private static int count=0;
	private static String mqName ;
	private MQService mqService;
	
	public MQSendThread(){
		setName("MQSendThread");
		mqName = MQDataSource.getInstance().getMqName();
		mqService = new MQService();
	}
		
	public void run(){
		log.info("--MQSendThread-- MQ消息发送线程启动！");
		while(flag){
			try {
				String response = queue.take();
				process(response);
				count++;
			} catch (InterruptedException e) {
				log.error("MQ消息发送线程异常:"+e.getMessage());
			}
		}
	}
	/**
	 * 处理消息	
	 * @param response
	 */
	public void process(String response){
		
		
		mqService.send(response,mqName);
		log.info("消息发送成功,消息：[{}],队列 ：[{}]",response,mqName);
	}
	
	
	public void put(String response){
		try {
			queue.put(response);
		} catch (InterruptedException e) {
			log.error("MQ消息queue异常！"+e.getMessage());
		}
	}

	public static int getCount() {
		return count;
	}

	@SuppressWarnings("static-access")
	public void setCount(int count) {
		this.count = count;
	}

	
}
