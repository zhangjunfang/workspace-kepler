package com.ctfo.archives.beans;

import java.io.Serializable;

public class Archives implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 5824822605735356811L;
	private String comId;//公司id
	private String comName;//公司名称
	private long registTime;//注册时间
	private long validDate;//注册时间
	private String setbookName;//帐套名称
	private long createTime;//帐套创建时间
	private String total;//客户端用户数
	private String total_s;//服务端用户数
	
	
	public String getComId() {
		return comId;
	}
	public void setComId(String comId) {
		this.comId = comId;
	}
	public String getComName() {
		return comName;
	}
	public void setComName(String comName) {
		this.comName = comName;
	}
	public long getRegistTime() {
		return registTime;
	}
	public void setRegistTime(long registTime) {
		this.registTime = registTime;
	}
	public long getValidDate() {
		return validDate;
	}
	public void setValidDate(long validDate) {
		this.validDate = validDate;
	}
	public String getSetbookName() {
		return setbookName;
	}
	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}
	public long getCreateTime() {
		return createTime;
	}
	public void setCreateTime(long createTime) {
		this.createTime = createTime;
	}
	public String getTotal() {
		return total;
	}
	public void setTotal(String total) {
		this.total = total;
	}
	public String getTotal_s() {
		return total_s;
	}
	public void setTotal_s(String total_s) {
		this.total_s = total_s;
	}
}
