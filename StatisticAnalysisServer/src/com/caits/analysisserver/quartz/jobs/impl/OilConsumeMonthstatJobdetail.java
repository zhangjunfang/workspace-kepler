package com.caits.analysisserver.quartz.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Types;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;

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
public class OilConsumeMonthstatJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(OilConsumeMonthstatJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String statOilConsumeMonthstatSql; // 保存企业日车辆告警情况

//	private int count = 0;// 计数器

	/*private long statDate;
	private long beginTime;
	private long endTime;*/
	private String statMonth;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public OilConsumeMonthstatJobdetail(int year,int month) {
		/*this.beginTime = CDate.getFirstDayOfMonth(year,month).getTime();//当月第一天零点
		this.endTime = CDate.getDateFromParam(CDate.getFirstDayOfMonth(year,month),0,1,1,0,0,0,0).getTime();//下月第一天零点
		 */
		if (month<10){
			this.statMonth = year+"0"+month;
		}else{
			this.statMonth = year+""+month;
		}
		
		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 调用燃油消耗月统计存储过程
		statOilConsumeMonthstatSql = SQLPool.getinstance().getSql(
				"sql_procOilConsumeMonthStat");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		//PreparedStatement dbPstmt0 = null;
		CallableStatement dbPstmt1 = null;
		/*PreparedStatement dbPstmt2 = null;
		PreparedStatement dbPstmt3 = null;*/
		Connection dbConnection = null;

		// 结果集对象
		//ResultSet dbResultSet = null;

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

				// 统计当月燃油消耗情况
				dbPstmt1 = dbConnection
						.prepareCall(statOilConsumeMonthstatSql);
				dbPstmt1.setString(1, statMonth);
				dbPstmt1.registerOutParameter(2, Types.INTEGER);
				dbPstmt1.execute();

				int successtag = dbPstmt1.getInt(2);

				if (successtag == 1) {
					logger.debug(statMonth + "车辆燃油消耗月统计信息生成成功！");
				}else{
					logger.error(statMonth+ "车辆燃油消耗月统计统计信息生成出错！");
				}
				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("生成车辆燃油消耗月统计信息出错：", e);
			flag = 0;
		} finally {
			try {
				/*if (dbResultSet != null) {
					dbResultSet.close();
				}
				if (dbPstmt0 != null) {
					dbPstmt0.close();
				}*/
				if (dbPstmt1 != null) {
					dbPstmt1.close();
				}
				/*if (dbPstmt2 != null) {
					dbPstmt2.close();
				}
				if (dbPstmt3 != null) {
					dbPstmt3.close();
				}*/
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
