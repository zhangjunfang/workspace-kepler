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
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.model.TrackCache;
import com.ctfo.filesaveservice.util.ConfigLoader;

/*****************************************
 * <li>描 述：轨迹文件处理线程
 * 
 *****************************************/
public class TrackStorage extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackStorage.class);
	/**	数据队列	*/
	private ArrayBlockingQueue<Location> dataQueue = null;
	/**	缓存映射表	*/
	private Map<String, TrackCache> serviceFileMap = new ConcurrentHashMap<String, TrackCache>();
	/** 线程编号 */
	private String threadName;
	/** 计数器 */
	private int index;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	/** 提交间隔 (单位：毫秒) */
	private int commitInterval;
	/** 本地补传文件线程	 */
	private LocalBlind localBlind;
	/**
	 * @param threadId 线程编号
	 * @param queueSize 
	 */
	public TrackStorage(int threadId, int queueSize) { 
		dataQueue = new ArrayBlockingQueue<Location>(queueSize);
		this.threadName = "TrackStorage-" + threadId; 
		String trackInterval = ConfigLoader.fileParamMap.get("trackInterval");
		this.commitInterval = Integer.parseInt(trackInterval);
		setName(this.threadName);
		localBlind = new LocalBlind(threadId, this); 
		localBlind.start();
	}

	@Override
	public void run() {
		logger.info("轨迹文件存储线程-[" + this.threadName+ "]启动");
		while (true) {
			try {
				Location location = dataQueue.poll();// 获取并移除此队列的头，如果此队列为空，则返回
				if (location != null) {
					index++;
					cacheLocation(location);
				} else {
					Thread.sleep(1);
				}
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > commitInterval) {
					int dataSize = saveLocation();  // 定时将缓存数据写入磁盘
					long e = System.currentTimeMillis();
					int size = getQueueSize();
					logger.info("{}, 存储文件[{}]个, 数据:[{}]条, 耗时:[{}]ms, 排队:[{}]条", threadName, dataSize, index, (e - currentTime), size);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error("TrackStorage-异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 缓存位置信息
	 * 
	 * @param location
	 */
	public void cacheLocation(Location location) {
		String vid = location.getVid();
//		logger.info("--------缓存数据vid={}, mapSize={}", vid, serviceFileMap.size()); 
		TrackCache trackCache = serviceFileMap.get(vid);
//		如果缓存为空，就判断文件是否存在，如果不存在就创建文件，然后将数据加入缓存； 
		if (trackCache == null) {
			trackCache = new TrackCache();
			createAndCacheFile(location, trackCache);
			serviceFileMap.put(location.getVid(), trackCache); 	
//		如果缓存不为空，就判断时间是否为当天，天相同就直接加入缓存； 天不相同就判断文件是否存在，文件不存在就创建文件，然后将缓存数据写入磁盘，再将新数据加入缓存
		} else {
//			logger.debug("locationPath:{}, lastPath:{}", location.getPath(), trackCache.getPath());
			if(trackCache.getPath().equals(location.getPath())){
				trackCache.setContent(trackCache.getContent() + location.getContent());
			} else {
				writeToDisk(trackCache); //vid相同，存储路径不同，就先将缓存中数据写入磁盘，再创建新存储文件并加入缓存
				trackCache.resetAll(); 
				createAndCacheFile(location, trackCache);//将新数据加入缓存 
			}
		}
	}
	/**
	 * 将缓存中的数据存储到磁盘
	 */
	public int saveLocation() { 
		int index = 0;
		for(TrackCache file : serviceFileMap.values()){
			if(file.getContent().length() > 0){
				index++;
				RandomAccessFile rf = null;
				try {
					rf = new RandomAccessFile(file.getPath(), "rw"); 
					rf.seek(rf.length());// 将指针移动到文件末尾
					rf.writeBytes(file.getContent());
					logger.debug("{}-----文件写入磁盘完成, vid:[{}]" , threadName, file.getPath()); 
				} catch (Exception e) {
					logger.error("Save File Error[" + file.getPath() + "] :" + e.getMessage(), e);
				} finally {
					if (rf != null) {
						try {
							rf.close();
						} catch (IOException ex) {
							logger.error("Save File Close Error[" + file.getPath() + "] :" + ex.getMessage(), ex);
						}
					}
					file.resetContent();// 清空缓存内容 
				}
			}
		}
		return index;
	}


	/**
	 * 将缓存数据写入磁盘
	 * @param trackCache
	 */
	public void writeToDisk(TrackCache trackCache) {
		RandomAccessFile rf = null;
		try {
			rf = new RandomAccessFile(trackCache.getPath(), "rw");
			rf.seek(rf.length());// 将指针移动到文件末尾
			rf.writeBytes(trackCache.getContent());
			logger.debug("{}-----文件写入磁盘完成:[{}]" , threadName, trackCache.getPath()); 
		} catch (Exception ex) {
			logger.error("将缓存数据写入磁盘异常:" + trackCache.getPath() + " Error："+ ex.getMessage(), ex);
		} finally {
			try {
				rf.close();
			} catch (IOException e) {
				logger.error("将缓存数据写入磁盘, 关闭资源异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 创建并缓存文件内容
	 * @param location
	 * @param trackCache 
	 */
	public void createAndCacheFile(Location location, TrackCache trackCache) {
		File file = new File(location.getPath());
		if(!file.exists()){
			try {
				File dir = new File(file.getParent());
				if(!dir.isDirectory()){ // 如果目录不存在就创建目录
					if(dir.mkdirs()){
						if(file.createNewFile()){
							trackCache.setContent(location.getContent());
							trackCache.setPath(location.getPath()); 
						} else {
							logger.error("Create File Fail:["+location.getPath()+"], Content:["+location.getContent()+"]");
						}
					} else{
						logger.error("Create File Directory Fail:["+location.getPath()+"], Content:["+location.getContent()+"]");
					}
				} else {
					if(file.createNewFile()){
						trackCache.setContent(location.getContent());
						trackCache.setPath(location.getPath()); 
					} else {
						logger.error("Create File Fail:["+location.getPath()+"], Content:["+location.getContent()+"]");
					}
				}
			} catch (IOException e) {
				logger.error("Create File Error:["+location.getPath()+"], Content:["+location.getContent()+"]异常:" + e.getMessage(), e);
			}
		} else {
//		如果文件存在就加入缓存
			trackCache.setContent(location.getContent());
			trackCache.setPath(location.getPath()); 
		}
	}
	
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false
	 * @param location
	 */
	public boolean offerDataMap(Location location) {
		return dataQueue.offer(location);
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false
	 * @param location
	 */
	public void putDataMap(Location location) {
		try {
			dataQueue.put(location);
		} catch (InterruptedException e) {
			logger.error("数据写入轨迹存储队列异常:{} - {}", location.getVid(), location.getContent());
		}
	}
	/**
	 * 获取队列大小
	 * @return
	 */
	public int getQueueSize() {
		return dataQueue.size();
	}
	/**
	 * 设置本地模式开始，通知本地补传服务停止补传
	 * @param b
	 */
	public void setLocalMode(boolean b) {
		localBlind.setLocalMode(b);
	}

}
