package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车厂三包服务单<br>
 * 描述： 车厂三包服务单<br>
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
public class TbMaintainThreeGuaranty implements Serializable {

	/** */
	private static final long serialVersionUID = -7522921734870933458L;

	/** 信息id */
	private String tgId;

	/** 审批通过日期 */
	private Long approvalDate;

	/** 提交日期 */
	private Long submitTime;

	/** 三包结算单ID */
	private String stId;

	/** 服务单号 */
	private String serviceNo;

	/** 修改时间 */
	private Long repairsTime;

	/** 车厂服务单号 */
	private String serviceNoYt;

	/** 车厂审批状态 */
	private String approveStatusYt;

	/** 车厂保单序列号 */
	private String seriesNumYt;

	/** 车厂单据类型 */
	private String receiptType;

	/** 是否外出 */
	private String whetherGoOut;

	/** 改装情况 */
	private String refitCase;

	/** 服务站编码 */
	private String serviceStationCode;

	/** 服务站名称 */
	private String serviceStationName;

	/** 客户关联id */
	private String customerId;

	/** 客户名称 */
	private String customerName;

	/** 客户编码 */
	private String customerCode;

	/** 客户邮编 */
	private String customerPostcode;

	/** 车辆使用单位 */
	private String vehicleUseCorp;

	/** 车辆所在地 */
	private String vehicleLocation;

	/** 报修人 */
	private String repairerId;

	/** 报修人姓名 */
	private String repairerName;

	/** 报修人电话 */
	private String repairerMobile;

	/** 送修人id */
	private String sendVehicleId;

	/** 送修人姓名 */
	private String sentVehicleName;

	/** 送修人手机 */
	private String sentVehicleMobile;

	/** 联系人 */
	private String linkman;

	/** 预约手机 */
	private String linkManMobile;

	/** 车牌号 */
	private String vehicleNo;

	/** 车工号(车厂编号） */
	private String depotNo;

	/** 车辆vin */
	private String vehicleVin;

	/** 车型 */
	private String vehicleModel;

	/** 发动机 */
	private String engineNum;

	/** 行驶证号 */
	private String drivingLicenseNo;

	/** 出厂时间 */
	private Long productionTime;

	/** 产地 */
	private String producingArea;

	/** 行驶里程 */
	private BigDecimal travelMileage;

	/** 建议保养时间 */
	private Long maintainTime;

	/** 建议保养里程 */
	private BigDecimal maintainMileage;

	/** 司机姓名 */
	private String driverName;

	/** 司机手机 */
	private String driverMobile;

	/** 是否二级服务站 */
	private String whetherSecondStation;

	/** 维修开始时间 */
	private Long startWorkTime;

	/** 维修结束时间 */
	private Long completeWorkTime;

	/** 鉴定人 */
	private String appraiserId;

	/** 鉴定人姓名 */
	private String appraiserName;

	/** 特殊约定质保（是、否） */
	private String promiseGuarantee;

	/** 车厂审批人 */
	private String approverIdYt;

	/** 车厂审批人姓名 */
	private String approverNameYt;

	/** 政策照顾审批编码 */
	private String policyApprovalNo;

	/** 费用类型(政策照顾) */
	private String costTypePolicy;

	/** 费用类型(服务活动) */
	private String costTypeService;

	/** 情况简述 */
	private String describes;

	/** 产品改进通知号 */
	private String productNoticeNo;

	/** 是否宇通车 */
	private String whetherYt;

	/** 故障责任单位 */
	private String faultDutyCorp;

	/** 故障原因 */
	private String faultCause;

	/** 故障系统 */
	private String faultSystem;

	/** 故障总成 */
	private String faultAssembly;

	/** 故障部件 */
	private String faultPart;

	/** 故障模式 */
	private String faultSchema;

	/** 故障描述 */
	private String faultDescribe;

	/** 原因分析 */
	private String reasonAnalysis;

	/** 处理结果 */
	private String disposeResult;

	/** 配件购买日期 */
	private Long partsBuyTime;

	/** 配件购买单位 */
	private String partsBuyCorp;

	/** 是否包含工时费 */
	private String containManHourCost;

	/** 配件编码 */
	private String partsCode;

	/** 物料描述 */
	private String materielDescribe;

	/** 配件首次安装服务站 */
	private String firstInstallStation;

	/** 配件协议保养期 */
	private String partGuaranteePeriod;

	/** 反馈数量 */
	private BigDecimal feedbackNum;

	/** 外出事由 */
	private String gooutCause;

	/** 外出批准人 */
	private String gooutApprover;

	/** 外出地点 */
	private String gooutPlace;

	/** 交通方式 */
	private String meansTraffic;

	/** 外出时间 */
	private Long gooutTime;

	/** 外出返回时间 */
	private Long gooutBackTime;

	/** 外出里程 */
	private BigDecimal gooutMileage;

	/** 外出人数 */
	private Long gooutPeopleNum;

	/** 路程补助 */
	private BigDecimal journeySubsidy;

	/** 工时补助 */
	private BigDecimal manHourSubsidy;

	/** 工时费用 */
	private BigDecimal manHourSumMoney;

	/** 配件费用 */
	private BigDecimal fittingSumMoney;

	/** 其它费用 */
	private BigDecimal otherItemSumMoney;

	/** 合计费用 */
	private BigDecimal serviceSumCost;

	/** 差旅费 */
	private BigDecimal travelCost;

	/** 单据状态 */
	private String infoStatus;

	/** 创建时间 */
	private Long createTime;

	/** 修改时间 */
	private Long updateTime;

	/** 信息删除状态（1|有效；0|删除） */
	private String enableFlag;

	/** 备注 */
	private String remarks;

	/** 创建人（制单人） */
	private String createBy;

	/** 最后修改人id */
	private String updateBy;

	/** 经办人 */
	private String responsibleOpid;

	/** 部门 */
	private String orgName;

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

	/** 客户性质 */
	private String customerProperty;

	/** 客户地址 */
	private String customerAddress;

	/** 维修人ID */
	private String repairManId;

	/** 维修人名称 */
	private String repairManName;

	/** 故障系统名称 */
	private String faultSystemName;

	/** 故障总成名称 */
	private String faultAssemblyName;

	/** 故障部件名称 */
	private String faultPartName;

	/** 故障模式名称 */
	private String faultSchemaName;

	/** 车船费 */
	private String trafficFee;

	/** 其他费用说明 */
	private String otherItemSumMoneyRemark;

	/** 车辆使用单位名称 */
	private String vehicleUseCorpName;

	/** 车型类型 */
	private String vehicleModelClass;

	/** 车型名称 */
	private String vehicleModelName;

	/** 审核意见 */
	private String verifyInfo;

	/** 整车完工时间 */
	private Long repairCompletionTime;

	public String getTgId() {
		return tgId;
	}

	public void setTgId(String tgId) {
		this.tgId = tgId;
	}

	public Long getApprovalDate() {
		return approvalDate;
	}

	public void setApprovalDate(Long approvalDate) {
		this.approvalDate = approvalDate;
	}

	public Long getSubmitTime() {
		return submitTime;
	}

	public void setSubmitTime(Long submitTime) {
		this.submitTime = submitTime;
	}

	public String getStId() {
		return stId;
	}

	public void setStId(String stId) {
		this.stId = stId;
	}

	public String getServiceNo() {
		return serviceNo;
	}

	public void setServiceNo(String serviceNo) {
		this.serviceNo = serviceNo;
	}

	public Long getRepairsTime() {
		return repairsTime;
	}

	public void setRepairsTime(Long repairsTime) {
		this.repairsTime = repairsTime;
	}

	public String getServiceNoYt() {
		return serviceNoYt;
	}

	public void setServiceNoYt(String serviceNoYt) {
		this.serviceNoYt = serviceNoYt;
	}

	public String getApproveStatusYt() {
		return approveStatusYt;
	}

	public void setApproveStatusYt(String approveStatusYt) {
		this.approveStatusYt = approveStatusYt;
	}

	public String getSeriesNumYt() {
		return seriesNumYt;
	}

	public void setSeriesNumYt(String seriesNumYt) {
		this.seriesNumYt = seriesNumYt;
	}

	public String getReceiptType() {
		return receiptType;
	}

	public void setReceiptType(String receiptType) {
		this.receiptType = receiptType;
	}

	public String getWhetherGoOut() {
		return whetherGoOut;
	}

	public void setWhetherGoOut(String whetherGoOut) {
		this.whetherGoOut = whetherGoOut;
	}

	public String getRefitCase() {
		return refitCase;
	}

	public void setRefitCase(String refitCase) {
		this.refitCase = refitCase;
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

	public String getCustomerPostcode() {
		return customerPostcode;
	}

	public void setCustomerPostcode(String customerPostcode) {
		this.customerPostcode = customerPostcode;
	}

	public String getVehicleUseCorp() {
		return vehicleUseCorp;
	}

	public void setVehicleUseCorp(String vehicleUseCorp) {
		this.vehicleUseCorp = vehicleUseCorp;
	}

	public String getVehicleLocation() {
		return vehicleLocation;
	}

	public void setVehicleLocation(String vehicleLocation) {
		this.vehicleLocation = vehicleLocation;
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

	public String getRepairerMobile() {
		return repairerMobile;
	}

	public void setRepairerMobile(String repairerMobile) {
		this.repairerMobile = repairerMobile;
	}

	public String getSendVehicleId() {
		return sendVehicleId;
	}

	public void setSendVehicleId(String sendVehicleId) {
		this.sendVehicleId = sendVehicleId;
	}

	public String getSentVehicleName() {
		return sentVehicleName;
	}

	public void setSentVehicleName(String sentVehicleName) {
		this.sentVehicleName = sentVehicleName;
	}

	public String getSentVehicleMobile() {
		return sentVehicleMobile;
	}

	public void setSentVehicleMobile(String sentVehicleMobile) {
		this.sentVehicleMobile = sentVehicleMobile;
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

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getDepotNo() {
		return depotNo;
	}

	public void setDepotNo(String depotNo) {
		this.depotNo = depotNo;
	}

	public String getVehicleVin() {
		return vehicleVin;
	}

	public void setVehicleVin(String vehicleVin) {
		this.vehicleVin = vehicleVin;
	}

	public String getVehicleModel() {
		return vehicleModel;
	}

	public void setVehicleModel(String vehicleModel) {
		this.vehicleModel = vehicleModel;
	}

	public String getEngineNum() {
		return engineNum;
	}

	public void setEngineNum(String engineNum) {
		this.engineNum = engineNum;
	}

	public String getDrivingLicenseNo() {
		return drivingLicenseNo;
	}

	public void setDrivingLicenseNo(String drivingLicenseNo) {
		this.drivingLicenseNo = drivingLicenseNo;
	}

	public Long getProductionTime() {
		return productionTime;
	}

	public void setProductionTime(Long productionTime) {
		this.productionTime = productionTime;
	}

	public String getProducingArea() {
		return producingArea;
	}

	public void setProducingArea(String producingArea) {
		this.producingArea = producingArea;
	}

	public BigDecimal getTravelMileage() {
		return travelMileage;
	}

	public void setTravelMileage(BigDecimal travelMileage) {
		this.travelMileage = travelMileage;
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

	public String getWhetherSecondStation() {
		return whetherSecondStation;
	}

	public void setWhetherSecondStation(String whetherSecondStation) {
		this.whetherSecondStation = whetherSecondStation;
	}

	public Long getStartWorkTime() {
		return startWorkTime;
	}

	public void setStartWorkTime(Long startWorkTime) {
		this.startWorkTime = startWorkTime;
	}

	public Long getCompleteWorkTime() {
		return completeWorkTime;
	}

	public void setCompleteWorkTime(Long completeWorkTime) {
		this.completeWorkTime = completeWorkTime;
	}

	public String getAppraiserId() {
		return appraiserId;
	}

	public void setAppraiserId(String appraiserId) {
		this.appraiserId = appraiserId;
	}

	public String getAppraiserName() {
		return appraiserName;
	}

	public void setAppraiserName(String appraiserName) {
		this.appraiserName = appraiserName;
	}

	public String getPromiseGuarantee() {
		return promiseGuarantee;
	}

	public void setPromiseGuarantee(String promiseGuarantee) {
		this.promiseGuarantee = promiseGuarantee;
	}

	public String getApproverIdYt() {
		return approverIdYt;
	}

	public void setApproverIdYt(String approverIdYt) {
		this.approverIdYt = approverIdYt;
	}

	public String getApproverNameYt() {
		return approverNameYt;
	}

	public void setApproverNameYt(String approverNameYt) {
		this.approverNameYt = approverNameYt;
	}

	public String getPolicyApprovalNo() {
		return policyApprovalNo;
	}

	public void setPolicyApprovalNo(String policyApprovalNo) {
		this.policyApprovalNo = policyApprovalNo;
	}

	public String getCostTypePolicy() {
		return costTypePolicy;
	}

	public void setCostTypePolicy(String costTypePolicy) {
		this.costTypePolicy = costTypePolicy;
	}

	public String getCostTypeService() {
		return costTypeService;
	}

	public void setCostTypeService(String costTypeService) {
		this.costTypeService = costTypeService;
	}

	public String getDescribes() {
		return describes;
	}

	public void setDescribes(String describes) {
		this.describes = describes;
	}

	public String getProductNoticeNo() {
		return productNoticeNo;
	}

	public void setProductNoticeNo(String productNoticeNo) {
		this.productNoticeNo = productNoticeNo;
	}

	public String getWhetherYt() {
		return whetherYt;
	}

	public void setWhetherYt(String whetherYt) {
		this.whetherYt = whetherYt;
	}

	public String getFaultDutyCorp() {
		return faultDutyCorp;
	}

	public void setFaultDutyCorp(String faultDutyCorp) {
		this.faultDutyCorp = faultDutyCorp;
	}

	public String getFaultCause() {
		return faultCause;
	}

	public void setFaultCause(String faultCause) {
		this.faultCause = faultCause;
	}

	public String getFaultSystem() {
		return faultSystem;
	}

	public void setFaultSystem(String faultSystem) {
		this.faultSystem = faultSystem;
	}

	public String getFaultAssembly() {
		return faultAssembly;
	}

	public void setFaultAssembly(String faultAssembly) {
		this.faultAssembly = faultAssembly;
	}

	public String getFaultPart() {
		return faultPart;
	}

	public void setFaultPart(String faultPart) {
		this.faultPart = faultPart;
	}

	public String getFaultSchema() {
		return faultSchema;
	}

	public void setFaultSchema(String faultSchema) {
		this.faultSchema = faultSchema;
	}

	public String getFaultDescribe() {
		return faultDescribe;
	}

	public void setFaultDescribe(String faultDescribe) {
		this.faultDescribe = faultDescribe;
	}

	public String getReasonAnalysis() {
		return reasonAnalysis;
	}

	public void setReasonAnalysis(String reasonAnalysis) {
		this.reasonAnalysis = reasonAnalysis;
	}

	public String getDisposeResult() {
		return disposeResult;
	}

	public void setDisposeResult(String disposeResult) {
		this.disposeResult = disposeResult;
	}

	public Long getPartsBuyTime() {
		return partsBuyTime;
	}

	public void setPartsBuyTime(Long partsBuyTime) {
		this.partsBuyTime = partsBuyTime;
	}

	public String getPartsBuyCorp() {
		return partsBuyCorp;
	}

	public void setPartsBuyCorp(String partsBuyCorp) {
		this.partsBuyCorp = partsBuyCorp;
	}

	public String getContainManHourCost() {
		return containManHourCost;
	}

	public void setContainManHourCost(String containManHourCost) {
		this.containManHourCost = containManHourCost;
	}

	public String getPartsCode() {
		return partsCode;
	}

	public void setPartsCode(String partsCode) {
		this.partsCode = partsCode;
	}

	public String getMaterielDescribe() {
		return materielDescribe;
	}

	public void setMaterielDescribe(String materielDescribe) {
		this.materielDescribe = materielDescribe;
	}

	public String getFirstInstallStation() {
		return firstInstallStation;
	}

	public void setFirstInstallStation(String firstInstallStation) {
		this.firstInstallStation = firstInstallStation;
	}

	public String getPartGuaranteePeriod() {
		return partGuaranteePeriod;
	}

	public void setPartGuaranteePeriod(String partGuaranteePeriod) {
		this.partGuaranteePeriod = partGuaranteePeriod;
	}

	public BigDecimal getFeedbackNum() {
		return feedbackNum;
	}

	public void setFeedbackNum(BigDecimal feedbackNum) {
		this.feedbackNum = feedbackNum;
	}

	public String getGooutCause() {
		return gooutCause;
	}

	public void setGooutCause(String gooutCause) {
		this.gooutCause = gooutCause;
	}

	public String getGooutApprover() {
		return gooutApprover;
	}

	public void setGooutApprover(String gooutApprover) {
		this.gooutApprover = gooutApprover;
	}

	public String getGooutPlace() {
		return gooutPlace;
	}

	public void setGooutPlace(String gooutPlace) {
		this.gooutPlace = gooutPlace;
	}

	public String getMeansTraffic() {
		return meansTraffic;
	}

	public void setMeansTraffic(String meansTraffic) {
		this.meansTraffic = meansTraffic;
	}

	public Long getGooutTime() {
		return gooutTime;
	}

	public void setGooutTime(Long gooutTime) {
		this.gooutTime = gooutTime;
	}

	public Long getGooutBackTime() {
		return gooutBackTime;
	}

	public void setGooutBackTime(Long gooutBackTime) {
		this.gooutBackTime = gooutBackTime;
	}

	public BigDecimal getGooutMileage() {
		return gooutMileage;
	}

	public void setGooutMileage(BigDecimal gooutMileage) {
		this.gooutMileage = gooutMileage;
	}

	public Long getGooutPeopleNum() {
		return gooutPeopleNum;
	}

	public void setGooutPeopleNum(Long gooutPeopleNum) {
		this.gooutPeopleNum = gooutPeopleNum;
	}

	public BigDecimal getJourneySubsidy() {
		return journeySubsidy;
	}

	public void setJourneySubsidy(BigDecimal journeySubsidy) {
		this.journeySubsidy = journeySubsidy;
	}

	public BigDecimal getManHourSubsidy() {
		return manHourSubsidy;
	}

	public void setManHourSubsidy(BigDecimal manHourSubsidy) {
		this.manHourSubsidy = manHourSubsidy;
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

	public BigDecimal getServiceSumCost() {
		return serviceSumCost;
	}

	public void setServiceSumCost(BigDecimal serviceSumCost) {
		this.serviceSumCost = serviceSumCost;
	}

	public BigDecimal getTravelCost() {
		return travelCost;
	}

	public void setTravelCost(BigDecimal travelCost) {
		this.travelCost = travelCost;
	}

	public String getInfoStatus() {
		return infoStatus;
	}

	public void setInfoStatus(String infoStatus) {
		this.infoStatus = infoStatus;
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

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
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

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
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

	public String getCustomerProperty() {
		return customerProperty;
	}

	public void setCustomerProperty(String customerProperty) {
		this.customerProperty = customerProperty;
	}

	public String getCustomerAddress() {
		return customerAddress;
	}

	public void setCustomerAddress(String customerAddress) {
		this.customerAddress = customerAddress;
	}

	public String getRepairManId() {
		return repairManId;
	}

	public void setRepairManId(String repairManId) {
		this.repairManId = repairManId;
	}

	public String getRepairManName() {
		return repairManName;
	}

	public void setRepairManName(String repairManName) {
		this.repairManName = repairManName;
	}

	public String getFaultSystemName() {
		return faultSystemName;
	}

	public void setFaultSystemName(String faultSystemName) {
		this.faultSystemName = faultSystemName;
	}

	public String getFaultAssemblyName() {
		return faultAssemblyName;
	}

	public void setFaultAssemblyName(String faultAssemblyName) {
		this.faultAssemblyName = faultAssemblyName;
	}

	public String getFaultPartName() {
		return faultPartName;
	}

	public void setFaultPartName(String faultPartName) {
		this.faultPartName = faultPartName;
	}

	public String getFaultSchemaName() {
		return faultSchemaName;
	}

	public void setFaultSchemaName(String faultSchemaName) {
		this.faultSchemaName = faultSchemaName;
	}

	public String getTrafficFee() {
		return trafficFee;
	}

	public void setTrafficFee(String trafficFee) {
		this.trafficFee = trafficFee;
	}

	public String getOtherItemSumMoneyRemark() {
		return otherItemSumMoneyRemark;
	}

	public void setOtherItemSumMoneyRemark(String otherItemSumMoneyRemark) {
		this.otherItemSumMoneyRemark = otherItemSumMoneyRemark;
	}

	public String getVehicleUseCorpName() {
		return vehicleUseCorpName;
	}

	public void setVehicleUseCorpName(String vehicleUseCorpName) {
		this.vehicleUseCorpName = vehicleUseCorpName;
	}

	public String getVehicleModelClass() {
		return vehicleModelClass;
	}

	public void setVehicleModelClass(String vehicleModelClass) {
		this.vehicleModelClass = vehicleModelClass;
	}

	public String getVehicleModelName() {
		return vehicleModelName;
	}

	public void setVehicleModelName(String vehicleModelName) {
		this.vehicleModelName = vehicleModelName;
	}

	public String getVerifyInfo() {
		return verifyInfo;
	}

	public void setVerifyInfo(String verifyInfo) {
		this.verifyInfo = verifyInfo;
	}

	public Long getRepairCompletionTime() {
		return repairCompletionTime;
	}

	public void setRepairCompletionTime(Long repairCompletionTime) {
		this.repairCompletionTime = repairCompletionTime;
	}

}