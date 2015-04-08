package com.ctfo.analy.beans;

/**
 * 站点对象
 * @author yujch
 *
 */
public class TbLineStationBean {
	
	/**
	 * 站点ID
	 */
	private String stationId;
	
	/**
	 * 站点编码
	 */
	private String stationCode;
	
	/**
	 * 站点名称
	 */
	private String stationName;
	
	/**
	 * 站点半径 米
	 */
	private long stationRadius;
	
	/**
	 * 中心点经度
	 */
	private long mapLon;
	
	/**
	 * 中心点纬度
	 */
	private long mapLat;
	
	/**
	 * 所属线路ID
	 */
	private String lineId;
	
	/**
	 * 站点顺序
	 */
	private long stationNumber;
	
	/**
	 * 线路包含站点个数
	 */
	private long stationNum;

	public String getStationId() {
		return stationId;
	}

	public void setStationId(String stationId) {
		this.stationId = stationId;
	}

	public String getStationCode() {
		return stationCode;
	}

	public void setStationCode(String stationCode) {
		this.stationCode = stationCode;
	}

	public String getStationName() {
		return stationName;
	}

	public void setStationName(String stationName) {
		this.stationName = stationName;
	}

	public long getStationRadius() {
		return stationRadius;
	}

	public void setStationRadius(long stationRadius) {
		this.stationRadius = stationRadius;
	}

	public long getMapLon() {
		return mapLon;
	}

	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}

	public long getMapLat() {
		return mapLat;
	}

	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}

	public long getStationNumber() {
		return stationNumber;
	}

	public void setStationNumber(long stationNumber) {
		this.stationNumber = stationNumber;
	}

	public String getLineId() {
		return lineId;
	}

	public void setLineId(String lineId) {
		this.lineId = lineId;
	}

	public long getStationNum() {
		return stationNum;
	}

	public void setStationNum(long stationNum) {
		this.stationNum = stationNum;
	}

}
