package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 帐套<br>
 * 描述： 帐套<br>
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
public class SysSetbook implements Serializable {

	/** */
	private static final long serialVersionUID = 8752189277232426827L;

	/** id */
	private String id;

	/** 帐套名称 */
	private String setbookName;

	/** 帐套代码 */
	private String setbookCode;

	/** 是否主帐套 */
	private String isMainSetBook;

	/** 所属公司名称 */
	private String comName;

	/** 组织结构代码 */
	private String organizationCode;

	/** 法人 */
	private String legalPerson;

	/** 开户银行 */
	private String openingBank;

	/** 银行帐号 */
	private String bankAccount;

	/** 所属省，关联字典码表 */
	private String province;

	/** 所属市，关联字典码表 */
	private String city;

	/** 所属区县，关联字典码表 */
	private String county;

	/** 通讯地址 */
	private String postalAddress;

	/** 邮政编码 */
	private String zipCode;

	/** 公司网址 */
	private String companyWebSite;

	/** 电子邮件 */
	private String email;

	/** 联系人 */
	private String contact;

	/** 联系电话 */
	private String contactTelephone;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 状态 0停用，1启用 */
	private String status;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 删除标记，0删除，1未删除 */
	private String enableFlag;

	public String getSetBookId() {
		return setBookId;
	}

	public void setSetBookId(String setBookId) {
		this.setBookId = setBookId;
	}

	public String getSetbookName() {
		return setbookName;
	}

	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}

	public String getSetbookCode() {
		return setbookCode;
	}

	public void setSetbookCode(String setbookCode) {
		this.setbookCode = setbookCode;
	}

	public String getIsMainSetBook() {
		return isMainSetBook;
	}

	public void setIsMainSetBook(String isMainSetBook) {
		this.isMainSetBook = isMainSetBook;
	}

	public String getComName() {
		return comName;
	}

	public void setComName(String comName) {
		this.comName = comName;
	}

	public String getOrganizationCode() {
		return organizationCode;
	}

	public void setOrganizationCode(String organizationCode) {
		this.organizationCode = organizationCode;
	}

	public String getLegalPerson() {
		return legalPerson;
	}

	public void setLegalPerson(String legalPerson) {
		this.legalPerson = legalPerson;
	}

	public String getOpeningBank() {
		return openingBank;
	}

	public void setOpeningBank(String openingBank) {
		this.openingBank = openingBank;
	}

	public String getBankAccount() {
		return bankAccount;
	}

	public void setBankAccount(String bankAccount) {
		this.bankAccount = bankAccount;
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

	public String getPostalAddress() {
		return postalAddress;
	}

	public void setPostalAddress(String postalAddress) {
		this.postalAddress = postalAddress;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getCompanyWebSite() {
		return companyWebSite;
	}

	public void setCompanyWebSite(String companyWebSite) {
		this.companyWebSite = companyWebSite;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getContact() {
		return contact;
	}

	public void setContact(String contact) {
		this.contact = contact;
	}

	public String getContactTelephone() {
		return contactTelephone;
	}

	public void setContactTelephone(String contactTelephone) {
		this.contactTelephone = contactTelephone;
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

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
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

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

}