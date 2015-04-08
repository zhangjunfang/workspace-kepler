package com.ocean.transilink.mina;

import java.util.concurrent.LinkedBlockingQueue;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;

/**
 * @Description: mina服务端业务处理类
 * @author ocean
 * @date 2015年1月1日
 */
public class ServerHandler extends IoHandlerAdapter {

	private final static Log log = LogFactory.getLog(ServerHandler.class);

	private LinkedBlockingQueue<String> blockingqueue = null;

	public ServerHandler(LinkedBlockingQueue<String> blockingqueue) {
		this.blockingqueue = blockingqueue;
	}

	@Override
	public void exceptionCaught(IoSession session, Throwable cause)
			throws Exception {
	}

	@Override
	public void messageReceived(IoSession session, Object message)
			throws Exception {
		log.debug("服务端收到信息-------------");

		// 获取客户端发过来的message
		blockingqueue.put(message.toString());
		System.err.println("-------key:" + message.toString());
		// 保存客户端的会话session
		SessionMap sessionMap = SessionMap.newInstance();
		sessionMap.addSession(session.getId() + "", session);

	}

	@Override
	public void messageSent(IoSession session, Object message) throws Exception {
		log.debug("------------服务端发消息到客户端---");
	}

	@Override
	public void sessionClosed(IoSession session) throws Exception {
		log.debug("远程session关闭了一个..." + session.getRemoteAddress().toString());
	}

	@Override
	public void sessionCreated(IoSession session) throws Exception {
		log.debug(session.getRemoteAddress().toString()
				+ "----------------------create");
	}

	@Override
	public void sessionIdle(IoSession session, IdleStatus status)
			throws Exception {
		log.debug(session.getServiceAddress() + "IDS");
	}

	@Override
	public void sessionOpened(IoSession session) throws Exception {
		log.debug("连接打开：" + session.getLocalAddress());
	}

}