package com.ctfo.version.beans;

import java.io.Serializable;

public class UpExe implements Serializable{

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 3626882295797033125L;
	
	private String flag;      //成功与否
	private String version;   //版本号
	private String address;   //更新文件地址
	private long size;        //更新文件大小
	
	public String getFlag() {
		return flag;
	}
	public void setFlag(String flag) {
		this.flag = flag;
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
	public long getSize() {
		return size;
	}
	public void setSize(long size) {
		this.size = size;
	}
	
}
