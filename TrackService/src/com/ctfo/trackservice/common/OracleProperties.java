/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.util OracleProperties.java	</li><br>
 * <li>时        间：2013-10-22  下午4:24:32	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.common;

/*****************************************
 * <li>描        述：oracle参数		
 * 
 *****************************************/
public class OracleProperties {
	/** 连接地址 */
	private String url;
	/** 用户名 */
	private String username;
	/**	密码 */
	private String password;
	/**	初始化大小 */
	private int initialSize;
	/** 最大 */
	private int maxActive;
	/** 最小 */
	private int minIdle;
	/** 最大等待 */
	private long maxWait;
	/** 配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒 */
	private long timeBetweenEvictionRunsMillis;
	/** 配置一个连接在池中最小生存的时间，单位是毫秒 */
	private long minEvictableIdleTimeMillis;
	/** 测试空闲连接 */
	private boolean testWhileIdle;
	/** 测试获取返回 */
	private boolean testOnBorrow;
	/** 测试返回连接 */
	private boolean testOnReturn;
	/** 打开PSCache，并且指定每个连接上PSCache的大小 */
	private int maxPoolPreparedStatementPerConnectionSize;
	/**
	 * 获得连接地址的值
	 * @return the url 连接地址  
	 */
	public String getUrl() {
		return url;
	}
	/**
	 * 设置连接地址的值
	 * @param url 连接地址  
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
	 * 获得初始化大小的值
	 * @return the initialSize 初始化大小  
	 */
	public int getInitialSize() {
		return initialSize;
	}
	/**
	 * 设置初始化大小的值
	 * @param initialSize 初始化大小  
	 */
	public void setInitialSize(int initialSize) {
		this.initialSize = initialSize;
	}
	/**
	 * 获得最大的值
	 * @return the maxActive 最大  
	 */
	public int getMaxActive() {
		return maxActive;
	}
	/**
	 * 设置最大的值
	 * @param maxActive 最大  
	 */
	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}
	/**
	 * 获得最小的值
	 * @return the minIdle 最小  
	 */
	public int getMinIdle() {
		return minIdle;
	}
	/**
	 * 设置最小的值
	 * @param minIdle 最小  
	 */
	public void setMinIdle(int minIdle) {
		this.minIdle = minIdle;
	}
	/**
	 * 获得最大等待的值
	 * @return the maxWait 最大等待  
	 */
	public long getMaxWait() {
		return maxWait;
	}
	/**
	 * 设置最大等待的值
	 * @param maxWait 最大等待  
	 */
	public void setMaxWait(long maxWait) {
		this.maxWait = maxWait;
	}
	/**
	 * 获得配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒的值
	 * @return the timeBetweenEvictionRunsMillis 配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒  
	 */
	public long getTimeBetweenEvictionRunsMillis() {
		return timeBetweenEvictionRunsMillis;
	}
	/**
	 * 设置配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒的值
	 * @param timeBetweenEvictionRunsMillis 配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒  
	 */
	public void setTimeBetweenEvictionRunsMillis(long timeBetweenEvictionRunsMillis) {
		this.timeBetweenEvictionRunsMillis = timeBetweenEvictionRunsMillis;
	}
	/**
	 * 获得配置一个连接在池中最小生存的时间，单位是毫秒的值
	 * @return the minEvictableIdleTimeMillis 配置一个连接在池中最小生存的时间，单位是毫秒  
	 */
	public long getMinEvictableIdleTimeMillis() {
		return minEvictableIdleTimeMillis;
	}
	/**
	 * 设置配置一个连接在池中最小生存的时间，单位是毫秒的值
	 * @param minEvictableIdleTimeMillis 配置一个连接在池中最小生存的时间，单位是毫秒  
	 */
	public void setMinEvictableIdleTimeMillis(long minEvictableIdleTimeMillis) {
		this.minEvictableIdleTimeMillis = minEvictableIdleTimeMillis;
	}
	/**
	 * 获得测试空闲连接的值
	 * @return the testWhileIdle 测试空闲连接  
	 */
	public boolean getTestWhileIdle() {
		return testWhileIdle;
	}
	/**
	 * 设置测试空闲连接的值
	 * @param testWhileIdle 测试空闲连接  
	 */
	public void setTestWhileIdle(boolean testWhileIdle) {
		this.testWhileIdle = testWhileIdle;
	}
	/**
	 * 获得测试获取返回的值
	 * @return the testOnBorrow 测试获取返回  
	 */
	public boolean getTestOnBorrow() {
		return testOnBorrow;
	}
	/**
	 * 设置测试获取返回的值
	 * @param testOnBorrow 测试获取返回  
	 */
	public void setTestOnBorrow(boolean testOnBorrow) {
		this.testOnBorrow = testOnBorrow;
	}
	/**
	 * 获得测试返回连接的值
	 * @return the testOnReturn 测试返回连接  
	 */
	public boolean getTestOnReturn() {
		return testOnReturn;
	}
	/**
	 * 设置测试返回连接的值
	 * @param testOnReturn 测试返回连接  
	 */
	public void setTestOnReturn(boolean testOnReturn) {
		this.testOnReturn = testOnReturn;
	}
	/**
	 * 获得打开PSCache，并且指定每个连接上PSCache的大小的值
	 * @return the maxPoolPreparedStatementPerConnectionSize 打开PSCache，并且指定每个连接上PSCache的大小  
	 */
	public int getMaxPoolPreparedStatementPerConnectionSize() {
		return maxPoolPreparedStatementPerConnectionSize;
	}
	/**
	 * 设置打开PSCache，并且指定每个连接上PSCache的大小的值
	 * @param maxPoolPreparedStatementPerConnectionSize 打开PSCache，并且指定每个连接上PSCache的大小  
	 */
	public void setMaxPoolPreparedStatementPerConnectionSize(int maxPoolPreparedStatementPerConnectionSize) {
		this.maxPoolPreparedStatementPerConnectionSize = maxPoolPreparedStatementPerConnectionSize;
	}

}
