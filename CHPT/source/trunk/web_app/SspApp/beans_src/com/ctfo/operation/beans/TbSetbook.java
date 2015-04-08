package com.ctfo.operation.beans;
/**
 * 
 * @author 于博洋
 *
 * 2014-11-4
 * 帐套bean
 */
public class TbSetbook {
	
	public TbSetbook(){}
	
	private String setbookId;//帐套编码(无意义主键)
	private String setbookName;//公司帐套名称
	private String setbookCode;//帐套代码
	private String comId;//帐套所属公司编码
	private String createBy;//创建人
	private long createTime;//创建时间
	private String updateBy;//最后编辑人
	private long updateTime;//最后编辑时间
	private String status;//状态:0吊销；1启用
	
	
	
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	public String getSetbookId() {
		return setbookId;
	}
	public void setSetbookId(String setbookId) {
		this.setbookId = setbookId;
	}
	public String getSetbookName() {
		return setbookName;
	}
	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}
	public String getSetbookCode() {
		return setbookCode;
	}
	public void setSetbookCode(String setbookCode) {
		this.setbookCode = setbookCode;
	}
	public String getComId() {
		return comId;
	}
	public void setComId(String comId) {
		this.comId = comId;
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
