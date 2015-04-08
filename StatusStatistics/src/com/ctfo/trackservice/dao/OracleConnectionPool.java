package com.ctfo.trackservice.dao;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

import com.alibaba.druid.pool.DruidDataSource;
import com.ctfo.trackservice.model.OracleProperties;


/*****************************************
 * <li>描        述：连接池		
 * 
 *****************************************/
public class OracleConnectionPool {
	private static DruidDataSource dds = null; 
	
	public static void init(OracleProperties properties) throws SQLException{ 
		dds = new DruidDataSource();
		dds.setUrl(properties.getUrl());
		dds.setUsername(properties.getUsername());
		dds.setPassword(properties.getPassword());
//		<!-- 配置初始化大小、最小、最大 -->
		dds.setInitialSize(properties.getInitialSize());
		dds.setMaxActive(properties.getMaxActive());
		dds.setMinIdle(properties.getMinIdle());
//		<!-- 配置获取连接等待超时的时间。 单位是毫秒 -->
		dds.setMaxWait(properties.getMaxWait());
		
		dds.setTimeBetweenEvictionRunsMillis(properties.getTimeBetweenEvictionRunsMillis());
		dds.setMinEvictableIdleTimeMillis(properties.getMinEvictableIdleTimeMillis());
		dds.setValidationQuery("SELECT 'x' FROM DUAL");
		dds.setTestWhileIdle(properties.isTestWhileIdle());
		dds.setTestOnBorrow(properties.isTestOnBorrow());
		dds.setTestOnReturn(properties.isTestOnReturn());
//		<!-- 打开PSCache，并且指定每个连接上PSCache的大小 -->
		dds.setPoolPreparedStatements(true); 
		dds.setMaxOpenPreparedStatements(properties.getMaxOpenPreparedStatements());
//		<!-- 配置监控统计拦截的filters -->
//		dds.setFilters("stat");
		dds.setRemoveAbandoned(properties.isRemoveAbandoned());
		dds.setName("oracle");
		dds.init(); 
	}

	public static Connection getConnection() throws SQLException {
		if (dds == null){ 
			throw new SQLException("OracleDataSource is null.");
		}
		return dds.getConnection();
	}
	/**
	 * 打印连接池当前状态
	 * @return
	 */
	public static String getPoolStackTrace (){
		StringBuffer sb = new StringBuffer();
		sb.append("当前连接池状态:").append("\r\n");
		sb.append(dds.dump()).append("\r\n");
		sb.append("活动连接状态:").append("\r\n");
		List<String> stack = dds.getActiveConnectionStackTrace();
		for(String str : stack){
			sb.append(str).append("\r\n");
		}
		return sb.toString();
	}
	
}
