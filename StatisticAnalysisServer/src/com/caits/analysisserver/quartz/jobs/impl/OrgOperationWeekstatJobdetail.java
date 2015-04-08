package com.caits.analysisserver.quartz.jobs.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Date;
import java.util.UUID;

import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： <br>
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
public class OrgOperationWeekstatJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(OrgOperationWeekstatJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String delOrgOpterationWeekstatSql;// 删除企业月车辆运营情况
	private String queryOrgOperationWeekstatSql;// 统计企业月车辆运营情况
	private String saveOrgOpterationWeekstatSql; // 保存企业月车辆运营情况

	private int count = 0;// 计数器

	private int year;
	private int week;
	private long beginTime;
	private long endTime;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public OrgOperationWeekstatJobdetail(int year,int weekOfYear) {
		this.year=year;
		this.week = weekOfYear;
		this.beginTime = CDate.getFirstDayOfWeek(year,weekOfYear).getTime();//周1 0点对应时间
		this.endTime = CDate.getLastDayOfWeek(year,weekOfYear).getTime()+1000*60*60*24; //下周1 0点对应时间

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 删除企业月车辆运营情况
		delOrgOpterationWeekstatSql = SQLPool.getinstance().getSql(
				"sql_delOrgOpterationWeekstatSql");
		// 统计企业月车辆运营情况
		queryOrgOperationWeekstatSql = SQLPool.getinstance().getSql(
				"sql_queryOrgOperationWeekstatSql");
		// 保存企业月车辆运营情况
		saveOrgOpterationWeekstatSql = SQLPool.getinstance().getSql(
				"sql_saveOrgOpterationWeekstatSql");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		PreparedStatement dbPstmt0 = null;
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt2 = null;
		PreparedStatement dbPstmt3 = null;
		Connection dbConnection = null;

		// 结果集对象
		ResultSet dbResultSet = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 删除7日前车辆运营属性，即此数据保留7日
				/*
				 * dbPstmt0 = dbConnection
				 * .prepareStatement(delOrgOpterationDaystatSql);
				 * dbPstmt0.setLong(1, statDate - 1000 * 60 * 60 * 24 * 7);
				 * dbPstmt0.executeUpdate();
				 */

				// 删除当月企业运营情况统计结果
				dbPstmt1 = dbConnection
						.prepareStatement(delOrgOpterationWeekstatSql);
				dbPstmt1.setLong(1, beginTime);
				dbPstmt1.executeUpdate();

				// 查询当月企业车辆运营情况
				dbPstmt2 = dbConnection
						.prepareStatement(queryOrgOperationWeekstatSql);
				dbPstmt2.setLong(1, beginTime);
				dbPstmt2.setLong(2, endTime);
				dbPstmt2.setLong(3, beginTime);
				dbPstmt2.setLong(4, endTime);
				dbPstmt2.setLong(5, beginTime);
				dbPstmt2.setLong(6, endTime);
				dbPstmt2.setLong(7, beginTime);
				dbPstmt2.setLong(8, endTime);
				dbPstmt2.setLong(9, beginTime);
				dbPstmt2.setLong(10, endTime);
				dbResultSet = dbPstmt2.executeQuery();

				while (dbResultSet.next()) {
					String entId = dbResultSet.getString("ENT_ID");
					long totalVehicle = dbResultSet.getLong("TOTAL_VEHICLE");
					long addVehicle = dbResultSet.getLong("ADD_VEHICLE");
					long mechanicalVehicle = dbResultSet
							.getLong("MECHANICAL_VEHICLE");
					long operationNum = dbResultSet.getLong("OPERATION_NUM");
					long onlineNum = dbResultSet.getLong("ONLINE_NUM");
					Double averageRateOnline = dbResultSet
							.getDouble("AVERAGE_RATE_ONLINE");
					Double totalMileage = dbResultSet
							.getDouble("TOTAL_MILEAGE");
					Double mechanicalVehicleMileage = dbResultSet
							.getDouble("MECHANICAL_VEHICLE_MILEAGE");
					Double mechanicalVehicleOil = dbResultSet
							.getDouble("MECHANICAL_VEHICLE_OIL");
					Double oilBoxAnomalous = dbResultSet
							.getDouble("OIL_BOX_ANOMALOUS");

					dbPstmt3 = dbConnection
							.prepareStatement(saveOrgOpterationWeekstatSql);
					((OraclePreparedStatement) dbPstmt3).setExecuteBatch(100);

					dbPstmt3.setString(1, UUID.randomUUID().toString().replace("-", ""));
					dbPstmt3.setString(2, entId);
					dbPstmt3.setLong(3, week);
					dbPstmt3.setLong(4, year);
					dbPstmt3.setLong(5, beginTime);
					dbPstmt3.setLong(6, totalVehicle);
					dbPstmt3.setLong(7, addVehicle);
					dbPstmt3.setLong(8, mechanicalVehicle);
					dbPstmt3.setLong(9, operationNum);
					dbPstmt3.setLong(10, onlineNum);
					dbPstmt3.setDouble(11, averageRateOnline);
					dbPstmt3.setDouble(12, totalMileage);
					dbPstmt3.setDouble(13, mechanicalVehicleMileage);
					dbPstmt3.setDouble(14, mechanicalVehicleOil);
					dbPstmt3.setDouble(15, oilBoxAnomalous);

					dbPstmt3.executeUpdate();
					count++;
				}
				if (dbPstmt3!=null){
				((OraclePreparedStatement) dbPstmt3).sendBatch();
				}
				Date dt = new Date();
				dt.setTime(this.beginTime);
				if (count == 0) {
					logger.error(CDate.dateToStr(dt)
							+ "企业车辆月运营情况统计信息生成出错！生成结果为 0！");
				}else{
				logger.debug(CDate.dateToStr(dt) + "企业车辆月运营情况统计信息生成成功！");
				}
				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("生成企业车辆月运营情况统计信息出错：", e);
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
				if (dbPstmt3 != null) {
					dbPstmt3.close();
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