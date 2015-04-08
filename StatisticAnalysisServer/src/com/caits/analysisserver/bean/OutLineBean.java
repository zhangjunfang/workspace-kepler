package com.caits.analysisserver.bean;

public class OutLineBean {
	private long outline_begin_time =0;
	
	private long outline_end_time = 0;
	
	private long mapLat = 0;
	
	private long mapLon = 0;
	
	private String address = "";

	public long getOutline_begin_time() {
		return outline_begin_time;
	}

	public void setOutline_begin_time(long outlineBeginTime) {
		outline_begin_time = outlineBeginTime;
	}

	public long getOutline_end_time() {
		return outline_end_time;
	}

	public void setOutline_end_time(long outlineEndTime) {
		outline_end_time = outlineEndTime;
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

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}
	
}
