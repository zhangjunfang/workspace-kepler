package com.caits.analysisserver.quartz.service.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Types;

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
public class StatFactoryWeekstatJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(StatFactoryWeekstatJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String statFactoryWeeksStatSql;// 删除企业月车辆运营情况

//	private int count = 0;// 计数器

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
	public StatFactoryWeekstatJobdetail(int year,int weekOfYear) {
		this.year=year;
		this.week = weekOfYear;
		this.beginTime = CDate.getFirstDayOfWeek(year,weekOfYear).getTime();//周1 0点对应时间
		this.endTime = CDate.getLastDayOfWeek(year,weekOfYear).getTime()+1000*60*60*24; //下周1 0点对应时间

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 删除企业月车辆运营情况
		statFactoryWeeksStatSql = SQLPool.getinstance().getSql(
				"sql_procStatFactoryWeeks");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		CallableStatement dbPstmt1 = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 按企业告警级别进行周统计
				dbPstmt1 = dbConnection.prepareCall(statFactoryWeeksStatSql);
				dbPstmt1.setLong(1, week);
				dbPstmt1.setLong(2, beginTime);
				dbPstmt1.setLong(3, endTime);
				dbPstmt1.registerOutParameter(4, Types.INTEGER);
				dbPstmt1.execute();

				int weeksuccesstag = dbPstmt1.getInt(4);
				
				if (weeksuccesstag == 1) {
					logger.debug(year + "年度第"+week+"周 车厂系统指标周统计成功！");
				}else{
					logger.error(year+ "年度第"+week+"周 车厂系统指标周统计出错！");
				}

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("车厂系统指标周统计信息出错：", e);
			flag = 0;
		} finally {
			try {
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