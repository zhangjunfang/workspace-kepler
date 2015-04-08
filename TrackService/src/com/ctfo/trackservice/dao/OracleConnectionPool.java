package com.ctfo.trackservice.dao;

import java.sql.Connection;
import java.sql.SQLException;

import oracle.jdbc.pool.OracleDataSource;

import com.ctfo.trackservice.common.OracleProperties;
/**
 * Oracle连接池
 *
 */
public class OracleConnectionPool {
//	private static OracleConnectionPool instance = new OracleConnectionPool();
	
	private static OracleDataSource ops = null;
 
//	private OracleConnectionPool(){
//		if(instance == null){
//			instance = new OracleConnectionPool(); 
//		}
//	}
	public static Connection getConnection() throws SQLException {
		// 获得连接
		if (ops == null) 
			throw new SQLException("OracleConnectionPoolDataSource is null.");
		return ops.getConnection();
	}
	/**
	 * 初始化连接池
	 * @param oracleProperties 
	 * @throws SQLException
	 */
	public static void init(OracleProperties oracleProperties) throws SQLException {
		ops = new OracleDataSource(); 
		ops.setURL(oracleProperties.getUrl());
		ops.setUser(oracleProperties.getUsername());
		ops.setPassword(oracleProperties.getPassword());
	}
}
