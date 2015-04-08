/**
 * 
 */
package com.ocean.transilink.mina.spout;

import org.apache.mina.core.filterchain.IoFilterAdapter;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.transport.socket.nio.NioSocketConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * @author ocean
 * @date 2015年1月1日
 */
public class ReconnectionIoFilterAdapter extends IoFilterAdapter {
	private static final Logger logger = LoggerFactory
			.getLogger(ReconnectionIoFilterAdapter.class);

	/** 连接管理器 */
	private NioSocketConnector connector = null;
	private String msg = null;

	/**
	 * @param connector
	 */
	public ReconnectionIoFilterAdapter(NioSocketConnector connector, String msg) {
		super();
		this.connector = connector;
		this.msg = msg;
	}

	/**
	 * 
	 */
	public ReconnectionIoFilterAdapter() {
		super();
		
	}

	@Override
	public void sessionClosed(NextFilter nextFilter, IoSession ioSession)
			throws Exception {
		IoSession session = null;
		for (;;) {
			try {
				Thread.sleep(3000);
				ConnectFuture future = connector.connect();
				future.awaitUninterruptibly();// 等待连接创建成功
				session = future.getSession();// 获取会话
				session.write("key=" + msg);
				if (session.isConnected()) {
					logger.info("断线重连["
							+ connector.getDefaultRemoteAddress().getHostName()
							+ ":"
							+ connector.getDefaultRemoteAddress().getPort()
							+ "]成功");
					break;
				}
			} catch (Exception ex) {
				logger.info("重连服务器登录失败,3秒再连接一次:" + ex.getMessage());

			}
		}
	}
}
