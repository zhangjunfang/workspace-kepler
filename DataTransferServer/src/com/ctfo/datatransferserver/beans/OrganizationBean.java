package com.ctfo.datatransferserver.beans;

import java.io.Serializable;

/**
 * 组织结构
 * 
 * @author yangyi
 * 
 */
public class OrganizationBean implements Serializable {

	private static final long serialVersionUID = -2876731610712860229L;
	private String entid;// 组织结构ID
	private Integer level;// 级别
	private Integer enttype;// 类型

	public String getEntid() {
		return entid;
	}

	public void setEntid(String entid) {
		this.entid = entid;
	}

	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

	public Integer getEnttype() {
		return enttype;
	}

	public void setEnttype(Integer enttype) {
		this.enttype = enttype;
	}

	@Override
	public String toString() {
		return "OrganizationBean[entid=" + entid + ",level=" + level
				+ ",enttype=" + enttype + "]";
	}

}
