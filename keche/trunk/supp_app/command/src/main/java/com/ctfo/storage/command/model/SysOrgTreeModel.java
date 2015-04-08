/**
 * 2014-6-9SysOrgTreeModel.java
 */
package com.ctfo.storage.command.model;

/**
 * SysOrgTreeModel
 * 
 * 
 * @author huangjincheng 2014-6-9下午03:35:05
 * 
 */
public class SysOrgTreeModel {

	/** 组织ID */
	private String entId;

	/** 父组织ID */
	private String parentId;

	/** 分中心ID */
	private String centerCode;

	/** 父id url */
	private String entIdUrl;

	/**
	 * 获取的值
	 * 
	 * @return entId
	 */
	public String getEntId() {
		return entId;
	}

	/**
	 * 设置的值
	 * 
	 * @param entId
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}

	/**
	 * 获取的值
	 * 
	 * @return parentId
	 */
	public String getParentId() {
		return parentId;
	}

	/**
	 * 设置的值
	 * 
	 * @param parentId
	 */
	public void setParentId(String parentId) {
		this.parentId = parentId;
	}

	/**
	 * 获取分中心ID的值
	 * 
	 * @return centerCode
	 */
	public String getCenterCode() {
		return centerCode;
	}

	/**
	 * 设置分中心ID的值
	 * 
	 * @param centerCode
	 */
	public void setCenterCode(String centerCode) {
		this.centerCode = centerCode;
	}

	public String getEntIdUrl() {
		return entIdUrl;
	}

	public void setEntIdUrl(String entIdUrl) {
		this.entIdUrl = entIdUrl;
	}

}
