package com.ctfo.storage.model.maintain;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 旧件收/发货单表<br>
 * 描述： 旧件收/发货单表<br>
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
public class TbMaintainOldpartReceivSend implements Serializable {

	/** */
	private static final long serialVersionUID = -541840774065267762L;

	/** 收货信息id */
	private String oldpartId;

	/** 所属公司 */
	private String orgId;

	/** 旧件收/发货单号 */
	private String receiptsNo;

	/** 收/发货日期 */
	private Long receiptTime;

	/** 关联客户信息id */
	private String customerId;

	/** 客户名称 */
	private String customerName;

	/** 客户编码 */
	private String customerCode;

	/** 联系人 */
	private String linkman;

	/** 预约手机 */
	private String linkManMobile;

	/** 区分旧件收/发货单(1|收货单；2|发货单) */
	private String receiptType;

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

	/** 审核意见字段 */
	private String verifyAdvice;

	/** 单据导入状态，0开放、1占用、3锁定，默认状态下为0 */
	private String importStatus;

	public String getOldpartId() {
		return oldpartId;
	}

	public void setOldpartId(String oldpartId) {
		this.oldpartId = oldpartId;
	}

	public String getOrgId() {
		return orgId;
	}

	public void setOrgId(String orgId) {
		this.orgId = orgId;
	}

	public String getReceiptsNo() {
		return receiptsNo;
	}

	public void setReceiptsNo(String receiptsNo) {
		this.receiptsNo = receiptsNo;
	}

	public Long getReceiptTime() {
		return receiptTime;
	}

	public void setReceiptTime(Long receiptTime) {
		this.receiptTime = receiptTime;
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

	public String getReceiptType() {
		return receiptType;
	}

	public void setReceiptType(String receiptType) {
		this.receiptType = receiptType;
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

	public String getVerifyAdvice() {
		return verifyAdvice;
	}

	public void setVerifyAdvice(String verifyAdvice) {
		this.verifyAdvice = verifyAdvice;
	}

	public String getImportStatus() {
		return importStatus;
	}

	public void setImportStatus(String importStatus) {
		this.importStatus = importStatus;
	}

}