package com.ctfo.storage.model.maintain;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修预约单表<br>
 * 描述： 维修预约单表<br>
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
public class TbMaintainReservation implements Serializable {

	/** */
	private static final long serialVersionUID = 6533069471875026042L;

	/** 信息id */
	private String reservId;

	/** 维修单号 */
	private String maintainNo;

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
	private String engineType;

	/** 司机姓名 */
	private String driverName;

	/** 司机手机 */
	private String driverMobile;

	/** 车辆颜色 */
	private String vehicleColor;

	/** 预约单号 */
	private String reservationNo;

	/** 预约日期 */
	private Long reservationDate;

	/** 客户关联id */
	private String customerId;

	/** 客户名称 */
	private String customerName;

	/** 客户编码 */
	private String customerCode;

	/** 联系人 */
	private String linkman;

	/** 联系人手机 */
	private String linkManMobile;

	/** 预约人 */
	private String reservationMan;

	/** 预约手机 */
	private String reservationMobile;

	/** 维修类别 */
	private String maintainType;

	/** 维修付费方式 */
	private String maintainPayment;

	/** 是否接车（1：是；0：否） */
	private String whetherGreet;

	/** 接车地址 */
	private String greetSite;

	/** 预约进厂时间 */
	private Long maintainTime;

	/** 故障描述 */
	private String faultDescribe;

	/** 备注 */
	private String remark;

	/** 创建时间 */
	private Long createTime;

	/** 修改时间 */
	private Long updateTime;

	/** 信息状态（1|激活；2|作废；0|删除） */
	private String enableFlag;

	/** 创建人（制单人） */
	private String createBy;

	/** 创建人姓名 */
	private String createName;

	/** 修改人姓名 */
	private String updateName;

	/** 最后修改人id */
	private String updateBy;

	/** 单据状态(关联码表) */
	private String documentStatus;

	/** 经办人 */
	private String responsibleOpid;

	/** 部门 */
	private String orgName;

	/** 经办人姓名 */
	private String responsibleName;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	public String getReservId() {
		return reservId;
	}

	public void setReservId(String reservId) {
		this.reservId = reservId;
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

	public String getReservationNo() {
		return reservationNo;
	}

	public void setReservationNo(String reservationNo) {
		this.reservationNo = reservationNo;
	}

	public Long getReservationDate() {
		return reservationDate;
	}

	public void setReservationDate(Long reservationDate) {
		this.reservationDate = reservationDate;
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

	public String getLinkManMobile() {
		return linkManMobile;
	}

	public void setLinkManMobile(String linkManMobile) {
		this.linkManMobile = linkManMobile;
	}

	public String getReservationMan() {
		return reservationMan;
	}

	public void setReservationMan(String reservationMan) {
		this.reservationMan = reservationMan;
	}

	public String getReservationMobile() {
		return reservationMobile;
	}

	public void setReservationMobile(String reservationMobile) {
		this.reservationMobile = reservationMobile;
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

	public String getWhetherGreet() {
		return whetherGreet;
	}

	public void setWhetherGreet(String whetherGreet) {
		this.whetherGreet = whetherGreet;
	}

	public String getGreetSite() {
		return greetSite;
	}

	public void setGreetSite(String greetSite) {
		this.greetSite = greetSite;
	}

	public Long getMaintainTime() {
		return maintainTime;
	}

	public void setMaintainTime(Long maintainTime) {
		this.maintainTime = maintainTime;
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

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
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

	public String getOrgName() {
		return orgName;
	}

	public void setOrgName(String orgName) {
		this.orgName = orgName;
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

	public String getMaintainNo() {
		return maintainNo;
	}

	public void setMaintainNo(String maintainNo) {
		this.maintainNo = maintainNo;
	}

}