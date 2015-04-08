package com.ctfo.ypt.client;

import java.util.Date;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.util.DateUtil;
import com.ctfo.util.SerialUtil;
import com.ctfo.util.Tools;

/**
 * 
 * 云平台连接事件
 * 
 * @author 蒋东卿
 * @date 2014年11月17日下午7:40:55
 * @since JDK1.6
 */ 
public class YptClientHandler extends IoHandlerAdapter{
	
	private static final Logger logger = LoggerFactory.getLogger(YptClientHandler.class);
	
	// 当客户端连接进入时  
    @Override  
    public void sessionOpened(IoSession session) throws Exception {  
    	String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
    	String msgServerLoginName = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("msgServerLoginName");
    	String msgServerLoginPassword = 
    			(String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("msgServerLoginPassword");
    	String msgServerAuthenticationCode = 
    			(String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("msgServerAuthenticationCode");
    	
    	StringBuffer msg = new StringBuffer();
    	msg.append(serviceStationId).append("$");//服务站ID
    	msg.append(SerialUtil.getInt()).append("$"); //流水号
    	msg.append("L").append("$"); //消息主类型
    	msg.append("L1").append("$");//消息子类型
    	msg.append(DateUtil.dateToUtcTime(new Date())).append("$"); //时间戳
    	msg.append(msgServerLoginName).append("$");//登录帐号
    	msg.append(msgServerLoginPassword).append("$");//登录密码
    	msg.append(msgServerAuthenticationCode);//鉴权码
    	
    	//生成校验码
    	String checkContent = msg.toString();
    	String checkCode = Tools.getCheckCode(checkContent);
    	
    	//协议文本
    	StringBuffer msg_ = new StringBuffer();
    	msg_.append("[");
    	msg_.append(checkContent).append("$");
    	msg_.append(checkCode);
    	msg_.append("]");
    	
    	System.out.println("client---进入: " + session.getRemoteAddress());
    //    logger.info("client---进入: " + session.getRemoteAddress());  
        
        //登录云平台
        session.write(msg_.toString());  
    }  
  
    @Override  
    public void exceptionCaught(IoSession session, Throwable cause)  
            throws Exception {  
    	System.out.println("client---客户端发送信息异常....");
    //    logger.info("client---客户端发送信息异常....");  
    }  
  
    // 收到服务端消息
    @Override  
    public void messageReceived(IoSession session, Object message) throws Exception {  
    	System.out.println("client---服务器返回的数据：" + message.toString());
    //	logger.info("client---服务器返回的数据：" + message.toString());  
    }  
  
    //客户端消息发送时
    @Override  
    public void messageSent(IoSession session, Object message) throws Exception{
    	System.out.println("client---客户端发送的数据：" + message.toString());
    //	logger.info("client---客户端发送的数据：" + message.toString()); 
    }
    
	/**
	 * 连接空闲时
	 */
	@Override
	public void sessionIdle(IoSession session, IdleStatus status) throws Exception {
		String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
		StringBuffer msg = new StringBuffer();
		msg.append(serviceStationId).append("$");//服务站id
		msg.append(SerialUtil.getInt()).append("$"); //流水号
		msg.append("L").append("$"); //消息主类型
		msg.append("L2").append("$"); //消息子类型
		msg.append(DateUtil.dateToUtcTime(new Date())); //时间戳
		
    	//生成校验码
    	String checkContent = msg.toString();
    	String checkCode = Tools.getCheckCode(checkContent);
		
    	//协议文本
    	StringBuffer msg_ = new StringBuffer();
    	msg_.append("[");
    	msg_.append(checkContent).append("$");
    	msg_.append(checkCode);
    	msg_.append("]");
    	
		//发送心跳
		session.write(msg_.toString());
	}
    
	/**
	 * 连接关闭
	 */
    @Override  
    public void sessionClosed(IoSession session) throws Exception {  
        System.out.println("client---客户端与服务端断开连接.....");
   // 	logger.info("client---客户端与服务端断开连接.....");  
    }  
  
	/**
	 * 当一个新的连接建立
	 */
    @Override  
    public void sessionCreated(IoSession session) throws Exception {  
        System.out.println("client---创建一个连接" + session.getRemoteAddress());
    //	logger.info("client---创建一个连接" + session.getRemoteAddress());  
    }
}
