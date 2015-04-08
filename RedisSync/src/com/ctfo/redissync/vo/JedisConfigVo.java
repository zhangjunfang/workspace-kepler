package com.ctfo.redissync.vo;

import java.util.Properties;

/**
 * Redis配置Vo对象，每项参数见System.properties
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class JedisConfigVo extends Object {
	private int maxActive;
	private int maxIdel;
	private long maxWait;
	private boolean testOnBorrow;
	
	public JedisConfigVo() {
		
	}
	
	public JedisConfigVo(int maxActive, int maxIdel, long maxWait, boolean testOnBorrow) {
		this.maxActive = maxActive;
		this.maxIdel = maxIdel;
		this.maxWait = maxWait;
		this.testOnBorrow = testOnBorrow;
	}
	
	 public int getMaxActive() {
		return maxActive;
	}
	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}
	public int getMaxIdel() {
		return maxIdel;
	}
	public void setMaxIdel(int maxIdel) {
		this.maxIdel = maxIdel;
	}
	public long getMaxWait() {
		return maxWait;
	}
	public void setMaxWait(long maxWait) {
		this.maxWait = maxWait;
	}
	public boolean isTestOnBorrow() {
		return testOnBorrow;
	}
	public void setTestOnBorrow(boolean testOnBorrow) {
		this.testOnBorrow = testOnBorrow;
	}
	@Override
	public String toString() {
		return "JedisConfigVo [maxActive=" + maxActive + ", maxIdel=" + maxIdel + ", maxWait=" + maxWait
				+ ", testOnBorrow=" + testOnBorrow + "]";
	}

	public JedisConfigVo makeSelf(final Properties p) {
		this.maxActive = Integer.valueOf(p.getProperty("JD_maxActive", "50").trim());
		this.maxIdel = Integer.valueOf(p.getProperty("JD_maxIdel", "10").trim());
		this.maxWait = Long.valueOf(p.getProperty("JD_maxWait", "1000").trim());
		this.testOnBorrow = Boolean.valueOf(p.getProperty("JD_testOnBorrow", "true").trim());
		
		return this;
	}
}
