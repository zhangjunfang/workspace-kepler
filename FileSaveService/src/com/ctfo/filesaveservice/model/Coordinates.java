package com.ctfo.filesaveservice.model;

public class Coordinates {
	/** 经度	*/
	private String lon;
	/** 经度	*/
	private String lat;
	/** 偏移经度	*/
	private String maplon;
	/** 偏移经度	*/
	private String maplat;
	/**
	 * 获得经度的值
	 * @return the lon 经度  
	 */
	public String getLon() {
		return lon;
	}
	/**
	 * 设置经度的值
	 * @param lon 经度  
	 */
	public void setLon(String lon) {
		this.lon = lon;
	}
	/**
	 * 获得经度的值
	 * @return the lat 经度  
	 */
	public String getLat() {
		return lat;
	}
	/**
	 * 设置经度的值
	 * @param lat 经度  
	 */
	public void setLat(String lat) {
		this.lat = lat;
	}
	/**
	 * 获得偏移经度的值
	 * @return the maplon 偏移经度  
	 */
	public String getMaplon() {
		return maplon;
	}
	/**
	 * 设置偏移经度的值
	 * @param maplon 偏移经度  
	 */
	public void setMaplon(String maplon) {
		this.maplon = maplon;
	}
	/**
	 * 获得偏移经度的值
	 * @return the maplat 偏移经度  
	 */
	public String getMaplat() {
		return maplat;
	}
	/**
	 * 设置偏移经度的值
	 * @param maplat 偏移经度  
	 */
	public void setMaplat(String maplat) {
		this.maplat = maplat;
	}
	
}
