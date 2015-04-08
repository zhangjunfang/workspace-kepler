package com.caits.analysisserver.quartz.jobs.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
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
public class VehicleOperationPropertyDaysJobdetail{

	private static final Logger logger = LoggerFactory
			.getLogger(VehicleOperationPropertyDaysJobdetail.class);

	// ------获得xml拼接的Sql语句
	private String delVehicleOpterationPropertySql;
	private String saveOnnetVehicleOpterationPropertySql; // 生成系统在网车辆日运营属性
	@SuppressWarnings("unused")
	private String updateOnnetVehicleOpterationAddnewSql; // 更新在网车辆当日运营本月新增属性
	private String saveOffnetVehicleOpterationPropertySql; // 生成当日离网车辆日运营属性

//	private int count = 0;// 清理未对应日志时间间隔

	private long statDate;
	private long beginTime;
	private long endTime;

	/**
	 * 初始化统计周期：传入日期
	 * 
	 * @param statDate
	 *            当日12点日期时间
	 */
	public VehicleOperationPropertyDaysJobdetail(Date currDay) {
		this.statDate = currDay.getTime();
		this.beginTime = statDate - 1000 * 60 * 60 * 12;
		this.endTime = statDate + 1000 * 60 * 60 * 12;

		this.initAnalyser();
	}

	// 初始化方法
	public void initAnalyser() {
		// 清空当日车辆运营属性
		delVehicleOpterationPropertySql = SQLPool.getinstance().getSql(
				"sql_delVehicleOpterationPropertySql");
		// 添加在网车辆当日运营属性
		saveOnnetVehicleOpterationPropertySql = SQLPool.getinstance().getSql(
				"sql_saveOnnetVehicleOpterationPropertySql");
		//更新在网车辆当日运营本月新增属性
		updateOnnetVehicleOpterationAddnewSql = SQLPool.getinstance().getSql(
		"sql_updateOnnetVehicleOpterationAddnewSql");
		// 添加退网车辆当日运营属性
		saveOffnetVehicleOpterationPropertySql = SQLPool.getinstance().getSql(
				"sql_saveOffnetVehicleOpterationPropertySql");

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
		PreparedStatement dbPstmt3 = null;
		PreparedStatement dbPstmt4 = null;
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 删除7日前车辆运营属性，即此数据保留7日
				dbPstmt0 = dbConnection
						.prepareStatement(delVehicleOpterationPropertySql);
				dbPstmt0.setLong(1, statDate - 1000 * 60 * 60 * 24 * 7);
				dbPstmt0.executeUpdate();

				// 删除当日车辆运营属性
				dbPstmt1 = dbConnection
						.prepareStatement(delVehicleOpterationPropertySql);
				dbPstmt1.setLong(1, statDate);
				dbPstmt1.executeUpdate();

				// 添加在网车辆当日运营属性
				dbPstmt2 = dbConnection
						.prepareStatement(saveOnnetVehicleOpterationPropertySql);
				dbPstmt2.setLong(1, statDate);
				dbPstmt2.setLong(2, beginTime);
				dbPstmt2.setLong(3, endTime);
				dbPstmt2.setLong(4, beginTime);
				dbPstmt2.setLong(5, endTime);
				int innetnum = dbPstmt2.executeUpdate();
				
				// 更新在网车辆当日运营本月新增属性
				//车辆转租不算新增，去掉以下部分
				/*dbPstmt3 = dbConnection
						.prepareStatement(updateOnnetVehicleOpterationAddnewSql);
				dbPstmt3.setLong(1, statDate);
				dbPstmt3.setLong(2, statDate);
				dbPstmt3.setLong(3, statDate - 1000 * 60 * 60 * 24);
				int addnew = dbPstmt3.executeUpdate();*/

				// 添加退网车辆当日运营属性
				dbPstmt4 = dbConnection
						.prepareStatement(saveOffnetVehicleOpterationPropertySql);
				dbPstmt4.setLong(1, statDate);
				dbPstmt4.setLong(2, statDate - 1000 * 60 * 60 * 24);
				dbPstmt4.setLong(3, statDate);
				int offnet = dbPstmt4.executeUpdate();

				Date dt = new Date();
				dt.setTime(this.statDate);
				if ((innetnum + offnet) == 0) {
					logger.error(CDate.dateToStr(dt) + "车辆日运营属性信息生成出错！生成结果为 0！");
				}else{
				logger.debug(CDate.dateToStr(dt) + "车辆日运营属性信息生成成功！");
				}
				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("生成车辆日运营属性出错：", e);
			flag = 0;
		} finally {
			try {
				if (dbPstmt0 != null) {
					dbPstmt0.close();
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
				if (dbPstmt4 != null) {
					dbPstmt4.close();
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
