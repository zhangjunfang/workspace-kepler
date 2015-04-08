/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service EloadFileHandleThread.java	</li><br>
 * <li>时        间：2013-9-9  下午3:59:59	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Calendar;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.util.Base64_URl;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.FileUtil;


/*****************************************
 * <li>描        述：发动机负荷率处理线程		
 * 
 *****************************************/
public class EloadFileHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(EloadFileHandleThread.class);

	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号	*/
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	
	/** 驾驶行为事件文件目录	  */
	private String eloadFilePath;
	
	
	public EloadFileHandleThread(int threadId){
		super("EloadFileHandleThread" + threadId);
		this.threadId = threadId;
		this.eloadFilePath = ConfigLoader.fileParamMap.get("eloadPath");
	}
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return dataQueue.size();
	}
	
	@Override
	public void run() {
		logger.info("发动机信息文件线程-" + threadId + "启动");
		while (true) {
			try {// 依次向各个分析线程分发报文
				Map<String,String> dataMap = dataQueue.take();
				saveEloaddist(dataMap.get(Constant.N514), dataMap.get(Constant.VID));
				long currentTime = System.currentTimeMillis(); //按时间批量提交
				if((currentTime - lastTime) > 10000){
					lastTime = currentTime;
					logger.info("eloadfile-:" + threadId + ",size:" + getQueueSize() + ",10秒处理数据:"+index+"条");
					index = 0;
				}
				index ++;
			} catch (Exception e) {
				logger.error("文件存储主线程队列出错" + e.getMessage(),e);
			}
		}
	}
	/****
	 * 存储发动机负荷率
	 * 
	 * @param config
	 * @param nodeName
	 * @param value
	 * @param vid
	 */
	public void saveEloaddist(String value, String vid) {
		Calendar cal = Calendar.getInstance();
		int month = cal.get(Calendar.MONTH) + 1;
		int day = cal.get(Calendar.DAY_OF_MONTH);
		StringBuffer fileName = new StringBuffer(eloadFilePath);
		fileName.append("/");
		fileName.append(cal.get(Calendar.YEAR));
		fileName.append("/");
		if (month < 10) {
			fileName.append("0");
			fileName.append(month);
		} else {
			fileName.append(month);
		}
		fileName.append("/");
		if (day < 10) {
			fileName.append("0");
			fileName.append(day);
		} else {
			fileName.append(day);
		}
		fileName.append("/");
		fileName.append(vid);
		fileName.append(".txt");

		RandomAccessFile rf = null;
		StringBuffer eload = new StringBuffer();
		try {
			byte[] b;
			b = Base64_URl.base64DecodeToArray(value);
			for (int i = 0; i < b.length; i++) {
				eload.append(b[i] & 0xff);
				if (b.length - 1 > i) {
					eload.append(" ");
				}
			}
			rf = new RandomAccessFile(fileName.toString(), "rw");
			rf.seek(rf.length());
			rf.writeBytes(eload.toString() + "\r\n");

		} catch (FileNotFoundException e) {
			logger.error("车辆编号 ： " + vid + "找不到文件操作" + fileName + " 失败！" + e.getMessage(), e);
			FileUtil.coverFolder(eloadFilePath);
			try {
				rf = new RandomAccessFile(fileName.toString(), "rw");
				rf.seek(rf.length());
				rf.writeBytes(eload.toString() + "\r\n");
			} catch (Exception e1) {
				logger.error("重新创建目录后写入文件异常:" + e1.getMessage(), e1);
			}
		} catch (IOException e) {
			logger.error("车辆编号 ： " + vid + "IO操作" + fileName + " 失败！" + e.getMessage(), e);
		} catch (Exception e) {
			logger.error("车辆编号 ： " + vid + "操作文件" + fileName + " 失败！" + e.getMessage(), e);
		} finally {
			try {
				if (rf != null) {
					rf.close();
				}
			} catch (IOException e) {
				rf = null;
			}
		}
	}
	
}
