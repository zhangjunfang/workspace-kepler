package com.caits.analysisserver.bean;

public class OilmassChangedDetail {
	private String changeType = null;
	
	private long utc = 0;
	
	private long lat = 0;
	
	private long lon = 0;
	
	private long mapLat = 0;
	
	private long mapLon = 0;
	
	private int elevation = 0;
	
	private int direction = 0;
	
	private int gps_speed = 0;
	
	private int curr_oillevel =0;
	
	private double curr_oilmass = 0.0;
	
	private double change_oilmass = 0;
	
	private int oil = 0;
	
	private String status; //R 行车 S 停车
	
	private String gpsTime = null;

	public String getChangeType() {
		return changeType;
	}

	public void setChangeType(String changeType) {
		this.changeType = changeType;
	}

	public long getUtc() {
		return utc;
	}

	public void setUtc(long utc) {
		this.utc = utc;
	}

	public long getLat() {
		return lat;
	}

	public void setLat(long lat) {
		this.lat = lat;
	}

	public long getLon() {
		return lon;
	}

	public void setLon(long lon) {
		this.lon = lon;
	}

	public int getElevation() {
		return elevation;
	}

	public void setElevation(int elevation) {
		this.elevation = elevation;
	}

	public int getDirection() {
		return direction;
	}

	public void setDirection(int direction) {
		this.direction = direction;
	}

	public int getGps_speed() {
		return gps_speed;
	}

	public void setGps_speed(int gpsSpeed) {
		gps_speed = gpsSpeed;
	}

	public int getCurr_oillevel() {
		return curr_oillevel;
	}

	public void setCurr_oillevel(int currOillevel) {
		curr_oillevel = currOillevel;
	}

	public double getCurr_oilmass() {
		return curr_oilmass;
	}

	public void setCurr_oilmass(double currOilmass) {
		curr_oilmass = currOilmass;
	}

	public double getChange_oilmass() {
		return change_oilmass;
	}

	public void setChange_oilmass(double changeOilmass) {
		change_oilmass = changeOilmass;
	}

	public int getOil() {
		return oil;
	}

	public void setOil(int oil) {
		this.oil = oil;
	}

	public long getMapLat() {
		return mapLat;
	}

	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}

	public long getMapLon() {
		return mapLon;
	}

	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}

	public String getGpsTime() {
		return gpsTime;
	}

	public void setGpsTime(String gpsTime) {
		this.gpsTime = gpsTime;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}
	
	
}
