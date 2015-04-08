package com.ctfo.storage.model.member;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： <br>
 * 描述： <br>
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
public class TbCustomerserCallback implements Serializable {

	/** */
	private static final long serialVersionUID = -8936269200234598283L;

	/** 回访记录ID */
	private String callbackId;

	/** 回访客户id */
	private String callbackCorp;

	/** 回访时间 */
	private Long callbackTime;

	/** 回访类型（关联字典码表回访类型id） */
	private String callbackType;

	/** 回访方式（关联字典码表回访方式id） */
	private String callbackMode;

	/** 回访标题 */
	private String title;

	/** 回访内容 */
	private String record;

	/** 被回访人员名称 */
	private String callbackBy;

	/** 被回访人员部门名称 */
	private String callbackByOrg;

	/** 被回访人电话 */
	private String callbackByPhone;

	/** 被回访人职务名称 */
	private String callbackByDuty;

	/** 经办人名称 */
	private String handleName;

	/** 经办人部门名称 */
	private String handleOrg;

	/** 状态，关联字典码表 */
	private String status;

	/** 数据来源，关联字典码表 */
	private String dataSources;

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

	public String getCallbackId() {
		return callbackId;
	}

	public void setCallbackId(String callbackId) {
		this.callbackId = callbackId;
	}

	public String getCallbackCorp() {
		return callbackCorp;
	}

	public void setCallbackCorp(String callbackCorp) {
		this.callbackCorp = callbackCorp;
	}

	public Long getCallbackTime() {
		return callbackTime;
	}

	public void setCallbackTime(Long callbackTime) {
		this.callbackTime = callbackTime;
	}

	public String getCallbackType() {
		return callbackType;
	}

	public void setCallbackType(String callbackType) {
		this.callbackType = callbackType;
	}

	public String getCallbackMode() {
		return callbackMode;
	}

	public void setCallbackMode(String callbackMode) {
		this.callbackMode = callbackMode;
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

	public String getCallbackBy() {
		return callbackBy;
	}

	public void setCallbackBy(String callbackBy) {
		this.callbackBy = callbackBy;
	}

	public String getCallbackByOrg() {
		return callbackByOrg;
	}

	public void setCallbackByOrg(String callbackByOrg) {
		this.callbackByOrg = callbackByOrg;
	}

	public String getCallbackByPhone() {
		return callbackByPhone;
	}

	public void setCallbackByPhone(String callbackByPhone) {
		this.callbackByPhone = callbackByPhone;
	}

	public String getCallbackByDuty() {
		return callbackByDuty;
	}

	public void setCallbackByDuty(String callbackByDuty) {
		this.callbackByDuty = callbackByDuty;
	}

	public String getHandleName() {
		return handleName;
	}

	public void setHandleName(String handleName) {
		this.handleName = handleName;
	}

	public String getHandleOrg() {
		return handleOrg;
	}

	public void setHandleOrg(String handleOrg) {
		this.handleOrg = handleOrg;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getDataSources() {
		return dataSources;
	}

	public void setDataSources(String dataSources) {
		this.dataSources = dataSources;
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
}