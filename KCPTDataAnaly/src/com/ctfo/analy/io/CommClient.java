package com.ctfo.analy.io;

import java.net.InetSocketAddress;
import java.nio.charset.Charset;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.service.IoHandlerAdapter;
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

import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.protocal.AnalyseServiceInit;


/*****************************************
 * <li>描        述：连接客户端		
 * 
 *****************************************/
public class CommClient {
	private static final Logger logger = LoggerFactory.getLogger(CommClient.class);
	/** 接收发数据的业务类 */
	private IoHandler handler = new IoHandler();
	/** 监听 */
//	private IoListener listener;
	/** 服务端IP */
	private String host;
	/** 端口 */
	private int port;
	
	/** 登录MSG用户名	*/
	private String userName;
	/** 登录MSG密码	*/
	private String password;
	/** 登录MSG组编号	*/
	private String groupId;
	/** 登录MSG组名	*/
	private String group;
	/** 登录MSG类型	*/
	private String loginType;
	
	/** 会话 */
	private IoSession session;
	/** 连接管理器 */
	private NioSocketConnector connector;
	/** 字符编码 */
	private String encoding = "GBK";
	/** 空闲间隔 */
	private static long CONNECT_TIMEOUT = 30 * 1000L; // 30 seconds
	
	private boolean isLogined = false;

	/**
	 * 
	 * @param ip
	 * @param port
	 * @param handler
	 */
	public CommClient() {
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

		connector.getFilterChain().addLast("mdc", new MdcInjectionFilter());
		MyTextLineCodecFactory factory = new MyTextLineCodecFactory(Charset.forName(encoding), LineDelimiter.WINDOWS.getValue(), LineDelimiter.WINDOWS.getValue());
		factory.setDecoderMaxLineLength(10240);
		factory.setEncoderMaxLineLength(10240);
		connector.getFilterChain().addLast("codec", new ProtocolCodecFilter(factory));
		LoggingFilter filter = new LoggingFilter();
		filter.setExceptionCaughtLogLevel(LogLevel.ERROR);
		filter.setMessageReceivedLogLevel(LogLevel.INFO);
		filter.setMessageSentLogLevel(LogLevel.DEBUG);
		filter.setSessionClosedLogLevel(LogLevel.DEBUG);
		filter.setSessionCreatedLogLevel(LogLevel.DEBUG);
		filter.setSessionIdleLogLevel(LogLevel.INFO);
		filter.setSessionOpenedLogLevel(LogLevel.INFO);
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
		} else {
			connector.setHandler(new IoHandler());
		}
		
		try {
			ConnectFuture future = connector.connect();
			// 等待连接创建成功
			future.awaitUninterruptibly();
			// 获取会话
			session = future.getSession();

			logger.info("连接服务端" + host + ":" + port + "[成功]" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()));

		} catch (Exception e) {
			logger.error("连接服务端" + host + ":" + port + "失败" + ",,时间:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date()) + ", 连接MSG异常,请检查MSG端口、IP是否正确,MSG服务是否启动,异常内容:" + e.getMessage(), e);
			reconnect();
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

	
	class IoHandler extends IoHandlerAdapter{

		/** 最近时间  */
		private long lastTime = System.currentTimeMillis();
		/** 计数器	  */
		private int index = 0;
		
		@Override
		public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
			logger.error("连接MSG ("+userName+")客户端发送消息异常:"+ cause.getMessage(), cause);
		}


		@Override
		public void messageReceived(IoSession session, Object message) throws Exception {
			try{
			if(message instanceof String){
				DataPool.setCountValue();
				String s = message.toString();
				logger.debug("从"+session.getRemoteAddress() + "收到:" + s);
				if (s!=null&&!"".equals(s.trim())) {
					String[] command = s.split("\\s+");
					if ("LACK".equals(command[0])){
						//接收到登陆确认消息
						if (command.length>=2&&("0".equals(command[1])||"1".equals(command[1])||"2".equals(command[1]))){
							isLogined = true;
						}
					}
					if (isLogined){
						//3s接收指令数量
						long currentTime =System.currentTimeMillis();
						if( currentTime - lastTime >= 3000){
							sendHeartbeat(session);
							lastTime = currentTime;
							logger.info("---monitor----3s接收:"+index);
							index =0;
						}
						index++;
						
						if ("NOOP_ACK".equals(command[0])){
							//接收到空操作确认消息
						}else if ("CAITS".equals(command[0])){
							//接收到终端上传消息
							String msgid = "0";
							if (command.length == 5) {
								msgid = command[4];
							}
							
							MessageBean messageBean = new MessageBean();
							messageBean.setCommand(s);
							messageBean.setMsgid(msgid);
							
							AnalyseServiceInit.getAnalyseServiceThread()[0].addPacket(messageBean);
						}
						//接收结束后发送一条缓存中指令
						sendData(session);
					}
				}
			}
			}catch(Exception e){
				logger.info("---通讯接收处理器异常:"+e.getMessage(),e);
			}
		}

		@Override
		public void messageSent(IoSession session, Object message) throws Exception {
		}

		@Override
		public void sessionClosed(IoSession session) throws Exception {
			logger.error("客户端与服务端[关闭]连接---sessionClosed;将开始重连！");
			isLogined = false;
			reconnect();
		}

		@Override
		public void sessionCreated(IoSession session) throws Exception {
			logger.info("-客户端与服务端[建立]连接---sessionCreated");
		}

		@Override
		public void sessionIdle(IoSession session, IdleStatus status) throws Exception {
			//30s发一次心跳
			long currentTime =System.currentTimeMillis();
			if( currentTime - lastTime >= 3000){
				sendHeartbeat(session);
				lastTime = currentTime;
				logger.info("---monitor----3s接收:"+index);
				index =0;
			}
			//空闲时发送数据
			sendData(session);
		}

		@Override
		public void sessionOpened(IoSession session) throws Exception {
			//session建立后向服务器发送登陆消息
			loginIn(session);
		}
	}
	
	public void loginIn(IoSession session){
		StringBuffer loginStr = new StringBuffer();
		loginStr.append("LOGI ");
		/*loginStr.append(loginType).append(":").append(group).append(" ");
		loginStr.append(userName).append(":").append(groupId).append(" ");*/
		loginStr.append(loginType).append(" ");
		loginStr.append(userName).append(" ");
		loginStr.append(password).append(" \r\n "); 
		session.write(loginStr.toString());
		logger.info("已向MSG发送登陆信息:"+loginStr.toString());
	}
	
	/**
	 * 发送心跳数据
	 * @param session
	 */
	public void sendHeartbeat(IoSession session){
		session.write("NOOP \r\n");
		logger.debug( "已向MSG发送心跳信息---sendHeartbeat");
	}
	
	public void sendData(IoSession session){
		//结束时发送现有数据
		MessageBean messageB = DataPool.getReceivePacket();
		if (messageB!=null){
			session.write(messageB.getCommand());
		}
	}
	
	public void reconnect(){
		try {
			if (session!=null){
				session.close(true);
				session = null;
			}
			connector = null;
			Thread.sleep(5000);//间隔5s后重连
			connect();
		}catch(Exception ex){
			logger.error("重连服务器失败过程中,稍后继续重连！",ex);
			reconnect();
		}
	}
	
	public String getUserName() {
		return userName;
	}
	public void setUserName(String userName) {
		this.userName = userName;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public String getGroupId() {
		return groupId;
	}
	public void setGroupId(String groupId) {
		this.groupId = groupId;
	}
	public String getGroup() {
		return group;
	}
	public void setGroup(String group) {
		this.group = group;
	}
	public String getLoginType() {
		return loginType;
	}
	public void setLoginType(String loginType) {
		this.loginType = loginType;
	}

}
