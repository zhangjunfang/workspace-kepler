/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.util RedisProperties.java	</li><br>
 * <li>时        间：2013-10-22  下午5:08:16	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.model;

/*****************************************
 * <li>描        述：redis配置		
 * 
 *****************************************/
public class RedisProperties {
	/**	服务器地址	*/
	private String 	host;
	/**	端口	*/
    private int port;
    /**	密码	*/
    private String 	pwd;
    /**	最大活动连接	*/
    private int maxActive;
    /**	最大空闲连接	*/
    private int maxIdle;
    /**	最大等待时间	*/
    private long 	maxWait;
    /**	超时时间	*/
    private int redisTimeout;
	/**
	 * 获得服务器地址的值
	 * @return the host 服务器地址  
	 */
	public String getHost() {
		return host;
	}
	/**
	 * 设置服务器地址的值
	 * @param host 服务器地址  
	 */
	public void setHost(String host) {
		this.host = host;
	}
	/**
	 * 获得端口的值
	 * @return the port 端口  
	 */
	public int getPort() {
		return port;
	}
	/**
	 * 设置端口的值
	 * @param port 端口  
	 */
	public void setPort(int port) {
		this.port = port;
	}
	/**
	 * 获得密码的值
	 * @return the pwd 密码  
	 */
	public String getPwd() {
		return pwd;
	}
	/**
	 * 设置密码的值
	 * @param pwd 密码  
	 */
	public void setPwd(String pwd) {
		this.pwd = pwd;
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
	 * 获得最大空闲连接的值
	 * @return the maxIdle 最大空闲连接  
	 */
	public int getMaxIdle() {
		return maxIdle;
	}
	/**
	 * 设置最大空闲连接的值
	 * @param maxIdle 最大空闲连接  
	 */
	public void setMaxIdle(int maxIdle) {
		this.maxIdle = maxIdle;
	}
	/**
	 * 获得最大等待时间的值
	 * @return the maxWait 最大等待时间  
	 */
	public long getMaxWait() {
		return maxWait;
	}
	/**
	 * 设置最大等待时间的值
	 * @param maxWait 最大等待时间  
	 */
	public void setMaxWait(long maxWait) {
		this.maxWait = maxWait;
	}
	/**
	 * 获得超时时间的值
	 * @return the redisTimeout 超时时间  
	 */
	public int getRedisTimeout() {
		return redisTimeout;
	}
	/**
	 * 设置超时时间的值
	 * @param redisTimeout 超时时间  
	 */
	public void setRedisTimeout(int redisTimeout) {
		this.redisTimeout = redisTimeout;
	}
    
}
