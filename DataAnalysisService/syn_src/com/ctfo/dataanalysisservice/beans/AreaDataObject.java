package com.ctfo.dataanalysisservice.beans;

import java.util.List;

public class AreaDataObject extends BaseDataObject {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private String lonlat;
	private List<String> lonlats;
	/**
	 * 左下经度
	 */
	private String lLon;
	/**
	 * 左下纬度
	 */
	private String lLat;
	/**
	 * 右上经度
	 */
	private String rLon;
	/**
	 * 右上纬度
	 */
	private String rLat;
	/**
	 * 围栏ID
	 */
	private Long areaID;
 
	/**
	 * 持续时间
	 */
	private String supperSpeedTime;

	// 围栏判定开始时间
	private Long beginTime;
	// 围栏判定结束时间
	private Long endTime;


 

	/**
	 * @return the lLon
	 */
	public String getlLon() {
		return lLon;
	}

	/**
	 * @param lLon
	 *            the lLon to set
	 */
	public void setlLon(String lLon) {
		this.lLon = lLon;
	}

	/**
	 * @return the lLat
	 */
	public String getlLat() {
		return lLat;
	}

	/**
	 * @param lLat
	 *            the lLat to set
	 */
	public void setlLat(String lLat) {
		this.lLat = lLat;
	}

	/**
	 * @return the rLon
	 */
	public String getrLon() {
		return rLon;
	}

	/**
	 * @param rLon
	 *            the rLon to set
	 */
	public void setrLon(String rLon) {
		this.rLon = rLon;
	}

	/**
	 * @return the rLat
	 */
	public String getrLat() {
		return rLat;
	}

	/**
	 * @param rLat
	 *            the rLat to set
	 */
	public void setrLat(String rLat) {
		this.rLat = rLat;
	}

 


	/**
	 * @return the supperSpeedTime
	 */
	public String getSupperSpeedTime() {
		return supperSpeedTime;
	}

	/**
	 * @param supperSpeedTime
	 *            the supperSpeedTime to set
	 */
	public void setSupperSpeedTime(String supperSpeedTime) {
		this.supperSpeedTime = supperSpeedTime;
	}

	public Long getBeginTime() {
		return beginTime;
	}

	public void setBeginTime(Long beginTime) {
		this.beginTime = beginTime;
	}

	public Long getEndTime() {
		return endTime;
	}

	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}

	public Long getAreaID() {
		return areaID;
	}

	public void setAreaID(Long areaID) {
		this.areaID = areaID;
	}

	public String getLonlat() {
		return lonlat;
	}

	public void setLonlat(String lonlat) {
		this.lonlat = lonlat;
	}

	public List<String> getLonlats() {
		return lonlats;
	}

	public void setLonlats(List<String> lonlats) {
		this.lonlats = lonlats;
	}


 

}
