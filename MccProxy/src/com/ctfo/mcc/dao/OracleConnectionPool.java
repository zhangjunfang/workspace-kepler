/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.dao OracleConnectionPool.java	</li><br>
 * <li>时        间：2013-9-18  上午11:54:56	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.mcc.dao;

import java.sql.Connection;
import java.sql.SQLException;

import com.alibaba.druid.pool.DruidDataSource;
import com.ctfo.mcc.model.OracleProperties;


/*****************************************
 * <li>描        述：连接池		
 * 
 *****************************************/
public class OracleConnectionPool {
	private static DruidDataSource dds = null;

	public OracleConnectionPool(){
	}
	public static void init(OracleProperties oracleProperties) throws SQLException{ 
//		<!-- 配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒 -->
//		<property name="timeBetweenEvictionRunsMillis" value="60000" />
//		<!-- 配置一个连接在池中最小生存的时间，单位是毫秒 -->
//		<property name="minEvictableIdleTimeMillis" value="300000" />
//		<property name="validationQuery" value="SELECT 'x'" />
//		<property name="testWhileIdle" value="true" />
//		<property name="testOnBorrow" value="false" />
//		<property name="testOnReturn" value="false" />

		dds = new DruidDataSource();
		dds.setUrl(oracleProperties.getUrl());
		dds.setUsername(oracleProperties.getUsername());
		dds.setPassword(oracleProperties.getPassword());
//		<!-- 配置初始化大小、最小、最大 -->
		dds.setInitialSize(oracleProperties.getInitialSize());
		dds.setMaxActive(oracleProperties.getMaxActive());
		dds.setMinIdle(oracleProperties.getMinIdle());
//		<!-- 配置获取连接等待超时的时间。 单位是毫秒 -->
		dds.setMaxWait(oracleProperties.getMaxWait());
		
		dds.setTimeBetweenEvictionRunsMillis(oracleProperties.getTimeBetweenEvictionRunsMillis());
		dds.setMinEvictableIdleTimeMillis(oracleProperties.getMinEvictableIdleTimeMillis());
		dds.setValidationQuery("SELECT 'x' FROM DUAL");
		dds.setTestWhileIdle(oracleProperties.getTestWhileIdle());
		dds.setTestOnBorrow(oracleProperties.getTestOnBorrow());
		dds.setTestOnReturn(oracleProperties.getTestOnReturn());
//		<!-- 打开PSCache，并且指定每个连接上PSCache的大小 -->
		dds.setPoolPreparedStatements(true); 
		dds.setMaxPoolPreparedStatementPerConnectionSize(oracleProperties.getMaxPoolPreparedStatementPerConnectionSize());
//		<!-- 配置监控统计拦截的filters -->
		dds.setFilters("stat");
		
		dds.setName("oracle");
		
		dds.init(); 
	}

	public static Connection getConnection() throws SQLException {
		// 获得连接
		if (dds == null){
			throw new SQLException("OracleConnectionPoolDataSource is null.");
		}
		return dds.getConnection();
	}
}
