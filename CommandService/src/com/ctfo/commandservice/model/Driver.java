/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model DriverInfo.java	</li><br>
 * <li>时        间：2013-12-12  下午2:44:19	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.model;

/*****************************************
 * <li>描        述：驾驶员信息		
 * 
 *****************************************/
public class Driver {
	/** 记录编号	*/
	private String uuid;
	/** 车辆编号	*/
	private String vid;
	/** 车牌号码	*/
	private String plate;
	/** 车牌颜色	*/
	private String plateColor;
	/** 手机号	*/
	private String phoneNumber;
	/** IC卡插拔状态(107)(1: 上班，2:下班)	*/
	private int matingStatus;
	/** IC卡插拔时间()	*/
	private long matingTime;
	/** IC卡读取状态(109)	*/
	private int readStatus;
	/** 姓名(110)	*/
	private String staffName = "";
	/** 身份证号(111) 	*/
	private String idNumber = "";
	/** 从业资格证号(112)	*/
	private String qualificationId = "";
	/** 资格证发证机构名称(113) 	*/
	private String qualificationName = "";
	/** 从业资格证有效期(114)	*/
	private long qualificationValid = -1;
	/** 所属车队编号	*/
	private String teamId = "";
	/** 所属车队编号	*/
	private String teamName = "";
	/** 所属车队编号	*/
	private String entId = "";
	/** 所属车队编号	*/
	private String entName = "";
	/** 系统记录时间	*/
	private long sysUtc;
	/**
	 * 获得记录编号的值
	 * @return the uuid 记录编号  
	 */
	public String getUuid() {
		return uuid;
	}
	/**
	 * 设置记录编号的值
	 * @param uuid 记录编号  
	 */
	public void setUuid(String uuid) {
		this.uuid = uuid;
	}
	/**
	 * 获得车辆编号的值
	 * @return the vid 车辆编号  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获得车牌号码的值
	 * @return the plate 车牌号码  
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号码的值
	 * @param plate 车牌号码  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * 获得车牌颜色的值
	 * @return the plateColor 车牌颜色  
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌颜色的值
	 * @param plateColor 车牌颜色  
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	/**
	 * 获得手机号的值
	 * @return the phoneNumber 手机号  
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号的值
	 * @param phoneNumber 手机号  
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	/**
	 * 获得IC卡插拔状态(107)(1:上班，2:下班)的值
	 * @return the matingStatus IC卡插拔状态(107)(1:上班，2:下班)  
	 */
	public int getMatingStatus() {
		return matingStatus;
	}
	/**
	 * 设置IC卡插拔状态(107)(1:上班，2:下班)的值
	 * @param matingStatus IC卡插拔状态(107)(1:上班，2:下班)  
	 */
	public void setMatingStatus(int matingStatus) {
		this.matingStatus = matingStatus;
	}
	/**
	 * 获得IC卡插拔时间()的值
	 * @return the matingTime IC卡插拔时间()  
	 */
	public long getMatingTime() {
		return matingTime;
	}
	/**
	 * 设置IC卡插拔时间()的值
	 * @param matingTime IC卡插拔时间()  
	 */
	public void setMatingTime(long matingTime) {
		this.matingTime = matingTime;
	}
	/**
	 * 获得IC卡读取状态(109)的值
	 * @return the readStatus IC卡读取状态(109)  
	 */
	public int getReadStatus() {
		return readStatus;
	}
	/**
	 * 设置IC卡读取状态(109)的值
	 * @param readStatus IC卡读取状态(109)  
	 */
	public void setReadStatus(int readStatus) {
		this.readStatus = readStatus;
	}
	/**
	 * 获得姓名(110)的值
	 * @return the staffName 姓名(110)  
	 */
	public String getStaffName() {
		return staffName;
	}
	/**
	 * 设置姓名(110)的值
	 * @param staffName 姓名(110)  
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}
	/**
	 * 获得身份证号(111)的值
	 * @return the idNumber 身份证号(111)  
	 */
	public String getIdNumber() {
		return idNumber;
	}
	/**
	 * 设置身份证号(111)的值
	 * @param idNumber 身份证号(111)  
	 */
	public void setIdNumber(String idNumber) {
		this.idNumber = idNumber;
	}
	/**
	 * 获得从业资格证号(112)的值
	 * @return the qualificationId 从业资格证号(112)  
	 */
	public String getQualificationId() {
		return qualificationId;
	}
	/**
	 * 设置从业资格证号(112)的值
	 * @param qualificationId 从业资格证号(112)  
	 */
	public void setQualificationId(String qualificationId) {
		this.qualificationId = qualificationId;
	}
	/**
	 * 获得资格证发证机构名称(113)的值
	 * @return the qualificationName 资格证发证机构名称(113)  
	 */
	public String getQualificationName() {
		return qualificationName;
	}
	/**
	 * 设置资格证发证机构名称(113)的值
	 * @param qualificationName 资格证发证机构名称(113)  
	 */
	public void setQualificationName(String qualificationName) {
		this.qualificationName = qualificationName;
	}
	/**
	 * 获得从业资格证有效期(114)的值
	 * @return the qualificationValid 从业资格证有效期(114)  
	 */
	public long getQualificationValid() {
		return qualificationValid;
	}
	/**
	 * 设置从业资格证有效期(114)的值
	 * @param qualificationValid 从业资格证有效期(114)  
	 */
	public void setQualificationValid(long qualificationValid) {
		this.qualificationValid = qualificationValid;
	}
	/**
	 * 获得所属车队编号的值
	 * @return the teamId 所属车队编号  
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置所属车队编号的值
	 * @param teamId 所属车队编号  
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	/**
	 * 获得所属车队编号的值
	 * @return the teamName 所属车队编号  
	 */
	public String getTeamName() {
		return teamName;
	}
	/**
	 * 设置所属车队编号的值
	 * @param teamName 所属车队编号  
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	/**
	 * 获得所属车队编号的值
	 * @return the entId 所属车队编号  
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置所属车队编号的值
	 * @param entId 所属车队编号  
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * 获得所属车队编号的值
	 * @return the entName 所属车队编号  
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置所属车队编号的值
	 * @param entName 所属车队编号  
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * 获得系统记录时间的值
	 * @return the sysUtc 系统记录时间  
	 */
	public long getSysUtc() {
		return sysUtc;
	}
	/**
	 * 设置系统记录时间的值
	 * @param sysUtc 系统记录时间  
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	
}
