package com.ctfo.savecenter.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.HashMap;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.AlarmTypeBean;
import com.ctfo.savecenter.util.CDate;
import com.lingtu.xmlconf.XmlConf;
import com.mysql.jdbc.exceptions.MySQLNonTransientConnectionException;
 

public class TrackManagerKcptMysqlDBAdapter {
	private static final Logger logger = LoggerFactory.getLogger(TrackManagerKcptMysqlDBAdapter.class);
	
	//记录存在空值
	private static final Logger nulldata = LoggerFactory.getLogger("nulldata");

	private Connection mysqldbCon = null;
	
	private Connection mysqlAlarmdbCon = null;
	
	/** 更新轨迹带总线数据最后位置 */
	private PreparedStatement mysqlstUpdateLastTrackLine;

	/** 更新轨迹带总线数据最后位置报警 */
	private PreparedStatement mysqlstUpdateLastTrackALine;

	/** 更新轨迹最后位置 */
	private PreparedStatement mysqlstUpdateLastTrack;

	/** 更新轨迹最后位置报警 */
	private PreparedStatement mysqlstUpdateLastTrackA;

	/** 纪录报警报文 */
	private PreparedStatement mysqlstSaveAlarmTrack;

	/*** 更新报警包结束时间 */
	private PreparedStatement mysqlstUpdateAlarmTrack;

	/*** 更新最新位置表在线状态及数据是否有效状态信息 ***/
	private PreparedStatement stUpdateLastTrackISonLine;

	/*** 更新最新位置表在线状态 ***/
	private PreparedStatement stUpdateTrackIsonline;
	
	private String queryVehicleStatusMysql="";
	
	private String updateLastTrackAMysql = "";

	private String updateLastTrackMysql = "";

	private String updateLastTrackALineMysql = "";

	private String updateLastTrackLineMysql = "";

	// 存储合法报警报文
	private String sql_saveAlarmTrack_Mysql = "";

	// 更新报警包结束时间
	private String sql_updateAlarmTrack_Mysql = "";

	private String sql_UpdateLastTrackISonLine = "";
	
	//更新最新位置表车辆状态信息
	private String updateVehicleisonline = null;

	private int alarmTrackCount = 0;

	private int trackCount = 0;

	private int alarmTrackLineCount = 0;

	private int trackLineCount = 0;

	private int alarmCount = 0;

	private int alarmUpdateCount = 0;

	// 轨迹批量数据库提交数
	private int commitTrackCount = 0;

	// 报警批量数据库提交数
	private int commitAlarmCount = 0;
	
	private int threadId = 0;

	/**
	 * 构造函数
	 * 
	 * @param dbDriver
	 *            数据库驱动
	 * @param dbConString
	 *            数据库连接字
	 * @param dbUserName
	 *            数据库用户名
	 * @param dbPassword
	 *            数据库密码
	 * @param reconnectWait
	 *            数据库断线重新连接时间(秒)
	 */
	public void initDBAdapter(XmlConf config, String nodeName,int threadId) throws Exception {
		this.threadId = threadId;
		// 轨迹包更新最后位置到数据库
		updateLastTrackLineMysql = config.getStringValue(nodeName
				+ "|mysql_sql_updateLastTrackLine");

		// 轨迹包更新最后位置到数据库
		updateLastTrackALineMysql = config.getStringValue(nodeName
				+ "|mysql_sql_updateLastTrackALine");

		// 轨迹包更新最后位置到数据库
		updateLastTrackMysql = config.getStringValue(nodeName
				+ "|mysql_sql_updateLastTrack");

		// 轨迹包更新最后位置到数据库
		updateLastTrackAMysql = config.getStringValue(nodeName
				+ "|mysql_sql_updateLastTrackA");

		// 存储合法报警报文
		sql_saveAlarmTrack_Mysql = config.getStringValue(nodeName
				+ "|mysql_sql_saveAlarmTrack");

		// 更新报警包结束时间
		sql_updateAlarmTrack_Mysql = config.getStringValue(nodeName
				+ "|mysql_sql_updateAlarmTrack");

		sql_UpdateLastTrackISonLine = config.getStringValue(nodeName
				+ "|mysql_sql_updateLastTrackISonLine");
		
		queryVehicleStatusMysql = config.getStringValue(nodeName
				+ "|mysql_sql_queryVehicleStatus");

		// 更新MYSQL最新位置表
		updateVehicleisonline = config.getStringValue(nodeName + "|mysql_sql_updateVehicleisonline");
		
		// 轨迹批量数据库提交数
		commitTrackCount = config.getIntValue(nodeName + "|commitTrackCount");

		// 报警批量数据库提交数
		commitAlarmCount = config.getIntValue(nodeName + "|commitAlarmCount");

	}

	public void createPreparedStatement() {
		try {
//			mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//			mysqlstUpdateLastTrackLine = mysqldbCon.prepareStatement(updateLastTrackLineMysql);
//			mysqlstUpdateLastTrackALine = mysqldbCon.prepareStatement(updateLastTrackALineMysql);
//
//			mysqlstUpdateLastTrack = mysqldbCon.prepareStatement(updateLastTrackMysql);
//			mysqlstUpdateLastTrackA = mysqldbCon.prepareStatement(updateLastTrackAMysql);
//			
//			stUpdateLastTrackISonLine = mysqldbCon.prepareStatement(sql_UpdateLastTrackISonLine);
			
			mysqlAlarmdbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			mysqlstSaveAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_saveAlarmTrack_Mysql);
			mysqlstUpdateAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_updateAlarmTrack_Mysql);
			
			
		} catch (SQLException e) {
			logger.error("MYSQL初始化statement出错." + e.getMessage());
		}
	}

	public void commit() throws SQLException {
//		try{
//			if(trackLineCount > 0 ){
//				mysqlstUpdateLastTrackLine.executeBatch();
//				trackLineCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqldbCon.getMetaData();
//				mysqlstUpdateLastTrackLine = mysqldbCon.prepareStatement(updateLastTrackLineMysql);
//			} catch (SQLException e) {
//				if(mysqlstUpdateLastTrackLine != null){
//					mysqlstUpdateLastTrackLine.close();
//				}
//				
//				if(mysqldbCon != null){
//					mysqldbCon.close();
//					mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//				}
//				mysqlstUpdateLastTrackLine = mysqldbCon.prepareStatement(updateLastTrackLineMysql);
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//		}
//		
//		try{
//			if(alarmTrackLineCount > 0){
//				mysqlstUpdateLastTrackALine.executeBatch();
//				alarmTrackLineCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqldbCon.getMetaData();
//				mysqlstUpdateLastTrackALine = mysqldbCon.prepareStatement(updateLastTrackALineMysql);
//			} catch (SQLException e) {
//				if(mysqlstUpdateLastTrackALine != null){
//					mysqlstUpdateLastTrackALine.close();
//				}
//					if(mysqldbCon != null){
//						mysqldbCon.close();
//						mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//					}
//					mysqlstUpdateLastTrackALine = mysqldbCon.prepareStatement(updateLastTrackALineMysql);
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//		}
//		
//		try{
//			if(alarmTrackCount > 0){
//				mysqlstUpdateLastTrackA.executeBatch();
//				
//				alarmTrackCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqldbCon.getMetaData();
//				mysqlstUpdateLastTrackA = mysqldbCon.prepareStatement(updateLastTrackAMysql);
//			} catch (SQLException e) {
//				if(mysqlstUpdateLastTrackA  != null){
//					mysqlstUpdateLastTrackA.close();
//				}
//				if(mysqldbCon != null){
//					mysqldbCon.close();
//					mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//				}
//				mysqlstUpdateLastTrackA = mysqldbCon.prepareStatement(updateLastTrackAMysql);
//				
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//		}
//		
//		try{
//			if(trackCount > 0){
//				mysqlstUpdateLastTrack.executeBatch();
//				trackCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqldbCon.getMetaData();
//				mysqlstUpdateLastTrack = mysqldbCon.prepareStatement(updateLastTrackMysql);
//			} catch (SQLException e) {
//				if(mysqlstUpdateLastTrack != null){
//					mysqlstUpdateLastTrack.close();
//				}
//				if(mysqldbCon != null){
//					mysqldbCon.close();
//					mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);	
//				}
//				mysqlstUpdateLastTrack = mysqldbCon.prepareStatement(updateLastTrackMysql);
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//		}
//		
//		try{
//			if(alarmCount > 0){
//				mysqlstSaveAlarmTrack.executeBatch();
//				alarmCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqlAlarmdbCon.getMetaData();
//				mysqlstSaveAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_saveAlarmTrack_Mysql);
//			} catch (SQLException e) {
//				if(mysqlstSaveAlarmTrack  != null){
//					mysqlstSaveAlarmTrack.close();
//				}
//				if( mysqlAlarmdbCon != null){
//					mysqlAlarmdbCon.close();
//					mysqlAlarmdbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//				}
//				mysqlstSaveAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_saveAlarmTrack_Mysql);
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//			
//		}
//		
//		try{
//			if(alarmUpdateCount > 0){
//				mysqlstUpdateAlarmTrack.executeBatch();
//				alarmUpdateCount = 0;
//			}
//		}catch(MySQLNonTransientConnectionException ex){
//			try {
//				mysqlAlarmdbCon.getMetaData();
//				mysqlstUpdateAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_updateAlarmTrack_Mysql);
//			} catch (SQLException e) {
//				if(mysqlstUpdateAlarmTrack != null){
//					mysqlstUpdateAlarmTrack.close();
//				}
//				if(mysqlAlarmdbCon != null){
//					mysqlAlarmdbCon.close();
//					mysqlAlarmdbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
//				}
//				mysqlstUpdateAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_updateAlarmTrack_Mysql);
//			}
//		}catch(Exception ex){
//			logger.error(ex);
//		}
	}

	/**
	 * mysql更新轨迹包最后位置到数据库
	 * 
	 * @param app
	 *            位置报文类
	 * @throws SQLException 
	 */
	public void mysqlUpdateLastTrackLine(Map<String, String> app) throws SQLException {
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
				mysqlstUpdateLastTrackALine.setLong(1, lon);// LON
				mysqlstUpdateLastTrackALine.setLong(2, lat);// LAT
				mysqlstUpdateLastTrackALine.setInt(3, gpsSpeed);// SPEED
				mysqlstUpdateLastTrackALine.setInt(4, head);// HEAD
				mysqlstUpdateLastTrackALine.setLong(5, utc);// UTC
				mysqlstUpdateLastTrackALine.setString(6,String.valueOf(System.currentTimeMillis()));

				mysqlstUpdateLastTrackALine.setString(7, alarmCode);
				mysqlstUpdateLastTrackALine.setLong(8, mapLon);
				mysqlstUpdateLastTrackALine.setLong(9, mapLat);

				mysqlstUpdateLastTrackALine.setLong(10, utc);
				 
				
				if (app.get("6") != null) { // 
					mysqlstUpdateLastTrackALine.setLong(11,Long.parseLong(app.get("6")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(11, Types.INTEGER);
				}
				
				if (app.get("210") != null) { // 引擎转速（发动机转速）
					mysqlstUpdateLastTrackALine.setLong(12,
							Long.parseLong(app.get("210")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(12, Types.INTEGER);
				}

				if (app.get("216") != null) { // 瞬时油耗
					mysqlstUpdateLastTrackALine.setLong(13,
							Long.parseLong(app.get("216")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(13, Types.INTEGER);
				}

				if (app.get("215") != null && !app.get("215").equals("")) { // 机油压力
					mysqlstUpdateLastTrackALine.setLong(14,
							Long.parseLong(app.get("215")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(14, Types.INTEGER);
				}
				if (app.get("508") != null) { // 机油温度（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(15,
							Long.parseLong(app.get("508")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(15, Types.INTEGER);
				}
				if (app.get("504") != null && !app.get("504").equals("")) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(16,
							Long.parseLong(app.get("504")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(16, Types.INTEGER);
				}
				if (app.get("213") != null) {
					mysqlstUpdateLastTrackALine.setLong(17,Long.parseLong(app.get("213")));
				}else{
					mysqlstUpdateLastTrackALine.setLong(17, -1);
				}
				if (app.get("213") != null) { // 累计油耗
					mysqlstUpdateLastTrackALine.setLong(18,
							Long.parseLong(app.get("213")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(18, Types.INTEGER);
				}
				
				if (app.get("505") != null) {
					mysqlstUpdateLastTrackALine.setLong(19,Long.parseLong(app.get("505")));
				}else{
					mysqlstUpdateLastTrackALine.setNull(19, Types.INTEGER);
				}
				
				if (app.get("505") != null) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(20,
							Long.parseLong(app.get("505")));
				} else {
					mysqlstUpdateLastTrackALine.setLong(20, -1);
				}
				if (app.get("510") != null) { // 进气温度（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(21,
							Long.parseLong(app.get("510")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(21, Types.INTEGER);
				}
				if (app.get("511") != null && !app.get("511").equals("")) { // 大气压力（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(22,
							Long.parseLong(app.get("511")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(22, Types.INTEGER);
				}
				if (app.get("7") != null) { // 脉冲车速（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setInt(23,
							Integer.parseInt(app.get("7")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(23, Types.INTEGER);
				}

				if (app.get("507") != null) { // 终端内置电池电压（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(24,
							Long.parseLong(app.get("507")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(24, Types.INTEGER);
				}

				if (app.get("509") != null && !app.get("509").equals("")) { // 冷却液温度（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(25,
							Long.parseLong(app.get("509")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(25, Types.INTEGER);
				}

				if (app.get("506") != null) { // 车辆蓄电池电压（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(26,
							Long.parseLong(app.get("506")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(26, Types.INTEGER);
				}

				if (app.get("503") != null && !app.get("503").equals("")) { // 发动机扭矩（随位置汇报上传）
					mysqlstUpdateLastTrackALine.setLong(27,
							Long.parseLong(app.get("503")));
				} else {
					mysqlstUpdateLastTrackALine.setNull(27, Types.INTEGER);
				}
				mysqlstUpdateLastTrackALine.setLong(28,Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackALine.setLong(29, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackALine.setString(30, basestatus);
				mysqlstUpdateLastTrackALine.setString(31, extendstatus);
				mysqlstUpdateLastTrackALine.setString(32, msgid);
				mysqlstUpdateLastTrackALine.setString(33, app.get(Constant.SPEEDFROM)); // 车速来源
				if (app.get("24") != null) { // OIL_MEASURE 判断数据
					mysqlstUpdateLastTrackALine.setLong(34, Long.parseLong(app.get("24")));
				}else{ 
					mysqlstUpdateLastTrackALine.setLong(34, -1);
				}
				
				//精准油耗
				if(null != app.get("219")){
					mysqlstUpdateLastTrackALine.setLong(35, Long.parseLong(app.get("219")));
				}else{
					mysqlstUpdateLastTrackALine.setLong(35, -1);
				}
				
				mysqlstUpdateLastTrackALine.setLong(36, vid);
				mysqlstUpdateLastTrackALine.addBatch();
				alarmTrackLineCount++;
				if (alarmTrackLineCount % commitTrackCount == 0) {
					long s1=System.currentTimeMillis();
					mysqlstUpdateLastTrackALine.executeBatch();
					mysqlstUpdateLastTrackALine.clearBatch();
					logger.info( "ThreadId : "+ this.threadId + " mysqlstUpdateLastTrackALine"+alarmTrackLineCount+","+(System.currentTimeMillis()-s1)+"ms");
					
					alarmTrackLineCount = 0;
				}
			}catch(NullPointerException ex ){
				nulldata.trace(app.get(Constant.COMMAND));
			} catch(MySQLNonTransientConnectionException ex){

				try{
					mysqldbCon.getMetaData();
					mysqlstUpdateLastTrackALine = mysqldbCon.prepareStatement(updateLastTrackALineMysql);
				}catch(Exception ex1){
					if ( mysqlstUpdateLastTrackALine != null) {
						mysqlstUpdateLastTrackALine.close();
					}
					if( mysqldbCon != null){
						mysqldbCon.close();
						mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
					}
					mysqlstUpdateLastTrackALine = mysqldbCon.prepareStatement(updateLastTrackALineMysql);
					
				}
			}catch (SQLException e) {
				logger.error("MYSQL最新位置报警更新出错.", e);
			}
		} else {
			try {
				mysqlstUpdateLastTrackLine.setLong(1, lon);
				mysqlstUpdateLastTrackLine.setLong(2, lat);
				mysqlstUpdateLastTrackLine.setInt(3, gpsSpeed);
				mysqlstUpdateLastTrackLine.setInt(4, head);
				mysqlstUpdateLastTrackLine.setLong(5, utc);
				mysqlstUpdateLastTrackLine.setString(6,
						String.valueOf(System.currentTimeMillis()));

				mysqlstUpdateLastTrackLine.setLong(7, mapLon);
				mysqlstUpdateLastTrackLine.setLong(8, mapLat);
			 
				if (app.get("6") != null) { 
					mysqlstUpdateLastTrackLine.setLong(9,
							Long.parseLong(app.get("6")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(9, Types.INTEGER);
				}
				
				if (app.get("210") != null) { // 引擎转速（发动机转速）
					mysqlstUpdateLastTrackLine.setLong(10,
							Long.parseLong(app.get("210")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(10, Types.INTEGER);
				}

				if (app.get("216") != null) { // 瞬时油耗
					mysqlstUpdateLastTrackLine.setLong(11,
							Long.parseLong(app.get("216")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(11, Types.INTEGER);
				}

				if (app.get("215") != null && !app.get("215").equals("")) { // 机油压力
					mysqlstUpdateLastTrackLine.setLong(12,
							Long.parseLong(app.get("215")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(12, Types.INTEGER);
				}
				if (app.get("508") != null) { // 机油温度（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(13,
							Long.parseLong(app.get("508")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(13, Types.INTEGER);
				}
				if (app.get("504") != null && !app.get("504").equals("")) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(14,
							Long.parseLong(app.get("504")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(14, Types.INTEGER);
				}
				
				if (app.get("213") != null) {
					mysqlstUpdateLastTrackLine.setLong(15,Long.parseLong(app.get("213")));
				}else{
					mysqlstUpdateLastTrackLine.setLong(15, -1);
				}
				
				if (app.get("213") != null) { // 累计油耗
					mysqlstUpdateLastTrackLine.setLong(16,
							Long.parseLong(app.get("213")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(16, Types.INTEGER);
				}
				
				if (app.get("505") != null) {
					mysqlstUpdateLastTrackLine.setLong(17,
							Long.parseLong(app.get("505")));
				}else{
					mysqlstUpdateLastTrackLine.setLong(17, -1);
				}
				
				if (app.get("505") != null) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(18,
							Long.parseLong(app.get("505")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(18, Types.INTEGER);
				}
				if (app.get("510") != null) { // 进气温度（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(19,
							Long.parseLong(app.get("510")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(19, Types.INTEGER);
				}

				if (app.get("511") != null && !app.get("511").equals("")) { // 大气压力（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(20,
							Long.parseLong(app.get("511")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(20, Types.INTEGER);
				}

				if (app.get("7") != null) { // 脉冲车速（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setInt(21,
							Integer.parseInt(app.get("7")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(21, Types.INTEGER);
				}

				if (app.get("507") != null) { // 终端内置电池电压（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(22,
							Long.parseLong(app.get("507")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(22, Types.INTEGER);
				}

				if (app.get("509") != null && !app.get("509").equals("")) { // 冷却液温度（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(23,
							Long.parseLong(app.get("509")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(23, Types.INTEGER);
				}

				if (app.get("506") != null) { // 车辆蓄电池电压（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(24,
							Long.parseLong(app.get("506")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(24, Types.INTEGER);
				}

				if (app.get("503") != null && !app.get("503").equals("")) { // 发动机扭矩（随位置汇报上传）
					mysqlstUpdateLastTrackLine.setLong(25,
							Long.parseLong(app.get("503")));
				} else {
					mysqlstUpdateLastTrackLine.setNull(25, Types.INTEGER);
				}
				mysqlstUpdateLastTrackLine.setLong(26, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackLine.setLong(27, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackLine.setString(28, basestatus);
				mysqlstUpdateLastTrackLine.setString(29, extendstatus);
				mysqlstUpdateLastTrackLine.setString(30, msgid);
				mysqlstUpdateLastTrackLine.setString(31, app.get(Constant.SPEEDFROM));//车速来源
				if (app.get("24") != null) { // OIL_MEASURE 判断数据
					mysqlstUpdateLastTrackLine.setLong(32, Long.parseLong(app.get("24")));
				}else{
					mysqlstUpdateLastTrackLine.setLong(32, -1);
				}
				
				//精准油耗
				if (app.get("219") != null) { 
					mysqlstUpdateLastTrackLine.setLong(33, Long.parseLong(app.get("219")));
				}else{
					mysqlstUpdateLastTrackLine.setLong(33, -1);
				}
				
				mysqlstUpdateLastTrackLine.setLong(34, vid);
				mysqlstUpdateLastTrackLine.addBatch();
				trackLineCount++;
				if (trackLineCount % commitTrackCount == 0) {
					long s1=System.currentTimeMillis();
					mysqlstUpdateLastTrackLine.executeBatch();
					mysqlstUpdateLastTrackLine.clearBatch();
					logger.info( "ThreadId : "+ this.threadId + " mysqlstUpdateLastTrackLine"+trackLineCount+","+(System.currentTimeMillis()-s1)+"ms");
					
					trackLineCount = 0;
				}
			}catch(NullPointerException ex ){
				nulldata.trace(app.get(Constant.COMMAND));
			}catch(MySQLNonTransientConnectionException ex){
				
				try{
					mysqldbCon.getMetaData();
					mysqlstUpdateLastTrackLine = mysqldbCon.prepareStatement(updateLastTrackLineMysql);
				}catch(Exception ex1){
					if ( mysqlstUpdateLastTrackLine != null) {
						mysqlstUpdateLastTrackLine.close();
					}
					if( mysqldbCon != null){
						mysqldbCon.close();
						mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
					}
					mysqlstUpdateLastTrackLine = mysqldbCon.prepareStatement(updateLastTrackLineMysql);
				}
			}catch (SQLException e) {
				logger.error("MYSQL最新位置更新出错.", e);
			}
		}
	}

	/**
	 * mysql更新轨迹包最后位置到数据库
	 * 
	 * @param app
	 *            位置报文类
	 * @throws SQLException 
	 */
	public void mysqlUpdateLastTrack(Map<String, String> app) throws SQLException{
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
		
		if(alarmCode != null){
			
			if (app.get("21")!=null) {
				alarmCode = alarmCode.substring(0, alarmCode.length() - 1)	+ app.get("21");
			}
			alarmCode = alarmCode.replaceAll("\\,\\,", ",");
		}
		String basestatus = app.get("8");// 基本状态  {210=-1, 216=-1, 213=-1, filealarmcode=,11,24,, 218=1, 21=,, 20=,11,24
		String extendstatus = app.get("500");// 扩展状态
		String msgid=app.get(Constant.MSGID);
		if (alarmCode != null && alarmCode.length() > 1) {
			try {
				mysqlstUpdateLastTrackA.setLong(1, lon);// LON
				mysqlstUpdateLastTrackA.setLong(2, lat);// LAT
				mysqlstUpdateLastTrackA.setInt(3, gpsSpeed);// SPEED
				mysqlstUpdateLastTrackA.setInt(4, head);// HEAD
				mysqlstUpdateLastTrackA.setLong(5, utc);// UTC
				mysqlstUpdateLastTrackA.setString(6,
						String.valueOf(System.currentTimeMillis()));

				mysqlstUpdateLastTrackA.setString(7, alarmCode);
				mysqlstUpdateLastTrackA.setLong(8, mapLon);
				mysqlstUpdateLastTrackA.setLong(9, mapLat);

				mysqlstUpdateLastTrackA.setLong(10, utc);
				if (app.get("6") != null) {  
					mysqlstUpdateLastTrackA.setLong(11,
							Long.parseLong(app.get("6")));
				} else {
					mysqlstUpdateLastTrackA.setNull(11, Types.INTEGER);
				}
				if (app.get("210") != null) { // 引擎转速（发动机转速）
					mysqlstUpdateLastTrackA.setLong(12,
							Long.parseLong(app.get("210")));
				} else {
					mysqlstUpdateLastTrackA.setNull(12, Types.INTEGER);
				}

				if (app.get("216") != null) { // 瞬时油耗
					mysqlstUpdateLastTrackA.setLong(13,
							Long.parseLong(app.get("216")));
				} else {
					mysqlstUpdateLastTrackA.setNull(13, Types.INTEGER);
				}

				if (app.get("504") != null && !app.get("504").equals("")) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传）
					mysqlstUpdateLastTrackA.setLong(14,
							Long.parseLong(app.get("504")));
				} else {
					mysqlstUpdateLastTrackA.setNull(14, Types.INTEGER);
				}
				if (app.get("213") != null) {
					mysqlstUpdateLastTrackA.setLong(15,
							Long.parseLong(app.get("213")));
				}else{
					mysqlstUpdateLastTrackA.setLong(15, -1);
				}
				if (app.get("213") != null) { // 累计油耗
					mysqlstUpdateLastTrackA.setLong(16,
							Long.parseLong(app.get("213")));
				} else {
					mysqlstUpdateLastTrackA.setNull(16, Types.INTEGER);
				}

				if (app.get("7") != null) { // 脉冲车速（随位置汇报上传）
					mysqlstUpdateLastTrackA.setInt(17,
							Integer.parseInt(app.get("7")));
				} else {
					mysqlstUpdateLastTrackA.setNull(17, Types.INTEGER);
				}

				if (app.get("503") != null && !app.get("503").equals("")) { // 发动机扭矩（随位置汇报上传）
					mysqlstUpdateLastTrackA.setLong(18,
							Long.parseLong(app.get("503")));
				} else {
					mysqlstUpdateLastTrackA.setNull(18, Types.INTEGER);
				}
				mysqlstUpdateLastTrackA.setLong(19, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackA.setLong(20, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrackA.setString(21, basestatus);
				mysqlstUpdateLastTrackA.setString(22, extendstatus);
				mysqlstUpdateLastTrackA.setString(23, msgid);
				mysqlstUpdateLastTrackA.setString(24, app.get(Constant.SPEEDFROM)); //车速来源
				if (app.get("24") != null) { // OIL_MEASURE 判断数据
					mysqlstUpdateLastTrackA.setLong(25, Long.parseLong(app.get("24")));
				}else{
					mysqlstUpdateLastTrackA.setLong(25, -1);
				}
				
				//精准油耗
				if (app.get("219") != null) {
					mysqlstUpdateLastTrackA.setLong(26, Long.parseLong(app.get("219")));
				}else{
					mysqlstUpdateLastTrackA.setLong(26, -1);
				}
				mysqlstUpdateLastTrackA.setLong(27, vid);
				mysqlstUpdateLastTrackA.addBatch();
				alarmTrackCount++;
				if (alarmTrackCount % commitTrackCount == 0) {
					long s1=System.currentTimeMillis();
					mysqlstUpdateLastTrackA.executeBatch();
					mysqlstUpdateLastTrackA.clearBatch();
					logger.info( "ThreadId : "+ this.threadId + " mysqlstUpdateLastTrackA"+alarmTrackCount+","+(System.currentTimeMillis()-s1)+"ms");
					
					alarmTrackCount = 0;
				}
			}catch(NullPointerException ex ){
				nulldata.trace(app.get(Constant.COMMAND));
			}catch(MySQLNonTransientConnectionException ex){

				try{
					mysqldbCon.getMetaData();
					mysqlstUpdateLastTrackA = mysqldbCon.prepareStatement(updateLastTrackAMysql);
				}catch(Exception ex1){
					if ( mysqlstUpdateLastTrackA != null) {
						mysqlstUpdateLastTrackA.close();
					}
					if(mysqldbCon != null){
						mysqldbCon.close();
						mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
					}
					mysqlstUpdateLastTrackA = mysqldbCon.prepareStatement(updateLastTrackAMysql);
				}
								
			} catch (SQLException e) {
				logger.error("MYSQL最新位置报警更新出错.", e);
			}
		} else {
			try {
				mysqlstUpdateLastTrack.setLong(1, lon);
				mysqlstUpdateLastTrack.setLong(2, lat);
				mysqlstUpdateLastTrack.setInt(3, gpsSpeed);
				mysqlstUpdateLastTrack.setInt(4, head);
				mysqlstUpdateLastTrack.setLong(5, utc);
				mysqlstUpdateLastTrack.setString(6,
						String.valueOf(System.currentTimeMillis()));

				mysqlstUpdateLastTrack.setLong(7, mapLon);
				mysqlstUpdateLastTrack.setLong(8, mapLat);
		 
				if (app.get("6") != null) { //  
					mysqlstUpdateLastTrack.setLong(9,
							Long.parseLong(app.get("6")));
				} else {
					mysqlstUpdateLastTrack.setNull(9, Types.INTEGER);
				}
				if (app.get("210") != null) { // 引擎转速（发动机转速）
					mysqlstUpdateLastTrack.setLong(10,
							Long.parseLong(app.get("210")));
				} else {
					mysqlstUpdateLastTrack.setNull(10, Types.INTEGER);
				}

				if (app.get("216") != null) { // 瞬时油耗
					mysqlstUpdateLastTrack.setLong(11,
							Long.parseLong(app.get("216")));
				} else {
					mysqlstUpdateLastTrack.setNull(11, Types.INTEGER);
				}

				if (app.get("504") != null && !app.get("504").equals("")) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传）
					mysqlstUpdateLastTrack.setLong(12,
							Long.parseLong(app.get("504")));
				} else {
					mysqlstUpdateLastTrack.setNull(12, Types.INTEGER);
				}
				if (app.get("213") != null) {
					mysqlstUpdateLastTrack.setLong(13,
							Long.parseLong(app.get("213")));
				}else{
					mysqlstUpdateLastTrack.setLong(13, -1);
				}
				if (app.get("213") != null) { // 累计油耗
					mysqlstUpdateLastTrack.setLong(14,
							Long.parseLong(app.get("213")));
				} else {
					mysqlstUpdateLastTrack.setNull(14, Types.INTEGER);
				}

				if (app.get("7") != null) { // 脉冲车速（随位置汇报上传）
					mysqlstUpdateLastTrack.setInt(15,
							Integer.parseInt(app.get("7")));
				} else {
					mysqlstUpdateLastTrack.setNull(15, Types.INTEGER);
				}

				if (app.get("503") != null && !app.get("503").equals("")) { // 发动机扭矩（随位置汇报上传）
					mysqlstUpdateLastTrack.setLong(16,
							Long.parseLong(app.get("503")));
				} else {
					mysqlstUpdateLastTrack.setNull(16, Types.INTEGER);
				}
				
				mysqlstUpdateLastTrack.setLong(17, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrack.setLong(18, Long.parseLong(app.get("9")));
				mysqlstUpdateLastTrack.setString(19, basestatus);
				mysqlstUpdateLastTrack.setString(20, extendstatus);
				mysqlstUpdateLastTrack.setString(21, msgid);
				mysqlstUpdateLastTrack.setString(22, app.get(Constant.SPEEDFROM)); //车速来源
				if (app.get("24") != null) { // OIL_MEASURE 判断数据
					mysqlstUpdateLastTrack.setLong(23, Long.parseLong(app.get("24")));
				}else{
					mysqlstUpdateLastTrack.setLong(23, -1);
				}
				
				//精准油耗
				if (app.get("219") != null) {
					mysqlstUpdateLastTrack.setLong(24, Long.parseLong(app.get("219")));
				}else{
					mysqlstUpdateLastTrack.setLong(24, -1);
				}
				mysqlstUpdateLastTrack.setLong(25, vid);
				mysqlstUpdateLastTrack.addBatch();
				trackCount++;
				if (trackCount % commitTrackCount == 0) {
					long s1=System.currentTimeMillis();
					mysqlstUpdateLastTrack.executeBatch();
					mysqlstUpdateLastTrack.clearBatch();
					logger.info( "ThreadId : "+ this.threadId + " mysqlstUpdateLastTrack"+trackCount+","+(System.currentTimeMillis()-s1)+"ms");
					
					trackCount = 0;
				}
			}catch(NullPointerException ex ){
				nulldata.trace(app.get(Constant.COMMAND));
			}catch(MySQLNonTransientConnectionException ex){
				try{
					mysqldbCon.getMetaData();
					mysqlstUpdateLastTrack = mysqldbCon.prepareStatement(updateLastTrackMysql);
				}catch(Exception ex1){
					if (mysqlstUpdateLastTrack  != null) {
						mysqlstUpdateLastTrack.close();
					}
					if(mysqldbCon != null){
						mysqldbCon.close();
						mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
					}
					mysqlstUpdateLastTrack = mysqldbCon.prepareStatement(updateLastTrackMysql);
				}
			}catch (SQLException e) {
				logger.error("MYSQL最新位置更新出错.", e);
			}
		}
	}

	/**
	 * mysql报警包存入数据库
	 * 
	 * @param app
	 *            位置报文类
	 * @throws SQLException 
	 */
	public void mysqlSaveAlarmTrack(Map<String, String> app, String alarmCode,String key,Long spd) throws SQLException {
		try {
//			logger.info("key----------:"+key);{210=-1, 216=-1, 213=-1, filealarmcode=,11,24,27,, 218=1, 21=,, 20=,11,24,, maplon=67696656, command=CAITS 0_1 E001_15286845384 0 U_REPT {TYPE:0,RET:0,1:67693540,2:14448930,20:150996992,21:0,210:-1,213:-1,216:-1,218:1,24:0,3:622,4:20130608/222648,5:123,500:17,503:-1,504:-1,520:0,6:84,7:0,8:3,9:1845290} , tid=10177, head=CAITS, speedfrom=1, vid=693, TYPE=0, macid=E001_15286845384, commdr=15286845384, maplat=14447235, msgid=10002, RET=0, 503=-1, 504=-1, rearaxlerate=3.8, utc=1370701608000, tyrer=0.0, 24=0, mtype=U_REPT, gears=-100, 500=17, vehicleno=湘N61353, content=TYPE:0,RET:0,1:67693540,2:14448930,20:150996992,21:0,210:-1,213:-1,216:-1,218:1,24:0,3:622,4:20130608/222648,5:123,500:17,503:-1,504:-1,520:0,6:84,7:0,8:3,9:1845290, 3=622, 2=14448930, 1=67693540, 7=0, ratio=-100.0, platecolorid=2, 6=84, 5=123, 520=0, 4=20130608/222648, seq=0_1, 9=1845290, uuid=854f3a02-e800-409b-9de8-7ebdc21e244c, 8=3, channel=0, ptype=track}
			String basestatus = app.get("8");// 基本状态
			String extendstatus = app.get("500");// 扩展状态
			String alarmAdd = app.get("32");
//			int spd = Utils.getSpeed(app.get(Constant.SPEEDFROM),app); // 根据不同速度来源获取速度
			
			mysqlstSaveAlarmTrack.setString(1, key);
			mysqlstSaveAlarmTrack.setLong(2,Long.parseLong(app.get(Constant.VID)));
			mysqlstSaveAlarmTrack.setLong(3,Long.parseLong(app.get(Constant.UTC)));
			mysqlstSaveAlarmTrack.setLong(4, Long.parseLong(app.get("2")));
			mysqlstSaveAlarmTrack.setLong(5, Long.parseLong(app.get("1")));
			mysqlstSaveAlarmTrack.setLong(6, Long.parseLong(app.get(Constant.MAPLON)));
			mysqlstSaveAlarmTrack.setLong(7, Long.parseLong(app.get(Constant.MAPLAT)));
			if (app.get("6") != null) {
				mysqlstSaveAlarmTrack.setLong(8, Long.parseLong(app.get("6")));
			} else {
				mysqlstSaveAlarmTrack.setNull(8, Types.INTEGER);
			}
			mysqlstSaveAlarmTrack.setInt(9, Integer.parseInt(app.get("5")));
			mysqlstSaveAlarmTrack.setLong(10, spd); 
			mysqlstSaveAlarmTrack.setLong(11, Long.parseLong(app.get("9")));
			if (app.get("213") != null) {
				mysqlstSaveAlarmTrack.setLong(12, Long.parseLong(app.get("213")));
			} else {
				mysqlstSaveAlarmTrack.setNull(12, Types.INTEGER);
			}
			mysqlstSaveAlarmTrack.setString(13, alarmCode);
			mysqlstSaveAlarmTrack.setLong(14, System.currentTimeMillis());
			mysqlstSaveAlarmTrack.setInt(15, 1);
			mysqlstSaveAlarmTrack.setLong(16,Long.parseLong(app.get(Constant.UTC)));
			mysqlstSaveAlarmTrack.setString(17, ""); // 当班司机
			mysqlstSaveAlarmTrack.setString(18, ""); // 车牌号
			AlarmTypeBean alarmTypeBean = TempMemory.getAlarmtypeMapValue(Integer.parseInt(alarmCode));
			if (alarmTypeBean != null) {
				mysqlstSaveAlarmTrack.setString(19, alarmTypeBean.getParentCode());
			} else {
				mysqlstSaveAlarmTrack.setNull(19, Types.NULL);
			}
			
			if(basestatus != null){
				mysqlstSaveAlarmTrack.setString(20, basestatus);
			}else{
				mysqlstSaveAlarmTrack.setNull(20, Types.NULL);
			}
			
			if(extendstatus != null){
				mysqlstSaveAlarmTrack.setString(21, extendstatus);
			}else{
				mysqlstSaveAlarmTrack.setNull(21, Types.NULL);
			}
			
			if(alarmAdd != null){
				mysqlstSaveAlarmTrack.setString(22, alarmAdd);
			}else{
				mysqlstSaveAlarmTrack.setNull(22, Types.NULL);
			}
			
			mysqlstSaveAlarmTrack.addBatch();
			alarmCount++;
			
			if (alarmCount % commitAlarmCount == 0) {
				long s1=System.currentTimeMillis();
				mysqlstSaveAlarmTrack.executeBatch();
				mysqlstSaveAlarmTrack.clearBatch();
				long s2 = System.currentTimeMillis();
				logger.info( "ThreadId : "+ this.threadId + " mysqlstSaveAlarmTrack"+alarmCount+","+(s2-s1)+"ms");
				alarmCount = 0;
			}
		} catch(MySQLNonTransientConnectionException ex){
			
			try{
				mysqlAlarmdbCon.getMetaData();
				mysqlstSaveAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_saveAlarmTrack_Mysql);
			}catch(Exception ex1){
				if (mysqlstSaveAlarmTrack  != null) {
					mysqlstSaveAlarmTrack.close();
				}
				if(mysqlAlarmdbCon != null){
					mysqlAlarmdbCon.close();
					mysqlAlarmdbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
				}
				mysqlstSaveAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_saveAlarmTrack_Mysql);
				
			}
		}catch (Exception e) {
//			e.printStackTrace(); //TODO
//			logger.error(key + "---key,alarmcode:"+alarmCode+", "+app.toString());
			logger.error("MYSQL存储报警出错."+ e.getMessage(),e);
		}
	}
	
	/**
	 * 更新最新报警结束时间
	 * 
	 * @param gpsTime
	 * @param vid
	 * @throws SQLException
	 */
	public void updateAlarmEndTime(Map<String, String> app, String alarmCode,String alarmid,Long spd) throws SQLException
			 {
		try {
			String alarmAdd = app.get("32");
//			int spd = Utils.getSpeed(app.get(Constant.SPEEDFROM),app); // 根据不同速度来源获取速度
			mysqlstUpdateAlarmTrack.setLong(1,Long.parseLong(app.get(Constant.UTC)));
			mysqlstUpdateAlarmTrack.setLong(2, Long.parseLong(app.get("2")));
			mysqlstUpdateAlarmTrack.setLong(3, Long.parseLong(app.get("1")));

			mysqlstUpdateAlarmTrack.setLong(4,
					Long.parseLong(app.get(Constant.MAPLAT)));
			mysqlstUpdateAlarmTrack.setLong(5,
					Long.parseLong(app.get(Constant.MAPLON)));
	 
			if (app.get("6") != null) {
				mysqlstUpdateAlarmTrack.setLong(6,
						Long.parseLong(app.get("6")));
			} else {
				mysqlstUpdateAlarmTrack.setNull(6, Types.INTEGER);
			}
			
			mysqlstUpdateAlarmTrack.setInt(7, Integer.parseInt(app.get("5")));
			mysqlstUpdateAlarmTrack.setLong(8, spd);
			mysqlstUpdateAlarmTrack.setLong(9,Long.parseLong(app.get("9")));
			
			if (app.get("213") != null) {
				mysqlstUpdateAlarmTrack.setLong(10,
						Long.parseLong(app.get("213")));
			} else {
				mysqlstUpdateAlarmTrack.setNull(10, Types.INTEGER);
			}
			
			if(alarmAdd != null){
				mysqlstUpdateAlarmTrack.setString(11, alarmAdd);
			}else{
				mysqlstUpdateAlarmTrack.setNull(11, Types.NULL);
			}
			
			if (app.get(Constant.MAXRPM)!=null&&!"".equals(app.get(Constant.MAXRPM))){
				mysqlstUpdateAlarmTrack.setLong(12, Long.parseLong(app.get(Constant.MAXRPM)));
			}else{
				mysqlstUpdateAlarmTrack.setNull(12, Types.NULL);
			}

			mysqlstUpdateAlarmTrack.setString(13, alarmid);
			
			mysqlstUpdateAlarmTrack.addBatch();

			alarmUpdateCount++;
			if (alarmUpdateCount % commitAlarmCount == 0) {
				long s1=System.currentTimeMillis();
				mysqlstUpdateAlarmTrack.executeBatch();
				mysqlstUpdateAlarmTrack.clearBatch();
				
				logger.info( "ThreadId : "+ this.threadId + " updateAlarmEndTime"+alarmUpdateCount+","+(System.currentTimeMillis()-s1)+"ms");
				alarmUpdateCount = 0;
			}
		}catch(MySQLNonTransientConnectionException ex){

			try{
				mysqlAlarmdbCon.getMetaData();
				mysqlstUpdateAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_updateAlarmTrack_Mysql);
			}catch(Exception ex1){
				if ( mysqlstUpdateAlarmTrack != null) {
					mysqlstUpdateAlarmTrack.close();
				}
					if(mysqlAlarmdbCon != null){
						mysqlAlarmdbCon.close();
						mysqlAlarmdbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
					}
					mysqlstUpdateAlarmTrack = mysqlAlarmdbCon.prepareStatement(sql_updateAlarmTrack_Mysql);
			}
		} catch (Exception e) {
//			e.printStackTrace(); //TODO
			logger.error("MYSQL存储报警出错."+e.getMessage(),e);
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
	public void updateLastTrackISonLine(int isValid, long vid,String msgid) throws SQLException{
		try {
			stUpdateLastTrackISonLine.setInt(1, isValid);
			stUpdateLastTrackISonLine.setString(2, msgid);
			stUpdateLastTrackISonLine.setLong(3, vid);
			stUpdateLastTrackISonLine.executeUpdate();
		}catch(MySQLNonTransientConnectionException ex){
			try{
				mysqldbCon.getMetaData();
				stUpdateLastTrackISonLine = mysqldbCon.prepareStatement(sql_UpdateLastTrackISonLine);
			}catch(Exception ex1){
				if ( stUpdateLastTrackISonLine != null) {
					stUpdateLastTrackISonLine.close();
				}
				if(mysqldbCon != null){
					mysqldbCon.close();
					mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
				}
				stUpdateLastTrackISonLine = mysqldbCon.prepareStatement(sql_UpdateLastTrackISonLine);
			}
		} catch (Exception e) {
			logger.error("更新MYSQL轨迹状态出错.", e);
		}
	}
	
	
	/***
	 * 查询车辆最新状态
	 * 
	 * @param isValid
	 * @param utc
	 * @param vid
	 * @throws SQLException
	 */
	public Map<String, String> queryVehicleStatus(long vid,String macid) {
		ResultSet rs = null;
		PreparedStatement stQueryVehicleStatus = null;
		Connection innerConn = null;
		try {
			innerConn = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stQueryVehicleStatus = innerConn.prepareStatement(queryVehicleStatusMysql);
			
			stQueryVehicleStatus.setLong(1, vid);
			rs = stQueryVehicleStatus.executeQuery();
	 
			if (rs.next()) {
				Map<String, String> map = new HashMap<String, String>();
				String alarmcode = rs.getString("ALARMCODE");// 车机类型码
				map.put(Constant.VID, String.valueOf(vid));
				map.put(Constant.ALARMCODE, alarmcode);
				return map;
			}
			return null;
			
		} catch (Exception e) {
			logger.error("查询车辆最新状态出错."+e.getMessage());
			return null;
		}finally{
			if(rs != null){
				try {
					rs.close();
				} catch (SQLException e) {
					logger.error(Constant.SPACES,e);
				}
			}
			if(stQueryVehicleStatus != null){
				try {
					stQueryVehicleStatus.close();
				} catch (SQLException e) {
					logger.error(Constant.SPACES,e);
				}
			}
			
			if(innerConn != null){
				try {
					innerConn.close();
				} catch (SQLException e) {
					logger.error(Constant.SPACES,e);
				}
			}
		}
	}
	
	/****
	 * 更新车辆上下线状态信息
	 * 
	 * @param isOnline
	 * @param vid
	 * @throws SQLException 
	 * @throws SQLException
	 */
	public void mysqlUpdateIsonline(Map<String, String> packet) throws SQLException{
		Connection mysqlConn = null;
		try {
			String parm = packet.get("18");
			String parms[] = parm.split("/");
			Long vid = Long.parseLong(packet.get(Constant.VID));
			String msgid = packet.get(Constant.MSGID);
			if (parms.length == 4) {
				mysqlConn = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
				stUpdateTrackIsonline = mysqlConn.prepareStatement(updateVehicleisonline);
				stUpdateTrackIsonline.setInt(1, Integer.parseInt(parms[0]));
				stUpdateTrackIsonline.setLong(2, CDate.getCurrentUtcMsDate());
				stUpdateTrackIsonline.setString(3, msgid);
				stUpdateTrackIsonline.setLong(4, vid);
				stUpdateTrackIsonline.executeUpdate();
			}
		}catch(Exception ex){
			logger.error(Constant.SPACES,ex);
		} finally {
			if (stUpdateTrackIsonline != null) {
				stUpdateTrackIsonline.close();
			}

			if (mysqlConn != null) {
				mysqlConn.close();
			}
		}
	}
}
