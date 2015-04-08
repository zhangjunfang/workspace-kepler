package com.ctfo.analy.beans;

/**
 * 企业父ID对象
 * 
 * @author yujch
 * 
 */
public class OrgParentInfo {
	private String entId;
	private String entName;
	private String parent;

	public String getEntName() {
		return this.entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getParent() {
		return this.parent;
	}

	public void setParent(String parent) {
		this.parent = parent;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}
	
	public String toString() {
		return "entId=" + this.entId + ",entName=" + this.entName + ",parent=" + this.parent;
	}
}
