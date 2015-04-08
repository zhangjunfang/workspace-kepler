package com.ctfo.storage.parse;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.service.RedisService;
import com.ctfo.storage.util.Constant;
import com.ctfo.storage.util.Tools;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 消息应答<br>
 * 描述： 消息应答<br>
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
 * <td>2014-10-27</td>
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
@SuppressWarnings("static-access")
public class FileResponseListen extends Thread {

	private static Logger logger = LoggerFactory.getLogger(FileResponseListen.class);

	/** 队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);

	/** 应答数量 */
	private static int count = 0;

	/** 会话 */
	private static IoSession session;

	/** Redis接口 */
	private RedisService redisService;

	/** 消息服务器会话列表 */
	private static Map<String, IoSession> fileMsgMap = new ConcurrentHashMap<String, IoSession>();

	public FileResponseListen() {
		setName("ResponseListen");
		redisService = new RedisService();
	}

	public void run() {
		logger.info("--ResponseListen-- 应答监听线程启动！");
		while (true) {
			try {
				String response = queue.take();
				process(response);
			} catch (InterruptedException e) {
				logger.error("应答监听线程异常:" + e.getMessage());
			}
		}
	}

	/**
	 * 发送应答
	 * 
	 * @param response
	 */
	public void process(String response) {
		StringBuffer buff = new StringBuffer();
		String arr[] = response.substring(1, response.length() - 1).split("\\" + Constant.DOLLAR);

		buff.append(Constant.LEFT_BRACKET);
		buff.append(arr[0] + Constant.DOLLAR); // 服务站id
		buff.append(arr[1]); // 流水号

		if (response.indexOf(Constant.TYPE_L) > 0) { // 客户端链路管理类
			buff.append(Constant.TYPE_A); // 消息主类型
			buff.append(arr[3].replace("L", "A") + Constant.DOLLAR); // 子类型
			buff.append(arr[4] + Constant.DOLLAR); // 时间戳
			if (arr[3].equals(Constant.TYPE_L1)) { // 登录验证
				buff.append(getLoginResponse(arr) + Constant.DOLLAR);
			} else if (arr[3].equals(Constant.TYPE_L2)) { // 心跳
				buff.append(Constant.N1 + Constant.DOLLAR);
			}
		} else { // 通用应答
			buff.append(Constant.TYPE_R); // 消息主类型
			buff.append(Constant.DOLLAR); // 消息子类型为空
			buff.append(arr[4] + Constant.DOLLAR); // 时间戳
			buff.append(Constant.N1 + Constant.DOLLAR);
		}
		buff.append(Tools.getCheckCode(buff.substring(1, buff.length() - 1))); // 校验码
		buff.append(Constant.RIGHT_BRACKET);
		session.write(buff.toString());
		count++;
		logger.info("SEND:{}", buff.toString());
	}

	/**
	 * 获取登录验证结果
	 * 
	 * @param arr
	 * @return
	 */
	private String getLoginResponse(String arr[]) {
		String response = null;
		String username = arr[5]; // 登录账号
		String password = arr[6]; // 密码
		String authCode = arr[7]; // 鉴权码
		String loginInfo = this.redisService.getLoginInfo(username);
		if (null != loginInfo) {
			if (loginInfo.equals(password + Constant.UNDERLINE + authCode)) {
				fileMsgMap.put(arr[0], session); // 登录验证成功的会话放在内存中
				response = Constant.N1; // 登录成功
			} else {
				response = Constant.N2; // 密码错误
			}
		} else {
			response = Constant.N3; // 用户不存在
		}
		return response;
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public static void put(String response) {
		try {
			queue.put(response);
		} catch (InterruptedException e) {
			logger.error("应答监听queue异常！" + e.getMessage());
		}
	}

	/******************************** GET AND SET ********************************/

	public static int getCount() {
		return count;
	}

	public void setCount(int count) {
		this.count = count;
	}

	public static ArrayBlockingQueue<String> getQueue() {
		return queue;
	}

	public static void setQueue(ArrayBlockingQueue<String> queue) {
		FileResponseListen.queue = queue;
	}

	public IoSession getSession() {
		return session;
	}

	public static void setSession(IoSession session) {
		FileResponseListen.session = session;
	}

	public static Map<String, IoSession> getFileMsgMap() {
		return fileMsgMap;
	}

	public static void setFileMsgMap(Map<String, IoSession> fileMsgMap) {
		FileResponseListen.fileMsgMap = fileMsgMap;
	}

}
