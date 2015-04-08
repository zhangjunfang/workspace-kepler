package com.ctfo.statistics.alarm.model;

public class VehicleInfo {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车队编号	*/
	private String teamId;
	/**	车队名称	*/
	private String teamName;
	/**	组织名称	*/
	private String entId;
	/**	车牌号	*/
	private String entName;
	/**	超速阀值	*/
	private int speedThreshold;
	
	/**
	 * 获取[车辆编号]值
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置[车辆编号] 值
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获取[车牌号]值
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置[车牌号] 值
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * 获取[车队编号]值
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置[车队编号] 值
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	/**
	 * 获取[车队名称]值
	 */
	public String getTeamName() {
		return teamName;
	}
	/**
	 * 设置[车队名称] 值
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	/**
	 * 获取[组织名称]值
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置[组织名称] 值
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * 获取[车牌号]值
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置[车牌号] 值
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * 获取[超速阀值]值
	 */
	public int getSpeedThreshold() {
		return speedThreshold;
	}
	/**
	 * 设置[超速阀值] 值
	 */
	public void setSpeedThreshold(int speedThreshold) {
		this.speedThreshold = speedThreshold;
	}
	
}
