package com.ctfo.operation.beans;

import java.util.List;

import com.ctfo.annouce.beans.TbAttachment;

/**
 * 公告管理
 * @author Administrator
 *
 */
public class BulletinManage {
	
	public BulletinManage(){}
	
	private String annoucId;//公告管理id
	private String annoucSubject;//公告标题
	private String mainWord;//关键字
	private long releaseDate;//公告发布日期
	private String receiveCode;//接收方编码
	private String comCode;//公司编码
	private String comName;//公司名称
	private String setbookName;//帐套名称
	private String setbookId;//帐套编码
	private String annouceDeptName;//发布部门名称
	private String annouceDept;//发布部门编码
	private String annoucePeople;//发布人
	private String annoucePeopleId;//发布人编号
	private String annouceContent;//发布内容
	private String annouceStatus;//发布状态0草稿1已发布2待审核3已撤回
	private String createBy;//创建人编辑人
	private long createTime;//创建时间
	private String updateBy;//最后编辑人
	private long updateTime;//最后编辑时间
	private String enableFlag;//删除状态
	
	private String[] attachAliasName;//附件名称
	private String[] attachName;//附件名称
	private String[] attachRemark;//附件备注
	
	private List<TbAttachment> tbAttachment;//附件bean
	
	
	public List<TbAttachment> getTbAttachment() {
		return tbAttachment;
	}
	public void setTbAttachment(List<TbAttachment> tbAttachment) {
		this.tbAttachment = tbAttachment;
	}
	public String[] getAttachAliasName() {
		return attachAliasName;
	}
	public void setAttachAliasName(String[] attachAliasName) {
		this.attachAliasName = attachAliasName;
	}
	public String[] getAttachName() {
		return attachName;
	}
	public void setAttachName(String[] attachName) {
		this.attachName = attachName;
	}
	public String[] getAttachRemark() {
		return attachRemark;
	}
	public void setAttachRemark(String[] attachRemark) {
		this.attachRemark = attachRemark;
	}
	public String getAnnoucId() {
		return annoucId;
	}
	public void setAnnoucId(String annoucId) {
		this.annoucId = annoucId;
	}
	public String getAnnoucSubject() {
		return annoucSubject;
	}
	public void setAnnoucSubject(String annoucSubject) {
		this.annoucSubject = annoucSubject;
	}
	public String getMainWord() {
		return mainWord;
	}
	public void setMainWord(String mainWord) {
		this.mainWord = mainWord;
	}
	public long getReleaseDate() {
		return releaseDate;
	}
	public void setReleaseDate(long releaseDate) {
		this.releaseDate = releaseDate;
	}
	public String getReceiveCode() {
		return receiveCode;
	}
	public void setReceiveCode(String receiveCode) {
		this.receiveCode = receiveCode;
	}
	public String getComCode() {
		return comCode;
	}
	public void setComCode(String comCode) {
		this.comCode = comCode;
	}
	public String getSetbookId() {
		return setbookId;
	}
	public void setSetbookId(String setbookId) {
		this.setbookId = setbookId;
	}
	public String getAnnouceDeptName() {
		return annouceDeptName;
	}
	public void setAnnouceDeptName(String annouceDeptName) {
		this.annouceDeptName = annouceDeptName;
	}
	public String getAnnouceDept() {
		return annouceDept;
	}
	public void setAnnouceDept(String annouceDept) {
		this.annouceDept = annouceDept;
	}
	public String getAnnoucePeople() {
		return annoucePeople;
	}
	public void setAnnoucePeople(String annoucePeople) {
		this.annoucePeople = annoucePeople;
	}
	public String getAnnouceContent() {
		return annouceContent;
	}
	public void setAnnouceContent(String annouceContent) {
		this.annouceContent = annouceContent;
	}
	public String getAnnouceStatus() {
		return annouceStatus;
	}
	public void setAnnouceStatus(String annouceStatus) {
		this.annouceStatus = annouceStatus;
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
	public String getEnableFlag() {
		return enableFlag;
	}
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	public String getComName() {
		return comName;
	}
	public void setComName(String comName) {
		this.comName = comName;
	}
	public String getSetbookName() {
		return setbookName;
	}
	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}
	public String getAnnoucePeopleId() {
		return annoucePeopleId;
	}
	public void setAnnoucePeopleId(String annoucePeopleId) {
		this.annoucePeopleId = annoucePeopleId;
	}
	
	
	

}
