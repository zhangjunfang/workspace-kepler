package com.caits.analysisserver.bean;

import java.util.Date;

import com.ctfo.sendcommand.manager.commproxy.SendCommandBean;

public class FailureSendCommandBean {
	
	private Date beginTime;//生效开始时间
	
	private Date endTime;//生效截止时间   当前日期在开始时间和结束时间的指令会重新发送
	
	private String userId;
	
	private String id;
	
	private String vid;
	
	private String switchFlag; //0、禁用 1、启用
	
	private String vehicleNo;
	
	private String oemCode;
	
	private String commaddr;
	
	private SendCommandBean scb;

	public Date getBeginTime() {
		return beginTime;
	}

	public void setBeginTime(Date beginTime) {
		this.beginTime = beginTime;
	}

	public Date getEndTime() {
		return endTime;
	}

	public void setEndTime(Date endTime) {
		this.endTime = endTime;
	}

	public SendCommandBean getScb() {
		return scb;
	}

	public void setScb(SendCommandBean scb) {
		this.scb = scb;
	}

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getSwitchFlag() {
		return switchFlag;
	}

	public void setSwitchFlag(String switchFlag) {
		this.switchFlag = switchFlag;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getOemCode() {
		return oemCode;
	}

	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}
	
	

}
