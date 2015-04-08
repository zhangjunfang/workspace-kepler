package com.ocean.transilink.mina.spout;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class MsgConnectPool {
	private static Logger logger = LoggerFactory.getLogger(MsgConnectPool.class);
	private static MsgConnectPool instence = new MsgConnectPool();
	private static IoSession ioSession;
	
	private MsgConnectPool(){
		logger.info("MsgConnectPool启动"); 
	}
	
	public static MsgConnectPool getInstence(){
		if(instence == null){
			instence = new MsgConnectPool();
		} 
		return instence;
	}
	
	/** 消息服务器会话列表 */
	private static Map<String, IoSession> msgMap = new ConcurrentHashMap<String, IoSession>();

	/*****************************************
	 * <li>描 述：添加信息服务器</li><br>
	 * <li>时 间：2013-9-26 下午4:59:14</li><br>
	 * <li>参数： @param msg</li><br>
	 * 
	 *****************************************/
	public void addMsg(IoSession session) {
		ioSession = session; 
	}

	/*****************************************
	 * <li>描 述：发送消息</li><br>
	 * <li>时 间：2013-9-26 下午5:19:06</li><br>
	 * <li>参数： @param msgKey <li>参数： @param message <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public boolean sendMessage(String message) {
		if(ioSession != null){
			ioSession.write(message + " \r\n");
			logger.debug("已成功向MSG发送信息:" + message);
			return true;
		} else {
			logger.error("发送消息异常:MSG连接为空!");
			return false;
		}
	}
	/*****************************************
	 * <li>描 述：删除无效连接</li><br>
	 * <li>时 间：2013-8-8 下午6:39:03</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public  void removeConnector() {
		try {
			for (Map.Entry<String, IoSession> msg : msgMap.entrySet()) {
				msgMap.remove(msg.getKey());
				logger.info("--MsgConnectPool--删除无效连接完成!");
			}
		} catch (Exception e) {
			logger.error("--MsgConnectPool--删除无效连接异常:" + e.getMessage(), e);
		}
	}
}
