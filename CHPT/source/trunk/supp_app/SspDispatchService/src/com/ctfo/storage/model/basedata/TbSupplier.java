package com.ctfo.storage.model.basedata;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 供应商档案<br>
 * 描述： 供应商档案<br>
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
public class TbSupplier implements Serializable {

	/** */
	private static final long serialVersionUID = -7775133802481578159L;

	/** id */
	private String supId;

	/** 供应商编码 */
	private String supCode;

	/** 供应商简称 */
	private String supShortName;

	/** 供应商首拼 */
	private String supFirstSpell;

	/** 供应商全称 */
	private String supFullName;

	/** 供应商分类，关联字码表典 */
	private String supType;

	/** 联系地址 */
	private String supAddress;

	/** 所属省，关联字典码表 */
	private String province;

	/** 所属市，关联字典码表 */
	private String city;

	/** 所属区县，关联字典码表 */
	private String county;

	/** 邮箱 */
	private String supEmail;

	/** 电话 */
	private String supTel;

	/** 传真 */
	private String supFax;

	/** 邮政编码 */
	private String zipCode;

	/** 网站 */
	private String supWebsite;

	/** 企业性质，关联字典码表 */
	private String unitProperties;

	/** 法人 */
	private String legalPerson;

	/** 纳税人识别号 */
	private String taxNum;

	/** 信用等级，关联字典表 */
	private String creditClass;

	/** 信用额度 */
	private BigDecimal creditLine;

	/** 信用账期 */
	private BigDecimal creditAccountPeriod;

	/** 价格类型，关联字典码表 */
	private String priceType;

	/** 备注 */
	private String remark;

	/** 状态 0，停用 1，启用 */
	private String status;

	/** 删除标记，0删除，1未删除 默认为1 */
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

	public String getSupId() {
		return supId;
	}

	public void setSupId(String supId) {
		this.supId = supId;
	}

	public String getSupCode() {
		return supCode;
	}

	public void setSupCode(String supCode) {
		this.supCode = supCode;
	}

	public String getSupShortName() {
		return supShortName;
	}

	public void setSupShortName(String supShortName) {
		this.supShortName = supShortName;
	}

	public String getSupFirstSpell() {
		return supFirstSpell;
	}

	public void setSupFirstSpell(String supFirstSpell) {
		this.supFirstSpell = supFirstSpell;
	}

	public String getSupFullName() {
		return supFullName;
	}

	public void setSupFullName(String supFullName) {
		this.supFullName = supFullName;
	}

	public String getSupType() {
		return supType;
	}

	public void setSupType(String supType) {
		this.supType = supType;
	}

	public String getSupAddress() {
		return supAddress;
	}

	public void setSupAddress(String supAddress) {
		this.supAddress = supAddress;
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

	public String getSupEmail() {
		return supEmail;
	}

	public void setSupEmail(String supEmail) {
		this.supEmail = supEmail;
	}

	public String getSupTel() {
		return supTel;
	}

	public void setSupTel(String supTel) {
		this.supTel = supTel;
	}

	public String getSupFax() {
		return supFax;
	}

	public void setSupFax(String supFax) {
		this.supFax = supFax;
	}

	public String getZipCode() {
		return zipCode;
	}

	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}

	public String getSupWebsite() {
		return supWebsite;
	}

	public void setSupWebsite(String supWebsite) {
		this.supWebsite = supWebsite;
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

	public String getTaxNum() {
		return taxNum;
	}

	public void setTaxNum(String taxNum) {
		this.taxNum = taxNum;
	}

	public String getCreditClass() {
		return creditClass;
	}

	public void setCreditClass(String creditClass) {
		this.creditClass = creditClass;
	}

	public BigDecimal getCreditLine() {
		return creditLine;
	}

	public void setCreditLine(BigDecimal creditLine) {
		this.creditLine = creditLine;
	}

	public BigDecimal getCreditAccountPeriod() {
		return creditAccountPeriod;
	}

	public void setCreditAccountPeriod(BigDecimal creditAccountPeriod) {
		this.creditAccountPeriod = creditAccountPeriod;
	}

	public String getPriceType() {
		return priceType;
	}

	public void setPriceType(String priceType) {
		this.priceType = priceType;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
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