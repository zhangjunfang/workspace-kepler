package com.ctfo.savecenter.io;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.node.utils.MsgConnectUtil;
import com.ctfo.savecenter.beans.Message;

/**
 * 数据发送线程
 * 
 * @author yangyi
 * 
 */
public class SendMsgThread extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(SendMsgThread.class);

	public SendMsgThread() {

	}

	/**
	 * 发送数据线程执行体
	 */

	public void run() {
		logger.debug("数据发送线程启动");
		while (true) {
			Message message = null;
			try {
				if (!DataPool.isSendPacketEmpty()) {
					message = DataPool.getSendPacket();
					MsgConnectUtil.sendMessage(message.getMsgid(),
							message.getCommand());
					logger.debug("发送数据[" + message.getCommand() + "]");
				} else {

					sleep(1000);
				}
			} catch (Exception e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

		}

	}
}
