package com.ctfo.analy.beans;

import java.util.List;

/**
 * 区域报警实体对象
 * @author yangyi
 *
 */
public class AreaAlarmBean {

	private List<String> lonlats;//经纬度点集合
	private String areaid;//围栏ID
	private String areaName;//围栏名称
	private String vid;//车辆ID
	private Long areamaxspeed;//最大速度
	private Long superspeedtimes;//超速持续时间
	private Long beginTime;// 围栏判定开始时间
	private Long endTime;// 围栏判定结束时间
	private String areasharp;//区域形状包括：	1-圆形区域,	2-多边形区域,	3-线路,	4-兴趣点标记, 5-矩形
	private String[] usetype;//业务类型,1-限时,2-限速,3-进报警给平台,4-进报警给驾驶员,5-出报警给平台,6-出报警给驾驶员（多个以逗号分隔）
	private String[] messageValue;//下发车机报警提示内容，格式：进区域告警内容#出区域告警内容
	private String[] vehicleDoorType;//格式为 1,2,3,4 1：区域内开门告警，2：区域外开门告警 3：区域内停车告警 4：区域外停车告警
	
	private Long arealowspeed;//最低限速
	private Long lowspeedtimes;//低速持续时间
	
	public List<String> getLonlats() {
		return lonlats;
	}
	public void setLonlats(List<String> lonlats) {
		this.lonlats = lonlats;
	}
	public String getAreaid() {
		return areaid;
	}
	public void setAreaid(String areaid) {
		this.areaid = areaid;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public Long getAreamaxspeed() {
		return areamaxspeed;
	}
	public void setAreamaxspeed(Long areamaxspeed) {
		this.areamaxspeed = areamaxspeed;
	}
	public Long getSuperspeedtimes() {
		return superspeedtimes;
	}
	public void setSuperspeedtimes(Long superspeedtimes) {
		this.superspeedtimes = superspeedtimes;
	}
	public Long getBeginTime() {
		return beginTime;
	}
	public void setBeginTime(Long beginTime) {
		this.beginTime = beginTime;
	}
	public Long getEndTime() {
		return endTime;
	}
	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}
	public String getAreasharp() {
		return areasharp;
	}
	public void setAreasharp(String areasharp) {
		this.areasharp = areasharp;
	}
	public String[] getUsetype() {
		return usetype;
	}
	public void setUsetype(String[] usetype) {
		this.usetype = usetype;
	}
	public String getAreaName() {
		return areaName;
	}
	public void setAreaName(String areaName) {
		this.areaName = areaName;
	}
	public String[] getMessageValue() {
		return messageValue;
	}
	public void setMessageValue(String[] messageValue) {
		this.messageValue = messageValue;
	}
	public String[] getVehicleDoorType() {
		return vehicleDoorType;
	}
	public void setVehicleDoorType(String[] vehicleDoorType) {
		this.vehicleDoorType = vehicleDoorType;
	}
	public Long getArealowspeed() {
		return arealowspeed;
	}
	public void setArealowspeed(Long arealowspeed) {
		this.arealowspeed = arealowspeed;
	}
	public Long getLowspeedtimes() {
		return lowspeedtimes;
	}
	public void setLowspeedtimes(Long lowspeedtimes) {
		this.lowspeedtimes = lowspeedtimes;
	}
	 
}
