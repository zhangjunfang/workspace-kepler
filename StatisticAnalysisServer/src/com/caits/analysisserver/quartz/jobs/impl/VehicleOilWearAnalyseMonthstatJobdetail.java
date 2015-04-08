package com.caits.analysisserver.quartz.jobs.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
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
public class VehicleOilWearAnalyseMonthstatJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(VehicleOilWearAnalyseMonthstatJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String delvehicleoilwearanalysemonthstatSql;// 删除车辆周油耗分析情况
	private String addvehicleoilwearanalysemonthstatSql;// 统计车辆周油耗分析情况


//	private int count = 0;// 计数器

//	private int year;
//	private int week;
	private long beginTime;
	private long endTime;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public VehicleOilWearAnalyseMonthstatJobdetail(int year,int month) {
		this.beginTime = CDate.getFirstDayOfMonth(year,month).getTime();//当月第一天零点
		this.endTime = CDate.getDateFromParam(CDate.getFirstDayOfMonth(year,month),0,1,1,0,0,0,0).getTime();//下月第一天零点

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 删除企业周车辆运营情况
		delvehicleoilwearanalysemonthstatSql = SQLPool.getinstance().getSql(
				"sql_delvehicleoilwearanalysemonthstatSql");
		// 统计企业周车辆运营情况
		addvehicleoilwearanalysemonthstatSql = SQLPool.getinstance().getSql(
				"sql_addvehicleoilwearanalysemonthstatSql");

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

				// 删除上月车辆油耗统计结果
				dbPstmt1 = dbConnection
						.prepareStatement(delvehicleoilwearanalysemonthstatSql);
				dbPstmt1.setLong(1, beginTime);
				dbPstmt1.setLong(2, endTime);
				dbPstmt1.executeUpdate();

				// 生成上月企业车辆油耗统计情况
				dbPstmt2 = dbConnection
						.prepareStatement(addvehicleoilwearanalysemonthstatSql);
				dbPstmt2.setLong(1, beginTime);
				dbPstmt2.setLong(2, beginTime);
				dbPstmt2.setLong(3, endTime);
				dbPstmt2.executeUpdate();

				Date dt = new Date();
				dt.setTime(this.beginTime);

				logger.debug(CDate.dateToStr(dt) + "企业车辆月油耗情况统计信息生成成功！");
				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("生成企业车辆月油耗情况统计信息出错：", e);
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