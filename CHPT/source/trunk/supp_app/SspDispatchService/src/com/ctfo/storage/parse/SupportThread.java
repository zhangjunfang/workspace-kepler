package com.ctfo.storage.parse;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.service.MySqlService;
import com.ctfo.storage.util.ConfigLoader;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 支撑系统监控类数据同步<br>
 * 描述： 支撑系统监控类数据同步<br>
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
 * <td>2014-12-4</td>
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
public class SupportThread extends Thread {

	private static Logger logger = LoggerFactory.getLogger(SupportThread.class);

	/** 数据队列 */
	private static ArrayBlockingQueue<Object> queue = new ArrayBlockingQueue<Object>(500000);

	/** 数据Map */
	private static Map<String, List<Object>> supportMap = new ConcurrentHashMap<String, List<Object>>();

	/** 提交数据库总条数 */
	private static Map<String, Integer> processSizeMap = new ConcurrentHashMap<String, Integer>();

	/** 默认批量提交条数 */
	private int batchSize = 50;

	/** 最后处理时间 */
	private static long lastTime = System.currentTimeMillis();

	private static MySqlService mySqlService;

	public SupportThread() {
		batchSize = Integer.valueOf(ConfigLoader.commitParamMap.get("commitBatchCount"));
		mySqlService = new MySqlService();
		mySqlService.setSqlMap(ConfigLoader.sqlMap);
	}

	public void run() {
		while (true) {
			try {
				Object o = queue.take();
				process(o);
			} catch (Exception e) {
				logger.error("支撑系统监控类数据队列数据异常:" + e.getMessage());
			}
		}
	}

	private void process(Object o) {
		String className = o.getClass().getSimpleName();
		List<Object> list = supportMap.get(className);
		if (null == list) {
			list = new ArrayList<Object>();
		}
		list.add(o);
		if (list.size() == batchSize) {
			long start = System.currentTimeMillis();
			if ("TbComInfo".equals(className)) {
				mySqlService.tbComInfoUpdate(list);
			} else if ("TbUserOnline".equals(className)) {
				mySqlService.tbUserOnlineInsert(list);
			} else if ("TbUserBehaviorMonitor".equals(className)) {
				mySqlService.tbUserBehaviorMonitorInsert(list);
			} else if ("TlUserFunctionLog".equals(className)) {
				mySqlService.tlUserFunctionLogInsert(list);
			}
			int processSize = 0;
			if (processSizeMap.containsKey(o.getClass().getSimpleName())) {
				processSize = processSizeMap.get(o.getClass().getSimpleName()) + list.size();
			} else {
				processSize = list.size();
			}
			processSizeMap.put(o.getClass().getSimpleName(), processSize);
			supportMap.remove(o.getClass().getSimpleName());
			logger.info("【{}】批量提交:{}, 耗时:{}ms, 共提交总数:{}", o.getClass().getSimpleName(), list.size(), (System.currentTimeMillis() - start), processSize);
		} else {
			supportMap.put(o.getClass().getSimpleName(), list);
		}
	}

	/**
	 * 定时批量操作
	 * 
	 * @param mySqlService
	 */
	public static void updateList(MySqlService mySqlService) {
		Iterator<String> iter = supportMap.keySet().iterator();
		while (iter.hasNext()) {
			long start = System.currentTimeMillis();
			String key = (String) iter.next();
			List<Object> value = supportMap.get(key);
			if ("TbComInfo".equals(key)) {
				mySqlService.tbComInfoUpdate(value);
			} else if ("TbUserOnline".equals(key)) {
				mySqlService.tbUserOnlineInsert(value);
			} else if ("TbUserBehaviorMonitor".equals(key)) {
				mySqlService.tbUserBehaviorMonitorInsert(value);
			} else if ("TlUserFunctionLog".equals(key)) {
				mySqlService.tlUserFunctionLogInsert(value);
			}

			int processSize = 0;
			if (processSizeMap.containsKey(key)) {
				processSize = processSizeMap.get(key) + value.size();
			} else {
				processSize = value.size();
			}
			processSizeMap.put(key, processSize);

			supportMap.remove(key);
			lastTime = System.currentTimeMillis();
			logger.info("【{}】 批量提交:{}, 耗时:{}ms, 共提交总数:{}", key, value.size(), (System.currentTimeMillis() - start), processSize);
		}
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public void put(Object o) {
		try {
			queue.put(o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}

	public static long getLastTime() {
		return lastTime;
	}

	public static void setLastTime(long lastTime) {
		SupportThread.lastTime = lastTime;
	}

}
