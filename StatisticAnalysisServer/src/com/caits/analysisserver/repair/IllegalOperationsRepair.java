package com.caits.analysisserver.repair;

import java.io.File;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Calendar;
import java.util.Date;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.TmpOracleDBAdapter;
import com.caits.analysisserver.utils.MathUtils;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： <br>
 * 描述： <br>夜间非法营运数据补录
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
@SuppressWarnings("unused")
public class IllegalOperationsRepair {

	private static final Logger logger = LoggerFactory
			.getLogger(IllegalOperationsRepair.class);

	// ------获得xml拼接的Sql语句

	private String delOrgAlarmDaystatSql;// 删除企业日车辆告警情况
	private String queryOrgAlarmDaystatSql;// 统计企业日车辆告警情况
	private String saveOrgAlarmDaystatSql; // 保存企业日车辆告警情况

	private int count = 0;// 计数器

	private long statDate;
	private int entId=0;
	private long beginTime;
	private long endTime;
	private Date currDay;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public IllegalOperationsRepair(Date currDay,int entId) {
		this.currDay = currDay;
		this.statDate = currDay.getTime();
		this.beginTime = statDate - 1000 * 60 * 60 * 12;
		this.endTime = statDate + 1000 * 60 * 60 * 12;
		this.entId = entId;

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 删除企业日车辆运营情况
		delOrgAlarmDaystatSql = SQLPool.getinstance().getSql(
				"sql_delOrgAlarmDaystatSql");
		// 统计企业日车辆运营情况
		queryOrgAlarmDaystatSql = SQLPool.getinstance().getSql(
				"sql_queryOrgAlarmStatSql");
		// 保存企业日车辆运营情况
		saveOrgAlarmDaystatSql = SQLPool.getinstance().getSql(
				"sql_saveOrgAlarmDaystatSql");

	}

	/**
	 * 生成车辆日运营属性
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeStatRecorder() {
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt2 = null;
		PreparedStatement dbPstmt3 = null;
		Connection dbConnection = null;

		// 结果集对象
		ResultSet dbResultSet = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		AnalysisDBAdapter dba = new AnalysisDBAdapter();
		try {
			//删除符合条件的非法运营信息
			String vehicleInfoStr = "select VID,VEHICLE_NO from tb_vehicle where enable_flag='1' ";
			String deleteAlarmStr = "delete from th_vehicle_alarm where alarm_code='110' and utc>=? and utc < ?";
			String delAlarmEventStr = "delete from th_vehicle_alarm_event where alarm_code='110' and BEGIN_UTC >= ?  and BEGIN_UTC < ? ";
			if (this.entId!=0){
				deleteAlarmStr += " and vid in (select vid from tb_vehicle where ent_id in (select ent_id from tb_organization start with ent_id= ? connect by prior ent_id = parent_id))";
				delAlarmEventStr += " and TEAM_ID in (select ent_id from tb_organization start with ent_id= ? connect by prior ent_id = parent_id)";
				vehicleInfoStr += " and ent_id in (select ent_id from tb_organization start with ent_id= ? connect by prior ent_id = parent_id)";
			}
			
			//---------------------------------
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 删除告警明细表数据
				dbPstmt1 = dbConnection
						.prepareStatement(deleteAlarmStr);
				dbPstmt1.setLong(1, beginTime);
				dbPstmt1.setLong(2, endTime);
				if (this.entId>0){
					dbPstmt1.setLong(3, entId);
				}
				dbPstmt1.executeUpdate();

				// 删除告警事件表数据
				dbPstmt2 = dbConnection
						.prepareStatement(delAlarmEventStr);
				dbPstmt2.setLong(1, beginTime);
				dbPstmt2.setLong(2, endTime);
				if (this.entId>0){
					dbPstmt2.setLong(3, entId);
				}
				dbPstmt2.executeQuery();

				//查询重新统计的车辆ID
				dbPstmt3 = dbConnection.prepareStatement(vehicleInfoStr);
				if (this.entId>0){
					dbPstmt3.setLong(1, entId);
				}
				dbResultSet = dbPstmt3.executeQuery();
				
				// 创建一个可重用固定线程数的线程池
				ExecutorService pool = Executors.newFixedThreadPool(5);
				
				TmpOracleDBAdapter.initDBAdapter();
				
				dba.initDBAdapter();
				
				if (TmpOracleDBAdapter.illeOptAlarmMap==null||TmpOracleDBAdapter.illeOptAlarmMap.size()==0){
					TmpOracleDBAdapter.queryIllegalOptionsAlarm();
					logger.debug("非法营运配置信息读取完成："+TmpOracleDBAdapter.illeOptAlarmMap.size());
				}
				
				if (TmpOracleDBAdapter.illeOptAlarmMap!=null&&TmpOracleDBAdapter.illeOptAlarmMap.size()>0){
					
				while (dbResultSet.next()) {
					long vid = dbResultSet.getLong("VID");
					
					Calendar cal = Calendar.getInstance();
					cal.setTime(this.currDay);
					
					int year = cal.get(Calendar.YEAR);
					int month = cal.get(Calendar.MONTH) + 1;
					int day = cal.get(Calendar.DATE);

					String filePath = FilePool.getinstance().getFile(this.statDate,"trackfileurl")+"/"+year+"/"+MathUtils.addSuffixZero(month)+"/"+MathUtils.addSuffixZero(day)+"/"+vid+".txt";
					
					File f = new File(filePath);

					if (f.exists()){
						//启动线程池，处理数据
						Thread t1 = new IllegalOperationsRepairThread(f);
						// 将线程放入池中进行执行
						pool.execute(t1);
					}else{
						logger.info(this.currDay+" VID："+vid+" 轨迹记录文件不存在");
					}
				}
				
				// 关闭线程池
				pool.shutdown();
				//清空缓存对象
				while(!pool.awaitTermination(1, TimeUnit.SECONDS)){
					//线程池没有关闭时一直等待
				}
				//线程池关闭后同时清空缓存
				TmpOracleDBAdapter.illeOptAlarmMap.clear();
				
				logger.info("补录非法运营信息全部完成");
				flag = 1;
				}else{
					logger.info("不存在夜间非法运营配置！");
				}
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("补录夜间非法运营信息出错：", e);
		} finally {
			try {
				AnalysisDBAdapter.clearCollections();
				
				if (dbResultSet != null) {
					dbResultSet.close();
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
