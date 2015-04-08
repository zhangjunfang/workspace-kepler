package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 公司档案<br>
 * 描述： 公司档案<br>
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
public class TbCompany implements Serializable {

	/** */
	private static final long serialVersionUID = -5423952104931719559L;

	/** 公司档案ID */
	private String comId;

	/** 上级公司 */
	private String parentId;

	/** 公司编码 */
	private String comCode;

	/** 公司全称 */
	private String comName;

	/** 公司简称 */
	private String comShortName;

	/** 公司地址 */
	private String comAddress;

	/** 省份，关联字典码表 */
	private String province;

	/** 市，关联字典码表 */
	private String city;

	/** 区县，关联字典码表 */
	private String county;

	/** 是否总公司，0否，1是 */
	private String isTotalCompany;

	/** 站别 1是一级站，2是二级站 */
	private String category;

	/** 邮政编码 */
	private String zipCode;

	/** 网址 */
	private String comWebsite;

	/** 联系人 */
	private String comContact;

	/** 电子邮件 */
	private String comEmail;

	/** 手机 */
	private String comPhone;

	/** 联系电话 */
	private String comTel;

	/** 传真 */
	private String comFax;

	/** 维修资质，关联字码表典 */
	private String repairQualification;

	/** 单位性质，关联字典码表 */
	private String unitProperties;

	/** 法人/负责人 */
	private String legalPerson;

	/** 组织机构代码 */
	private String certificateCode;

	/** 独立核算 0 为否 1为是 */
	private String indepenCheck;

	/** 独立法人 0 为否 1为是 */
	private String indepenLegalperson;

	/** 财务独立 0 为否 1为是 */
	private String financialIndepen;

	/** 纳税人资格，关联字典码表 */
	private String taxQualification;

	/** 开户银行，关联字典码表 */
	private String openBank;

	/** 银行账号 */
	private String bankAccount;

	/** 纳税账号 */
	private String taxAccount;

	/** 税号 */
	private String taxNum;

	/** 公司类型，关联字典码表 */
	private String comType;

	/** 备注 */
	private String remark;

	/** 上班时间 */
	private String workTime;

	/** 服务车数 */
	private Integer serCarNum;

	/** 是否维修新能源 0 为否 1为是 */
	private String isRepairNewenergy;

	/** 是否维修NG车 0 为否 1为是 */
	private String isRepairNg;

	/** 人员数量 */
	private Integer staffCounts;

	/** 服务人员数量 */
	private Integer serStaffCounts;

	/** 机修人员数 */
	private Integer machRepairStaffCounts;

	/** 持证电工数 */
	private Integer holderElectricianCounts;

	/** 地沟/举升机数 */
	private Integer trenchCounts;

	/** 大于12M标准地沟/举升机数 */
	private Integer twelveTrenchCounts;

	/** 四轮定位仪数 */
	private Integer fourLocationCounts;

	/** 发动机检测仪数 */
	private Integer engineTestCounts;

	/** 厂区占地总面积 */
	private BigDecimal factoryArea;

	/** 停车区面积 */
	private BigDecimal parkingArea;

	/** 接待室面积 */
	private BigDecimal receptionArea;

	/** 客户休息室面积 */
	private BigDecimal custLoungeArea;

	/** 客户洗手间面积 */
	private BigDecimal custToiletArea;

	/** 会议室面积 */
	private BigDecimal meetingRoomArea;

	/** 培训室面积 */
	private BigDecimal trainingRoomArea;

	/** 结算区面积 */
	private BigDecimal settlementArea;

	/** 待修区面积 */
	private BigDecimal repairedArea;

	/** 检查区面积 */
	private BigDecimal checkArea;

	/** 维修车间面积 */
	private BigDecimal repairWorkshopArea;

	/** 总成大修区面积 */
	private BigDecimal bigRepairedArea;

	/** 配件销售面积 */
	private BigDecimal partsSalesArea;

	/** 配件仓库面积 */
	private BigDecimal partsWarehouseArea;

	/** 旧件仓库面积 */
	private BigDecimal oldpartsWarehouseArea;

	/** 维修说明 */
	private String repairInstructions;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 删除标记，0为删除，1未删除 默认1 */
	private String enableFlag;

	/** 宇通配件仓库面积 */
	private BigDecimal ytpartsWarehouseArea;

	/** 申请时间---宇通 */
	private Long applyTime;

	/** 建站时间---宇通 */
	private Long approvedTime;

	/** 级别---宇通 */
	private String comLevel;

	/** 工时单价---宇通 */
	private BigDecimal workhoursPrice;

	/** 冬季补贴---宇通 */
	private BigDecimal winterSubsidy;

	/** 服务大区---宇通 */
	private String territory;

	/** 国家---宇通 */
	private String country;

	/** 街道---宇通 */
	private String street;

	/** 组织机构代码（工商）---宇通 */
	private String institutionCode;

	/** 热线电话 */
	private String servicePhone;

	/** 星级---宇通 */
	private String starLevel;

	/** SAP代码---宇通 */
	private String sapCode;

	/** 对应中心库站---宇通 */
	private String centerLibrary;

	/** 数据来源，1自建 2宇通 */
	private String dataSource;

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

	public String getComId() {
		return comId;
	}

	public void setComId(String comId) {
		this.comId = comId;
	}

	public String getParentId() {
		return parentId;
	}

	public void setParentId(String parentId) {
		this.parentId = parentId;
	}

	public String getComCode() {
		return comCode;
	}

	public void setComCode(String comCode) {
		this.comCode = comCode;
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

	public String getIsTotalCompany() {
		return isTotalCompany;
	}

	public void setIsTotalCompany(String isTotalCompany) {
		this.isTotalCompany = isTotalCompany;
	}

	public String getCategory() {
		return category;
	}

	public void setCategory(String category) {
		this.category = category;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getComWebsite() {
		return comWebsite;
	}

	public void setComWebsite(String comWebsite) {
		this.comWebsite = comWebsite;
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

	public String getComPhone() {
		return comPhone;
	}

	public void setComPhone(String comPhone) {
		this.comPhone = comPhone;
	}

	public String getComTel() {
		return comTel;
	}

	public void setComTel(String comTel) {
		this.comTel = comTel;
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

	public String getIndepenCheck() {
		return indepenCheck;
	}

	public void setIndepenCheck(String indepenCheck) {
		this.indepenCheck = indepenCheck;
	}

	public String getIndepenLegalperson() {
		return indepenLegalperson;
	}

	public void setIndepenLegalperson(String indepenLegalperson) {
		this.indepenLegalperson = indepenLegalperson;
	}

	public String getFinancialIndepen() {
		return financialIndepen;
	}

	public void setFinancialIndepen(String financialIndepen) {
		this.financialIndepen = financialIndepen;
	}

	public String getTaxQualification() {
		return taxQualification;
	}

	public void setTaxQualification(String taxQualification) {
		this.taxQualification = taxQualification;
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

	public String getTaxAccount() {
		return taxAccount;
	}

	public void setTaxAccount(String taxAccount) {
		this.taxAccount = taxAccount;
	}

	public String getTaxNum() {
		return taxNum;
	}

	public void setTaxNum(String taxNum) {
		this.taxNum = taxNum;
	}

	public String getComType() {
		return comType;
	}

	public void setComType(String comType) {
		this.comType = comType;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public String getWorkTime() {
		return workTime;
	}

	public void setWorkTime(String workTime) {
		this.workTime = workTime;
	}

	public Integer getSerCarNum() {
		return serCarNum;
	}

	public void setSerCarNum(Integer serCarNum) {
		this.serCarNum = serCarNum;
	}

	public String getIsRepairNewenergy() {
		return isRepairNewenergy;
	}

	public void setIsRepairNewenergy(String isRepairNewenergy) {
		this.isRepairNewenergy = isRepairNewenergy;
	}

	public String getIsRepairNg() {
		return isRepairNg;
	}

	public void setIsRepairNg(String isRepairNg) {
		this.isRepairNg = isRepairNg;
	}

	public Integer getStaffCounts() {
		return staffCounts;
	}

	public void setStaffCounts(Integer staffCounts) {
		this.staffCounts = staffCounts;
	}

	public Integer getSerStaffCounts() {
		return serStaffCounts;
	}

	public void setSerStaffCounts(Integer serStaffCounts) {
		this.serStaffCounts = serStaffCounts;
	}

	public Integer getMachRepairStaffCounts() {
		return machRepairStaffCounts;
	}

	public void setMachRepairStaffCounts(Integer machRepairStaffCounts) {
		this.machRepairStaffCounts = machRepairStaffCounts;
	}

	public Integer getHolderElectricianCounts() {
		return holderElectricianCounts;
	}

	public void setHolderElectricianCounts(Integer holderElectricianCounts) {
		this.holderElectricianCounts = holderElectricianCounts;
	}

	public Integer getTrenchCounts() {
		return trenchCounts;
	}

	public void setTrenchCounts(Integer trenchCounts) {
		this.trenchCounts = trenchCounts;
	}

	public Integer getTwelveTrenchCounts() {
		return twelveTrenchCounts;
	}

	public void setTwelveTrenchCounts(Integer twelveTrenchCounts) {
		this.twelveTrenchCounts = twelveTrenchCounts;
	}

	public Integer getFourLocationCounts() {
		return fourLocationCounts;
	}

	public void setFourLocationCounts(Integer fourLocationCounts) {
		this.fourLocationCounts = fourLocationCounts;
	}

	public Integer getEngineTestCounts() {
		return engineTestCounts;
	}

	public void setEngineTestCounts(Integer engineTestCounts) {
		this.engineTestCounts = engineTestCounts;
	}

	public BigDecimal getFactoryArea() {
		return factoryArea;
	}

	public void setFactoryArea(BigDecimal factoryArea) {
		this.factoryArea = factoryArea;
	}

	public BigDecimal getParkingArea() {
		return parkingArea;
	}

	public void setParkingArea(BigDecimal parkingArea) {
		this.parkingArea = parkingArea;
	}

	public BigDecimal getReceptionArea() {
		return receptionArea;
	}

	public void setReceptionArea(BigDecimal receptionArea) {
		this.receptionArea = receptionArea;
	}

	public BigDecimal getCustLoungeArea() {
		return custLoungeArea;
	}

	public void setCustLoungeArea(BigDecimal custLoungeArea) {
		this.custLoungeArea = custLoungeArea;
	}

	public BigDecimal getCustToiletArea() {
		return custToiletArea;
	}

	public void setCustToiletArea(BigDecimal custToiletArea) {
		this.custToiletArea = custToiletArea;
	}

	public BigDecimal getMeetingRoomArea() {
		return meetingRoomArea;
	}

	public void setMeetingRoomArea(BigDecimal meetingRoomArea) {
		this.meetingRoomArea = meetingRoomArea;
	}

	public BigDecimal getTrainingRoomArea() {
		return trainingRoomArea;
	}

	public void setTrainingRoomArea(BigDecimal trainingRoomArea) {
		this.trainingRoomArea = trainingRoomArea;
	}

	public BigDecimal getSettlementArea() {
		return settlementArea;
	}

	public void setSettlementArea(BigDecimal settlementArea) {
		this.settlementArea = settlementArea;
	}

	public BigDecimal getRepairedArea() {
		return repairedArea;
	}

	public void setRepairedArea(BigDecimal repairedArea) {
		this.repairedArea = repairedArea;
	}

	public BigDecimal getCheckArea() {
		return checkArea;
	}

	public void setCheckArea(BigDecimal checkArea) {
		this.checkArea = checkArea;
	}

	public BigDecimal getRepairWorkshopArea() {
		return repairWorkshopArea;
	}

	public void setRepairWorkshopArea(BigDecimal repairWorkshopArea) {
		this.repairWorkshopArea = repairWorkshopArea;
	}

	public BigDecimal getBigRepairedArea() {
		return bigRepairedArea;
	}

	public void setBigRepairedArea(BigDecimal bigRepairedArea) {
		this.bigRepairedArea = bigRepairedArea;
	}

	public BigDecimal getPartsSalesArea() {
		return partsSalesArea;
	}

	public void setPartsSalesArea(BigDecimal partsSalesArea) {
		this.partsSalesArea = partsSalesArea;
	}

	public BigDecimal getPartsWarehouseArea() {
		return partsWarehouseArea;
	}

	public void setPartsWarehouseArea(BigDecimal partsWarehouseArea) {
		this.partsWarehouseArea = partsWarehouseArea;
	}

	public BigDecimal getOldpartsWarehouseArea() {
		return oldpartsWarehouseArea;
	}

	public void setOldpartsWarehouseArea(BigDecimal oldpartsWarehouseArea) {
		this.oldpartsWarehouseArea = oldpartsWarehouseArea;
	}

	public String getRepairInstructions() {
		return repairInstructions;
	}

	public void setRepairInstructions(String repairInstructions) {
		this.repairInstructions = repairInstructions;
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

	public BigDecimal getYtpartsWarehouseArea() {
		return ytpartsWarehouseArea;
	}

	public void setYtpartsWarehouseArea(BigDecimal ytpartsWarehouseArea) {
		this.ytpartsWarehouseArea = ytpartsWarehouseArea;
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

	public String getComLevel() {
		return comLevel;
	}

	public void setComLevel(String comLevel) {
		this.comLevel = comLevel;
	}

	public BigDecimal getWorkhoursPrice() {
		return workhoursPrice;
	}

	public void setWorkhoursPrice(BigDecimal workhoursPrice) {
		this.workhoursPrice = workhoursPrice;
	}

	public BigDecimal getWinterSubsidy() {
		return winterSubsidy;
	}

	public void setWinterSubsidy(BigDecimal winterSubsidy) {
		this.winterSubsidy = winterSubsidy;
	}

	public String getTerritory() {
		return territory;
	}

	public void setTerritory(String territory) {
		this.territory = territory;
	}

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getStreet() {
		return street;
	}

	public void setStreet(String street) {
		this.street = street;
	}

	public String getInstitutionCode() {
		return institutionCode;
	}

	public void setInstitutionCode(String institutionCode) {
		this.institutionCode = institutionCode;
	}

	public String getServicePhone() {
		return servicePhone;
	}

	public void setServicePhone(String servicePhone) {
		this.servicePhone = servicePhone;
	}

	public String getStarLevel() {
		return starLevel;
	}

	public void setStarLevel(String starLevel) {
		this.starLevel = starLevel;
	}

	public String getSapCode() {
		return sapCode;
	}

	public void setSapCode(String sapCode) {
		this.sapCode = sapCode;
	}

	public String getCenterLibrary() {
		return centerLibrary;
	}

	public void setCenterLibrary(String centerLibrary) {
		this.centerLibrary = centerLibrary;
	}

	public String getDataSource() {
		return dataSource;
	}

	public void setDataSource(String dataSource) {
		this.dataSource = dataSource;
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