/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * @author zjhl
 *
 */
public class VehicleTemp {
	/**	车辆编号	*/
	private long vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌号	颜色	*/
	private int plateColor;
	/**	车架号	*/
	private String vinCode;
	/**	车辆内部编码	*/
	private long innerCode;
	/**	终端号	*/
	private long tid;
	/**	终端型号	*/
	private int terminalType;
	/**	手机号	*/
	private long phoneNumber;
	/**	厂商编号	*/
	private String oemCode;
	/**	组织编号	*/
	private long entId;
	/** 企业名称*/
	private String entName ;
	/** 车队ID*/
	private long teamId ;
	/** 车队名称*/
	private String teamName ;
	/** 驾驶员ID*/
	private long staffId ;
	/** 驾驶员名称*/
	private String staffName ;
	/**	在线状态	*/
	private int online;
	/**
	 * @return 获取 车辆编号
	 */
	public long getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(long vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return 获取 车牌号颜色
	 */
	public int getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌号颜色
	 * @param plateColor 车牌号颜色 
	 */
	public void setPlateColor(int plateColor) {
		this.plateColor = plateColor;
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
	public long getInnerCode() {
		return innerCode;
	}
	/**
	 * 设置车辆内部编码
	 * @param innerCode 车辆内部编码 
	 */
	public void setInnerCode(long innerCode) {
		this.innerCode = innerCode;
	}
	/**
	 * @return 获取 终端号
	 */
	public long getTid() {
		return tid;
	}
	/**
	 * 设置终端号
	 * @param tid 终端号 
	 */
	public void setTid(long tid) {
		this.tid = tid;
	}
	/**
	 * @return 获取 终端型号
	 */
	public int getTerminalType() {
		return terminalType;
	}
	/**
	 * 设置终端型号
	 * @param terminalType 终端型号 
	 */
	public void setTerminalType(int terminalType) {
		this.terminalType = terminalType;
	}
	/**
	 * @return 获取 手机号
	 */
	public long getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号
	 * @param phoneNumber 手机号 
	 */
	public void setPhoneNumber(long phoneNumber) {
		this.phoneNumber = phoneNumber;
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
	/**
	 * @return 获取 组织编号
	 */
	public long getEntId() {
		return entId;
	}
	/**
	 * 设置组织编号
	 * @param entId 组织编号 
	 */
	public void setEntId(long entId) {
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
	public long getTeamId() {
		return teamId;
	}
	/**
	 * 设置车队ID
	 * @param teamId 车队ID 
	 */
	public void setTeamId(long teamId) {
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
	public long getStaffId() {
		return staffId;
	}
	/**
	 * 设置驾驶员ID
	 * @param staffId 驾驶员ID 
	 */
	public void setStaffId(long staffId) {
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
}
