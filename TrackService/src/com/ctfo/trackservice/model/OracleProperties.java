/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model OracleProperties.java	</li><br>
 * <li>时        间：2013-10-23  下午1:34:09	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.model;

/*****************************************
 * <li>描        述：Oracle配置		
 * 
 *****************************************/
public class OracleProperties {
	/** oracle地址 */
	private String url;
	/** 用户名 */
	private String username;
	/** 密码 */
	private String password;
	/** 最大活动连接 */
	private int maxActive = 200;
	/** 最小空闲连接 */
	private int minIdle = 10;
	/** 初始化连接数 */
	private int initialSize = 10;
	/** 获取连接时最大等待时间，单位毫秒 */
	private int maxWait = 60000;
	/** Destroy线程会检测连接的间隔时间、testWhileIdle的判断依据 */
	private int timeBetweenEvictionRunsMillis = 60000;
	/** 最小生存时间 */
	private int minEvictableIdleTimeMillis = 300000;
	/** 最大预定义缓存 */
	private int maxOpenPreparedStatements = 20;
	/** 申请连接的时候检测连接是否有效 */
	private boolean testWhileIdle = true;
	/** 申请连接时执行validationQuery检测连接是否有效 */
	private boolean testOnBorrow = true;
	/** 归还连接时执行validationQuery检测连接是否有效 */
	private boolean testOnReturn = false;
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
	/**
	 * 获得获取连接时最大等待时间，单位毫秒的值
	 * @return the maxWait 获取连接时最大等待时间，单位毫秒  
	 */
	public int getMaxWait() {
		return maxWait;
	}
	/**
	 * 设置获取连接时最大等待时间，单位毫秒的值
	 * @param maxWait 获取连接时最大等待时间，单位毫秒  
	 */
	public void setMaxWait(int maxWait) {
		this.maxWait = maxWait;
	}
	/**
	 * 获得Destroy线程会检测连接的间隔时间、testWhileIdle的判断依据的值
	 * @return the timeBetweenEvictionRunsMillis Destroy线程会检测连接的间隔时间、testWhileIdle的判断依据  
	 */
	public int getTimeBetweenEvictionRunsMillis() {
		return timeBetweenEvictionRunsMillis;
	}
	/**
	 * 设置Destroy线程会检测连接的间隔时间、testWhileIdle的判断依据的值
	 * @param timeBetweenEvictionRunsMillis Destroy线程会检测连接的间隔时间、testWhileIdle的判断依据  
	 */
	public void setTimeBetweenEvictionRunsMillis(int timeBetweenEvictionRunsMillis) {
		this.timeBetweenEvictionRunsMillis = timeBetweenEvictionRunsMillis;
	}
	/**
	 * 获得最小生存时间的值
	 * @return the minEvictableIdleTimeMillis 最小生存时间  
	 */
	public int getMinEvictableIdleTimeMillis() {
		return minEvictableIdleTimeMillis;
	}
	/**
	 * 设置最小生存时间的值
	 * @param minEvictableIdleTimeMillis 最小生存时间  
	 */
	public void setMinEvictableIdleTimeMillis(int minEvictableIdleTimeMillis) {
		this.minEvictableIdleTimeMillis = minEvictableIdleTimeMillis;
	}
	/**
	 * 获得最大预定义缓存的值
	 * @return the maxOpenPreparedStatements 最大预定义缓存  
	 */
	public int getMaxOpenPreparedStatements() {
		return maxOpenPreparedStatements;
	}
	/**
	 * 设置最大预定义缓存的值
	 * @param maxOpenPreparedStatements 最大预定义缓存  
	 */
	public void setMaxOpenPreparedStatements(int maxOpenPreparedStatements) {
		this.maxOpenPreparedStatements = maxOpenPreparedStatements;
	}
	/**
	 * 获得申请连接的时候检测连接是否有效的值
	 * @return the testWhileIdle 申请连接的时候检测连接是否有效  
	 */
	public boolean isTestWhileIdle() {
		return testWhileIdle;
	}
	/**
	 * 设置申请连接的时候检测连接是否有效的值
	 * @param testWhileIdle 申请连接的时候检测连接是否有效  
	 */
	public void setTestWhileIdle(boolean testWhileIdle) {
		this.testWhileIdle = testWhileIdle;
	}
	/**
	 * 获得申请连接时执行validationQuery检测连接是否有效的值
	 * @return the testOnBorrow 申请连接时执行validationQuery检测连接是否有效  
	 */
	public boolean isTestOnBorrow() {
		return testOnBorrow;
	}
	/**
	 * 设置申请连接时执行validationQuery检测连接是否有效的值
	 * @param testOnBorrow 申请连接时执行validationQuery检测连接是否有效  
	 */
	public void setTestOnBorrow(boolean testOnBorrow) {
		this.testOnBorrow = testOnBorrow;
	}
	/**
	 * 获得归还连接时执行validationQuery检测连接是否有效的值
	 * @return the testOnReturn 归还连接时执行validationQuery检测连接是否有效  
	 */
	public boolean isTestOnReturn() {
		return testOnReturn;
	}
	/**
	 * 设置归还连接时执行validationQuery检测连接是否有效的值
	 * @param testOnReturn 归还连接时执行validationQuery检测连接是否有效  
	 */
	public void setTestOnReturn(boolean testOnReturn) {
		this.testOnReturn = testOnReturn;
	}

}
