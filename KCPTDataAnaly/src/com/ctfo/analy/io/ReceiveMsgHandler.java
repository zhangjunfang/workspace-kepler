package com.ctfo.analy.io;

/**
 * 通讯接收数据处理类
 */

import org.apache.log4j.Logger;
import org.apache.mina.core.session.IoSession;

import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.protocal.AnalyseServiceInit;
import com.ctfo.node.client.MsgClientHandler;
 
/**
 * 接收处理
 * @author Administrator
 *
 */
public class ReceiveMsgHandler extends MsgClientHandler {

	String command;
	long count = 0;
	long lastNoopTime = System.currentTimeMillis();

	private static final Logger logger = Logger.getLogger(ReceiveMsgHandler.class);

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
		logger.debug("接收数据[" + command + "]");
		if (!command.equals("NOOP_ACK")) {
			MessageBean messagecommand = new MessageBean();
			messagecommand.setCommand(command);
			messagecommand.setMsgid(getMsgId());
			
			AnalyseServiceInit.getAnalyseServiceThread()[id].addPacket(messagecommand);
		}
	}

	@Override
	public void setNumToConfigServer(int nums) {
		// TODO Auto-generated method stub

	}

}
