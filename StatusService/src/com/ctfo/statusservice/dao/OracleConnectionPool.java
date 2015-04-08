package com.ctfo.statusservice.dao;

import java.sql.Connection;
import java.sql.SQLException;

import com.alibaba.druid.pool.DruidDataSource;

public class OracleConnectionPool {
	private static DruidDataSource ops = null;
	
	public OracleConnectionPool(DruidDataSource ops){
		OracleConnectionPool.ops = ops;
	}
	
	public static Connection getConnection() throws SQLException {
		// 获得连接
		if (ops == null) 
			throw new SQLException("OracleConnectionPoolDataSource is null.");
		return ops.getConnection();
	}
}
