package com.ctfo.storage.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.util.Constant;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 指令解析线程<br>
 * 描述： 指令解析线程<br>
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
 * <td>2014-10-23</td>
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
public class FileParse extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(FileParse.class);

	/** 数据队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);

	/** 文件处理线程 */
	private FileManageThread fileManageThread;

	/** 计数器 */
	private int index;

	/** 最后记录时间 */
	private long lastTime = System.currentTimeMillis();

	public FileParse() {
		setName("FileParse-thread");
		fileManageThread = new FileManageThread();
		fileManageThread.start();
	}

	public void run() {
		while (true) {
			try {
				String message = queue.take(); // 获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）
				index++;
				long current = System.currentTimeMillis();
				if (current - lastTime > 10000) {
					// logger.info("-----------------10秒处理[{}]条,应答[{}],剩余[{}]", index, ResponseListen.getCount(), ResponseListen.getQueue().size());
					index = 0;
					lastTime = System.currentTimeMillis();
				}
				process(message);
			} catch (Exception e) {
				logger.error("Parse处理队列数据异常:" + e.getMessage());
			}
		}
	}

	/**
	 * 数据解析
	 * 
	 * @param message
	 */
	private void process(String message) {
		if (message.indexOf(Constant.TYPE_F) > 0) { // 服务站上传附件类 F
			fileManageThread.put(message);
		}
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public void put(String data) {
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}
}
