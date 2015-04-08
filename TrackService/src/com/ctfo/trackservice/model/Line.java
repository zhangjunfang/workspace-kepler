package com.ctfo.trackservice.model;

import java.util.List;

public class Line {
	/**	线路ID	*/
	private String lineid;
	/**	线路名称	*/
	private String lineName;
	/**	路段ID	*/
	private String pid;
	/**	车辆ID	*/
	private String vid;
	/**	经纬度点集合	*/
	private List<String> lonlats;
	/**	路宽	*/
	private Long roadwight;
	/**	最大速度	*/
	private Long speedthreshold;
	/**	最大速度持续时间	*/
	private Long speedtimethreshold;
	/**	线路规则开始时间	*/
	private Long beginTime;
	/**	线路规则结束时间	*/
	private Long endTime; 
	/**	是否在线路上	*/
	private boolean isOnline;
	/**	业务类型,1-限时,2-限速,3-进报警给平台,4-进报警给驾驶员,5-出报警给平台,6-出报警给驾驶员（多个以逗号分隔） 	*/
	private String[] usetype;
	/**
	 * @return the 线路ID
	 */
	public String getLineid() {
		return lineid;
	}
	/**
	 * @param 线路ID the lineid to set
	 */
	public void setLineid(String lineid) {
		this.lineid = lineid;
	}
	/**
	 * @return the 线路名称
	 */
	public String getLineName() {
		return lineName;
	}
	/**
	 * @param 线路名称 the lineName to set
	 */
	public void setLineName(String lineName) {
		this.lineName = lineName;
	}
	/**
	 * @return the 路段ID
	 */
	public String getPid() {
		return pid;
	}
	/**
	 * @param 路段ID the pid to set
	 */
	public void setPid(String pid) {
		this.pid = pid;
	}
	/**
	 * @return the 车辆ID
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * @param 车辆ID the vid to set
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return the 经纬度点集合
	 */
	public List<String> getLonlats() {
		return lonlats;
	}
	/**
	 * @param 经纬度点集合 the lonlats to set
	 */
	public void setLonlats(List<String> lonlats) {
		this.lonlats = lonlats;
	}
	/**
	 * @return the 路宽
	 */
	public Long getRoadwight() {
		return roadwight;
	}
	/**
	 * @param 路宽 the roadwight to set
	 */
	public void setRoadwight(Long roadwight) {
		this.roadwight = roadwight;
	}
	/**
	 * @return the 最大速度
	 */
	public Long getSpeedthreshold() {
		return speedthreshold;
	}
	/**
	 * @param 最大速度 the speedthreshold to set
	 */
	public void setSpeedthreshold(Long speedthreshold) {
		this.speedthreshold = speedthreshold;
	}
	/**
	 * @return the 最大速度持续时间
	 */
	public Long getSpeedtimethreshold() {
		return speedtimethreshold;
	}
	/**
	 * @param 最大速度持续时间 the speedtimethreshold to set
	 */
	public void setSpeedtimethreshold(Long speedtimethreshold) {
		this.speedtimethreshold = speedtimethreshold;
	}
	/**
	 * @return the 线路规则开始时间
	 */
	public Long getBeginTime() {
		return beginTime;
	}
	/**
	 * @param 线路规则开始时间 the beginTime to set
	 */
	public void setBeginTime(Long beginTime) {
		this.beginTime = beginTime;
	}
	/**
	 * @return the 线路规则结束时间
	 */
	public Long getEndTime() {
		return endTime;
	}
	/**
	 * @param 线路规则结束时间 the endTime to set
	 */
	public void setEndTime(Long endTime) {
		this.endTime = endTime;
	}
	/**
	 * @return the 是否在线路上
	 */
	public boolean isOnline() {
		return isOnline;
	}
	/**
	 * @param 是否在线路上 the isOnline to set
	 */
	public void setOnline(boolean isOnline) {
		this.isOnline = isOnline;
	}
	/**
	 * @return the 业务类型1-限时2-限速3-进报警给平台4-进报警给驾驶员5-出报警给平台6-出报警给驾驶员（多个以逗号分隔）
	 */
	public String[] getUsetype() {
		return usetype;
	}
	/**
	 * @param 业务类型1-限时2-限速3-进报警给平台4-进报警给驾驶员5-出报警给平台6-出报警给驾驶员（多个以逗号分隔） the usetype to set
	 */
	public void setUsetype(String[] usetype) {
		this.usetype = usetype;
	}
	
}
