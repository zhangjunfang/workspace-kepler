package com.caits.analysisserver.database;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.Calendar;
import java.util.Properties;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;



import oracle.jdbc.pool.OracleConnectionCacheManager;
import oracle.jdbc.pool.OracleDataSource;

@SuppressWarnings("deprecation")
public class OracleConnectionPool {
	private static final Logger logger = LoggerFactory.getLogger(OracleConnectionPool.class);
	private static OracleDataSource oracleDataSource = null;
	private  static String CACHE_NAME = "KCPT"; // 新建的线程池名，也可以说是标识符
	
	public static void initConnection(String url,String user,String password,String minLimit,String maxLimit,String InitialLimit,String connectionWaitTimeout,String propertyCheckInterval) throws SQLException{
		oracleDataSource = new OracleDataSource();
		oracleDataSource.setURL(url);
		oracleDataSource.setUser(user); // 用户名
		oracleDataSource.setPassword(password);// 口令
		// open cache
		OracleConnectionCacheManager oracleConnectionCacheManager = OracleConnectionCacheManager
				.getConnectionCacheManagerInstance();
		if (oracleConnectionCacheManager.existsCache(CACHE_NAME)) {
			// 在类重载后tomcat会重新创建单例，而此前建立的数据库缓存并没有移除，
			// 所以会造成"您要建立的缓存已经存在"的错误，
			// 此判断用来移除已经存在的缓存
			oracleConnectionCacheManager.removeCache(CACHE_NAME, 0); // 重载后移除缓存
			logger.debug(Calendar.getInstance().getTime()+ " 重新建立数据库连接缓存 ");
		} else {
			oracleDataSource.setConnectionCachingEnabled(true); // 开启缓存
		}
		// 设置配置文件
		Properties cacheProperties = new Properties();
		cacheProperties.setProperty("MinLimit", minLimit);
		cacheProperties.setProperty("MaxLimit", maxLimit);
		cacheProperties.setProperty("InitialLimit", InitialLimit);
		cacheProperties.setProperty("ConnectionWaitTimeout", connectionWaitTimeout);
//			cacheProperties.setProperty("AbandonedConnectionTimeout", abandonedConnectionTimeout);
//			cacheProperties.setProperty("InactivityTimeout", inactivityTimeout); // Sets the maximum time a physical connection can remain idle in a connection cache.
		cacheProperties.setProperty("PropertyCheckInterval", propertyCheckInterval); // Sets the time interval at which the cache manager inspects and enforces all specified cache properties
		oracleConnectionCacheManager.createCache(CACHE_NAME, oracleDataSource, cacheProperties);// 激活参数
		cacheProperties = null;
	}

	public static synchronized Connection getConnection() throws SQLException {
		//logger.debug("-----get db connection start!-----");
		// 获得连接
		if (oracleDataSource == null)
			throw new SQLException("OracleDataSource is null.");
		
		Connection conn = oracleDataSource.getConnection();
		//logger.debug("-----get db connection OK!-----");
		return conn;
	}

	public static void closePooledConnection() throws SQLException {
		// 关闭缓存池
		if (oracleDataSource != null)
			oracleDataSource.close();
	}

	public static String listCacheInfos() throws SQLException {
		// 查看运行时连接信息
		StringBuffer buf = new StringBuffer("");
		OracleConnectionCacheManager oracleConnectionCacheManager = OracleConnectionCacheManager.getConnectionCacheManagerInstance();
		if (oracleConnectionCacheManager.existsCache(CACHE_NAME)) {
			buf.append(oracleConnectionCacheManager.getNumberOfAvailableConnections(CACHE_NAME) + " connections are available in cache " + CACHE_NAME);
			buf.append(";");
			buf.append(oracleConnectionCacheManager.getNumberOfActiveConnections(CACHE_NAME) + " connections are active.");
		}
		return buf.toString();
	}
}
