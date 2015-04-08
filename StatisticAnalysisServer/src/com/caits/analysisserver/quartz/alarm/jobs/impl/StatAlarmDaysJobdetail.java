package com.caits.analysisserver.quartz.alarm.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： <br>告警按企业告警类别按日统计分析
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2013-01-16</td>
 * <td>yujch</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author yujch
 * @since JDK1.6
 */
public class StatAlarmDaysJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(StatAlarmDaysJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String statAlarmDaysStatSql;// 访问记录按小时分析
	
	private String queryAlarmDaysStatSql;
	
	private String saveAlarmDaysStatSql;
	
	private String queryVehicleAlarmSql;
	
	private String saveVehicleAlarmEventSql;

//	private int count = 0;// 计数器

	private long statDate;
	private String dateStr;
	
	private long beginUtc;
	
	private long endUtc;
	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public StatAlarmDaysJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.dateStr = CDate.utc2Str(statDate, "yyyyMMdd");
		
		this.beginUtc = this.statDate - 12*60*60*1000;
		this.endUtc = this.statDate + 12*60*60*1000;

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 企业按告警类别统计
		statAlarmDaysStatSql = SQLPool.getinstance().getSql(
				"sql_procStatAlarmDays");
		
		queryAlarmDaysStatSql = SQLPool.getinstance().getSql(
				"sql_queryStatAlarmDaysInfo");
		
		saveAlarmDaysStatSql = SQLPool.getinstance().getSql(
		"sql_saveVehicleAlarmInfo");
		
		queryVehicleAlarmSql = SQLPool.getinstance().getSql(
		"sql_queryVehicleAlarmInfo");
		
		saveVehicleAlarmEventSql = SQLPool.getinstance().getSql(
				"sql_saveVehicleAlarmEventInfo");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		CallableStatement dbPstmt0 = null;
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt2 = null;
		PreparedStatement dbPstmt3 = null;
		PreparedStatement dbPstmt4 = null;
		Connection dbConnection = null;

		// 结果集对象
		ResultSet dbResultSet = null;
		ResultSet dbResultSet0 = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {
				//从告警明细表向告警事件表同步数据
				dbPstmt3 = dbConnection.prepareStatement(queryVehicleAlarmSql);
				dbPstmt3.setLong(1, beginUtc);
				dbPstmt3.setLong(2, endUtc);
				dbResultSet0 = dbPstmt3.executeQuery();
				logger.debug(dateStr + "告警明细数据查询成功！");
				dbPstmt4 = dbConnection.prepareStatement(saveVehicleAlarmEventSql);
				while(dbResultSet0.next()){
					dbPstmt4.setString(1, dbResultSet0.getString("ALARM_ID"));
					dbPstmt4.setString(2, dbResultSet0.getString("VID"));
					dbPstmt4.setString(3, dbResultSet0.getString("COMMADDR"));
					dbPstmt4.setString(4, dbResultSet0.getString("ALARM_CODE"));
					String areaId =dbResultSet0.getString("AREA_ID");
					if (areaId != null && !"".equals(areaId)) {
						dbPstmt4.setString(5,areaId);
					} else {
						dbPstmt4.setNull(5, Types.VARCHAR);
					}

					dbPstmt4.setString(6, null);
					dbPstmt4.setString(7, null);
					
					long utc0 = dbResultSet0.getLong("ALARM_START_UTC");
					
					dbPstmt4.setLong(8, utc0);
					dbPstmt4.setLong(9, dbResultSet0.getLong("LAT"));
					dbPstmt4.setLong(10, dbResultSet0.getLong("LON"));
					dbPstmt4.setLong(11, dbResultSet0.getLong("MAPLAT"));
					dbPstmt4.setLong(12, dbResultSet0.getLong("MAPLON"));
					dbPstmt4.setInt(13, dbResultSet0.getInt("ELEVATION"));
					dbPstmt4.setInt(14, dbResultSet0.getInt("DIRECTION"));
					dbPstmt4.setLong(15, dbResultSet0.getLong("GPS_SPEED"));

					long utc1 = dbResultSet0.getLong("ALARM_END_UTC");
					dbPstmt4.setLong(16, utc1);
					dbPstmt4.setLong(17, dbResultSet0.getLong("END_LAT"));
					dbPstmt4.setLong(18, dbResultSet0.getLong("END_LON"));
					dbPstmt4.setLong(19, dbResultSet0.getLong("END_MAPLAT"));
					dbPstmt4.setLong(20, dbResultSet0.getLong("END_MAPLON"));
					dbPstmt4.setInt(21, dbResultSet0.getInt("END_ELEVATION"));
					dbPstmt4.setInt(22, dbResultSet0.getInt("END_DIRECTION"));
					dbPstmt4.setLong(23, dbResultSet0.getLong("END_GPS_SPEED"));

					long tmpTime = (utc1 - utc0)/1000;
					if (tmpTime>0){
						dbPstmt4.setDouble(24,tmpTime);
					}else{
						dbPstmt4.setDouble(24,0);
					}
					
					dbPstmt4.setNull(25, Types.INTEGER);
					dbPstmt4.setNull(26, Types.VARCHAR);
					dbPstmt4.setString(27,dbResultSet0.getString("INNER_CODE"));
					dbPstmt4.setString(28,dbResultSet0.getString("VEHICLE_NO"));
					dbPstmt4.setLong(29, dbResultSet0.getLong("MILEAGE"));
					dbPstmt4.setLong(30, dbResultSet0.getLong("OIL"));
					dbPstmt4.setString(31,dbResultSet0.getString("VIN_CODE"));
					dbPstmt4.setString(32, null);
					dbPstmt4.setString(33,dbResultSet0.getString("PENT_ID"));
					dbPstmt4.setString(34,dbResultSet0.getString("PENT_NAME"));
					dbPstmt4.setString(35,dbResultSet0.getString("ENT_ID"));
					dbPstmt4.setString(36,dbResultSet0.getString("ENT_NAME"));
					dbPstmt4.setInt(37, dbResultSet0.getInt("ALARM_SRC"));
					dbPstmt4.setNull(38, Types.INTEGER);
					dbPstmt4.setNull(39, Types.INTEGER);
					dbPstmt4.setString(40,dbResultSet0.getString("DRIVER_ID"));
					dbPstmt4.setString(41,dbResultSet0.getString("DRIVER_NAME"));
					dbPstmt4.setString(42,dbResultSet0.getString("DRIVER_SRC"));
					dbPstmt4.addBatch();
				}
				
				dbPstmt4.executeBatch();
				logger.debug(dateStr + "告警明细数据同步入告警事件表成功！");
				//企业告警日统计
				//查询企业告警统计结果
				dbPstmt1 = dbConnection.prepareStatement(queryAlarmDaysStatSql);
				dbPstmt1.setLong(1, beginUtc);
				dbPstmt1.setLong(2, endUtc);
				dbResultSet = dbPstmt1.executeQuery();
				logger.debug(dateStr + "告警数据日统计执行成功！");
				//保存企业日统计结果
				dbPstmt2 = dbConnection.prepareStatement(saveAlarmDaysStatSql);
				int count =0 ;
				while(dbResultSet.next()){
					String tmpId = GeneratorPK.instance().getPKString();
					dbPstmt2.setString(1, tmpId);
					dbPstmt2.setLong(2, this.statDate);
					dbPstmt2.setString(3, dbResultSet.getString("VID"));
					dbPstmt2.setString(4, dbResultSet.getString("CORP_ID"));
					dbPstmt2.setString(5, dbResultSet.getString("CORP_NAME"));
					dbPstmt2.setString(6, dbResultSet.getString("TEAM_ID"));
					dbPstmt2.setString(7, dbResultSet.getString("TEAM_NAME"));
					dbPstmt2.setString(8, dbResultSet.getString("VEHICLE_NO"));
					dbPstmt2.setString(9, dbResultSet.getString("VIN_CODE"));
					dbPstmt2.setString(10, dbResultSet.getString("ALARM_CODE"));
					dbPstmt2.setLong(11, dbResultSet.getLong("ALARM_NUM"));
					dbPstmt2.setString(12, dbResultSet.getString("ALARM_CLASS"));
					
					double alarmTime = dbResultSet.getDouble("ALARM_TIME");
					if (alarmTime>=0){
						dbPstmt2.setDouble(13, dbResultSet.getDouble("ALARM_TIME")/1000);
					}else{
						dbPstmt2.setDouble(13, 0);
					}
					dbPstmt2.setLong(14, dbResultSet.getLong("MILEAGE"));
					dbPstmt2.setLong(15, dbResultSet.getLong("OIL_WEAR"));
					dbPstmt2.setNull(16, Types.VARCHAR);
					dbPstmt2.setNull(17, Types.VARCHAR);
					dbPstmt2.setString(18, dbResultSet.getString("DRIVER_ID"));
					dbPstmt2.addBatch();

					count++;
					
					if (count>=100){
						dbPstmt2.executeBatch();
						count=0;
					}
				}
				if (count>0){
					dbPstmt2.executeBatch();
				}
				
				logger.debug(dateStr + "保存告警日统计结果成功！");
				// 按企业告警级别统计
				dbPstmt0 = dbConnection.prepareCall(statAlarmDaysStatSql);
				dbPstmt0.setString(1, dateStr);
				dbPstmt0.registerOutParameter(2, Types.INTEGER);
				dbPstmt0.execute();

				int successtag = dbPstmt0.getInt(2);
				
				if (successtag == 1) {
					logger.debug(dateStr + "告警按企业告警级别日统计成功！");
				}else{
					logger.error(dateStr+ "告警按企业告警级别按日统计出错！");
				}

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("告警按企业告警级别按日统计出错：", e);
			flag = 0;
		} finally {
			try {
				if (dbResultSet != null) {
					dbResultSet.close();
				}
				if (dbPstmt0 != null) {
					dbPstmt0.close();
				}
				if (dbPstmt1 != null) {
					dbPstmt1.close();
				}
				if (dbPstmt2 != null) {
					dbPstmt2.close();
				}
				if (dbConnection != null) {
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.", e);
			}
		}
		return flag;
	}

	/**
	 * 将空值转换为空字符串
	 * 
	 * @param str
	 *            字符串
	 * @return String 返回处理后的字符串
	 */
	public static String nullToStr(String str) {
		return str == null || str.equals("null") ? "" : str.trim();
	}

}

