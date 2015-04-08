/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.io FileSaveServiceHandler.java	</li><br>
 * <li>时        间：2013-9-9  上午9:39:29	</li><br>		
 * </ul>
 *****************************************/
package com.ocean.transilink.mina.spout;

import java.util.concurrent.LinkedBlockingQueue;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ocean.transilink.mina.SessionMap;

/*****************************************
 * <li>描 述：通讯接收处理器
 * 
 *****************************************/
public class IoHandler extends IoHandlerAdapter {
	private static final Logger logger = LoggerFactory
			.getLogger(IoHandler.class);

	private LinkedBlockingQueue<String> blockingqueue = null;

	public IoHandler(LinkedBlockingQueue<String> blockingqueue) {
		this.blockingqueue = blockingqueue;
	}

	@Override
	public void exceptionCaught(IoSession session, Throwable cause)
			throws Exception {
		logger.error("解析MSG信息异常!");
		if (logger.isDebugEnabled()) {
			logger.error(cause.getMessage());
		}
	}

	@Override
	public void messageReceived(IoSession session, Object message)
			throws Exception {
		logger.info("客户端与服务端[接受数据]连接---messageReceived");
		// 获取客户端发过来的message
		blockingqueue.put(message.toString());
		System.err.println("-------key:" + message.toString());
		// 保存客户端的会话session
		SessionMap sessionMap = SessionMap.newInstance();
		sessionMap.addSession(session.getId() + "", session);
	}

	@Override
	public void messageSent(IoSession session, Object message) throws Exception {
		logger.info("客户端与服务端[发送数据]连接---messageSent");
	}

	@Override
	public void sessionClosed(IoSession session) throws Exception {
		logger.info("客户端与服务端[关闭]连接---sessionClosed");
	}

	@Override
	public void sessionCreated(IoSession session) throws Exception {
		logger.info("-客户端与服务端[建立]连接---sessionCreated");
	}

	@Override
	public void sessionIdle(IoSession session, IdleStatus status)
			throws Exception {
		logger.error("-客户端与服务端连接空闲---发送心跳--sessionIdle:" + status.toString());
		// 发送心跳
		sendHeartbeat(session);
		if (session != null) {
			logger.error("-客户端与服务端连接空闲---session.close(true)---sessionIdle:"
					+ status.toString());
			session.close(true);
		}
	}

	@Override
	public void sessionOpened(IoSession session) throws Exception {
		StringBuffer loginStr = new StringBuffer();
		session.write(loginStr.toString());
		logger.info("已向MSG发送登陆信息:" + loginStr.toString());
	}

	/**
	 * 发送心跳数据
	 * 
	 * @param session
	 */
	public void sendHeartbeat(IoSession session) {
		session.write("NOOP \r\n");
		logger.debug("已向MSG发送心跳信息---sendHeartbeat");
	}

}
