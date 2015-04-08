package com.ctfo.savecenter.beans;

import java.io.Serializable;

public class AlarmTypeBean implements Serializable   {
 
	private static final long serialVersionUID = 5375480682925747644L;
	private Integer alarmcode;//报警类型定义编号
	private String alarmname;//报警名称
	private Integer alarmtype;//0单次报警 1 持续报警+
	private String parentCode;//父类报警编码
	
	public AlarmTypeBean(String alarmname,Integer alarmtype,String parentCode){
		this.alarmname=alarmname;
		this.alarmtype=alarmtype;
		this.parentCode=parentCode;
	}

	public Integer getAlarmcode() {
		return alarmcode;
	}

	public void setAlarmcode(Integer alarmcode) {
		this.alarmcode = alarmcode;
	}

	public String getAlarmname() {
		return alarmname;
	}

	public void setAlarmname(String alarmname) {
		this.alarmname = alarmname;
	}

	public Integer getAlarmtype() {
		return alarmtype;
	}

	public void setAlarmtype(Integer alarmtype) {
		this.alarmtype = alarmtype;
	}

	public String getParentCode() {
		return parentCode;
	}

	public void setParentCode(String parentCode) {
		this.parentCode = parentCode;
	}
}
