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
public class DriverInfo {
	/** 记录编号	*/
	private String autoId;
	/** 车辆编号	*/
	private String vid;
	/** 手机号	*/
	private String phoneNumber;
	/** 姓名	*/
	private String staffName;
	/** 身份证号 	*/
	private String idNumber;
	/** 从业资格证号	*/
	private String qualificationId;
	/** 资格证发证机构名称 	*/
	private String qualificationName;
	/** 上线时间	*/
	private long onlineTime;
	/** 下线时间	*/
	private long offlineTime;
	/** 从业资格证IC卡读取状态	*/
	private int icStatus;
	/** 上下班状态	*/
	private int onoffStatus;
	/** 所属车队编号	*/
	private String teamId;
	/** 所属车队编号	*/
	private String teamName;
	/** 所属车队编号	*/
	private String entId;
	/** 所属车队编号	*/
	private String entName;
	/** 从业资格证有效期	*/
	private long qualificationValid;
	/** 系统记录时间	*/
	private long sysUtc;
	/** 车牌号码	*/
	private String plate;
	/** 车牌颜色	*/
	private String plateColor;
	
	public String getAutoId() {
		return autoId;
	}
	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getPhoneNumber() {
		return phoneNumber;
	}
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	public String getStaffName() {
		return staffName;
	}
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}
	public String getIdNumber() {
		return idNumber;
	}
	public void setIdNumber(String idNumber) {
		this.idNumber = idNumber;
	}
	public String getQualificationId() {
		return qualificationId;
	}
	public void setQualificationId(String qualificationId) {
		this.qualificationId = qualificationId;
	}
	public String getQualificationName() {
		return qualificationName;
	}
	public void setQualificationName(String qualificationName) {
		this.qualificationName = qualificationName;
	}
	public long getOnlineTime() {
		return onlineTime;
	}
	public void setOnlineTime(long onlineTime) {
		this.onlineTime = onlineTime;
	}
	public long getOfflineTime() {
		return offlineTime;
	}
	public void setOfflineTime(long offlineTime) {
		this.offlineTime = offlineTime;
	}
	public int getIcStatus() {
		return icStatus;
	}
	public void setIcStatus(int icStatus) {
		this.icStatus = icStatus;
	}
	public int getOnoffStatus() {
		return onoffStatus;
	}
	public void setOnoffStatus(int onoffStatus) {
		this.onoffStatus = onoffStatus;
	}
	public String getTeamId() {
		return teamId;
	}
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	public String getTeamName() {
		return teamName;
	}
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	public String getEntId() {
		return entId;
	}
	public void setEntId(String entId) {
		this.entId = entId;
	}
	public String getEntName() {
		return entName;
	}
	public void setEntName(String entName) {
		this.entName = entName;
	}
	public long getQualificationValid() {
		return qualificationValid;
	}
	public void setQualificationValid(long qualificationValid) {
		this.qualificationValid = qualificationValid;
	}
	public long getSysUtc() {
		return sysUtc;
	}
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	public String getPlate() {
		return plate;
	}
	public void setPlate(String plate) {
		this.plate = plate;
	}
	public String getPlateColor() {
		return plateColor;
	}
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	
}
