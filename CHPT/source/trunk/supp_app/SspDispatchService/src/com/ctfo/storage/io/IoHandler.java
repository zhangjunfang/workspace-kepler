package com.ctfo.storage.io;

import java.util.Map;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.parse.Parse;
import com.ctfo.storage.util.Constant;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 通讯接收处理器<br>
 * 描述： 通讯接收处理器<br>
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
 * <td>2014-10-15</td>
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
public class IoHandler extends IoHandlerAdapter {

	private final static Logger logger = LoggerFactory.getLogger(IoHandler.class);
	private final static Logger requestLog = LoggerFactory.getLogger("requestLog");

	/** 指令解析线程 */
	private Parse parse;

	public IoHandler() {
		parse = new Parse();
		parse.start();
	}

	/**
	 * 当接收了一个消息时调用
	 */
	@Override
	public void messageReceived(IoSession iosession, Object message) throws Exception {
		try {
			if (message instanceof String) {
				String arr[] = message.toString().substring(1, message.toString().length() - 1).split("\\" + Constant.DOLLAR);
				if (SendMsgThread.getMsgMap().containsKey(arr[0])) {
					parse.put(message.toString());
				}
			}
		} catch (Exception e) {
			logger.info("---通讯接收处理器异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 当一个新的连接建立
	 */
	@Override
	public void sessionCreated(IoSession session) throws Exception {
		requestLog.info("创建一个新连接：{}", session.getRemoteAddress());
	}

	/**
	 * 连接关闭
	 */
	@Override
	public void sessionClosed(IoSession session) throws Exception {
		requestLog.info("服务端与客户端连接断开...,ip为:" + session.getRemoteAddress());
		if (SendMsgThread.getMsgMap().size() > 0) {
			for (Map.Entry<String, IoSession> entry : SendMsgThread.getMsgMap().entrySet()) {
				if (entry.getValue().equals(session)) {
					SendMsgThread.getMsgMap().remove(entry.getKey());
				}
			}
		}
	}

	/**
	 * 连接空闲时
	 */
	@Override
	public void sessionIdle(IoSession session, IdleStatus status) throws Exception {
	}

	/**
	 * 当接口中其他方法抛出异常未被捕获时触发此方法
	 */
	@Override
	public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
		// logger.error("服务端发送数据异常:" + cause.getMessage(), cause);
	}
}
