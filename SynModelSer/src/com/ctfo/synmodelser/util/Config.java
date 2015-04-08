package com.ctfo.synmodelser.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Properties;

import com.ctfo.synmodelser.jdbc.JdbcManager;

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
public class Config {
	/**
	 * URL地址
	 */
	public String url;
	/**
	 * 用户名
	 */
	public String userName;
	/**
	 * 密码
	 */
	public String password;
	/**
	 * Memcached 服务器地址
	 */
	public String memServer;
	/**
	 * 驱动名称
	 */
	public String className;
	/**
	 * 配置
	 */
	public static Config config;
	/**
	 * HTTP服务器地址
	 */
	public String httpServer;

	/**
	 * 获取系统默认的配置信息
	 * 
	 * @return 返回配置信息
	 */
	public static Config getInstance() {
		if (config == null) {
			config = new Config();
			config.getConfig();
		}
		return config;
	}

	public Config() {
	}

	/**
	 * 根据参数获取配置信息
	 * 
	 * @param filePath
	 */
	public Config(String filePath) {
		getConfig(filePath);
	}

	/**
	 * 获取默认配置信息
	 */
	private void getConfig() {
		Properties p = new Properties();
		try {
			p.load(JdbcManager.class.getClass().getResourceAsStream("/system.properties"));
			this.url = p.getProperty("oracle_url");
			this.userName = p.getProperty("oracle_user");
			this.password = p.getProperty("oracle_password");
			this.memServer = p.getProperty("memServer");
			this.className = p.getProperty("oracle_xaDataSourceClassName");
			this.httpServer = p.getProperty("httpServer");
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * 根据文件获取配置信息
	 * 
	 * @param filePath
	 *            文件目录
	 */
	private void getConfig(String filePath) {
		Properties p = new Properties();
		try {
			p.load(new FileInputStream(new File(filePath)));
			this.url = p.getProperty("oracle_url");
			this.userName = p.getProperty("oracle_user");
			this.password = p.getProperty("oracle_password");
			this.memServer = p.getProperty("memServer");
			this.className = p.getProperty("oracle_xaDataSourceClassName");
			this.httpServer = p.getProperty("httpServer");
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * @return the url
	 */
	public String getUrl() {
		return url;
	}

	/**
	 * @param url
	 *            the url to set
	 */
	public void setUrl(String url) {
		this.url = url;
	}

	/**
	 * @return the userName
	 */
	public String getUserName() {
		return userName;
	}

	/**
	 * @param userName
	 *            the userName to set
	 */
	public void setUserName(String userName) {
		this.userName = userName;
	}

	/**
	 * @return the password
	 */
	public String getPassword() {
		return password;
	}

	/**
	 * @param password
	 *            the password to set
	 */
	public void setPassword(String password) {
		this.password = password;
	}

	/**
	 * @return the memServer
	 */
	public String getMemServer() {
		return memServer;
	}

	/**
	 * @param memServer
	 *            the memServer to set
	 */
	public void setMemServer(String memServer) {
		this.memServer = memServer;
	}

	/**
	 * @return the className
	 */
	public String getClassName() {
		return className;
	}

	/**
	 * @param className
	 *            the className to set
	 */
	public void setClassName(String className) {
		this.className = className;
	}

	/**
	 * @return the httpServer
	 */
	public String getHttpServer() {
		return httpServer;
	}

	/**
	 * @param httpServer
	 *            the httpServer to set
	 */
	public void setHttpServer(String httpServer) {
		this.httpServer = httpServer;
	}

}
