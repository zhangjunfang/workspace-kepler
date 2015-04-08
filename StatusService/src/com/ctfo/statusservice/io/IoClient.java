/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.io FileSaveServiceClient.java	</li><br>
 * <li>时        间：2013-9-9  上午10:19:21	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.io;

import java.net.InetSocketAddress;
import java.nio.charset.Charset;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.mina.core.RuntimeIoException;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.service.IoService;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.filter.codec.textline.LineDelimiter;
import org.apache.mina.filter.codec.textline.TextLineCodecFactory;
import org.apache.mina.filter.logging.MdcInjectionFilter;
import org.apache.mina.transport.socket.nio.NioSocketConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/*****************************************
 * <li>描 述：连接客户端
 * 
 *****************************************/
public class IoClient {
	private static final Logger logger = LoggerFactory.getLogger(IoClient.class);
	/** 接收发数据的业务类 */
	private IoHandler handler;
	/** 服务端IP */
	private String host;
	/** 端口 */
	private int port;
	/** 会话 */
	private static IoSession session;
	/** 连接管理器 */
	private NioSocketConnector connector;
	/** 字符编码 */
	private String encoding = "GBK";
	/** 连接超时时间 */
	private static long CONNECT_TIMEOUT = 60 * 1000L; // 60 seconds

	/**
	 * 
	 * @param ip
	 * @param port
	 * @param handler
	 */
	public IoClient() {
	}

	/*****************************************
	 * <li>描 述：启动IO连接</li><br>
	 * <li>时 间：2013-9-9 上午10:37:10</li><br>
	 * <li>参数： @throws InterruptedException</li><br>
	 * 
	 * @throws Exception
	 * 
	 *****************************************/
	public void connect() throws Exception {
		if (session != null && session.isConnected()) {
			throw new IllegalStateException("连接已存在,请关闭后重连!");
		}
		connector = new NioSocketConnector();
		connector.setConnectTimeoutMillis(CONNECT_TIMEOUT);


		connector.getFilterChain().addLast("mdc", new MdcInjectionFilter());
		TextLineCodecFactory factory = new TextLineCodecFactory(Charset.forName(encoding), LineDelimiter.WINDOWS.getValue(), LineDelimiter.WINDOWS.getValue());
		factory.setDecoderMaxLineLength(1064960);
		factory.setEncoderMaxLineLength(1064960);
		connector.getFilterChain().addLast("codec", new ProtocolCodecFilter(factory));
		// 设置接收缓冲区的大小
		connector.getSessionConfig().setReceiveBufferSize(1064960);
		// 设置输出缓冲区的大小
		connector.getSessionConfig().setSendBufferSize(1064960);
		// 设置读写通道空闲值
		// connector.getSessionConfig().setIdleTime(IdleStatus.BOTH_IDLE, 30);
		connector.getSessionConfig().setIdleTime(IdleStatus.READER_IDLE, 50);
		connector.getSessionConfig().setIdleTime(IdleStatus.WRITER_IDLE, 55);
		// 设置默认访问地址
		connector.setDefaultRemoteAddress(new InetSocketAddress(host, port));
		// 添加服务监听
		if (this.handler != null) {
			connector.setHandler(this.handler);
		} else {
			logger.error("--------IoFilterAdapter-----无业务处理类");
		}
		connector.addListener(new IoListener() {
			@Override
			public void serviceDeactivated(IoService arg0) throws Exception {
				try {
					Thread.sleep(1000);
					logger.info("-----------------------------IoFilterAdapter----------------sessionClosed session");
					if (handler != null) {
						connector.setHandler(handler);
					} else {
						logger.error("--------IoFilterAdapter-----无业务处理类");
					}
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
			public void sessionDestroyed(IoSession arg0) throws Exception {
				try {
					Thread.sleep(1000);
					logger.info("-----------------------------IoFilterAdapter----------------sessionClosed session");
					if (handler != null) {
						connector.setHandler(handler);
					} else {
						logger.error("--------IoFilterAdapter-----无业务处理类");
					}
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
		});

		for (;;) {
			try {
				ConnectFuture future = connector.connect();
				// 等待连接创建成功
				future.awaitUninterruptibly();
				// 获取会话
				session = future.getSession();
				logger.info("连接服务端" + host + ":" + port + "[成功]" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
				break;
			} catch (RuntimeIoException e) {
				System.out.println("连接服务端" + host + ":" + port + "失败" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date())
						+ ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage());
				logger.error("连接服务端" + host + ":" + port + "失败" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date())
						+ ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage(), e);
				Thread.sleep(5000);// 重连间隔5s
			}
		}
	}

	/**
	 * 注销动作
	 * 
	 * @param logo
	 */
	public void quit(String logo) {
		if (session != null) {
			if (session.isConnected()) {
				// 发启注销连接
				session.close(true);
				// 等待连接关闭
				session.getCloseFuture().awaitUninterruptibly();
				// logger.info("与服务器：" + HOSTNAME + ":" + PORT + ",正常断开连接");
			}
		}
		try {
			connector.dispose();
		} catch (Exception e) {
			logger.info("与服务器：" + host + ":" + port + ",正常断开连接异常:" + e);
		}
		logger.info("与服务器：" + host + ":" + port + ",断开连接");
	}

	/**
	 * @param b
	 */
	public static boolean write(byte[] b) {
		if (session != null && session.isConnected()) {
			session.write(b);
			return true;
		} else {
			logger.error("数据发送失败,与服务连接异常");
		}
		return false;
	}

	public static boolean setMessage(String msg){
		if (session != null && session.isConnected()) {
			session.write(msg);
			logger.debug("数据发送成功:" + msg);
			return true;
		} else {
			logger.error("数据发送失败,与服务连接异常");
		}
		return false;
	}
	/**
	 * @param b
	 */
	public boolean write(String b) {
		if (session != null && session.isConnected()) {
			session.write(b);
			return true;
		} else {
			logger.error("数据发送失败,与服务连接异常");
		}
		return false;
	}

	public IoSession getSession() {
		return session;
	}

	public void setSession(IoSession ioSession) {
		session = ioSession;
	}

	public String getEncoding() {
		return encoding;
	}

	public void setEncoding(String encoding) {
		this.encoding = encoding;
	}

	public String getHost() {
		return host;
	}

	public void setHost(String host) {
		this.host = host;
	}

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}

	public IoHandler getHandler() {
		return handler;
	}

	public void setHandler(IoHandler handler) {
		this.handler = handler;
	}

	public NioSocketConnector getConnector() {
		return connector;
	}

	public void setConnector(NioSocketConnector connector) {
		this.connector = connector;
	}
}
