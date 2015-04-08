package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织<br>
 * 描述： 组织<br>
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
 * <td>2014-5-14</td>
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
public class TbOrganization implements Serializable {

	/* */
	private static final long serialVersionUID = 3148176768559230877L;

	/** 实体ID */
	private String entId;

	/** 实体名称 */
	private String entName;

	/** 实体类型，1为企业，2为车队 */
	private Integer entType;

	/** 父节点ID，根节点为-1 */
	private String parentId;

	/** 创建人id */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 修改人id */
	private String updateBy;

	/** 修改时间 */
	private Long updateTime;

	/** 有效标记 1:有效 0:无效 默认为1 */
	private String enableFlag;

	/** 实体状态：1为正常，0为吊销 */
	private String entState;

	/** 备注 */
	private String memo;

	/** 6位数字序列吗，1开头 */
	private String seqCode;

	/** 是否是总公司（1-是，0-不是） */
	private Integer iscompany;

	/** 所属分中心编码 */
	private String centerCode;

	// 附加信息
	/** 创建人 */
	private String createName;

	/** 企业简称 */
	private String orgShortname;

	/** 企业编码 从600001开始,6位编码 */
	private String corpCode;

	/** 网址 */
	private String url;

	/** 地址 */
	private String orgAddress;

	/** 邮政编码 */
	private String orgCzip;

	/** 邮件地址 */
	private String orgCmail;

	/** 所属省编码 */
	private String corpProvince;

	/** 所属市编码 */
	private String corpCity;

	/** 企业性质 */
	private String corpQuale;

	/** 企业等级编码 */
	private String corpLevel;

	/** 道路运输许可证号 */
	private String licenceNo;

	/** 联系人 */
	private String orgCname;

	/** 传真号码 */
	private String orgCfax;

	/** 电话号码 */
	private String orgCphone;

	/** 父企业 */
	private String parentName;

	/** 是否超级企业（1：是，0：否）默认为0否 */
	private String issuper;

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

	public Integer getEntType() {
		return entType;
	}

	public void setEntType(Integer entType) {
		this.entType = entType;
	}

	public String getParentId() {
		return parentId;
	}

	public void setParentId(String parentId) {
		this.parentId = parentId;
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

	public String getEntState() {
		return entState;
	}

	public void setEntState(String entState) {
		this.entState = entState;
	}

	public String getMemo() {
		return memo;
	}

	public void setMemo(String memo) {
		this.memo = memo;
	}

	public String getSeqCode() {
		return seqCode;
	}

	public void setSeqCode(String seqCode) {
		this.seqCode = seqCode;
	}

	public Integer getIscompany() {
		return iscompany;
	}

	public void setIscompany(Integer iscompany) {
		this.iscompany = iscompany;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public String getOrgShortname() {
		return orgShortname;
	}

	public void setOrgShortname(String orgShortname) {
		this.orgShortname = orgShortname;
	}

	public String getCorpCode() {
		return corpCode;
	}

	public void setCorpCode(String corpCode) {
		this.corpCode = corpCode;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
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

	public String getOrgCmail() {
		return orgCmail;
	}

	public void setOrgCmail(String orgCmail) {
		this.orgCmail = orgCmail;
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

	public String getCorpQuale() {
		return corpQuale;
	}

	public void setCorpQuale(String corpQuale) {
		this.corpQuale = corpQuale;
	}

	public String getCorpLevel() {
		return corpLevel;
	}

	public void setCorpLevel(String corpLevel) {
		this.corpLevel = corpLevel;
	}

	public String getLicenceNo() {
		return licenceNo;
	}

	public void setLicenceNo(String licenceNo) {
		this.licenceNo = licenceNo;
	}

	public String getOrgCname() {
		return orgCname;
	}

	public void setOrgCname(String orgCname) {
		this.orgCname = orgCname;
	}

	public String getOrgCfax() {
		return orgCfax;
	}

	public void setOrgCfax(String orgCfax) {
		this.orgCfax = orgCfax;
	}

	public String getOrgCphone() {
		return orgCphone;
	}

	public void setOrgCphone(String orgCphone) {
		this.orgCphone = orgCphone;
	}

	public String getParentName() {
		return parentName;
	}

	public void setParentName(String parentName) {
		this.parentName = parentName;
	}

	public String getIssuper() {
		return issuper;
	}

	public void setIssuper(String issuper) {
		this.issuper = issuper;
	}

}
