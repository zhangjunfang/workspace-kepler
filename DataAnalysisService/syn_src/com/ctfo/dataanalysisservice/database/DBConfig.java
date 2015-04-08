package com.ctfo.dataanalysisservice.database;

import java.io.File;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.lingtu.xmlconf.XmlConf;

/**
 * 读取配置文件数据库操作参数
 * 
 * @author yangyi
 * 
 */
public class DBConfig {
	public File file = null;
	public Logger root = Logger.getLogger(this.getClass());
	private String driverUame;
	private String databaseUrl;
	private String databaseUser;
	private String databasePassword;

	private String mysqldriverUame;
	private String mysqldatabaseUrl;
	private String mysqldatabaseUser;
	private String mysqldatabasePassword;

	private int timeout = 5000;
	private int maxIdletime = 1800;
	private int minPoolsize = 5;
	private int maxPoolsize = 50;
	private int acquireIncrement = 5;

	private int initialPoolsize = 10;
	private int idleTestPeriod = 3000;
	private String validate = "true";

	public static DBConfig config = null;

	public static DBConfig getInstance() {
		if (config == null) {
			// Properties p = new Properties();
			return new DBConfig(DataAnalysisServiceMain.config);
		}
		return null;
	}

	public DBConfig(XmlConf config) {
		try {
			// oracle配置
			this.driverUame = config.getStringValue("DBconfig|DRIVER_NAME");
			this.databaseUrl = config.getStringValue("DBconfig|DATABASE_URL");
			this.databaseUser = config.getStringValue("DBconfig|DATABASE_USER");
			this.databasePassword = config
					.getStringValue("DBconfig|DATABASE_PASSWORD");

			// 连接池配置
			this.initialPoolsize = new Integer(
					config.getStringValue("DBconfig|INITIAL_POOLSIZE"));
			this.minPoolsize = new Integer(
					config.getStringValue("DBconfig|MIN_POOLSIZE"));
			this.maxPoolsize = new Integer(
					config.getStringValue("DBconfig|MAX_POOLSIZE"));
			this.acquireIncrement = new Integer(
					config.getStringValue("DBconfig|ACQUIRE_INCREMENT"));
			this.timeout = new Integer(
					config.getStringValue("DBconfig|TIMEOUT"));
			this.maxIdletime = new Integer(
					config.getStringValue("DBconfig|MAX_IDLETIME"));
			this.idleTestPeriod = new Integer(
					config.getStringValue("DBconfig|IDLE_TEST_PERIOD"));
			this.validate = config.getStringValue("DBconfig|VALIDATE");

			// mysql配置
			this.mysqldriverUame = config
					.getStringValue("DBconfig|MYSQLDRIVER_NAME");
			this.mysqldatabaseUrl = config
					.getStringValue("DBconfig|MYSQLDATABASE_URL");
			this.mysqldatabaseUser = config
					.getStringValue("DBconfig|MYSQLDATABASE_USER");
			this.mysqldatabasePassword = config
					.getStringValue("DBconfig|MYSQLDATABASE_PASSWORD");

		} catch (Exception ex) {
			root.error("数据库配置加载失败", ex);
		}

	}

	/**
	 * @return the driverUame
	 */
	public String getDriverUame() {
		return driverUame;
	}

	/**
	 * @param driverUame
	 *            the driverUame to set
	 */
	public void setDriverUame(String driverUame) {
		this.driverUame = driverUame;
	}

	/**
	 * @return the databaseUrl
	 */
	public String getDatabaseUrl() {
		return databaseUrl;
	}

	/**
	 * @param databaseUrl
	 *            the databaseUrl to set
	 */
	public void setDatabaseUrl(String databaseUrl) {
		this.databaseUrl = databaseUrl;
	}

	/**
	 * @return the databaseUser
	 */
	public String getDatabaseUser() {
		return databaseUser;
	}

	/**
	 * @param databaseUser
	 *            the databaseUser to set
	 */
	public void setDatabaseUser(String databaseUser) {
		this.databaseUser = databaseUser;
	}

	/**
	 * @return the databasePassword
	 */
	public String getDatabasePassword() {
		return databasePassword;
	}

	/**
	 * @param databasePassword
	 *            the databasePassword to set
	 */
	public void setDatabasePassword(String databasePassword) {
		this.databasePassword = databasePassword;
	}

	/**
	 * @return the timeout
	 */
	public int getTimeout() {
		return timeout;
	}

	/**
	 * @param timeout
	 *            the timeout to set
	 */
	public void setTimeout(int timeout) {
		this.timeout = timeout;
	}

	/**
	 * @return the maxIdletime
	 */
	public int getMaxIdletime() {
		return maxIdletime;
	}

	/**
	 * @param maxIdletime
	 *            the maxIdletime to set
	 */
	public void setMaxIdletime(int maxIdletime) {
		this.maxIdletime = maxIdletime;
	}

	/**
	 * @return the minPoolsize
	 */
	public int getMinPoolsize() {
		return minPoolsize;
	}

	/**
	 * @param minPoolsize
	 *            the minPoolsize to set
	 */
	public void setMinPoolsize(int minPoolsize) {
		this.minPoolsize = minPoolsize;
	}

	/**
	 * @return the maxPoolsize
	 */
	public int getMaxPoolsize() {
		return maxPoolsize;
	}

	/**
	 * @param maxPoolsize
	 *            the maxPoolsize to set
	 */
	public void setMaxPoolsize(int maxPoolsize) {
		this.maxPoolsize = maxPoolsize;
	}

	/**
	 * @return the initialPoolsize
	 */
	public int getInitialPoolsize() {
		return initialPoolsize;
	}

	/**
	 * @param initialPoolsize
	 *            the initialPoolsize to set
	 */
	public void setInitialPoolsize(int initialPoolsize) {
		this.initialPoolsize = initialPoolsize;
	}

	/**
	 * @return the idleTestPeriod
	 */
	public int getIdleTestPeriod() {
		return idleTestPeriod;
	}

	/**
	 * @param idleTestPeriod
	 *            the idleTestPeriod to set
	 */
	public void setIdleTestPeriod(int idleTestPeriod) {
		this.idleTestPeriod = idleTestPeriod;
	}

	/**
	 * @return the validate
	 */
	public String getValidate() {
		return validate;
	}

	/**
	 * @param validate
	 *            the validate to set
	 */
	public void setValidate(String validate) {
		this.validate = validate;
	}

	/**
	 * @return the config
	 */
	public static DBConfig getConfig() {
		return config;
	}

	/**
	 * @param config
	 *            the config to set
	 */
	public static void setConfig(DBConfig config) {
		DBConfig.config = config;
	}

	/**
	 * @return the acquireIncrement
	 */
	public int getAcquireIncrement() {
		return acquireIncrement;
	}

	/**
	 * @param acquireIncrement
	 *            the acquireIncrement to set
	 */
	public void setAcquireIncrement(int acquireIncrement) {
		this.acquireIncrement = acquireIncrement;
	}

	public String getMysqldriverUame() {
		return mysqldriverUame;
	}

	public void setMysqldriverUame(String mysqldriverUame) {
		this.mysqldriverUame = mysqldriverUame;
	}

	public String getMysqldatabaseUrl() {
		return mysqldatabaseUrl;
	}

	public void setMysqldatabaseUrl(String mysqldatabaseUrl) {
		this.mysqldatabaseUrl = mysqldatabaseUrl;
	}

	public String getMysqldatabaseUser() {
		return mysqldatabaseUser;
	}

	public void setMysqldatabaseUser(String mysqldatabaseUser) {
		this.mysqldatabaseUser = mysqldatabaseUser;
	}

	public String getMysqldatabasePassword() {
		return mysqldatabasePassword;
	}

	public void setMysqldatabasePassword(String mysqldatabasePassword) {
		this.mysqldatabasePassword = mysqldatabasePassword;
	}
}
