package com.ocean.transilink.mina.spout;

import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.service.IoService;
import org.apache.mina.core.service.IoServiceListener;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.transport.socket.nio.NioSocketConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class IoListener implements IoServiceListener {
	private static final Logger logger = LoggerFactory
			.getLogger(IoListener.class);
	/** 连接管理器 */
	private NioSocketConnector connector = null;
	
	
	
	/**
	 * 
	 */
	public IoListener() {
		super();
		
	}

	/**
	 * @param connector
	 */
	public IoListener(NioSocketConnector connector) {
		super();
		this.connector = connector;
	}

	@Override
	public void serviceActivated(IoService arg0) throws Exception {
	}


	@Override
	public void serviceIdle(IoService arg0, IdleStatus arg1) throws Exception {
	}

	@Override
	public void sessionCreated(IoSession arg0) throws Exception {
	}

	@Override
	public void serviceDeactivated(IoService arg0) throws Exception {
		IoSession session=null;
		try {
			Thread.sleep(1000);
			logger.info("-----------------------------IoFilterAdapter----------------sessionClosed session");
			
			ConnectFuture future = connector.connect();
			// 等待连接创建成功
			future.awaitUninterruptibly();
			// 获取会话
			session = future.getSession();
		} catch (Exception e) {
			if (e.getMessage().indexOf("Failed to get the session") > -1 || e.getCause().getMessage().indexOf("Connection refused") > -1) {
				logger.info("异常情况被捕获,服务器登录失败,重新连接服务器，每3秒连接一次:" + e.getMessage());
				for (;;) {
					try {
						Thread.sleep(3000);
						ConnectFuture future = connector.connect();
						// 等待连接创建成功
						future.awaitUninterruptibly();
						// 获取会话
						session = future.getSession();
						if (session.isConnected()) {
							logger.info("断线重连[" + connector.getDefaultRemoteAddress().getHostName() + ":" + connector.getDefaultRemoteAddress().getPort() + "]成功");
							break;
						}
					} catch (Exception ex) {
						logger.info("重连服务器登录失败,3秒再连接一次:" + ex.getMessage());
					}
				}
			}
		}
	}
	
	@Override
	public void sessionClosed(IoSession session) throws Exception {
		
		
	}
	@Override
	public void sessionDestroyed(IoSession arg0) throws Exception {
		IoSession session =null;
		try {
			Thread.sleep(1000);
			logger.info("-----------------------------IoFilterAdapter----------------sessionClosed session");
			ConnectFuture future = connector.connect();
			// 等待连接创建成功
			future.awaitUninterruptibly();
			// 获取会话
			 session = future.getSession();
		} catch (Exception e) {
			if (e.getMessage().indexOf("Failed to get the session") > -1
					|| e.getCause().getMessage()
							.indexOf("Connection refused") > -1) {
				logger.info("异常情况被捕获,服务器登录失败,重新连接服务器，每3秒连接一次:"
						+ e.getMessage());
				for (;;) {
					try {
						Thread.sleep(3000);
						ConnectFuture future = connector.connect();
						// 等待连接创建成功
						future.awaitUninterruptibly();
						// 获取会话
						session = future.getSession();
						if (session.isConnected()) {
							logger.info("断线重连["
									+ connector
											.getDefaultRemoteAddress()
											.getHostName()
									+ ":"
									+ connector
											.getDefaultRemoteAddress()
											.getPort() + "]成功");
							break;
						}
					} catch (Exception ex) {
						logger.info("重连服务器登录失败,3秒再连接一次:"
								+ ex.getMessage());
					}
				}
			}
		}
	}

}
