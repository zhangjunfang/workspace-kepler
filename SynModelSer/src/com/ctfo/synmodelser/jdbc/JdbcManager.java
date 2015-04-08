package com.ctfo.synmodelser.jdbc;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import com.ctfo.synmodelser.util.Config;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Feb 14, 2012</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class JdbcManager {
	
	/**
	 * 获取数据库连接
	 * 
	 * @return 数据库连接
	 */
	public static Connection getConnection(Config c) {
		if (c == null)
			c = Config.getInstance();
		try {
			Class.forName(c.getClassName());
			String url = c.getUrl();
			String userName = c.getUserName();
			String password = c.getPassword();
			Connection conn = DriverManager.getConnection(url, userName, password);
			System.out.println("-------------------DB connection is success!---------------------");
			return conn;
		} catch (ClassNotFoundException e) {
			System.out.println("-------------------DB connection is error!---------------------");
			System.out.println(e.getMessage());
		} catch (SQLException e) {
			System.out.println("-------------------DB connection is error!---------------------");
			System.out.println(e.getMessage());
		}
		return null;
	}

	/**
	 * 获取数据库连接
	 * 
	 * @return 数据库连接
	 */
	public static Connection getConnection() {
		Config c = Config.getInstance();
		try {
			Class.forName(c.getClassName());
			String url = c.getUrl();
			String userName = c.getUserName();
			String password = c.getPassword();
			Connection conn = DriverManager.getConnection(url, userName, password);
			System.out.println("-------------------DB connection is success!---------------------");
			return conn;
		} catch (ClassNotFoundException e) {
			System.out.println("-------------------DB connection is error!---------------------");
			System.out.println(e.getMessage());
		} catch (SQLException e) {
			System.out.println("-------------------DB connection is error!---------------------");
			System.out.println(e.getMessage());
		}
		return null;
	}

	/**
	 * 断开数据库连接
	 * 
	 * @param conn
	 *            数据库连接
	 */
	public static void disConn(Connection conn) {
		if (conn != null) {
			try {
				conn.close();
				System.out.println("-----------------Dis connection is success.-----------------------");
			} catch (SQLException e) {
				System.out.println("-----------------Dis connection is error.-----------------------");
				System.out.println(e.getMessage());
			}
		}
	}

	public static void main(String[] args) {
		// JdbcManager j = new JdbcManager();
		// Connection conn = j.getConnection();
		// j.disConn(conn);
	}
}
