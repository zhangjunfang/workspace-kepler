package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 销售业务参数<br>
 * 描述： 销售业务参数<br>
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
public class SysSaleParam implements Serializable {

	/** */
	private static final long serialVersionUID = -5616711946321967803L;

	/** id */
	private String saleParamId;

	/** 启用销售计划审核 */
	private String salesPlanAudit;

	/** 启用销售订单审核 */
	private String salesOrderAudit;

	/** 启用销售开单审核 */
	private String salesOpenAudit;

	/** 启用销售开单直接生成出入库单 */
	private String salesOpenOutin;

	/** 销售订单-信用额度 */
	private String salesOrderLineCredit;

	/** 销售开单-信用额度 */
	private String salesOpenLineCredit;

	/** 制单人和编辑人可以为同一人 */
	private String singleEditorsSamePerson;

	/** 制单人和审核人可以为同一人 */
	private String singleAuditSamePerson;

	/** 制单人和作废人可以为同一人 */
	private String singleDisabledSamePerson;

	/** 制单人和删除人可以为同一人 */
	private String singleDeleteSamePerson;

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

	/** 服务站帐套 */
	private String bookId;

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

	public String getSaleParamId() {
		return saleParamId;
	}

	public void setSaleParamId(String saleParamId) {
		this.saleParamId = saleParamId;
	}

	public String getSalesPlanAudit() {
		return salesPlanAudit;
	}

	public void setSalesPlanAudit(String salesPlanAudit) {
		this.salesPlanAudit = salesPlanAudit;
	}

	public String getSalesOrderAudit() {
		return salesOrderAudit;
	}

	public void setSalesOrderAudit(String salesOrderAudit) {
		this.salesOrderAudit = salesOrderAudit;
	}

	public String getSalesOpenAudit() {
		return salesOpenAudit;
	}

	public void setSalesOpenAudit(String salesOpenAudit) {
		this.salesOpenAudit = salesOpenAudit;
	}

	public String getSalesOpenOutin() {
		return salesOpenOutin;
	}

	public void setSalesOpenOutin(String salesOpenOutin) {
		this.salesOpenOutin = salesOpenOutin;
	}

	public String getSalesOrderLineCredit() {
		return salesOrderLineCredit;
	}

	public void setSalesOrderLineCredit(String salesOrderLineCredit) {
		this.salesOrderLineCredit = salesOrderLineCredit;
	}

	public String getSalesOpenLineCredit() {
		return salesOpenLineCredit;
	}

	public void setSalesOpenLineCredit(String salesOpenLineCredit) {
		this.salesOpenLineCredit = salesOpenLineCredit;
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

	public String getBookId() {
		return bookId;
	}

	public void setBookId(String bookId) {
		this.bookId = bookId;
	}

}