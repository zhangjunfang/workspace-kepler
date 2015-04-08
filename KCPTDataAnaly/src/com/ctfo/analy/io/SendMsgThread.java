package com.ctfo.analy.io;

import org.apache.log4j.Logger;

import com.ctfo.analy.beans.MessageBean;
import com.ctfo.node.utils.MsgConnectUtil;
 

/**
 * 数据发送线程
 * 
 * @author yangyi
 * 
 */
public class SendMsgThread extends Thread {

	private static final Logger logger = Logger.getLogger(SendMsgThread.class);
	public SendMsgThread() {

	}
 

	/**
	 * 发送数据线程执行体
	 */

	public void run() {
		while (true) {
			MessageBean message = DataPool.getReceivePacket();
			if (message != null) {
				MsgConnectUtil.sendMessage(message.getMsgid(),message.getCommand());
				logger.debug("服务器ID["+message.getMsgid()+"],发送数据[" + message.getCommand()+"]");
			}else{
				try {
					sleep(10000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}

	}
}
