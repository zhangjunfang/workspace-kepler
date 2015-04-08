package com.ctfo.storage.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.service.MySqlService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 存储操作异常数据处理<br>
 * 描述： 存储操作异常数据处理<br>
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
 * <td>2014-11-24</td>
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
public class ExceptionDataListenHandler {

	private static Logger logger = LoggerFactory.getLogger(ExceptionDataListenHandler.class);

	/** 添加异常数据队列 */
	private static ArrayBlockingQueue<List<Object>> addQueue = null;

	/** 修改异常数据队列 */
	private static ArrayBlockingQueue<List<Object>> updateQueue = null;

	/** 删除异常数据队列 */
	private static ArrayBlockingQueue<List<Object>> deleteQueue = null;

	private ExceptionDataThread exceptionDataThread;

	private MySqlService mySqlService = new MySqlService();

	public ExceptionDataListenHandler() {
		addQueue = new ArrayBlockingQueue<List<Object>>(500000);
		updateQueue = new ArrayBlockingQueue<List<Object>>(500000);
		deleteQueue = new ArrayBlockingQueue<List<Object>>(500000);
		mySqlService = new MySqlService();
		exceptionDataThread = new ExceptionDataThread();
		exceptionDataThread.start();
		// autoCommit();
	}

	public void autoCommit() {
		new Thread("ExceptionDataListenHandler") {
			public void run() {
				while (true) {
					// 增加操作
					try {
						int addSize = addQueue.size();
						if (addSize > 0) {
							List<Object> list = new ArrayList<Object>();
							list.addAll(addQueue.poll());
							for (Object data : list) {
								mySqlService.add(data);
							}
						}
					} catch (Exception e) {
						logger.error(getName() + "异常数据处理出错:\n" + e.getMessage());
					}
					// 修改操作
					try {
						int updateSize = updateQueue.size();
						if (updateSize > 0) {
							List<Object> list = new ArrayList<Object>();
							list.addAll(updateQueue.poll());
							for (Object data : list) {
								mySqlService.update(data); // 逻辑删除
							}
						}
					} catch (Exception e) {
						logger.error(getName() + "异常数据处理出错:\n" + e.getMessage());
					}
					// 删除操作
					try {
						int deleteSize = deleteQueue.size();
						if (deleteSize > 0) {
							List<Object> list = new ArrayList<Object>();
							list.addAll(deleteQueue.poll());
							for (Object data : list) {
								mySqlService.delete(data);
							}
						}
					} catch (Exception e) {
						logger.error(getName() + "异常数据处理出错:\n" + e.getMessage());
					}
				}
			}
		}.start();
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public static void putAddQueue(List<Object> o) {
		try {
			addQueue.put(o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}

	public static void putUpdateQueue(List<Object> o) {
		try {
			updateQueue.put(o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}

	public static void putDeleteQueue(List<Object> o) {
		try {
			deleteQueue.put(o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}

}
