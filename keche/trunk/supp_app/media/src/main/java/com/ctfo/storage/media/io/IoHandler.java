/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.io FileSaveServiceHandler.java	</li><br>
 * <li>时        间：2013-9-9  上午9:39:29	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.storage.media.io;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.parse.Parse;
import com.ctfo.storage.media.util.MD5;
import com.ctfo.storage.media.util.SerialUtil;
import com.ctfo.storage.media.util.Tools;




/*****************************************
 * <li>描 述：通讯接收处理器
 * 
 *****************************************/
public class IoHandler extends IoHandlerAdapter {
	private static final Logger logger = LoggerFactory.getLogger(IoHandler.class);
	private MD5 md5;
	
	/** 登录MSG用户名 */
	private String userName;
	/** 登录MSG密码 */
	private String password;
	/** 消息来源	 */
	private int source;
	/** 消息目的地	 */
	private int destination;
	
	private String msgUserName;
	
	private String msgPassword;
	
	private String msgSource;
	
	private String msgDestination;
	
	private Parse parse;
	
	private Header header;
	
	
	/**
	 * 
	 */
	public IoHandler() {
		parse = new Parse();
		parse.start(); 
		md5 = new MD5();
		header = new Header();

		
	}

	/** 最近时间 */
	private long lastTime = System.currentTimeMillis();
	/** 计数器 */
	private int index = 0;

	@Override
	public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
		logger.error(cause.getMessage(), cause); 
//		logger.error("解析MSG信息异常!");
//		if(logger.isDebugEnabled()){
//			logger.equals(cause.getMessage());
//		}
	}

	@Override
	public void messageReceived(IoSession session, Object message) throws Exception {
		try {
			logger.debug("RECEIVED - " + message.toString());
			// 30s发一次心跳
			long currentTime = System.currentTimeMillis();
			if (currentTime - lastTime >= 3000) {
				sendHeartbeat(session);
//				sendSubScription(session);
				lastTime = currentTime;
				logger.info("---monitor----3s接收:" + index);
				index = 0;
			}
			index++;
			if (message instanceof String) {
				parse.put(message.toString()); 
			}
		} catch (Exception e) {
			logger.info("---通讯接收处理器异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 发送订阅
	 */
	private void sendSubScription(IoSession session) {
		/** ---------- 订阅分中心 ---------------  */
		StringBuffer centerLogin = new StringBuffer();
		StringBuffer centerSubScription = new StringBuffer();
		centerLogin.append(msgSource);		// 消息来源
		centerLogin.append(msgDestination); // 消息目的地
		centerLogin.append("1100");			// 消息类型
		centerLogin.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"));		// 消息流水号
//		login.append("000dbce9");
		centerLogin.append("00000004"); 	//长度（固定长度）
		StringBuffer centerBody = new StringBuffer();
		centerBody.append("1103");		//	WORD 	子消息类型  
		centerBody.append("02");		//	BYTE 	0删除订阅；1覆盖订阅；2增量订阅
		centerBody.append("00");		//	BYTE 	为0表示删除或订阅所有中心
//		centerBody.append("00");		//	WORD	主分类类型		重复订阅个数（如果订阅所有就为空）
		centerLogin.append(centerBody); // 	消息体
	    String centerLoginString = centerLogin.toString();
		String centerVerifyCode = Tools.getCheckCode(centerLoginString);
		centerSubScription.append("5b");				// 	消息开头	
		centerSubScription.append(centerLoginString);	//	消息头 + 消息体
		centerSubScription.append(centerVerifyCode);	// 	效验码
		centerSubScription.append("5d");				// 	消息结束
		session.write(centerSubScription.toString());
		logger.info("已向MSG发送中心订阅信息:" + centerSubScription.toString());
		
		/** ---------- 订阅消息类型  ---------------  */
		StringBuffer login = new StringBuffer();
		StringBuffer subScription = new StringBuffer();
		login.append(msgSource);		// 消息来源
		login.append(msgDestination); 	// 消息目的地
		login.append("1100");			// 消息类型
		login.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"));		// 消息流水号
		login.append("00000008"); 		//长度（固定长度）
		StringBuffer body = new StringBuffer();
		body.append("1104");			// WORD	子消息类型
		body.append("02");				// BYTE	0删除订阅；1覆盖订阅；2增量订阅
		body.append("02");				// BYTE	订阅ID个数 为0表示删除或订阅所有消息
		body.append("12071208");// WORD	重复订阅个数
	    login.append(body); 			// 消息体
	    String loginString = login.toString();
		String verifyCode = Tools.getCheckCode(loginString);
		subScription.append("5b");			// 消息开头	
		subScription.append(loginString);	//	消息头 + 消息体
		subScription.append(verifyCode);	// 效验码
		subScription.append("5d");			// 消息结束
		session.write(subScription.toString());
		
		logger.info("已向MSG发送类型订阅信息:" + subScription.toString());
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
		StringBuffer login = new StringBuffer();
		login.append(msgSource);		// 消息来源
		login.append(msgDestination); 	// 消息目的地
		login.append("1100");			// 消息类型
		login.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"));		// 消息流水号
//		login.append("000dbce9");
		login.append("00000032"); 		//长度（固定长度）
		StringBuffer body = new StringBuffer();
		body.append("1101");			// 子消息类型
		body.append(msgUserName);		// 用户名
		body.append(msgPassword);		// 密码
	    login.append(body); 			// 消息体
	    String loginString = login.toString();
		String verifyCode = Tools.getCheckCode(loginString);
		loginStr.append("5b");			// 消息开头	
		loginStr.append(loginString);	//	消息头 + 消息体
		loginStr.append(verifyCode);	// 效验码
		loginStr.append("5d");			// 消息结束
		session.write(loginStr.toString());
		
		logger.info("已向MSG发送登陆信息:" + loginStr.toString());
		sendSubScription(session);
	}

	/**
	 * 发送心跳数据
	 * 
	 * @param session
	 */
	public void sendHeartbeat(IoSession session) {
//		String heart = "5b57c5d80a05397fb11100000dbce9000000021102ea5d";
		StringBuffer heart = new StringBuffer();
		header.setDestination(destination);
		header.setSource(source);
		header.setLength(50);
		header.setSerial(SerialUtil.getInt());
		header.setType("1100");
		String loginString = header.toString() + "1102";
		String verifyCode = Tools.getCheckCode(loginString);
		
		heart.append("5b");				// 开始
		heart.append(loginString);// 消息头 + 消息体
		heart.append(verifyCode);	// 验证码
		heart.append("5d"); 		// 结束
		
		session.write(heart.toString());
		logger.info("已向MSG发送心跳信息---sendHeartbeat:" + heart.toString());
	}

	/*-----------------------------getter & setter-----------------------------------*/
	public String getUserName() {
		return userName;
	}

	public void setUserName(String userName) {
		this.userName = userName;
		this.msgUserName = Tools.fillNBitAfter(Tools.getHzHexStr(userName), 32, "0");;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
		this.msgPassword = Tools.getHzHexStr(md5.getMD5ofStr(password));
	}
	/**
	 * @return 获取 source
	 */
	public int getSource() {
		return source;
	}
	/**
	 * 设置source
	 * @param source source 
	 */
	public void setSource(int source) {
		this.source = source;
		this.msgSource = Tools.fillNBitBefore(Integer.toHexString(source), 8, "0");
	}
	/**
	 * @return 获取 destination
	 */
	public int getDestination() {
		return destination;
	}
	/**
	 * 设置destination
	 * @param destination destination 
	 */
	public void setDestination(int destination) {
		this.destination = destination;
		this.msgDestination = Tools.fillNBitBefore(Integer.toHexString(destination), 8, "0");;
	}
}
