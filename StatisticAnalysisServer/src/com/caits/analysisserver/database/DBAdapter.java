package com.caits.analysisserver.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.StatusCode;
import com.caits.analysisserver.bean.VehicleInfo;
import com.lingtu.xmlconf.XmlConf;

/**
 * Title: 轨迹分析服务数据库基础类 Description: 轨迹分析服务数据库接口
 */
public class DBAdapter {
	// 配置
	protected XmlConf config;
	// 日志类
//	protected org.apache.log4j.Category cat;

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
	//存储车辆状态对应参考值
	public static Map<String, StatusCode> statusMap = new HashMap<String,StatusCode>();
	
	//初始化存储所有报警编码与报警类型对应关系
	public static Map<String,String> alarmTypeMap = new HashMap<String,String>();
	
	//车辆对应的车辆编号，车架号，企业编号，企业名称，车队编号，车队名称,车牌号、车辆内部编码、线路、司机 
	public static Map<String,VehicleInfo> vehicleInfoMap = new HashMap<String,VehicleInfo>();
	
	public static Map<String,String> oilMonitorMap = new HashMap<String,String>();
	
	public static Map<String,Long>	areaSpeedThresholdMap = new HashMap<String,Long>();
	public static Map<String,Long>	lineSpeedThresholdMap = new HashMap<String,Long>();
	
	public static Map<String,Vector<AlarmCacheBean>>	softAlarmDetailMap = new HashMap<String,Vector<AlarmCacheBean>>();
	
	/**
	 * 构造函数
	 */
	public DBAdapter() {
	}

	/**
	 * 初始化函数
	 * 
	 * @param config
	 *            配置文件类
	 * @param cat
	 *            日志文件类
	 */
	public void initDBAdapter(XmlConf config)
			throws Exception {
		this.config = config;
//		this.cat = cat;

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
	 * 获得 Connection 对象
	 * @param driver,dbUrl,dbUserName,dbPassword
	 * @return
	 * @throws SQLException
	 */
	public static Connection getOracleConnection(String driver, String dbUrl,
			String dbUserName, String dbPassword) {
		Connection conn = null;
		try {
			Class.forName(driver);
			conn = DriverManager.getConnection(dbUrl, dbUserName, dbPassword);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return conn;
	}
	
//	/**
//	 * 关闭 Connection 对象
//	 * @param conn
//	 * @return
//	 * @throws SQLException
//	 */
//	public static void close(Connection conn) {
//		if (conn != null)
//			try {
//				conn.close();
//			} catch (Exception e) {
//				e.printStackTrace();
//			}
//	}
	
	/**
	 * 关闭 Statement 对象
	 * @param stmt
	 * @return
	 * @throws SQLException
	 */
	public static void close(Statement stmt) {
		if (stmt != null)
			try {
				stmt.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
	}
	/**
	 * 关闭 ResultSet 对象
	 * @param rs
	 * @return
	 * @throws SQLException
	 */
	public static void close(ResultSet rs) {
		if (rs != null)
			try {
				rs.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
	}
	
	public static void clearCollections(){
		statusMap.clear();
		alarmTypeMap.clear();
		vehicleInfoMap.clear();
		oilMonitorMap.clear();
		areaSpeedThresholdMap.clear();
		lineSpeedThresholdMap.clear();
		softAlarmDetailMap.clear();
	}
}
