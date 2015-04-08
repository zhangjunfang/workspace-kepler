package com.ctfo.filesaveservice.model;


public class ServiceFileUnit {

	private String vid;
	private String filecontent=""; //轨迹文件
	private String day;
	private String alarmfilecontent=""; //报警文件
	private String gpsTime = "";
	private int recordCount = 0; // 文件数据量

	/**	目录（格式:/path/file/track/2014/01/01/1.txt）	*/
	private String path;
	
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

	/**
	 * 获得目录（格式:pathfiletrack201401011.txt）的值
	 * @return the path 目录（格式:pathfiletrack201401011.txt）  
	 */
	public String getPath() {
		return path;
	}
	/**
	 * 设置目录（格式:pathfiletrack201401011.txt）的值
	 * @param path 目录（格式:pathfiletrack201401011.txt）  
	 */
	public void setPath(String path) {
		this.path = path;
	}
	
}
