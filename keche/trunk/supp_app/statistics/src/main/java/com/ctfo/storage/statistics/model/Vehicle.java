/**
 * 
 */
package com.ctfo.storage.statistics.model;

/**
 * @author zjhl
 *
 */
public class Vehicle {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌号	颜色	*/
	private String plateColor;
	/**	车架号	*/
	private String vinCode;
	/**	车辆内部编码	*/
	private String innerCode;
	/**	终端号	*/
	private String tid;
	/**	终端型号	*/
	private String terminalType;
	/**	手机号	*/
	private String phoneNumber;
	/**	厂商编号	*/
	private String oemCode;
	/**	组织编号	*/
	private String entId;
	/** 企业名称*/
	private String entName ;
	/** 车队ID*/
	private String teamId ;
	/** 车队名称*/
	private String teamName ;
	/** 驾驶员ID*/
	private String staffId ;
	/** 驾驶员名称*/
	private String staffName ;
	/**	在线状态	*/
	private int online;
	//	/**	车辆类型	*/
//	private String vehicleType;
	/**
	 * @return 获取 手机号
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号
	 * @param phoneNumber 手机号 
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * @return 获取 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
//	/**
//	 * @return 获取 车辆类型
//	 */
//	public String getVehicleType() {
//		return vehicleType;
//	}
//	/**
//	 * 设置车辆类型
//	 * @param vehicleType 车辆类型 
//	 */
//	public void setVehicleType(String vehicleType) {
//		this.vehicleType = vehicleType;
//	}
	/**
	 * @return 获取 组织编号
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置组织编号
	 * @param entId 组织编号 
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * @return 获取 企业名称
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置企业名称
	 * @param entName 企业名称 
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * @return 获取 车队ID
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置车队ID
	 * @param teamId 车队ID 
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	/**
	 * @return 获取 车队名称
	 */
	public String getTeamName() {
		return teamName;
	}
	/**
	 * 设置车队名称
	 * @param teamName 车队名称 
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	/**
	 * @return 获取 驾驶员ID
	 */
	public String getStaffId() {
		return staffId;
	}
	/**
	 * 设置驾驶员ID
	 * @param staffId 驾驶员ID 
	 */
	public void setStaffId(String staffId) {
		this.staffId = staffId;
	}
	/**
	 * @return 获取 驾驶员名称
	 */
	public String getStaffName() {
		return staffName;
	}
	/**
	 * 设置驾驶员名称
	 * @param staffName 驾驶员名称 
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}
	/**
	 * @return 获取 车架号
	 */
	public String getVinCode() {
		return vinCode;
	}
	/**
	 * 设置车架号
	 * @param vinCode 车架号 
	 */
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	/**
	 * @return 获取 车辆内部编码
	 */
	public String getInnerCode() {
		return innerCode;
	}
	/**
	 * 设置车辆内部编码
	 * @param innerCode 车辆内部编码 
	 */
	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}
	/**
	 * @return 获取 车牌号颜色
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌号颜色
	 * @param plateColor 车牌号颜色 
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	/**
	 * @return 获取 终端号
	 */
	public String getTid() {
		return tid;
	}
	/**
	 * 设置终端号
	 * @param tid 终端号 
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}
	/**
	 * @return 获取 终端型号
	 */
	public String getTerminalType() {
		return terminalType;
	}
	/**
	 * 设置终端型号
	 * @param terminalType 终端型号 
	 */
	public void setTerminalType(String terminalType) {
		this.terminalType = terminalType;
	}
	/**
	 * @return 获取 在线状态
	 */
	public int getOnline() {
		return online;
	}
	/**
	 * 设置在线状态
	 * @param online 在线状态 
	 */
	public void setOnline(int online) {
		this.online = online;
	}
	/**
	 * @return 获取 厂商编号
	 */
	public String getOemCode() {
		return oemCode;
	}
	/**
	 * 设置厂商编号
	 * @param oemCode 厂商编号 
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
	
}
