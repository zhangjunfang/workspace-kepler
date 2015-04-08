package com.ctfo.storage.model.member;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 信息反馈记录表<br>
 * 描述： 信息反馈记录表<br>
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
public class TbCustomerserFeedback implements Serializable {

	/** */
	private static final long serialVersionUID = -2127666846563142964L;

	/** 信息反馈记录ID */
	private String feedbackId;

	/** 反馈客户id */
	private String corpId;

	/** 反馈时间 */
	private Long feedbackTime;

	/** 反馈人 */
	private String feedbackBy;

	/** 反馈人电话 */
	private String feedbackPhone;

	/** 反馈类型（关联字典码表反馈类型id） */
	private String feedbackType;

	/** 反馈方式（关联字典码表反馈方式id） */
	private String feedbackMode;

	/** 反馈标题 */
	private String title;

	/** 反馈内容 */
	private String record;

	/** 经办人名称 */
	private String handleName;

	/** 处理人id */
	private String disposeById;

	/** 处理人名称 */
	private String disposeByName;

	/** 处理人部门名称 */
	private String disposeOrg;

	/** 处理时间 */
	private Long disposeTime;

	/** 处理意见 */
	private String disposeIdea;

	/** 审批人id */
	private String approveById;

	/** 审批人名称 */
	private String approveByName;

	/** 审批人意见 */
	private String approveIdea;

	/** 审批时间 */
	private Long approveTime;

	/** 状态（10、待处理，20、处理待审批，21、驳回，30、已处理） */
	private Integer status;

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

	private String handleOrg;

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

	public String getFeedbackId() {
		return feedbackId;
	}

	public void setFeedbackId(String feedbackId) {
		this.feedbackId = feedbackId;
	}

	public String getCorpId() {
		return corpId;
	}

	public void setCorpId(String corpId) {
		this.corpId = corpId;
	}

	public Long getFeedbackTime() {
		return feedbackTime;
	}

	public void setFeedbackTime(Long feedbackTime) {
		this.feedbackTime = feedbackTime;
	}

	public String getFeedbackBy() {
		return feedbackBy;
	}

	public void setFeedbackBy(String feedbackBy) {
		this.feedbackBy = feedbackBy;
	}

	public String getFeedbackPhone() {
		return feedbackPhone;
	}

	public void setFeedbackPhone(String feedbackPhone) {
		this.feedbackPhone = feedbackPhone;
	}

	public String getFeedbackType() {
		return feedbackType;
	}

	public void setFeedbackType(String feedbackType) {
		this.feedbackType = feedbackType;
	}

	public String getFeedbackMode() {
		return feedbackMode;
	}

	public void setFeedbackMode(String feedbackMode) {
		this.feedbackMode = feedbackMode;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getRecord() {
		return record;
	}

	public void setRecord(String record) {
		this.record = record;
	}

	public String getHandleName() {
		return handleName;
	}

	public void setHandleName(String handleName) {
		this.handleName = handleName;
	}

	public String getDisposeById() {
		return disposeById;
	}

	public void setDisposeById(String disposeById) {
		this.disposeById = disposeById;
	}

	public String getDisposeByName() {
		return disposeByName;
	}

	public void setDisposeByName(String disposeByName) {
		this.disposeByName = disposeByName;
	}

	public String getDisposeOrg() {
		return disposeOrg;
	}

	public void setDisposeOrg(String disposeOrg) {
		this.disposeOrg = disposeOrg;
	}

	public Long getDisposeTime() {
		return disposeTime;
	}

	public void setDisposeTime(Long disposeTime) {
		this.disposeTime = disposeTime;
	}

	public String getDisposeIdea() {
		return disposeIdea;
	}

	public void setDisposeIdea(String disposeIdea) {
		this.disposeIdea = disposeIdea;
	}

	public String getApproveById() {
		return approveById;
	}

	public void setApproveById(String approveById) {
		this.approveById = approveById;
	}

	public String getApproveByName() {
		return approveByName;
	}

	public void setApproveByName(String approveByName) {
		this.approveByName = approveByName;
	}

	public String getApproveIdea() {
		return approveIdea;
	}

	public void setApproveIdea(String approveIdea) {
		this.approveIdea = approveIdea;
	}

	public Long getApproveTime() {
		return approveTime;
	}

	public void setApproveTime(Long approveTime) {
		this.approveTime = approveTime;
	}

	public Integer getStatus() {
		return status;
	}

	public void setStatus(Integer status) {
		this.status = status;
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

	public String getHandleOrg() {
		return handleOrg;
	}

	public void setHandleOrg(String handleOrg) {
		this.handleOrg = handleOrg;
	}

}