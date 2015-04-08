package com.ctfo.savecenter.dao;


import java.sql.SQLException;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据库访问类
 * 
 * 继承框架的类
 * 
 */
public class TrackManagerKcptDBAdapter {
	private static final Logger logger = LoggerFactory.getLogger(TrackManagerKcptDBAdapter.class);
	/*** 存储跨域统计连接对象***/
	private OracleConnection spannedStatisticsConn;
	/*** 存储跨域统计信息 ***/
	private OraclePreparedStatement insertSpannedStatistics;
	/*** 存储跨域统计信息***/
	private String sql_insertSpannedStatistics = null;
	



	// 轨迹批量数据库提交数
	private int spannedStatisticsCount = 0;



	/****
	 * 
	 * @param config
	 * @param nodeName
	 * @throws Exception
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {
		spannedStatisticsConn = (OracleConnection)OracleConnectionPool.getConnection();
		// 报警批量数据库提交数
		spannedStatisticsCount = config.getIntValue(nodeName + "|spannedStatisticsCount");
		// 存储跨域统计信息
		sql_insertSpannedStatistics = config.getStringValue(nodeName + "|sql_insertSpannedStatistics");
//		spannedStatisticsConn.setAutoCommit(false);
		insertSpannedStatistics = createStatement(spannedStatisticsConn,spannedStatisticsCount,sql_insertSpannedStatistics);
	}

	public void createStatement() {
		try {
			// 从连接池获得连接
			spannedStatisticsConn = (OracleConnection)OracleConnectionPool.getConnection();
			// 轨迹包带总线数据更新最后位置到数据库
			insertSpannedStatistics = (OraclePreparedStatement)spannedStatisticsConn.prepareStatement(sql_insertSpannedStatistics);
			insertSpannedStatistics.setExecuteBatch(spannedStatisticsCount);
			
		} catch (SQLException e) {
			logger.error("oracle初始化statement出错.", e);
		}
	}

	public void commit() throws SQLException {
		try{
			insertSpannedStatistics.sendBatch();
		}catch(Exception e){
			logger.error("",e);
			try{
				spannedStatisticsConn.getMetaData();
				if(insertSpannedStatistics == null){
					insertSpannedStatistics = createStatement(spannedStatisticsConn,spannedStatisticsCount,sql_insertSpannedStatistics);
				}
			}catch(Exception ex){
				insertSpannedStatistics = recreateStatement(spannedStatisticsConn,spannedStatisticsCount,sql_insertSpannedStatistics);
			}
		}
		
	}
	
	private OraclePreparedStatement recreateStatement(OracleConnection dbCon, int count,String sql){
		OraclePreparedStatement stat= null;
		try {
			
			if(dbCon != null){
				dbCon.close();
				dbCon = null;
			}
				// 从连接池获得连接
			dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			
			// 轨迹包更新最后位置到数据库
			stat = (OraclePreparedStatement)dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
			logger.info("Create statement successfully!");
		} catch (SQLException e) {
			logger.error("Create statement error",e);
		}
		return stat;
	}
	
	private OraclePreparedStatement createStatement(OracleConnection dbCon,int count,String sql){
		// 轨迹包更新最后位置到数据库
		OraclePreparedStatement stat= null;
		try {
			stat = (OraclePreparedStatement)dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
		} catch (SQLException e) {
			logger.error("跨域信息创建数据库定义错误",e);
		}
		return stat;
	}
	
	/**
	 * 保存跨域信息
	 * 
	 * @param primaryKey  主键
	 * @param localCode  属地区域编码
	 * @param currentCode 当前区域编码
	 * @param currentTime 当前时间 
	 * @throws SQLException
	 * 
	 */
	public void saveThSpannedStatistics(String primaryKey, String localCode, String currentCode, long currentTime) throws SQLException {
		try {
			insertSpannedStatistics.setString(1, primaryKey);
			insertSpannedStatistics.setString(2, localCode);
			insertSpannedStatistics.setString(3, currentCode);
			insertSpannedStatistics.setLong(4, currentTime);
			insertSpannedStatistics.setString(5, localCode.substring(0, 4));
			insertSpannedStatistics.setString(6, currentCode.substring(0, 4));
			insertSpannedStatistics.setString(7, localCode.substring(0, 2));
			insertSpannedStatistics.setString(8, currentCode.substring(0, 2));
			insertSpannedStatistics.executeUpdate();
//			insertSpannedStatistics.executeBatch();
		}  catch (SQLException e) {
			logger.error("ORACLE保存跨域信息出错,"+ e.getMessage());
			try{
				spannedStatisticsConn.getMetaData();
				if(insertSpannedStatistics == null){
					insertSpannedStatistics = createStatement(spannedStatisticsConn,spannedStatisticsCount,sql_insertSpannedStatistics);
				}
			}catch(Exception ex){
				insertSpannedStatistics = recreateStatement(spannedStatisticsConn,spannedStatisticsCount,sql_insertSpannedStatistics);
			}
		}
	}
}

