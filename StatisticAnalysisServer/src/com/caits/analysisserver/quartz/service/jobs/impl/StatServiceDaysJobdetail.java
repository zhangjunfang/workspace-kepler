package com.caits.analysisserver.quartz.service.jobs.impl;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.statisticanalysis.GradeStatisticThread;
import com.caits.analysisserver.addin.kcpt.statisticanalysis.SummaryVehicleDay;
import com.caits.analysisserver.bean.SystemBaseInfo;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.Utils;

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
public class StatServiceDaysJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(StatServiceDaysJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String statServiceDaysStatSql;// 访问记录按小时分析

//	private int count = 0;// 计数器

	private long statDate;
	private String dateStr;
	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public StatServiceDaysJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.dateStr = CDate.utc2Str(statDate, "yyyyMMdd");

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 企业按告警类别统计
		statServiceDaysStatSql = SQLPool.getinstance().getSql(
				"sql_procStatServiceDays");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		CallableStatement dbPstmt0 = null;
//		CallableStatement dbPstmt1 = null;
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
				dbPstmt0 = dbConnection.prepareCall(statServiceDaysStatSql);
				dbPstmt0.setString(1, dateStr);
				dbPstmt0.registerOutParameter(2, Types.INTEGER);
				dbPstmt0.execute();

				int successtag = dbPstmt0.getInt(2);
				
				if (successtag == 1) {
					logger.debug(dateStr + "车辆按日统计成功！");
					
					// 更新总累计表
					SummaryVehicleDay summaryVehicleDay = new SummaryVehicleDay();
					summaryVehicleDay.setTime(statDate);
					summaryVehicleDay.initAnalyser();
					summaryVehicleDay.run();
					
					//启动评分统计分析线程
					GradeStatisticThread gradeStatisticThread = new GradeStatisticThread();
					gradeStatisticThread.initAnalyser();
					gradeStatisticThread.run();
					
					// 任务结束通知报表同步服务同步数据
					SystemBaseInfo sys = SystemBaseInfoPool.getinstance().getBaseInfoMap("reportSystem");
					if(sys != null){
						if(sys.getIsLoad().equals("true")){ 
							try {
								String res = Utils.Post(sys.getValue());
								if(res.trim().matches("success!")){
									logger.info("Running synchronization services of report system to successful！URL:" + sys.getValue());
								}else{
									logger.info("Running synchronization services of report system to failed！URL:" + sys.getValue());
								}
							} catch (Exception e) {
								logger.error("通知报表同步服务出错.",e);
							}
						}
					}
					
				}else{
					logger.error(dateStr+ "车辆按日统计出错！");
				}

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("车辆按日统计出错：", e);
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


