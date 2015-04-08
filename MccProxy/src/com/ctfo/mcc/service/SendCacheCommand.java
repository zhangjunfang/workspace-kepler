package com.ctfo.mcc.service;

import java.sql.SQLException;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.dao.RedisConnectionPool;
import com.ctfo.mcc.model.OnLine;

import redis.clients.jedis.Jedis;


public class SendCacheCommand extends Thread {
	protected static Logger logger = LoggerFactory.getLogger(SendCacheCommand.class);
	/** 连接队列 */
	private static ArrayBlockingQueue<String> vPacket = new ArrayBlockingQueue<String>(100000);
	/** 最后处理时间 */
	private long lastTime = System.currentTimeMillis();
	/** 接收计数器 */
	private int receivedIndex = 0;
	/** 在线数据计数器 */
	private int onlineIndex = 0;
	/** 连接实例 */
	private Jedis jedis;

	public static void addPacket(String command) {
		try {
			vPacket.put(command);
		} catch (InterruptedException e) {
			logger.error(e.getMessage(), e);
		}
	}

	public void init() {
		start();
	}

	public void run() {
		setName("SendCacheCommand");
		try {
			jedis = RedisConnectionPool.getJedisConnection();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		jedis.select(2); // 连接缓存数据库
		process();
	}

	public void process() {
		while (true) {
			String msg;
			try {
				msg = vPacket.take();
				receivedIndex++;
				OnLine online = processOnoffline(msg);
				if (online != null) {
					onlineIndex++;
					String macId = online.getMacId();
					try {
						if (jedis.exists(macId)) {
							Set<String> keys = jedis.hkeys(macId);
							Iterator<String> it = keys.iterator();
							while (it.hasNext()) {
								String type = it.next();
								String value = jedis.hget(macId, type);
								if ("D_SETP_14".equals(type)) { // 判断电子围栏，因为电子围栏以LIST类型存储

									List<String> list = jedis.lrange(value, 0, -1);
									for (String command : list) {
										SendQueue.put(command);
//										connectionService.sendMessageToMSG(command + "\r\n");
									}// End for
								} else {
									String command = jedis.get(value);
									SendQueue.put(command);
//									connectionService.sendMessageToMSG(command + "\r\n");
								}
								jedis.del(value); // 发送成功删除缓存
							}// End while

							// 删除缓存HSET
							jedis.del(macId);
						}
					} catch (Exception e1) {
						logger.error("处理redis缓存异常[" + macId + "] Error:" + e1.getMessage(), e1);
						if (jedis != null) {
							RedisConnectionPool.returnBrokenResource(jedis);
						}
						jedis = RedisConnectionPool.getJedisConnection();
						jedis.select(2); // 连接缓存数据库
					}
					logger.debug("发送车辆[" + macId + "]消息 to msg " + online.getMsgId());
				}
				long currentTime = System.currentTimeMillis();
				if (currentTime - lastTime > 10000) {
					int size = getQueueSize();
					logger.info("10秒接收[" + receivedIndex + "]条, 在线[" + onlineIndex + "]条, 队列等待:[" + size + "]");
					receivedIndex = 0;
					onlineIndex = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error("向MSG发送消息异常 Send Message To MSG ERROR:" + e.getMessage(), e);
				if (jedis != null) {
					RedisConnectionPool.returnBrokenResource(jedis);
				}
				try {
					jedis = RedisConnectionPool.getJedisConnection();
					jedis.select(2); // 连接缓存数据库
				} catch (Exception e2) {
					logger.error("向MSG发送消息重启Redis异常 Send Message To MSG Restart Redis ERROR:" + e2.getMessage(), e2);
				}
				continue;
			}
		}// End while
	}

	public Integer getQueueSize() {
		return vPacket.size();
	}

	private OnLine processOnoffline(String msg) {
		if (msg == null || msg.length() == 0) {
			return null;
		}
		String[] array = StringUtils.split(msg);
		if (array != null && array.length == 6) {
			String key = array[0]; // 关键字
			String macid = array[2]; // 通讯码
			String mtype = array[4]; // 指令字
			String content = array[5].substring(1, array[5].length() - 1); // 内容
			String[] contentArray = StringUtils.split(content, "/");
			if (key.equals("CAITS") && mtype.equals("U_REPT") && contentArray.length == 4 && content.indexOf("TYPE:5,18:1") > -1) {
				OnLine online = new OnLine();
				online.setMacId(macid);
				online.setMsgId(contentArray[3]);
				return online;
			}
		}
		return null;
	}


	// public static void main(String[] args) {
	// SendCacheCommand scc = new SendCacheCommand();
	// scc.processOnoffline("CAITS 0_0 E001_15290423433 0 U_REPT {TYPE:5,18:1/0/0/10002}");
	// System.exit(0);
	// }
}
