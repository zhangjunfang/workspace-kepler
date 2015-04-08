package com.ctfo.analy.beans;

import java.util.List;

/**
 * 线路实体对象
 * @author yangyi
 *
 */
public class LineAlarmBean {
 
	private List<String> lonlats;//经纬度点集合
	private String lineid;//线路ID
	private String lineName;//线路名称
	private String pid;//路段ID
	private String vid;//车辆ID
	private Long roadwight;//路宽
	private Long speedthreshold;//最大速度
	private Long speedtimethreshold;//最大速度持续时间
	private Long beginTime;// 围栏判定开始时间
	private Long endTime;// 围栏判定结束时间
	private boolean isOnline;//是否在线路上
	private String[] usetype;//业务类型,1-限时,2-限速,3-进报警给平台,4-进报警给驾驶员,5-出报警给平台,6-出报警给驾驶员（多个以逗号分隔） 
	
	public List<String> getLonlats() {
		return lonlats;
	}
	public void setLonlats(List<String> lonlats) {
		this.lonlats = lonlats;
	}
	public String getLineid() {
		return lineid;
	}
	public void setLineid(String lineid) {
		this.lineid = lineid;
	}
	public String getPid() {
		return pid;
	}
	public void setPid(String pid) {
		this.pid = pid;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public Long getRoadwight() {
		return roadwight;
	}
	public void setRoadwight(Long roadwight) {
		this.roadwight = roadwight;
	}
	public Long getSpeedthreshold() {
		return speedthreshold;
	}
	public void setSpeedthreshold(Long speedthreshold) {
		this.speedthreshold = speedthreshold;
	}
	public Long getSpeedtimethreshold() {
		return speedtimethreshold;
	}
	public void setSpeedtimethreshold(Long speedtimethreshold) {
		this.speedtimethreshold = speedtimethreshold;
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
	public String[] getUsetype() {
		return usetype;
	}
	public void setUsetype(String[] usetype) {
		this.usetype = usetype;
	}
	public String getLineName() {
		return lineName;
	}
	public void setLineName(String lineName) {
		this.lineName = lineName;
	}
	public boolean isOnline() {
		return isOnline;
	}
	public void setOnline(boolean isOnline) {
		this.isOnline = isOnline;
	}
 
}
