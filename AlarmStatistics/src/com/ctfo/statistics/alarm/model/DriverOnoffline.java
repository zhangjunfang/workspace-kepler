package com.ctfo.statistics.alarm.model;

public class DriverOnoffline implements Comparable<DriverOnoffline>{
	/**	驾驶员编号	*/
	private String driverId;
	/**	上线时间	*/
	private long onlineUtc;
	/**	离线时间	*/
	private long offlineUtc;
	/**	上线时间（秒）	*/
	private int onlineSecond;
	
	public DriverOnoffline(){
	}
	public DriverOnoffline(String id, long online, long offline) {
		this.driverId = id;
		this.onlineUtc = online;
		if(online > 100000000000l ){
			this.onlineSecond = Integer.parseInt(String.valueOf(online).substring(0, 10));
		}
		this.offlineUtc = offline;
	}
	/**
	 * 获取[驾驶员编号]值
	 */
	public String getDriverId() {
		return driverId;
	}
	/**
	 * 设置[驾驶员编号] 值
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}
	/**
	 * 获取[上线时间]值
	 */
	public long getOnlineUtc() {
		return onlineUtc;
	}
	/**
	 * 设置[上线时间] 值
	 */
	public void setOnlineUtc(long onlineUtc) {
		this.onlineUtc = onlineUtc;
	}
	/**
	 * 获取[离线时间]值
	 */
	public long getOfflineUtc() {
		return offlineUtc;
	}
	/**
	 * 设置[离线时间] 值
	 */
	public void setOfflineUtc(long offlineUtc) {
		this.offlineUtc = offlineUtc;
	}
	/**
	 * 获取[上线时间（秒）]值
	 */
	public int getOnlineSecond() {
		return onlineSecond;
	}
	/**
	 * 设置[上线时间（秒）] 值
	 */
	public void setOnlineSecond(int onlineSecond) {
		this.onlineSecond = onlineSecond;
	}
	
	@Override
	public int compareTo(DriverOnoffline driverOnoffline) {
		return  driverOnoffline.getOnlineSecond() - this.onlineSecond;
	}
}
