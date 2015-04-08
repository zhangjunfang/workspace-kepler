package com.ctfo.combusiness.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 信息反馈
 * 
 * @author xuehui
 * 
 */
@SuppressWarnings("serial")
public class TbFeedback extends ETB_Base {

	private Long replyId;

	private String replyType;

	private String replyTheme;

	private String publishStatus;

	private Long publishTime;

	private String replyFlag;

	private Long parentReplyId;

	private Long entId;

	private Long createBy;

	private Long createTime;

	private Long updateBy;

	private Long updateTime;

	private String enableFlag;

	private String replyContent;

	private String createName;

	public Long getReplyId() {
		return replyId;
	}

	public void setReplyId(Long replyId) {
		this.replyId = replyId;
	}

	public String getReplyType() {
		return replyType;
	}

	public void setReplyType(String replyType) {
		this.replyType = replyType;
	}

	public String getReplyTheme() {
		return replyTheme;
	}

	public void setReplyTheme(String replyTheme) {
		this.replyTheme = replyTheme;
	}

	public String getPublishStatus() {
		return publishStatus;
	}

	public void setPublishStatus(String publishStatus) {
		this.publishStatus = publishStatus;
	}

	public Long getPublishTime() {
		return publishTime;
	}

	public void setPublishTime(Long publishTime) {
		this.publishTime = publishTime;
	}

	public String getReplyFlag() {
		return replyFlag;
	}

	public void setReplyFlag(String replyFlag) {
		this.replyFlag = replyFlag;
	}

	public Long getParentReplyId() {
		return parentReplyId;
	}

	public void setParentReplyId(Long parentReplyId) {
		this.parentReplyId = parentReplyId;
	}

	public Long getEntId() {
		return entId;
	}

	public void setEntId(Long entId) {
		this.entId = entId;
	}

	public Long getCreateBy() {
		return createBy;
	}

	public void setCreateBy(Long createBy) {
		this.createBy = createBy;
	}

	public Long getCreateTime() {
		return createTime;
	}

	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}

	public Long getUpdateBy() {
		return updateBy;
	}

	public void setUpdateBy(Long updateBy) {
		this.updateBy = updateBy;
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

	public String getReplyContent() {
		return replyContent;
	}

	public void setReplyContent(String replyContent) {
		this.replyContent = replyContent;
	}

	public String getCreateName() {
		return createName;
	}

	public void setCreateName(String createName) {
		this.createName = createName;
	}

}
