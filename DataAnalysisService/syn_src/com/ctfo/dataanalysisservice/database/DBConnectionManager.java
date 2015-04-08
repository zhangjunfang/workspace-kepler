package com.ctfo.dataanalysisservice.database;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.mchange.v2.c3p0.ComboPooledDataSource;

/**
 * 初始化连接池
 * 
 * @author Administrator
 * 
 */
public class DBConnectionManager {
	private static Log log = LogFactory.getLog(DBConnectionManager.class);
	private static ComboPooledDataSource cpds = null;
	private static ComboPooledDataSource mysqlcpds = null;

	/**
	 * oracle初始化c3po连接池
	 */
	public static void init() {
		log.debug("oracle数据库配置加载Begin");

		try {
			DBConfig config = DBConfig.getInstance();

			// oracle初始化
			cpds = new ComboPooledDataSource();
			cpds.setDriverClass(config.getDriverUame()); // 驱动器
			cpds.setJdbcUrl(config.getDatabaseUrl()); // 数据库url
			cpds.setUser(config.getDatabaseUser()); // 用户名
			cpds.setPassword(config.getDatabasePassword()); // 密码
			cpds.setInitialPoolSize(config.getInitialPoolsize()); // 初始化连接池大小
			cpds.setMinPoolSize(config.getMinPoolsize()); // 最少连接数
			cpds.setMaxPoolSize(config.getMaxPoolsize()); // 最大连接数
			cpds.setAcquireIncrement(config.getAcquireIncrement()); // 连接数的增量
			cpds.setIdleConnectionTestPeriod(config.getIdleTestPeriod()); // \x{fffd}测连接有效的时间间隔
			cpds.setTestConnectionOnCheckout(Boolean.getBoolean(config
					.getValidate())); // 每次连接验证连接是否可用
			cpds.setMaxIdleTime(config.getMaxIdletime());

		} catch (Exception ex) {
			ex.printStackTrace();
		}
		log.debug("oracle数据库配置加载End");
	}

	/**
	 * mysql初始化c3po连接池
	 */
	public static void mysqlInit() {
		log.debug("mysql数据库配置加载Begin");

		try {
			DBConfig config = DBConfig.getInstance();

			// mysql初始化
			mysqlcpds = new ComboPooledDataSource();
			mysqlcpds.setDriverClass(config.getMysqldriverUame()); // 驱动器
			mysqlcpds.setJdbcUrl(config.getMysqldatabaseUrl()); // 数据库url
			mysqlcpds.setUser(config.getMysqldatabaseUser()); // 用户名
			mysqlcpds.setPassword(config.getMysqldatabasePassword()); // 密码
			
			mysqlcpds.setInitialPoolSize(config.getInitialPoolsize()); // 初始化连接池大小
			mysqlcpds.setMinPoolSize(config.getMinPoolsize()); // 最少连接数
			mysqlcpds.setMaxPoolSize(config.getMaxPoolsize()); // 最大连接数
			mysqlcpds.setAcquireIncrement(config.getAcquireIncrement()); // 连接数的增量
			mysqlcpds.setIdleConnectionTestPeriod(config.getIdleTestPeriod()); // \x{fffd}测连接有效的时间间隔
			mysqlcpds.setTestConnectionOnCheckout(Boolean.getBoolean(config
					.getValidate())); // 每次连接验证连接是否可用
			mysqlcpds.setMaxIdleTime(config.getMaxIdletime());
		} catch (Exception ex) {
			ex.printStackTrace();
		}
		log.debug("mysql数据库配置加载End");
	}

	/**
	 * oracle获取数据库连接
	 * 
	 * @return
	 */
	public static Connection getConnection() {
		Connection connection = null;
		try {
			if (cpds == null) {
				init();
			}
			connection = cpds.getConnection();
		} catch (SQLException ex) {
			ex.printStackTrace();
		}
		return connection;
	}

	/**
	 * mysql获取数据库连接
	 * 
	 * @return
	 */
	public static Connection getMysqlConnection() {
		Connection connection = null;
		try {
			if (mysqlcpds == null) {
				mysqlInit();
			}
			connection = mysqlcpds.getConnection();
		} catch (SQLException ex) {
			ex.printStackTrace();
		}
		return connection;
	}

	/**
	 * 重置连接
	 */
	public static void release() {
		try {
			if (cpds != null) {
				cpds.close();
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * mysql重置连接
	 */
	public static void mysqlRelease() {
		try {
			if (mysqlcpds != null) {
				mysqlcpds.close();
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * 释放数据库通道资源
	 * 
	 * @param rs
	 *            ResultSet
	 * @param st
	 *            Statement
	 * @param conn
	 *            Connection
	 */
	public static void freeDBResource(ResultSet rs, Statement st,
			Connection conn) {
		if (rs != null) {
			try {
				rs.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}

		if (st != null) {
			try {
				st.close();
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}

		freeConnection(conn);
	}

	/**
	 * 释放数据库资源
	 */
	public static void freeDBResource(Statement st, Connection conn) {
		if (st != null) {
			try {
				st.close();
			} catch (SQLException e) {
				e.printStackTrace();
				log.error(e);
			}
		}

		freeConnection(conn);
	}

	/**
	 * 释放连接
	 */
	private static void freeConnection(Connection conn) {
		try {
			if (conn != null) {
				conn.close();
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

}
