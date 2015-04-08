package com.ctfo.savecenter.beans;


public class ServiceFileUnit {

	private String vid;
	private String filecontent=""; //轨迹文件
	private String day;
	private String alarmfilecontent=""; //报警文件
	private String gpsTime = "";
	private int recordCount = 0; // 文件数据量

	public void addRecordCount(int num){
		recordCount = recordCount + num;
	}
	
	public void resetUnit(){
		this.recordCount = 0;
		this.filecontent = "";
		this.day = "";
		this.alarmfilecontent = "";
		this.gpsTime = "";
	}
	
	public void resetRecordCount(){
		this.recordCount = 0;
		this.filecontent = "";
		this.alarmfilecontent = "";
	}
	
	public int getRecordCount(){
		return this.recordCount;
	}
	
	public String getGpsTime() {
		return gpsTime;
	}
	public void setGpsTime(String gpsTime) {
		this.gpsTime = gpsTime;
	}

	public String getAlarmfilecontent() {
		return alarmfilecontent;
	}
	public void setAlarmfilecontent(String alarmfilecontent) {
		this.alarmfilecontent = alarmfilecontent;
	}

	public String getFilecontent() {
		return filecontent;
	}
	public void setFilecontent(String filecontent) {
		this.filecontent = filecontent;
	}
	
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public void setDay(String day) {
		this.day = day;
	}
	public String getDay() {
		return day;
	}
}
