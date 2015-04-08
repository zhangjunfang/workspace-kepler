/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model OracleProperties.java	</li><br>
 * <li>时        间：2013-10-23  下午1:34:09	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.model;

/*****************************************
 * <li>描        述：Oracle配置		
 * 
 *****************************************/
public class OrclProperties {
	/** oracle地址 */
	private String url;
	/** 用户名 */
	private String username;
	/** 密码 */
	private String password;
	/** 最大活动连接 */
	private int maxActive;
	/** 最小空闲连接 */
	private int minIdle;
	/** 初始化连接数 */
	private int initialSize;
	/**
	 * 获得oracle地址的值
	 * @return the url oracle地址  
	 */
	public String getUrl() {
		return url;
	}
	/**
	 * 设置oracle地址的值
	 * @param url oracle地址  
	 */
	public void setUrl(String url) {
		this.url = url;
	}
	/**
	 * 获得用户名的值
	 * @return the username 用户名  
	 */
	public String getUsername() {
		return username;
	}
	/**
	 * 设置用户名的值
	 * @param username 用户名  
	 */
	public void setUsername(String username) {
		this.username = username;
	}
	/**
	 * 获得密码的值
	 * @return the password 密码  
	 */
	public String getPassword() {
		return password;
	}
	/**
	 * 设置密码的值
	 * @param password 密码  
	 */
	public void setPassword(String password) {
		this.password = password;
	}
	/**
	 * 获得最大活动连接的值
	 * @return the maxActive 最大活动连接  
	 */
	public int getMaxActive() {
		return maxActive;
	}
	/**
	 * 设置最大活动连接的值
	 * @param maxActive 最大活动连接  
	 */
	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}
	/**
	 * 获得最小空闲连接的值
	 * @return the minIdle 最小空闲连接  
	 */
	public int getMinIdle() {
		return minIdle;
	}
	/**
	 * 设置最小空闲连接的值
	 * @param minIdle 最小空闲连接  
	 */
	public void setMinIdle(int minIdle) {
		this.minIdle = minIdle;
	}
	/**
	 * 获得初始化连接数的值
	 * @return the initialSize 初始化连接数  
	 */
	public int getInitialSize() {
		return initialSize;
	}
	/**
	 * 设置初始化连接数的值
	 * @param initialSize 初始化连接数  
	 */
	public void setInitialSize(int initialSize) {
		this.initialSize = initialSize;
	}

}
