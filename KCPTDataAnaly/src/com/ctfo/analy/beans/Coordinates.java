package com.ctfo.analy.beans;

public class Coordinates {
	/** 经度	*/
	private long lon;
	/** 经度	*/
	private long lat;
	/** 偏移经度	*/
	private long maplon;
	/** 偏移经度	*/
	private long maplat;
	/**
	 * 获得经度的值
	 * @return the lon 经度  
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * 设置经度的值
	 * @param lon 经度  
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * 获得经度的值
	 * @return the lat 经度  
	 */
	public long getLat() {
		return lat;
	}
	/**
	 * 设置经度的值
	 * @param lat 经度  
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}
	/**
	 * 获得偏移经度的值
	 * @return the maplon 偏移经度  
	 */
	public long getMaplon() {
		return maplon;
	}
	/**
	 * 设置偏移经度的值
	 * @param maplon 偏移经度  
	 */
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}
	/**
	 * 获得偏移经度的值
	 * @return the maplat 偏移经度  
	 */
	public long getMaplat() {
		return maplat;
	}
	/**
	 * 设置偏移经度的值
	 * @param maplat 偏移经度  
	 */
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}
	
}
