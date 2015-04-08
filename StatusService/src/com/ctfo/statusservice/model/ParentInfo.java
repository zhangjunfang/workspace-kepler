package com.ctfo.statusservice.model;
/**
 *	父组织编号信息
 */
public class ParentInfo {
	/**	组织名称	*/
	private String entName;
	/**	父组织编号	*/
	private String parent;
	
	public String getEntName() {
		return entName;
	}
	public void setEntName(String entName) {
		this.entName = entName;
	}
	public String getParent() {
		return parent;
	}
	public void setParent(String parent) {
		this.parent = parent;
	}
	
	@Override
	public String toString() {
		return "entName=" + this.entName + ",parent=" + this.parent;
	}
}
