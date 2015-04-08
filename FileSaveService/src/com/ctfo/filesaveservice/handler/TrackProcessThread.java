/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service TrackFileHandle.java	</li><br>
 * <li>时        间：2013-9-9  下午2:30:25	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.model.Driver;
import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.LocalDriverCacle;

/*****************************************
 * <li>描 述：轨迹文件处理线程
 * 
 *****************************************/
public class TrackProcessThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackProcessThread.class);
	/** 数据队列 */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 目录分隔符	 */
	private static String separator = System.getProperty("file.separator");
	/** 线程编号 */
	private int threadId;
	/** 计数器 */
	private int index;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	/** 轨迹文件目录 */
	private String trackfileurl;
	/** 本地模式（如果存储队列满了，就使用本地模式）	 */
	private boolean localMode = false;
	/** 本地模式持续时间（本地模式持续时间  默认:10分钟）	 */
	private long localModeDuration = 600000;
	/** 开启本地模式	 */
	private boolean openLocalMode = false;
	/** 最近一次本地模式时间 */
	private long lastLocalTime = System.currentTimeMillis();
	/** 轨迹存储线程	 */
	private TrackStorage trackStorage;
	/** 轨迹本地模式存储线程	*/
	private TrackLocalModeStorage trackLocalModeStorage;
	
	public TrackProcessThread(int threadId) {
		super("TrackFileHandleThread-" + threadId);
		this.threadId = threadId;
		this.trackfileurl = ConfigLoader.fileParamMap.get("trackPath");
		this.localModeDuration = Integer.parseInt(ConfigLoader.fileParamMap.get("localModeDuration"));
		this.openLocalMode = Boolean.parseBoolean(ConfigLoader.fileParamMap.get("openLocalMode"));
		if(openLocalMode){
			trackStorage = new TrackStorage(threadId, 15000);
		} else {
			trackStorage = new TrackStorage(threadId, 100000);
		}
		trackStorage.start();
		
		trackLocalModeStorage = new TrackLocalModeStorage(threadId);
		trackLocalModeStorage.start();
	}

	/**
	 * 加入数据队列
	 * @param dataMap
	 */
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	/**
	 * 获取队列长度
	 * @return
	 */
	public int getQueueSize() {
		return dataQueue.size();
	}

	@Override
	public void run() {
		logger.info("文件处理线程" + threadId + "启动");
		while (true) {
			try {
				Map<String, String> map = dataQueue.take();
				index++;
				Location location = processTrackFile(map);
				if(localMode){
					long currentModeTime = System.currentTimeMillis();
//					本地模式持续时间到了就结束本地模式
					if((currentModeTime - lastLocalTime) > localModeDuration){
						localMode = false;
//						本地模式结束，设置本地补传线程可运行状态，可以读取文件补传
						logger.info("Thread-{} 关闭本地写入模式[false], 开启补传模式:-------[true]", threadId);
						trackStorage.setLocalMode(true);
					}
					if(!trackLocalModeStorage.offerDataMap(location)){
						logger.error("LocalMode Save Track Error:{}", JSON.toJSONString(location)); 
					}
				} else {
					if (openLocalMode) {
						// 如果存储队列已满，就开启本地模式
						if (!trackStorage.offerDataMap(location)) { 
							localMode = true;
							if (!trackLocalModeStorage.offerDataMap(location)) {
								logger.error("LocalMode Save Track Error:{}", JSON.toJSONString(location));
							}
							// 本地模式开始，设置本地补传线程不可运行状态，避免读写文件冲突
							trackStorage.setLocalMode(false);
							logger.info("Thread-{} 开启本地写入模式[true], 关闭补传模式:-------[false]", threadId);
							lastLocalTime = System.currentTimeMillis();
						}
					} else {
						trackStorage.putDataMap(location);
					}
				}
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > 10000) {
					int size = getQueueSize();
					logger.info("trackfile-{}, 本地写入模式:{} 10s处理数据:[{}]条, 排队:[{}]", threadId, localMode, index, size);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				logger.error("文件存储主线程队列出错" + e.getMessage(), e);
			}
		}
	}
	/***
	 * 处理轨迹文件
	 * @param map
	 * @return
	 */
	public Location processTrackFile(Map<String, String> map){
		Location location = new Location();
		String vid = map.get(Constant.VID); 
		Driver driver = null;
		if(map.get(Constant.TYPE).equals("0")){
			driver = LocalDriverCacle.getInstance().getDriverInfo(vid);
		}
		String trackContent = getTrackString(map, driver);
		String dateDirectory = getDateDirectory(map.get("4"));
		String path = trackfileurl + separator + dateDirectory + separator + vid + ".txt";
		location.setVid(vid);
		location.setContent(trackContent);
		location.setPath(path);
		
		return location;
	}
	/**
	 * 获取时间目录
	 * @param vid
	 * @return
	 */

	public String getDateDirectory(String dateStr) {
		return separator + dateStr.substring(0, 4) + separator +dateStr.substring(4, 6)+ separator + dateStr.substring(6, 8)+ separator;
	}

	/**
	 * 拼接轨迹文件字符串
	 * @param map 
	 * @param driver 
	 * @return
	 */
	private String getTrackString(Map<String, String> map, Driver driver) { 
		StringBuffer trackBuffer = new StringBuffer(256);
		trackBuffer.append(map.get(Constant.MAPLON)); // 经度0
		trackBuffer.append(":");
		trackBuffer.append(map.get(Constant.MAPLAT)); // 纬度1
		trackBuffer.append(":");
		trackBuffer.append(map.get("4")); // GPS时间2
		trackBuffer.append(":");
		trackBuffer.append(map.get("3")); // GPS 速度3
		trackBuffer.append(":");
		if (map.containsKey("5") && !"".equals(map.get("5"))) {
			trackBuffer.append(map.get("5")); // 正北方向夹角4
		}
		trackBuffer.append(":");
		if (map.containsKey("26")) {
			trackBuffer.append(map.get("26")); // 车辆状态5
		}
		trackBuffer.append(":");
		trackBuffer.append(map.get(Constant.FILEALARMCODE)); // 报警编码6
		trackBuffer.append(":");
		trackBuffer.append(map.get("1")); // 经度7
		trackBuffer.append(":");
		trackBuffer.append(map.get("2")); // 纬度8
		trackBuffer.append(":");
		if (map.containsKey("6")) {
			trackBuffer.append(map.get("6")); // 海拔9
		}
		trackBuffer.append(":");
		if (map.containsKey("9")) {
			trackBuffer.append(map.get("9")); // 里程10
		}
		trackBuffer.append(":");
		if (map.containsKey("213")) {
			trackBuffer.append(map.get("213")); // 累计油耗11
		}
		trackBuffer.append(":");
		if (map.containsKey("505")) {
			trackBuffer.append(map.get("505")); // 发动机运行总时长12
		}
		trackBuffer.append(":");
		if (map.containsKey("210")) {
			trackBuffer.append(map.get("210")); // 引擎转速（发动机转速）13
		}
		trackBuffer.append(":");
		if (map.containsKey("8") && !"null".equals(map.get("8"))) {// 位置基本信息状态位14
			trackBuffer.append(map.get("8"));
		}
		trackBuffer.append(":");
		if (map.containsKey("32")) {
			String areaInfo = map.get("32");
			String temp = areaInfo.replaceAll("null", "");
			trackBuffer.append(temp); // 区域/线路报警附加信息15
		}
		trackBuffer.append(":");
		if (map.containsKey("509")) { // 冷却液温度16
			trackBuffer.append(map.get("509"));
		}
		trackBuffer.append(":");
		// 蓄电池电压17
		if (map.containsKey("507")) {
			trackBuffer.append(map.get("507"));
		}
		trackBuffer.append(":");
		// 瞬时油耗18
		if (map.containsKey("216")) {
			trackBuffer.append(map.get("216"));
		}
		trackBuffer.append(":");
		// 行驶记录仪速度(km/h)19
		if (map.containsKey("7")) {
			trackBuffer.append(map.get("7"));
		}
		trackBuffer.append(":");
		// 机油压力 (20 COL)
		if (map.containsKey("215")) {
			trackBuffer.append(map.get("215"));
		}
		trackBuffer.append(":");
		// 大气压力21
		if (map.containsKey("511")) {
			trackBuffer.append(map.get("511"));
		}
		trackBuffer.append(":");
		// 发动机扭矩百分比，1bit=1%，0=-125% 22
		if (map.containsKey("503")) {
			trackBuffer.append(map.get("503"));
		}
		trackBuffer.append(":");
		// 车辆信号状态 23
		if (map.containsKey("500")) {
			trackBuffer.append(map.get("500"));
		}
		trackBuffer.append(":");
		trackBuffer.append(map.get(Constant.SPEEDFROM)); // 车速来源 24
		trackBuffer.append(":");
		if (map.containsKey("24")) {
			trackBuffer.append(map.get("24")); // 油量（对应仪表盘读数） 25
		}
		trackBuffer.append(":");
		if (map.containsKey("31")) {
			trackBuffer.append(map.get("31")); // 超速报警附加信息 26
		}
		trackBuffer.append(":");
		if (map.containsKey("35")) {
			trackBuffer.append(map.get("35")); // 路线行驶时间不足/过长 27
		}
		trackBuffer.append(":");
		if (map.containsKey("504")) {
			trackBuffer.append(map.get("504")); // 油门踏板位置，(1bit=0.4%，0=0%) 28
		}
		trackBuffer.append(":");
		if (map.containsKey("506")) {
			trackBuffer.append(map.get("506")); // 终端内置电池电压 29
		}
		trackBuffer.append(":");
		if (map.containsKey("214")) {
			trackBuffer.append(map.get("214")); // 发动机水温 30
		}
		trackBuffer.append(":");
		if (map.containsKey("508")) {
			trackBuffer.append(map.get("508")); // 机油温度 31
		}
		trackBuffer.append(":");
		if (map.containsKey("510")) {
			trackBuffer.append(map.get("510")); // 进气温度 32
		}
		trackBuffer.append(":");
		if (map.containsKey("217")) {
			trackBuffer.append(map.get("217")); // 开门状态 33
		}
		trackBuffer.append(":");
		if (map.containsKey("519")) {
			trackBuffer.append(map.get("519")); // 需要人工确认报警事件的ID 34
		}
		trackBuffer.append(":");
		if (map.containsKey("219")) {
			trackBuffer.append(map.get("219")); // 计量仪油耗，1bit=0.01L,0=0L 35
		}
		trackBuffer.append(":");
		if(driver != null){
			trackBuffer.append(driver.getDriverId());  		// 驾驶员编号	36
			trackBuffer.append(":");
			trackBuffer.append(driver.getDriverSource()); 	// 驾驶员信息来源  37
		} else {
			trackBuffer.append(":");
		}
		trackBuffer.append(":");
		trackBuffer.append(System.currentTimeMillis()); // 系统时间 38
		trackBuffer.append("\r\n"); // 标记换行
		
		return trackBuffer.toString();
	}

}
