package com.ctfo.monitor.beans;

import java.io.Serializable;

public class UserBehavior implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 5755499279359021586L;
	private long watchTime;//监控时间
	private String onlineType;//在线行为类型
	private String clientAccount;//账号
	private String roleName;//角色
	private String clientAccountOrgid;//所属组织
	private String comName;//公司
	private String setbookName;//帐套
	private String loadIdAddr;//IP地址
	private String clientMac;//mac地址
	public long getWatchTime() {
		return watchTime;
	}
	public void setWatchTime(long watchTime) {
		this.watchTime = watchTime;
	}
	public String getOnlineType() {
		return onlineType;
	}
	public void setOnlineType(String onlineType) {
		this.onlineType = onlineType;
	}
	public String getClientAccount() {
		return clientAccount;
	}
	public void setClientAccount(String clientAccount) {
		this.clientAccount = clientAccount;
	}
	public String getRoleName() {
		return roleName;
	}
	public void setRoleName(String roleName) {
		this.roleName = roleName;
	}
	public String getClientAccountOrgid() {
		return clientAccountOrgid;
	}
	public void setClientAccountOrgid(String clientAccountOrgid) {
		this.clientAccountOrgid = clientAccountOrgid;
	}
	public String getComName() {
		return comName;
	}
	public void setComName(String comName) {
		this.comName = comName;
	}
	public String getSetbookName() {
		return setbookName;
	}
	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}
	public String getLoadIdAddr() {
		return loadIdAddr;
	}
	public void setLoadIdAddr(String loadIdAddr) {
		this.loadIdAddr = loadIdAddr;
	}
	public String getClientMac() {
		return clientMac;
	}
	public void setClientMac(String clientMac) {
		this.clientMac = clientMac;
	}
}
