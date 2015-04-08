package com.caits.analysisserver.quartz.visitlog.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
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
 * 功能： <br>访问记录按小时、按日分析
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
public class AnalyserVisitlogDaysJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(AnalyserVisitlogDaysJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String statAnalyserVisitlogHoursStatSql;// 访问记录按小时分析
	private String statAnalyserVisitlogDaysStatSql;// 访问记录按日分析

	@SuppressWarnings("unused")
	private int count = 0;// 计数器

	private long statDate;
	private String dateStr;
	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public AnalyserVisitlogDaysJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.dateStr = CDate.utc2Str(statDate, "yyyyMMdd");

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 按小时分析访问记录
		statAnalyserVisitlogHoursStatSql = SQLPool.getinstance().getSql(
				"sql_procAnalyserVisitlogHoursStat");
		// 按日分析访问记录
		statAnalyserVisitlogDaysStatSql = SQLPool.getinstance().getSql(
				"sql_procAnalyserVisitlogDaysStat");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		CallableStatement dbPstmt0 = null;
		CallableStatement dbPstmt1 = null;
		Connection dbConnection = null;

		// 结果集对象
		ResultSet dbResultSet = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 按小时统计访问记录
				dbPstmt0 = dbConnection.prepareCall(statAnalyserVisitlogHoursStatSql);
				dbPstmt0.setString(1, dateStr);
				dbPstmt0.registerOutParameter(2, Types.INTEGER);
				dbPstmt0.execute();

				int successtag0 = dbPstmt0.getInt(2);
				
				if (successtag0 == 1) {
					logger.debug(dateStr + "访问记录按小时分析成功！");
				}else{
					logger.error("{}访问记录按小时分析出错！错误代码[{}]", dateStr , successtag0);
				}
				
				// 按日统计访问记录
				dbPstmt1 = dbConnection.prepareCall(statAnalyserVisitlogDaysStatSql);
				dbPstmt1.setString(1, dateStr);
				dbPstmt1.registerOutParameter(2, Types.INTEGER);
				dbPstmt1.execute();

				int successtag = dbPstmt1.getInt(2);
				
				if (successtag == 1) {
					logger.debug(dateStr + "访问记录按日分析成功！");
				}else{
					logger.error(dateStr+ "访问记录按日分析出错！错误代码:" + successtag);
				}

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("生成企业车辆日告警情况统计信息出错：", e);
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
