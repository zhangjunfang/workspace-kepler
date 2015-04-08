package com.ctfo.savecenter.dao;


import java.sql.SQLException;
import java.sql.Types;
import java.util.Map;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.AlarmTypeBean;
import com.ctfo.savecenter.beans.EquipmentStatus;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.Tools;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据库访问类
 * 
 * 继承框架的类
 * 
 */
public class TrackManagerKcptDBAdapter {
	private static final Logger logger = LoggerFactory.getLogger(TrackManagerKcptDBAdapter.class);
	//记录存在空值
	private static final Logger nulldata = LoggerFactory.getLogger("nulldata");
	
	/**  数据库连接对象  */
	private OracleConnection dbCon;
	
	/**  数据库连接对象  */
	private OracleConnection dbAlarmCon;
	
	/**  存储非法轨迹连接  */
	private OracleConnection saveDumpTrackConn;
	
	/**  更新车辆总线状态连接  */
	private OracleConnection updateStatusConn;
	
	/** 更新轨迹最后位置 */
	private OraclePreparedStatement stUpdateLastTrack;

	/** 更新轨迹最后位置报警 */
	private OraclePreparedStatement stUpdateLastTrackA;

	/** 更新轨迹带总线数据最后位置 */
	private OraclePreparedStatement stUpdateLastTrackLine;

	/** 更新轨迹带总线数据最后位置报警 */
	private OraclePreparedStatement stUpdateLastTrackALine;

	/** 纪录报警报文 */
	private OraclePreparedStatement stSaveAlarmTrack;

	/** 更新车辆报警 */
	private OraclePreparedStatement stUpdateAlarmTrack;

	/** 记录非法轨迹包 */
	private OraclePreparedStatement stSaveDumpTrack;

	/** 更新车辆总线状态 */
	private OraclePreparedStatement stUpdateVehicleLineStatus;

	/*** 更新车辆是否在线状态及数据是否有效值 ***/
	private OraclePreparedStatement stUpdateLastTrackISonLine;
	
	/*** 更新车辆是否在线状态 ***/
	private OraclePreparedStatement stUpdateTrackISonLine;

	// 轨迹包更新最后位置到数据库
	private String updateLastTrack = null;

	// 轨迹包更新最后位置到数据库
	private String updateLastTrackA = null;

	// 轨迹包带总线数据更新最后位置到数据库
	private String updateLastTrackLine = null;

	// 轨迹包带总线数据更新最后位置到数据库
	private String updateLastTrackALine = null;

	// 存储合法报警报文
	private String saveAlarmTrack = null;

	// 更新报警包结束时间
	private String sql_updateAlarmTrack = null;

	// 非法轨迹包存入数据库
	private String saveDumpTrack = null;

	// 更新车辆总线状态信息
	private String updateVehicleLineStatus = null;

	// 更新轨迹在线状态
	private String sql_UpdateLastTrackISonLine;
	
	//更新是否在线状态值
	private String sql_updatIsOnlineLastTrack = null;

	// 轨迹批量数据库提交数
	private int commitTrackCount = 0;

	// 报警批量数据库提交数
	private int commitAlarmCount = 0;

	// 安全设备批量数据库提交数
	private int commitEqCount = 0;

	/****
	 * 
	 * @param config
	 * @param nodeName
	 * @throws Exception
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {

		// 轨迹包更新最后位置到数据库
		updateLastTrackLine = config.getStringValue(nodeName + "|sql_updateLastTrackLine");
		// 轨迹包更新最后位置到数据库
		updateLastTrackALine = config.getStringValue(nodeName + "|sql_updateLastTrackALine");
		// 轨迹包更新最后位置到数据库
		updateLastTrack = config.getStringValue(nodeName + "|sql_updateLastTrack");
		// 轨迹包更新最后位置到数据库
		updateLastTrackA = config.getStringValue(nodeName + "|sql_updateLastTrackA");
		// 更新轨迹在线状态
		sql_UpdateLastTrackISonLine = config.getStringValue(nodeName 	+ "|sql_updateLastTrackISonLine");

		// 存储合法报警报文
		saveAlarmTrack = config.getStringValue(nodeName + "|sql_saveAlarmTrack");
		// 更新报警包结束时间
		sql_updateAlarmTrack = config.getStringValue(nodeName + "|sql_updateAlarmTrack");
		
		// 非法轨迹包存入数据库
		saveDumpTrack = config.getStringValue(nodeName + "|sql_saveDumpTrack");
	
		// 更新车辆总线状态信息
		updateVehicleLineStatus = config.getStringValue(nodeName + "|sql_updateVehicleLineStatus");


		// 轨迹批量数据库提交数
		commitTrackCount = config.getIntValue(nodeName + "|commitTrackCount");

		// 报警批量数据库提交数
		commitAlarmCount = config.getIntValue(nodeName + "|commitAlarmCount");

		// 安全设备批量数据库提交数
		commitEqCount = config.getIntValue(nodeName + "|commitEqCount");
		
		// 更新ORACLE最新位置表
		sql_updatIsOnlineLastTrack = config.getStringValue(nodeName + "|sql_updateTrackisonline");
	}

	public void createStatement() {
		try {
			//1. 更新轨迹连接
			dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			// 轨迹包带总线数据更新最后位置到数据库
			stUpdateLastTrackLine = (OraclePreparedStatement)dbCon.prepareStatement(updateLastTrackLine);
			stUpdateLastTrackLine.setExecuteBatch(commitTrackCount);
			
			// 轨迹包带总线数据更新最后位置到数据库
			stUpdateLastTrackALine = (OraclePreparedStatement)dbCon.prepareStatement(updateLastTrackALine);
			stUpdateLastTrackALine.setExecuteBatch(commitTrackCount);
			
			// 轨迹包更新最后位置到数据库
			stUpdateLastTrack = (OraclePreparedStatement)dbCon.prepareStatement(updateLastTrack);
			stUpdateLastTrack.setExecuteBatch(commitTrackCount);
			
			// 轨迹包更新最后位置到数据库
			stUpdateLastTrackA = (OraclePreparedStatement)dbCon.prepareStatement(updateLastTrackA);
			stUpdateLastTrackA.setExecuteBatch(commitTrackCount);
			
			stUpdateLastTrackISonLine = (OraclePreparedStatement)dbCon.prepareStatement(sql_UpdateLastTrackISonLine);
			stUpdateLastTrackISonLine.setExecuteBatch(1);
			
			
			//2. 非法轨迹包存入数据库
			saveDumpTrackConn = (OracleConnection)OracleConnectionPool.getConnection();
			stSaveDumpTrack = (OraclePreparedStatement)saveDumpTrackConn.prepareStatement(saveDumpTrack);
			stSaveDumpTrack.setExecuteBatch(20);

			
			//3. 更新车辆总线状态连接
			updateStatusConn = (OracleConnection)OracleConnectionPool.getConnection();
			stUpdateVehicleLineStatus = (OraclePreparedStatement) updateStatusConn.prepareStatement(updateVehicleLineStatus);
			stUpdateVehicleLineStatus.setExecuteBatch(commitEqCount);
			
			
			//4. 存储合法报警报文
			dbAlarmCon = (OracleConnection)OracleConnectionPool.getConnection();
			stSaveAlarmTrack = (OraclePreparedStatement)dbAlarmCon.prepareStatement(saveAlarmTrack);
			stSaveAlarmTrack.setExecuteBatch(commitAlarmCount);
			// 更新报警包结束时间
			stUpdateAlarmTrack = (OraclePreparedStatement)dbAlarmCon.prepareStatement(sql_updateAlarmTrack);
			stUpdateAlarmTrack.setExecuteBatch(1);
			
			
		} catch (SQLException e) {
			logger.error("oracle初始化statement出错.", e);
		}
	}

	public void commit() throws SQLException {
		try{
			stSaveAlarmTrack.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbAlarmCon.getMetaData();
				if(stSaveAlarmTrack == null){
					stSaveAlarmTrack = createStatement(dbAlarmCon,commitAlarmCount,saveAlarmTrack);
				}
			}catch(Exception ex){
				stSaveAlarmTrack = recreateStatement(dbAlarmCon,commitAlarmCount,saveAlarmTrack);
			}
		}
		
		try{
			stUpdateAlarmTrack.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbAlarmCon.getMetaData();
				if(stUpdateAlarmTrack == null){
					stUpdateAlarmTrack = createStatement(dbAlarmCon,1,sql_updateAlarmTrack);
				}
			}catch(Exception ex){
				stUpdateAlarmTrack = recreateStatement(dbAlarmCon,1,sql_updateAlarmTrack);
			}
		}
		
		try{
			stUpdateLastTrackLine.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbCon.getMetaData();
				if(stUpdateLastTrackLine == null){
					stUpdateLastTrackLine = createStatement(dbCon,commitTrackCount,updateLastTrackLine);
				}
			}catch(Exception ex){
				stUpdateLastTrackLine = recreateStatement(dbCon,commitTrackCount,updateLastTrackLine);
			}
		}
		
		try{
			stUpdateLastTrackALine.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbCon.getMetaData();
				if(stUpdateLastTrackALine == null){
					stUpdateLastTrackALine = createStatement(dbCon,commitTrackCount,updateLastTrackALine);
				}
			}catch(Exception ex){
				stUpdateLastTrackALine = recreateStatement(dbCon,commitTrackCount,updateLastTrackALine);
			}
		}
		
		try{
			stUpdateLastTrack.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbCon.getMetaData();
				if(stUpdateLastTrack == null){
					stUpdateLastTrack = createStatement(dbCon,commitTrackCount,updateLastTrack);
				}
			}catch(Exception ex){
				stUpdateLastTrack = recreateStatement(dbCon,commitTrackCount,updateLastTrack);
			}
		}
		
		try{
			stUpdateLastTrackA.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				dbCon.getMetaData();
				if(stUpdateLastTrackA == null){
					stUpdateLastTrackA = createStatement(dbCon,commitTrackCount,updateLastTrackA);
				}
			}catch(Exception ex){
				stUpdateLastTrackA = recreateStatement(dbCon,commitTrackCount,updateLastTrackA);
			}
		}
		
		try{
			stUpdateVehicleLineStatus.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				updateStatusConn.getMetaData();
				if(stUpdateVehicleLineStatus == null){
					stUpdateVehicleLineStatus = createStatement(updateStatusConn,commitEqCount,updateVehicleLineStatus);
				}
			}catch(Exception ex){
				stUpdateVehicleLineStatus = recreateStatement(updateStatusConn,commitEqCount,updateVehicleLineStatus);
			}
		}
		
		try{
			stSaveDumpTrack.sendBatch();
		}catch(Exception e){
			logger.error(Constant.SPACES,e);
			try{
				saveDumpTrackConn.getMetaData();
				if(stSaveDumpTrack == null){
					stSaveDumpTrack = createStatement(saveDumpTrackConn, 20, saveDumpTrack);
				}
			}catch(Exception ex){
				stSaveDumpTrack = recreateStatement(saveDumpTrackConn, 20, saveDumpTrack);
			}
		}
	}
	
	private OraclePreparedStatement recreateStatement(OracleConnection dbCon, int count,String sql){
		OraclePreparedStatement stat= null;
		try {
			
			if(dbCon != null){
				dbCon.close();
				dbCon = null;
			}
				// 从连接池获得连接
			dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			
			// 轨迹包更新最后位置到数据库
			stat = (OraclePreparedStatement)dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
			logger.info("Create statement successfully!");
		} catch (SQLException e) {
			logger.error("Create statement error",e);
		}
		return stat;
	}
	
	private OraclePreparedStatement createStatement(OracleConnection dbCon,int count,String sql){
		// 轨迹包更新最后位置到数据库
		OraclePreparedStatement stat= null;
		try {
			stat = (OraclePreparedStatement)dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
		} catch (SQLException e) {
			logger.error(Constant.SPACES,e);
		}
		return stat;
	}
	
	/**
	 * 更新轨迹包带总线数据最后位置到数据库
	 * 
	 * @param app
	 *            位置报文类
	 */
	public void updateLastTrackLine(Map<String, String> app)
			throws SQLException {
		int gpsSpeed = Integer.parseInt(app.get("3"));
		long lon = Long.parseLong(app.get("1"));
		long lat = Long.parseLong(app.get("2"));
		long vid = Long.parseLong(app.get(Constant.VID));
		int head = Integer.parseInt(app.get("5"));
 
		//String gpsTime = app.get("4");
		long utc = Long.parseLong(app.get(Constant.UTC));
		long mapLon = Long.parseLong(app.get(Constant.MAPLON));
		long mapLat = Long.parseLong(app.get(Constant.MAPLAT));
		String alarmCode = app.get("20");
		if (app.containsKey("21")) {
			alarmCode = alarmCode.substring(0, alarmCode.length() - 1)
					+ app.get("21");
		}
		alarmCode = alarmCode.replaceAll("\\,\\,", ",");
		String basestatus = app.get("8");// 基本状态
		String extendstatus = app.get("500");// 扩展状态
		
		String msgid=app.get(Constant.MSGID);

		if (alarmCode != null && alarmCode.length() > 1) {
			try {
				stUpdateLastTrackALine.setLong(1, lon); // LON
				stUpdateLastTrackALine.setLong(2, lat); // LAT
				stUpdateLastTrackALine.setInt(3, gpsSpeed); // GPS_SPEED
				String mileage = app.get("9");
				if (mileage != null) {
					stUpdateLastTrackALine.setLong(4, Long.parseLong(mileage)); // MILEAGE
					stUpdateLastTrackALine.setLong(5, Long.parseLong(mileage)); // MILEAGE
				}else{
					stUpdateLastTrackALine.setLong(4, -1); // MILEAGE
					stUpdateLastTrackALine.setNull(5, Types.INTEGER); // MILEAGE
				}
				stUpdateLastTrackALine.setInt(6, head); // DIRECTION
				stUpdateLastTrackALine.setLong(7, utc); // UTC
				stUpdateLastTrackALine.setLong(8, CDate.getCurrentUtcMsDate()); // SYSUTC
				stUpdateLastTrackALine.setLong(9, mapLon); // MAPLON
				stUpdateLastTrackALine.setLong(10, mapLat); // MAPLAT
				stUpdateLastTrackALine.setLong(11, utc); // ALARM_UTC
				if (app.get("6") != null) { //  
					stUpdateLastTrackALine.setLong(12,
							Long.parseLong(app.get("6")));   
				} else {
					stUpdateLastTrackALine.setNull(12, Types.INTEGER); //  
				}
				
				if (app.get("24") != null) { // OIL_MEASURE 判断数据
					stUpdateLastTrackALine.setLong(13,Long.parseLong(app.get("24"))); // 油量（单位：L）
				}else{
					stUpdateLastTrackALine.setLong(13, -1); // 油量（单位：L）
				}
				if (app.get("24") != null) { // OIL_MEASURE
					stUpdateLastTrackALine.setLong(14,
							Long.parseLong(app.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackALine.setNull(14, Types.INTEGER); // 油量（单位：L）
				}

				if (app.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrackALine.setLong(15,
							Long.parseLong(app.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackALine.setNull(15, Types.INTEGER);
				}

				if (app.get("503") != null && !app.get("503").equals("")) { // E_TORQUE
					stUpdateLastTrackALine.setLong(16,
							Long.parseLong(app.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrackALine.setNull(16, Types.INTEGER);
				}

				if (app.get("216") != null) { // OIL_INSTANT
					stUpdateLastTrackALine.setLong(17,
							Long.parseLong(app.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackALine.setNull(17, Types.INTEGER);
				}

				if (app.get("507") != null) { // BATTERY_VOLTAGE
					stUpdateLastTrackALine.setLong(18,
							Long.parseLong(app.get("507"))); // 电池电压
				} else {
					stUpdateLastTrackALine.setNull(18, Types.INTEGER);
				}

				if (app.get("506") != null) { // EXT_VOLTAGE
					stUpdateLastTrackALine.setLong(19,
							Long.parseLong(app.get("506"))); // 外部电压
				} else {
					stUpdateLastTrackALine.setNull(19, Types.INTEGER);
				}

				if (app.get("509") != null && !app.get("509").equals("")) { // E_WATER_TEMP
					stUpdateLastTrackALine.setLong(20,
							Long.parseLong(app.get("509"))); // 冷却液温度
				} else {
					stUpdateLastTrackALine.setNull(20, Types.INTEGER);
				}
				if (!app.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrackALine.setDouble(21,
							Double.parseDouble(app.get(Constant.RATIO)));
				} else {
					stUpdateLastTrackALine.setNull(21, Types.DOUBLE);
				}

				if (!app.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrackALine.setInt(22,
							Integer.parseInt(app.get(Constant.GEARS)));
				} else {
					stUpdateLastTrackALine.setNull(22, Types.INTEGER);
				}

				if (app.get("7") != null) {// VEHICLE_SPEED
					stUpdateLastTrackALine.setInt(23,
							Integer.parseInt(app.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackALine.setNull(23, Types.INTEGER); // 脉冲车速
				}

				// 大气压力
				if (app.get("511") != null && !app.get("511").equals("")) {
					stUpdateLastTrackALine.setLong(24,
							Long.parseLong(app.get("511")));
				} else {
					stUpdateLastTrackALine.setNull(24, Types.INTEGER);
				}

				// 进气温度
				if (app.get("510") != null) {
					stUpdateLastTrackALine.setLong(25,
							Long.parseLong(app.get("510")));
				} else {
					stUpdateLastTrackALine.setNull(25, Types.INTEGER);
				}
				
				if (app.get("505") != null) {
					stUpdateLastTrackALine.setLong(26,
							Long.parseLong(app.get("505")));
				}else{
					stUpdateLastTrackALine.setLong(26, -1);
				}
				// 发动机运行总时长
				if (app.get("505") != null) {
					stUpdateLastTrackALine.setLong(27,
							Long.parseLong(app.get("505")));
				} else {
					stUpdateLastTrackALine.setNull(27, Types.INTEGER);
				}
				
				if (app.get("213") != null) {
					stUpdateLastTrackALine.setLong(28,
								Long.parseLong(app.get("213")));
				}else{
					stUpdateLastTrackALine.setLong(28, -1);
				}
				
				// 累计油耗
				if (app.get("213") != null) {
					stUpdateLastTrackALine.setLong(29,
							Long.parseLong(app.get("213")));
				} else {
					stUpdateLastTrackALine.setNull(29, Types.INTEGER);
				}

				// 油门踏板位置
				if (app.get("504") != null && !app.get("504").equals("")) {
					stUpdateLastTrackALine.setLong(30,
							Long.parseLong(app.get("504")));
				} else {
					stUpdateLastTrackALine.setNull(30, Types.INTEGER);
				}

				// 机油温度
				if (app.get("508") != null) {
					stUpdateLastTrackALine.setLong(31,
							Long.parseLong(app.get("508")));
				} else {
					stUpdateLastTrackALine.setNull(31, Types.INTEGER);
				}

				// 机油压力
				if (app.get("215") != null && !app.get("215").equals("")) {
					stUpdateLastTrackALine.setLong(32,
							Long.parseLong(app.get("215")));
				} else {
					stUpdateLastTrackALine.setNull(32, Types.INTEGER);
				}
				
				stUpdateLastTrackALine.setString(33, alarmCode);

				stUpdateLastTrackALine.setString(34, basestatus);
				stUpdateLastTrackALine.setString(35, extendstatus);
				stUpdateLastTrackALine.setString(36, msgid);
				stUpdateLastTrackALine.setString(37, app.get(Constant.SPEEDFROM)); // 车速来源
				
				//精准油耗
				if (null != app.get("219")) {
					stUpdateLastTrackALine.setLong(38,
							Long.parseLong(app.get("219")));
				} else {
					stUpdateLastTrackALine.setNull(38, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackALine.setInt(39, status);
				stUpdateLastTrackALine.setInt(40, acc);
				//判断锁车状态  TODO
				if(StringUtils.isNotBlank(app.get("570"))){
					stUpdateLastTrackALine.setString(41,app.get("570"));
				}else{
					stUpdateLastTrackALine.setNull(41,Types.NULL);
				}
				
				stUpdateLastTrackALine.setLong(42, vid);// VID
				stUpdateLastTrackALine.executeUpdate();
			}catch(NullPointerException ex ){
				logger.error("更新轨迹包带总线数据并带有报警出错,空指针异常:", ex);
				nulldata.trace(app.get(Constant.COMMAND));
			}catch (SQLException e) {
				logger.error("更新轨迹包带总线数据并带有报警出错", e);
				try{
					dbCon.getMetaData();
					if(stUpdateLastTrackALine == null){
						stUpdateLastTrackALine = createStatement(dbCon,commitTrackCount,updateLastTrackALine);
					}
				}catch(Exception ex){
					logger.error("更新轨迹包带总线数据并带有报警出错,重建连接异常:", ex);
					stUpdateLastTrackALine = recreateStatement(dbCon,commitTrackCount,updateLastTrackALine);
				}
			}
		} else {
			try {
				stUpdateLastTrackLine.setLong(1, lon);
				stUpdateLastTrackLine.setLong(2, lat);
				stUpdateLastTrackLine.setInt(3, gpsSpeed);
				if (app.get("9") != null) {
					stUpdateLastTrackLine.setLong(4,Long.parseLong(app.get("9")));
				}else{
					stUpdateLastTrackLine.setLong(4, -1);
				}
				if (app.get("9") != null) {
					stUpdateLastTrackLine.setLong(5, Long.parseLong(app.get("9")));
				} else {
					stUpdateLastTrackLine.setNull(5, Types.INTEGER);
				}
				stUpdateLastTrackLine.setInt(6, head);
				stUpdateLastTrackLine.setLong(7, utc);
				stUpdateLastTrackLine.setLong(8, CDate.getCurrentUtcMsDate());
				stUpdateLastTrackLine.setLong(9, mapLon);
				stUpdateLastTrackLine.setLong(10, mapLat);
				if (app.get("6") != null) {
					stUpdateLastTrackLine.setLong(11,
							Long.parseLong(app.get("6"))); //  
				} else {
					stUpdateLastTrackLine.setNull(11, Types.INTEGER);
				}
				
				if (app.get("24") != null) { // 判断数据
					stUpdateLastTrackLine.setLong(12,
							Long.parseLong(app.get("24"))); // 油量（单位：L）
				}else{
					stUpdateLastTrackLine.setLong(12, -1);
				}
				if (app.get("24") != null) {
					stUpdateLastTrackLine.setLong(13,
							Long.parseLong(app.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrackLine.setNull(13, Types.INTEGER);
				}
				if (app.get("210") != null) {
					stUpdateLastTrackLine.setLong(14,
							Long.parseLong(app.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackLine.setNull(14, Types.INTEGER);
				}
				if (app.get("503") != null && !app.get("503").equals("")) {
					stUpdateLastTrackLine.setLong(15,
							Long.parseLong(app.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrackLine.setNull(15, Types.INTEGER);
				}
				if (app.get("216") != null) {
					stUpdateLastTrackLine.setLong(16,
							Long.parseLong(app.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackLine.setNull(16, Types.INTEGER);
				}
				if (app.get("507") != null) {
					stUpdateLastTrackLine.setLong(17,
							Long.parseLong(app.get("507"))); // 电池电压
				} else {
					stUpdateLastTrackLine.setNull(17, Types.INTEGER);
				}
				if (app.get("506") != null) {
					stUpdateLastTrackLine.setLong(18,
							Long.parseLong(app.get("506"))); // 外部电压
				} else {
					stUpdateLastTrackLine.setNull(18, Types.INTEGER);
				}
				if (app.get("509") != null && !app.get("509").equals("")) {
					stUpdateLastTrackLine.setLong(19,
							Long.parseLong(app.get("509"))); // 冷却液温度
				} else {
					stUpdateLastTrackLine.setNull(19, Types.INTEGER);
				}
				if (!app.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrackLine.setDouble(20,
							Double.parseDouble(app.get(Constant.RATIO)));
				} else {
					stUpdateLastTrackLine.setNull(20, Types.DOUBLE);
				}

				if (!app.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrackLine.setInt(21,
							Integer.parseInt(app.get(Constant.GEARS)));
				} else {
					stUpdateLastTrackLine.setNull(21, Types.INTEGER);
				}

				if (app.get("7") != null) {
					stUpdateLastTrackLine.setInt(22,
							Integer.parseInt(app.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackLine.setNull(22, Types.INTEGER);
				}

				// 大气压力
				if (app.get("511") != null && !app.get("511").equals("")) {
					stUpdateLastTrackLine.setLong(23,
							Long.parseLong(app.get("511")));
				} else {
					stUpdateLastTrackLine.setNull(23, Types.INTEGER);
				}

				// 进气温度
				if (app.get("510") != null) {
					stUpdateLastTrackLine.setLong(24,
							Long.parseLong(app.get("510")));
				} else {
					stUpdateLastTrackLine.setNull(24, Types.INTEGER);
				}

				if (app.get("505") != null) {
					stUpdateLastTrackLine.setLong(25,
							Long.parseLong(app.get("505")));
				}else{
					stUpdateLastTrackLine.setLong(25, -1);
				}
				
				// 发动机运行总时长
				if (app.get("505") != null) {
					stUpdateLastTrackLine.setLong(26,
							Long.parseLong(app.get("505")));
				} else {
					stUpdateLastTrackLine.setNull(26, Types.INTEGER);
				}
				
				if (app.get("213") != null) {
					stUpdateLastTrackLine.setLong(27,
							Long.parseLong(app.get("213")));
				}else{
					stUpdateLastTrackLine.setLong(27, -1);
				}
				
				// 累计油耗
				if (app.get("213") != null) {
					stUpdateLastTrackLine.setLong(28,
							Long.parseLong(app.get("213")));
				} else {
					stUpdateLastTrackLine.setNull(28, Types.INTEGER);
				}

				// 油门踏板位置
				if (app.get("504") != null && !app.get("504").equals("")) {
					stUpdateLastTrackLine.setLong(29,
							Long.parseLong(app.get("504")));
				} else {
					stUpdateLastTrackLine.setNull(29, Types.INTEGER);
				}

				// 机油温度
				if (app.get("508") != null) {
					stUpdateLastTrackLine.setLong(30,
							Long.parseLong(app.get("508")));
				} else {
					stUpdateLastTrackLine.setNull(30, Types.INTEGER);
				}

				// 机油压力
				if (app.get("215") != null && !app.get("215").equals("")) {
					stUpdateLastTrackLine.setLong(31,
							Long.parseLong(app.get("215")));
				} else {
					stUpdateLastTrackLine.setNull(31, Types.INTEGER);
				}
				stUpdateLastTrackLine.setString(32, basestatus);
				stUpdateLastTrackLine.setString(33, extendstatus);
				stUpdateLastTrackLine.setString(34, msgid);
				stUpdateLastTrackLine.setString(35, app.get(Constant.SPEEDFROM)); //车速来源
				// 精准油耗
				if (null != app.get("219")) {
					stUpdateLastTrackLine.setLong(36,
							Long.parseLong(app.get("219")));
				} else {
					stUpdateLastTrackLine.setNull(36, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackLine.setInt(37, status);
				stUpdateLastTrackLine.setInt(38, acc);
				//判断锁车状态  TODO
				if(StringUtils.isNotBlank(app.get("570"))){
					stUpdateLastTrackLine.setString(39,app.get("570"));
				}else{
					stUpdateLastTrackLine.setNull(39,Types.NULL);
				}
				stUpdateLastTrackLine.setLong(40, vid);
				stUpdateLastTrackLine.executeUpdate();
			}catch(NullPointerException ex ){
				logger.error("更新轨迹包带总线数据并带有报警出错,空指针异常:", ex);
				nulldata.trace(app.get(Constant.COMMAND));
			}catch (SQLException e) {
				logger.error("更新轨迹包带总线数据出错", e);
				
				try{
					dbCon.getMetaData();
					if(stUpdateLastTrackLine == null){
						stUpdateLastTrackLine = createStatement(dbCon,commitTrackCount,updateLastTrackLine);
					}
				}catch(Exception ex){
					logger.error("更新轨迹包带总线数据并带有报警出错,重建连接异常:", ex);
					stUpdateLastTrackLine = recreateStatement(dbCon,commitTrackCount,updateLastTrackLine);
				}
			}
		}
	}

	/**
	 * 更新轨迹包最后位置到数据库
	 * 
	 * @param app
	 *            位置报文类
	 */
	public void updateLastTrack(Map<String, String> app){
		Integer gpsSpeed = 0;
		String speed = app.get("3");
		if(speed != null){
			gpsSpeed = Integer.parseInt(speed);
		}
		long lon = 0l;
		long lat = 0;
		String lonStr = app.get("1");
		String latStr = app.get("2");
		if(lonStr != null){
			lon = Long.parseLong(lonStr);
		}
		if(latStr != null){
			lat = Long.parseLong(latStr);
		}
		String vidStr = app.get(Constant.VID);
//		if(vidStr == null){
//			logger.error("updateLastTrack---VID==null:"+app.get(Constant.COMMAND)); 
//			return ;
//		}
		if(app.get("5")==null){
			logger.error("updateLastTrack---app.get(5)==null:"+app.get(Constant.COMMAND)); 
			return ;
		}
		String utcStr = app.get(Constant.UTC);
		if(utcStr == null){
			logger.error("updateLastTrack---UTC==null:"+app.get(Constant.COMMAND)); 
			return ;
		}
		long vid = Long.parseLong(vidStr);
		int head = Integer.parseInt(app.get("5"));
		long utc =  Long.parseLong(utcStr);
		long mapLon = Long.parseLong(app.get(Constant.MAPLON) == null ? "0" : app.get(Constant.MAPLON));
		long mapLat = Long.parseLong(app.get(Constant.MAPLAT) == null ? "0" : app.get(Constant.MAPLAT));
		String alarmCode = app.get("20");
		if(alarmCode!=null){
			if (app.get("21")!=null) {
				alarmCode = alarmCode.substring(0, alarmCode.length() - 1) + app.get("21");
			}else{
				alarmCode = alarmCode.replaceAll("\\,\\,", ",");
				
			}
		}
		String basestatus = app.get("8");// 基本状态
		String extendstatus = app.get("500");// 扩展状态
		String msgid=app.get(Constant.MSGID);
		
//		int i = alarmCode.length();
		if (alarmCode != null && alarmCode.length() > 0) {
			try {
				stUpdateLastTrackA.setLong(1, lon); // LON
				stUpdateLastTrackA.setLong(2, lat); // LAT
				stUpdateLastTrackA.setInt(3, gpsSpeed); // GPS_SPEED
				String mileage = app.get("9");
				if(StringUtils.isNumeric(mileage)){ 
					stUpdateLastTrackA.setLong(4, Long.parseLong(app.get("9"))); // MILEAGE
					stUpdateLastTrackA.setLong(5, Long.parseLong(app.get("9"))); // MILEAGE
				}else{
					stUpdateLastTrackA.setLong(4, -1); // MILEAGE
					stUpdateLastTrackA.setNull(5, Types.INTEGER); // MILEAGE
				}
				stUpdateLastTrackA.setInt(6, head); // DIRECTION
				stUpdateLastTrackA.setLong(7, utc); // UTC
				stUpdateLastTrackA.setLong(8, CDate.getCurrentUtcMsDate()); // SYSUTC
				stUpdateLastTrackA.setLong(9, mapLon); // MAPLON
				stUpdateLastTrackA.setLong(10, mapLat); // MAPLAT
				stUpdateLastTrackA.setLong(11, utc); // ALARM_UTC
				String elevation = app.get("6");
				if (StringUtils.isNumeric(elevation)) { // ELEVATION 
					stUpdateLastTrackA.setLong(12, Long.parseLong(elevation)); //  
				} else {
					stUpdateLastTrackA.setNull(12, Types.INTEGER); //  
				}
				String oil_measure = app.get("24");
				if (StringUtils.isNumeric(oil_measure)) { // OIL_MEASURE 判断数据
					stUpdateLastTrackA.setLong(13, Long.parseLong(oil_measure)); // 油量（单位：L）
					stUpdateLastTrackA.setLong(14, Long.parseLong(oil_measure)); // 油量（单位：L）
				}else{
					stUpdateLastTrackA.setLong(13, -1); // 油量（单位：L）
					stUpdateLastTrackA.setNull(14, Types.INTEGER); // 油量（单位：L）
				}
				if (!app.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrackA.setDouble(15,
							Double.parseDouble(app.get(Constant.RATIO)));
				} else {
					stUpdateLastTrackA.setNull(15, Types.DOUBLE);
				}

				if (!app.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrackA.setInt(16,
							Integer.parseInt(app.get(Constant.GEARS)));
				} else {
					stUpdateLastTrackA.setNull(16, Types.INTEGER);
				}
				if (app.get("7") != null) {// VEHICLE_SPEED
					stUpdateLastTrackA.setInt(17,
							Integer.parseInt(app.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrackA.setNull(17, Types.INTEGER); // 脉冲车速
				}
				if(!alarmCode.equals(",")){ 
					stUpdateLastTrackA.setString(18, alarmCode);
				}else{
					stUpdateLastTrackA.setNull(18, Types.NULL);
				}
				stUpdateLastTrackA.setString(19, basestatus);
				stUpdateLastTrackA.setString(20, extendstatus);
				if (app.get("213") != null) {
					stUpdateLastTrackA.setLong(21,
							Long.parseLong(app.get("213")));
				}else{
					stUpdateLastTrackA.setLong(21, -1);
				}
				// 累计油耗
				if (app.get("213") != null) {
					stUpdateLastTrackA.setLong(22,
							Long.parseLong(app.get("213")));
				} else {
					stUpdateLastTrackA.setNull(22, Types.INTEGER);
				}
				// 油门踏板位置
				if (StringUtils.isNumeric(app.get("504"))) { 
					stUpdateLastTrackA.setLong(23, Long.parseLong(app.get("504")));
				} else {
					stUpdateLastTrackA.setNull(23, Types.INTEGER);
				}

				if (app.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrackA.setLong(24,
							Long.parseLong(app.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrackA.setNull(24, Types.INTEGER);
				}

				if (app.get("216") != null && !app.get("216").equals("")) { // OIL_INSTANT
					stUpdateLastTrackA.setLong(25, Long.parseLong(app.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrackA.setNull(25, Types.INTEGER);
				}
				String e_torque = app.get("503");
				if (StringUtils.isNumeric(e_torque)) { // E_TORQUE 
					stUpdateLastTrackA.setLong(26, Long.parseLong(e_torque)); // 发动机扭矩
				} else {
					stUpdateLastTrackA.setNull(26, Types.INTEGER);
				}
				stUpdateLastTrackA.setString(27, msgid);// msid
				stUpdateLastTrackA.setString(28, app.get(Constant.SPEEDFROM));// 车速来源
				//精准油耗
				if (null != app.get("219")) {
					stUpdateLastTrackA.setLong(29, Long.parseLong(app.get("219")));
				} else {
					stUpdateLastTrackA.setNull(29, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrackA.setInt(30, status);
				stUpdateLastTrackA.setInt(31, acc);
				//判断锁车状态  TODO
				if(StringUtils.isNotBlank(app.get("570"))){
					stUpdateLastTrackA.setString(32,app.get("570"));
				}else{
					stUpdateLastTrackA.setNull(32,Types.NULL);
				}
				
				stUpdateLastTrackA.setLong(33, vid);// VID
				stUpdateLastTrackA.executeUpdate();
			}catch(NullPointerException ex ){
				nulldata.error("更新轨迹包带报警空指针异常:"+app.get(Constant.COMMAND));
				logger.error("更新轨迹包带报警空指针异常:", ex);
			}catch (SQLException e) {
				logger.error("更新轨迹包带报警出错", e);
				try{
					dbCon.getMetaData();
					if(stUpdateLastTrackA == null){
						stUpdateLastTrackA = createStatement(dbCon,commitTrackCount,updateLastTrackA);
					}
				}catch(Exception ex){
					logger.error("更新轨迹包带报警空指针异常,重建连接异常:", ex);
					stUpdateLastTrackA = recreateStatement(dbCon,commitTrackCount,updateLastTrackA);
				}
			}
		} else {
			try {
				
				
				stUpdateLastTrack.setLong(1, lon);
				stUpdateLastTrack.setLong(2, lat);
				stUpdateLastTrack.setInt(3, gpsSpeed);
				if (app.get("9") != null) {
					stUpdateLastTrack.setLong(4, Long.parseLong(app.get("9")));
				}else{
					stUpdateLastTrack.setLong(4, -1);
				}
				if (app.get("9") != null) {
					stUpdateLastTrack.setLong(5, Long.parseLong(app.get("9")));
				} else {
					stUpdateLastTrack.setNull(5, Types.INTEGER);
				}
				stUpdateLastTrack.setInt(6, head);
				stUpdateLastTrack.setLong(7, utc);
				stUpdateLastTrack.setLong(8, CDate.getCurrentUtcMsDate()); //SYSUTC
				stUpdateLastTrack.setLong(9, mapLon);
				stUpdateLastTrack.setLong(10, mapLat);
				if (app.get("6") != null) {
					stUpdateLastTrack.setLong(11,
							Long.parseLong(app.get("6"))); // 
				} else {
					stUpdateLastTrack.setNull(11, Types.INTEGER);
				}
				
				if (app.get("24") != null) { // 判断数据
					stUpdateLastTrack.setLong(12,
							Long.parseLong(app.get("24"))); // 油量（单位：L）
				}else{
					stUpdateLastTrack.setLong(12, -1);
				}
				if (app.get("24") != null) {
					stUpdateLastTrack.setLong(13,
							Long.parseLong(app.get("24"))); // 油量（单位：L）
				} else {
					stUpdateLastTrack.setNull(13, Types.INTEGER);
				}

				if (!app.get(Constant.RATIO).equals("-100")) {// 速比
					stUpdateLastTrack.setDouble(14,
							Double.parseDouble(app.get(Constant.RATIO)));
				} else {
					stUpdateLastTrack.setNull(14, Types.DOUBLE);
				}

				if (!app.get(Constant.GEARS).equals("-100")) {// 档位rob
					stUpdateLastTrack.setInt(15,
							Integer.parseInt(app.get(Constant.GEARS)));
				} else {
					stUpdateLastTrack.setNull(15, Types.INTEGER);
				}

				if (app.get("7") != null) {
					stUpdateLastTrack
							.setInt(16, Integer.parseInt(app.get("7"))); // 脉冲车速
				} else {
					stUpdateLastTrack.setNull(16, Types.INTEGER);
				}

				stUpdateLastTrack.setString(17, basestatus);
				stUpdateLastTrack.setString(18, extendstatus);
				
				if (app.get("213") != null) {
					stUpdateLastTrack.setLong(19,Long.parseLong(app.get("213")));
				}else{
					stUpdateLastTrack.setLong(19, -1);
				}
				
				// 累计油耗
				if (app.get("213") != null) {
					stUpdateLastTrack.setLong(20,
							Long.parseLong(app.get("213")));
				} else {
					stUpdateLastTrack.setNull(20, Types.INTEGER);
				}
				// 油门踏板位置
				if (app.get("504") != null && !app.get("504").equals("")) {
					stUpdateLastTrack.setLong(21,
							Long.parseLong(app.get("504")));
				} else {
					stUpdateLastTrack.setNull(21, Types.INTEGER);
				}

				if (app.get("210") != null) {// ENGINE_ROTATE_SPEED
					stUpdateLastTrack.setLong(22,
							Long.parseLong(app.get("210"))); // 发动机转速
				} else {
					stUpdateLastTrack.setNull(22, Types.INTEGER);
				}

				if (app.get("216") != null) { // OIL_INSTANT
					stUpdateLastTrack.setLong(23,
							Long.parseLong(app.get("216"))); // 瞬时油耗
				} else {
					stUpdateLastTrack.setNull(23, Types.INTEGER);
				}
				if (app.get("503") != null && !app.get("503").equals("")) { // E_TORQUE
					stUpdateLastTrack.setLong(24,
							Long.parseLong(app.get("503"))); // 发动机扭矩
				} else {
					stUpdateLastTrack.setNull(24, Types.INTEGER);
				}
				stUpdateLastTrack.setString(25, msgid);
				stUpdateLastTrack.setString(26, app.get(Constant.SPEEDFROM)); // 车速来源
				//精准油耗
				if (null != app.get("219")) {
					stUpdateLastTrack.setLong(27,
							Long.parseLong(app.get("219")));
				} else {
					stUpdateLastTrack.setNull(27, Types.INTEGER);
				}
				int status = Tools.getPositioning(basestatus);
				int acc = Tools.getACCStatus(basestatus);
				stUpdateLastTrack.setInt(28, status);
				stUpdateLastTrack.setInt(29, acc);
				//判断锁车状态  TODO
				if(StringUtils.isNotBlank(app.get("570"))){
					stUpdateLastTrack.setString(30,app.get("570"));
				}else{
					stUpdateLastTrack.setNull(30,Types.NULL);
				}
				
				stUpdateLastTrack.setLong(31, vid);
				stUpdateLastTrack.executeUpdate();

			}catch(NullPointerException ex ){
				nulldata.trace(app.get(Constant.COMMAND));
				logger.error("更新轨迹包带报警出错,空指针异常:", ex);
			}catch (SQLException e) {
				logger.error("更新轨迹包带报警出错", e);
				try{
					dbCon.getMetaData();
					if(stUpdateLastTrack == null){
						stUpdateLastTrack = createStatement(dbCon,commitTrackCount,updateLastTrack);
					}
				}catch(Exception ex){
					logger.error("更新轨迹包带报警出错,重连异常:", ex);
					stUpdateLastTrack = recreateStatement(dbCon,commitTrackCount,updateLastTrack);
				}
			}
		}
	}

	/**
	 * 报警包存入数据库
	 * 
	 * @param app
	 *            位置报文类
	 */
	public void saveAlarmTrack(Map<String, String> app, String alarmCode,String key,Long spd){
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----存储报警,报警类型:"+alarmCode+",报警ID:"+key); 
		if (alarmCode == null || alarmCode.equals("")) {
			return;
		}
		String basestatus = app.get("8");// 基本状态
		String extendstatus = app.get("500");// 扩展状态
		String alarmAdd = app.get("32");
//		int spd = Utils.getSpeed(app.get(Constant.SPEEDFROM),app);
		
		try {
			stSaveAlarmTrack.setString(1, key);
			stSaveAlarmTrack.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveAlarmTrack.setLong(3, Long.parseLong(app.get(Constant.UTC)));
			stSaveAlarmTrack.setLong(4, Long.parseLong(app.get("2")));
			stSaveAlarmTrack.setLong(5, Long.parseLong(app.get("1")));
			stSaveAlarmTrack.setLong(6,
					Long.parseLong(app.get(Constant.MAPLON)));
			stSaveAlarmTrack.setLong(7,
					Long.parseLong(app.get(Constant.MAPLAT)));
			if (app.get("6") != null) {
				stSaveAlarmTrack.setLong(8, Long.parseLong(app.get("6")));
			} else {
				stSaveAlarmTrack.setNull(8, Types.INTEGER);
			}
			stSaveAlarmTrack.setInt(9, Integer.parseInt(app.get("5")));
			stSaveAlarmTrack.setLong(10, spd);
			if (app.get("9") != null) {
				stSaveAlarmTrack.setLong(11, Long.parseLong(app.get("9")));
			} else {
				stSaveAlarmTrack.setNull(11, Types.INTEGER);
			}
			if (app.get("213") != null) {
				stSaveAlarmTrack.setLong(12, Long.parseLong(app.get("213")));
			} else {
				stSaveAlarmTrack.setNull(12, Types.INTEGER);
			}
			stSaveAlarmTrack.setString(13, alarmCode);
			stSaveAlarmTrack.setLong(14, System.currentTimeMillis());
			stSaveAlarmTrack.setInt(15, 1);
			stSaveAlarmTrack.setLong(16, Long.parseLong(app.get(Constant.UTC)));
			stSaveAlarmTrack.setString(17, "");
			stSaveAlarmTrack.setString(18, "");
			AlarmTypeBean alarmTypeBean = TempMemory
					.getAlarmtypeMapValue(Integer.parseInt(alarmCode));
			if (alarmTypeBean != null) {
				stSaveAlarmTrack.setString(19, alarmTypeBean.getParentCode());
			} else {
				stSaveAlarmTrack.setNull(19, Types.NULL);
			}
			if(basestatus != null){
				stSaveAlarmTrack.setString(20, basestatus);
			}else{
				stSaveAlarmTrack.setNull(20, Types.NULL);
			}
			if(extendstatus != null){
				stSaveAlarmTrack.setString(21, extendstatus);
			}else{
				stSaveAlarmTrack.setNull(21, Types.NULL);
			}
			
			if(alarmAdd != null){
				stSaveAlarmTrack.setString(22, alarmAdd);
			}else{
				stSaveAlarmTrack.setNull(22, Types.NULL);
			}
			stSaveAlarmTrack.executeUpdate();
		} catch (SQLException e) {
			logger.error("ORACLE存储报警出错,"+ e.getMessage());
			try{
				dbAlarmCon.getMetaData();
				if(stSaveAlarmTrack == null){
					stSaveAlarmTrack = createStatement(dbAlarmCon,commitAlarmCount,saveAlarmTrack);
				}
			}catch(Exception ex){
				stSaveAlarmTrack = recreateStatement(dbAlarmCon,commitAlarmCount,saveAlarmTrack);
			}
		}
	}

	/**
	 * 更新最新报警结束时间
	 * 
	 * @param gpsTime
	 * @param vid
	 * @throws SQLException
	 *             UPDATE TH_VEHICLE_ALARM SET ALARM_END_UTC =
	 *             ?,END_LAT=?,END_LON
	 *             =?,END_MAPLAT=?,END_ELEVATION=?,END_DIRECTION
	 *             =?,END_GPS_SPEED=?,END_MILEAGE=?,END_OIL_TOTAL=?
	 *             ALARM_STATUS=0,MAX_RPM=? WHERE VID = ? AND ALARM_END_UTC is null AND
	 *             ALARM_CODE = ?
	 */
	public void updateAlarmEndTime(Map<String, String> app, String alarmCode,String alarmid,Long spd){
//		logger.debug("-------------alarmcode----"+app.get(Constant.COMMDR)+"-----更新报警,报警类型:"+alarmCode+",报警ID:"+alarmid); 
		try {
			String alarmAdd = app.get("32");
//			int spd = Utils.getSpeed(app.get(Constant.SPEEDFROM),app);
			stUpdateAlarmTrack.setLong(1, Long.parseLong(app.get(Constant.UTC))); // ALARM_END_UTC
			stUpdateAlarmTrack.setLong(2, Long.parseLong(app.get("2")));// END_LAT
			stUpdateAlarmTrack.setLong(3, Long.parseLong(app.get("1")));// END_LON

			stUpdateAlarmTrack.setLong(4,
					Long.parseLong(app.get(Constant.MAPLAT)));// END_MAPLAT
			stUpdateAlarmTrack.setLong(5,
					Long.parseLong(app.get(Constant.MAPLON)));
			if (app.get("6") != null) {
				stUpdateAlarmTrack.setLong(6, Long.parseLong(app.get("6")));
			} else {
				stUpdateAlarmTrack.setNull(6, Types.INTEGER);
			}
			stUpdateAlarmTrack.setInt(7, Integer.parseInt(app.get("5")));
			stUpdateAlarmTrack.setLong(8, spd);
			if (app.get("9") != null) {
				stUpdateAlarmTrack.setLong(9, Long.parseLong(app.get("9")));
			} else {
				stUpdateAlarmTrack.setNull(9, Types.INTEGER);
			}
			if (app.get("213") != null) {
				stUpdateAlarmTrack.setLong(10, Long.parseLong(app.get("213")));
			} else {
				stUpdateAlarmTrack.setNull(10, Types.INTEGER);
			}
			if(alarmAdd != null){
				stUpdateAlarmTrack.setString(11, alarmAdd);
			}else{
				stUpdateAlarmTrack.setNull(11, Types.NULL);
			}
			
			if (app.get(Constant.MAXRPM)!=null&&!"".equals(app.get(Constant.MAXRPM))){
				stUpdateAlarmTrack.setLong(12, Long.parseLong(app.get(Constant.MAXRPM)));
			}else{
				stUpdateAlarmTrack.setNull(12, Types.NULL);
			}
			
			stUpdateAlarmTrack.setString(13, alarmid);
			
			stUpdateAlarmTrack.executeUpdate();
		} catch (SQLException e) {
			logger.error("ORACLE更新报警出错,", e);
			try{
				dbAlarmCon.getMetaData();
				if(stUpdateAlarmTrack == null){
					stUpdateAlarmTrack = createStatement(dbAlarmCon,commitAlarmCount,sql_updateAlarmTrack);
				}
			}catch(Exception ex){
				stUpdateAlarmTrack = recreateStatement(dbAlarmCon,commitAlarmCount,sql_updateAlarmTrack);
			}
		}
	}

	/**
	 * 非法轨迹包存入数据库
	 * 
	 * @param app
	 *            位置报文类 INSERT INTO TH_VEHICLE_DUMP
	 *            (AUTO_ID,VID,ISVALID,SYSUTC,COMMAND)
	 *            VALUES(SEQ_DUMP_ID.NEXTVAL,?,?,?,?)
	 */
	public void saveDumpTrack(Map<String, String> app, int isPValid){
		try {
			stSaveDumpTrack.setLong(1, Long.parseLong(app.get(Constant.VID)));
			stSaveDumpTrack.setInt(2, isPValid);
			stSaveDumpTrack.setLong(3, System.currentTimeMillis());
			stSaveDumpTrack.setString(4, app.get(Constant.COMMAND));
			stSaveDumpTrack.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储非法轨迹", e);
			try{
				saveDumpTrackConn.getMetaData();
				if(stSaveDumpTrack == null){
					stSaveDumpTrack = createStatement(saveDumpTrackConn,1,saveDumpTrack);
				}
			}catch(Exception ex){
				logger.error("存储非法轨迹重连:", e);
				stSaveDumpTrack = recreateStatement(saveDumpTrackConn,1,saveDumpTrack);
			}
		}
	}

	/**
	 * 根据车辆ID更新车辆总线最新状态
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void updateVehicleLineStatus(EquipmentStatus app){
		try {
			stUpdateVehicleLineStatus.setLong(1, System.currentTimeMillis());

			stUpdateVehicleLineStatus.setInt(2, app.getTerminalStatus());
			if (app.getTerminalValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(3, app.getTerminalValue());
			} else {
				stUpdateVehicleLineStatus.setNull(3, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(4, app.getGpsStatusStatus());
			if (app.getGpsValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(5, app.getGpsValue());
			} else {
				stUpdateVehicleLineStatus.setNull(5, Types.DOUBLE);
			}
			
			stUpdateVehicleLineStatus.setInt(6, app.geteWaterStatus()); // 冷却液温度 比较值
			stUpdateVehicleLineStatus.setInt(7, app.geteWaterStatus()); // 冷却液温度 更新值
			
			stUpdateVehicleLineStatus.setDouble(8, app.geteWaterValue());
			
			if(app.geteWaterValue() == -2){
				stUpdateVehicleLineStatus.setDouble(9, -1); // 将值置为无效
			}else{
				stUpdateVehicleLineStatus.setDouble(9, app.geteWaterValue());
			}
			
			 // 蓄电池电压比较值
			stUpdateVehicleLineStatus.setInt(10, app.getExtVoltageStatus());
			stUpdateVehicleLineStatus.setInt(11, app.getExtVoltageStatus()); // 蓄电池电压更新值
			
			stUpdateVehicleLineStatus.setDouble(12, app.getExtVoltageValue());
			
			if(app.getExtVoltageValue() == -2){
				stUpdateVehicleLineStatus.setDouble(13, -1);
			}else{
				stUpdateVehicleLineStatus.setDouble(13, app.getExtVoltageValue());
			}

			stUpdateVehicleLineStatus.setInt(14, app.getOilPressureStatus());
			
			if (app.getOilPressureValue() != -1 ) {
				stUpdateVehicleLineStatus.setDouble(15,app.getOilPressureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(15, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(16, app.getBrakePressureStatus());
			if (app.getBrakePressureValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(17,
						app.getBrakePressureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(17, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(18, app.getBrakepadFrayStatus());
			if (app.getBrakepadFrayValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(19,
						app.getBrakepadFrayValue());
			} else {
				stUpdateVehicleLineStatus.setNull(19, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(20, app.getOilAramStatus());
			if (app.getOilAramValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(21, app.getOilAramValue());
			} else {
				stUpdateVehicleLineStatus.setNull(21, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(22, app.getAbsBugStatus());
			if (app.getAbsBugValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(23, app.getAbsBugValue());
			} else {
				stUpdateVehicleLineStatus.setNull(23, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(24, app.getCoolantLevelStatus());
			if (app.getCoolantLevelValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(25,
						app.getCoolantLevelValue());
			} else {
				stUpdateVehicleLineStatus.setNull(25, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(26, app.getAirFilterStatus());
			if (app.getAirFilterValue() != -1) {
				stUpdateVehicleLineStatus
						.setDouble(27, app.getAirFilterValue());
			} else {
				stUpdateVehicleLineStatus.setNull(27, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(28, app.getMwereBlockingStatus());
			if (app.getMwereBlockingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(29,
						app.getMwereBlockingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(29, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(30, app.getFuelBlockingStatus());
			if (app.getFuelBlockingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(31,
						app.getFuelBlockingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(31, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus
					.setInt(32, app.getEoilTemperatureStatus()); // 机油温度
			if (app.getEoilTemperatureValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(33,
						app.getEoilTemperatureValue());
			} else {
				stUpdateVehicleLineStatus.setNull(33, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(34, app.getRetarerHtStatus());
			if (app.getRetarerHtValue() != -1) {
				stUpdateVehicleLineStatus
						.setDouble(35, app.getRetarerHtValue());
			} else {
				stUpdateVehicleLineStatus.setNull(35, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(36, app.getEhousingStatus());
			if (app.getEhousingValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(37, app.getEhousingValue());
			} else {
				stUpdateVehicleLineStatus.setNull(37, Types.DOUBLE);
			}

			// 整车状态（根据前边所有状态进行判断，只要有一个状态为红色，此处就标记红色，全部为绿色时此处标记为绿色，其他情况标记为灰色）(0
			// 绿灯 1红灯 2 灰灯)
			if (app.getTerminalStatus() == 0 
					&& app.getGpsStatusStatus() == 0
					&& app.geteWaterStatus() == 0
					&& app.getExtVoltageStatus() == 0
					&& app.getOilPressureStatus() == 0
					&& app.getBrakePressureStatus() == 0
					&& app.getBrakepadFrayStatus() == 0
					&& app.getOilAramStatus() == 0
					&& app.getAbsBugStatus() == 0
					&& app.getCoolantLevelStatus() == 0
					&& app.getAirFilterStatus() == 0
					&& app.getMwereBlockingStatus() == 0
					&& app.getFuelBlockingStatus() == 0
					&& app.getEoilTemperatureStatus() == 0
					&& app.getRetarerHtStatus() == 0
					&& app.getEhousingStatus() == 0
					&& app.getAirPressureStatus() == 0
					
					&& app.getGpsFaultStatus() ==0
					&& app.getGpsOpenciruitStatus() == 0
					&& app.getGpsShortciruitStatus() == 0
					&& app.getTerminalUnderVoltageStatus() == 0
					&& app.getTerminalPowerDownStatus() == 0
					&& app.getTerminalScreenfalutStatus() == 0
					&& app.getTtsFaultStatus() == 0
					&& app.getCameraFaultStatus() == 0
					) {
				stUpdateVehicleLineStatus.setInt(38, 0);
			} else if (app.getTerminalStatus() == 2
					&& app.getGpsStatusStatus() == 2
					&& app.geteWaterStatus() == 2
					&& app.getExtVoltageStatus() == 2
					&& app.getOilPressureStatus() == 2
					&& app.getBrakePressureStatus() == 2
					&& app.getBrakepadFrayStatus() == 2
					&& app.getOilAramStatus() == 2
					&& app.getAbsBugStatus() == 2
					&& app.getCoolantLevelStatus() == 2
					&& app.getAirFilterStatus() == 2
					&& app.getMwereBlockingStatus() == 2
					&& app.getFuelBlockingStatus() == 2
					&& app.getEoilTemperatureStatus() == 2
					&& app.getRetarerHtStatus() == 2
					&& app.getEhousingStatus() == 2
					&& app.getAirPressureStatus() == 2
					
					&& app.getGpsFaultStatus() ==2
					&& app.getGpsOpenciruitStatus() == 2
					&& app.getGpsShortciruitStatus() == 2
					&& app.getTerminalUnderVoltageStatus() == 2
					&& app.getTerminalPowerDownStatus() == 2
					&& app.getTerminalScreenfalutStatus() == 2
					&& app.getTtsFaultStatus() == 2
					&& app.getCameraFaultStatus() == 2) {
				stUpdateVehicleLineStatus.setInt(38, 2);
			} else {
				stUpdateVehicleLineStatus.setInt(38, 1);
			}
			
			stUpdateVehicleLineStatus.setInt(39, app.getAirPressureStatus()); // 大气压力比较值
			stUpdateVehicleLineStatus.setInt(40, app.getAirPressureStatus()); // 大气压力更新值

			
			stUpdateVehicleLineStatus.setDouble(41,app.getAirPressureValue());
	
			if(app.getAirPressureValue() == -2){
				stUpdateVehicleLineStatus.setDouble(42,-1);
			}else{
				stUpdateVehicleLineStatus.setDouble(42,app.getAirPressureValue());
			}
			
			stUpdateVehicleLineStatus.setInt(43, app.getGpsFaultStatus());
			if (app.getGpsFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(44, app.getGpsFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(44, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(45, app.getGpsOpenciruitStatus());
			if (app.getGpsOpenciruitValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(46,
						app.getGpsOpenciruitValue());
			} else {
				stUpdateVehicleLineStatus.setNull(46, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(47, app.getGpsShortciruitStatus());
			if (app.getGpsShortciruitValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(48,
						app.getGpsShortciruitValue());
			} else {
				stUpdateVehicleLineStatus.setNull(48, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(49,
					app.getTerminalUnderVoltageStatus());
			if (app.getTerminalUnderVoltageValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(50,
						app.getTerminalUnderVoltageValue());
			} else {
				stUpdateVehicleLineStatus.setNull(50, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(51,
					app.getTerminalPowerDownStatus());
			if (app.getTerminalPowerDownValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(52,
						app.getTerminalPowerDownValue());
			} else {
				stUpdateVehicleLineStatus.setNull(52, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(53,
					app.getTerminalScreenfalutStatus());
			if (app.getTerminalScreenfalutValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(54,
						app.getTerminalScreenfalutValue());
			} else {
				stUpdateVehicleLineStatus.setNull(54, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(55, app.getTtsFaultStatus());
			if (app.getTtsFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(56, app.getTtsFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(56, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setInt(57, app.getCameraFaultStatus());
			if (app.getCameraFaultValue() != -1) {
				stUpdateVehicleLineStatus.setDouble(58,
						app.getCameraFaultValue());
			} else {
				stUpdateVehicleLineStatus.setNull(58, Types.DOUBLE);
			}

			stUpdateVehicleLineStatus.setLong(59, app.getVid());
			stUpdateVehicleLineStatus.executeUpdate();
		} catch (SQLException e) {
			logger.error("提交安全设备状态出错.", e);
			try{
				updateStatusConn.getMetaData();
				if(stUpdateVehicleLineStatus == null){
					stUpdateVehicleLineStatus = createStatement(updateStatusConn,commitEqCount,updateVehicleLineStatus);
				}
			}catch(Exception ex){
				logger.error("提交安全设备状态出错异常:", ex);
				stUpdateVehicleLineStatus = recreateStatement(updateStatusConn,commitEqCount,updateVehicleLineStatus);
			}
		}
	}

	/***
	 * 更新轨迹表状态
	 * 
	 * @param isValid
	 * @param utc
	 * @param vid
	 * @throws SQLException
	 */
	public void updateLastTrackISonLine(int isValid, long vid,String msgid){
		try {
//			stUpdateLastTrackISonLine.setInt(1, isValid);
//			stUpdateLastTrackISonLine.setString(2, msgid);
			stUpdateLastTrackISonLine.setLong(1, System.currentTimeMillis());
			stUpdateLastTrackISonLine.setLong(2, vid);
			stUpdateLastTrackISonLine.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新轨迹表状态", e);
			try{
				dbCon.getMetaData();
				if(stUpdateLastTrackISonLine == null){
					stUpdateLastTrackISonLine = createStatement(dbCon,1,sql_UpdateLastTrackISonLine);
				}
			}catch(Exception ex){
				logger.error("更新轨迹表状态重连", ex);
				stUpdateLastTrackISonLine = recreateStatement(dbCon,1,sql_UpdateLastTrackISonLine);
			}
		}
	}

	/***
	 * 更新车辆上下线状态值
	 * 
	 * @param isOnline
	 * @param vid
	 * @throws SQLException
	 */
	public void oracleUpdateIsonline(Map<String, String> packet)
			throws SQLException {
		OracleConnection dbCon = null;
		try {
			String parm = packet.get("18");
			String parms[] = parm.split("/");
			Long vid = Long.parseLong(packet.get(Constant.VID));
			String msgid = packet.get(Constant.MSGID);
			int status = Tools.getPositioning(packet.get("8"));
			int acc = Tools.getACCStatus(packet.get("8"));
			if (parms.length == 4) {
				// 从连接池获得连接
				
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stUpdateTrackISonLine = (OraclePreparedStatement)dbCon.prepareStatement(sql_updatIsOnlineLastTrack);
				stUpdateTrackISonLine.setExecuteBatch(1);
				stUpdateTrackISonLine.setInt(1, Integer.parseInt(parms[0]));
				stUpdateTrackISonLine.setLong(2, CDate.getCurrentUtcMsDate());
				stUpdateTrackISonLine.setString(3, msgid);
				stUpdateTrackISonLine.setInt(4, status);
				stUpdateTrackISonLine.setInt(5, acc);
				stUpdateTrackISonLine.setLong(6, vid);
				stUpdateTrackISonLine.executeUpdate();
			}
		}catch(Exception ee){
			logger.error(" 更新车辆上下线状态值异常:",ee);
		}finally {
			if (stUpdateTrackISonLine != null) {
				stUpdateTrackISonLine.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
}

