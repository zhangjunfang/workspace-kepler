package com.ctfo.storage.model.maintain;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 领料退货单信息表<br>
 * 描述： 领料退货单信息表<br>
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
public class TbMaintainRefundMaterial implements Serializable {

	/** */
	private static final long serialVersionUID = -2596483846268716888L;

	/** 领料信息id */
	private String refundId;

	/** 所属公司 */
	private String orgId;

	/** 领料退料单号 */
	private String refundNo;

	/** 退料时间 */
	private Long refundTime;

	/** 领料时间 */
	private Long packingTime;

	/** 车辆品牌 */
	private String vehicleBrand;

	/** 适用车型 */
	private String vehicleModel;

	/** 客户名称 */
	private String customerName;

	/** 客户编码 */
	private String customerCode;

	/** 联系人 */
	private String linkman;

	/** 联系人手机 */
	private String linkManMobile;

	/** 审核意见 */
	private String verifyAdvice;

	/** 领料单号 */
	private String materialNo;

	/** 退料人 */
	private String refundOpid;

	/** 关联领料单id */
	private String fetchId;

	/** 领料人 */
	private String fetchOpid;

	/** 备注 */
	private String remarks;

	/** 单据状态 */
	private String infoStatus;

	/** 信息状态（1|有效；0|删除） */
	private String enableFlag;

	/** 创建时间 */
	private Long createTime;

	/** 修改时间 */
	private Long updateTime;

	/** 创建人 */
	private String createBy;

	/** 最后修改人 */
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

	public String getRefundId() {
		return refundId;
	}

	public void setRefundId(String refundId) {
		this.refundId = refundId;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
	}

	public String getRefundNo() {
		return refundNo;
	}

	public void setRefundNo(String refundNo) {
		this.refundNo = refundNo;
	}

	public Long getRefundTime() {
		return refundTime;
	}

	public void setRefundTime(Long refundTime) {
		this.refundTime = refundTime;
	}

	public Long getPackingTime() {
		return packingTime;
	}

	public void setPackingTime(Long packingTime) {
		this.packingTime = packingTime;
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

	public String getVerifyAdvice() {
		return verifyAdvice;
	}

	public void setVerifyAdvice(String verifyAdvice) {
		this.verifyAdvice = verifyAdvice;
	}

	public String getMaterialNo() {
		return materialNo;
	}

	public void setMaterialNo(String materialNo) {
		this.materialNo = materialNo;
	}

	public String getRefundOpid() {
		return refundOpid;
	}

	public void setRefundOpid(String refundOpid) {
		this.refundOpid = refundOpid;
	}

	public String getFetchId() {
		return fetchId;
	}

	public void setFetchId(String fetchId) {
		this.fetchId = fetchId;
	}

	public String getFetchOpid() {
		return fetchOpid;
	}

	public void setFetchOpid(String fetchOpid) {
		this.fetchOpid = fetchOpid;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public String getInfoStatus() {
		return infoStatus;
	}

	public void setInfoStatus(String infoStatus) {
		this.infoStatus = infoStatus;
	}

	public String getEnableFlag() {
		return enableFlag;
	}

	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
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

}