package com.ctfo.mgdb.beans;


/**
 * @author huangjincheng
 *
 */
public class UserInfo {

	private int msgServicePort;
	private String msgServiceAddr;

	private int reConnectTime;

	private String logintype;
	private String userid;
	private String password;
	
	private String fileAddr;

	public int getMsgServicePort() {
		return msgServicePort;
	}

	public void setMsgServicePort(int msgServicePort) {
		this.msgServicePort = msgServicePort;
	}

	public String getMsgServiceAddr() {
		return msgServiceAddr;
	}

	public void setMsgServiceAddr(String msgServiceAddr) {
		this.msgServiceAddr = msgServiceAddr;
	}

	public int getReConnectTime() {
		return reConnectTime;
	}

	public void setReConnectTime(int reConnectTime) {
		this.reConnectTime = reConnectTime;
	}

	public String getLogintype() {
		return logintype;
	}

	public void setLogintype(String logintype) {
		this.logintype = logintype;
	}

	public String getUserid() {
		return userid;
	}

	public void setUserid(String userid) {
		this.userid = userid;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getFileAddr() {
		return fileAddr;
	}

	public void setFileAddr(String fileAddr) {
		this.fileAddr = fileAddr;
	}
	
	


}
