/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.util RedisProperties.java	</li><br>
 * <li>时        间：2013-10-22  下午5:08:16	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.util;

/*****************************************
 * <li>描        述：redis配置		
 * 
 *****************************************/
public class RedisProperties {
	private String 	host;
    private Integer port;
    private String 	pwd;
    private Integer maxActive;
    private Integer maxIdle;
    private Long 	maxWait;
    private Integer redisTimeout;
    
	public String getHost() {
		return host;
	}
	public void setHost(String host) {
		this.host = host;
	}
	public Integer getPort() {
		return port;
	}
	public void setPort(Integer port) {
		this.port = port;
	}
	public String getPwd() {
		return pwd;
	}
	public void setPwd(String pwd) {
		this.pwd = pwd;
	}
	public Integer getMaxActive() {
		return maxActive;
	}
	public void setMaxActive(Integer maxActive) {
		this.maxActive = maxActive;
	}
	public Integer getMaxIdle() {
		return maxIdle;
	}
	public void setMaxIdle(Integer maxIdle) {
		this.maxIdle = maxIdle;
	}
	public Long getMaxWait() {
		return maxWait;
	}
	public void setMaxWait(Long maxWait) {
		this.maxWait = maxWait;
	}
	public Integer getRedisTimeout() {
		return redisTimeout;
	}
	public void setRedisTimeout(Integer redisTimeout) {
		this.redisTimeout = redisTimeout;
	}
	
}
