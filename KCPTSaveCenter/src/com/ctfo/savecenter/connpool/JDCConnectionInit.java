package com.ctfo.savecenter.connpool;

import java.sql.SQLException;

import com.ctfo.redis.pool.JedisConnectionPool;
import com.lingtu.xmlconf.XmlConf;

public class JDCConnectionInit {
	XmlConf config;
	public JDCConnectionInit(XmlConf config){
		this.config=config;
	}

	public  void init() throws ClassNotFoundException,
			InstantiationException, IllegalAccessException, SQLException {

		int connectionPoolSize = 0;// 初始化时，在连接池中建立的连接对象数目
		//String oracledbDriver = config.getStringValue("database|JDBCDriver");// mysql数据库驱动
		String oracledbConString = config.getStringValue("database|JDBCUrl");// mysql数据库连接字
		String oracledbUserName = config.getStringValue("database|JDBCUser");// mysql数据库用户名
		String oracledbPassword = config
				.getStringValue("database|JDBCPassword");// mysql数据库密码
		String mysqldbDriver = config
				.getStringValue("database|MysqlJDBCDriver");
		String mysqldbConString = config
				.getStringValue("database|MysqlJDBCUrl");
		String mysqldbUserName = config
				.getStringValue("database|MysqlJDBCUser");
		String mysqldbPassword = config
				.getStringValue("database|MysqlJDBCPassword");
		int timeout = config.getIntValue("database|DBReconnectWait");// 设置连接超时时间
		int maxLimit = config.getIntValue("database|connectionPoolSize");// 连接池中至多能建立的连接对象数目，即连接池的上限
		int minLimit = config.getIntValue("database|minLimit");// 连接池中最小连接数
		int InitialLimit = config.getIntValue("database|InitialLimit");// 连接池中初始化连接数
		
		String abandonedConnectionTimeout = config.getStringValue("database|abandonedConnectionTimeout");
		String propertyCheckInterval = config.getStringValue("database|propertyCheckInterval");
		String inactivityTimeout= config.getStringValue("database|inactivityTimeout");
		
		// 初始ORACLE连接池
		OracleConnectionPool.initConnection(oracledbConString, oracledbUserName, oracledbPassword, minLimit + "", maxLimit + "", InitialLimit + "", timeout + "",inactivityTimeout,abandonedConnectionTimeout,propertyCheckInterval);

		// 初始MYSQL连接池
		new JDCConnectionMysqlDriver(mysqldbDriver, mysqldbConString,
				mysqldbUserName, mysqldbPassword, connectionPoolSize, timeout,
				maxLimit);
		
//		RedisConnectionPool.initRedisConnectionPool(config.getStringValue("database|redisHost"), config.getIntValue("database|redisPort"), config.getStringValue("database|redisPwd"),config.getIntValue("database|redisMaxActive") ,config.getIntValue("database|redisMaxIdle") ,config.getIntValue("database|redisMaxWait"),config.getIntValue("database|redisTimeout"));
		JedisConnectionPool.initRedisConnectionPool(config.getStringValue("database|redisHost"), config.getIntValue("database|redisPort"), config.getStringValue("database|redisPwd"),config.getIntValue("database|redisMaxActive") ,config.getIntValue("database|redisMaxIdle") ,config.getIntValue("database|redisMaxWait"),config.getIntValue("database|redisTimeout"));
	}

}
