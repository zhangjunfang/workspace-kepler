package com.ctfo.storage.command.model;

/**
 * SysGeneralCodeModel
 * 
 * 
 * @author huangjincheng
 * 2014-6-9下午01:47:19
 * 
 */
public class SysGeneralCodeModel {
	/** */
	private String generalCode ;
	
	/** */
	private String codeName ;
	
	/** */
	private String parentGeneralCode ;

	/**
	 * 获取的值
	 * @return generalCode  
	 */
	public String getGeneralCode() {
		return generalCode;
	}

	/**
	 * 设置的值
	 * @param generalCode
	 */
	public void setGeneralCode(String generalCode) {
		this.generalCode = generalCode;
	}

	/**
	 * 获取的值
	 * @return codeName  
	 */
	public String getCodeName() {
		return codeName;
	}

	/**
	 * 设置的值
	 * @param codeName
	 */
	public void setCodeName(String codeName) {
		this.codeName = codeName;
	}

	/**
	 * 获取的值
	 * @return parentGeneralCode  
	 */
	public String getParentGeneralCode() {
		return parentGeneralCode;
	}

	/**
	 * 设置的值
	 * @param parentGeneralCode
	 */
	public void setParentGeneralCode(String parentGeneralCode) {
		this.parentGeneralCode = parentGeneralCode;
	}
	
	
}
