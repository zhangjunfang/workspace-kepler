package com.ctfo.storage.model.basedata;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 用户注册信息<br>
 * 描述： 用户注册信息<br>
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
 * <td>2014-11-5</td>
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
public class TbSigningInfo implements Serializable {

	/** */
	private static final long serialVersionUID = -4822489755222489373L;

	/** id */
	private String signId;

	/** 编码 */
	private String signCode;

	/** 签约品牌，关联字典表 */
	private String signBrand;

	/** 签约服务站编码 */
	private String comCode;

	/** 签约类型 */
	private String signType;

	/** 服务站简称 */
	private String comShortName;

	/** 服务站全称 */
	private String comName;

	/** 申请时间 */
	private Long applyTime;

	/** 建站时间 */
	private Long approvedTime;

	/** 协议到期时间 */
	private Long protocolExpiresTime;

	/** 系统名称 */
	private String systemName;

	/** 协议类型，关联字典表 */
	private String protocolType;

	/** 服务器IP */
	private String serverIp;

	/** 服务器端口 */
	private String serverPort;

	/** 密钥 */
	private String secretKey;

	/** 法人 */
	private String legalPerson;

	/** 热线电话 */
	private String hotline;

	/** 省 */
	private String province;

	/** 市 */
	private String city;

	/** 区 */
	private String county;

	/** 邮编 */
	private String zipCode;

	/** 联系地址 */
	private String contactAddress;

	/** 联系人 */
	private String contact;

	/** 联系电话 */
	private String contactTel;

	/** 联系手机 */
	private String contactPhone;

	/** 维修资质 */
	private String repairQualification;

	/** 单位性质 */
	private String natureUnit;

	/** 电子邮件 */
	private String email;

	/** 传真 */
	private String fax;

	/** 机器序列码 */
	private String machineCodeSequence;

	/** socket用户名 */
	private String sUser;

	/** socket密码 */
	private String sPwd;

	/** 鉴权码 */
	private String authentication;

	/** 授权码 */
	private String grantAuthorization;

	/** 数据来源 */
	private String dataSources;

	/** 鉴权状态 0未授权 1授权 */
	private String authenticationStatus;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套ID */
	private String setBookId;

	public String getSignId() {
		return signId;
	}

	public void setSignId(String signId) {
		this.signId = signId;
	}

	public String getSignCode() {
		return signCode;
	}

	public void setSignCode(String signCode) {
		this.signCode = signCode;
	}

	public String getSignBrand() {
		return signBrand;
	}

	public void setSignBrand(String signBrand) {
		this.signBrand = signBrand;
	}

	public String getComCode() {
		return comCode;
	}

	public void setComCode(String comCode) {
		this.comCode = comCode;
	}

	public String getSignType() {
		return signType;
	}

	public void setSignType(String signType) {
		this.signType = signType;
	}

	public String getComShortName() {
		return comShortName;
	}

	public void setComShortName(String comShortName) {
		this.comShortName = comShortName;
	}

	public String getComName() {
		return comName;
	}

	public void setComName(String comName) {
		this.comName = comName;
	}

	public Long getApplyTime() {
		return applyTime;
	}

	public void setApplyTime(Long applyTime) {
		this.applyTime = applyTime;
	}

	public Long getApprovedTime() {
		return approvedTime;
	}

	public void setApprovedTime(Long approvedTime) {
		this.approvedTime = approvedTime;
	}

	public Long getProtocolExpiresTime() {
		return protocolExpiresTime;
	}

	public void setProtocolExpiresTime(Long protocolExpiresTime) {
		this.protocolExpiresTime = protocolExpiresTime;
	}

	public String getSystemName() {
		return systemName;
	}

	public void setSystemName(String systemName) {
		this.systemName = systemName;
	}

	public String getProtocolType() {
		return protocolType;
	}

	public void setProtocolType(String protocolType) {
		this.protocolType = protocolType;
	}

	public String getServerIp() {
		return serverIp;
	}

	public void setServerIp(String serverIp) {
		this.serverIp = serverIp;
	}

	public String getServerPort() {
		return serverPort;
	}

	public void setServerPort(String serverPort) {
		this.serverPort = serverPort;
	}

	public String getSecretKey() {
		return secretKey;
	}

	public void setSecretKey(String secretKey) {
		this.secretKey = secretKey;
	}

	public String getLegalPerson() {
		return legalPerson;
	}

	public void setLegalPerson(String legalPerson) {
		this.legalPerson = legalPerson;
	}

	public String getHotline() {
		return hotline;
	}

	public void setHotline(String hotline) {
		this.hotline = hotline;
	}

	public String getProvince() {
		return province;
	}

	public void setProvince(String province) {
		this.province = province;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getCounty() {
		return county;
	}

	public void setCounty(String county) {
		this.county = county;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getContactAddress() {
		return contactAddress;
	}

	public void setContactAddress(String contactAddress) {
		this.contactAddress = contactAddress;
	}

	public String getContact() {
		return contact;
	}

	public void setContact(String contact) {
		this.contact = contact;
	}

	public String getContactTel() {
		return contactTel;
	}

	public void setContactTel(String contactTel) {
		this.contactTel = contactTel;
	}

	public String getContactPhone() {
		return contactPhone;
	}

	public void setContactPhone(String contactPhone) {
		this.contactPhone = contactPhone;
	}

	public String getRepairQualification() {
		return repairQualification;
	}

	public void setRepairQualification(String repairQualification) {
		this.repairQualification = repairQualification;
	}

	public String getNatureUnit() {
		return natureUnit;
	}

	public void setNatureUnit(String natureUnit) {
		this.natureUnit = natureUnit;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getFax() {
		return fax;
	}

	public void setFax(String fax) {
		this.fax = fax;
	}

	public String getMachineCodeSequence() {
		return machineCodeSequence;
	}

	public void setMachineCodeSequence(String machineCodeSequence) {
		this.machineCodeSequence = machineCodeSequence;
	}

	public String getAuthentication() {
		return authentication;
	}

	public void setAuthentication(String authentication) {
		this.authentication = authentication;
	}

	public String getGrantAuthorization() {
		return grantAuthorization;
	}

	public void setGrantAuthorization(String grantAuthorization) {
		this.grantAuthorization = grantAuthorization;
	}

	public String getDataSources() {
		return dataSources;
	}

	public void setDataSources(String dataSources) {
		this.dataSources = dataSources;
	}

	public String getAuthenticationStatus() {
		return authenticationStatus;
	}

	public void setAuthenticationStatus(String authenticationStatus) {
		this.authenticationStatus = authenticationStatus;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
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

	public String getSerStationId() {
		return serStationId;
	}

	public void setSerStationId(String serStationId) {
		this.serStationId = serStationId;
	}

	public String getSUser() {
		return sUser;
	}

	public void setSUser(String sUser) {
		this.sUser = sUser;
	}

	public String getSPwd() {
		return sPwd;
	}

	public void setSPwd(String sPwd) {
		this.sPwd = sPwd;
	}

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}

}