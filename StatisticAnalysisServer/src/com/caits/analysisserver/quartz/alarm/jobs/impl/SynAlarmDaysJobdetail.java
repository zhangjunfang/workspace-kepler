package com.caits.analysisserver.quartz.alarm.jobs.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.ctfo.memcache.beans.AlarmNum;
import com.ctfo.redis.core.RedisAdapter;
import com.ctfo.redis.util.RedisJsonUtil;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： <br>
 * 告警按企业告警类别按日统计分析 描述： <br>
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
public class SynAlarmDaysJobdetail {

	private static final Logger logger = LoggerFactory
			.getLogger(SynAlarmDaysJobdetail.class);

	private final String ALARM_DAY_KEY = "mAlarmInfoDay";
	private final String ALARM_WEEK_KEY = "mAlarmInfoWeek";
	private final String ALARM_MONTH_KEY = "mAlarmInfoMonth";

	// ------获得xml拼接的Sql语句
	private String queryAlarmDaysStatSql;// 访问记录按小时分析
	private String queryAlarmWeeksStatSql;// 访问记录按小时分析
	private String queryAlarmMonthsStatSql;// 访问记录按小时分析

//	private int count = 0;// 计数器

	private long statDate;
	private long beginTime;
	@SuppressWarnings("unused")
	private long endTime;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public SynAlarmDaysJobdetail() {
		this.statDate = CDate.getCurrentDayUTC();
		this.beginTime = statDate - 1000 * 60 * 60 * 12;
		this.endTime = statDate + 1000 * 60 * 60 * 12;

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 企业按告警类别统计
		queryAlarmDaysStatSql = SQLPool.getinstance().getSql(
				"sql_queryStatAlarmDaysStat");
		queryAlarmWeeksStatSql = SQLPool.getinstance().getSql(
				"sql_queryStatAlarmWeeksStat");
		queryAlarmMonthsStatSql = SQLPool.getinstance().getSql(
				"sql_queryStatAlarmMonthsStat");
	}

	/**
	 * 同步系统首页告警数据--日数据
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	@SuppressWarnings("unused")
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

				// 产生7日日期数组
				Long yesterday = CDate.getYesDayUTC();
				String dates[] = new String[7];
				for (int i = 6; i >= 0; i--) {
					dates[i] = CDate.utc2Str(yesterday
							- ((6 - i) * 24 * 60 * 60 * 1000), "yyyy-MM-dd");
				}

				OrgValueMap ovmDays = new OrgValueMap(dates);

				// 查询最近七日告警统计信息
				dbPstmt0 = dbConnection.prepareStatement(queryAlarmDaysStatSql);
				dbPstmt0.setLong(1, yesterday - 6 * 24 * 60 * 60 * 1000);
				dbPstmt0.setLong(2, CDate.getCurrentDayUTC());
				dbResultSet = dbPstmt0.executeQuery();

				while (dbResultSet.next()) {
					AlarmNum alarm = new AlarmNum();
					String entId = dbResultSet.getString("ENT_ID");
					Long date0 = dbResultSet.getLong("STAT_DATE");
					String date = CDate.utc2Str(date0, "yyyy-MM-dd");
					alarm.setAlarmDate(date);
					alarm.setGeneralCount(dbResultSet.getLong("GENERALCOUNT")
							+ ""); // 一般报警
					alarm.setSeriousCount(dbResultSet.getLong("SERIOUSCOUNT")
							+ ""); // 严重报警
					alarm.setSuggestionCount(dbResultSet
							.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
					alarm.setUrgentCount(dbResultSet.getLong("URGENTCOUNT")
							+ ""); // 紧急报警

					ovmDays.putHm(entId, date, alarm);
				}

				// 产生最近七周数组
				Long pre7 = yesterday - 6 * 24 * 60 * 60 * 1000;
				String year = CDate.utc2Str(yesterday, "yyyy");
				int previousweek = CDate.getPreviousWeek();
				String weeks[] = new String[7];
				for (int i = 0; i < 7; i++) {
					if (previousweek == 0) {
						int preyear = CDate.getPreviousYear();
						previousweek = CDate.getDaysWeek(CDate.strToDateByFormat(preyear+"1231", "yyyyMMdd"));
						if (previousweek==1){
							previousweek = CDate.getDaysWeek(CDate.strToDateByFormat(preyear+"1224", "yyyyMMdd"));;
						}
						year = "" + preyear;
					}
					weeks[i] = year + "-" + StringUtils.leftPad(""+previousweek, 2,"0");
					
					if (i<6){
						previousweek = previousweek - 1;
					}
				}

				OrgValueMap ovmWeeks = new OrgValueMap(weeks);

				// 查询最近七周告警统计信息
				dbPstmt1 = dbConnection
						.prepareStatement(queryAlarmWeeksStatSql);
				dbPstmt1.setLong(1, CDate.getFirstDayOfWeek(Integer.parseInt(year),previousweek).getTime());
				dbPstmt1.setLong(2, CDate.getCurrentDayUTC());
				dbResultSet = dbPstmt1.executeQuery();

				while (dbResultSet.next()) {
					AlarmNum alarm = new AlarmNum();
					String entId = dbResultSet.getString("ENT_ID");
					String year0 = dbResultSet.getString("STAT_YEAR");
					String week0 = dbResultSet.getString("STAT_WEEK");
					alarm.setAlarmDate(year0 + "-" + StringUtils.leftPad(week0, 2,"0"));
					alarm.setGeneralCount(dbResultSet.getLong("GENERALCOUNT")
							+ ""); // 一般报警
					alarm.setSeriousCount(dbResultSet.getLong("SERIOUSCOUNT")
							+ ""); // 严重报警
					alarm.setSuggestionCount(dbResultSet
							.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
					alarm.setUrgentCount(dbResultSet.getLong("URGENTCOUNT")
							+ ""); // 紧急报警

					ovmWeeks.putHm(entId, year0 + "-" + StringUtils.leftPad(week0, 2,"0"), alarm);
				}

				// 产生最近七月数组
				String preMonthy = CDate.utc2Str(CDate.getPreviousMonthUtc(),
						"yyyy");
				int previousmonth = CDate.getPreviousMonth();
				String months[] = new String[7];
				for (int i = 0; i < 7; i++) {
					
					if (previousmonth == 0) {
						int preyear = CDate.getPreviousYear();
						previousmonth = 12;
						preMonthy = "" + preyear;
					}
					if (previousmonth<10){
						months[i] = preMonthy + "-0" + previousmonth;
					}else{
						months[i] = preMonthy + "-" + previousmonth;
					}
					
					
					previousmonth = previousmonth - 1;
				}

				OrgValueMap ovmMonths = new OrgValueMap(months);

				// 查询最近七周告警统计信息
				dbPstmt2 = dbConnection
						.prepareStatement(queryAlarmMonthsStatSql);
				dbPstmt2.setLong(
						1,
						CDate.getFirstDayOfMonth(Integer.parseInt(preMonthy),previousmonth
								).getTime());
				dbPstmt2.setLong(
						2,
						CDate.getFirstDayOfMonth(CDate.getCurrentYear(),CDate.getCurrentMonth()
								).getTime());
				dbResultSet = dbPstmt2.executeQuery();

				while (dbResultSet.next()) {
					AlarmNum alarm = new AlarmNum();
					String entId = dbResultSet.getString("ENT_ID");
					String year0 = dbResultSet.getString("STAT_YEAR");
					String month0 = dbResultSet.getString("STAT_MONTH");
					alarm.setAlarmDate(year0 + "-" + month0);
					alarm.setGeneralCount(dbResultSet.getLong("GENERALCOUNT")
							+ ""); // 一般报警
					alarm.setSeriousCount(dbResultSet.getLong("SERIOUSCOUNT")
							+ ""); // 严重报警
					alarm.setSuggestionCount(dbResultSet
							.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
					alarm.setUrgentCount(dbResultSet.getLong("URGENTCOUNT")
							+ ""); // 紧急报警

					ovmMonths.putHm(entId, year0 + "-" + month0, alarm);
				}

				// 同步数据到redis

				RedisAdapter.setAlarmTrends(ALARM_DAY_KEY,
						RedisJsonUtil.objectToJson(ovmDays.getMap()));
				RedisAdapter.setAlarmTrends(ALARM_WEEK_KEY,
						RedisJsonUtil.objectToJson(ovmWeeks.getMap()));
				RedisAdapter.setAlarmTrends(ALARM_MONTH_KEY,
						RedisJsonUtil.objectToJson(ovmMonths.getMap()));

				logger.info(beginTime + "同步首页告警统计信息成功！");

				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("同步首页告警统计信息出错：", e);
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
