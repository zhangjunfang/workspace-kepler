package com.ctfo.mgdb.beans;

/**
 * 
 * 插入mongo的一条记录
 * @author huangjincheng
 *
 */
public class Record {
	private String ip = "";
	private int appType= 0;
	private long utcTime = 0l;
    private String phoneNum = "";
    private int dataType = 0;
    private int enable_flag = 0;//0正常，1错误
    private String content = "";
	public String getIp() {
		return ip;
	}
	public void setIp(String ip) {
		this.ip = ip;
	}
	public int getAppType() {
		return appType;
	}
	public void setAppType(int appType) {
		this.appType = appType;
	}
	public long getUtcTime() {
		return utcTime;
	}
	public void setUtcTime(long utcTime) {
		this.utcTime = utcTime;
	}
	public String getPhoneNum() {
		return phoneNum;
	}
	public void setPhoneNum(String phoneNum) {
		this.phoneNum = phoneNum;
	}
	public int getDataType() {
		return dataType;
	}
	public void setDataType(int dataType) {
		this.dataType = dataType;
	}
	public int getEnable_flag() {
		return enable_flag;
	}
	public void setEnable_flag(int enable_flag) {
		this.enable_flag = enable_flag;
	}
	public String getContent() {
		return content;
	}
	public void setContent(String content) {
		this.content = content;
	}
    
    
    
    

}
