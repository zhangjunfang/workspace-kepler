package com.ctfo.basic.beans;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 企业组织基本信息 <br>
 * 描述： 企业组织基本信息<br>
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
 * <td>2014-6-10</td>
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
public class TbOrgInfo implements Serializable {

	/** */
	private static final long serialVersionUID = -5339790106799814411L;

	/** 组织id */
	private String entId;

	/** 企业编码 */
	private String corpCode;

	/** 企业用户计费代码 */
	private String corpBusinessno;

	/** 行业 */
	private String specialId;

	/** 经营范围 */
	private String businessScope;

	/** 道路运输许可证号发证机关 */
	private String certificateOffice;

	/** 道路运输许可证号 */
	private String licenceNo;

	/** 企业性质 */
	private String corpQuale;

	/** 企业法人 */
	private BigDecimal corpBoss;

	/** 缴费状态1-已缴纳0-未缴纳 */
	private BigDecimal corpPaystate;

	/** 0：普通1：VIP2：公免3：试用 */
	private BigDecimal corpPaytype;

	/** 经济类型 */
	private String corpEconomytype;

	/** 地址 */
	private String orgAddress;

	/** 邮政编码 */
	private String orgCzip;

	/** 传真号码 */
	private String orgCfax;

	/** 网址 */
	private String url;

	/** 邮件地址 */
	private String orgCmail;

	/** 联系人 */
	private String orgCname;

	/** 联系人身份证号码 */
	private String orgCno;

	/** 业务开通日期 */
	private Long createUtc;

	/** 企业logo，存放url，为空时显示默认LOGO */
	private String orgLogo;

	/** 企业备注 */
	private String orgMem;

	/** 电话号码 */
	private String orgCphone;

	/** 企业简称 */
	private String orgShortname;

	/** 所属国家编码 */
	private String corpCountry;

	/** 所属省编码 */
	private String corpProvince;

	/** 所属市编码 */
	private String corpCity;

	/** 道路运输经营许可证字 */
	private String licenceWord;

	/** 标识是否默认车队 1:默认车队。0：不为默认车队 */
	private String isdeteam;

	/** 企业等级编码，JT415中5.3.3章节，对应企业等级编码表 */
	private String corpLevel;

	/** 企业经营许可证号 */
	private String businessLicense;

	/** 所属分中心编码 */
	private String centerCode;

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getCorpCode() {
		return corpCode;
	}

	public void setCorpCode(String corpCode) {
		this.corpCode = corpCode;
	}

	public String getCorpBusinessno() {
		return corpBusinessno;
	}

	public void setCorpBusinessno(String corpBusinessno) {
		this.corpBusinessno = corpBusinessno;
	}

	public String getSpecialId() {
		return specialId;
	}

	public void setSpecialId(String specialId) {
		this.specialId = specialId;
	}

	public String getBusinessScope() {
		return businessScope;
	}

	public void setBusinessScope(String businessScope) {
		this.businessScope = businessScope;
	}

	public String getCertificateOffice() {
		return certificateOffice;
	}

	public void setCertificateOffice(String certificateOffice) {
		this.certificateOffice = certificateOffice;
	}

	public String getLicenceNo() {
		return licenceNo;
	}

	public void setLicenceNo(String licenceNo) {
		this.licenceNo = licenceNo;
	}

	public String getCorpQuale() {
		return corpQuale;
	}

	public void setCorpQuale(String corpQuale) {
		this.corpQuale = corpQuale;
	}

	public BigDecimal getCorpBoss() {
		return corpBoss;
	}

	public void setCorpBoss(BigDecimal corpBoss) {
		this.corpBoss = corpBoss;
	}

	public BigDecimal getCorpPaystate() {
		return corpPaystate;
	}

	public void setCorpPaystate(BigDecimal corpPaystate) {
		this.corpPaystate = corpPaystate;
	}

	public BigDecimal getCorpPaytype() {
		return corpPaytype;
	}

	public void setCorpPaytype(BigDecimal corpPaytype) {
		this.corpPaytype = corpPaytype;
	}

	public String getCorpEconomytype() {
		return corpEconomytype;
	}

	public void setCorpEconomytype(String corpEconomytype) {
		this.corpEconomytype = corpEconomytype;
	}

	public String getOrgAddress() {
		return orgAddress;
	}

	public void setOrgAddress(String orgAddress) {
		this.orgAddress = orgAddress;
	}

	public String getOrgCzip() {
		return orgCzip;
	}

	public void setOrgCzip(String orgCzip) {
		this.orgCzip = orgCzip;
	}

	public String getOrgCfax() {
		return orgCfax;
	}

	public void setOrgCfax(String orgCfax) {
		this.orgCfax = orgCfax;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getOrgCmail() {
		return orgCmail;
	}

	public void setOrgCmail(String orgCmail) {
		this.orgCmail = orgCmail;
	}

	public String getOrgCname() {
		return orgCname;
	}

	public void setOrgCname(String orgCname) {
		this.orgCname = orgCname;
	}

	public String getOrgCno() {
		return orgCno;
	}

	public void setOrgCno(String orgCno) {
		this.orgCno = orgCno;
	}

	public Long getCreateUtc() {
		return createUtc;
	}

	public void setCreateUtc(Long createUtc) {
		this.createUtc = createUtc;
	}

	public String getOrgLogo() {
		return orgLogo;
	}

	public void setOrgLogo(String orgLogo) {
		this.orgLogo = orgLogo;
	}

	public String getOrgMem() {
		return orgMem;
	}

	public void setOrgMem(String orgMem) {
		this.orgMem = orgMem;
	}

	public String getOrgCphone() {
		return orgCphone;
	}

	public void setOrgCphone(String orgCphone) {
		this.orgCphone = orgCphone;
	}

	public String getOrgShortname() {
		return orgShortname;
	}

	public void setOrgShortname(String orgShortname) {
		this.orgShortname = orgShortname;
	}

	public String getCorpCountry() {
		return corpCountry;
	}

	public void setCorpCountry(String corpCountry) {
		this.corpCountry = corpCountry;
	}

	public String getCorpProvince() {
		return corpProvince;
	}

	public void setCorpProvince(String corpProvince) {
		this.corpProvince = corpProvince;
	}

	public String getCorpCity() {
		return corpCity;
	}

	public void setCorpCity(String corpCity) {
		this.corpCity = corpCity;
	}

	public String getLicenceWord() {
		return licenceWord;
	}

	public void setLicenceWord(String licenceWord) {
		this.licenceWord = licenceWord;
	}

	public String getIsdeteam() {
		return isdeteam;
	}

	public void setIsdeteam(String isdeteam) {
		this.isdeteam = isdeteam;
	}

	public String getCorpLevel() {
		return corpLevel;
	}

	public void setCorpLevel(String corpLevel) {
		this.corpLevel = corpLevel;
	}

	public String getBusinessLicense() {
		return businessLicense;
	}

	public void setBusinessLicense(String businessLicense) {
		this.businessLicense = businessLicense;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

}
