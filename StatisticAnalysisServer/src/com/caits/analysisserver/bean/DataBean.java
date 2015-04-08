package com.caits.analysisserver.bean;

public class DataBean {
	private Long gpsTime = 0l;//对应GPS UTC时间
	private String data = null; // 每行数据
	public Long getGpsTime() {
		return gpsTime;
	}
	public void setGpsTime(Long gpsTime) {
		this.gpsTime = gpsTime;
	}
	public String getData() {
		return data;
	}
	public void setData(String data) {
		this.data = data;
	}
}
