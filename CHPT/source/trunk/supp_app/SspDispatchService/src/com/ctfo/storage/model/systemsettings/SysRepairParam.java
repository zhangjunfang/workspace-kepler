package com.ctfo.storage.model.systemsettings;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 维修业务参数<br>
 * 描述： 维修业务参数<br>
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
 * <td>2014-11-5</td>
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
public class SysRepairParam implements Serializable {

	/** */
	private static final long serialVersionUID = 1716623213713433050L;

	/** id */
	private String rParamId;

	/** 启用预约单审核 */
	private String appointmentAudit;

	/** 启用维修单接待审 */
	private String rReceptionAudit;

	/** 启用维修单调度质检 */
	private String rSchedulQualityIns;

	/** 启用维修单结算审核 */
	private String rSettlementAudit;

	/** 启用救援单审核 */
	private String rescueAudit;

	/** 启用返修单审核 */
	private String rReturnAudit;

	/** 启用领料单审核 */
	private String requisitionAudit;

	/** 领料退货单审核 */
	private String materialReturnAudit;

	/** 预约单->维修单接待->维修单调度->维修单结算 */
	private String repailFlow1;

	/** 制单人和编辑人必须为同一人 */
	private String singleEditorsSamePerson;

	/** 制单人和审核人必须为同一人 */
	private String singleAuditSamePerson;

	/** 制单人和作废人必须为同一人 */
	private String singleDisabledSamePerson;

	/** 制单人和删除人必须为同一人 */
	private String singleDeleteSamePerson;

	/** 维修单接待必须导入前置单据生成 */
	private String repairReceptionImportPre;

	/** 返修单必须导入前置单据生成 */
	private String repairReturnImportPre;

	/** 领料单必须导入前置单据生成 */
	private String requisitionImportPre;

	/** 领料退货单必须导入前置单据生成 */
	private String materialReturnImportPre;

	/** 宇通三包服务单必须导入前置单据生成 */
	private String threeServiceImportPreYt;

	/** 旧件入库单必须导入前置单据生成 */
	private String oldPiecesStorageImportPre;

	/** 允许领料数量大于维修单配件数量 */
	private String allowMaterialLargerPartsR;

	/** 领料单自动生成出库单 */
	private String requisitionAutoOutbound;

	/** 创建人，关联人员表 */
	private String createBy;

	/** 创建时间 */
	private Long createTime;

	/** 最后编辑人，关联人员表 */
	private String updateBy;

	/** 最后编辑时间 */
	private Long updateTime;

	/** 服务站id，云平台用 */
	private String serStationId;

	/** 帐套id，云平台用 */
	private String setBookId;

	/** 维修单接待->维修单调度->维修单结算 */
	private String repailFlow2;

	/** 维修单结算 */
	private String repailFlow3;

	/** 启用三包服务单审核 */
	private String threeServiceAudit;

	/** 服务站帐套id */
	private String bookId;

	/** 工时标准 */
	private BigDecimal timeStandards;

	public String getRParamId() {
		return rParamId;
	}

	public void setRParamId(String rParamId) {
		this.rParamId = rParamId;
	}

	public String getAppointmentAudit() {
		return appointmentAudit;
	}

	public void setAppointmentAudit(String appointmentAudit) {
		this.appointmentAudit = appointmentAudit;
	}

	public String getRReceptionAudit() {
		return rReceptionAudit;
	}

	public void setRReceptionAudit(String rReceptionAudit) {
		this.rReceptionAudit = rReceptionAudit;
	}

	public String getRSchedulQualityIns() {
		return rSchedulQualityIns;
	}

	public void setRSchedulQualityIns(String rSchedulQualityIns) {
		this.rSchedulQualityIns = rSchedulQualityIns;
	}

	public String getRSettlementAudit() {
		return rSettlementAudit;
	}

	public void setRSettlementAudit(String rSettlementAudit) {
		this.rSettlementAudit = rSettlementAudit;
	}

	public String getRescueAudit() {
		return rescueAudit;
	}

	public void setRescueAudit(String rescueAudit) {
		this.rescueAudit = rescueAudit;
	}

	public String getRReturnAudit() {
		return rReturnAudit;
	}

	public void setRReturnAudit(String rReturnAudit) {
		this.rReturnAudit = rReturnAudit;
	}

	public String getRequisitionAudit() {
		return requisitionAudit;
	}

	public void setRequisitionAudit(String requisitionAudit) {
		this.requisitionAudit = requisitionAudit;
	}

	public String getMaterialReturnAudit() {
		return materialReturnAudit;
	}

	public void setMaterialReturnAudit(String materialReturnAudit) {
		this.materialReturnAudit = materialReturnAudit;
	}

	public String getRepailFlow1() {
		return repailFlow1;
	}

	public void setRepailFlow1(String repailFlow1) {
		this.repailFlow1 = repailFlow1;
	}

	public String getSingleEditorsSamePerson() {
		return singleEditorsSamePerson;
	}

	public void setSingleEditorsSamePerson(String singleEditorsSamePerson) {
		this.singleEditorsSamePerson = singleEditorsSamePerson;
	}

	public String getSingleAuditSamePerson() {
		return singleAuditSamePerson;
	}

	public void setSingleAuditSamePerson(String singleAuditSamePerson) {
		this.singleAuditSamePerson = singleAuditSamePerson;
	}

	public String getSingleDisabledSamePerson() {
		return singleDisabledSamePerson;
	}

	public void setSingleDisabledSamePerson(String singleDisabledSamePerson) {
		this.singleDisabledSamePerson = singleDisabledSamePerson;
	}

	public String getSingleDeleteSamePerson() {
		return singleDeleteSamePerson;
	}

	public void setSingleDeleteSamePerson(String singleDeleteSamePerson) {
		this.singleDeleteSamePerson = singleDeleteSamePerson;
	}

	public String getRepairReceptionImportPre() {
		return repairReceptionImportPre;
	}

	public void setRepairReceptionImportPre(String repairReceptionImportPre) {
		this.repairReceptionImportPre = repairReceptionImportPre;
	}

	public String getRepairReturnImportPre() {
		return repairReturnImportPre;
	}

	public void setRepairReturnImportPre(String repairReturnImportPre) {
		this.repairReturnImportPre = repairReturnImportPre;
	}

	public String getRequisitionImportPre() {
		return requisitionImportPre;
	}

	public void setRequisitionImportPre(String requisitionImportPre) {
		this.requisitionImportPre = requisitionImportPre;
	}

	public String getMaterialReturnImportPre() {
		return materialReturnImportPre;
	}

	public void setMaterialReturnImportPre(String materialReturnImportPre) {
		this.materialReturnImportPre = materialReturnImportPre;
	}

	public String getThreeServiceImportPreYt() {
		return threeServiceImportPreYt;
	}

	public void setThreeServiceImportPreYt(String threeServiceImportPreYt) {
		this.threeServiceImportPreYt = threeServiceImportPreYt;
	}

	public String getOldPiecesStorageImportPre() {
		return oldPiecesStorageImportPre;
	}

	public void setOldPiecesStorageImportPre(String oldPiecesStorageImportPre) {
		this.oldPiecesStorageImportPre = oldPiecesStorageImportPre;
	}

	public String getAllowMaterialLargerPartsR() {
		return allowMaterialLargerPartsR;
	}

	public void setAllowMaterialLargerPartsR(String allowMaterialLargerPartsR) {
		this.allowMaterialLargerPartsR = allowMaterialLargerPartsR;
	}

	public String getRequisitionAutoOutbound() {
		return requisitionAutoOutbound;
	}

	public void setRequisitionAutoOutbound(String requisitionAutoOutbound) {
		this.requisitionAutoOutbound = requisitionAutoOutbound;
	}

	public String getCreateBy() {
		return createBy;
	}

	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public String getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}

	public Long getUpdateTime() {
		return updateTime;
	}

	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
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

	public String getRepailFlow2() {
		return repailFlow2;
	}

	public void setRepailFlow2(String repailFlow2) {
		this.repailFlow2 = repailFlow2;
	}

	public String getRepailFlow3() {
		return repailFlow3;
	}

	public void setRepailFlow3(String repailFlow3) {
		this.repailFlow3 = repailFlow3;
	}

	public String getThreeServiceAudit() {
		return threeServiceAudit;
	}

	public void setThreeServiceAudit(String threeServiceAudit) {
		this.threeServiceAudit = threeServiceAudit;
	}

	public String getBookId() {
		return bookId;
	}

	public void setBookId(String bookId) {
		this.bookId = bookId;
	}

	public BigDecimal getTimeStandards() {
		return timeStandards;
	}

	public void setTimeStandards(BigDecimal timeStandards) {
		this.timeStandards = timeStandards;
	}

}