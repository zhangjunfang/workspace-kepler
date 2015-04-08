package com.ctfo.syn.membeans;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public final class DataBaseConnection {

	private static String DBDRIVER = "";
	private static String DBURL = "";
	private static String DBUSERNAME = "";
	private static String DBPASSWORD = "";

	private Connection conn = null;

	// 构造函数私有
	private DataBaseConnection() {

	}

	// 构造私有实例
	private static DataBaseConnection instance = null;

	public static DataBaseConnection getInstance(String dbdriver, String dburl,
			String username, String password) {
		DBDRIVER = dbdriver;
		DBURL = dburl;
		DBUSERNAME = username;
		DBPASSWORD = password;

		// 延迟加载
		if (instance == null) {

			// 加锁 防止线程并发
			synchronized (DataBaseConnection.class) {
				if (instance == null) {

					instance = new DataBaseConnection();

				}
			}
		}
		return instance;
	}
	
	// 获取数据库连接
	public Connection getConnection() throws SQLException {		
		try {
			Class.forName(DBDRIVER); 
		} catch (ClassNotFoundException e) {
			throw new ExceptionInInitializerError(e); 
		}
		return DriverManager.getConnection(DBURL, DBUSERNAME, DBPASSWORD);
		
	}
	
	// 关闭数据库连接
	public void close(PreparedStatement pstmt, ResultSet rs, Connection conn) {	
		try {		
			if(pstmt != null){
				pstmt.close();
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
			} catch (Exception e) {
				e.printStackTrace();
			} finally {
				try {
					if(conn!=null){
						conn.close();
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
	}
}
