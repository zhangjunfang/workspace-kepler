package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;

/**
 * 日统计开始前初始化工作，如中间表清空等
 * @author yujch
 *
 */

public class DaystatInit {
	private static final Logger logger = LoggerFactory.getLogger(DaystatInit.class);
	
	private String deleteStateEventInfoSql = null;
	private String deleteServicestatInfoSql = null;
	
	private long utc = 0; // 指定查询UTC时间
	
	public void initAnalyser(){

		// 清空状态事件表
		deleteStateEventInfoSql = SQLPool.getinstance().getSql("sql_deleteStateEventInfo");
		deleteServicestatInfoSql = SQLPool.getinstance().getSql("sql_deleteServicestatInfo");
	}
	
	public void run(){
		//从连接池获取连接
		try {
			deleteStateEvent();
			deleteStopstartInfo();
		} catch (SQLException e) {
			logger.error("更新车辆总累计表出错.",e);
		}
		
	}
	
	// 设置统计时间
	public  void setTime(long utc){
		this.utc = utc;
	}
	public long getTime(){
		return utc;
	}
	/**
	 * @description:车辆状态事件表 （每日凌晨统计上一日数据）
	 * @param:
	 * @modifyInformation：
	 */
	private void deleteStateEvent() throws SQLException{
		Connection dbCon = null;
		PreparedStatement stDeleteStateEventInfo = null;
		try{
			Long startTime = System.currentTimeMillis();
			dbCon = OracleConnectionPool.getConnection();
			//清空原有状态数据
			stDeleteStateEventInfo = dbCon.prepareStatement(deleteStateEventInfoSql);
			stDeleteStateEventInfo.executeUpdate();
			logger.info("日统计初始化:清空状态事件表成功. 耗时："+(System.currentTimeMillis()-startTime)/1000+"s");
		}catch(SQLException e){
			logger.error("日统计初始化: 清空状态事件和起步停车信息出错.",e);
		}finally{
			if(stDeleteStateEventInfo != null){
				stDeleteStateEventInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
	
	/**
	 * @description:车辆状态事件表 （每日凌晨统计上一日数据）
	 * @param:
	 * @modifyInformation：
	 */
	private void deleteStopstartInfo() throws SQLException{
		Connection dbCon = null;
		PreparedStatement stDeleteStopstartInfo = null;
		try{
			Long startTime = System.currentTimeMillis();
			dbCon = OracleConnectionPool.getConnection();
			//清空原有状态数据
			stDeleteStopstartInfo = dbCon.prepareStatement(deleteServicestatInfoSql);
			stDeleteStopstartInfo.executeUpdate();
			logger.info("日统计初始化:清空车辆运营日统计临时表成功. 耗时："+(System.currentTimeMillis()-startTime)/1000+"s");
		}catch(SQLException e){
			logger.error("日统计初始化: 清空车辆运营日统计临时表出错.",e);
		}finally{
			if(stDeleteStopstartInfo != null){
				stDeleteStopstartInfo.close();
			}
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
}

