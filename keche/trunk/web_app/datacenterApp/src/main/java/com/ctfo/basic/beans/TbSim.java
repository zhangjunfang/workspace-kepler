package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： sim卡管理<br>
 * 描述： sim卡管理<br>
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
 * <td>2014-6-16</td>
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
public class TbSim implements Serializable {

	/** */
	private static final long serialVersionUID = 1L;

	/** SIM卡ID **/
	private String sid;

	/** 所属实体ID **/
	private String entId;

	/** 真实sim卡号 **/
	private String realCommaddr;

	/** 虚拟sim卡号 **/
	private String commaddr;

	/** 服务单元密码 **/
	private String password;

	/** 电子ICCID **/
	private String iccidElectron;

	/** SIM卡运营商 **/
	private String businessId;

	/** 业务开通日期 **/
	private Long svcStart;

	/** 业务关闭日期 **/
	private Long svcStop;

	/** 备注 **/
	private String sudesc;

	/** sim卡所属地区（省） **/
	private String province;

	/** 创建人id **/
	private String createBy;

	/** 创建时间 **/
	private Long createTime;

	/** 修改人id **/
	private String updateBy;

	/** 修改时间 **/
	private Long updateTime;

	/** 有效标记 **/
	private String enableFlag;

	/** Sim卡状态 **/
	private String simState;

	/** 印刷ICCID **/
	private String iccidPrint;

	/** IMSI **/
	private String imsi;

	/** APN接入点名称 **/
	private String apn;

	/** PIN **/
	private String pin;

	/** PUK **/
	private String puk;

	/** im卡所属地区（市） **/
	private String city;

	/** 交付状态 **/
	private String deliveryStatus;

	/** 操作标示 4:车厂 **/
	private Integer orgType;

	/** 开卡日期 */
	private Long openTime;

	/** 失效日期 */
	private Long expireTime;

	/** 最近缴费日期 */
	private Long lastPayTime;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加信息
	/** 运营商名称 */
	private String businessName;

	/** 创建人姓名 */
	private String createName;

	/** 编辑人姓名 */
	private String updateName;

	/** 所属车队名称 **/
	private String entName;

	/** 所属企业名称 **/
	private String parentEntName;

	public String getSid() {
		return sid;
	}

	public void setSid(String sid) {
		this.sid = sid;
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

	public String getRealCommaddr() {
		return realCommaddr;
	}

	public void setRealCommaddr(String realCommaddr) {
		this.realCommaddr = realCommaddr;
	}

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getIccidElectron() {
		return iccidElectron;
	}

	public void setIccidElectron(String iccidElectron) {
		this.iccidElectron = iccidElectron;
	}

	public String getBusinessId() {
		return businessId;
	}

	public void setBusinessId(String businessId) {
		this.businessId = businessId;
	}

	public Long getSvcStart() {
		return svcStart;
	}

	public void setSvcStart(Long svcStart) {
		this.svcStart = svcStart;
	}

	public Long getSvcStop() {
		return svcStop;
	}

	public void setSvcStop(Long svcStop) {
		this.svcStop = svcStop;
	}

	public String getSudesc() {
		return sudesc;
	}

	public void setSudesc(String sudesc) {
		this.sudesc = sudesc;
	}

	public String getProvince() {
		return province;
	}

	public void setProvince(String province) {
		this.province = province;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getSimState() {
		return simState;
	}

	public void setSimState(String simState) {
		this.simState = simState;
	}

	public String getIccidPrint() {
		return iccidPrint;
	}

	public void setIccidPrint(String iccidPrint) {
		this.iccidPrint = iccidPrint;
	}

	public String getImsi() {
		return imsi;
	}

	public void setImsi(String imsi) {
		this.imsi = imsi;
	}

	public String getApn() {
		return apn;
	}

	public void setApn(String apn) {
		this.apn = apn;
	}

	public String getPin() {
		return pin;
	}

	public void setPin(String pin) {
		this.pin = pin;
	}

	public String getPuk() {
		return puk;
	}

	public void setPuk(String puk) {
		this.puk = puk;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getDeliveryStatus() {
		return deliveryStatus;
	}

	public void setDeliveryStatus(String deliveryStatus) {
		this.deliveryStatus = deliveryStatus;
	}

	public Integer getOrgType() {
		return orgType;
	}

	public void setOrgType(Integer orgType) {
		this.orgType = orgType;
	}

	public String getBusinessName() {
		return businessName;
	}

	public void setBusinessName(String businessName) {
		this.businessName = businessName;
	}

	public Long getOpenTime() {
		return openTime;
	}

	public void setOpenTime(Long openTime) {
		this.openTime = openTime;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public Long getExpireTime() {
		return expireTime;
	}

	public void setExpireTime(Long expireTime) {
		this.expireTime = expireTime;
	}

	public Long getLastPayTime() {
		return lastPayTime;
	}

	public void setLastPayTime(Long lastPayTime) {
		this.lastPayTime = lastPayTime;
	}

	public String getUpdateName() {
		return updateName;
	}

	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}

	public String getParentEntName() {
		return parentEntName;
	}

	public void setParentEntName(String parentEntName) {
		this.parentEntName = parentEntName;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

}