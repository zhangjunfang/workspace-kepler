package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 注册信息管理<br>
 * 描述： 注册信息管理<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-6-12</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class TrServiceunit implements Serializable {

	/** */
	private static final long serialVersionUID = 3148176768559230877L;

	/** 服务单元id */
	private String suid;

	/** 终端id */
	private String tid;

	/** sim卡信息id */
	private String sid;

	/** 车辆编号 */
	private String vid;

	/** 创建人id */
	private String createUser;

	/** 创建时间 */
	private Long createTime;

	/** 更新人id */
	private String updateUser;

	/** 更新时间 */
	private Long updateTime;

	/** 备注 */
	private String remark;

	/** modelname */
	private String modelname;

	/** 视频终端id */
	private String dvrId;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加信息
	/** 创建人姓名 */
	private String createUsername;

	/** 修改人姓名 */
	private String updateUsername;

	/** 车牌号码 */
	private String vehicleNo;

	/** 车架号(VIN) */
	private String vinCode;

	/** 车辆内部编码 */
	private String innerCode;

	/** 车辆状态 */
	private String vehicleState;

	/** 车辆运营状态 */
	private String vehicleOperationState;

	/** 企业id */
	private String entId;

	/** 企业名称 */
	private String entName;

	/** 终端号 */
	private String tmac;

	/** 设备厂家 */
	private String oemCode;

	/** 终端型号名称 */
	private String codeName;

	/** 手机卡号 */
	private String commaddr;

	/** 视频终端号 */
	private String dvrNo;

	/** 车工号 */
	private String autoSn;

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

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateUser() {
		return updateUser;
	}

	public void setUpdateUser(String updateUser) {
		this.updateUser = updateUser;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public String getModelname() {
		return modelname;
	}

	public void setModelname(String modelname) {
		this.modelname = modelname;
	}

	public String getDvrId() {
		return dvrId;
	}

	public void setDvrId(String dvrId) {
		this.dvrId = dvrId;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
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

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public String getInnerCode() {
		return innerCode;
	}

	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}

	public String getVehicleState() {
		return vehicleState;
	}

	public void setVehicleState(String vehicleState) {
		this.vehicleState = vehicleState;
	}

	public String getVehicleOperationState() {
		return vehicleOperationState;
	}

	public void setVehicleOperationState(String vehicleOperationState) {
		this.vehicleOperationState = vehicleOperationState;
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

	public String getDvrNo() {
		return dvrNo;
	}

	public void setDvrNo(String dvrNo) {
		this.dvrNo = dvrNo;
	}

	public String getAutoSn() {
		return autoSn;
	}

	public void setAutoSn(String autoSn) {
		this.autoSn = autoSn;
	}

}
