package com.ctfo.savecenter.addin.kcpt.trackmanager;


import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.EquipmentStatus;
import com.ctfo.savecenter.beans.StatusCode;
import com.ctfo.savecenter.dao.MonitorDBAdapter;
import com.ctfo.savecenter.dao.RedisDBAdapter;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;
import com.ctfo.savecenter.dao.TrackManagerKcptMysqlDBAdapter;
import com.ctfo.savecenter.util.AccountUtils;
import com.ctfo.savecenter.util.MathUtils;

public class LogicAnalysis extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(LogicAnalysis.class);

	// 异步数据报向量
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
	//计数器
	private int index = 0 ;
	// 线程id
	private int nId = 0;

	private TrackManagerKcptDBAdapter oracleDB = null;
	
	private RedisDBAdapter redisDB = null;

	private EquipmentManagerThread eqThread = null;

	// ORACLE 处理轨迹
	private TrackManagerThread trackThread = null;

	private AlarmManagerThread alarmThread = null;
	
	
	private TrackRedisManagerThread redis = null;

	// 批量数据库提交间隔时间(单位:S)
	private long commitTime = 0;
	//提交频率（秒）	
	private long commit = 10;
	
	private long lastCommitTime = System.currentTimeMillis();

	public LogicAnalysis(TrackManagerKcptDBAdapter oracleDB,
			TrackManagerKcptMysqlDBAdapter mysqlDB,RedisDBAdapter redisDB, int nId, long trackcCmmitTime) {
		this.oracleDB = oracleDB;
		this.redisDB = redisDB;
		this.nId = nId;
		
		this.commit = trackcCmmitTime ;
		// 批量数据库提交间隔时间(单位:S)
		this.commitTime = this.commit * 1000 ;

		// 初始化statement
		oracleDB.createStatement();
		mysqlDB.createPreparedStatement();

		eqThread = new EquipmentManagerThread(oracleDB,nId);
		eqThread.start();

		trackThread = new TrackManagerThread(oracleDB,nId);
		trackThread.start();
		
		alarmThread = new AlarmManagerThread(oracleDB, mysqlDB, nId);
		alarmThread.start();
		
		redis = new TrackRedisManagerThread(redisDB,nId);
		redis.start();
	}

	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("",e);
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	/**  */
	@Override
	public void run() {
		logger.info("轨迹线程" + nId + "启动");
		Map<String, String> app = null;
		while (TrackManagerKcptMainThread.isRunning) {
			try {
				app = vPacket.take();
				if (app.get("TYPE").equals("5")) { // 上下线通知，更新最新位置表上下线状态值
					updateTrackIsOnline(app);
				}else{
					analysisLastPosition(app);
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastCommitTime) > commitTime){
					oracleDB.commit();
					lastCommitTime = currentTime;
					logger.warn("logic-----:" + nId + ",size:" + vPacket.size() + ","+commit+"秒处理数据:"+index+"条");
					index = 0;
				}
				index ++;
			} catch (Exception e) {
				logger.error("监控位置线程" + nId + ":" + (app !=null ? app.get(Constant.COMMAND):"")  + e.getMessage(), e);
			}
		}// End while
	}
	
	/****
	 * 更新最新位置表上下线状态值
	 * @param app
	 */
	private void updateTrackIsOnline(Map<String, String> app){
		try {
			oracleDB.oracleUpdateIsonline(app);
			redisDB.setOffLine(app);
			
//			JedisConnectionServer.updateOnlineAndOfflineStatus(app.get(Constant.VID), app.get("18"), app.get(Constant.MSGID));
		} catch (SQLException e) {
			logger.error("更新车辆状态出错." + e.getMessage());
		}
		
	}

	/***
	 * 解析数据
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void analysisLastPosition(Map<String, String> app)
			throws SQLException {
		EquipmentStatus eqStatus = new EquipmentStatus();
		//String
		int gpsSpeed = 0;
		if(app.get("3") != null){
			gpsSpeed = Integer.parseInt(app.get("3"));
		}
		
		long lon = Long.parseLong(app.get("1"));
		long lat = Long.parseLong(app.get("2"));
		long vid = Long.parseLong(app.get(Constant.VID));
		long utc = Long.parseLong(app.get(Constant.UTC));
		
//		初始化时更新，减少程序运行时耗时
//		String macid = app.get(Constant.MACID);
//		Map<String, String> map = TempMemory.vehicleStatusMap.get(macid);
//		if (map == null) {
//			map = redisDB.getLastAlarmCode(vid + "");
//			// 设置缓存中值
//			if(map!=null){
//				TempMemory.vehicleStatusMap.put(macid, map);
//			}
//		}
		
		analyVehicleLineStatus(eqStatus, app, vid, utc);
		alarmThread.addPacket(app);
		
		// 更新轨迹表
		int isPValid = isPValid(lon, lat, utc, gpsSpeed, Integer.parseInt(app.get("5")));
		if (isPValid == 0) {
			double ratio = -100;
			int gears = -100;
			if (app.containsKey("210") && gpsSpeed > 0
					&& app.containsKey(Constant.TYRER)
					&& app.containsKey(Constant.REARAXLERATE)) {
				String tempRatio = AccountUtils
						.accountRatio(vid, String.valueOf(gpsSpeed),
								app.get("210"), app.get(Constant.TYRER),
								app.get(Constant.REARAXLERATE));
				if (!tempRatio.equals("-")) {
					ratio = Double.parseDouble(tempRatio); // 速比
				}

				String tempGears = AccountUtils
						.accountGears(vid, String.valueOf(gpsSpeed),
								app.get("210"), app.get(Constant.TYRER),
								app.get(Constant.REARAXLERATE));
				if (!tempGears.equals("-")) {
					gears = Integer.parseInt(tempGears); // 档位rob
				}
			}
			app.put(Constant.RATIO, String.valueOf(ratio));
			app.put(Constant.GEARS, String.valueOf(gears));
			app.put(Constant.ISPVALID, "0");
			trackThread.addPacket(app);
			redis.addPacket(app);
		} else {
			app.put(Constant.ISPVALID, isPValid + "");
			trackThread.addPacket(app);
			redis.addPacket(app);
		}

		// 更新车辆设备状态(分流到安装设备状态线程)
		eqThread.addPacket(eqStatus);
	}

	/****
	 * 处理整车状态数据
	 * 
	 * @param eqStatus
	 * @param app
	 * @param vid
	 * @param utc
	 * @throws SQLException
	 */
	private void analyVehicleLineStatus(EquipmentStatus eqStatus,
			Map<String, String> app, long vid, long utc) throws SQLException {
		StatusCode statusCode = null;
		statusCode = MonitorDBAdapter.queryStatusCode(vid);
		if (statusCode != null) {
			
			eqStatus.setVid(vid);
			// 车辆状态值用(0 绿灯 1红灯 2 灰灯)表示
			// 实体bean属性以code结尾的表示状态，
			// 8:位置基本信息状态位
			// GPS状态
			if (statusCode.getGpsStatus().getType() == 0) {
				if (app.containsKey("8")) {
					String tempStatus = app.get("8");
					String status = Long.toBinaryString(Long.parseLong(tempStatus));
					if (status.matches(".*0\\d{1}") || status.equals("0") || status.equals("1")) {
						eqStatus.setGpsStatusStatus(1);
						eqStatus.setGpsValue(0);
					} else {
						eqStatus.setGpsStatusStatus(0);
						eqStatus.setGpsValue(1);
					}
				} else {
					eqStatus.setGpsStatusStatus(2);
				}
			} else {
				eqStatus.setGpsStatusStatus(2);
			}

			// 冷却液温度
			if (app.get("509") != null && !app.get("509").equals("-1") && !app.get("509").equals("")) {
				double eWater = Double.parseDouble(app.get("509"));
				eqStatus.seteWaterValue(eWater);
				if (MathUtils.checkEWaterValue(statusCode.geteWater(),eWater - 40)) {
					eqStatus.seteWaterStatus(0);
					TempMemory.addFiveEWaterStatus(vid, 0); // 缓存当前状态值
				} else {
					eqStatus.seteWaterStatus(1);
					TempMemory.addFiveEWaterStatus(vid, 1); // 缓存当前状态值
				}
			} else if(app.get("509") != null && app.get("509").equals("-1")) { // 上报无效值
				eqStatus.seteWaterValue(-2);
				eqStatus.seteWaterStatus(2);
				TempMemory.addFiveEWaterStatus(vid, 2); // 缓存当前状态值
			}else{//无值上报
				int status = TempMemory.getFiveEWaterStatus(vid);
				eqStatus.seteWaterStatus(status);
				if(status == 2){
					eqStatus.seteWaterValue(-2);
				}
			}

			// 蓄电池电压
			if (app.containsKey("507")  && !app.get("507").equals("-1") && !app.get("507").equals("")) { // 1bit=0.1V,// 0=0V
				double extVoltage = Double.parseDouble(app.get("507"));
				eqStatus.setExtVoltageValue(extVoltage);
				if (MathUtils.checkValue(statusCode.getExtVoltage(),
						extVoltage * 0.1)) {
					eqStatus.setExtVoltageStatus(0);
					TempMemory.addFiveExtVoltageStatus(vid, 0); // 缓存当前状态值
				} else {
					eqStatus.setExtVoltageStatus(1);
					TempMemory.addFiveExtVoltageStatus(vid, 1); // 缓存当前状态值
				}
			} else if(app.get("507") != null && app.get("507").equals("-1")){ // 上报无效值
				eqStatus.setExtVoltageValue(-2);
				eqStatus.setExtVoltageStatus(2);
				TempMemory.addFiveExtVoltageStatus(vid, 2); // 缓存当前状态值
			}else{ // 无值上报
				int status = TempMemory.getFiveExtVoltageStatus(vid);
				if(status == 2){
					eqStatus.setExtVoltageValue(-2);
				}
				eqStatus.setExtVoltageStatus(status);
			}

			// 大气压力
			if (app.containsKey("511") && !app.get("511").equals("-1") && !app.get("511").equals("")) {
				double airPressure = Double.parseDouble(app.get("511"));
				if (MathUtils.checkValue(statusCode.getAirPressure(),
						airPressure / 2)) {
					eqStatus.setAirPressureStatus(0);
					TempMemory.addFiveAirPressureStatus(vid, 0); // 缓存当前状态值
				} else {
					eqStatus.setAirPressureStatus(1);
					TempMemory.addFiveAirPressureStatus(vid, 1); // 缓存当前状态值
				}
				eqStatus.setAirPressureValue(airPressure);
			} else if(app.get("511") != null && app.get("511").equals("-1")){ // 上报无效值
				eqStatus.setAirPressureValue(-2);
				eqStatus.setAirPressureStatus(2);
				TempMemory.addFiveAirPressureStatus(vid, 2); // 缓存当前状态值
			}else{ // 无值上报
				int status = TempMemory.getFiveAirPressureStatus(vid);
				eqStatus.setAirPressureStatus(status);
				if(status == 2){
					eqStatus.setAirPressureValue(-2);
				}
			}

			String alarmCode = app.get("21"); //扩展报警位
			if (alarmCode == null) {
				eqStatus.setOilPressureStatus(2);// 油压状态
				eqStatus.setBrakePressureStatus(2);// 制动气压值
				eqStatus.setBrakepadFrayStatus(2);// 制动蹄片磨损
				eqStatus.setOilAramStatus(2);// 燃油告警
				eqStatus.setCoolantLevelStatus(2);// 水位低状态
				eqStatus.setAirFilterStatus(2);// 空滤堵塞
				eqStatus.setMwereBlockingStatus(2);// 机虑堵塞
				eqStatus.setFuelBlockingStatus(2);// 燃油堵塞
				eqStatus.setEoilTemperatureStatus(2);// 机油温度
				eqStatus.setRetarerHtStatus(2);// 缓速器高温
				eqStatus.setEhousingStatus(2);// 仓温高状态
				eqStatus.setAbsBugStatus(2); // ABS故障状态
			} else {

				// ABS故障状态
				if (alarmCode.contains(",53,")) { // 开始
					eqStatus.setAbsBugStatus(1);
					eqStatus.setAbsBugValue(1);
				} else { // 结束
					eqStatus.setAbsBugStatus(0);
					eqStatus.setAbsBugValue(0);
				}
				
				// 油压状态
				if (alarmCode.contains(",34,")) { // 开始
					eqStatus.setOilPressureStatus(1);
					eqStatus.setOilPressureValue(1);
				} else { // 结束
					eqStatus.setOilPressureStatus(0);
					eqStatus.setOilPressureValue(0);
				}

				// 制动气压值
				if (alarmCode.contains(",33,")) { // 开始
					eqStatus.setBrakePressureStatus(1);
					eqStatus.setBrakePressureValue(1);
				} else { // 结束
					eqStatus.setBrakePressureStatus(0);
					eqStatus.setBrakePressureValue(0);
				}

				// 制动蹄片磨损
				if (alarmCode.contains(",36,")) { // 开始
					eqStatus.setBrakepadFrayStatus(1);
					eqStatus.setBrakepadFrayValue(1);
				} else { // 结束
					eqStatus.setBrakepadFrayStatus(0);
					eqStatus.setBrakepadFrayValue(0);
				}

				// 燃油告警
				if (alarmCode.contains(",41,")) { // 开始
					eqStatus.setOilAramStatus(1);
					eqStatus.setOilAramValue(1);
				} else { // 结束
					eqStatus.setOilAramStatus(0);
					eqStatus.setOilAramValue(0);
				}

				// 水位低状态
				if (alarmCode.contains(",35,")) { // 开始
					eqStatus.setCoolantLevelStatus(1);
					eqStatus.setCoolantLevelValue(1);
				} else { // 结束
					eqStatus.setCoolantLevelStatus(0);
					eqStatus.setCoolantLevelValue(0);
				}

				// 空滤堵塞
				if (alarmCode.contains(",37,")) { // 开始
					eqStatus.setAirFilterStatus(1);
					eqStatus.setAirFilterValue(1);
				} else { // 结束
					eqStatus.setAirFilterStatus(0);
					eqStatus.setAirFilterValue(0);
				}

				// 机虑堵塞
				if (alarmCode.contains(",40,")) { // 开始
					eqStatus.setMwereBlockingStatus(1);
					eqStatus.setMwereBlockingValue(1);
				} else { // 结束
					eqStatus.setMwereBlockingStatus(0);
					eqStatus.setMwereBlockingValue(0);
				}

				// 燃油堵塞
				if (alarmCode.contains(",41,")) { // 开始
					eqStatus.setFuelBlockingStatus(1);
					eqStatus.setFuelBlockingValue(1);
				} else { // 结束
					eqStatus.setFuelBlockingStatus(0);
					eqStatus.setFuelBlockingValue(0);
				}

				// 机油温度
				if (alarmCode.contains(",42,")) { // 开始
					eqStatus.setEoilTemperatureStatus(1);
					eqStatus.setEoilTemperatureValue(1);
				} else { // 结束
					eqStatus.setEoilTemperatureStatus(0);
					eqStatus.setEoilTemperatureValue(0);
				}

				// 缓速器高温
				if (alarmCode.contains(",38,")) { // 开始
					eqStatus.setRetarerHtStatus(1);
					eqStatus.setRetarerHtValue(1);
				} else { // 结束
					eqStatus.setRetarerHtStatus(0);
					eqStatus.setRetarerHtValue(0);
				}

				// 仓温高状态
				if (alarmCode.contains(",39,")) { // 开始
					eqStatus.setEhousingStatus(1);
					eqStatus.setEhousingValue(1);
				} else { // 结束
					eqStatus.setEhousingStatus(0);
					eqStatus.setEhousingValue(0);
				}
			}
			
			// 终端状态
			String alarmAddCode = app.get("20"); // 标准报警
			if(alarmAddCode == null){
				eqStatus.setGpsFaultStatus(2); // GNSS模块故障报警
				eqStatus.setGpsOpenciruitStatus(2); // GNSS天线未接或被剪断报警
				eqStatus.setGpsShortciruitStatus(2); // GNSS天线短路报警
				eqStatus.setTerminalUnderVoltageStatus(2); // 终端主电源欠压报警
				eqStatus.setTerminalPowerDownStatus(2); // 终端主电源掉电报警
				eqStatus.setTerminalScreenfalutStatus(2); // 终端LCD显示屏故障报警
				eqStatus.setTtsFaultStatus(2); // TTS模块故障报警
				eqStatus.setCameraFaultStatus(2); // 摄像头故障报警
				eqStatus.setTerminalStatus(2);// 终端状态
			}else{
				// GNSS模块故障报警
				if (alarmAddCode.contains(",4,")) { // 开始
					eqStatus.setGpsFaultStatus(1);
					eqStatus.setGpsFaultValue(1);
				} else { // 结束
					eqStatus.setGpsFaultStatus(0);
					eqStatus.setGpsFaultValue(0);
				}

				// GNSS天线未接或被剪断报警
				if (alarmAddCode.contains(",5,")) { // 开始
					eqStatus.setGpsOpenciruitStatus(1);
					eqStatus.setGpsOpenciruitValue(1);
				} else { // 结束
					eqStatus.setGpsOpenciruitStatus(0);
					eqStatus.setGpsOpenciruitValue(0);
				}

				// GNSS天线短路报警
				if (alarmAddCode.contains(",6,")) { // 开始
					eqStatus.setGpsShortciruitStatus(1);
					eqStatus.setGpsShortciruitValue(1);
				} else { // 结束
					eqStatus.setGpsShortciruitStatus(0);
					eqStatus.setGpsShortciruitValue(0);
				}

				// 终端主电源欠压报警
				if (alarmAddCode.contains(",7,")) { // 开始
					eqStatus.setTerminalUnderVoltageStatus(1);
					eqStatus.setTerminalUnderVoltageValue(1);
				} else { // 结束
					eqStatus.setTerminalUnderVoltageStatus(0);
					eqStatus.setTerminalUnderVoltageValue(0);
				}

				// 终端主电源掉电报警
				if (alarmAddCode.contains(",8,")) { // 开始
					eqStatus.setTerminalPowerDownStatus(1);
					eqStatus.setTerminalPowerDownValue(1);
				} else { // 结束
					eqStatus.setTerminalPowerDownStatus(0);
					eqStatus.setTerminalPowerDownValue(0);
				}

				// 终端LCD显示屏故障报警
				if (alarmAddCode.contains(",9,")) { // 开始
					eqStatus.setTerminalScreenfalutStatus(1);
					eqStatus.setTerminalScreenfalutValue(1);
				} else { // 结束
					eqStatus.setTerminalScreenfalutStatus(0);
					eqStatus.setTerminalScreenfalutValue(0);
				}

				// TTS模块故障报警
				if (alarmAddCode.contains(",10,")) { // 开始
					eqStatus.setTtsFaultStatus(1);
					eqStatus.setTtsFaultValue(1);
				} else { // 结束
					eqStatus.setTtsFaultStatus(0);
					eqStatus.setTtsFaultValue(0);
				}

				// 摄像头故障报警
				if (alarmAddCode.contains(",11,")) { // 开始
					eqStatus.setCameraFaultStatus(1);
					eqStatus.setCameraFaultValue(1);
				} else { // 结束
					eqStatus.setCameraFaultStatus(0);
					eqStatus.setCameraFaultValue(0);
				}
				
				if (alarmAddCode.contains(",4,") // GNSS模块故障报警 开始
						|| alarmAddCode.contains(",5,") // GNSS天线未接或被剪断报警 开始
						|| alarmAddCode.contains(",6,") // GNSS天线短路报警 开始
						|| alarmAddCode.contains(",7,") // 终端主电源欠压报警 开始
						|| alarmAddCode.contains(",8,") // 终端主电源掉电报警 开始
						|| alarmAddCode.contains(",9,") // 终端LCD显示屏故障报警 开始
						|| alarmAddCode.contains(",10,") // TTS模块故障报警 开始
						|| alarmAddCode.contains(",11,")) { // 摄像头故障报警 开始
					eqStatus.setTerminalStatus(1);
					eqStatus.setTerminalValue(1);
				} else {
					eqStatus.setTerminalStatus(0);
					eqStatus.setTerminalValue(0);
				}
			}
		}
	}

	/**
	 * 判断轨迹的合法性
	 */
	private short isPValid(long lon, long lat, long utc, int speed, int head) {

		// 1经度错误 2纬度错误 3定位时间错误 4车辆速度错误 5行驶方向错误 6车辆状态错误
		if (lon < 43200000 || lon > 81600000) {// 经度范围72-136(43200000-81600000)
			return 1;
		} else if (lat < 10800000 || lat > 32400000) {// 纬度范围18-54(10800000-32400000)
			return 2;
		} else if (Math.abs(utc / 1000 - System.currentTimeMillis() / 1000) > 86400) {// 定位时间与当前服务器系统时间差不超过24小时
			return 3;
		} else if (speed < 0 || speed > 1600) {// 车辆速度0~1600(单位：0.1km/h)
			return 4;
		} else if (head < 0 || head > 360) {// 行驶方向0~360
			return 5;
		} else {// 合法
			return 0;
		}
	}
}
