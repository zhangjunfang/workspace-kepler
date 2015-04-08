package com.ctfo.filesaveservice.model;

public class DriftLatLon {
	private int accCount = 0;
	
	private long lon = 0;
	
	private long lat = 0;
	
	private long mapLon = 0;
	
	private long mapLat = 0;

	public void resetAll(){
		this.accCount = 0;
		this.lon = 0;
		this.lat = 0;
		this.mapLon = 0;
		this.mapLat = 0;
	}
	
	public int getAccCount() {
		return accCount;
	}

	public void addAccCount(int accCount) {
		this.accCount = accCount + this.accCount;
	}

	public long getLon() {
		return lon;
	}

	public void setLon(long lon) {
		this.lon = lon;
	}

	public long getLat() {
		return lat;
	}

	public void setLat(long lat) {
		this.lat = lat;
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
}
