package com.ctfo.storage.parse;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.model.file.MessageFile;
import com.ctfo.storage.service.MongoService;
import com.ctfo.storage.service.MySqlService;
import com.ctfo.storage.service.ProtocolAnalyService;
import com.ctfo.storage.util.ConfigLoader;
import com.ctfo.storage.util.Constant;
import com.ctfo.storage.util.Tools;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 文件数据处理线程<br>
 * 描述： 文件数据处理线程<br>
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
 * <td>2014-11-6</td>
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
public class FileManageThread extends Thread {

	private static Logger logger = LoggerFactory.getLogger(FileManageThread.class);
	/** 数据队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);
	/** 附件添加数据List */
	private static List<String[]> addFileList = new ArrayList<String[]>();
	/** 附件更新数据List */
	private static List<String[]> updateFileList = new ArrayList<String[]>();
	/** 附件删除数据List */
	private static List<String[]> deleteFileList = new ArrayList<String[]>();
	/** 最后处理时间 */
	private static long lastTime = System.currentTimeMillis();
	/** 数据解析 */
	private ProtocolAnalyService protocolAnalyService;
	/** MySql服务接口 */
	private static MySqlService mySqlService;
	/** MongoDB接口 */
	private static MongoService mongoService;
	/** 处理数据结束最近时间 */
	private long endLastTime = System.currentTimeMillis();
	/** 默认每30秒提交一次 */
	private long batchTime = 30000;

	public FileManageThread() {
		batchTime = Integer.valueOf(ConfigLoader.commitParamMap.get("commitBatchTime"));
		protocolAnalyService = new ProtocolAnalyService();
		mySqlService = new MySqlService();
		mongoService = new MongoService();
		mongoService.setDbName(ConfigLoader.mongoParamMap.get("dbName"));
		mongoService.setFileDir(ConfigLoader.mongoParamMap.get("fileDir"));
	}

	public void run() {
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				int addSize = queue.size();
				if (addSize > 0 && ((currentTime - endLastTime) >= batchTime)) {
					for (int i = 0; i < addSize; i++) {
						String str = queue.poll();
						String data[] = str.substring(1, str.length() - 1).split("\\" + Constant.DOLLAR);
						String operType = data[7]; // 操作类型 0删除 1添加 2更新
						if (operType.equals(Constant.N0)) {
							addFileList.add(data);
						} else if (operType.equals(Constant.N1)) {
							updateFileList.add(data);
						} else if (operType.equals(Constant.N2)) {
							deleteFileList.add(data);
						}
					}
					if (addFileList.size() > 0) {
						saveBatchFile();
					}
					if (updateFileList.size() > 0) {
						updateBatchFile();
					}
					if (deleteFileList.size() > 0) {
						deleteBatchFile();
					}
				}

			} catch (Exception e) {
				logger.error("附件数据处理队列数据异常:" + e.getMessage());
			}
		}
	}

	/**
	 * 批量保存附件
	 * 
	 * @param data
	 */
	private void saveBatchFile() {
		long start = System.currentTimeMillis();
		List<Object> obj = new ArrayList<Object>();
		for (String[] data : addFileList) {
			String className = ConfigLoader.protocolMap.get(data[3]); // 实体name
			String fileName = data[0] + Constant.MINUS + className + Constant.MINUS + data[5]; // mongo文件名
			Object o = protocolAnalyService.getFileTableFromControl(data[8], fileName, className); // 解析数据对象
			obj.add(o);

			// 文件存mongodb
			MessageFile file = new MessageFile();
			file.setContentType(data[6]);
			file.setFileName(fileName + Constant.PERIOD + file.getContentType()); // 文件名
			file.setContent(Tools.hexStrToBytes(data[9])); // 文件内容
			mongoService.save(file);
		}
		mySqlService.addBatch(obj);
		logger.info("附件表批量增加:{}, 耗时:{}ms", addFileList.size(), (System.currentTimeMillis() - start));
		addFileList.clear();
	}

	/**
	 * 批量更新附件
	 * 
	 * @param data
	 */
	private void updateBatchFile() {
		long start = System.currentTimeMillis();
		List<Object> obj = new ArrayList<Object>();
		for (String[] data : updateFileList) {
			String className = ConfigLoader.protocolMap.get(data[3]); // 实体name
			String fileName = data[0] + Constant.MINUS + className + Constant.MINUS + data[5]; // mongo文件名
			Object o = protocolAnalyService.getFileTableFromControl(data[8], fileName, className); // 解析数据对象
			obj.add(o);

			// 文件存mongodb
			MessageFile file = new MessageFile();
			file.setContentType(data[6]);
			file.setFileName(fileName + Constant.PERIOD + file.getContentType()); // 文件名
			file.setContent(Tools.hexStrToBytes(data[9])); // 文件内容
			mongoService.delete(file.getFileName());
			mongoService.save(file);
		}
		mySqlService.deleteBatch(obj);
		mySqlService.addBatch(obj);
		logger.info("附件表批量提交:{}, 耗时:{}ms", updateFileList.size(), (System.currentTimeMillis() - start));
		updateFileList.clear();
	}

	/**
	 * 批量删除附件
	 * 
	 * @param data
	 */
	private void deleteBatchFile() {
		long start = System.currentTimeMillis();
		List<Object> obj = new ArrayList<Object>();
		for (String[] data : deleteFileList) {
			String className = ConfigLoader.protocolMap.get(data[3]); // 实体name
			String fileName = data[0] + Constant.MINUS + className + Constant.MINUS + data[5]; // mongo文件名
			Object o = protocolAnalyService.getFileTableFromControl(data[8], fileName, className); // 解析数据对象
			obj.add(o);

			// mongodb删除文件
			mongoService.delete(fileName + Constant.PERIOD + data[6]);
		}
		mySqlService.updateBatch(obj);
		logger.info("附件表批量提交:{}, 耗时:{}ms", deleteFileList.size(), (System.currentTimeMillis() - start));
		deleteFileList.clear();
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param o
	 */
	public void put(String o) {
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
		FileManageThread.lastTime = lastTime;
	}
}
