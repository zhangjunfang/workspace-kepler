package com.caits.analysisserver.bean;

import java.util.Date;

public class PhotoSettingDetailBean {
	
	private String vid;
	private int timeInterval;
	private int distinceInterval;
	private Date beginTime;
	private Date endTime;
	private String userId;
	private String detailId;
	private String vehicleNo;
	private String oemCode;
	private String commaddr;
	private Long sendTime;
	private String switchFlag;
	private String terType;//终端类型 （1:车载终端 2:3G视频终端）
	private String dvrNo;//视频终端号
	private String isNew;// 新增记录标记，新增记录为空，非新增记录有值。
	private String tprotocolType;//终端协议类型 808 808B
	private String pid;//通道号
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public int getTimeInterval() {
		return timeInterval;
	}
	public void setTimeInterval(int timeInterval) {
		this.timeInterval = timeInterval;
	}
	public int getDistinceInterval() {
		return distinceInterval;
	}
	public void setDistinceInterval(int distinceInterval) {
		this.distinceInterval = distinceInterval;
	}
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
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	public String getDetailId() {
		return detailId;
	}
	public void setDetailId(String detailId) {
		this.detailId = detailId;
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
	public Long getSendTime() {
		return sendTime;
	}
	public void setSendTime(Long sendTime) {
		this.sendTime = sendTime;
	}
	public String getSwitchFlag() {
		return switchFlag;
	}
	public void setSwitchFlag(String switchFlag) {
		this.switchFlag = switchFlag;
	}
	public String getTerType() {
		return terType;
	}
	public void setTerType(String terType) {
		this.terType = terType;
	}
	public String getDvrNo() {
		return dvrNo;
	}
	public void setDvrNo(String dvrNo) {
		this.dvrNo = dvrNo;
	}
	public String getIsNew() {
		return isNew;
	}
	public void setIsNew(String isNew) {
		this.isNew = isNew;
	}
	public String getTprotocolType() {
		return tprotocolType;
	}
	public void setTprotocolType(String tprotocolType) {
		this.tprotocolType = tprotocolType;
	}
	public String getPid() {
		return pid;
	}
	public void setPid(String pid) {
		this.pid = pid;
	}

}
