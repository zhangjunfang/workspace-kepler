package com.ctfo.version.beans;

import java.io.Serializable;

public class Version implements Serializable{

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5093987554872117400L;

	private String id;  //ID
	private String version; //版本号
	private String address; //升级包地址
	private String versionType;    //版本类型
	private Long createTime;
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getVersion() {
		return version;
	}
	public void setVersion(String version) {
		this.version = version;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getVersionType() {
		return versionType;
	}
	public void setVersionType(String versionType) {
		this.versionType = versionType;
	}
	public Long getCreateTime() {
		return createTime;
	}
	public void setCreateTime(Long createTime) {
		this.createTime = createTime;
	}       
	
}
