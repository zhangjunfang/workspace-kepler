package com.ctfo.dataanalysisservice.beans;

import java.io.Serializable;

public class AlarmVehicleBean implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String vid;// 车辆ID
	private String mobileNo;// 手机号码
	private String alarmType;// 报警类型

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getMobileNo() {
		return mobileNo;
	}

	public void setMobileNo(String mobileNo) {
		this.mobileNo = mobileNo;
	}

	public String getAlarmType() {
		return alarmType;
	}

	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}
}
