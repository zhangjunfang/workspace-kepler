package com.ctfo.storage.io;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.transport.socket.nio.NioSocketAcceptor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 附件数据服务端<br>
 * 描述： 附件数据服务端<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-11-8</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class FileIoServer {

	private static final Logger logger = LoggerFactory.getLogger(FileIoServer.class);

	/** 服务端端口 */
	private int port;

	/** 接收发数据的业务类 */
	private FileIoHandler handler;

	/** 接收管理器 */
	private NioSocketAcceptor acceptor;

	/**
	 * 启动服务
	 * 
	 * @throws Exception
	 */
	public void start() throws Exception {
		acceptor = new NioSocketAcceptor(); // 初始化Acceptor
		acceptor.getFilterChain().addLast("codec", new ProtocolCodecFilter(new FileByteCodecFactory()));
		// LoggingFilter filter = new LoggingFilter();
		// filter.setExceptionCaughtLogLevel(LogLevel.DEBUG);
		// filter.setMessageReceivedLogLevel(LogLevel.DEBUG);
		// filter.setMessageSentLogLevel(LogLevel.DEBUG);
		// filter.setSessionClosedLogLevel(LogLevel.DEBUG);
		// filter.setSessionCreatedLogLevel(LogLevel.DEBUG);
		// filter.setSessionIdleLogLevel(LogLevel.DEBUG);
		// filter.setSessionOpenedLogLevel(LogLevel.DEBUG);
		// acceptor.getFilterChain().addLast("logger", filter); // 加入过滤器（Filter）到Acceptor
		acceptor.setReuseAddress(true); // 设置的是主服务监听的端口可以重用
		acceptor.getSessionConfig().setReuseAddress(true); // 设置每一个非主监听连接的端口可以重用
		acceptor.getSessionConfig().setReceiveBufferSize(1024); // 设置输入缓冲区的大小
		acceptor.getSessionConfig().setSendBufferSize(10240); // 设置输出缓冲区的大小
		acceptor.getSessionConfig().setTcpNoDelay(true); // 设置为非延迟发送，为true则不组装成大包发送，收到东西马上发出
		acceptor.setBacklog(100); // 设置主服务监听端口的监听队列的最大值为100，如果当前已经有100个连接，再新的连接来将被服务器拒绝
		acceptor.setDefaultLocalAddress(new InetSocketAddress(port));
		if (null != this.handler) {
			acceptor.setHandler(this.handler); // 添加服务处理器
		} else {
			logger.info("无业务处理类");
		}
		try {
			acceptor.bind();
			logger.info("附件同步服务端启动成功，端口：" + port + ",时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
		} catch (IOException e) {
			logger.error("附件同步服务端" + port + "异常 " + ",时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage(), e);
		}
		logger.info("TCP附件同步服务启动，端口：" + port);
	}

	/******************************** GET AND SET ********************************/

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}

	public FileIoHandler getHandler() {
		return handler;
	}

	public void setHandler(FileIoHandler handler) {
		this.handler = handler;
	}

	public NioSocketAcceptor getAcceptor() {
		return acceptor;
	}

	public void setAcceptor(NioSocketAcceptor acceptor) {
		this.acceptor = acceptor;
	}
}
