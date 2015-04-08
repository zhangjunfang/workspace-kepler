/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service TrackFileHandle.java	</li><br>
 * <li>时        间：2013-9-9  下午2:30:25	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.io.File;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;

/*****************************************
 * <li>描 述：轨迹本地模式存储线程
 * 
 *****************************************/
public class TrackLocalModeStorage extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackLocalModeStorage.class);
	private static final int QUEUE_SIZE = 50000;
	/**	序列时间格式化	*/
	private SimpleDateFormat format = new SimpleDateFormat("yyyyMMddHHmmssSSS");
	/**	当前时间目录格式化	*/
	private SimpleDateFormat currentDir = new SimpleDateFormat("//yyyy//MM/dd//");
	/**	数据队列	*/
	private ArrayBlockingQueue<Location> dataQueue = new ArrayBlockingQueue<Location>(QUEUE_SIZE);
	/**	缓存列表	*/
	private List<Location> list = new ArrayList<Location>();
	/** 线程名称	 */
	private String threadName;
	/** 线程编号	 */
	private int threadId;
	/** 计数器 */
	private int index;
	/** 最近处理时间 */
	private long lastTime = System.currentTimeMillis();
	/** 提交间隔 (单位:毫秒;  默认10分钟) */
	private int commitInterval = 60000;
	/** 提交量 (单位：条)*/
	private int batchSize = 10000;
	/** 本地模式存储路径 	*/
	private String localModePath;
	/**
	 * 
	 * @param threadId 线程编号
	 */
	public TrackLocalModeStorage(int threadId) {
		this.threadName = "TrackLocalModeStorage-" + threadId; 
		this.threadId = threadId;
		setName(this.threadName);
		this.commitInterval = Integer.parseInt(ConfigLoader.fileParamMap.get("localInterval"));
		this.batchSize = Integer.parseInt(ConfigLoader.fileParamMap.get("localBatchSize"));
		this.localModePath = ConfigLoader.fileParamMap.get("localModePath");
	}
	/**
	 * 线程启动方法
	 * 
	 */
	public void run() {
		logger.info("本地轨迹文件存储线程-[" + this.threadName+ "]启动");
		while (true) {
			try {
				Location location = dataQueue.poll();// 获取并移除此队列的头，如果此队列为空，则返回
				if (location != null) {
					index++;
					list.add(location);
				} else {
					Thread.sleep(1);
				}
				long currentTime = System.currentTimeMillis();
				if ( index == batchSize ||  (currentTime - lastTime) > commitInterval) {
					long s = System.currentTimeMillis();
					if(index > 0){
						saveLocationList(list);
						list.clear();
					}
					long e = System.currentTimeMillis();
					int size = getQueueSize();
					logger.info("{}, 存储数据:[{}]条, 耗时:[{}]ms, 排队:[{}]条", threadName, index, (e -s), size);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error("TrackLocalModeStorage-异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 本地存储路径必须正常，批量提交数量不能大于队列长度，批量提交时间间隔不能超过1小时
	 * @throws Exception
	 */
	public void init() throws Exception{ 
		File file = new File(localModePath); 
		logger.info("LocalFile path Error:{} size Error:{} time Error:{}", !file.isDirectory(), batchSize > QUEUE_SIZE, commitInterval > 3600000);
		if(!file.isDirectory() || batchSize > QUEUE_SIZE || commitInterval > 3600000){
			logger.error("文件存储线程[{}] 启动异常:本地存储目录不存在，批量提交数量不能大于队列长度，批量提交时间间隔不能超过1小时 ; localModePath={}, batchSize={}, submitTime={}" ,threadName, localModePath, batchSize, commitInterval);
			throw new Exception("LocalFile--文件存储线程[" + this.threadName+ "] 启动异常:本地存储目录不存在，批量提交数量不能大于队列长度，批量提交时间间隔不能超过1小时");
		} else {
			this.start(); 
		}
	} 
	
	/**
	 * 获得存储数据队列序列号
	 * @return
	 */
	private String getSaveQueueSeq() {
		try {
			return format.format(new Date());
		} catch (Exception e) {
			return String.valueOf(System.currentTimeMillis());
		}
	}
	/**
	 * 存储
	 * @param list
	 * @param fileSeq
	 */
	private void saveLocationList(List<Location> list) {
		String seq = getSaveQueueSeq(); // 文件序号
		String dateDir = getCurrentDateDirectory(); // 日期目录 （格式://yyyy//MM/dd//）
		String localPath = localModePath + dateDir + "thread-"+ threadId+ "-" + seq + ".txt";
		File file = new File(localPath);
		if (!file.exists()) {
			File dir = new File(file.getParent());
			try {
				if(!dir.isDirectory()){
					if(!dir.mkdirs()){
						logger.error("LocalFile Create Directory Error:{}", dir.getPath()); 
					}
				}
				file.createNewFile();
			} catch (IOException e) {
				logger.error("Save Local File Error:{}", localPath);
				return;
			}
		}
		StringBuilder sb = new StringBuilder(102400);
		for (Location l : list) {
			sb.append(JSON.toJSONString(l)).append("\r\n");
		}
		savefile(file.getPath(), sb.toString());
		sb.setLength(0); 
	}
	/**
	 * 生成当前日期文件目录
	 * @return
	 */
	private String getCurrentDateDirectory() {
		try {
			return currentDir.format(new Date());
		} catch (Exception e) {
			logger.error("根据数据生成目录异常:" +  e.getMessage(), e); 
			return "";
		}
	}

	private static void savefile(String path, String content) {
		RandomAccessFile rf = null;
		try {
			rf = new RandomAccessFile(path, "rw");
			rf.seek(rf.length());// 将指针移动到文件末尾
			rf.writeBytes(content);
			logger.debug("LocalFile--本地文件写入磁盘完成, path:[{}]" , path);
		} catch (Exception e) {
			System.out.println("Save File Error[" + path + "] :" + e.getMessage());
		} finally {
			if (rf != null) {
				try {
					rf.close();
				} catch (IOException ex) {
					System.out.println("Save File Close Error[" + path + "] :" + ex.getMessage());
				}
			}
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false
	 * 
	 * @param location
	 */
	public boolean offerDataMap(Location location) {
		return dataQueue.offer(location);
	}

	/**
	 * 获取队列大小
	 * 
	 * @return
	 */
	public int getQueueSize() {
		return dataQueue.size();
	}
}
