package com.ctfo.operation.beans;

import java.io.Serializable;

@SuppressWarnings("serial")
public class CompanyInfo implements Serializable{
	private String comId;//公司档案ID
	private String csComId;//cs端公司id
	private String comName;//公司全称
	private String comShortName;//公司简称
	private String comAddress;//公司地址
	private String province;//省份
	private String city;//城市
	private String county;//区县
	private String zipCode;//邮政编码
	private String comContact;//联系人
	private String comEmail;//电子邮件
	private String comTel;//联系电话
	private String hotLtel;//热线电话
	private String comFax;//传真
	private String repairQualification;//维修资质
	private String unitProperties;//单位性质
	private String legalPerson;//法人
	private String certificateCode;//组织结构代码
	private String registerAuthentication;//注册鉴权情况
	private String registerMethod;//注册鉴权方式
	private String comType;//公司类型
	private String machineSerial;//机器序列号
	private String macAddress;//mac地址
	private String serviceVersion;//服务端版本
	private String serviceStatus;//服务端在线状态
	private long registTime;//注册时间
	private String registIp;//注册IP
	private String clientNums;//客户端在线数
	private String ytCrmLinkedStatus;//宇通CRM系统链路
	private String setbookId;//帐套编码
	private String status;//状态
	private String authorizationCode;//授权码
	private long validDate;//有效期
	private String approver;//审批人
	private long approvalTime;//审批时间
	private String approvalAdvice;//审批意见
	private String remark;//备注
	private String enableFlag;//删除标记，0为删除，1未删除  默认1
	private String openBank;//开户银行，关联字典码表
	private String bankAccount;//银行账号
	private String valueaddAppId;//增值应用，关联增值业务表
	private String createBy;//创建人，关联人员表
	private long createTime;//创建时间
	private String updateBy;//最后编辑人，关联人员表
	private long updateTime;//最后编辑时间
	
	private String comAccount;//用户名(账号)
	private String comPassWord;//密码
	private String authCode;//鉴权码
	
	private String total;//客户端在线数
	
	private String addApp;
	
	private String serviceStationSap;//宇通sap代码
    private String accessCode;//宇通接入码
	
    
	public String getServiceStationSap() {
		return serviceStationSap;
	}
	public void setServiceStationSap(String serviceStationSap) {
		this.serviceStationSap = serviceStationSap;
	}
	public String getAccessCode() {
		return accessCode;
	}
	public void setAccessCode(String accessCode) {
		this.accessCode = accessCode;
	}
	public String getAddApp() {
		return addApp;
	}
	public void setAddApp(String addApp) {
		this.addApp = addApp;
	}
	public String getTotal() {
		return total;
	}
	public void setTotal(String total) {
		this.total = total;
	}
	public String getComAccount() {
		return comAccount;
	}
	public void setComAccount(String comAccount) {
		this.comAccount = comAccount;
	}
	public String getComPassWord() {
		return comPassWord;
	}
	public void setComPassWord(String comPassWord) {
		this.comPassWord = comPassWord;
	}
	public String getAuthCode() {
		return authCode;
	}
	public void setAuthCode(String authCode) {
		this.authCode = authCode;
	}
	public String getComId() {
		return comId;
	}
	public void setComId(String comId) {
		this.comId = comId;
	}
	
	public String getCsComId() {
		return csComId;
	}
	public void setCsComId(String csComId) {
		this.csComId = csComId;
	}
	public String getComName() {
		return comName;
	}
	public void setComName(String comName) {
		this.comName = comName;
	}
	public String getComShortName() {
		return comShortName;
	}
	public void setComShortName(String comShortName) {
		this.comShortName = comShortName;
	}
	public String getComAddress() {
		return comAddress;
	}
	public void setComAddress(String comAddress) {
		this.comAddress = comAddress;
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
	public String getComContact() {
		return comContact;
	}
	public void setComContact(String comContact) {
		this.comContact = comContact;
	}
	public String getComEmail() {
		return comEmail;
	}
	public void setComEmail(String comEmail) {
		this.comEmail = comEmail;
	}
	public String getComTel() {
		return comTel;
	}
	public void setComTel(String comTel) {
		this.comTel = comTel;
	}
	public String getHotLtel() {
		return hotLtel;
	}
	public void setHotLtel(String hotLtel) {
		this.hotLtel = hotLtel;
	}
	public String getComFax() {
		return comFax;
	}
	public void setComFax(String comFax) {
		this.comFax = comFax;
	}
	public String getRepairQualification() {
		return repairQualification;
	}
	public void setRepairQualification(String repairQualification) {
		this.repairQualification = repairQualification;
	}
	public String getUnitProperties() {
		return unitProperties;
	}
	public void setUnitProperties(String unitProperties) {
		this.unitProperties = unitProperties;
	}
	public String getLegalPerson() {
		return legalPerson;
	}
	public void setLegalPerson(String legalPerson) {
		this.legalPerson = legalPerson;
	}
	public String getCertificateCode() {
		return certificateCode;
	}
	public void setCertificateCode(String certificateCode) {
		this.certificateCode = certificateCode;
	}
	public String getRegisterAuthentication() {
		return registerAuthentication;
	}
	public void setRegisterAuthentication(String registerAuthentication) {
		this.registerAuthentication = registerAuthentication;
	}
	public String getRegisterMethod() {
		return registerMethod;
	}
	public void setRegisterMethod(String registerMethod) {
		this.registerMethod = registerMethod;
	}
	public String getComType() {
		return comType;
	}
	public void setComType(String comType) {
		this.comType = comType;
	}
	public String getMachineSerial() {
		return machineSerial;
	}
	public void setMachineSerial(String machineSerial) {
		this.machineSerial = machineSerial;
	}
	public String getMacAddress() {
		return macAddress;
	}
	public void setMacAddress(String macAddress) {
		this.macAddress = macAddress;
	}
	public String getServiceVersion() {
		return serviceVersion;
	}
	public void setServiceVersion(String serviceVersion) {
		this.serviceVersion = serviceVersion;
	}
	public String getServiceStatus() {
		return serviceStatus;
	}
	public void setServiceStatus(String serviceStatus) {
		this.serviceStatus = serviceStatus;
	}
	public long getRegistTime() {
		return registTime;
	}
	public void setRegistTime(long registTime) {
		this.registTime = registTime;
	}
	public String getRegistIp() {
		return registIp;
	}
	public void setRegistIp(String registIp) {
		this.registIp = registIp;
	}
	public String getClientNums() {
		return clientNums;
	}
	public void setClientNums(String clientNums) {
		this.clientNums = clientNums;
	}
	public String getYtCrmLinkedStatus() {
		return ytCrmLinkedStatus;
	}
	public void setYtCrmLinkedStatus(String ytCrmLinkedStatus) {
		this.ytCrmLinkedStatus = ytCrmLinkedStatus;
	}
	public String getSetbookId() {
		return setbookId;
	}
	public void setSetbookId(String setbookId) {
		this.setbookId = setbookId;
	}
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	public String getAuthorizationCode() {
		return authorizationCode;
	}
	public void setAuthorizationCode(String authorizationCode) {
		this.authorizationCode = authorizationCode;
	}
	public long getValidDate() {
		return validDate;
	}
	public void setValidDate(long validDate) {
		this.validDate = validDate;
	}
	public String getApprover() {
		return approver;
	}
	public void setApprover(String approver) {
		this.approver = approver;
	}

	public String getApprovalAdvice() {
		return approvalAdvice;
	}
	public void setApprovalAdvice(String approvalAdvice) {
		this.approvalAdvice = approvalAdvice;
	}
	public String getRemark() {
		return remark;
	}
	public void setRemark(String remark) {
		this.remark = remark;
	}
	public String getEnableFlag() {
		return enableFlag;
	}
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	public String getOpenBank() {
		return openBank;
	}
	public void setOpenBank(String openBank) {
		this.openBank = openBank;
	}
	public String getBankAccount() {
		return bankAccount;
	}
	public void setBankAccount(String bankAccount) {
		this.bankAccount = bankAccount;
	}
	public String getValueaddAppId() {
		return valueaddAppId;
	}
	public void setValueaddAppId(String valueaddAppId) {
		this.valueaddAppId = valueaddAppId;
	}
	public String getCreateBy() {
		return createBy;
	}
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public String getUpdateBy() {
		return updateBy;
	}
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	public long getApprovalTime() {
		return approvalTime;
	}
	public void setApprovalTime(long approvalTime) {
		this.approvalTime = approvalTime;
	}
	public long getCreateTime() {
		return createTime;
	}
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	public long getUpdateTime() {
		return updateTime;
	}
	public void setUpdateTime(long updateTime) {
		this.updateTime = updateTime;
	}

}
