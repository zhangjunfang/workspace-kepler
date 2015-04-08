/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.io FileSaveServiceClient.java	</li><br>
 * <li>时        间：2013-9-9  上午10:19:21	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.analy.io;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.nio.charset.Charset;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.mina.core.RuntimeIoException;
import org.apache.mina.core.filterchain.IoFilterAdapter;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.filter.codec.textline.LineDelimiter;
import org.apache.mina.filter.codec.textline.TextLineCodecFactory;
import org.apache.mina.filter.logging.LogLevel;
import org.apache.mina.filter.logging.LoggingFilter;
import org.apache.mina.filter.logging.MdcInjectionFilter;
import org.apache.mina.transport.socket.nio.NioSocketConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/*****************************************
 * <li>描        述：连接客户端		
 * 
 *****************************************/
public class IoClient {
	private static final Logger logger = LoggerFactory.getLogger(IoClient.class);
	/** 接收发数据的业务类 */
	private IoHandler handler;
	/** 监听 */
//	private IoListener listener;
	/** 服务端IP */
	private String host;
	/** 端口 */
	private int port;
	/** 会话 */
	private IoSession session;
	/** 连接管理器 */
	private NioSocketConnector connector;
	/** 字符编码 */
	private String encoding = "GBK";
	/** 空闲间隔 */
	private static long CONNECT_TIMEOUT = 30 * 1000L; // 30 seconds

	/**
	 * 
	 * @param ip
	 * @param port
	 * @param handler
	 */
	public IoClient() {
	}
	/*****************************************
	 * <li>描        述：启动IO连接 		</li><br>
	 * <li>时        间：2013-9-9  上午10:37:10	</li><br>
	 * <li>参数： @throws InterruptedException			</li><br>
	 * 
	 *****************************************/
	public void connect() throws Exception {
		if (session != null && session.isConnected()) {
			throw new IllegalStateException("连接已存在,请关闭后重连!");
		}
		connector = new NioSocketConnector();
		connector.setConnectTimeoutMillis(CONNECT_TIMEOUT);
//		断线重连回调过滤器
		connector.getFilterChain().addFirst("reconnection", new IoFilterAdapter() {
			@Override
			public void sessionClosed(NextFilter nextFilter, IoSession ioSession) throws Exception {
				Thread.sleep(1000);
				try{
					logger.info("-----------------------------IoFilterAdapter----------------sessionClosed session");
					ConnectFuture future = connector.connect();
					// 等待连接创建成功
					future.awaitUninterruptibly();
					// 获取会话
					session = future.getSession();
				}catch (Exception e){
					if(e.getMessage().indexOf("Failed to get the session") > -1 || e.getCause().getMessage().indexOf("Connection refused") > -1){
						logger.error("异常情况被捕获,服务器登录失败,重新连接服务器，每3秒连接一次:" + e.getMessage());
						for(;;){
							Thread.sleep(3000);
							try{
								ConnectFuture future = connector.connect();
								// 等待连接创建成功
								future.awaitUninterruptibly();
								// 获取会话
								session = future.getSession();
								if(session.isConnected()){
									logger.info("断线重连["+ connector.getDefaultRemoteAddress().getHostName() +":"+ connector.getDefaultRemoteAddress().getPort()+"]成功");
									break;
								}
							}catch(Exception ex){
								logger.error("重连服务器登录失败,3秒再连接一次:" + ex.getMessage());
							}
						}
					} 
				}
			}
		});
		
		connector.getFilterChain().addLast("mdc", new MdcInjectionFilter());
		TextLineCodecFactory factory = new TextLineCodecFactory(Charset.forName(encoding), LineDelimiter.WINDOWS.getValue(), LineDelimiter.WINDOWS.getValue());
		factory.setDecoderMaxLineLength(10240);
		factory.setEncoderMaxLineLength(10240);
		connector.getFilterChain().addLast("codec", new ProtocolCodecFilter(factory));
		LoggingFilter filter = new LoggingFilter();
		filter.setExceptionCaughtLogLevel(LogLevel.ERROR);
		filter.setMessageReceivedLogLevel(LogLevel.INFO);
		filter.setMessageSentLogLevel(LogLevel.DEBUG);
		filter.setSessionClosedLogLevel(LogLevel.DEBUG);
		filter.setSessionCreatedLogLevel(LogLevel.DEBUG);
		filter.setSessionIdleLogLevel(LogLevel.DEBUG);
		filter.setSessionOpenedLogLevel(LogLevel.DEBUG);
		// 加入日志过滤
		connector.getFilterChain().addLast("logger", filter);
		// 设置接收缓冲区的大小
		connector.getSessionConfig().setReceiveBufferSize(10240);
		// 设置输出缓冲区的大小
		connector.getSessionConfig().setSendBufferSize(10240);
		// 读写通道10s内无操作,则进入空闲状
		connector.getSessionConfig().setIdleTime(IdleStatus.BOTH_IDLE, 10);
		// 设置默认访问地址
		connector.setDefaultRemoteAddress(new InetSocketAddress(host, port));
		//添加服务监听
//		connector.addListener(this.listener);
		if (this.handler != null) {
			connector.setHandler(this.handler);
//			this.handler.setNioSocketConnector(connector);
		} else {
			logger.info("无业务处理类");
		}
		for (int i = 0; i < 100; i++) {
			try {
				ConnectFuture future = connector.connect();
				// 等待连接创建成功
				future.awaitUninterruptibly();
				// 获取会话
				session = future.getSession();
				
				
				logger.error("连接服务端" + host + ":" + port + "[成功]" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));
				break;
			} catch (RuntimeIoException e) {
				System.out.println("连接服务端" + host + ":" + port + "失败" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage());
				logger.error("连接服务端" + host + ":" + port + "失败" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage(), e);
				Thread.sleep(5000);// 连接失败后,重连10次,间隔30s
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
				// session.write(logo != null ? logo : "QUIT");
				session.close(true);
				// 等待连接关闭
				session.getCloseFuture().awaitUninterruptibly();
//				logger.info("与服务器：" + HOSTNAME + ":" + PORT + ",正常断开连接");
			}
		}
		try{
			connector.dispose();
		}catch(Exception e){
			logger.info("与服务器：" + host + ":" + port + ",正常断开连接异常:"+ e);
		}
		logger.info("与服务器：" + host + ":" + port + ",断开连接");
	}

	/**
	 * @param b
	 */
	public boolean write(byte[] b) {
		if (session != null && session.isConnected()) {
			session.write(b);
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

	/*****************************************
	 * <li>描        述：测试端口是否能正常连接 		</li><br>
	 * <li>时        间：2013-9-25  上午10:41:02	</li><br>
	 * <li>参数： @param serverHost
	 * <li>参数： @param serverPort			</li><br>
	 * 
	 *****************************************/
	public boolean testMSGStatus() {
		try {
 			if(host.contains("127.0.0.1")){
				logger.info("连接IP为[127.0.0.1],不做测试!");
				 return true;
			}
			Socket socket = null;
			BufferedReader in = null;
			PrintWriter out = null;
			String response;
			int loginIndex = 0;
			try {
				socket = new Socket(host, port);
				if(!socket.isConnected()){
					System.out.println("本机:["+socket.getLocalAddress()+"]连接远程服务器["+socket.getInetAddress()+"],端口:["+socket.getPort()+"]失败!");
					logger.error("测试连接通讯服务器异常:本机:["+socket.getLocalAddress()+"]连接远程服务器["+socket.getInetAddress()+"],端口:["+socket.getPort()+"]失败!");
					return false;
				}
				in = new BufferedReader(new InputStreamReader(socket.getInputStream(), "GBK"));
				out = new PrintWriter(new OutputStreamWriter(socket.getOutputStream(), "GBK"));
				StringBuffer loginStr = new StringBuffer();
				loginStr.append("LOGI ");
				loginStr.append(handler.getLoginType()).append(":").append(handler.getGroup()).append(" ");
				loginStr.append(handler.getUserName()).append(":").append(handler.getGroupId()).append(" ");
				loginStr.append(handler.getPassword()).append(" \r\n"); 
				System.out.println("发送测试登录到通讯服务器:" + loginStr.substring(0, loginStr.length() - 3)); 
				logger.error("发送测试登录到通讯服务器:" + loginStr.substring(0, loginStr.length() - 3));
				out.write(loginStr.toString());
				out.flush();
				while (true) {
					if(in.ready()){
						response = in.readLine();
						/**
						 * 正常:LACK 0 0 0 10002
						 * 重复登录:LACK -2
						 * 无此用户:LACK -4
						 */
						if(response.startsWith("LACK 0 0 0")){
							System.out.println("测试连接通讯服务器异常:正常!");
							logger.error("测试连接通讯服务器异常:正常!");
							return true;
						} else if(response.startsWith("LACK -2")){
							System.out.println("测试连接通讯服务器异常:重复登录!");
							logger.error("测试连接通讯服务器异常:重复登录!");
							return false;
						}else if(response.startsWith("LACK -4")){
							System.out.println("测试连接通讯服务器异常:无此用户!");
							logger.error("测试连接通讯服务器异常:无此用户!");
							return false;
						}else{
							loginIndex++;
						}
					}else {
						Thread.sleep(1);
						loginIndex++;
					}
					if(loginIndex == 10){
						logger.error("测试连接通讯服务器超时!");
						System.out.println("测试连接通讯服务器超时!");
						return false;
					}
				}
			}catch(Exception e){
				logger.error("测试连接通讯服务器异常:"+ e.getMessage(), e);
				System.out.println("测试连接通讯服务器异常:"+ e.getMessage());
				return false;
			}finally{
				if (in != null){
					in.close();
				}
				if (out != null){
					out.close();				
				}
				if (socket != null){
					socket.close();
				}
			}	
		} catch (Exception e) {
			logger.error("测试连接通讯服务器异常:"+ e.getMessage(), e);
			System.out.println("测试连接通讯服务器异常:"+ e.getMessage());
			return false;
		}
	}
	
	public IoSession getSession() {
		return session;
	}

	public void setSession(IoSession session) {
		this.session = session;
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
//	public IoListener getListener() {
//		return listener;
//	}
//
//	public void setListener(IoListener listener) {
//		this.listener = listener;
//	}


}
