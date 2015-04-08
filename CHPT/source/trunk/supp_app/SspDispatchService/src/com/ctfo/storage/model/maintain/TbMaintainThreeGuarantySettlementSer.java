package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修服务单<br>
 * 描述： 维修服务单<br>
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
public class TbMaintainThreeGuarantySettlementSer implements Serializable {

	/** */
	private static final long serialVersionUID = -9143152624590434853L;

	/** 信息id */
	private String serId;

	/** 服务单号 */
	private String serviceNo;

	/** 提交时间 */
	private Long submitTime;

	/** 车工号(车厂编号） */
	private String depotNo;

	/** 单据类型 */
	private String receiptType;

	/** 报修人 */
	private String repairerId;

	/** 报修人姓名 */
	private String repairerName;

	/** 工时费用 */
	private BigDecimal manHourSumMoney;

	/** 配件费用 */
	private BigDecimal fittingSumMoney;

	/** 其它费用 */
	private BigDecimal otherItemSumMoney;

	/** 差旅费用 */
	private BigDecimal travelCost;

	/** 服务总费用 */
	private BigDecimal serviceSumCost;

	/** 关联id（三包结算单信息id） */
	private String stId;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

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

	public String getSerId() {
		return serId;
	}

	public void setSerId(String serId) {
		this.serId = serId;
	}

	public String getServiceNo() {
		return serviceNo;
	}

	public void setServiceNo(String serviceNo) {
		this.serviceNo = serviceNo;
	}

	public Long getSubmitTime() {
		return submitTime;
	}

	public void setSubmitTime(Long submitTime) {
		this.submitTime = submitTime;
	}

	public String getDepotNo() {
		return depotNo;
	}

	public void setDepotNo(String depotNo) {
		this.depotNo = depotNo;
	}

	public String getReceiptType() {
		return receiptType;
	}

	public void setReceiptType(String receiptType) {
		this.receiptType = receiptType;
	}

	public String getRepairerId() {
		return repairerId;
	}

	public void setRepairerId(String repairerId) {
		this.repairerId = repairerId;
	}

	public String getRepairerName() {
		return repairerName;
	}

	public void setRepairerName(String repairerName) {
		this.repairerName = repairerName;
	}

	public BigDecimal getManHourSumMoney() {
		return manHourSumMoney;
	}

	public void setManHourSumMoney(BigDecimal manHourSumMoney) {
		this.manHourSumMoney = manHourSumMoney;
	}

	public BigDecimal getFittingSumMoney() {
		return fittingSumMoney;
	}

	public void setFittingSumMoney(BigDecimal fittingSumMoney) {
		this.fittingSumMoney = fittingSumMoney;
	}

	public BigDecimal getOtherItemSumMoney() {
		return otherItemSumMoney;
	}

	public void setOtherItemSumMoney(BigDecimal otherItemSumMoney) {
		this.otherItemSumMoney = otherItemSumMoney;
	}

	public BigDecimal getTravelCost() {
		return travelCost;
	}

	public void setTravelCost(BigDecimal travelCost) {
		this.travelCost = travelCost;
	}

	public BigDecimal getServiceSumCost() {
		return serviceSumCost;
	}

	public void setServiceSumCost(BigDecimal serviceSumCost) {
		this.serviceSumCost = serviceSumCost;
	}

	public String getStId() {
		return stId;
	}

	public void setStId(String stId) {
		this.stId = stId;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
}