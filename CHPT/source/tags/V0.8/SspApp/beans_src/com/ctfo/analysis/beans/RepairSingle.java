package com.ctfo.analysis.beans;

import java.io.Serializable;
import java.math.BigDecimal;

public class RepairSingle implements Serializable{
	 /**
	 * 
	 */
	private static final long serialVersionUID = -307229471164499953L;

	/**
     * tb_maintain_settlement_info.settlement_id
     * 维修结算信息id
     */
    private String settlementId;

    /**
     * tb_maintain_settlement_info.org_id
     * 所属公司
     */
    private String orgId;

    /**
     * tb_maintain_settlement_info.man_hour_sum_money
     * 工时货款
     */
    private BigDecimal manHourSumMoney;

    /**
     * tb_maintain_settlement_info.man_hour_tax_rate
     * 工时税率
     */
    private BigDecimal manHourTaxRate;

    /**
     * tb_maintain_settlement_info.man_hour_tax_cost
     * 工时税额
     */
    private BigDecimal manHourTaxCost;

    /**
     * tb_maintain_settlement_info.man_hour_sum
     * 工时税费合计
     */
    private BigDecimal manHourSum;

    /**
     * tb_maintain_settlement_info.fitting_sum_money
     * 配件货款
     */
    private BigDecimal fittingSumMoney;

    /**
     * tb_maintain_settlement_info.fitting_tax_rate
     * 配件税率
     */
    private BigDecimal fittingTaxRate;

    /**
     * tb_maintain_settlement_info.fitting_tax_cost
     * 配件税额
     */
    private BigDecimal fittingTaxCost;

    /**
     * tb_maintain_settlement_info.fitting_sum
     * 配件税费合计
     */
    private BigDecimal fittingSum;

    /**
     * tb_maintain_settlement_info.other_item_sum_money
     * 其它项目费用
     */
    private BigDecimal otherItemSumMoney;

    /**
     * tb_maintain_settlement_info.other_item_tax_rate
     * 其它项目税率
     */
    private BigDecimal otherItemTaxRate;

    /**
     * tb_maintain_settlement_info.other_item_tax_cost
     * 其它项目税额
     */
    private BigDecimal otherItemTaxCost;

    /**
     * tb_maintain_settlement_info.other_item_sum
     * 其它项目税费合计
     */
    private BigDecimal otherItemSum;

    /**
     * tb_maintain_settlement_info.should_sum
     * 应收总额
     */
    private BigDecimal shouldSum;

    /**
     * tb_maintain_settlement_info.received_sum
     * 实收总额
     */
    private BigDecimal receivedSum;

    /**
     * tb_maintain_settlement_info.privilege_cost
     * 优惠费用
     */
    private BigDecimal privilegeCost;

    /**
     * tb_maintain_settlement_info.debt_cost
     * 本次欠款金额
     */
    private BigDecimal debtCost;

    /**
     * tb_maintain_settlement_info.make_invoice_type
     * 开票类型
     */
    private String makeInvoiceType;

    /**
     * tb_maintain_settlement_info.payment_terms
     * 结算方式
     */
    private String paymentTerms;

    /**
     * tb_maintain_settlement_info.settlement_account
     * 结算账户
     */
    private String settlementAccount;

    /**
     * tb_maintain_settlement_info.settle_company
     * 结算单位
     */
    private String settleCompany;

    /**
     * tb_maintain_settlement_info.enable_flag
     * 信息状态（1|有效；0|删除）
     */
    private String enableFlag;

    /**
     * tb_maintain_settlement_info.document_status
     * 单据状态
     */
    private String documentStatus;

    /**
     * tb_maintain_settlement_info.is_occupy
     * 是否占用
     */
    private String isOccupy;

    /**
     * tb_maintain_settlement_info.update_time
     * 修改时间
     */
    private Long updateTime;

    /**
     * tb_maintain_settlement_info.create_time
     * 创建时间
     */
    private Long createTime;

    /**
     * tb_maintain_settlement_info.maintain_id
     * 关联id
     */
    private String maintainId;

    /**
     * tb_maintain_settlement_info.ser_station_id
     * 服务站id，云平台用
     */
    private String serStationId;

    /**
     * tb_maintain_settlement_info.set_book_id
     * 帐套id，云平台用
     */
    private String setBookId;
	private String exportCompleteTime;
	
	
	
    /**
     * tb_maintain_info.before_orderId
     * 前置单据ID
     */
    private String beforeOrderid;

    /**
     * tb_maintain_info.dispatch_status
     * 默认0，0调度草稿、1未派工、2已派工未开工、3已开工、4已完工、5已停工、6已质检未通过、7已质检通过
     */
    private String dispatchStatus;

    /**
     * tb_maintain_info.orders_source
     * 单据来源,0自建、1预约单、2返修单，默认0自建
     */
    private String ordersSource;

    /**
     * tb_maintain_info.reception_time
     * 接待时间
     */
    private Long receptionTime;

    /**
     * tb_maintain_info.vehicle_no
     * 车牌号
     */
    private String vehicleNo;

    /**
     * tb_maintain_info.vehicle_vin
     * 车辆vin
     */
    private String vehicleVin;

    /**
     * tb_maintain_info.vehicle_brand
     * 车辆品牌
     */
    private String vehicleBrand;

    /**
     * tb_maintain_info.vehicle_model
     * 车型
     */
    private String vehicleModel;

    /**
     * tb_maintain_info.engine_no
     * 发动机
     */
    private String engineNo;

    /**
     * tb_maintain_info.driver_name
     * 司机姓名
     */
    private String driverName;

    /**
     * tb_maintain_info.driver_mobile
     * 司机手机
     */
    private String driverMobile;

    /**
     * tb_maintain_info.vehicle_color
     * 车辆颜色
     */
    private String vehicleColor;

    /**
     * tb_maintain_info.maintain_no
     * 预约单号
     */
    private String maintainNo;

    /**
     * tb_maintain_info.customer_id
     * 关联客户信息id
     */
    private String customerId;

    /**
     * tb_maintain_info.customer_name
     * 客户名称
     */
    private String customerName;

    /**
     * tb_maintain_info.customer_code
     * 客户编码
     */
    private String customerCode;

    /**
     * tb_maintain_info.member_id
     * 关联会员信息id
     */
    private String memberId;

    /**
     * tb_maintain_info.linkman
     * 联系人
     */
    private String linkman;

    /**
     * tb_maintain_info.link_man_mobile
     * 预约手机
     */
    private String linkManMobile;

    /**
     * tb_maintain_info.import_status
     * 单据导入状态，0开放、1占用、3锁定，默认状态下为0
     */
    private String importStatus;

    /**
     * tb_maintain_info.maintain_type
     * 维修类别
     */
    private String maintainType;

    /**
     * tb_maintain_info.maintain_payment
     * 维修付费方式
     */
    private String maintainPayment;

    /**
     * tb_maintain_info.maintain_man
     * 服务顾问
     */
    private String maintainMan;

    /**
     * tb_maintain_info.oil_into_factory
     * 进厂油量
     */
    private BigDecimal oilIntoFactory;

    /**
     * tb_maintain_info.travel_mileage
     * 行驶里程
     */
    private BigDecimal travelMileage;

    /**
     * tb_maintain_info.completion_time
     * 预计完工时间
     */
    private Long completionTime;

    /**
     * tb_maintain_info.set_meal
     * 维修套餐
     */
    private String setMeal;

    /**
     * tb_maintain_info.info_status
     * 单据状态
     */
    private String infoStatus;

    /**
     * tb_maintain_info.fault_describe
     * 故障描述
     */
    private String faultDescribe;

    /**
     * tb_maintain_info.remark
     * 备注
     */
    private String remark;

    /**
     * tb_maintain_info.create_by
     * 创建人（制单人）
     */
    private String createBy;

    /**
     * tb_maintain_info.update_by
     * 最后修改人id
     */
    private String updateBy;

    /**
     * tb_maintain_info.responsible_opid
     * 经办人
     */
    private String responsibleOpid;

    /**
     * tb_maintain_info.create_name
     * 创建人姓名
     */
    private String createName;

    /**
     * tb_maintain_info.update_name
     * 修改人姓名
     */
    private String updateName;

    /**
     * tb_maintain_info.responsible_name
     * 经办人姓名
     */
    private String responsibleName;

    /**
     * tb_maintain_info.org_name
     * 部门
     */
    private String orgName;

    /**
     * tb_maintain_info.complete_work_time
     * 实际完工时间
     */
    private Long completeWorkTime;

    /**
     * tb_maintain_info.maintain_time
     * 建议保养时间
     */
    private Long maintainTime;

    /**
     * tb_maintain_info.maintain_mileage
     * 建议保养里程
     */
    private BigDecimal maintainMileage;

    /**
     * tb_maintain_info.favorable_reason
     * 优惠原因
     */
    private String favorableReason;
    
    /**
     * 会员卡号
     */
    private String memberNumber;
	
    /**
     * 项目折扣
     */
    private String workhoursDiscount;
    
    /**
     * 用料折扣
     */
    private String accessoriesDiscount;
    
    /**
     * 会员等级
     */
    private String memberClass;
	
	public String getMemberNumber() {
		return memberNumber;
	}
	public void setMemberNumber(String memberNumber) {
		this.memberNumber = memberNumber;
	}
	public String getWorkhoursDiscount() {
		return workhoursDiscount;
	}
	public void setWorkhoursDiscount(String workhoursDiscount) {
		this.workhoursDiscount = workhoursDiscount;
	}
	public String getAccessoriesDiscount() {
		return accessoriesDiscount;
	}
	public void setAccessoriesDiscount(String accessoriesDiscount) {
		this.accessoriesDiscount = accessoriesDiscount;
	}
	public String getMemberClass() {
		return memberClass;
	}
	public void setMemberClass(String memberClass) {
		this.memberClass = memberClass;
	}
	public String getVehicleNo() {
		return vehicleNo;
	}
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}
	public String getCustomerCode() {
		return customerCode;
	}
	public void setCustomerCode(String customerCode) {
		this.customerCode = customerCode;
	}
	public String getCustomerName() {
		return customerName;
	}
	public void setCustomerName(String customerName) {
		this.customerName = customerName;
	}
	public String getMaintainPayment() {
		return maintainPayment;
	}
	public void setMaintainPayment(String maintainPayment) {
		this.maintainPayment = maintainPayment;
	}
	public String getMaintainType() {
		return maintainType;
	}
	public void setMaintainType(String maintainType) {
		this.maintainType = maintainType;
	}
	public static long getSerialversionuid() {
		return serialVersionUID;
	}
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
	public String getIsOccupy() {
		return isOccupy;
	}
	public void setIsOccupy(String isOccupy) {
		this.isOccupy = isOccupy;
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
	public String getExportCompleteTime() {
		return exportCompleteTime;
	}
	public void setExportCompleteTime(String exportCompleteTime) {
		this.exportCompleteTime = exportCompleteTime;
	}
	public String getBeforeOrderid() {
		return beforeOrderid;
	}
	public void setBeforeOrderid(String beforeOrderid) {
		this.beforeOrderid = beforeOrderid;
	}
	public String getDispatchStatus() {
		return dispatchStatus;
	}
	public void setDispatchStatus(String dispatchStatus) {
		this.dispatchStatus = dispatchStatus;
	}
	public String getOrdersSource() {
		return ordersSource;
	}
	public void setOrdersSource(String ordersSource) {
		this.ordersSource = ordersSource;
	}
	public Long getReceptionTime() {
		return receptionTime;
	}
	public void setReceptionTime(Long receptionTime) {
		this.receptionTime = receptionTime;
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
	public String getEngineNo() {
		return engineNo;
	}
	public void setEngineNo(String engineNo) {
		this.engineNo = engineNo;
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
	public String getMaintainNo() {
		return maintainNo;
	}
	public void setMaintainNo(String maintainNo) {
		this.maintainNo = maintainNo;
	}
	public String getCustomerId() {
		return customerId;
	}
	public void setCustomerId(String customerId) {
		this.customerId = customerId;
	}
	public String getMemberId() {
		return memberId;
	}
	public void setMemberId(String memberId) {
		this.memberId = memberId;
	}
	public String getLinkman() {
		return linkman;
	}
	public void setLinkman(String linkman) {
		this.linkman = linkman;
	}
	public String getLinkManMobile() {
		return linkManMobile;
	}
	public void setLinkManMobile(String linkManMobile) {
		this.linkManMobile = linkManMobile;
	}
	public String getImportStatus() {
		return importStatus;
	}
	public void setImportStatus(String importStatus) {
		this.importStatus = importStatus;
	}
	public String getMaintainMan() {
		return maintainMan;
	}
	public void setMaintainMan(String maintainMan) {
		this.maintainMan = maintainMan;
	}
	public BigDecimal getOilIntoFactory() {
		return oilIntoFactory;
	}
	public void setOilIntoFactory(BigDecimal oilIntoFactory) {
		this.oilIntoFactory = oilIntoFactory;
	}
	public BigDecimal getTravelMileage() {
		return travelMileage;
	}
	public void setTravelMileage(BigDecimal travelMileage) {
		this.travelMileage = travelMileage;
	}
	public Long getCompletionTime() {
		return completionTime;
	}
	public void setCompletionTime(Long completionTime) {
		this.completionTime = completionTime;
	}
	public String getSetMeal() {
		return setMeal;
	}
	public void setSetMeal(String setMeal) {
		this.setMeal = setMeal;
	}
	public String getInfoStatus() {
		return infoStatus;
	}
	public void setInfoStatus(String infoStatus) {
		this.infoStatus = infoStatus;
	}
	public String getFaultDescribe() {
		return faultDescribe;
	}
	public void setFaultDescribe(String faultDescribe) {
		this.faultDescribe = faultDescribe;
	}
	public String getRemark() {
		return remark;
	}
	public void setRemark(String remark) {
		this.remark = remark;
	}
	public Long getCompleteWorkTime() {
		return completeWorkTime;
	}
	public void setCompleteWorkTime(Long completeWorkTime) {
		this.completeWorkTime = completeWorkTime;
	}
	public Long getMaintainTime() {
		return maintainTime;
	}
	public void setMaintainTime(Long maintainTime) {
		this.maintainTime = maintainTime;
	}
	public BigDecimal getMaintainMileage() {
		return maintainMileage;
	}
	public void setMaintainMileage(BigDecimal maintainMileage) {
		this.maintainMileage = maintainMileage;
	}
	public String getFavorableReason() {
		return favorableReason;
	}
	public void setFavorableReason(String favorableReason) {
		this.favorableReason = favorableReason;
	}
}
