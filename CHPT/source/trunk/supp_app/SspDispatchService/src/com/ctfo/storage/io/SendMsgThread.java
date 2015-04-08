package com.ctfo.storage.io;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.util.Constant;
import com.ctfo.storage.util.SerialUtil;
import com.ctfo.storage.util.Tools;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 通讯发送数据处理类<br>
 * 描述： 通讯发送数据处理类<br>
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
 * <td>2014-10-28</td>
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
public class SendMsgThread extends Thread {

	/** 日志 */
	private static Logger logger = LoggerFactory.getLogger(SendMsgThread.class);

	/** 数据队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);

	/** 消息服务器会话列表 */
	private static Map<String, IoSession> msgMap = new ConcurrentHashMap<String, IoSession>();

	/** 提交数据库总条数 */
	private static int processSize = 0;

	public SendMsgThread() {
		setName("SendMsgThread");
	}

	/**
	 * 发送数据线程执行体
	 */
	public void run() {
		while (true) {
			try {
				String message = queue.take(); // 获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）
				process(message);
			} catch (Exception e) {
				logger.error("发送数据异常:" + e.getMessage());
			}
		}
	}

	/**
	 * 发送数据
	 * 
	 * @param message
	 */
	private void process(String message) {
		Long start = System.currentTimeMillis();
		String stationId = null; // 服务站Id
		IoSession session = null; // 客户端连接会话
		String sendMsg = null; // 需发送的数据
		try {
			if (message.indexOf(Constant.TYPE_Y) > 0 || message.indexOf(Constant.TYPE_S) > 0 || message.indexOf(Constant.TYPE_C) > 0) { // 车厂数据和支撑系统数据
				stationId = message.split("\\" + Constant.DOLLAR)[5];
			} else if (message.indexOf(Constant.TYPE_W) > 0) { // 错误数据
				stationId = message.substring(1, message.indexOf(Constant.DOLLAR));
			}
			if (message.indexOf(Constant.TYPE_W) > 0) {
				sendMsg = message;
			} else {
				sendMsg = this.getSendMessage(message, stationId);
			}
			session = msgMap.get(stationId); // 客户端连接
			if (null != session) {
				if (session.isConnected()) {
					processSize++;
					// 封装要发送的消息
					session.write(sendMsg);
					logger.info("消息已发送 {}, 耗时:{}ms, 共提交总数:{}", sendMsg, (System.currentTimeMillis() - start), processSize);
				} else {
					msgMap.remove(stationId);
					logger.debug("连接已断开,stationId:{}", stationId);
				}
			} else {
				logger.debug("无连接");
			}
		} catch (Exception e) {
			logger.error("发送数据异常:" + e);
		}
	}

	/**
	 * 封装同步数据
	 * 
	 * @param msg
	 * @param stationId
	 * @return
	 */
	private String getSendMessage(String msg, String stationId) {
		String cunrrentTime = System.currentTimeMillis() + Constant.DOLLAR; // 时间戳
		StringBuffer sendMessage = new StringBuffer();
		String data[] = msg.substring(1, msg.length() - 1).split("\\" + Constant.DOLLAR);

		sendMessage.append(Constant.LEFT_BRACKET);
		sendMessage.append(stationId + Constant.DOLLAR); // 服务站id
		sendMessage.append(SerialUtil.getInt()); // 流水号

		if (msg.indexOf(Constant.TYPE_Y) > 0) { // 车厂数据
			sendMessage.append(Constant.TYPE_YD); // 消息主类型
			sendMessage.append(Constant.DOLLAR); // 子类型为空
			sendMessage.append(cunrrentTime); // 时间戳
			sendMessage.append(data[6] + Constant.DOLLAR); // 数据对象json串
		} else if (msg.indexOf(Constant.TYPE_S) > 0) { // 支撑系统公告数据
			sendMessage.append(Constant.TYPE_SD); // 消息主类型
			sendMessage.append(Constant.TYPE_SD1 + Constant.DOLLAR); // 消息子类型 SD1
			sendMessage.append(cunrrentTime); // 时间戳
			sendMessage.append(data[6] + Constant.DOLLAR); // 操作类型
			sendMessage.append(data[7] + Constant.DOLLAR); // 数据对象json串
		} else if (msg.indexOf(Constant.TYPE_C) > 0) { // 支撑系统控制指令
			sendMessage.append(Constant.TYPE_CD); // 消息主类型
			sendMessage.append(data[3].replace("C", "CD") + Constant.DOLLAR); // 消息子类型 SD1
			sendMessage.append(cunrrentTime); // 时间戳
			sendMessage.append(data[6] + Constant.DOLLAR); // 控制类型
		}
		sendMessage.append(Tools.getCheckCode(sendMessage.substring(1, sendMessage.length() - 1))); // 验证码
		sendMessage.append(Constant.RIGHT_BRACKET);
		return sendMessage.toString();
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public static void put(String data) {
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}

	public static Map<String, IoSession> getMsgMap() {
		return msgMap;
	}

	public static void setMsgMap(Map<String, IoSession> msgMap) {
		SendMsgThread.msgMap = msgMap;
	}

}
