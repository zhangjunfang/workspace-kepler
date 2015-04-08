package com.ctfo.savecenter.io;

/**
 * 通讯接收数据处理类
 */

import org.apache.mina.core.session.IoSession;

import com.ctfo.node.client.ConfigServerClientHandler;
import com.ctfo.node.client.MsgClientHandler;
import com.ctfo.savecenter.analy.AnalyseServiceInit;
import com.ctfo.savecenter.beans.Message;

public class ReceiveMsgHandler extends MsgClientHandler {

	String command;
	long count = 0;
	long lastNoopTime = System.currentTimeMillis();

	@Override
	public void doBytes(IoSession session, Object message) {
		// TODO Auto-generated method stub

	}

	@Override
	public void setPhandler(ConfigServerClientHandler phandler) {
		// TODO Auto-generated method stub
		super.setPhandler(phandler);
		super.setGroup(this.phandler.getCsTypeId());
	}

	/**
	 * 实时数据接收处理
	 */
	@Override
	public void doString(IoSession session, Object message) {
		command = message.toString();
		DataPool.setCountValue();
		if (!command.equals("NOOP_ACK")) {
			Message messagecommand = new Message();
			messagecommand.setCommand(command);
			messagecommand.setMsgid(getMsgId());
			
			AnalyseServiceInit.getAnalyseServiceThread()[id].addPacket(messagecommand);
		
			
		}
	}

	@Override
	public void setNumToConfigServer(int nums) {

	}

}
