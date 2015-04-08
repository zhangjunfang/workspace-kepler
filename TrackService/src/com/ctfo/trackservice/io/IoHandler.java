/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.io FileSaveServiceHandler.java	</li><br>
 * <li>时        间：2013-9-9  上午9:39:29	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.io;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.common.MsgProperties;
import com.ctfo.trackservice.parse.CommandParseThread;

/*****************************************
 * <li>描 述：通讯接收处理器
 * 
 *****************************************/
public class IoHandler extends IoHandlerAdapter {
	private static final Logger logger = LoggerFactory.getLogger(IoHandler.class);
	/** 登录MSG用户名 */
	private String userName;
	/** 登录MSG密码 */
	private String password;
	/** 登录MSG组编号 */
	private String groupId;
	/** 登录MSG组名 */
	private String group;
	/** 登录MSG类型 */
	private String loginType;
	/** 最近时间 */
	private long lastTime = System.currentTimeMillis();
	/** 计数器 */
	private int index = 0;
	/**  登录用户	 */
	private MsgProperties msgProperties;
	private CommandParseThread commandParseThread;
	
	public IoHandler(MsgProperties properties, CommandParseThread commandParse) {
		msgProperties = properties;
		commandParseThread = commandParse;
	}

	@Override
	public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
		logger.error("解析MSG信息异常!");
		if(logger.isDebugEnabled()){
			logger.error(cause.getMessage()); 
		}
	}

	@Override
	public void messageReceived(IoSession session, Object message) throws Exception {
		try {
			logger.debug("RECEIVED - {}", message);
			// 30s发一次心跳
			long currentTime = System.currentTimeMillis();
			if (currentTime - lastTime > 10000) {
				sendHeartbeat(session);
				lastTime = currentTime;
				logger.info("---monitor----10s接收:" + index);
				index = 0;
			}
			index++;
			if (message instanceof String) {
				String msg = message.toString();
				if(msg.startsWith("LACK")){
					logger.info("RECEIVED - {}",msg);
					if (msg.startsWith("LACK 0")) {
						logger.info("--Login OK!");
					} else if (msg.startsWith("LACK -1")) {// -1 密码错误
						logger.info("--通讯连接异常:" + msg + "-->密码错误");
					} else if (msg.startsWith("LACK -2")) {// -2 帐号已经登录
						logger.info("--通讯连接异常:" + msg + "-->帐号已经登录");
					} else if (msg.startsWith("LACK -3")) {// -3 帐号已经停用
						logger.info("--通讯连接异常:" + msg + "-->帐号已经停用");
					} else if (msg.startsWith("LACK -4")) {// -4 帐号不存在
						logger.info("--通讯连接异常:" + msg + "-->帐号不存在");
					} else if (msg.startsWith("LACK -5")) {// -5 sql查询失败
						logger.info("--通讯连接异常:" + msg + "-->sql查询失败");
					} else if (msg.startsWith("LACK -6")) {// -6 未登录数据库
						logger.info("--通讯连接异常:" + msg + "-->未登录数据库");
					}
				} else if(msg.startsWith("NOOP")){
					logger.info("RECEIVED - {}",msg);
				} else {
					commandParseThread.addCommand(msg);
				}
			}
		} catch (Exception e) {
			logger.info("---通讯接收处理器异常:" + e.getMessage(), e);
		}
	}

	@Override
	public void messageSent(IoSession session, Object message) throws Exception {
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
	public void sessionIdle(IoSession session, IdleStatus status) throws Exception {
		// 发送心跳
		sendHeartbeat(session);
	}

	@Override
	public void sessionOpened(IoSession session) throws Exception {
		StringBuffer loginStr = new StringBuffer();
		loginStr.append("LOGI ");
		loginStr.append(msgProperties.getLoginType()).append(":").append(msgProperties.getMsgGroup()).append(" ");
		loginStr.append(msgProperties.getMsgUserName()).append(":").append(msgProperties.getMsgGroupId()).append(" ");
		loginStr.append(msgProperties.getMsgPassword()).append(" \r\n");
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

	/*-----------------------------getter & setter-----------------------------------*/
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
