package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修单<br>
 * 描述： 维修单<br>
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
 * <td>2014-10-28</td>
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
public class TbMaintainInfo implements Serializable {

	/** */
	private static final long serialVersionUID = 1813903894049226451L;

	/** 维修单信息id */
	private String maintainId;

	/** 前置单据ID */
	private String beforeOrderid;

	/** 默认0，0调度草稿、1未派工、2已派工未开工、3已开工、4已完工、5已停工、6已质检未通过、7已质检通过 */
	private String dispatchStatus;

	/** 单据来源,0自建、1预约单、2返修单，默认0自建 */
	private String ordersSource;

	/** 接待时间 */
	private Long receptionTime;

	/** 所属公司 */
	private String orgId;

	/** 车牌号 */
	private String vehicleNo;

	/** 车辆vin */
	private String vehicleVin;

	/** 车辆品牌 */
	private String vehicleBrand;

	/** 车型 */
	private String vehicleModel;

	/** 发动机 */
	private String engineNo;

	/** 司机姓名 */
	private String driverName;

	/** 司机手机 */
	private String driverMobile;

	/** 车辆颜色 */
	private String vehicleColor;

	/** 预约单号 */
	private String maintainNo;

	/** 关联客户信息id */
	private String customerId;

	/** 客户名称 */
	private String customerName;

	/** 客户编码 */
	private String customerCode;

	/** 关联会员信息id */
	private String memberId;

	/** 联系人 */
	private String linkman;

	/** 预约手机 */
	private String linkManMobile;

	/** 维修类别 */
	private String maintainType;

	/** 维修付费方式 */
	private String maintainPayment;

	/** 服务顾问 */
	private String maintainMan;

	/** 进厂油量 */
	private BigDecimal oilIntoFactory;

	/** 行驶里程 */
	private BigDecimal travelMileage;

	/** 预计完工时间 */
	private Long completionTime;

	/** 维修套餐 */
	private String setMeal;

	/** 单据状态 */
	private String infoStatus;

	/** 故障描述 */
	private String faultDescribe;

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

	/** 部门 */
	private String orgName;

	/** 实际完工时间 */
	private Long completeWorkTime;

	/** 建议保养时间 */
	private Long maintainTime;

	/** 建议保养里程 */
	private BigDecimal maintainMileage;

	/** 优惠原因 */
	private String favorableReason;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	private String importStatus;

	public String getMaintainId() {
		return maintainId;
	}

	public void setMaintainId(String maintainId) {
		this.maintainId = maintainId;
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

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
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

	public String getMaintainType() {
		return maintainType;
	}

	public void setMaintainType(String maintainType) {
		this.maintainType = maintainType;
	}

	public String getMaintainPayment() {
		return maintainPayment;
	}

	public void setMaintainPayment(String maintainPayment) {
		this.maintainPayment = maintainPayment;
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

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
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

	public String getImportStatus() {
		return importStatus;
	}

	public void setImportStatus(String importStatus) {
		this.importStatus = importStatus;
	}

}
