/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service TrackHandleThread.java	</li><br>
 * <li>时        间：2013-9-16  下午1:41:02	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.AccountUtils;
import com.ctfo.trackservice.util.Cache;
import com.ctfo.trackservice.util.Constant;
import com.ctfo.trackservice.util.DateAgingCache;
import com.ctfo.trackservice.util.Tools;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/*****************************************
 * <li>描 述：轨迹存储线程
 * 
 *****************************************/
public class TrackAnalysisThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TrackAnalysisThread.class);
	/** 数据缓冲队列 */
	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号 */
	private int threadId;
	/** 计数器 */
	private int index;
	/** 上次时间 */
	private long lastTime = System.currentTimeMillis();
	/** 速度阀值	 */
	private int speedLimit = 140;
	/** 数据访问接口 */
	private OracleService oracleService;
	/** 时间阀值	  */
	private int timeLimit = 15 * 60 * 1000;
	/** 轨迹处理线程组 */
	private TrackHandleThread trackHandleThread;
	/** redis轨迹处理线程组 */
	private RedisTrackHandleThread redisTrackHandleThread;
	/** 状态处理线程组 */
	private StatusHandlerThread statusHandlerThread;
	
	private OnOffLineHandleThread onOffLineHandleThread;
	
	private StationHandler stationHandler;

	public TrackAnalysisThread(int id, int speedLimitSet, int timeLimitSet) throws Exception {
		try {
			setName("TrackAnalysisThread-" + id); 	// 线程名称
			threadId = id;							// 线程编号
			speedLimit = speedLimitSet;				// 速度阀值 - 
			timeLimit = timeLimitSet * 60 * 1000;	// 时间阀值
//			数据库操作服务接口
			oracleService = new OracleService();	
			oracleService.initService();
//			轨迹处理线程
			trackHandleThread = new TrackHandleThread(threadId, oracleService);
			trackHandleThread.start();
//			轨迹缓存处理线程
			redisTrackHandleThread = new RedisTrackHandleThread(threadId);
			redisTrackHandleThread.start();
//			状态处理线程
			statusHandlerThread = new StatusHandlerThread(threadId, oracleService);
			statusHandlerThread.start();
//			上下线处理线程
			onOffLineHandleThread = new OnOffLineHandleThread(threadId, oracleService);
			onOffLineHandleThread.start();
//			站点处理线程
			stationHandler = new StationHandler(threadId);
			stationHandler.start();
			
		} catch (Exception e) {
			logger.error("轨迹分析线程异常:" + e.getMessage(), e);
		}
	} 

	/*****************************************
	 * <li>描 述：将数据插入队列顶部</li><br>
	 * <li>时 间：2013-9-16 下午4:42:17</li><br>
	 * <li>参数： @param dataMap</li><br>
	 * 
	 *****************************************/
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-9-16 下午4:42:47</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public int getQueueSize() {
		return dataQueue.size();
	}

	/*****************************************
	 * <li>描 述：处理轨迹逻辑</li><br>
	 * <li>时 间：2013-9-16 下午4:43:13</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void run() {
		while (true) {
			try {
				Map<String, String> dataMap = dataQueue.take();
				index++;
				
				if(dataMap.get("TYPE").equals("5")){ 
					onOffLineHandleThread.putDataMap(dataMap);
					continue;
				}
				String vid = dataMap.get(Constant.VID);
//				处理实时非法时间数据
				if(invalidTime(vid, dataMap.get(Constant.UTC))){
					continue;
				}
				boolean isPvalid = parseBaseInfo(dataMap);
				if (isPvalid) {
					int gpsSpeed = 0;
					if(dataMap.get("3") != null){
						gpsSpeed = Integer.parseInt(dataMap.get("3"));
					}
					double ratio = -100;
					int gears = -100;
					if (dataMap.containsKey("210") && gpsSpeed > 0 && dataMap.containsKey(Constant.TYRER) && dataMap.containsKey(Constant.REARAXLERATE)) {
						String tempRatio = AccountUtils.accountRatio(vid, String.valueOf(gpsSpeed), dataMap.get("210"), dataMap.get(Constant.TYRER), dataMap.get(Constant.REARAXLERATE));
						if (!tempRatio.equals("-")) {
							ratio = Double.parseDouble(tempRatio); // 速比
						}

						String tempGears = AccountUtils.accountGears(vid, String.valueOf(gpsSpeed), dataMap.get("210"), dataMap.get(Constant.TYRER), dataMap.get(Constant.REARAXLERATE));
						if (!tempGears.equals("-")) {
							gears = Integer.parseInt(tempGears); // 档位rob
						}
					}
					dataMap.put(Constant.RATIO, String.valueOf(ratio));
					dataMap.put(Constant.GEARS, String.valueOf(gears));
					dataMap.put(Constant.ISPVALID, "0");
					
					stationHandler.put(dataMap);
				}
				parseAlarm(dataMap);
				
				trackHandleThread.putDataMap(dataMap);
				
				redisTrackHandleThread.putDataMap(dataMap);
				
				statusHandlerThread.putDataMap(dataMap);
				
				long currentTime1 = System.currentTimeMillis();
				if (currentTime1 - lastTime > 10000) {
					logger.info("analysis-{}, 排队:[{}], 10s处理正常轨迹:[{}]条", threadId, getQueueSize(), index); 
					lastTime = currentTime1;
					index = 0;
				}
			} catch (Exception ex) {
				logger.error("轨迹存储线程异常:" + ex.getMessage(), ex);
			}
		}
	}
	/**
	 * <pre>
	 * 检查数据时效
	 * <li> 本次数据的GPS时间必须比上次时间新，过滤808终端补传数据
	 * @param vid
	 * @return
	 * </pre>
	 */
	private boolean invalidTime(String vid, String currentTime) {
		Long lastTime = DateAgingCache.getLastTime(vid);
		if(lastTime != null){
			long current = Long.parseLong(currentTime);
			if(current >= lastTime){
				long sysTime = System.currentTimeMillis() + timeLimit;
//				判断当前GPS时间必须不能是未来时间 （系统时间 + 5分钟）
				if(current < sysTime){
					DateAgingCache.setCurrentTime(vid, current);
					return false;
				} 
				logger.debug("车辆{}当前上报时间[{}]比系统时间(包含阀值)[{}]超过5分钟以上:lastTime is not null", vid, currentTime, sysTime);
				return true;
			} else {
				logger.debug("车辆{}当前上报时间[{}]小于上一次上报时间[{}]", vid, currentTime, lastTime); 
				return true;
			}
		} else {
			long current = Long.parseLong(currentTime);
			long sysTime = System.currentTimeMillis() + timeLimit;
//			判断当前GPS时间必须不能是未来时间 （系统时间 + 5分钟）
			if(current < sysTime){
				DateAgingCache.setCurrentTime(vid, current);
				return false;
			}
			logger.debug("车辆{}当前上报时间[{}]比系统时间(包含阀值)[{}]超过5分钟以上:lastTime is null", vid, currentTime, sysTime);
			return true;
		}
	}

	/*****************************************
	 * <li>描 述：解析告警</li><br>
	 * <li>时 间：2013-7-11 下午9:52:14</li><br>
	 * <li>参数： @param dataMap 数据包</li><br>
	 * 
	 *****************************************/
	private void parseAlarm(Map<String, String> dataMap) {
		String alarmString = dataMap.get(Constant.N20);
		String vid = dataMap.get(Constant.VID);
		String allAlarm = ",";
		String status = null;
		int alarmLenght = 0;
		// 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(alarmString)) {
			String alarmStr = ",";
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmLenght; j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);

				if (status.equals(Constant.N1)) {
					if (Cache.vidEntMap.containsKey(vid)) { // 只实时处理企业对应设置的严重告警
						if (Cache.vidEntMap.get(vid).contains(Tools.addComma(j))) {
							alarmStr += (j + ",");
						}
					} else { // 如果不包含则取默认报警值
						String defaultAlarm = Cache.entAlarmMap.get(Constant.N1);
						if(defaultAlarm != null){
							if (defaultAlarm.contains(Tools.addComma(j))) {
								alarmStr += (j + ",");
							}
						} else {
							logger.error("没有默认报警类型 - defaultAlarm:" + defaultAlarm); 
						}
					}
					allAlarm += (j + ",");
				}
			}
			dataMap.put(Constant.N20, alarmStr);
		}
		alarmString = null;
		// 解析扩展报警标志位 -- 判断报警是否为空
		alarmString = dataMap.get(Constant.N21);
		if (StringUtils.isNumeric(alarmString)) {
			String alarmStr = ",";
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmstatus.length(); j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
				if (status.equals(Constant.N1)) {
					if (Cache.vidEntMap.containsKey(vid)) {// 只实时处理企业对应设置的严重告警
						if (Cache.vidEntMap.get(vid).contains(Tools.addComma(j+32))) {
							alarmStr += ((j + 32) + ",");
						}
					} else {// 如果不包含则取默认报警值
						String defaultAlarm = Cache.entAlarmMap.get(Constant.N1);
						if(defaultAlarm != null){
							if (defaultAlarm.contains(Tools.addComma(j+32))) {
								alarmStr = alarmStr + ((j + 32) + ",");
							}
						} else {
							logger.error("没有默认报警类型 - defaultAlarm:" + defaultAlarm); 
						}
					}
					allAlarm += ((j + 32) + ",");
				}
			}
			dataMap.put(Constant.N21, alarmStr);
		}
		// 有报警就存储
		dataMap.put(Constant.FILEALARMCODE, allAlarm);
	}

	/*****************************************
	 * <li>描 述：解析基础信息</li><br>
	 * <li>时 间：2013-10-20 下午3:20:05</li><br>
	 * <li>参数： @param dataMap</li><br>
	 * 
	 *****************************************/
	public boolean parseBaseInfo(Map<String, String> dataMap) {
		// 判断定位状态
		if (!Tools.isPositioning(dataMap.get("8"))) {
			dataMap.put("isPositioning", "false");
			dataMap.put("isPValid", "-1");
			return false;
		}

		// 解析地图经度、纬度
		Long lon = Long.parseLong(dataMap.get("1"));
		Long lat = Long.parseLong(dataMap.get("2"));
		// 经纬度信息数据只要有一个异常，平台判定未定位状态，不再分析其它数据（如高度、速度、方向、GPS时间数据）
		// 经度范围72-136(43200000-81600000) 纬度范围18-54(10800000-32400000)
		if (lon < 43200000 || lon > 81600000 || lat < 10800000 || lat > 32400000) {
			dataMap.put("isPositioning", "false");
			dataMap.put("isPValid", "1");
			return false;
		}
		// 1经度错误 ;2纬度错误 ;3定位时间错误 ;4车辆速度错误 ;5行驶方向错误 ;6车辆状态错误
		// 经纬度信息数据正常，但高度、速度、方向有异常数据，平台判定未定位状态，
		// 同时在相应应用界面显示高度、速度、方向异常状态，如果终端速度选择为GPS速度来源，
		// 在BS车辆详情、运行轨迹中等应用界面中车辆速度显示：速度异常，如果是VSS速度来源，则显示实际车辆VSS速度
		/**** 如果该终端不支持车速来源，则延用 VSS 有速度且大于0则使用VSS速度    车速来源 0：来自VSS   1：来自GPS */
		String gspSpeedStr = dataMap.get("3");
		String vssSpeedStr = dataMap.get("7");
		String speedFrom = dataMap.get("218");
		if (speedFrom == null || speedFrom.equals("1")){ // 报警来源为空，默认使用GPS速度
//			判断GPS时间是否为正整数,且在速度阀值内
			if(StringUtils.isNumeric(gspSpeedStr) && Integer.parseInt(gspSpeedStr) < speedLimit){ 
				dataMap.put("speed", gspSpeedStr);
				dataMap.put("speedfrom", "1");
			} else {
				dataMap.put("speed", "-1");
				dataMap.put("isPositioning", "false");
				dataMap.put("isPValid", "4");
				dataMap.put("speedfrom", "1");
				return false;
			}
		} else if(speedFrom.equals("0")){
			if(StringUtils.isNumeric(vssSpeedStr) && Integer.parseInt(vssSpeedStr) < speedLimit){ 
				dataMap.put("speed", vssSpeedStr);
				dataMap.put("speedfrom", "0");
			} else {
				dataMap.put("speed", "-1");
				dataMap.put("isPositioning", "false");
				dataMap.put("isPValid", "4");
				dataMap.put("speedfrom", "0");
				return false;
			}
		} else {
			dataMap.put("speed", "-1");
			dataMap.put("isPositioning", "false");
			dataMap.put("isPValid", "4");
			dataMap.put("speedfrom", "1");
			return false;
		}
		
		// 处理方向
		String directionStr = dataMap.get("5");
		if (directionStr != null) {
			int direction = Integer.parseInt(dataMap.get("5"));
			if (direction < 0 || direction > 360) {
				dataMap.put("5", "-1");
				dataMap.put("isPositioning", "false");
				dataMap.put("isPValid", "5");
				return false;
			}
		} else {
			dataMap.put("5", "-1");
			dataMap.put("isPositioning", "false");
			dataMap.put("isPValid", "5");
			return false;
		}

		String macid = dataMap.get(Constant.MACID);
		String[] oemStr = StringUtils.split(macid, "_", 2);
		dataMap.put(Constant.OEMCODE, oemStr[0]);

		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		dataMap.put(Constant.MAPLON, maplon + "");
		dataMap.put(Constant.MAPLAT, maplat + "");
		if (!StringUtils.isNumeric(dataMap.get(Constant.MAPLON))) {
			dataMap.put(Constant.MAPLON, "0");
		}
		if (!StringUtils.isNumeric(dataMap.get(Constant.MAPLAT))) {
			dataMap.put(Constant.MAPLAT, "0");
		}
		dataMap.put("isPValid", "0");
		return true;
	}
	public static void main(String[] args) { 
		System.out.println(StringUtils.isNumeric("140")); 
	}
}
