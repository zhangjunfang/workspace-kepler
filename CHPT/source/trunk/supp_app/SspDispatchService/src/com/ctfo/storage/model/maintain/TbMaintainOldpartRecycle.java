package com.ctfo.storage.model.maintain;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 车厂旧件返厂单表<br>
 * 描述： 车厂旧件返厂单表<br>
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
public class TbMaintainOldpartRecycle implements Serializable {

	/** */
	private static final long serialVersionUID = -6780431577221917475L;

	/** 返厂单信息id */
	private String returnId;

	/** 返厂单号 */
	private String receiptsNo;

	/** 创建时间开始 */
	private Long createTimeStart;

	/** 创建时间结束 */
	private Long createTimeEnd;

	/** 单据日期 */
	private Long receiptTime;

	/** 备注 */
	private String remarks;

	/** 旧件回收单号 */
	private String oldpartReceiptsNo;

	/** 服务站编码 */
	private String serviceStationCode;

	/** 服务站名称 */
	private String serviceStationName;

	/** 审核意见 */
	private String verifyAdvice;

	/** 单据状态---宇通 */
	private String infoStatusYt;

	/** 单据状态 */
	private String infoStatus;

	/** （宇通单据,从宇通获得字段）创建时间 */
	private Long createTimeYt;

	/** 回厂时间 */
	private Long depotTime;

	/** 确认时间 */
	private Long notarizeTime;

	/** 旧件回收费用 */
	private BigDecimal sumMoney;

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

	public String getReturnId() {
		return returnId;
	}

	public void setReturnId(String returnId) {
		this.returnId = returnId;
	}

	public String getReceiptsNo() {
		return receiptsNo;
	}

	public void setReceiptsNo(String receiptsNo) {
		this.receiptsNo = receiptsNo;
	}

	public Long getCreateTimeStart() {
		return createTimeStart;
	}

	public void setCreateTimeStart(Long createTimeStart) {
		this.createTimeStart = createTimeStart;
	}

	public Long getCreateTimeEnd() {
		return createTimeEnd;
	}

	public void setCreateTimeEnd(Long createTimeEnd) {
		this.createTimeEnd = createTimeEnd;
	}

	public Long getReceiptTime() {
		return receiptTime;
	}

	public void setReceiptTime(Long receiptTime) {
		this.receiptTime = receiptTime;
	}

	public String getRemarks() {
		return remarks;
	}

	public void setRemarks(String remarks) {
		this.remarks = remarks;
	}

	public String getOldpartReceiptsNo() {
		return oldpartReceiptsNo;
	}

	public void setOldpartReceiptsNo(String oldpartReceiptsNo) {
		this.oldpartReceiptsNo = oldpartReceiptsNo;
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

	public String getVerifyAdvice() {
		return verifyAdvice;
	}

	public void setVerifyAdvice(String verifyAdvice) {
		this.verifyAdvice = verifyAdvice;
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

	public Long getCreateTimeYt() {
		return createTimeYt;
	}

	public void setCreateTimeYt(Long createTimeYt) {
		this.createTimeYt = createTimeYt;
	}

	public Long getDepotTime() {
		return depotTime;
	}

	public void setDepotTime(Long depotTime) {
		this.depotTime = depotTime;
	}

	public Long getNotarizeTime() {
		return notarizeTime;
	}

	public void setNotarizeTime(Long notarizeTime) {
		this.notarizeTime = notarizeTime;
	}

	public BigDecimal getSumMoney() {
		return sumMoney;
	}

	public void setSumMoney(BigDecimal sumMoney) {
		this.sumMoney = sumMoney;
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