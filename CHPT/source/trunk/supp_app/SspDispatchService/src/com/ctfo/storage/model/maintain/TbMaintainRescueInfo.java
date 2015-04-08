package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 救援单信息表<br>
 * 描述： 救援单信息表<br>
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
 * <td>2015-1-7</td>
 * <td>Administrator</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author Administrator
 * @since JDK1.6
 */
public class TbMaintainRescueInfo implements Serializable {

	/** */
	private static final long serialVersionUID = 3296260783965715953L;

	/**
	 * tb_maintain_rescue_info.rescue_id 信息id
	 */
	private String rescueId;

	/**
	 * tb_maintain_rescue_info.org_id 所属公司
	 */
	private String orgId;

	/**
	 * tb_maintain_rescue_info.rescue_no 救援单号
	 */
	private String rescueNo;

	/**
	 * tb_maintain_rescue_info.make_time 制单时间
	 */
	private Long makeTime;

	/**
	 * tb_maintain_rescue_info.vehicle_no 车牌号
	 */
	private String vehicleNo;

	/**
	 * tb_maintain_rescue_info.vehicle_vin 车辆vin
	 */
	private String vehicleVin;

	/**
	 * tb_maintain_rescue_info.vehicle_brand 车辆品牌
	 */
	private String vehicleBrand;

	/**
	 * tb_maintain_rescue_info.vehicle_model 车型
	 */
	private String vehicleModel;

	/**
	 * tb_maintain_rescue_info.engine_type 发动机
	 */
	private String engineType;

	/**
	 * tb_maintain_rescue_info.driver_name 司机姓名
	 */
	private String driverName;

	/**
	 * tb_maintain_rescue_info.driver_mobile 司机手机
	 */
	private String driverMobile;

	/**
	 * tb_maintain_rescue_info.vehicle_color 车辆颜色
	 */
	private String vehicleColor;

	/**
	 * tb_maintain_rescue_info.customer_id 客户关联id
	 */
	private String customerId;

	/**
	 * tb_maintain_rescue_info.customer_name 客户名称
	 */
	private String customerName;

	/**
	 * tb_maintain_rescue_info.customer_code 客户编码
	 */
	private String customerCode;

	/**
	 * tb_maintain_rescue_info.linkman 联系人
	 */
	private String linkman;

	/**
	 * tb_maintain_rescue_info.linkman_mobile 联系人手机
	 */
	private String linkmanMobile;

	/**
	 * tb_maintain_rescue_info.rescue_type 救援类型
	 */
	private String rescueType;

	/**
	 * tb_maintain_rescue_info.service_vehicle_no 救援车号
	 */
	private String serviceVehicleNo;

	/**
	 * tb_maintain_rescue_info.apply_rescue_place 申救地点
	 */
	private String applyRescuePlace;

	/**
	 * tb_maintain_rescue_info.fault_describe 故障描述
	 */
	private String faultDescribe;

	/**
	 * tb_maintain_rescue_info.depart_time 出发时间
	 */
	private Long departTime;

	/**
	 * tb_maintain_rescue_info.rescue_mileage 救援里程
	 */
	private BigDecimal rescueMileage;

	/**
	 * tb_maintain_rescue_info.arrive_time 到达时间
	 */
	private Long arriveTime;

	/**
	 * tb_maintain_rescue_info.back_time 返回时间
	 */
	private Long backTime;

	/**
	 * tb_maintain_rescue_info.man_hour_sum_money 工时货款
	 */
	private BigDecimal manHourSumMoney;

	/**
	 * tb_maintain_rescue_info.man_hour_tax_rate 工时税率
	 */
	private BigDecimal manHourTaxRate;

	/**
	 * tb_maintain_rescue_info.man_hour_tax_cost 工时税额
	 */
	private BigDecimal manHourTaxCost;

	/**
	 * tb_maintain_rescue_info.man_hour_sum 工时税费合计
	 */
	private BigDecimal manHourSum;

	/**
	 * tb_maintain_rescue_info.fitting_sum_money 配件货款
	 */
	private BigDecimal fittingSumMoney;

	/**
	 * tb_maintain_rescue_info.fitting_tax_rate 配件税率
	 */
	private BigDecimal fittingTaxRate;

	/**
	 * tb_maintain_rescue_info.fitting_tax_cost 配件税额
	 */
	private BigDecimal fittingTaxCost;

	/**
	 * tb_maintain_rescue_info.fitting_sum 配件税费合计
	 */
	private BigDecimal fittingSum;

	/**
	 * tb_maintain_rescue_info.other_item_sum_money 其它项目费用
	 */
	private BigDecimal otherItemSumMoney;

	/**
	 * tb_maintain_rescue_info.other_item_tax_rate 其它项目税率
	 */
	private BigDecimal otherItemTaxRate;

	/**
	 * tb_maintain_rescue_info.other_item_tax_cost 其它项目税额
	 */
	private BigDecimal otherItemTaxCost;

	/**
	 * tb_maintain_rescue_info.other_item_sum 其它项目税费合计
	 */
	private BigDecimal otherItemSum;

	/**
	 * tb_maintain_rescue_info.should_sum 应收总额
	 */
	private BigDecimal shouldSum;

	/**
	 * tb_maintain_rescue_info.received_sum 实收总额
	 */
	private BigDecimal receivedSum;

	/**
	 * tb_maintain_rescue_info.privilege_cost 优惠费用
	 */
	private BigDecimal privilegeCost;

	/**
	 * tb_maintain_rescue_info.debt_cost 本次欠款金额
	 */
	private BigDecimal debtCost;

	/**
	 * tb_maintain_rescue_info.make_invoice_type 开票类型
	 */
	private String makeInvoiceType;

	/**
	 * tb_maintain_rescue_info.payment_terms 结算方式
	 */
	private String paymentTerms;

	/**
	 * tb_maintain_rescue_info.settlement_account 结算账户
	 */
	private String settlementAccount;

	/**
	 * tb_maintain_rescue_info.settle_company 结算单位
	 */
	private String settleCompany;

	/**
	 * tb_maintain_rescue_info.enable_flag 信息状态（1|有效；0|删除）
	 */
	private String enableFlag;

	/**
	 * tb_maintain_rescue_info.verify_advice 审核意见
	 */
	private String verifyAdvice;

	/**
	 * tb_maintain_rescue_info.document_status 单据状态
	 */
	private String documentStatus;

	/**
	 * tb_maintain_rescue_info.responsible_opid 经办人
	 */
	private String responsibleOpid;

	/**
	 * tb_maintain_rescue_info.responsible_name 经办人姓名
	 */
	private String responsibleName;

	/**
	 * tb_maintain_rescue_info.org_name 部门
	 */
	private String orgName;

	/**
	 * tb_maintain_rescue_info.create_by 创建人
	 */
	private String createBy;

	/**
	 * tb_maintain_rescue_info.update_by 最后修改人id
	 */
	private String updateBy;

	/**
	 * tb_maintain_rescue_info.remarks 救援备注
	 */
	private String remarks;

	/**
	 * tb_maintain_rescue_info.create_name 创建人姓名
	 */
	private String createName;

	/**
	 * tb_maintain_rescue_info.update_name 修改人姓名
	 */
	private String updateName;

	/**
	 * tb_maintain_rescue_info.update_time 修改时间
	 */
	private Long updateTime;

	/**
	 * tb_maintain_rescue_info.create_time 创建时间
	 */
	private Long createTime;

	/**
	 * tb_maintain_rescue_info.maintain_id 关联id
	 */
	private String maintainId;

	/**
	 * tb_maintain_rescue_info.ser_station_id 服务站id，云平台用
	 */
	private String serStationId;

	/**
	 * tb_maintain_rescue_info.set_book_id 帐套id，云平台用
	 */
	private String setBookId;

	public String getRescueId() {
		return rescueId;
	}

	public void setRescueId(String rescueId) {
		this.rescueId = rescueId;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
	}

	public String getRescueNo() {
		return rescueNo;
	}

	public void setRescueNo(String rescueNo) {
		this.rescueNo = rescueNo;
	}

	public Long getMakeTime() {
		return makeTime;
	}

	public void setMakeTime(Long makeTime) {
		this.makeTime = makeTime;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getVehicleVin() {
		return vehicleVin;
	}

	public void setVehicleVin(String vehicleVin) {
		this.vehicleVin = vehicleVin;
	}

	public String getVehicleBrand() {
		return vehicleBrand;
	}

	public void setVehicleBrand(String vehicleBrand) {
		this.vehicleBrand = vehicleBrand;
	}

	public String getVehicleModel() {
		return vehicleModel;
	}

	public void setVehicleModel(String vehicleModel) {
		this.vehicleModel = vehicleModel;
	}

	public String getEngineType() {
		return engineType;
	}

	public void setEngineType(String engineType) {
		this.engineType = engineType;
	}

	public String getDriverName() {
		return driverName;
	}

	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}

	public String getDriverMobile() {
		return driverMobile;
	}

	public void setDriverMobile(String driverMobile) {
		this.driverMobile = driverMobile;
	}

	public String getVehicleColor() {
		return vehicleColor;
	}

	public void setVehicleColor(String vehicleColor) {
		this.vehicleColor = vehicleColor;
	}

	public String getCustomerId() {
		return customerId;
	}

	public void setCustomerId(String customerId) {
		this.customerId = customerId;
	}

	public String getCustomerName() {
		return customerName;
	}

	public void setCustomerName(String customerName) {
		this.customerName = customerName;
	}

	public String getCustomerCode() {
		return customerCode;
	}

	public void setCustomerCode(String customerCode) {
		this.customerCode = customerCode;
	}

	public String getLinkman() {
		return linkman;
	}

	public void setLinkman(String linkman) {
		this.linkman = linkman;
	}

	public String getLinkmanMobile() {
		return linkmanMobile;
	}

	public void setLinkmanMobile(String linkmanMobile) {
		this.linkmanMobile = linkmanMobile;
	}

	public String getRescueType() {
		return rescueType;
	}

	public void setRescueType(String rescueType) {
		this.rescueType = rescueType;
	}

	public String getServiceVehicleNo() {
		return serviceVehicleNo;
	}

	public void setServiceVehicleNo(String serviceVehicleNo) {
		this.serviceVehicleNo = serviceVehicleNo;
	}

	public String getApplyRescuePlace() {
		return applyRescuePlace;
	}

	public void setApplyRescuePlace(String applyRescuePlace) {
		this.applyRescuePlace = applyRescuePlace;
	}

	public String getFaultDescribe() {
		return faultDescribe;
	}

	public void setFaultDescribe(String faultDescribe) {
		this.faultDescribe = faultDescribe;
	}

	public Long getDepartTime() {
		return departTime;
	}

	public void setDepartTime(Long departTime) {
		this.departTime = departTime;
	}

	public BigDecimal getRescueMileage() {
		return rescueMileage;
	}

	public void setRescueMileage(BigDecimal rescueMileage) {
		this.rescueMileage = rescueMileage;
	}

	public Long getArriveTime() {
		return arriveTime;
	}

	public void setArriveTime(Long arriveTime) {
		this.arriveTime = arriveTime;
	}

	public Long getBackTime() {
		return backTime;
	}

	public void setBackTime(Long backTime) {
		this.backTime = backTime;
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

	public String getVerifyAdvice() {
		return verifyAdvice;
	}

	public void setVerifyAdvice(String verifyAdvice) {
		this.verifyAdvice = verifyAdvice;
	}

	public String getDocumentStatus() {
		return documentStatus;
	}

	public void setDocumentStatus(String documentStatus) {
		this.documentStatus = documentStatus;
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

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
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