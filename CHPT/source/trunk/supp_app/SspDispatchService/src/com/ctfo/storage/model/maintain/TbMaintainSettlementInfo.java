package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修结算信息<br>
 * 描述： 维修结算信息<br>
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
 * <td>2014-10-31</td>
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
public class TbMaintainSettlementInfo implements Serializable {

	/** */
	private static final long serialVersionUID = -7179691496466198939L;

	/** 维修结算信息id */
	private String settlementId;

	/** 所属公司 */
	private String orgId;

	/** 工时货款 */
	private BigDecimal manHourSumMoney;

	/** 工时税率 */
	private BigDecimal manHourTaxRate;

	/** 工时税额 */
	private BigDecimal manHourTaxCost;

	/** 工时税费合计 */
	private BigDecimal manHourSum;

	/** 配件货款 */
	private BigDecimal fittingSumMoney;

	/** 配件税率 */
	private BigDecimal fittingTaxRate;

	/** 配件税额 */
	private BigDecimal fittingTaxCost;

	/** 配件税费合计 */
	private BigDecimal fittingSum;

	/** 其它项目费用 */
	private BigDecimal otherItemSumMoney;

	/** 其它项目税率 */
	private BigDecimal otherItemTaxRate;

	/** 其它项目税额 */
	private BigDecimal otherItemTaxCost;

	/** 其它项目税费合计 */
	private BigDecimal otherItemSum;

	/** 应收总额 */
	private BigDecimal shouldSum;

	/** 实收总额 */
	private BigDecimal receivedSum;

	/** 优惠费用 */
	private BigDecimal privilegeCost;

	/** 本次欠款金额 */
	private BigDecimal debtCost;

	/** 开票类型 */
	private String makeInvoiceType;

	/** 结算方式 */
	private String paymentTerms;

	/** 结算账户 */
	private String settlementAccount;

	/** 结算单位 */
	private String settleCompany;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

	/** 单据状态 */
	private String documentStatus;

	/** 创建人 */
	private String createBy;

	/** 经办人 */
	private String responsibleOpid;

	/** 经办人姓名 */
	private String responsibleName;

	/** 部门 */
	private String orgName;

	/** 最后修改人id */
	private String updateBy;

	/** 创建人姓名 */
	private String createName;

	/** 修改人姓名 */
	private String updateName;

	/** 修改时间 */
	private Long updateTime;

	/** 创建时间 */
	private Long createTime;

	/** 关联id */
	private String maintainId;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getSettlementId() {
		return settlementId;
	}

	public void setSettlementId(String settlementId) {
		this.settlementId = settlementId;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
	}

	public BigDecimal getManHourSumMoney() {
		return manHourSumMoney;
	}

	public void setManHourSumMoney(BigDecimal manHourSumMoney) {
		this.manHourSumMoney = manHourSumMoney;
	}

	public BigDecimal getManHourTaxRate() {
		return manHourTaxRate;
	}

	public void setManHourTaxRate(BigDecimal manHourTaxRate) {
		this.manHourTaxRate = manHourTaxRate;
	}

	public BigDecimal getManHourTaxCost() {
		return manHourTaxCost;
	}

	public void setManHourTaxCost(BigDecimal manHourTaxCost) {
		this.manHourTaxCost = manHourTaxCost;
	}

	public BigDecimal getManHourSum() {
		return manHourSum;
	}

	public void setManHourSum(BigDecimal manHourSum) {
		this.manHourSum = manHourSum;
	}

	public BigDecimal getFittingSumMoney() {
		return fittingSumMoney;
	}

	public void setFittingSumMoney(BigDecimal fittingSumMoney) {
		this.fittingSumMoney = fittingSumMoney;
	}

	public BigDecimal getFittingTaxRate() {
		return fittingTaxRate;
	}

	public void setFittingTaxRate(BigDecimal fittingTaxRate) {
		this.fittingTaxRate = fittingTaxRate;
	}

	public BigDecimal getFittingTaxCost() {
		return fittingTaxCost;
	}

	public void setFittingTaxCost(BigDecimal fittingTaxCost) {
		this.fittingTaxCost = fittingTaxCost;
	}

	public BigDecimal getFittingSum() {
		return fittingSum;
	}

	public void setFittingSum(BigDecimal fittingSum) {
		this.fittingSum = fittingSum;
	}

	public BigDecimal getOtherItemSumMoney() {
		return otherItemSumMoney;
	}

	public void setOtherItemSumMoney(BigDecimal otherItemSumMoney) {
		this.otherItemSumMoney = otherItemSumMoney;
	}

	public BigDecimal getOtherItemTaxRate() {
		return otherItemTaxRate;
	}

	public void setOtherItemTaxRate(BigDecimal otherItemTaxRate) {
		this.otherItemTaxRate = otherItemTaxRate;
	}

	public BigDecimal getOtherItemTaxCost() {
		return otherItemTaxCost;
	}

	public void setOtherItemTaxCost(BigDecimal otherItemTaxCost) {
		this.otherItemTaxCost = otherItemTaxCost;
	}

	public BigDecimal getOtherItemSum() {
		return otherItemSum;
	}

	public void setOtherItemSum(BigDecimal otherItemSum) {
		this.otherItemSum = otherItemSum;
	}

	public BigDecimal getShouldSum() {
		return shouldSum;
	}

	public void setShouldSum(BigDecimal shouldSum) {
		this.shouldSum = shouldSum;
	}

	public BigDecimal getReceivedSum() {
		return receivedSum;
	}

	public void setReceivedSum(BigDecimal receivedSum) {
		this.receivedSum = receivedSum;
	}

	public BigDecimal getPrivilegeCost() {
		return privilegeCost;
	}

	public void setPrivilegeCost(BigDecimal privilegeCost) {
		this.privilegeCost = privilegeCost;
	}

	public BigDecimal getDebtCost() {
		return debtCost;
	}

	public void setDebtCost(BigDecimal debtCost) {
		this.debtCost = debtCost;
	}

	public String getMakeInvoiceType() {
		return makeInvoiceType;
	}

	public void setMakeInvoiceType(String makeInvoiceType) {
		this.makeInvoiceType = makeInvoiceType;
	}

	public String getPaymentTerms() {
		return paymentTerms;
	}

	public void setPaymentTerms(String paymentTerms) {
		this.paymentTerms = paymentTerms;
	}

	public String getSettlementAccount() {
		return settlementAccount;
	}

	public void setSettlementAccount(String settlementAccount) {
		this.settlementAccount = settlementAccount;
	}

	public String getSettleCompany() {
		return settleCompany;
	}

	public void setSettleCompany(String settleCompany) {
		this.settleCompany = settleCompany;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}

	public String getDocumentStatus() {
		return documentStatus;
	}

	public void setDocumentStatus(String documentStatus) {
		this.documentStatus = documentStatus;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public String getResponsibleOpid() {
		return responsibleOpid;
	}

	public void setResponsibleOpid(String responsibleOpid) {
		this.responsibleOpid = responsibleOpid;
	}

	public String getResponsibleName() {
		return responsibleName;
	}

	public void setResponsibleName(String responsibleName) {
		this.responsibleName = responsibleName;
	}

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
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

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getMaintainId() {
		return maintainId;
	}

	public void setMaintainId(String maintainId) {
		this.maintainId = maintainId;
	}

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

}