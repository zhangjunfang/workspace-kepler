/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.io.File;
import java.io.FilenameFilter;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.io.FileUtils;
import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;

/*****************************************
 * <li>描 述：本地补传任务  
 * 
 *****************************************/
public class LocalBlind extends Thread {
	private static Logger logger = LoggerFactory.getLogger(LocalBlind.class);
	/**	轨迹存储线程	*/
	private TrackStorage trackStorage;
	/**	日期格式化	*/
	private SimpleDateFormat date = new SimpleDateFormat("\\yyyy\\MM\\dd\\HH");
	/**	线程编号	*/
	private int threadId;
	/**	本地存储路径	*/
	private String localModePath;
	/**	最近补传当天	*/
	private static String lastDay = "";
	/**	补传时间点	*/
	private static String blindHours = "23";
	/**	补传标记	*/
	private static boolean blindFlag = false;
	/**	本地模式运行状态	*/
	private static boolean localMode = true;
	/**	最近处理时间	*/
	private long lastTime = System.currentTimeMillis();
	
	/**	处理几天内的数据（0:只处理当天; 1:处理前一天; ...）	*/
	private int processDays = 1;
	/*****************************************
	 * <li>描 述：初始化</li><br>
	 * <li>时 间：2013-12-16 上午11:35:36</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public LocalBlind(int threadId, TrackStorage trackStorage) {
		setName("LocalBlindTask-" + threadId);
		this.threadId = threadId;
		this.localModePath = ConfigLoader.fileParamMap.get("localModePath");
		this.trackStorage = trackStorage;
		this.processDays = Integer.parseInt(ConfigLoader.fileParamMap.get("processDays"));
		LocalBlind.blindHours = ConfigLoader.fileParamMap.get("localblindHours");
	}

	/*****************************************
	 * <li>描 述：本地补传</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void run() {
		while (true) {
			try {
				// 判断当前是否是补传时间
				String currentDate = date.format(new Date());
				String currentStr = currentDate.substring(0, 12);
				if (!currentStr.equals(lastDay)) { // 隔天
					String hours = currentDate.substring(12);
					if (hours.equals(blindHours)) {
						blindFlag = true;
					}
				}
				logger.info("Thread-{}, 本地补传状态: 当前时间:[{}], 最近补传时间:[{}], 补传标记:[{}], 补传模式:[{}]", threadId, currentStr, lastDay, blindFlag, localMode);
				if(blindFlag && localMode){
					for(int i = 0; i <= processDays; i++){
						String directory = getDirectory(i, localModePath, currentStr);
						File file = new File(directory);
//					判断当天是否有要处理的文件
						String[] fileList = getProcessFileList(file);
						if(fileList != null){
							// 数据补传
							localDataBlind(fileList, directory, currentStr);
						}
					}
				} 
//				处理完成就休眠1分钟
				Thread.sleep(60000); 
			} catch (Exception e) {
				 logger.error("LocalBlind--本地补传异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取目录
	 * @param i	提前天数
	 * @param localDir 本地目录
	 * @param dateDir  时间目录
	 * @return
	 */
	public String getDirectory(int i, String localDir, String dateDir) {
		if(i == 0){
			return localDir + dateDir;
		} else {
			Calendar cal = Calendar.getInstance();
			cal.add(Calendar.DATE, -i);
			return localDir + date.format(new Date(cal.getTimeInMillis()));
		}
	}
	/**
	 * 获取目录下当前线程的所有本地存储文件列表
	 * @param file
	 * @return
	 */
	public String[] getProcessFileList(File file) {
		if(!file.isDirectory()){
			if(file.mkdirs()){
				logger.info("创建本地存储目录成功:{}" , file.getPath()); 
			} else {
				logger.info("创建本地存储目录失败:{}" , file.getPath());
			}
			return null;
		} else {
			String[] fileList = file.list(new FilenameFilter() {
				public boolean accept(File dir, String name) {
					return name.startsWith("thread-" + threadId); 
				}
			});
			if(fileList.length > 0){
				return fileList;
			} else {
				return null;
			}
		}
	}
	/**
	 * 本地数据补传到存储线程
	 * @param fileList 		 文件列表
	 * @param currentDateStr 当前日期字符串
	 * @param string 
	 */
	public void localDataBlind(String[] fileList, String directory, String currentDateStr) { 
		try {
			// 读取补传目录当前今天或者昨天的补传数据
			// 将获取的本地补传文件循环发送到文件存储服务中
			for (String fileName : fileList) {
				String path = directory + fileName;
				File blindFile = new File(path);
				List<String> list = FileUtils.readLines(blindFile);
//				List<Location> locationList = new ArrayList<Location>();
//				List<Location> removeList = new ArrayList<Location>();
				ArrayBlockingQueue<Location> queue = new ArrayBlockingQueue<Location>(list.size());
				// 从文件中获取本地轨迹对象列表
				for (String str : list) {
					if(StringUtils.isNotBlank(str)){
						Location location = JSON.parseObject(str, Location.class);
						if(location != null && location.getContent() != null){
//							locationList.add(location);
//							removeList.add(location);
							queue.add(location);
						}
					}
				}
				// 读取完对象后删除原文件
				blindFile.delete();
				// 将对象列表传入轨迹存储队列
				for (;;) {
					Location loc = queue.poll();
					if(loc != null){
						// 将数据发送到轨迹存储线程，如果队列满，就将未写入的数据存储为文件，等10分钟后继续发送
						if(!localMode){
//							rewrite(blindFile, removeList); 
							rewrite(blindFile, queue); 
							return;
						}
//						必须保证本地模式是开启状态，否则停止文件读写操作
						if (!trackStorage.offerDataMap(loc)) {
//							rewrite(blindFile, removeList); 
							rewrite(blindFile, queue); 
							return;
						} 
						// 保证写入速度不超过1000条/秒
						Thread.sleep(1);
						long cur = System.currentTimeMillis();
						if((cur - lastTime) > 10000){
							logger.info("Thread-{}, 本地补传正在运行，最近补传时间:[{}], 补传标记:[{}], 补传模式:[{}]", threadId, lastDay, blindFlag, localMode);
							lastTime = System.currentTimeMillis();
						}
					} else {
						break;
					}
				}
			}
//			补传结束就将重置标记，等待第二天开启
			blindFlag = false;
			lastDay = currentDateStr;
		} catch (Exception e) {
			logger.error("补传数据异常:" + e.getMessage(), e);
		} 
	}
//	/**
//	 *	重写补传文件
//	 * @param blindFile
//	 * @param locationList
//	 */
//	private void rewrite(File blindFile, List<Location> locationList) {
//		try {
//			// 如果插入失败,就将文件重新写入，等待10分钟后再次写入
//			blindFile.createNewFile();
//			StringBuilder sb = new StringBuilder(102400);
//			for (Location l : locationList) {
//				sb.append(JSON.toJSONString(l)).append("\r\n");
//			}
//			logger.info("[{}]条未写入，回写到[{}]", locationList.size() + 1, blindFile.getPath());
//			FileUtils.writeStringToFile(blindFile, sb.toString(), "UTF-8", true);
//		} catch (IOException e) {
//			logger.error("轨迹重写失败:["+ blindFile.getPath() + "], " + e.getMessage(), e);
//		}
//	}
	/**
	 *	重写补传文件
	 * @param blindFile
	 * @param queue
	 */
	private void rewrite(File blindFile, ArrayBlockingQueue<Location> queue) {
		try {
			// 如果插入失败,就将文件重新写入，等待10分钟后再次写入 
			blindFile.createNewFile();
			StringBuilder sb = new StringBuilder(102400);
			int index = 0 ;
			logger.info("处理前队列大小---{}", queue.size()); 
			for (;;) {
				index++;
				Location l = queue.poll();
				if(l != null){
					sb.append(JSON.toJSONString(l)).append("\r\n");
				} else {
					break;
				}
			}
			logger.info("处理后队列大小---{}", queue.size());
//			for (Location l : queue) {
//				index++;
//				sb.append(JSON.toJSONString(l)).append("\r\n");
//			}
			FileUtils.writeStringToFile(blindFile, sb.toString(), "UTF-8", true);
			logger.info("[{}]条未写入，回写到[{}]", index, blindFile.getPath());
		} catch (IOException e) {
			logger.error("轨迹重写失败:["+ blindFile.getPath() + "], " + e.getMessage(), e);
		}
	}

	/**
	 * 设置本地模式开始，通知本地补传服务停止补传
	 * @param b
	 */
	public void setLocalMode(boolean b) {
		LocalBlind.localMode = b;
		try {
			Thread.sleep(60000);
		} catch (Exception e) {
			logger.error("暂停异常" + e.getMessage(), e);
		}
	}
}
