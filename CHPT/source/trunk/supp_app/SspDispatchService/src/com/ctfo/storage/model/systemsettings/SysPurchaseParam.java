package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 采购业务参数<br>
 * 描述： 采购业务参数<br>
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
public class SysPurchaseParam implements Serializable {

	/** */
	private static final long serialVersionUID = 6580811529851623559L;

	/** id */
	private String purchaseParamId;

	/** 启用采购计划审核 */
	private String purchasePlanAudit;

	/** 启用采购订单审核 */
	private String purchaseOrderAudit;

	/** 启用采购开单审核 */
	private String purchaseOpenAudit;

	/** 启用宇通采购订单审核 */
	private String purchaseOrderAuditYt;

	/** 启用采购开单直接生成出入库单 */
	private String purchaseOpenOutin;

	/** 制单人和编辑人可以为同一人 */
	private String singleEditorsSamePerson;

	/** 制单人和审核人可以为同一人 */
	private String singleAuditSamePerson;

	/** 制单人和作废人可以为同一人 */
	private String singleDisabledSamePerson;

	/** 制单人和删除人可以为同一人 */
	private String singleDeleteSamePerson;

	/** 采购订单必须导入前置单据生成 */
	private String purchaseOrderImportPre;

	/** 采购开单必须导入前置单据生成 */
	private String purchaseOpenImportPre;

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

	public String getPurchaseParamId() {
		return purchaseParamId;
	}

	public void setPurchaseParamId(String purchaseParamId) {
		this.purchaseParamId = purchaseParamId;
	}

	public String getPurchasePlanAudit() {
		return purchasePlanAudit;
	}

	public void setPurchasePlanAudit(String purchasePlanAudit) {
		this.purchasePlanAudit = purchasePlanAudit;
	}

	public String getPurchaseOrderAudit() {
		return purchaseOrderAudit;
	}

	public void setPurchaseOrderAudit(String purchaseOrderAudit) {
		this.purchaseOrderAudit = purchaseOrderAudit;
	}

	public String getPurchaseOpenAudit() {
		return purchaseOpenAudit;
	}

	public void setPurchaseOpenAudit(String purchaseOpenAudit) {
		this.purchaseOpenAudit = purchaseOpenAudit;
	}

	public String getPurchaseOrderAuditYt() {
		return purchaseOrderAuditYt;
	}

	public void setPurchaseOrderAuditYt(String purchaseOrderAuditYt) {
		this.purchaseOrderAuditYt = purchaseOrderAuditYt;
	}

	public String getPurchaseOpenOutin() {
		return purchaseOpenOutin;
	}

	public void setPurchaseOpenOutin(String purchaseOpenOutin) {
		this.purchaseOpenOutin = purchaseOpenOutin;
	}

	public String getSingleEditorsSamePerson() {
		return singleEditorsSamePerson;
	}

	public void setSingleEditorsSamePerson(String singleEditorsSamePerson) {
		this.singleEditorsSamePerson = singleEditorsSamePerson;
	}

	public String getSingleAuditSamePerson() {
		return singleAuditSamePerson;
	}

	public void setSingleAuditSamePerson(String singleAuditSamePerson) {
		this.singleAuditSamePerson = singleAuditSamePerson;
	}

	public String getSingleDisabledSamePerson() {
		return singleDisabledSamePerson;
	}

	public void setSingleDisabledSamePerson(String singleDisabledSamePerson) {
		this.singleDisabledSamePerson = singleDisabledSamePerson;
	}

	public String getSingleDeleteSamePerson() {
		return singleDeleteSamePerson;
	}

	public void setSingleDeleteSamePerson(String singleDeleteSamePerson) {
		this.singleDeleteSamePerson = singleDeleteSamePerson;
	}

	public String getPurchaseOrderImportPre() {
		return purchaseOrderImportPre;
	}

	public void setPurchaseOrderImportPre(String purchaseOrderImportPre) {
		this.purchaseOrderImportPre = purchaseOrderImportPre;
	}

	public String getPurchaseOpenImportPre() {
		return purchaseOpenImportPre;
	}

	public void setPurchaseOpenImportPre(String purchaseOpenImportPre) {
		this.purchaseOpenImportPre = purchaseOpenImportPre;
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