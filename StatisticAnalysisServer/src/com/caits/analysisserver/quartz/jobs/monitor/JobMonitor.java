package com.caits.analysisserver.quartz.jobs.monitor;

	import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Date;

import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.ctfo.memcache.beans.JobInfo;

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
	public class JobMonitor {

		private static final Logger logger = LoggerFactory
				.getLogger(JobMonitor.class);
		
		private static final JobMonitor jobMonitor = new JobMonitor();

		// ------获得xml拼接的Sql语句
		private String queryCurrentJobSql;// 查询当前作业状态信息
		private String queryJobDependSql;// 查询依赖作业状态信息
		private String updateJobRunningMonitorSql;//更新作业状态信息

		/**
		 * 初始化统计周期：传入日期
		 * 
		 * @param statDate
		 *            当日12点日期时间
		 */
		public JobMonitor() {
			this.initAnalyser();
		}
		
		public static JobMonitor getInstance(){
			return jobMonitor;
		}

		// 初始化方法
		public void initAnalyser() {
			// 查询当前作业状态信息
			queryCurrentJobSql = SQLPool.getinstance().getSql(
					"sql_queryCurrentJobSql");
			// 查询依赖作业状态信息
			queryJobDependSql = SQLPool.getinstance().getSql(
					"sql_queryJobDependSql");
			// 更新作业状态信息
			updateJobRunningMonitorSql = SQLPool.getinstance().getSql(
					"sql_updateJobRunningMonitorSql");
		}
		
		/**
		 * 查询当前作业信息 
		 * 
		 * @param
		 * @return JobInfo null:执行失败, jobinfo 执行成功
		 */
		public JobInfo queryCurrentJob(String jobName) {
			PreparedStatement dbPstmt0 = null;
			Connection dbConnection = null;

			// 结果集对象
			ResultSet dbResultSet = null;
			
			JobInfo jobInfo = null;

			// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
//			int flag = 2;
			try {
				// 获得Connection对象
				dbConnection = OracleConnectionPool.getConnection();
				if (dbConnection != null) {

					// 查询当前作业状态信息
					dbPstmt0 = dbConnection.prepareStatement(queryCurrentJobSql);
					dbPstmt0.setString(1, jobName);
					dbResultSet = dbPstmt0.executeQuery();

					while (dbResultSet.next()) {
						jobInfo = new JobInfo();
						jobInfo.setJobName(dbResultSet.getString("JOB_NAME"));
						jobInfo.setJobDesc(dbResultSet.getString("JOB_DESC"));
						jobInfo.setJobDepend(dbResultSet.getString("JOB_DEPEND"));
						jobInfo.setResultStatus(dbResultSet.getString("RESULT_STATUS"));
						jobInfo.setTriggerTime(dbResultSet.getTimestamp("TRIGGER_TIME"));
						jobInfo.setFinishTime(dbResultSet.getTimestamp("FINISH_TIME"));
						jobInfo.setUseTime(dbResultSet.getLong("USE_TIME"));
						break;
					}

					//logger.debug("JOB "+ jobName +"依赖作业"+count+"项,执行结果为(0、失败 1、成功 2、等待)："+flag);
				} else {
					logger.debug("获取数据库链接失败");
				}
			} catch (Exception e) {
				logger.error("查询作业依赖关系时出错：", e);
				jobInfo = null;
			} finally {
				try {
					if (dbResultSet != null) {
						dbResultSet.close();
					}
					if (dbPstmt0 != null) {
						dbPstmt0.close();
					}
					if (dbConnection != null) {
						dbConnection.close();
					}
				} catch (SQLException e) {
					logger.error("连接放回连接池出错.", e);
				}
			}
			return jobInfo;
		}

		/**
		 * 查询作业依赖是否执行完成 1 成功 0 失败  2 等待
		 * 
		 * @param
		 * @return int 0:执行失败, 1执行成功
		 */
		public int queryJobDependStatus(String jobName) {
			PreparedStatement dbPstmt0 = null;
			Connection dbConnection = null;

			// 结果集对象
			ResultSet dbResultSet = null;

			// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
			int flag = 2;
			int count = 0;
			try {
				// 获得Connection对象
				dbConnection = OracleConnectionPool.getConnection();
				if (dbConnection != null) {

					// 删除当日企业运营情况统计结果
					dbPstmt0 = dbConnection.prepareStatement(queryJobDependSql);
					dbPstmt0.setString(1, jobName);
					dbResultSet = dbPstmt0.executeQuery();

					while (dbResultSet.next()) {
						count++;
						String resultStatus = dbResultSet.getString("RESULT_STATUS");
//						Date endTime = dbResultSet.getTimestamp("FINISH_TIME");
						//logger.info("endtime:::"+endTime);
						if (resultStatus==null){
							flag = 2;
							break;
						}else if ("0".equals(resultStatus)){
							flag = 0;
							break;
						}else if ("1".equals(resultStatus)){
							flag = 1;
						}
					}
					
					//当没有查出结果时表示此job无依赖关系
					if (count == 0) {
						flag =1;
					}
					logger.debug("JOB "+ jobName +"依赖作业"+count+"项,执行结果为(0、失败 1、成功 2、等待)："+flag);
				} else {
					logger.debug("获取数据库链接失败");
				}
			} catch (Exception e) {
				logger.error("查询作业依赖关系时出错：", e);
				flag = 0;
			} finally {
				try {
					if (dbResultSet != null) {
						dbResultSet.close();
					}
					if (dbPstmt0 != null) {
						dbPstmt0.close();
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
		 * 修改作业执行情况
		 * 
		 * @param
		 * @return int 0:执行失败, 1执行成功
		 */
		public int updateJobRunningMonitor(String jobName,String result,Date triggerTime,Date finishTime,long useTime) {
			PreparedStatement dbPstmt0 = null;
			Connection dbConnection = null;

			// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
			int flag = 0;
//			int count = 0;
			try {
				// 获得Connection对象
				dbConnection = OracleConnectionPool.getConnection();
				if (dbConnection != null) {
					// 修改作业执行情况
					dbPstmt0 = dbConnection.prepareStatement(updateJobRunningMonitorSql);
					((OraclePreparedStatement) dbPstmt0).setExecuteBatch(1);
					dbPstmt0.setString(1, result);

					if (triggerTime!=null){
						dbPstmt0.setTimestamp(2, new java.sql.Timestamp(triggerTime.getTime()));
					}else{
						dbPstmt0.setNull(2, Types.NULL);
					}
					if (finishTime!=null){
						dbPstmt0.setTimestamp(3, new java.sql.Timestamp(finishTime.getTime()));
					}else{
						dbPstmt0.setNull(3, Types.NULL);
					}
					if (useTime<0){
						dbPstmt0.setNull(4, Types.NULL);
					}else{
						dbPstmt0.setLong(4, useTime);
					}
					
					dbPstmt0.setString(5, jobName);
					
					dbPstmt0.executeUpdate();
					
					flag =1;
					//当没有查出结果时表示此job无依赖关系
					logger.debug("JOB "+ jobName +"执行结果(0、失败 1、成功 2、等待)更新为："+result+"成功！");
				} else {
					logger.debug("获取数据库链接失败");
				}
			} catch (Exception e) {
				logger.error("更新作业执行结果出错：", e);
				flag = 0;
			} finally {
				try {
					if (dbPstmt0 != null) {
						dbPstmt0.close();
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

