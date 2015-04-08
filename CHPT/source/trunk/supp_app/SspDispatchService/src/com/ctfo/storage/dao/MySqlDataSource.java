package com.ctfo.storage.dao;

import java.sql.Connection;
import java.sql.SQLException;

import com.alibaba.druid.pool.DruidDataSource;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： MySql数据接口<br>
 * 描述： MySql数据接口<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
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
 * <td>2014-10-14</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class MySqlDataSource {

	/** 连接池 */
	private static DruidDataSource druidDataSource = null;

	private static MySqlDataSource mySqlDataSource = new MySqlDataSource();

	/** 连接类 */
	private String driver;

	/** 连接地址 */
	private String url;

	/** 用户名 */
	private String username;

	/** 密码 */
	private String password;

	/** 最大连接数 */
	private int maxActive;

	/** 最小空闲数 */
	private int minIdle;

	/** 连接初始大小 */
	private int initialSize;

	public static MySqlDataSource getInstance() {
		if (mySqlDataSource == null) {
			mySqlDataSource = new MySqlDataSource();
		}
		return mySqlDataSource;
	}

	public void init() throws SQLException {
		druidDataSource = new DruidDataSource();
		// druidDataSource.setDriverClassName(driver);
		druidDataSource.setOracle(false);
		druidDataSource.setUrl(url);
		druidDataSource.setUsername(username);
		druidDataSource.setPassword(password);
		druidDataSource.setMaxActive(maxActive);
		druidDataSource.setMinIdle(minIdle);
		druidDataSource.setInitialSize(initialSize);
		druidDataSource.setMaxWait(3000); // 配置获取连接等待超时的时间。 单位是毫秒
		druidDataSource.setTimeBetweenEvictionRunsMillis(60000); // 配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒
		druidDataSource.setMinEvictableIdleTimeMillis(300000); // 配置一个连接在池中最小生存的时间，单位是毫秒
		druidDataSource.setPoolPreparedStatements(true); // 打开PSCache，并且指定每个连接上PSCache的大小
		druidDataSource.setTestOnBorrow(true);
		druidDataSource.setTestWhileIdle(true);
		druidDataSource.setValidationQuery("SELECT 'x'");
		druidDataSource.setFilters("stat");
		// druidDataSource.setRemoveAbandoned(false);
		// druidDataSource.setRemoveAbandonedTimeoutMillis(300000);
		druidDataSource.init();
	}

	/**
	 * 关闭连接
	 */
	public static void destroy() {
		druidDataSource.close();
	}

	public static Connection getConnection() throws SQLException {
		return druidDataSource.getConnection();
	}

	/******************************** GET AND SET ********************************/

	public String getDriver() {
		return driver;
	}

	public void setDriver(String driver) {
		this.driver = driver;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public int getMaxActive() {
		return maxActive;
	}

	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}

	public int getMinIdle() {
		return minIdle;
	}

	public void setMinIdle(int minIdle) {
		this.minIdle = minIdle;
	}

	public int getInitialSize() {
		return initialSize;
	}

	public void setInitialSize(int initialSize) {
		this.initialSize = initialSize;
	}

}
