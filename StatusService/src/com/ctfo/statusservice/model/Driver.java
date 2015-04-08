package com.ctfo.statusservice.model;


public class Driver {
	/**	驾驶员编号	*/
	private String driverId;
	/**	驾驶员信息来源	*/
	private int driverSource;
	/**	缓存时间	*/
	private long cacheTime;
	
	
	public Driver() {
		this.cacheTime = System.currentTimeMillis();
		this.driverId = "";
		this.driverSource = 0;
	}
	/**
	 * 获得驾驶员编号的值
	 * @return the driverId 驾驶员编号  
	 */
	public String getDriverId() {
		return driverId;
	}
	/**
	 * 设置驾驶员编号的值
	 * @param driverId 驾驶员编号  
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}
	/**
	 * 获得驾驶员信息来源的值
	 * @return the driverSource 驾驶员信息来源  
	 */
	public int getDriverSource() {
		return driverSource;
	}
	/**
	 * 设置驾驶员信息来源的值
	 * @param driverSource 驾驶员信息来源  
	 */
	public void setDriverSource(int driverSource) {
		this.driverSource = driverSource;
	}
	/**
	 * 获得缓存时间的值
	 * @return the cacheTime 缓存时间  
	 */
	public long getCacheTime() {
		return cacheTime;
	}
	/**
	 * 设置缓存时间的值
	 * @param cacheTime 缓存时间  
	 */
	public void setCacheTime(long cacheTime) {
		this.cacheTime = cacheTime;
	}
	
}
