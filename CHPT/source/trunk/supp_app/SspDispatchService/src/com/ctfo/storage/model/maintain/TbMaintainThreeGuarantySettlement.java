package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 三包结算单<br>
 * 描述： 三包结算单<br>
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
public class TbMaintainThreeGuarantySettlement implements Serializable {

	/** */
	private static final long serialVersionUID = 2549441018196545761L;

	/** 信息id */
	private String stId;

	/** 客户id */
	private String custId;

	/** 结算单号 */
	private String settlementNo;

	/** 服务站结算单号 */
	private String stationSettlementNo;

	/** 服务站sap代码 */
	private String serviceStationCode;

	/** 服务站名称 */
	private String serviceStationName;

	/** 财务凭证号 */
	private String financeVoucherNo;

	/** 会计年度 */
	private String accountantAnnual;

	/** 公司代码 */
	private String companyCode;

	/** 费用总计 */
	private BigDecimal sumCost;

	/** 服务单费用合计 */
	private BigDecimal billSumCost;

	/** 旧件费用合计 */
	private BigDecimal oldpartSumCost;

	/** 配件费用合计 */
	private BigDecimal fittingSumMoney;

	/** 工时费用合计 */
	private BigDecimal manHourSumMoney;

	/** 其它费用合计 */
	private BigDecimal otherSumCost;

	/** 结算周期 */
	private String settlementCycle;

	/** 结算月份 */
	private String settlementDate;

	/** 清帐时间 */
	private Long clearingTime;

	/** 单据状态--宇通 */
	private String infoStatusYt;

	/** 单据状态 */
	private String infoStatus;

	/** 单据审核确认状态 */
	private String auditStatus;

	/** 是否锁定 */
	private String isLock;

	/** 备注 */
	private String remark;

	/** 创建时间 */
	private Long createTime;

	/** 修改时间 */
	private Long updateTime;

	/** 信息删除状态（1|有效；0|删除） */
	private String enableFlag;

	/** 创建人（制单人） */
	private String createBy;

	/** 最后修改人id */
	private String updateBy;

	/** 经办人 */
	private String responsibleOpid;

	/** 创建人姓名 */
	private String createName;

	/** 修改人姓名 */
	private String updateName;

	/** 经办人姓名 */
	private String responsibleName;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	private Long settlementCycleStart;

	private Long settlementCycleEnd;

	public String getStId() {
		return stId;
	}

	public void setStId(String stId) {
		this.stId = stId;
	}

	public String getCustId() {
		return custId;
	}

	public void setCustId(String custId) {
		this.custId = custId;
	}

	public String getSettlementNo() {
		return settlementNo;
	}

	public void setSettlementNo(String settlementNo) {
		this.settlementNo = settlementNo;
	}

	public String getStationSettlementNo() {
		return stationSettlementNo;
	}

	public void setStationSettlementNo(String stationSettlementNo) {
		this.stationSettlementNo = stationSettlementNo;
	}

	public String getServiceStationCode() {
		return serviceStationCode;
	}

	public void setServiceStationCode(String serviceStationCode) {
		this.serviceStationCode = serviceStationCode;
	}

	public String getServiceStationName() {
		return serviceStationName;
	}

	public void setServiceStationName(String serviceStationName) {
		this.serviceStationName = serviceStationName;
	}

	public String getFinanceVoucherNo() {
		return financeVoucherNo;
	}

	public void setFinanceVoucherNo(String financeVoucherNo) {
		this.financeVoucherNo = financeVoucherNo;
	}

	public String getAccountantAnnual() {
		return accountantAnnual;
	}

	public void setAccountantAnnual(String accountantAnnual) {
		this.accountantAnnual = accountantAnnual;
	}

	public String getCompanyCode() {
		return companyCode;
	}

	public void setCompanyCode(String companyCode) {
		this.companyCode = companyCode;
	}

	public BigDecimal getSumCost() {
		return sumCost;
	}

	public void setSumCost(BigDecimal sumCost) {
		this.sumCost = sumCost;
	}

	public BigDecimal getBillSumCost() {
		return billSumCost;
	}

	public void setBillSumCost(BigDecimal billSumCost) {
		this.billSumCost = billSumCost;
	}

	public BigDecimal getOldpartSumCost() {
		return oldpartSumCost;
	}

	public void setOldpartSumCost(BigDecimal oldpartSumCost) {
		this.oldpartSumCost = oldpartSumCost;
	}

	public BigDecimal getFittingSumMoney() {
		return fittingSumMoney;
	}

	public void setFittingSumMoney(BigDecimal fittingSumMoney) {
		this.fittingSumMoney = fittingSumMoney;
	}

	public BigDecimal getManHourSumMoney() {
		return manHourSumMoney;
	}

	public void setManHourSumMoney(BigDecimal manHourSumMoney) {
		this.manHourSumMoney = manHourSumMoney;
	}

	public BigDecimal getOtherSumCost() {
		return otherSumCost;
	}

	public void setOtherSumCost(BigDecimal otherSumCost) {
		this.otherSumCost = otherSumCost;
	}

	public String getSettlementCycle() {
		return settlementCycle;
	}

	public void setSettlementCycle(String settlementCycle) {
		this.settlementCycle = settlementCycle;
	}

	public String getSettlementDate() {
		return settlementDate;
	}

	public void setSettlementDate(String settlementDate) {
		this.settlementDate = settlementDate;
	}

	public Long getClearingTime() {
		return clearingTime;
	}

	public void setClearingTime(Long clearingTime) {
		this.clearingTime = clearingTime;
	}

	public String getInfoStatusYt() {
		return infoStatusYt;
	}

	public void setInfoStatusYt(String infoStatusYt) {
		this.infoStatusYt = infoStatusYt;
	}

	public String getInfoStatus() {
		return infoStatus;
	}

	public void setInfoStatus(String infoStatus) {
		this.infoStatus = infoStatus;
	}

	public String getAuditStatus() {
		return auditStatus;
	}

	public void setAuditStatus(String auditStatus) {
		this.auditStatus = auditStatus;
	}

	public String getIsLock() {
		return isLock;
	}

	public void setIsLock(String isLock) {
		this.isLock = isLock;
	}

	public String getRemark() {
		return remark;
	}

	public void setRemark(String remark) {
		this.remark = remark;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
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

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public String getResponsibleOpid() {
		return responsibleOpid;
	}

	public void setResponsibleOpid(String responsibleOpid) {
		this.responsibleOpid = responsibleOpid;
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

	public String getResponsibleName() {
		return responsibleName;
	}

	public void setResponsibleName(String responsibleName) {
		this.responsibleName = responsibleName;
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

	public Long getSettlementCycleStart() {
		return settlementCycleStart;
	}

	public void setSettlementCycleStart(Long settlementCycleStart) {
		this.settlementCycleStart = settlementCycleStart;
	}

	public Long getSettlementCycleEnd() {
		return settlementCycleEnd;
	}

	public void setSettlementCycleEnd(Long settlementCycleEnd) {
		this.settlementCycleEnd = settlementCycleEnd;
	}

}