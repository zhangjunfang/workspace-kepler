/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service TrackFileHandle.java	</li><br>
 * <li>时        间：2013-9-9  下午2:30:25	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;


/*****************************************
 * <li>描 述：轨迹文件处理线程
 * 
 *****************************************/
//public class TrackFileHandleThread extends Thread {
//	private static final Logger logger = LoggerFactory.getLogger(TrackFileHandleThread.class);
//
//	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
//
//	private Map<String, ServiceFileUnit> serviceFileMap = new ConcurrentHashMap<String, ServiceFileUnit>();
//
//	private static String separator = System.getProperty("file.separator");
//	/** 线程编号 */
//	private int threadId;
//	/** 计数器 */
//	private int index;
//	/** 上次时间 */
//	private long lastTime = System.currentTimeMillis();
//	/** 轨迹文件目录 */
//	private String trackfileurl;
//	/** 报警分析文件目录 */
//	private String alarmFileUrl;
//	/** 提交频率时间 (毫秒) */
//	private int submitFrequencyTime;
//	/** 提交频率时间 (秒) */
//	private int submitTime;
//	/** 实际提交频率 */
//	// private int submitFrequency;
//	/** 文件批量提交数量 */
//	private int commitFileCount;
//	/** 本地模式（如果存储队列满了，就使用本地模式）	 */
//	private boolean localMode = false;
//	/** 本地模式持续时间（本地模式持续时间）	 */
//	private long localModeDuration;
//	/** 最近一次本地模式时间 */
//	private long lastLocalTime;
//	
//	private TrackStorage trackStorage;
//
//	private TrackLocalModeStorage trackLocalModeStorage;
//	
//	public TrackFileHandleThread(int threadId) {
//		super("TrackFileHandleThread" + threadId);
//		this.threadId = threadId;
//		this.trackfileurl = trackfileurl;
//		this.alarmFileUrl = alarmFileUrl;
//		// this.submitFrequency = submitFrequency;
////		this.submitFrequencyTime = submitFrequency;
//		this.commitFileCount = commitFileCount;
////		this.submitTime = submitFrequency / 1000;
//		
//		trackStorage = new TrackStorage(threadId);
////		trackStorage.setSubmitTime(submitFrequency); 
//		trackStorage.start();
//		
//		trackLocalModeStorage = new TrackLocalModeStorage(threadId);
//		trackLocalModeStorage.start();
//	}
//
//	public void putDataMap(Map<String, String> dataMap) {
//		try {
//			dataQueue.put(dataMap);
//		} catch (InterruptedException e) {
//			logger.error(e.getMessage());
//		}
//	}
//
//	public int getQueueSize() {
//		return dataQueue.size();
//	}
//
//	@Override
//	public void run() {
//		logger.info("文件存储主线程" + threadId + "启动");
//		while (true) {
//			try {
//				Map<String, String> map = dataQueue.take();
//				index++;
//				Location location = processTrackFile(map);
//				if(localMode){
//					long currentModeTime = System.currentTimeMillis();
////					本地模式持续时间到了就结束本地模式
//					if((currentModeTime - lastLocalTime) > localModeDuration){
//						localMode = false;
//					}
//					if(!trackLocalModeStorage.offerDataMap(location)){
//						logger.error("LocalMode Save Track Error:{}", JSON.toJSONString(location)); 
//					}
//				} else {
////					如果存储队列已满，就开启本地模式
//					if(!trackStorage.offerDataMap(location)){ // 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false
//						localMode = true;
//						if(!trackLocalModeStorage.offerDataMap(location)){
//							logger.error("LocalMode Save Track Error:{}", JSON.toJSONString(location)); 
//						}
//						lastLocalTime = System.currentTimeMillis();
//					}
//				}
//				long currentTime = System.currentTimeMillis();
//				if ((currentTime - lastTime) > 10000) {
//					int size = getQueueSize();
//					logger.info("trackfile-{}, 10s处理数据:[{}]条, 排队:[{}]", threadId, index, size);
//					index = 0;
//					lastTime = System.currentTimeMillis();
//				}
//			} catch (Exception e) {
//				logger.error("文件存储主线程队列出错" + e.getMessage(), e);
//			}
//		}
//	}
//	/**
//	 * 本地文件存储模式
//	 * @param location
//	 */
//	private void localSaveLocation(Location location) {
//		
//		
//	}
//
//	private Location processTrackFile(Map<String, String> map){
//		Location location = new Location();
//		String vid = map.get(Constant.VID);
//		String trackContent = getTrackString(map);
////		String alarmContent = getAlarmString(map);
//		String dateSeparator = getDateSeparator(map.get(Constant.UTC));
//		String path = trackfileurl + separator + dateSeparator + separator + vid + ".txt";
//		location.setVid(vid);
//		location.setContent(trackContent);
//		location.setPath(path);
//		
//		return location;
//	}
//	
//	private String getDateSeparator(String string) {
//		return null;
//	}
//
//	private String getAlarmString(Map<String, String> map) {
//		StringBuffer alarmBuffer = new StringBuffer(128);
//		return null;
//	}
//
//	private String getTrackString(Map<String, String> map) {
//		StringBuffer trackBuffer = new StringBuffer(256);
//		return null;
//	}
//
//	/**
//	 * 基本数据文件保存
//	 * 
//	 */
//	private void saveFileTrack(Map<String, String> app) {
//		String vid = app.get(Constant.VID);
//		String gpsTime = app.get("4");
//		StringBuffer buf = new StringBuffer("");
//		StringBuffer alarmBuf = new StringBuffer("");
//
//		buf.append(app.get(Constant.MAPLON)); // 经度0
//		buf.append(":");
//
//		buf.append(app.get(Constant.MAPLAT)); // 纬度1
//
//		buf.append(":");
//
//		buf.append(gpsTime); // GPS时间2
//
//		buf.append(":");
//		buf.append(app.get("3")); // GPS 速度3
//		buf.append(":");
//
//		if (app.get("5") != null && !"".equals(app.get("5"))) {
//			buf.append(app.get("5")); // 正北方向夹角4
//		}
//		buf.append(":");
//		if (app.get("26") != null) {
//			buf.append(app.get("26")); // 车辆状态5
//		}
//		buf.append(":");
//		buf.append(app.get(Constant.FILEALARMCODE)); // 报警编码6
//		buf.append(":");
//		buf.append(app.get("1")); // 经度7
//		buf.append(":");
//		buf.append(app.get("2")); // 纬度8
//		buf.append(":");
//		if (app.get("6") != null) {
//			buf.append(app.get("6")); // 海拔9
//		}
//		buf.append(":");
//		if (app.containsKey("9")) {
//			buf.append(app.get("9")); // 里程10
//		}
//		buf.append(":");
//
//		if (app.containsKey("213")) {
//			buf.append(app.get("213")); // 累计油耗11
//		}
//		buf.append(":");
//
//		if (app.containsKey("505")) {
//			buf.append(app.get("505")); // 发动机运行总时长12
//		}
//		buf.append(":");
//
//		if (app.containsKey("210")) {
//			buf.append(app.get("210")); // 引擎转速（发动机转速）13
//		}
//
//		buf.append(":");
//
//		if (app.containsKey("8") && !"null".equals(app.get("8"))) {// 位置基本信息状态位14
//			buf.append(app.get("8"));
//		}
//
//		buf.append(":");
//		if (app.containsKey("32")) {
//			String areaInfo = app.get("32");
//			if (areaInfo != null) {
//				String temp = areaInfo.replaceAll("null", "");
//				buf.append(temp); // 区域/线路报警附加信息15
//			}
//		}
//		buf.append(":");
//
//		if (app.containsKey("509")) { // 冷却液温度16
//			buf.append(app.get("509"));
//		}
//
//		buf.append(":");
//
//		// 蓄电池电压17
//		if (app.containsKey("507")) {
//			buf.append(app.get("507"));
//		}
//
//		buf.append(":");
//
//		// 瞬时油耗18
//		if (app.containsKey("216")) {
//			buf.append(app.get("216"));
//		}
//
//		buf.append(":");
//
//		// 行驶记录仪速度(km/h)19
//		if (app.containsKey("7")) {
//			buf.append(app.get("7"));
//		}
//
//		buf.append(":");
//
//		// 机油压力 (20 COL)
//		if (app.containsKey("215")) {
//			buf.append(app.get("215"));
//		}
//
//		buf.append(":");
//
//		// 大气压力21
//		if (app.containsKey("511")) {
//			buf.append(app.get("511"));
//		}
//
//		buf.append(":");
//		// 发动机扭矩百分比，1bit=1%，0=-125% 22
//		if (app.containsKey("503")) {
//			buf.append(app.get("503"));
//		}
//
//		buf.append(":");
//
//		// 车辆信号状态 23
//		if (app.containsKey("500")) {
//			buf.append(app.get("500"));
//		}
//
//		buf.append(":");
//
//		buf.append(app.get(Constant.SPEEDFROM)); // 车速来源 24
//
//		buf.append(":");
//
//		if (app.get("24") != null) {
//			buf.append(app.get("24")); // 油量（对应仪表盘读数） 25
//		}
//
//		buf.append(":");
//
//		if (app.get("31") != null) {
//			buf.append(app.get("31")); // 超速报警附加信息 26
//		}
//
//		buf.append(":");
//
//		if (app.get("35") != null) {
//			buf.append(app.get("35")); // 路线行驶时间不足/过长 27
//		}
//
//		buf.append(":");
//
//		if (app.get("504") != null) {
//			buf.append(app.get("504")); // 油门踏板位置，(1bit=0.4%，0=0%) 28
//		}
//
//		buf.append(":");
//
//		if (app.get("506") != null) {
//			buf.append(app.get("506")); // 终端内置电池电压 29
//		}
//
//		buf.append(":");
//
//		if (app.get("214") != null) {
//			buf.append(app.get("214")); // 发动机水温 30
//		}
//
//		buf.append(":");
//
//		if (app.get("508") != null) {
//			buf.append(app.get("508")); // 机油温度 31
//		}
//
//		buf.append(":");
//
//		if (app.get("510") != null) {
//			buf.append(app.get("510")); // 进气温度 32
//		}
//
//		buf.append(":");
//
//		if (app.get("217") != null) {
//			buf.append(app.get("217")); // 开门状态 33
//		}
//
//		buf.append(":");
//
//		if (app.get("519") != null) {
//			buf.append(app.get("519")); // 需要人工确认报警事件的ID 34
//		}
//
//		buf.append(":");
//
//		if (app.get("219") != null) {
//			buf.append(app.get("219")); // 计量仪油耗，1bit=0.01L,0=0L 35
//		}
//
//		buf.append(":");
//
//		buf.append(System.currentTimeMillis()); // 系统时间 36
//
//		buf.append("\r\n"); // 标记换行
//
//		// 报警存储
//		alarmBuf.append(app.get(Constant.FILEALARMCODE)); // 报警编码0
//		alarmBuf.append(":");
//		alarmBuf.append(app.get(Constant.MAPLON)); // MAP经度1
//		alarmBuf.append(":");
//		alarmBuf.append(app.get(Constant.MAPLAT)); // MAP纬度2
//		alarmBuf.append(":");
//		alarmBuf.append(app.get("1"));// 经度3
//		alarmBuf.append(":");
//		alarmBuf.append(app.get("2")); // 纬度4
//		alarmBuf.append(":");
//		alarmBuf.append(gpsTime); // GPS时间5
//		alarmBuf.append(":");
//		alarmBuf.append(app.get("SPEED")); // 根据车速来源获取车速6
//		alarmBuf.append(":");
//		alarmBuf.append(app.get("5")); // 正北方向夹角7
//		alarmBuf.append(":");
//		if (app.containsKey("213")) {
//			alarmBuf.append(app.get("213")); // 累计油耗8
//		}
//		alarmBuf.append(":");
//		if (app.containsKey("9")) {
//			alarmBuf.append(app.get("9")); // 里程9
//		}
//		alarmBuf.append(":");
//		if (app.containsKey("32")) {
//			alarmBuf.append(app.get("32")); // 报区域/线路报警10
//		}
//		alarmBuf.append(":");
//		if (app.get("6") != null) {
//			alarmBuf.append(app.get("6")); // 海拔11
//		}
//		alarmBuf.append(":");
//		alarmBuf.append(app.get(Constant.SPEEDFROM)); // 车速来源 12
//
//		alarmBuf.append(":");
//
//		alarmBuf.append(System.currentTimeMillis()); // 系统时间13
//		alarmBuf.append("\r\n"); // 标记换行
//
//		ServiceFileUnit serviceFileUnit = serviceFileMap.get(vid);
//		if (serviceFileUnit == null) {
//			ServiceFileUnit service = new ServiceFileUnit();
//			service.setDay(gpsTime.substring(6, 8));
//			service.setGpsTime(gpsTime);
//			service.setVid(vid);
//			service.setFilecontent(buf.toString().replaceAll("null", ""));
//			service.setAlarmfilecontent(alarmBuf.toString().replaceAll("null", ""));
//			service.addRecordCount(1);
//			serviceFileMap.put(vid, service);
//		} else {
//
//			String day = serviceFileUnit.getDay();
//			if (!gpsTime.substring(6, 8).equals(day)) {// 跨天提交
//				String year = serviceFileUnit.getGpsTime().substring(0, 4);
//				String month = serviceFileUnit.getGpsTime().substring(4, 6);
//
//				String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//				try {
//					RandomAccessFile rf = new RandomAccessFile(trackFile, "rw");
//					rf.seek(rf.length());// 将指针移动到文件末尾
//					rf.writeBytes(serviceFileUnit.getFilecontent());
//					rf.close();
//					logger.debug(vid + "跨天写入轨迹文件成功");
//
//					String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//
//					RandomAccessFile rfAlarm = new RandomAccessFile(alarmPath, "rw");
//					rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
//					rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
//					rfAlarm.close();
//					logger.debug(vid + "跨天写入报警文件成功");
//				} catch (Exception ex) {
//					logger.error("跨天存储出错", ex);
//				}
//				serviceFileUnit.resetUnit();
//
//				serviceFileUnit.setDay(gpsTime.substring(6, 8));// 存入当天日
//				serviceFileUnit.setGpsTime(gpsTime);
//			}
//
//			// 存储当天
//			serviceFileUnit.setFilecontent(serviceFileUnit.getFilecontent() + buf.toString().replaceAll("null", ""));
//			serviceFileUnit.setAlarmfilecontent(serviceFileUnit.getAlarmfilecontent() + alarmBuf.toString().replaceAll("null", ""));
//			serviceFileUnit.addRecordCount(1);
//			if (serviceFileUnit.getRecordCount() > commitFileCount) {
//				String year = serviceFileUnit.getGpsTime().substring(0, 4);
//				String month = serviceFileUnit.getGpsTime().substring(4, 6);
//				day = serviceFileUnit.getGpsTime().substring(6, 8);
//				String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//
//				RandomAccessFile rf = null;
//				try {
//					rf = new RandomAccessFile(trackFile, "rw");
//
//					rf.seek(rf.length());// 将指针移动到文件末尾
//					rf.writeBytes(serviceFileUnit.getFilecontent());
//
//					// logger.debug(vid + "写入轨迹文件成功");
//				} catch (FileNotFoundException e) {
//					logger.warn("在" + serviceFileUnit.getGpsTime() + "读 轨迹文件" + vid + ".txt 找不到.", e);
//					FileUtil.coverFolder(trackfileurl);
//				} catch (IOException e) {
//					logger.error("在" + serviceFileUnit.getGpsTime() + "写入轨迹文件" + vid + ".txt", e);
//				} finally {
//					if (rf != null) {
//						try {
//							rf.close();
//						} catch (IOException e) {
//							logger.error("在" + serviceFileUnit.getGpsTime() + "关闭轨迹文件" + vid + ".txt 找不到.", e);
//						}
//					}
//				}
//
//				String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//				RandomAccessFile rfAlarm = null;
//				try {
//					rfAlarm = new RandomAccessFile(alarmPath, "rw");
//					rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
//					rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
//					// logger.debug(vid + "写入报警文件成功");
//				} catch (FileNotFoundException e) {
//					logger.warn("在" + serviceFileUnit.getGpsTime() + "读 报警文件" + vid + ".txt 找不到.", e);
//					FileUtil.coverFolder(alarmFileUrl);
//				} catch (IOException e) {
//					logger.error("在" + serviceFileUnit.getGpsTime() + "写入报警文件" + vid + ".txt", e);
//				} finally {
//					if (rfAlarm != null) {
//						try {
//							rfAlarm.close();
//						} catch (IOException e) {
//							logger.error("在" + serviceFileUnit.getGpsTime() + "关闭报警文件" + vid + ".txt 找不到.", e);
//						}
//					}
//				}
//				serviceFileUnit.resetRecordCount();
//			}
//		}
//	}
//
//	/***
//	 * 将数据写入文件
//	 */
//	private void saveData() {
//		Set<String> st = serviceFileMap.keySet();
//		Iterator<String> it = st.iterator();
//		while (it.hasNext()) {
//			String vid = it.next();
//			ServiceFileUnit serviceFileUnit = serviceFileMap.get(vid);
//			if (serviceFileUnit.getRecordCount() == 0) {
//				continue;
//			}
//
//			// logger.error("VID:" + vid + "文件:" +
//			// serviceFileUnit.getGpsTime());
//			String year = serviceFileUnit.getGpsTime().substring(0, 4);
//			String month = serviceFileUnit.getGpsTime().substring(4, 6);
//			String day = serviceFileUnit.getGpsTime().substring(6, 8);
//			String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//
//			RandomAccessFile rf = null;
//			try {
//				rf = new RandomAccessFile(trackFile, "rw");
//
//				rf.seek(rf.length());// 将指针移动到文件末尾
//				rf.writeBytes(serviceFileUnit.getFilecontent());
//
//				// logger.debug(vid + "写入轨迹文件成功");
//			} catch (FileNotFoundException e) {
//				logger.warn("在" + serviceFileUnit.getGpsTime() + "读 轨迹文件" + vid + ".txt 找不到." + e.getMessage());
//				FileUtil.coverFolder(trackfileurl);
//			} catch (IOException e) {
//				logger.error("在" + serviceFileUnit.getGpsTime() + "写入轨迹文件" + vid + ".txt" + e.getMessage());
//			} finally {
//				if (rf != null) {
//					try {
//						rf.close();
//					} catch (IOException e) {
//						logger.error("在" + serviceFileUnit.getGpsTime() + "关闭轨迹文件" + vid + ".txt 找不到." + e.getMessage());
//					}
//				}
//			}
//			String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
//
//			RandomAccessFile rfAlarm = null;
//			try {
//				rfAlarm = new RandomAccessFile(alarmPath, "rw");
//				rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
//				rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
//				// logger.debug(vid + "写入报警文件成功");
//			} catch (FileNotFoundException e) {
//				logger.warn("在" + serviceFileUnit.getGpsTime() + "读 报警文件" + vid + ".txt 找不到." + e.getMessage(), e);
//				FileUtil.coverFolder(alarmFileUrl);
//			} catch (IOException e) {
//				logger.error("在" + serviceFileUnit.getGpsTime() + "写入报警文件" + vid + ".txt" + e.getMessage(), e);
//			} finally {
//				if (rfAlarm != null) {
//					try {
//						rfAlarm.close();
//					} catch (IOException e) {
//						logger.error("在" + serviceFileUnit.getGpsTime() + "关闭报警文件" + vid + ".txt 找不到." + e.getMessage(), e);
//					}
//				}
//			}
//			serviceFileUnit.resetRecordCount();
//		}// End while
//	}
//}
