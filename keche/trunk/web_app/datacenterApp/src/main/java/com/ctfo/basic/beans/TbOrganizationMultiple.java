package com.ctfo.basic.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 多组织<br>
 * 描述： 多组织<br>
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
 * <td>2014-6-20</td>
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
public class TbOrganizationMultiple implements Serializable {

	/** */
	private static final long serialVersionUID = 6311223430281133409L;

	/** 实体ID */
	private String entId;

	/** 实体名称 */
	private String entName;

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

	/** 是否超级企业（1：是，0：否）默认为0否 */
	private Integer issuper;

	/** 企业编码 从600001开始,6位编码 */
	private String corpCode;

	/** 企业简称 */
	private String orgShortname;

	// 附加信息
	/** 创建人名称 */
	private String createName;

	/** 修改人名称 */
	private String updateName;

	/** 上级企业 */
	private String parentName;

	/** 所属分中心编码 */
	private String centerCode;

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

	public Integer getIssuper() {
		return issuper;
	}

	public void setIssuper(Integer issuper) {
		this.issuper = issuper;
	}

	public String getCorpCode() {
		return corpCode;
	}

	public void setCorpCode(String corpCode) {
		this.corpCode = corpCode;
	}

	public String getOrgShortname() {
		return orgShortname;
	}

	public void setOrgShortname(String orgShortname) {
		this.orgShortname = orgShortname;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

	public String getUpdateName() {
		return updateName;
	}

	public void setUpdateName(String updateName) {
		this.updateName = updateName;
	}

	public String getParentName() {
		return parentName;
	}

	public void setParentName(String parentName) {
		this.parentName = parentName;
	}

	public String getCenterCode() {
		return centerCode;
	}

	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

}
