package com.caits.analysisserver.quartz.service.jobs.impl;

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
 * 功能： <br>车辆按日统计分析
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
public class FillStopStartDataDaysJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(FillStopStartDataDaysJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String queryStopstartInfoSql;// 访问记录按小时分析
	
	private String updateStopstartInfoSql;

//	private int count = 0;// 计数器

	private long statDate;
	private String dateStr;
	
	private long fromDate;
	private long endDate;
	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public FillStopStartDataDaysJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.dateStr = CDate.utc2Str(statDate, "yyyyMMdd");
		this.fromDate = statDate - 12*60*60*1000;

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 企业按告警类别统计
		queryStopstartInfoSql = SQLPool.getinstance().getSql(
				"sql_queryStopstartInfo");
		
		updateStopstartInfoSql = SQLPool.getinstance().getSql(
		"sql_updateStopstartInfo");

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
		Connection dbConnection = null;

		// 结果集对象
		ResultSet dbResultSet = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 按企业告警级别统计
				dbPstmt0 = dbConnection.prepareStatement(queryStopstartInfoSql);
				dbPstmt1 = dbConnection.prepareStatement(updateStopstartInfoSql);
				
				for (int i=1;i<=24;i++){//按小时补数据
					try{
					this.endDate = this.fromDate + i*60*60*1000;
					
					dbPstmt0.setLong(1, this.statDate);
					dbPstmt0.setLong(2, this.fromDate);
					dbPstmt0.setLong(3, this.endDate);
					dbResultSet = dbPstmt0.executeQuery();
					while (dbResultSet.next()) {
						//循环更新数据
						String autoId = dbResultSet.getString("AUTO_ID");
						String vid = dbResultSet.getString("VID");
						long fromDate = dbResultSet.getLong("LAUNCH_TIME");
						long endDate = dbResultSet.getLong("FIREOFF_TIME");
						
						if (fromDate>0&&endDate>0&&fromDate<endDate){
							dbPstmt1.setString(1, vid);
							dbPstmt1.setLong(2, fromDate);
							dbPstmt1.setLong(3, endDate);
							dbPstmt1.setLong(4, fromDate);
							dbPstmt1.setLong(5, endDate);
							
							dbPstmt1.setString(6, vid);
							dbPstmt1.setLong(7, fromDate);
							dbPstmt1.setLong(8, endDate);
							dbPstmt1.setLong(9, fromDate);
							dbPstmt1.setLong(10, endDate);
							
							dbPstmt1.setString(11, vid);
							dbPstmt1.setLong(12, fromDate);
							dbPstmt1.setLong(13, endDate);
							dbPstmt1.setLong(14, fromDate);
							dbPstmt1.setLong(15, endDate);
							
							dbPstmt1.setString(16, vid);
							dbPstmt1.setLong(17, fromDate);
							dbPstmt1.setLong(18, endDate);
							dbPstmt1.setLong(19, fromDate);
							dbPstmt1.setLong(20, endDate);
							
							dbPstmt1.setString(21, vid);
							dbPstmt1.setLong(22, fromDate);
							dbPstmt1.setLong(23, endDate);
							dbPstmt1.setLong(24, fromDate);
							dbPstmt1.setLong(25, endDate);
							
							dbPstmt1.setString(26, vid);
							dbPstmt1.setLong(27, fromDate);
							dbPstmt1.setLong(28, endDate);
							dbPstmt1.setLong(29, fromDate);
							dbPstmt1.setLong(30, endDate);
							
							dbPstmt1.setString(31, vid);
							dbPstmt1.setLong(32, fromDate);
							dbPstmt1.setLong(33, endDate);
							dbPstmt1.setLong(34, fromDate);
							dbPstmt1.setLong(35, endDate);
							
							dbPstmt1.setString(36, vid);
							dbPstmt1.setLong(37, fromDate);
							dbPstmt1.setLong(38, endDate);
							dbPstmt1.setLong(39, fromDate);
							dbPstmt1.setLong(40, endDate);
							
							dbPstmt1.setString(41, autoId);
							dbPstmt1.setLong(42, this.statDate);
							
							try{
							dbPstmt1.executeUpdate();
							}catch(Exception ex){
								logger.error(dateStr+" 起步停车状态及告警统计数据补数失败（VID："+vid+" fromDate："+fromDate+" endDate："+endDate+"）",ex);
							}
							
						}
					}
					this.fromDate = this.endDate;
					
					dbResultSet.close();
					logger.info(dateStr +"起步停车状态及告警需补数"+i+"时段成功！");
					Thread.sleep(2*1000);
					
					}catch(Exception ex){
						logger.error(dateStr+" 起步停车状态及告警需补数记录查询失败（ IDX："+i+"）",ex);
						dbResultSet.close();
					}
				}

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("起步停车状态及告警统计数据补数出错：", e);
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


