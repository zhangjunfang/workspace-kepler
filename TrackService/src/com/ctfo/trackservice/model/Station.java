package com.ctfo.trackservice.model;

public class Station {
	/** 站点ID */
	private String stationId;
	/** 站点半径 (米) */
	private long stationRadius;
	/** 中心点经度 */
	private long mapLon;
	/** 中心点纬度 */
	private long mapLat;
	/** 线路ID */
	private String lineId;
	/** 站点序号	*/
	private int stationNo;
	/** 站点方向线路方向（1:起点发车-上行；2:终点发车-下行）	*/
	private int lineDirection;
	/**
	 * @return the 站点ID
	 */
	public String getStationId() {
		return stationId;
	}
	/**
	 * @param 站点ID the stationId to set
	 */
	public void setStationId(String stationId) {
		this.stationId = stationId;
	}
	/**
	 * @return the 站点半径(米)
	 */
	public long getStationRadius() {
		return stationRadius;
	}
	/**
	 * @param 站点半径(米) the stationRadius to set
	 */
	public void setStationRadius(long stationRadius) {
		this.stationRadius = stationRadius;
	}
	/**
	 * @return the 中心点经度
	 */
	public long getMapLon() {
		return mapLon;
	}
	/**
	 * @param 中心点经度 the mapLon to set
	 */
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}
	/**
	 * @return the 中心点纬度
	 */
	public long getMapLat() {
		return mapLat;
	}
	/**
	 * @param 中心点纬度 the mapLat to set
	 */
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}
	/**
	 * @return the 站点序号
	 */
	public int getStationNo() {
		return stationNo;
	}
	/**
	 * @param 站点序号 the stationNo to set
	 */
	public void setStationNo(int stationNo) {
		this.stationNo = stationNo;
	}
	/**
	 * @return the 站点方向线路方向（1:起点发车-上行；2:终点发车-下行）
	 */
	public int getLineDirection() {
		return lineDirection;
	}
	/**
	 * @param 站点方向线路方向（1:起点发车-上行；2:终点发车-下行） the lineDirection to set
	 */
	public void setLineDirection(int lineDirection) {
		this.lineDirection = lineDirection;
	}
	public String getLineId() {
		return lineId;
	}
	public void setLineId(String lineId) {
		this.lineId = lineId;
	}
	public void reSet(){
		this.mapLat = 0;
		this.mapLon = 0;
		this.stationId = null;
	}
}
