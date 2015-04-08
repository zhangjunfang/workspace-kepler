package com.ctfo.storage.init.model;

import java.io.Serializable;


/**
 * TbOrg
 * 
 * 
 * @author huangjincheng
 * 2014-5-14下午04:23:45
 * 
 */
@SuppressWarnings("serial")
public class TbOrg extends BaseModel implements Serializable{
	/** 所属分中心编码*/
	private String centerCode = "";
	/** 有效标记 1:有效 0:无效*/
	private String enableFlag = "1";
	/** 创建人id */
	private String createBy;
	/** 创建时间*/
	private Long createTime = -1L;
	/** 实体ID(主键) */
	private String entId ="";
	/** 实体名称*/
	private String entName = "";
	/** 实体状态：1为正常，0为吊销*/
	private String entState = "";
	/** 实体类型，1为企业，2为车队*/
	private Long entType =-1L;
	/** 是否是总公司（1-是，0-不是）*/
	private Long isCompany =-1L;
	/** 备注*/
	private String memo = "";
	/** 父节点ID，根节点为-1*/
	private String parentId ;
	/** 6位数字序列吗，1开头*/
	private String seqCode = "";
	/** 修改人id*/
	private String updateBy ;
	/** 修改时间*/
	private Long updateTime =-1L;
	/**
	 * 获取所属分中心编码的值
	 * @return centerCode  
	 */
	public String getCenterCode() {
		return centerCode;
	}
	/**
	 * 设置所属分中心编码的值
	 * @param centerCode
	 */
	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}
	/**
	 * 获取有效标记1:有效0:无效的值
	 * @return enableFlag  
	 */
	public String getEnableFlag() {
		return enableFlag;
	}
	/**
	 * 设置有效标记1:有效0:无效的值
	 * @param enableFlag
	 */
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	/**
	 * 获取创建人id的值
	 * @return createBy  
	 */
	public String getCreateBy() {
		return createBy;
	}
	/**
	 * 设置创建人id的值
	 * @param createBy
	 */
	public void setCreateBy(String createBy) {
		this.createBy = createBy;
	}
	/**
	 * 获取创建时间的值
	 * @return createTime  
	 */
	public Long getCreateTime() {
		return createTime;
	}
	/**
	 * 设置创建时间的值
	 * @param createTime
	 */
	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}
	/**
	 * 获取实体ID(主键)的值
	 * @return entId  
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置实体ID(主键)的值
	 * @param entId
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * 获取实体名称的值
	 * @return entName  
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置实体名称的值
	 * @param entName
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * 获取实体状态：1为正常，0为吊销的值
	 * @return entState  
	 */
	public String getEntState() {
		return entState;
	}
	/**
	 * 设置实体状态：1为正常，0为吊销的值
	 * @param entState
	 */
	public void setEntState(String entState) {
		this.entState = entState;
	}
	/**
	 * 获取实体类型，1为企业，2为车队的值
	 * @return entType  
	 */
	public Long getEntType() {
		return entType;
	}
	/**
	 * 设置实体类型，1为企业，2为车队的值
	 * @param entType
	 */
	public void setEntType(Long entType) {
		this.entType = entType;
	}
	/**
	 * 获取是否是总公司（1-是，0-不是）的值
	 * @return isCompany  
	 */
	public Long getIsCompany() {
		return isCompany;
	}
	/**
	 * 设置是否是总公司（1-是，0-不是）的值
	 * @param isCompany
	 */
	public void setIsCompany(Long isCompany) {
		this.isCompany = isCompany;
	}
	/**
	 * 获取备注的值
	 * @return memo  
	 */
	public String getMemo() {
		return memo;
	}
	/**
	 * 设置备注的值
	 * @param memo
	 */
	public void setMemo(String memo) {
		this.memo = memo;
	}
	/**
	 * 获取父节点ID，根节点为-1的值
	 * @return parentId  
	 */
	public String getParentId() {
		return parentId;
	}
	/**
	 * 设置父节点ID，根节点为-1的值
	 * @param parentId
	 */
	public void setParentId(String parentId) {
		this.parentId = parentId;
	}
	/**
	 * 获取6位数字序列吗，1开头的值
	 * @return seqCode  
	 */
	public String getSeqCode() {
		return seqCode;
	}
	/**
	 * 设置6位数字序列吗，1开头的值
	 * @param seqCode
	 */
	public void setSeqCode(String seqCode) {
		this.seqCode = seqCode;
	}
	/**
	 * 获取修改人id的值
	 * @return updateBy  
	 */
	public String getUpdateBy() {
		return updateBy;
	}
	/**
	 * 设置修改人id的值
	 * @param updateBy
	 */
	public void setUpdateBy(String updateBy) {
		this.updateBy = updateBy;
	}
	/**
	 * 获取修改时间的值
	 * @return updateTime  
	 */
	public Long getUpdateTime() {
		return updateTime;
	}
	/**
	 * 设置修改时间的值
	 * @param updateTime
	 */
	public void setUpdateTime(Long updateTime) {
		this.updateTime = updateTime;
	}
	
	
	
	
	
	
	
	
}
