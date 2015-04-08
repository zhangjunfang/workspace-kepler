package com.ctfo.operation.beans;

import java.io.Serializable;

public class AddApp implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 8149553050006167002L;
	private String valueAddId;//增值业务id，与单据设置表关联
	private String bizName;//增值业务名称
	private String authorizationCode;//授权码
	private long validDate;//有效期
	private String comId;//公司编码id，关联公司档案信息表
	private String remark;//备注
	private String createBy;//创建人
	private long createTime;//创建时间
	private String updateBy;//最后编辑人
	private long updateTime;//最后编辑时间
	private String registerAuthentication;//注册鉴权情况
	private String status;//状态
	private String processingStatus;//处理状态
	private String autoId;
	
	
	public String getAutoId() {
		return autoId;
	}
	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}
	public String getRegisterAuthentication() {
		return registerAuthentication;
	}
	public void setRegisterAuthentication(String registerAuthentication) {
		this.registerAuthentication = registerAuthentication;
	}
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	public String getProcessingStatus() {
		return processingStatus;
	}
	public void setProcessingStatus(String processingStatus) {
		this.processingStatus = processingStatus;
	}
	public String getValueAddId() {
		return valueAddId;
	}
	public void setValueAddId(String valueAddId) {
		this.valueAddId = valueAddId;
	}
	public String getBizName() {
		return bizName;
	}
	public void setBizName(String bizName) {
		this.bizName = bizName;
	}
	public String getAuthorizationCode() {
		return authorizationCode;
	}
	public void setAuthorizationCode(String authorizationCode) {
		this.authorizationCode = authorizationCode;
	}
	public long getValidDate() {
		return validDate;
	}
	public void setValidDate(long validDate) {
		this.validDate = validDate;
	}
	public String getComId() {
		return comId;
	}
	public void setComId(String comId) {
		this.comId = comId;
	}
	public String getRemark() {
		return remark;
	}
	public void setRemark(String remark) {
		this.remark = remark;
	}
	public String getCreateBy() {
		return createBy;
	}
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}
	public long getCreateTime() {
		return createTime;
	}
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	public String getUpdateBy() {
		return updateBy;
	}
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	public long getUpdateTime() {
		return updateTime;
	}
	public void setUpdateTime(long updateTime) {
		this.updateTime = updateTime;
	}
}
