package com.ctfo.syncservice.model;

import java.io.Serializable;

/*****************************************
 * <li>描        述：组织		
 * 
 *****************************************/
public class Organization implements Serializable {
	private static final long serialVersionUID = 1L;
	/** 企业编号	 */
	private String entId;
	/** 企业名称 	*/
	private String entName;
	
	public String getEntId() {
		return entId;
	}
	public void setEntId(String entId) {
		this.entId = entId;
	}
	public String getEntName() {
		return entName;
	}
	public void setEntName(String entName) {
		this.entName = entName;
	}
}
