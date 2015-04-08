package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.addin.PacketAnalyser;
import com.ctfo.savecenter.addin.kcpt.filemanager.FileBlindManager;
import com.ctfo.savecenter.addin.kcpt.filemanager.FileManager;
import com.ctfo.savecenter.dao.RedisDBAdapter;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;
import com.ctfo.savecenter.dao.TrackManagerKcptMysqlDBAdapter;
import com.ctfo.savecenter.memcachemanager.IsOnlineManagerThread;
import com.ctfo.savecenter.util.FileUtil;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;
import com.lingtu.xmlconf.XmlConf;

public class TrackManagerKcptMainThread extends Thread implements PacketAnalyser {
	private static final Logger logger = LoggerFactory.getLogger(TrackManagerKcptMainThread.class);
	// 位置指令队列
	private ArrayBlockingQueue<Map<String, String>> trackPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	//当前线程编号
	private int threadId = 0;
	// 运行标志
	public static boolean isRunning = true;

	private LogicAnalysis trackKcptManagerThread;

	private IsOnlineManagerThread isOnlineManagerThread;

	private FileManager fileManager = null;

	// 盲区补传
	private FileBlindManager fileBlindManager = null;

	private int count = 0;

	public void initAnalyser(int threadId, XmlConf config, String nodeName) throws Exception {

		this.threadId = threadId;

		// 文件存储线程数
		count = config.getIntValue(nodeName + "|count");
		// 轨迹文件目录
		String trackfileurl = config.getStringValue(nodeName + "|trackfileurl");

		// 事件文件目录
		String eventfileurl = config.getStringValue(nodeName + "|eventfileurl");

		// 盲区补传轨迹和报警文件目录
		String blindTrackfileurl = config.getStringValue(nodeName + "|blindTrackfileurl");
		String blindAlarmFileUrl = config.getStringValue(nodeName + "|blindAlarmFileUrl");

		// 车辆报警统计分析目录
		String alarmFileUrl = config.getStringValue(nodeName + "|alarmfileurl");

		// 发动机负荷率
		String eloaddistfileurl = config.getStringValue(nodeName + "|eloaddistfileurl");

		// 油量变化目录
		String oilUrl = config.getStringValue(nodeName + "|oilUrl");

		// 批量数据库提交间隔时间(单位:S)
		int commitTime = config.getIntValue(nodeName + "|commitTime");

		// 创建目录
		FileUtil.createFolder(trackfileurl);
		FileUtil.createFolder(eventfileurl);
		FileUtil.createFolder(alarmFileUrl);

		FileUtil.createFolder(blindTrackfileurl);
		FileUtil.createFolder(blindAlarmFileUrl);

		FileUtil.createFolder(eloaddistfileurl);

		FileUtil.createFolder(oilUrl);

		TrackManagerKcptDBAdapter oracleDB = null;

		TrackManagerKcptMysqlDBAdapter mysqlDB = null;
		RedisDBAdapter redisDB = null;
		
		if (count != 0) {
			// 启动主线程
			start();

			mysqlDB = new TrackManagerKcptMysqlDBAdapter();
			mysqlDB.initDBAdapter(config, nodeName, this.threadId);

			oracleDB = new TrackManagerKcptDBAdapter();
			oracleDB.initDBAdapter(config, nodeName);
			
			redisDB = new RedisDBAdapter();
//			初始化车辆报警编码
			redisDB.initVehicleAlarmCode();
			// 初始化更新上下线线程
			isOnlineManagerThread = new IsOnlineManagerThread(0, config, nodeName);
			isOnlineManagerThread.start();
			
			// 初始化文件处理线程
			fileManager = new FileManager(threadId);
			fileManager.initAnalyser(config, nodeName);
			fileManager.start();

			// 初始化数据库处理线程
			trackKcptManagerThread = new LogicAnalysis(oracleDB, mysqlDB, redisDB, threadId, commitTime);
			trackKcptManagerThread.start();

			// 启动盲区补传线程
			fileBlindManager = new FileBlindManager(threadId);
			fileBlindManager.initAnalyser(config, nodeName);
			fileBlindManager.start();
		}
	}

	public void addPacket(Map<String, String> packet) {
		try {
			trackPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(Constant.SPACES,e);
		}
	}

	public int getPacketsSize() {
		return trackPacket.size();
	}

	public void endAnalyser() {
		trackKcptManagerThread.interrupt();
		fileManager.interrupt();
		fileBlindManager.interrupt();
		isOnlineManagerThread.interrupt();
	}
	public void run() {
		logger.info("监控位置主线程启动");
		Map<String, String> app = null;
		while (isRunning) {
			try// 依次向各个分析线程分发报文
			{
				app = trackPacket.take();
				
				long currentTime = System.currentTimeMillis();
				if(currentTime - tempTime > 3000){
					logger.warn("trackmain-:" + threadId + ",size:" + trackPacket.size() + ",3秒处理数据:"+index+"条");
					tempTime = currentTime;
					index = 0 ;
				}
				index ++;
				
				if (app.get("TYPE").equals("5")) { // 上线通知
					trackKcptManagerThread.addPacket(app);
					isOnlineManagerThread.addPacket(app);
					continue;
				}
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
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
				app.put(Constant.MAPLON, maplon + "");
				app.put(Constant.MAPLAT, maplat + "");
				if(!StringUtils.isNumeric(app.get(Constant.MAPLON))){ 
					app.put(Constant.MAPLON,  "0");
				}
				if(!StringUtils.isNumeric(app.get(Constant.MAPLAT))){ 
					app.put(Constant.MAPLAT,  "0");
				}
				/****
				 * 如果该终端不支持车速来源，则延用 VSS有速度且大于0则使用VSS速度
				 */
				if (app.get("218") != null) { // 车速来源 0：来自VSS 1：来自GPS
					app.put(Constant.SPEEDFROM, app.get("218"));
				} else {
					if (app.containsKey("7") && !app.get("7").equals("") && Integer.parseInt(app.get("7")) > 0) { // VSS有速度则使用VSS速度
						app.put(Constant.SPEEDFROM, "0");
					} else {
						app.put(Constant.SPEEDFROM, "1");
					}
				}
				analysisAlarm(app); // 解析报警
				// 处理盲区补传
				if (app.get("TYPE").equals("7")) {
					fileBlindManager.addPacket(app);
					fileManager.addPacket(app); // 盲区补传也存入实时文件
					continue;
				}

				trackKcptManagerThread.addPacket(app);
				fileManager.addPacket(app);
				
				
			} catch (Exception e) {
				logger.error("TrackManagerKcptMainThread : " + app.get(Constant.COMMAND) + e.getMessage());
			}
		}// End while
	}

	/***
	 * 解析报警
	 * 
	 * @param app
	 */
	private void analysisAlarm(Map<String, String> app) {
		// 解析标准报警标志位
		String alarmString = app.get("20");
		String vid = app.get(Constant.VID);
		String alarmCodeFile = ",";
		if (alarmString != null) {
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			String status;
			String alarmCode = ",";
//			StringBuffer sb = new StringBuffer("128");
//			sb.append(",");
			for (int i = 0; i < alarmstatus.length(); i++) {
				status = alarmstatus.substring(alarmstatus.length() - i - 1, alarmstatus.length() - i);
				// yujch modify 2013-05-21
				// 对TempMemory.entAlarmMap.get(Long.parseLong(ent_id +
				// ""))为空情况进行判断；扩展报警标志做相同处理
				if ("1".equals(status)) {
					if (TempMemory.vidEntMap.containsKey(Long.parseLong(vid))) { // 只实时处理企业对应设置的严重告警
						long ent_id = TempMemory.vidEntMap.get(Long.parseLong(vid));
						if (TempMemory.entAlarmMap.get(Long.parseLong(ent_id + "")) != null) {
							if (TempMemory.entAlarmMap.get(Long.parseLong(ent_id + "")).contains(String.valueOf(i))) {
								alarmCode = alarmCode + i + ",";
//								sb.append(i).append(","); 
							}
						}
					} else { // 如果不包含则取默认报警值
						if (TempMemory.entAlarmMap.get(Long.parseLong(1 + "")) != null) {
							if (TempMemory.entAlarmMap.get(Long.parseLong(1 + "")).contains(String.valueOf(i))) {
								alarmCode = alarmCode + i + ",";
//								sb.append(i).append(","); 
							}
						}
					}
					alarmCodeFile = alarmCodeFile + i + ","; // 文件存储报警CODE
				}
			}// End for
			app.put("20", alarmCode);
//			app.put("20", sb.toString());
			alarmString = null;
		}

		// 解析扩展报警标志位
		alarmString = app.get("21");
		if (alarmString != null) {
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			String status;
			String alarmCode = ",";
//			StringBuffer sb = new StringBuffer("128");
			for (int i = 0; i < alarmstatus.length(); i++) {
				status = alarmstatus.substring(alarmstatus.length() - i - 1, alarmstatus.length() - i);

				if ("1".equals(status)) {
					if (TempMemory.vidEntMap.containsKey(Long.parseLong(vid))) {// 只实时处理企业对应设置的严重告警
						long ent_id = TempMemory.vidEntMap.get(Long.parseLong(vid));
						if (TempMemory.entAlarmMap.get(Long.parseLong(ent_id + "")) != null) {
							if (TempMemory.entAlarmMap.get(Long.parseLong(ent_id + "")).contains(String.valueOf(i + 32))) {
								alarmCode = alarmCode + (i + 32) + ",";
//								sb.append(i + 32).append(","); 
							}
						}
					} else {// 如果不包含则取默认报警值
						if (TempMemory.entAlarmMap.get(Long.parseLong(1 + "")) != null) {
							if (TempMemory.entAlarmMap.get(Long.parseLong(1 + "")).contains(String.valueOf(i + 32))) {
								alarmCode = alarmCode + (i + 32) + ",";
//								sb.append(i + 32).append(","); 
							}
						}
					}
					alarmCodeFile = alarmCodeFile + (32 +i) + ","; // 文件存储报警CODE
				}
			}// End for
//			app.put("21", sb.toString());
			app.put("21", alarmCode);
		}
		app.put(Constant.FILEALARMCODE, alarmCodeFile);
	}
	 

	/*****************************************
	 * <li>描        述：解析告警 		</li><br>
	 * <li>时        间：2013-7-11  下午9:52:14	</li><br>
	 * <li>参数： @param app	数据包		</li><br>
	 * 
	 *****************************************/
//	private void parseAlarm(Map<String, String> app) {
//		String alarmString = app.get(Const.P20);
//		String vid = app.get(Constant.VID);
//		String allAlarm = Const.COMMA;
//		String status = null;
//		int alarmLenght = 0;
////		 解析基础报警 -- 判断报警是否为空
//		if (alarmString != null) {
//			String alarmStr = Const.COMMA;
//			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
//			alarmLenght = alarmstatus.length();
//			for (int j = 0; j < alarmLenght; j++) {
//				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
//				
//				if (status.equals(Const.P1)) { 
//					if (TempMemory.vidEntMap.containsKey(Long.parseLong(vid))) { // 只实时处理企业对应设置的严重告警
//						Long ent_id = TempMemory.vidEntMap.get(Long.parseLong(vid));
//						if (TempMemory.entAlarmMap.get(ent_id) != null) {
//							if (TempMemory.entAlarmMap.get(ent_id).contains(String.valueOf(j))) {
//								alarmStr += (j + Const.COMMA);
//							}
//						} 
//					} else { // 如果不包含则取默认报警值
//						if (TempMemory.entAlarmMap.get(1l) != null) {
//							if (TempMemory.entAlarmMap.get(1l).contains(String.valueOf(j))) {
//								alarmStr += (j + Const.COMMA);
//							}
//						}
//					}
//					allAlarm += (j +Const.COMMA);
//				}
//			}
//			app.put("T20", alarmStr); 
//		}
//		alarmString = null;
//		// 解析扩展报警标志位 -- 判断报警是否为空
//		alarmString = app.get(Const.P21);
////		StringUtils.isNotBlank(alarmString)
//		if (alarmString != null) {
//			String alarmStr  = Const.COMMA;
//			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
//			alarmLenght = alarmstatus.length();
//			for (int j = 0; j < alarmstatus.length(); j++) {
//				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
//				if (status.equals(Const.P1)) {
//					if (TempMemory.vidEntMap.containsKey(Long.parseLong(vid))) {// 只实时处理企业对应设置的严重告警
//						Long ent_id = TempMemory.vidEntMap.get(Long.parseLong(vid));
//						if (TempMemory.entAlarmMap.get(ent_id) != null) {
//							if (TempMemory.entAlarmMap.get(ent_id).contains(String.valueOf(j + 32))) {
//								alarmStr += ((j+32) + Const.COMMA);
//							}
//						}
//					} else {// 如果不包含则取默认报警值
//						if (TempMemory.entAlarmMap.get(1l) != null) {
//							if (TempMemory.entAlarmMap.get(1l).contains(String.valueOf(j + 32))) {
//								alarmStr = alarmStr + ((j+32) + Const.COMMA);
//							}
//						}
//					}
//					allAlarm += ((j+32) + Const.COMMA);
//				}
//			}
//			
//			app.put("T21", alarmStr);
//		}
//		app.put("T_FILEALARMCODE", allAlarm); 
//	}
}
