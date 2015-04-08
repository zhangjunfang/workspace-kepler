package com.ctfo.statusservice.handler;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.io.IoClient;

/*****************************************
 * <li>描        述：消息发送线程		
 * 
 ****************************************
 */
public class SendMsgThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(SendMsgThread.class);
	/**	数据发送队列		*/ 
	private static ArrayBlockingQueue<String> sendQueue =new ArrayBlockingQueue<String>(100000);
	/** 计数器	 */
	private int index = 0;
	/** 最近时间	 */
	private long lastTime = System.currentTimeMillis();
	
	public SendMsgThread() {
		super("SendMsgThread");
	}
	
	/*****************************************
	 * <li>描 述：将数据插入队列顶部</li><br>
	 * <li>时 间：2013-9-16 下午4:42:17</li><br>
	 * <li>参数： @param dataMap</li><br>
	 * 
	 *****************************************/
	public void putDataMap(String message) {
		try {
			sendQueue.put(message);
		} catch (InterruptedException e) {
			logger.error(e.getMessage(), e);
		}
	}

	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-9-16 下午4:42:47</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public int getQueueSize() {
		return sendQueue.size();
	}
	
	/*****************************************
	 * <li>描        述：发送报警数据 		</li><br>
	 * <li>时        间：2013-9-26  下午6:06:40	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void run() {
		logger.debug("报警数据发送线程启动");
		while (true) {
			String message = null;
			try {
				message = sendQueue.take();
				IoClient.setMessage(message);
				logger.debug("发送数据[" + message + "]");
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					int size = getQueueSize();
					logger.info("sendalarmtomsg,排队:" + size + ",10s处理:(" + index +")条");
					index = 0 ;
					lastTime = System.currentTimeMillis();
				}
				index++;
			} catch (Exception e) {
				logger.error("消息发送异常:"+ e.getMessage(), e);
			}
		}

	}
}
