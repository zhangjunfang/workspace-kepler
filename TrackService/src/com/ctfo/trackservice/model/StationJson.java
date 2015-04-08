package com.ctfo.trackservice.model;


/**
 * 站点信息缓存对象
 *
 */
public class StationJson {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车速	*/
	private int speed;
	/**	线路编号	*/
	private String lineId;
	/**	上一站站点编号	*/
	private String stationId; 
	/**	已过站点（上上站）编号	*/
	private String afterStationId;
	/**	与上一站距离（米）	*/
	private int distance;
	/**	与上一站距离百分比（0~99）	*/
	private int percentage;
	/** 上一站中心点经度 */
	private long mapLon;
	/** 上一站中心点纬度 */
	private long mapLat;
	/** 上上站中心点经度 */
	private long afterMapLon;
	/** 上上站中心点纬度 */
	private long afterMapLat;
	/** 判断线路		 */
	private boolean analyzingLine;
	/**
	 * @return the 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * @param 车辆编号 the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return the 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * @param 车牌号 the plate to set
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return the 车速
	 */
	public int getSpeed() {
		return speed;
	}
	/**
	 * @param 车速 the speed to set
	 */
	public void setSpeed(int speed) {
		this.speed = speed;
	}
	/**
	 * @return the 线路编号
	 */
	public String getLineId() {
		return lineId;
	}
	/**
	 * @param 线路编号 the lineId to set
	 */
	public void setLineId(String lineId) {
		this.lineId = lineId;
	}
	/**
	 * @return the 上一站站点编号
	 */
	public String getStationId() {
		return stationId;
	}
	/**
	 * @param 上一站站点编号 the stationId to set
	 */
	public void setStationId(String stationId) {
		this.stationId = stationId;
	}
	/**
	 * @return the 已过站点（上上站）编号
	 */
	public String getAfterStationId() {
		return afterStationId;
	}
	/**
	 * @param 已过站点（上上站）编号 the afterStationId to set
	 */
	public void setAfterStationId(String afterStationId) {
		this.afterStationId = afterStationId;
	}
	/**
	 * @return the 与上一站距离（米）
	 */
	public int getDistance() {
		return distance;
	}
	/**
	 * @param 与上一站距离（米） the distance to set
	 */
	public void setDistance(int distance) {
		this.distance = distance;
	}
	/**
	 * @return the 与上一站距离百分比（0~99）
	 */
	public int getPercentage() {
		return percentage;
	}
	/**
	 * @param 与上一站距离百分比（0~99） the percentage to set
	 */
	public void setPercentage(int percentage) {
		this.percentage = percentage;
	}
	/**
	 * @return the 上一站中心点经度
	 */
	public long getMapLon() {
		return mapLon;
	}
	/**
	 * @param 上一站中心点经度 the mapLon to set
	 */
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}
	/**
	 * @return the 上一站中心点纬度
	 */
	public long getMapLat() {
		return mapLat;
	}
	/**
	 * @param 上一站中心点纬度 the mapLat to set
	 */
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}
	/**
	 * @return the 上上站中心点经度
	 */
	public long getAfterMapLon() {
		return afterMapLon;
	}
	/**
	 * @param 上上站中心点经度 the afterMapLon to set
	 */
	public void setAfterMapLon(long afterMapLon) {
		this.afterMapLon = afterMapLon;
	}
	/**
	 * @return the 上上站中心点纬度
	 */
	public long getAfterMapLat() {
		return afterMapLat;
	}
	/**
	 * @param 上上站中心点纬度 the afterMapLat to set
	 */
	public void setAfterMapLat(long afterMapLat) {
		this.afterMapLat = afterMapLat;
	}
	/**
	 * @return the 判断线路
	 */
	public boolean isAnalyzingLine() {
		return analyzingLine;
	}
	/**
	 * @param 判断线路 the analyzingLine to set
	 */
	public void setAnalyzingLine(boolean analyzingLine) {
		this.analyzingLine = analyzingLine;
	}
	
}
