package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 客户档案<br>
 * 描述： 客户档案<br>
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
public class TbCustomer implements Serializable {

	/** */
	private static final long serialVersionUID = -2804759508090449642L;

	/** 客户ID */
	private String custId;

	/** 客户编码 */
	private String custCode;

	/** 客户名称 */
	private String custName;

	/** 客户简称 */
	private String custShortName;

	/** 简称首拼速查码 */
	private String custQuickCode;

	/** 客户类别，关联字典表关联 */
	private String custType;

	/** 法人 */
	private String legalPerson;

	/** 所属省，关联字典码表 */
	private String province;

	/** 所属市，关联字典码表 */
	private String city;

	/** 所属区县，关联字典码表 */
	private String county;

	/** 联系地址 */
	private String custAddress;

	/** 邮编 */
	private String zipCode;

	/** 固话 */
	private String custTel;

	/** 手机 */
	private String custPhone;

	/** 传真 */
	private String custFax;

	/** 电子邮箱 */
	private String custEmail;

	/** 网址 */
	private String custWebsite;

	/** 企业性质，关联码表字典 */
	private String enterpriseNature;

	/** 纳税人识别号 */
	private String taxNum;

	/** 信用等级，关联字码表典 */
	private String creditRating;

	/** 信用额度 */
	private Integer creditLine;

	/** 信用账期 */
	private Integer creditAccountPeriod;

	/** 价格类型，关联字典码表 */
	private String priceType;

	/** 开户银行，关联字典码表 */
	private String openBank;

	/** 银行账号 */
	private String bankAccount;

	/** 银行账号开户人 */
	private String bankAccountPerson;

	/** 开票名称 */
	private String billingName;

	/** 开票地址 */
	private String billingAddress;

	/** 开票账号 */
	private String billingAccount;

	/** 备注 */
	private String custRemark;

	/** 是否会员 */
	private String isMember;

	/** 会员编号 */
	private String memberNumber;

	/** 会员等级，关联字典码表 */
	private String memberClass;

	/** 会员有效期 */
	private Long memberPeriodValidity;

	/** 配件享受折扣 */
	private BigDecimal accessoriesDiscount;

	/** 工时费享受折扣 */
	private BigDecimal workhoursDiscount;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 删除标记，0删除，1未删除 默认1 */
	private String enableFlag;

	/** 宇通SAP代码 */
	private String ytSapCode;

	/** 宇通客户经理 */
	private String ytCustomerManager;

	/** 数据来源，关联字典码表 */
	private String dataSource;

	/** 国家---宇通 */
	private String country;

	/** 客户关系综述---宇通 */
	private String custRelation;

	/** 独立法人---宇通 */
	private String indepenLegalperson;

	/** 细分市场---宇通 */
	private String marketSegment;

	/** 组织机构代码（工商）---宇通 */
	private String institutionCode;

	/** 公司体制---宇通 */
	private String comConstitution;

	/** 注册资金(万)---宇通 */
	private String registeredCapital;

	/** 车辆结构---宇通 */
	private String vehicleStructure;

	/** 经销商---宇通 */
	private String agency;

	/** 客户sap代码---宇通 */
	private String sapCode;

	/** 业务经营范围---宇通 */
	private String businessScope;

	/** 企业资质---宇通 */
	private String entQualification;

	/** CRM客户ID */
	private String custCrmGuid;

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

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 车主电话---宇通 */
	// private String contPhone;

	public String getSerStationId() {
		return serStationId;
	}

	public void setSerStationId(String serStationId) {
		this.serStationId = serStationId;
	}

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}

	public String getCustId() {
		return custId;
	}

	public void setCustId(String custId) {
		this.custId = custId;
	}

	public String getCustCode() {
		return custCode;
	}

	public void setCustCode(String custCode) {
		this.custCode = custCode;
	}

	public String getCustName() {
		return custName;
	}

	public void setCustName(String custName) {
		this.custName = custName;
	}

	public String getCustShortName() {
		return custShortName;
	}

	public void setCustShortName(String custShortName) {
		this.custShortName = custShortName;
	}

	public String getCustQuickCode() {
		return custQuickCode;
	}

	public void setCustQuickCode(String custQuickCode) {
		this.custQuickCode = custQuickCode;
	}

	public String getCustType() {
		return custType;
	}

	public void setCustType(String custType) {
		this.custType = custType;
	}

	public String getLegalPerson() {
		return legalPerson;
	}

	public void setLegalPerson(String legalPerson) {
		this.legalPerson = legalPerson;
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

	public String getCustAddress() {
		return custAddress;
	}

	public void setCustAddress(String custAddress) {
		this.custAddress = custAddress;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getCustTel() {
		return custTel;
	}

	public void setCustTel(String custTel) {
		this.custTel = custTel;
	}

	public String getCustPhone() {
		return custPhone;
	}

	public void setCustPhone(String custPhone) {
		this.custPhone = custPhone;
	}

	public String getCustFax() {
		return custFax;
	}

	public void setCustFax(String custFax) {
		this.custFax = custFax;
	}

	public String getCustEmail() {
		return custEmail;
	}

	public void setCustEmail(String custEmail) {
		this.custEmail = custEmail;
	}

	public String getCustWebsite() {
		return custWebsite;
	}

	public void setCustWebsite(String custWebsite) {
		this.custWebsite = custWebsite;
	}

	public String getEnterpriseNature() {
		return enterpriseNature;
	}

	public void setEnterpriseNature(String enterpriseNature) {
		this.enterpriseNature = enterpriseNature;
	}

	public String getTaxNum() {
		return taxNum;
	}

	public void setTaxNum(String taxNum) {
		this.taxNum = taxNum;
	}

	public String getCreditRating() {
		return creditRating;
	}

	public void setCreditRating(String creditRating) {
		this.creditRating = creditRating;
	}

	public Integer getCreditLine() {
		return creditLine;
	}

	public void setCreditLine(Integer creditLine) {
		this.creditLine = creditLine;
	}

	public Integer getCreditAccountPeriod() {
		return creditAccountPeriod;
	}

	public void setCreditAccountPeriod(Integer creditAccountPeriod) {
		this.creditAccountPeriod = creditAccountPeriod;
	}

	public String getPriceType() {
		return priceType;
	}

	public void setPriceType(String priceType) {
		this.priceType = priceType;
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

	public String getBankAccountPerson() {
		return bankAccountPerson;
	}

	public void setBankAccountPerson(String bankAccountPerson) {
		this.bankAccountPerson = bankAccountPerson;
	}

	public String getBillingName() {
		return billingName;
	}

	public void setBillingName(String billingName) {
		this.billingName = billingName;
	}

	public String getBillingAddress() {
		return billingAddress;
	}

	public void setBillingAddress(String billingAddress) {
		this.billingAddress = billingAddress;
	}

	public String getBillingAccount() {
		return billingAccount;
	}

	public void setBillingAccount(String billingAccount) {
		this.billingAccount = billingAccount;
	}

	public String getCustRemark() {
		return custRemark;
	}

	public void setCustRemark(String custRemark) {
		this.custRemark = custRemark;
	}

	public String getIsMember() {
		return isMember;
	}

	public void setIsMember(String isMember) {
		this.isMember = isMember;
	}

	public String getMemberNumber() {
		return memberNumber;
	}

	public void setMemberNumber(String memberNumber) {
		this.memberNumber = memberNumber;
	}

	public String getMemberClass() {
		return memberClass;
	}

	public void setMemberClass(String memberClass) {
		this.memberClass = memberClass;
	}

	public Long getMemberPeriodValidity() {
		return memberPeriodValidity;
	}

	public void setMemberPeriodValidity(Long memberPeriodValidity) {
		this.memberPeriodValidity = memberPeriodValidity;
	}

	public BigDecimal getAccessoriesDiscount() {
		return accessoriesDiscount;
	}

	public void setAccessoriesDiscount(BigDecimal accessoriesDiscount) {
		this.accessoriesDiscount = accessoriesDiscount;
	}

	public BigDecimal getWorkhoursDiscount() {
		return workhoursDiscount;
	}

	public void setWorkhoursDiscount(BigDecimal workhoursDiscount) {
		this.workhoursDiscount = workhoursDiscount;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getYtSapCode() {
		return ytSapCode;
	}

	public void setYtSapCode(String ytSapCode) {
		this.ytSapCode = ytSapCode;
	}

	public String getYtCustomerManager() {
		return ytCustomerManager;
	}

	public void setYtCustomerManager(String ytCustomerManager) {
		this.ytCustomerManager = ytCustomerManager;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
	}

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getCustRelation() {
		return custRelation;
	}

	public void setCustRelation(String custRelation) {
		this.custRelation = custRelation;
	}

	public String getIndepenLegalperson() {
		return indepenLegalperson;
	}

	public void setIndepenLegalperson(String indepenLegalperson) {
		this.indepenLegalperson = indepenLegalperson;
	}

	public String getMarketSegment() {
		return marketSegment;
	}

	public void setMarketSegment(String marketSegment) {
		this.marketSegment = marketSegment;
	}

	public String getInstitutionCode() {
		return institutionCode;
	}

	public void setInstitutionCode(String institutionCode) {
		this.institutionCode = institutionCode;
	}

	public String getComConstitution() {
		return comConstitution;
	}

	public void setComConstitution(String comConstitution) {
		this.comConstitution = comConstitution;
	}

	public String getRegisteredCapital() {
		return registeredCapital;
	}

	public void setRegisteredCapital(String registeredCapital) {
		this.registeredCapital = registeredCapital;
	}

	public String getVehicleStructure() {
		return vehicleStructure;
	}

	public void setVehicleStructure(String vehicleStructure) {
		this.vehicleStructure = vehicleStructure;
	}

	public String getAgency() {
		return agency;
	}

	public void setAgency(String agency) {
		this.agency = agency;
	}

	public String getSapCode() {
		return sapCode;
	}

	public void setSapCode(String sapCode) {
		this.sapCode = sapCode;
	}

	public String getBusinessScope() {
		return businessScope;
	}

	public void setBusinessScope(String businessScope) {
		this.businessScope = businessScope;
	}

	public String getEntQualification() {
		return entQualification;
	}

	public void setEntQualification(String entQualification) {
		this.entQualification = entQualification;
	}

	public String getCustCrmGuid() {
		return custCrmGuid;
	}

	public void setCustCrmGuid(String custCrmGuid) {
		this.custCrmGuid = custCrmGuid;
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

}