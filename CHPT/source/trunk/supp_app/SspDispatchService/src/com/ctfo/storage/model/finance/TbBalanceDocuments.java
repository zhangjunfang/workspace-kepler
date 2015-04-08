package com.ctfo.storage.model.finance;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 结算单据<br>
 * 描述： 结算单据<br>
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
 * <td>2014-12-3</td>
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
public class TbBalanceDocuments implements Serializable {

	/** */
	private static final long serialVersionUID = 6597897009488498475L;

	/** 主键id */
	private String balanceDocumentsId;

	/** 应收付款单id */
	private String orderId;

	/** 单据名称 */
	private String documentsName;

	/** 单据id */
	private String documentsId;

	/** 单据号 */
	private String documentsNum;

	/** 单据日期 */
	private Long documentsDate;

	/** 结算单据类型 1：应收款结算单据 2：应付款结算单位 */
	private String settledType;

	/** 开单金额 */
	private String billingMoney;

	/** 已结算金额 */
	private BigDecimal settledMoney;

	/** 待结算金额 */
	private BigDecimal waitSettledMoney;

	/** 本次结算 */
	private BigDecimal settlementMoney;

	/** 收款 */
	private BigDecimal gathering;

	/** 实收金额 */
	private BigDecimal paidMoney;

	/** 折扣率% */
	private BigDecimal depositRate;

	/** 折扣额 */
	private BigDecimal deduction;

	/** 备注 */
	private String remark;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

	public String getBalanceDocumentsId() {
		return balanceDocumentsId;
	}

	public void setBalanceDocumentsId(String balanceDocumentsId) {
		this.balanceDocumentsId = balanceDocumentsId;
	}

	public String getOrderId() {
		return orderId;
	}

	public void setOrderId(String orderId) {
		this.orderId = orderId;
	}

	public String getDocumentsName() {
		return documentsName;
	}

	public void setDocumentsName(String documentsName) {
		this.documentsName = documentsName;
	}

	public String getDocumentsId() {
		return documentsId;
	}

	public void setDocumentsId(String documentsId) {
		this.documentsId = documentsId;
	}

	public String getDocumentsNum() {
		return documentsNum;
	}

	public void setDocumentsNum(String documentsNum) {
		this.documentsNum = documentsNum;
	}

	public Long getDocumentsDate() {
		return documentsDate;
	}

	public void setDocumentsDate(Long documentsDate) {
		this.documentsDate = documentsDate;
	}

	public String getSettledType() {
		return settledType;
	}

	public void setSettledType(String settledType) {
		this.settledType = settledType;
	}

	public String getBillingMoney() {
		return billingMoney;
	}

	public void setBillingMoney(String billingMoney) {
		this.billingMoney = billingMoney;
	}

	public BigDecimal getSettledMoney() {
		return settledMoney;
	}

	public void setSettledMoney(BigDecimal settledMoney) {
		this.settledMoney = settledMoney;
	}

	public BigDecimal getWaitSettledMoney() {
		return waitSettledMoney;
	}

	public void setWaitSettledMoney(BigDecimal waitSettledMoney) {
		this.waitSettledMoney = waitSettledMoney;
	}

	public BigDecimal getSettlementMoney() {
		return settlementMoney;
	}

	public void setSettlementMoney(BigDecimal settlementMoney) {
		this.settlementMoney = settlementMoney;
	}

	public BigDecimal getGathering() {
		return gathering;
	}

	public void setGathering(BigDecimal gathering) {
		this.gathering = gathering;
	}

	public BigDecimal getPaidMoney() {
		return paidMoney;
	}

	public void setPaidMoney(BigDecimal paidMoney) {
		this.paidMoney = paidMoney;
	}

	public BigDecimal getDepositRate() {
		return depositRate;
	}

	public void setDepositRate(BigDecimal depositRate) {
		this.depositRate = depositRate;
	}

	public BigDecimal getDeduction() {
		return deduction;
	}

	public void setDeduction(BigDecimal deduction) {
		this.deduction = deduction;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
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