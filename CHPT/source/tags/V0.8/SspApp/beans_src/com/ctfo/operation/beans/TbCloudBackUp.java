package com.ctfo.operation.beans;
/**
 * 云备份bean
 * @author Administrator
 *
 */
public class TbCloudBackUp {
	
	public TbCloudBackUp(){}
	
	   private String cloudId;         //云备份表id
	   private String comCode;         //公司编码
	   private String comName;     //公司名称
	   private String setbookId;       //帐套编号
	   private String setbookName; //帐套名称
	   private String cloudSize;       //云空间大小
	   private long cloudValidTime;   //云空间有效期
	   private String usedSpace;       //已用空间
	   private int fileNums;           //文件数目
	   private String remark;           //备注
	   private String createBy;        //创建人
	   private long createTime;        //创建时间(云备份时间)
	   private String enableFlag; //删除状态0正常 1删除
	   
	   private String remainSpace; //可用空间
	public String getCloudId() {
		return cloudId;
	}
	public void setCloudId(String cloudId) {
		this.cloudId = cloudId;
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
	public String getCloudSize() {
		return cloudSize;
	}
	public void setCloudSize(String cloudSize) {
		this.cloudSize = cloudSize;
	}
	public long getCloudValidTime() {
		return cloudValidTime;
	}
	public void setCloudValidTime(long cloudValidTime) {
		this.cloudValidTime = cloudValidTime;
	}
	public String getUsedSpace() {
		return usedSpace;
	}
	public void setUsedSpace(String usedSpace) {
		this.usedSpace = usedSpace;
	}
	public int getFileNums() {
		return fileNums;
	}
	public void setFileNums(int fileNums) {
		this.fileNums = fileNums;
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
	public String getEnableFlag() {
		return enableFlag;
	}
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	public String getRemainSpace() {
		return remainSpace;
	}
	public void setRemainSpace(String remainSpace) {
		this.remainSpace = remainSpace;
	}
	   
}
