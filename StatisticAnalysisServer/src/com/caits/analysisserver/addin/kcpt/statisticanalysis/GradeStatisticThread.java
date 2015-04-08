package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： GradeStaistic <br>
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
 * <td>2011-10-18</td>
 * <td>ningdh</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author ningdh
 * @since JDK1.6
 */
public class GradeStatisticThread {
	private static final Logger logger = LoggerFactory.getLogger(GradeStatisticThread.class);

	// -------声明Connection
	private Connection dbConnection;

	// -------节油部分 默认值
	private int oilOverSpeed_100 = 0; // 超速100分阀值
	private int oilOverSpeed_0 = 20; // 超速0分阀值

	private int oilOverRpm_100 = 0; // 超转100分阀值
	private int oilOverRpm_0 = 20; // 超转0分阀值

	private int oilLongIdle_100 = 0; // 超长怠速100分阀值
	private int oilLongIdle_0 = 7500; // 超长怠速0分阀值

	private int oilGearGlide_100 = 0; // 空档滑行100分阀值
	private int oilGearGlide_0 = 240; // 空档滑行0分阀值

/*	private int oilUrgent_100 = 0; // 急加急减速100分阀值
	private int oilUrgent_0 = 12; // 急加急减速0分阀值
*/	 
	private int oilUrgentSpeed_100 = 0; // 急加速100分阀值
	private int oilUrgentSpeed_0 = 12; // 急加速0分阀值
	
	private int oilUrgentLowdown_100 = 0; // 急减速100分阀值
	private int oilUrgentLowdown_0 = 12; // 急减速0分阀值

	private int oilAirCondition_100 = 0; // 怠速空调100分阀值
	private int oilAirCondition_0 = 3600; // 怠速空调0分阀值

	private int oilEconomicRun_100 = 0; // 超经济区100分阀值 单位%
	private int oilEconomicRun_0 = 48; // 超经济区0分阀值 单位%

	// ------安全部分
	private int safeOverSpeed_100 = 0; // 超速100分阀值
	private int safeOverSpeed_0 = 7; // 超速0分阀值

	private int safeGearGlide_100 = 0; // 空档滑行100分阀值
	private int safeGearGlide_0 = 240; // 空档滑行0分阀值

	/*private int safeUrgent_100 = 6; // 急加急减速100分阀值
	private int safeUrgent_0 = 12; // 急加急减速0分阀值
*/
	
	private int safeUrgentSpeed_100 = 0; // 急加速100分阀值
	private int safeUrgentSpeed_0 = 12; // 急加速0分阀值
	
	private int safeUrgentLowdown_100 = 0; // 急减速100分阀值
	private int safeUrgentLowdown_0 = 12; // 急减速0分阀值
	
	private int safeFatigue_100 = 0; // 疲劳驾驶100分阀值
	private int safeFatigue_0 = 1; // 疲劳驾驶0分阀值

	// ------获得xml拼接的Sql语句
	private String queryCheckMonthSetSql; // 查询考核月度设置表tb_checkmonth_set,开始时间，结束时间
	private String delGradeMonthStatSql; // 删除企业统计月的车辆评分结果表TS_GRADE_MONTHSTAT
	private String queryVehicleDayStatSql; // 查询车辆日统计信息TS_VEHICLE_DAYSTAT表数据
	private String insertGradeMonthStatSql; // 插入数据车辆评分结果表TS_GRADE_MONTHSTAT表
	
	private String accountGradeMonthCorpSql; // 统计企业月度考核
	private String accountGradeMonthTeamSql; // 统计车队月度考核
	

	public void run() {
		logger.info("月度绩效评分线程！");
		executeGradeMonthStat();
		if(dbConnection != null){
			try {
				dbConnection.close();
			} catch (SQLException e) {
				logger.error("关闭连接出错。",e);
			}
		}
	}

	// 初始化方法
	public void initAnalyser() throws Exception {
		// 获得Connection对象
		dbConnection = OracleConnectionPool.getConnection();

		// 查询考核月度设置表tb_checkmonth_set,开始时间，结束时间
		queryCheckMonthSetSql = SQLPool.getinstance().getSql("sql_queryCheckMonthSetSql");

		// 删除企业统计月的车辆评分结果表TS_GRADE_MONTHSTAT
		delGradeMonthStatSql = SQLPool.getinstance().getSql("sql_delGradeMonthStatSql");

		// 查询车辆日统计信息TS_VEHICLE_DAYSTAT表数据
		queryVehicleDayStatSql = SQLPool.getinstance().getSql("sql_queryVehicleDayStatSql");

		// 插入数据车辆评分结果表TS_GRADE_MONTHSTAT表
		insertGradeMonthStatSql = SQLPool.getinstance().getSql("sql_insertGradeMonthStatSql");
		
		// 统计企业月度考核
		accountGradeMonthCorpSql = SQLPool.getinstance().getSql("accountGradeMonthCorpSql");
		
		 // 统计车队月度考核
		accountGradeMonthTeamSql = SQLPool.getinstance().getSql("accountGradeMonthTeamSql");

	}

	/**
	 * 执行月度绩效评分
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeGradeMonthStat() {
		// PreparedStatement对象
		PreparedStatement dbPstmt = null;
		// 结果集对象
		ResultSet dbResultSet = null;
		// 成功标志位 0:执行失败, 1执行成功
		int flag = 0;
		try {
			// 获得上月月份 格式"YYYY-MM"
			String queryLastMonth = CDate.lastMonth();
			// 获得系统当前日期 格式 "YYYY-MM-DD"
			long sysCurrentDay=CDate.getYearMonthDayUtc();
			// ///////模拟数据为2011-11
			//queryLastMonth = "2012-01";
//			logger.debug("--查询考核月度设置tb_checkmonth_set表--:"
//					+ queryCheckMonthSetSql);
			// 开始日期
			String start_date = "";
//			long l_startDate=0;
			// 结束日期
			String end_date = "";
			long l_endDate=0;
			// 企业ID
			String corp_id = "";
			boolean isAccountorg = false; //是否统计组织月度考核
			dbPstmt = dbConnection.prepareStatement(queryCheckMonthSetSql);
			dbPstmt.setString(1, queryLastMonth);
			dbResultSet = dbPstmt.executeQuery();
			// 逐个执行每个公司,因为每个公司的考核月度设置不同
			while (dbResultSet.next()) {
				corp_id = dbResultSet.getString(1);
				start_date = dbResultSet.getString(2);
				end_date = dbResultSet.getString(3);
//				l_startDate=dbResultSet.getLong(4);
			    l_endDate=dbResultSet.getLong(5);
			    
				logger.debug("--corp_id--:" + corp_id + " --start_date--:"
						+ start_date + "  --end_date--:" + end_date);

				// 如果该企业进行了考核月度参数设置
				if ((!"".equals(start_date)) && (!"".equals(end_date))) {					
					
					//如果系统当前日期  大于 设定结束日期，就执行本次评分计算
					if(sysCurrentDay>l_endDate){						
							// 2、-----根据日期删除车辆评分结果表TS_GRADE_MONTHSTAT
							int del = delGradeMonthStat(queryLastMonth, corp_id);
							if (del == 1) {
								// 3、-----查询插入表
								// int ins=1;
								int ins = queryVehicleDayStat(start_date, end_date,
										queryLastMonth, corp_id);
								if (ins == 1) {
									isAccountorg = true;
									logger.debug("---->>执行汇总统计数据成功！ corp_id=" + corp_id);
									flag = 1;
								} else {
									logger.debug("---->>执行汇总统计数据失败！ corp_id=" + corp_id);
									flag = 0;
								}	
							} else {
								logger.debug("---->>删除表TS_GRADE_MONTHSTAT数据失败！ corp_id="
										+ corp_id);
								flag = 0;
							}
							
					}					
				} else {
					logger.debug("---->>该企业未进行考核月度参数设置！ corp_id=" + corp_id);
					flag = 0;
				}
			} // End while
			
			if(isAccountorg && deleteGradeMonthCorp(queryLastMonth) && deleteGradeMonthTeam(queryLastMonth)){ 
				accoutGradMonthCorp(queryLastMonth); // 统计企业月度考核
				accoutGradMonth(queryLastMonth); // 统计车队月度考核
			}
		} catch (Exception e) {
			logger.error("执行月度绩效评分",e);
			flag = 0;
		} finally {
			DBAdapter.close(dbResultSet);
			DBAdapter.close(dbPstmt);
		}
		return flag;
	}
	
	/****
	 * 删除企业月度考核
	 * @param queryLastMonth
	 */
	private boolean deleteGradeMonthCorp(String queryLastMonth){
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareCall(SQLPool.getinstance().getSql("sql_deleteGradeCorp"));
			dbPstmt.setString(1, queryLastMonth);
			dbPstmt.executeUpdate();
			return true;
		}catch(Exception ex){
			logger.error("删除企业月度考核ERROR--" + queryLastMonth,ex);
		}finally{
			if(dbPstmt != null){
				try {
					dbPstmt.close();
				} catch (SQLException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}
		return false;
	}
	
	/****
	 * 删除企业月度考核
	 * @param queryLastMonth
	 */
	private boolean deleteGradeMonthTeam(String queryLastMonth){
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareCall(SQLPool.getinstance().getSql("sql_deleteGradeTeam"));
			dbPstmt.setString(1, queryLastMonth);
			dbPstmt.executeUpdate();
			return true;
		}catch(Exception ex){
			logger.error("删除车队月度考核ERROR--" + queryLastMonth,ex);
		}finally{
			if(dbPstmt != null){
				try {
					dbPstmt.close();
				} catch (SQLException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}
		return false;
	}
	
	/*****
	 * 统计企业月度考核
	 * @param queryLastMonth
	 */
	public void accoutGradMonthCorp(String queryLastMonth){
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareCall(accountGradeMonthCorpSql);
			dbPstmt.setInt(1, oilOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(2, oilOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(3, oilOverRpm_100);// 超转100分阀值
			dbPstmt.setInt(4, oilOverRpm_0);// 超转0分阀值

			dbPstmt.setInt(5, oilLongIdle_100);// 超长怠速100分阀值
			dbPstmt.setInt(6, oilLongIdle_0);// 超长怠速0分阀值

			dbPstmt.setInt(7, oilGearGlide_100);// 超长怠速100分阀值
			dbPstmt.setInt(8, oilGearGlide_0);// 超长怠速0分阀值

			dbPstmt.setInt(9, oilUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(10, oilUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(11, oilUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(12, oilUrgentLowdown_0);// 急减速0分阀值
			
			dbPstmt.setInt(13, oilAirCondition_100);// 怠速空调100分阀值
			dbPstmt.setInt(14, oilAirCondition_0);// 怠速空调0分阀值

			dbPstmt.setInt(15, oilEconomicRun_100);// 超经济区100分阀值 单位%
			dbPstmt.setInt(16, oilEconomicRun_0);// 超经济区0分阀值 单位%

			// ------安全部分 默认值
			dbPstmt.setInt(17, safeOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(18, safeOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(19, safeGearGlide_100);// 空档滑行100分阀值
			dbPstmt.setInt(20, safeGearGlide_0);// 空档滑行0分阀值

			dbPstmt.setInt(21, safeUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(22, safeUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(23, safeUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(24, safeUrgentLowdown_0);// 急减速0分阀值

			dbPstmt.setInt(25, safeFatigue_100);// 疲劳驾驶100分阀值
			dbPstmt.setInt(26, safeFatigue_0);// 疲劳驾驶0分阀值
			dbPstmt.setString(27, queryLastMonth);
			dbPstmt.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计企业月度考核ERROR.",e);
		}finally{
			if(dbPstmt != null){
				try {
					dbPstmt.close();
				} catch (SQLException e) {
					logger.error(e.getMessage(), e);
					dbPstmt = null;
				}
			}
		}
	}
	
	/*****
	 * 统计车队月度考核
	 * @param queryLastMonth
	 */
	public void accoutGradMonth(String queryLastMonth){
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareCall(accountGradeMonthTeamSql);
			dbPstmt.setInt(1, oilOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(2, oilOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(3, oilOverRpm_100);// 超转100分阀值
			dbPstmt.setInt(4, oilOverRpm_0);// 超转0分阀值

			dbPstmt.setInt(5, oilLongIdle_100);// 超长怠速100分阀值
			dbPstmt.setInt(6, oilLongIdle_0);// 超长怠速0分阀值

			dbPstmt.setInt(7, oilGearGlide_100);// 超长怠速100分阀值
			dbPstmt.setInt(8, oilGearGlide_0);// 超长怠速0分阀值

			dbPstmt.setInt(9, oilUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(10, oilUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(11, oilUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(12, oilUrgentLowdown_0);// 急减速0分阀值
			
			dbPstmt.setInt(13, oilAirCondition_100);// 怠速空调100分阀值
			dbPstmt.setInt(14, oilAirCondition_0);// 怠速空调0分阀值

			dbPstmt.setInt(15, oilEconomicRun_100);// 超经济区100分阀值 单位%
			dbPstmt.setInt(16, oilEconomicRun_0);// 超经济区0分阀值 单位%

			// ------安全部分 默认值
			dbPstmt.setInt(17, safeOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(18, safeOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(19, safeGearGlide_100);// 空档滑行100分阀值
			dbPstmt.setInt(20, safeGearGlide_0);// 空档滑行0分阀值

			dbPstmt.setInt(21, safeUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(22, safeUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(23, safeUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(24, safeUrgentLowdown_0);// 急减速0分阀值

			dbPstmt.setInt(25, safeFatigue_100);// 疲劳驾驶100分阀值
			dbPstmt.setInt(26, safeFatigue_0);// 疲劳驾驶0分阀值
			dbPstmt.setString(27, queryLastMonth);
			dbPstmt.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计车队月度考核ERROR.",e);
		}finally{
			if(dbPstmt != null){
				try {
					dbPstmt.close();
				} catch (SQLException e) {
					logger.error(e.getMessage(), e);
					dbPstmt = null;
				}
			}
		}
	}

	/*
	 * 根据日期删除企业统计月的车辆评分结果表TS_GRADE_MONTHSTAT
	 */
	public int delGradeMonthStat(String queryLastMonth, String corpId) {
		// 成功标志位 0:执行失败, 1执行成功
		int flag = 0;
		// PreparedStatement对象
		PreparedStatement dbPstmt = null;

		int year = Integer.parseInt(queryLastMonth.substring(0, 4));
		int month = Integer.parseInt(queryLastMonth.substring(5, 7));
//		logger.debug("---删除企业统计月的车辆评分结果TS_GRADE_MONTHSTAT表-->>:"
//				+ delGradeMonthStatSql);
		try {
			dbPstmt = dbConnection.prepareStatement(delGradeMonthStatSql);
			dbPstmt.setInt(1, year);
			dbPstmt.setInt(2, month);
			dbPstmt.setString(3, corpId);
			dbPstmt.executeUpdate();
			flag = 1;
		} catch (Exception e) {
			logger.error("根据日期删除企业统计月的车辆评分结果表TS_GRADE_MONTHSTAT",e);
			flag = 0;
		} finally {
			DBAdapter.close(dbPstmt);
		}
		return flag;
	}

	/*
	 * 查询汇总数据插入到表TS_GRADE_MONTHSTAT 中
	 */
	public int queryVehicleDayStat(String startDate, String endDate,
			String queryLastMonth, String corpId) {
		// 成功标志位 0:执行失败, 1执行成功
		int flag = 0;
		// PreparedStatement对象
		PreparedStatement dbPstmt = null;
		// 结果集对象
		ResultSet dbResultSet = null;
//		logger.debug("---查询车辆日统计信息TS_VEHICLE_DAYSTAT表数据--->:"
//				+ queryVehicleDayStatSql);
		try {
			// i,汇总数据
			dbPstmt = dbConnection.prepareStatement(queryVehicleDayStatSql);
			// -------节油部分 默认值
			dbPstmt.setInt(1, oilOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(2, oilOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(3, oilOverRpm_100);// 超转100分阀值
			dbPstmt.setInt(4, oilOverRpm_0);// 超转0分阀值

			dbPstmt.setInt(5, oilLongIdle_100);// 超长怠速100分阀值
			dbPstmt.setInt(6, oilLongIdle_0);// 超长怠速0分阀值

			dbPstmt.setInt(7, oilGearGlide_100);// 超长怠速100分阀值
			dbPstmt.setInt(8, oilGearGlide_0);// 超长怠速0分阀值

			dbPstmt.setInt(9, oilUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(10, oilUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(11, oilUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(12, oilUrgentLowdown_0);// 急减速0分阀值
			
			dbPstmt.setInt(13, oilAirCondition_100);// 怠速空调100分阀值
			dbPstmt.setInt(14, oilAirCondition_0);// 怠速空调0分阀值

			dbPstmt.setInt(15, oilEconomicRun_100);// 超经济区100分阀值 单位%
			dbPstmt.setInt(16, oilEconomicRun_0);// 超经济区0分阀值 单位%

			// ------安全部分 默认值
			dbPstmt.setInt(17, safeOverSpeed_100);// 超速100分阀值
			dbPstmt.setInt(18, safeOverSpeed_0);// 超速0分阀值

			dbPstmt.setInt(19, safeGearGlide_100);// 空档滑行100分阀值
			dbPstmt.setInt(20, safeGearGlide_0);// 空档滑行0分阀值

			dbPstmt.setInt(21, safeUrgentSpeed_100);// 急加速100分阀值
			dbPstmt.setInt(22, safeUrgentSpeed_0);// 急加速0分阀值
			
			dbPstmt.setInt(23, safeUrgentLowdown_100);// 急减速100分阀值
			dbPstmt.setInt(24, safeUrgentLowdown_0);// 急减速0分阀值

			dbPstmt.setInt(25, safeFatigue_100);// 疲劳驾驶100分阀值
			dbPstmt.setInt(26, safeFatigue_0);// 疲劳驾驶0分阀值

			dbPstmt.setString(27, startDate);// 开始日期
			dbPstmt.setString(28, endDate);// 结束日期
			
			dbPstmt.setString(29, corpId);// 企业号
			dbPstmt.setString(30, queryLastMonth);// 查询月度
			dbPstmt.setString(31, corpId);// 企业号
			dbPstmt.setString(32, corpId);// 企业号
			
			dbPstmt.setString(33, queryLastMonth);// 查询月度
			
			dbResultSet = dbPstmt.executeQuery();

			// 插入到表TS_GRADE_MONTHSTAT 中
			insertGradeMonthStat(queryLastMonth, dbResultSet);

			flag = 1;
		} catch (Exception e) {
			logger.debug("查询车辆日统计信息TS_VEHICLE_DAYSTAT表数据",e);
			flag = 0;
		} finally {
			DBAdapter.close(dbResultSet);
			DBAdapter.close(dbPstmt);
		}
		return flag;
	}

	/*
	 * 汇总数据插入到表TS_GRADE_MONTHSTAT 中
	 */
	public int insertGradeMonthStat(String queryLastMonth, ResultSet dbResultSet) {
		// 成功标志位 0:执行失败, 1执行成功
		int flag = 0;
		// PreparedStatement对象
		PreparedStatement dbPstmt = null;
		try {
//			logger.debug("---插入数据车辆评分结果表TS_GRADE_MONTHSTAT表--->:"
//					+ insertGradeMonthStatSql);
			
			dbPstmt = dbConnection.prepareStatement(insertGradeMonthStatSql);

			int year = Integer.parseInt(queryLastMonth.substring(0, 4));
			int month = Integer.parseInt(queryLastMonth.substring(5, 7));
			while (dbResultSet.next()) {
				// 插入到表TS_GRADE_MONTHSTAT中
				dbPstmt.setInt(1, year); // 统计年
				dbPstmt.setInt(2, month); // 统计月
				dbPstmt.setString(3, queryLastMonth); // 统计年月
				dbPstmt.setString(4, dbResultSet.getString(1)); // 车辆编号
				dbPstmt.setString(5, dbResultSet.getString(2));// 车辆vin号
				dbPstmt.setString(6, dbResultSet.getString(3));// 车牌号
				dbPstmt.setString(7, dbResultSet.getString(4));// 车型品牌(宇通、金龙)
				dbPstmt.setString(8, dbResultSet.getString(5));// 车型(产品族)
				dbPstmt.setString(9, dbResultSet.getString(6));// 发动机型号
				dbPstmt.setString(10, dbResultSet.getString(7));// 线路ID
				dbPstmt.setString(11, dbResultSet.getString(8));// 线路名称
				dbPstmt.setString(12, dbResultSet.getString(9));// 企业号
				dbPstmt.setString(13, dbResultSet.getString(10));// 企业名称
				dbPstmt.setString(14, dbResultSet.getString(11));// 车队号
				dbPstmt.setString(15, dbResultSet.getString(12));// 车队名称
				dbPstmt.setInt(16, dbResultSet.getInt(13));// 行驶里程

				dbPstmt.setInt(17, dbResultSet.getInt(14));// 超速次数
				dbPstmt.setInt(18, dbResultSet.getInt(15));// 超速时间
				dbPstmt.setFloat(19, dbResultSet.getFloat(16));// 节油超速得分
				dbPstmt.setFloat(20, dbResultSet.getFloat(17));// 安全超速得分

				dbPstmt.setInt(21, dbResultSet.getInt(18));// 超转次数
				dbPstmt.setInt(22, dbResultSet.getInt(19));// 超转时间
				dbPstmt.setFloat(23, dbResultSet.getFloat(20));// 节油_超转得分

				dbPstmt.setInt(24, dbResultSet.getInt(21));// 空档滑行次数
				dbPstmt.setInt(25, dbResultSet.getInt(22));// 空档滑行时间
				dbPstmt.setFloat(26, dbResultSet.getFloat(23));// 节油_空档滑行得分
				dbPstmt.setFloat(27, dbResultSet.getFloat(24));// 安全_空档滑行得分

				dbPstmt.setInt(28, dbResultSet.getInt(25));// 超长怠速次数
				dbPstmt.setInt(29, dbResultSet.getInt(26));// 超长怠速时间
				dbPstmt.setFloat(30, dbResultSet.getFloat(27));// 节油_超长怠速得分

				dbPstmt.setInt(31, dbResultSet.getInt(28));// 疲劳驾驶次数
				dbPstmt.setInt(32, dbResultSet.getInt(29));// 疲劳驾驶时间
				dbPstmt.setFloat(33, dbResultSet.getFloat(30));// 安全_疲劳驾驶得分

				dbPstmt.setInt(34, dbResultSet.getInt(31));// 超经济区比例
				dbPstmt.setInt(35, dbResultSet.getInt(32));// 超经济区运行时间
				dbPstmt.setFloat(36, dbResultSet.getFloat(33));// 节油_超经济区运行得分

//				dbPstmt.setInt(37, dbResultSet.getInt(34));// 急加急减速次数
//				dbPstmt.setInt(38, dbResultSet.getInt(35));// 节油_急加急减速得分
//				dbPstmt.setInt(39, dbResultSet.getInt(36));// 安全_急加急减速得分

				dbPstmt.setInt(37, dbResultSet.getInt(34));// 怠速空调次数
				dbPstmt.setInt(38, dbResultSet.getInt(35));// 怠速空调时间
				dbPstmt.setFloat(39, dbResultSet.getFloat(36));// 节油_怠速空调得分

				dbPstmt.setInt(40, dbResultSet.getInt(37));// 发动机运行时间
				dbPstmt.setFloat(41, dbResultSet.getFloat(38));// 节油总得分
				dbPstmt.setFloat(42, dbResultSet.getFloat(39));// 安全总得分

				dbPstmt.setInt(43, dbResultSet.getInt(40));// 实际油耗值
				dbPstmt.setInt(44, dbResultSet.getInt(41));// 油耗考核值
				dbPstmt.setInt(45, dbResultSet.getInt(42));// 节油量
				dbPstmt.setInt(46, dbResultSet.getInt(43));// 节油率
				dbPstmt.setFloat(47, dbResultSet.getFloat(44));// 油耗考核得分
				dbPstmt.setFloat(48, dbResultSet.getFloat(45));// 绩效总得分
				
				dbPstmt.setInt(49, dbResultSet.getInt("URGENT_SPEED_NUM"));// 急减速次数
				dbPstmt.setFloat(50, dbResultSet.getFloat("OIL_URGENT_SPEEDDOWN_SCORE"));// 节油急减速得分
				dbPstmt.setFloat(51, dbResultSet.getFloat("SAFE_URGENT_SPEEDDOWN_SCORE"));// 安全急减速得分
				
				dbPstmt.setInt(52, dbResultSet.getInt("URGENT_LOWDOWN_NUM"));// 急加速次数
				dbPstmt.setFloat(53, dbResultSet.getFloat("OIL_URGENT_SPEEDDOWN_SCORE"));// 节油急加速得分
				dbPstmt.setFloat(54, dbResultSet.getFloat("SAFE_URGENT_SPEEDDOWN_SCORE"));// 安全急加速得分
				
				dbPstmt.setString(55,dbResultSet.getString("INNER_CODE") );// 车辆内部编号
				dbPstmt.setInt(56, dbResultSet.getInt("OIL_WEAR_SUM"));// 考核月总油耗
				
				dbPstmt.addBatch();// 批量装载数据
			}
			// 批量提交数据,清除缓存
			dbPstmt.executeBatch();
			flag = 1;
		} catch (Exception e) {
			logger.debug("汇总数据插入到表TS_GRADE_MONTHSTAT 中",e);
			flag = 0;
		} finally {
			DBAdapter.close(dbResultSet);
			DBAdapter.close(dbPstmt);
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
