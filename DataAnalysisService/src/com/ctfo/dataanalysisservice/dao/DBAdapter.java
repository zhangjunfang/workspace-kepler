package com.ctfo.dataanalysisservice.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.lingtu.xmlconf.XmlConf;

/**
 * Title: 轨迹分析服务数据库基础类 Description: 轨迹分析服务数据库接口
 */
public class DBAdapter {
	// 配置
	protected XmlConf config;
	// 日志类
	protected org.apache.log4j.Category cat;

	// 数据库驱动
	protected String dbDriver;
	// 数据库连接字
	protected String dbConString;
	// 数据库用户名
	protected String dbUserName;
	// 数据库密码
	protected String dbPassword;
	// 数据库断线重新连接时间(秒)
	protected int reconnectWait = 3;

	/**
	 * 构造函数
	 */
	public DBAdapter() {
		//
	}

	/**
	 * 初始化函数
	 * 
	 * @param config
	 *            配置文件类
	 * @param cat
	 *            日志文件类
	 */
	public void initDBAdapter(XmlConf config) throws Exception {
		this.config = config;

		dbDriver = config.getStringValue("database|JDBCDriver");
		dbConString = config.getStringValue("database|JDBCUrl");
		dbUserName = config.getStringValue("database|JDBCUser");
		dbPassword = config.getStringValue("database|JDBCPassword");
		reconnectWait = config.getIntValue("database|DBReconnectWait");
	}

	/**
	 * 返回数据库连接字信息
	 * 
	 * @return 数据库连接字信息
	 */
	public String getDBUrl() {
		return dbConString;
	}

	/**
	 * 提供获得连接的方法
	 * 
	 * @return Connection
	 */
	public Connection getDBConnection() {
		try {
			// 测试数据库连接
			Class.forName(dbDriver);
			Connection con = DriverManager.getConnection(dbConString,
					dbUserName, dbPassword);
			return con;
		} catch (Exception e) {
			return null;
		}
	}

	/**
	 * 测试连接
	 * 
	 * @return boolean true表示连接测试成功；false表示连接失败
	 */
	public boolean testDBConnection() {
		try {
			// 测试数据库连接
			Class.forName(dbDriver);
			Connection con = DriverManager.getConnection(dbConString,
					dbUserName, dbPassword);
			con.close();
			return true;
		} catch (Exception e) {
			return false;
		}
	}

	/**
	 * 判断数据库连接是否正常
	 * 
	 * @return true表示连接正常,false则不正常
	 */
	public boolean isDbConnectionActive() {
		return true;
	}

	/**
	 * 存储错误报文
	 * 
	 * @param app
	 *            位置报文类
	 */
	public void saveErrorPackage(String packagecontent, int errortype,
			String errorcontent) throws SQLException {
		return;
	}

	/**
	 * 根据平台代码查询平台编号
	 * 
	 * @param accesscode
	 *            车牌号
	 * 
	 * @return 平台编号
	 */
	public long queryAccessByCode(Long accesscode) throws SQLException {
		return -1;
	}

	/**
	 * 应用结束，关闭数据库连接和所有语句
	 */
	public void closeDB() {
		return;
	}

	/**
	 * @author lingtu-zhouhuabin 2006-03 统一调用关闭连接
	 * @param rs
	 *            记录集
	 * @param ps
	 *            预编译语句
	 * @param con
	 *            连接
	 */
	public static void closeConnection(ResultSet rs, Statement ps,
			Connection con) {
		if (rs != null) {
			try {
				rs.close();
			} catch (SQLException exrs) {
			}
		}
		if (ps != null) {
			try {
				ps.close();
			} catch (SQLException exps) {
				exps.printStackTrace();
			}
		}
		if (con != null) {
			try {
				con.close();
			} catch (SQLException exconn) {
				exconn.printStackTrace();
			}
		}
	}
}
