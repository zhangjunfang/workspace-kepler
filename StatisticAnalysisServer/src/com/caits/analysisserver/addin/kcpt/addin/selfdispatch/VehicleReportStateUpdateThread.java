package com.caits.analysisserver.addin.kcpt.addin.selfdispatch;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.SelfDispatch;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： VehicleReportStateUpdateThread <br>
 * 功能： <br>
 * 描述： 单车分析报告生成超时的记录更新为失败状态<br>
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
 * <td>2012-07-04</td>
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
public class VehicleReportStateUpdateThread extends SelfDispatch {
	private static final Logger logger = LoggerFactory.getLogger(TravelingRecorderCollectThread.class);

	// ------获得xml拼接的Sql语句
	private String updateVehicleReportStateSql; // 修改行驶记录命令解析状态

	// 初始化方法
	public void initAnalyser() {
		// 更新单车分析报告状态
		updateVehicleReportStateSql = SQLPool.getinstance().getSql(
				"sql_updateVehicleReportStateSql");

	}

	public void run() {
		//logger.info("单车分析 报告生成状态更新开始！");
		while (true) {
			UpdateThread pt = new UpdateThread();
			pt.run();
			try {
				Thread.sleep( 5 * 60 * 1000);// 每5分钟执行一次
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}

	}

	/**
	 * 执行更新单车分析报告生成状态
	 * 
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeUpdateVehicleReportState() {
		// logger.info("start parse!");
		// PreparedStatement对象
		PreparedStatement dbPstmt0 = null;
		Connection dbConnection = null;
		
		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {
				
				
				// 更新当前指令的解析次数,若3次未解析成功则不再解析
				dbPstmt0 = dbConnection.prepareStatement(updateVehicleReportStateSql);
				dbPstmt0.executeUpdate();
				if (dbPstmt0 != null) {
					dbPstmt0.close();
				}
				
				flag = 1;
			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("更新单车分析报告状态信息出错：", e);
			// flag = 0;
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

	class UpdateThread implements Runnable {

		@Override
		public void run() {
			//logger.info("Starting traveling recorder");
			// TODO Auto-generated method stub
			executeUpdateVehicleReportState();
		}
	}

	@Override
	public void costTime() {
		// TODO Auto-generated method stub

	}

}
