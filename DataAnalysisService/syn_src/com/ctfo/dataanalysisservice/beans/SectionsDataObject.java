package com.ctfo.dataanalysisservice.beans;

public class SectionsDataObject extends BaseDataObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	/**
	 * 线段ID
	 */
	private String sectionsId;
	/**
	 * 线段起始点
	 */
	private long startPoint;
	/**
	 * 线段结束点
	 */
	private long endPoint;
	/**
	 * 线段的开始点纬度
	 */
	private long startLat;
	/**
	 * 开始点经度
	 */
	private long startLon;
	/**
	 * 结束点纬度
	 */
	private long endLat;
	/**
	 * 结束点经度
	 */
	private long endLon;

	/**
	 * 路宽
	 */
	private String wight;
	/**
	 * 线路ID
	 */
	private String lineId;
	
	/**
	 * 限速
	 */
	private int limitSpeed=100;
	
	
	/**
	 * 超速阀值
	 */
	private int overSpeedTimer=60;
	
	
	/**
	 * 线段的有效开始时间  2012-02-20
	 */
	private long validStartTime;
	
	
	/**
	 * 线段的有效结束时间  2012-02-20
	 */
	private long validEndTime;
	
	
	
	
	
	public long getValidStartTime() {
		return validStartTime;
	}

	public void setValidStartTime(long validStartTime) {
		this.validStartTime = validStartTime;
	}

	public long getValidEndTime() {
		return validEndTime;
	}

	public void setValidEndTime(long validEndTime) {
		this.validEndTime = validEndTime;
	}

	public int getLimitSpeed() {
		return limitSpeed;
	}

	public void setLimitSpeed(int limitSpeed) {
		this.limitSpeed = limitSpeed;
	}

	public int getOverSpeedTimer() {
		return overSpeedTimer;
	}

	public void setOverSpeedTimer(int overSpeedTimer) {
		this.overSpeedTimer = overSpeedTimer;
	}

	/**
	 * @return the sectionsId
	 */
	public String getSectionsId() {
		return sectionsId;
	}

	/**
	 * @param sectionsId
	 *            the sectionsId to set
	 */
	public void setSectionsId(String sectionsId) {
		this.sectionsId = sectionsId;
	}

	/**
	 * @return the startPoint
	 */
	public long getStartPoint() {
		return startPoint;
	}

	/**
	 * @param startPoint
	 *            the startPoint to set
	 */
	public void setStartPoint(long startPoint) {
		this.startPoint = startPoint;
	}

	/**
	 * @return the endPoint
	 */
	public long getEndPoint() {
		return endPoint;
	}

	/**
	 * @param endPoint
	 *            the endPoint to set
	 */
	public void setEndPoint(long endPoint) {
		this.endPoint = endPoint;
	}

	/**
	 * @return the wight
	 */
	public String getWight() {
		return wight;
	}

	/**
	 * @param wight
	 *            the wight to set
	 */
	public void setWight(String wight) {
		this.wight = wight;
	}

	/**
	 * @return the startLat
	 */
	public long getStartLat() {
		return startLat;
	}

	/**
	 * @param startLat the startLat to set
	 */
	public void setStartLat(long startLat) {
		this.startLat = startLat;
	}

	/**
	 * @return the startLon
	 */
	public long getStartLon() {
		return startLon;
	}

	/**
	 * @param startLon the startLon to set
	 */
	public void setStartLon(long startLon) {
		this.startLon = startLon;
	}

	/**
	 * @return the endLat
	 */
	public long getEndLat() {
		return endLat;
	}

	/**
	 * @param endLat the endLat to set
	 */
	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}

	/**
	 * @return the endLon
	 */
	public long getEndLon() {
		return endLon;
	}

	/**
	 * @param endLon the endLon to set
	 */
	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}

	/**
	 * @return the lineId
	 */
	public String getLineId() {
		return lineId;
	}

	/**
	 * @param lineId the lineId to set
	 */
	public void setLineId(String lineId) {
		this.lineId = lineId;
	}

}
