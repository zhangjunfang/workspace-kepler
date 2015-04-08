package com.ctfo.dataanalysisservice.io;

/**
 * 通讯接收数据处理类
 */

import org.apache.mina.core.session.IoSession;

import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.node.client.MsgClientHandler;
 

public class ReceiveMsgHandler extends MsgClientHandler {

	String command;
	long count = 0;
	long lastNoopTime = System.currentTimeMillis();

	@Override
	public void doBytes(IoSession session, Object message) {
		// TODO Auto-generated method stub

	}

	/**
	 * 实时数据接收处理
	 */
	@Override
	public void doString(IoSession session, Object message) {
		command = message.toString();
 
		if (!command.equals("NOOP_ACK")) {
			Message messagecommand = new Message();
			messagecommand.setCommand(command);
			messagecommand.setMsgid(getMsgId());

			DataPool.setReceivePacketValue(messagecommand);
		}
	}

	@Override
	public void setNumToConfigServer(int nums) {
		// TODO Auto-generated method stub

	}

}
