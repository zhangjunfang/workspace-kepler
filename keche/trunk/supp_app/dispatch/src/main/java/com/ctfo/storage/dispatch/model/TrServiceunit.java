package com.ctfo.storage.dispatch.model;


import java.io.Serializable;


@SuppressWarnings("serial")
public class TrServiceunit extends BaseModel implements Serializable{

	/** 服务单元id */
	private String suid;

	/** 终端ID */
	private String tid;

	/** SIM卡信�?*/
	private String sid;

	/** 车辆编号 */
	private String vid;

	/** 创建�?*/
	private String createUser;

	/** 创建时间 */
	private String createTime;

	/** 修改�?*/
	private String updateUser;

	/** 修改时间 */
	private String updateTime;

	/** 创建人姓�?*/
	private String createUsername = "";

	/** 修改人姓�?*/
	private String updateUsername ="";

	/** 车牌号码 */
	private String vehicleNo ="++";

	/** 车牌颜色 */
	private String plateColor ="";

	/** 车架�?VIN) */
	private String vinCode ="";

	/** 企业ID */
	private String pentId;

	/** 企业名称 */
	private String pentName ="";

	/** 车队ID */
	private String entId;

	/** 车队名称 */
	private String entName ="";

	/** 终端�?*/
	private String tmac ="";

	/** 设备厂家 */
	private String oemCode ="";

	/** 终端型号名称 */
	private String codeName ="";

	/** 手机卡号 */
	private String commaddr ="";

	/** 车辆内部编码 */
	private String innerCode ="";

	/** 是否绑定线路 */
	private Integer classLineStatus =-1;

	/** 车辆运营状�? */
	private String vehicleOperationState ="";

	/** 运营状�?统计 */
	private String statisticVehicle;
	
	/** 3G视频终端id，对应表tb_dvr */
	private String dvrId ;
	
	/**  */
	private String remark = "";
	
	private String modelName ="";//模板名称  另存为模板专�?

	public String getModelName() {
		return modelName;
	}

	public void setModelName(String modelName) {
		this.modelName = modelName;
	}

	public String getSuid() {
		return suid;
	}

	public void setSuid(String suid) {
		this.suid = suid;
	}

	public String getTid() {
		return tid;
	}

	public void setTid(String tid) {
		this.tid = tid;
	}

	public String getSid() {
		return sid;
	}

	public void setSid(String sid) {
		this.sid = sid;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getCreateUser() {
		return createUser;
	}

	public void setCreateUser(String createUser) {
		this.createUser = createUser;
	}

	public String getCreateTime() {
		return createTime;
	}

	public void setCreateTime(String createTime) {
		this.createTime = createTime;
	}

	public String getUpdateUser() {
		return updateUser;
	}

	public void setUpdateUser(String updateUser) {
		this.updateUser = updateUser;
	}

	public String getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(String updateTime) {
		this.updateTime = updateTime;
	}

	public String getCreateUsername() {
		return createUsername;
	}

	public void setCreateUsername(String createUsername) {
		this.createUsername = createUsername;
	}

	public String getUpdateUsername() {
		return updateUsername;
	}

	public void setUpdateUsername(String updateUsername) {
		this.updateUsername = updateUsername;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getPlateColor() {
		return plateColor;
	}

	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public String getPentId() {
		return pentId;
	}

	public void setPentId(String pentId) {
		this.pentId = pentId;
	}

	public String getPentName() {
		return pentName;
	}

	public void setPentName(String pentName) {
		this.pentName = pentName;
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

	public String getTmac() {
		return tmac;
	}

	public void setTmac(String tmac) {
		this.tmac = tmac;
	}

	public String getOemCode() {
		return oemCode;
	}

	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	public String getCodeName() {
		return codeName;
	}

	public void setCodeName(String codeName) {
		this.codeName = codeName;
	}

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getInnerCode() {
		return innerCode;
	}

	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}

	public Integer getClassLineStatus() {
		return classLineStatus;
	}

	public void setClassLineStatus(Integer classLineStatus) {
		this.classLineStatus = classLineStatus;
	}

	public String getVehicleOperationState() {
		return vehicleOperationState;
	}

	public void setVehicleOperationState(String vehicleOperationState) {
		this.vehicleOperationState = vehicleOperationState;
	}

	public String getStatisticVehicle() {
		return statisticVehicle;
	}

	public void setStatisticVehicle(String statisticVehicle) {
		this.statisticVehicle = statisticVehicle;
	}

	/**
	 * 获取3G视频终端id，对应表tb_dvr的值
	 * @return dvrId  
	 */
	public String getDvrId() {
		return dvrId;
	}

	/**
	 * 设置3G视频终端id，对应表tb_dvr的值
	 * @param dvrId
	 */
	public void setDvrId(String dvrId) {
		this.dvrId = dvrId;
	}

	/**
	 * 获取的值
	 * @return remark  
	 */
	public String getRemark() {
		return remark;
	}

	/**
	 * 设置的值
	 * @param remark
	 */
	public void setRemark(String remark) {
		this.remark = remark;
	}
	
}