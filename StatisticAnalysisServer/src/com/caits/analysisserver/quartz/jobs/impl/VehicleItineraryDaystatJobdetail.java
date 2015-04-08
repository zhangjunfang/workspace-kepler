package com.caits.analysisserver.quartz.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Date;

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
public class VehicleItineraryDaystatJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(VehicleItineraryDaystatJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String delTransportDaysSql;// 删除当日车辆趟次统计结果
	private String statTransportDetailSql;// 车辆运行趟次明细统计
	private String statTransportDaysSql; // 车辆运行趟次日统计
	
	private String tangciqufenSql;

//	private int count = 0;// 计数器

	private long statDate;
	private long beginTime;
	private long endTime;
	private String dateStr;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public VehicleItineraryDaystatJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.beginTime = statDate - 1000 * 60 * 60 * 12;
		this.endTime = statDate + 1000 * 60 * 60 * 12;
		this.dateStr = CDate.utc2Str(statDate, "yyyyMMdd");

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 删除当日车辆趟次统计结果
		delTransportDaysSql = SQLPool.getinstance().getSql(
				"sql_delTransportDays");
		// 车辆运行趟次明细统计
		statTransportDetailSql = SQLPool.getinstance().getSql(
				"sql_statTransportDetail");
		// 车辆运行趟次日统计
		statTransportDaysSql = SQLPool.getinstance().getSql(
				"sql_statTransportDays");
		tangciqufenSql= SQLPool.getinstance().getSql(
		"sql_tangciqufen");

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
		CallableStatement dbPstmt3 = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 删除当日车辆趟次统计结果，重复统计时防止数据重复
				 dbPstmt0 = dbConnection.prepareStatement(delTransportDaysSql);
				 dbPstmt0.setLong(1, beginTime);
				 dbPstmt0.setLong(2, endTime);
				 dbPstmt0.setLong(3, beginTime);
				 dbPstmt0.setLong(4, endTime);
				 dbPstmt0.executeUpdate();
				 
				 //调用存储过程为区分趟次
				 dbPstmt3 = dbConnection.prepareCall(tangciqufenSql);
				 dbPstmt3.setString(1,dateStr);
				 dbPstmt3.registerOutParameter(2, Types.INTEGER);
				 dbPstmt3.execute();
				

				// 车辆趟次明细统计
				dbPstmt1 = dbConnection.prepareStatement(statTransportDetailSql);
				dbPstmt1.setLong(1, beginTime);
				dbPstmt1.setLong(2, endTime);
				dbPstmt1.executeUpdate();

				// 车辆趟次日统计
				dbPstmt2 = dbConnection.prepareStatement(statTransportDaysSql);
				dbPstmt2.setLong(1, beginTime);
				dbPstmt2.setLong(2, endTime);
				dbPstmt2.executeUpdate();

				Date dt = new Date();
				dt.setTime(this.statDate);
				logger.info(CDate.dateToStr(dt)
						+ "车辆趟次统计信息生成！");

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("车辆趟次统计信息生成出错：", e);
			flag = 0;
		} finally {
			try {
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

