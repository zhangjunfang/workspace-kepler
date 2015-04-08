package com.ctfo.dataanalysisservice.beans;

/**
 * 用户登录信息
 * 
 * @author yangyi
 * 
 */
public class UserInfo {

	private int msgServicePort;
	private String msgServiceAddr;

	private int reConnectTime;
	private int connectStateTime;

	private String logintype;
	private String userid;
	private String password;

	private String fileAddr;
	private int saveFile;

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

	public int getConnectStateTime() {
		return connectStateTime;
	}

	public void setConnectStateTime(int connectStateTime) {
		this.connectStateTime = connectStateTime;
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

	public void setFileAddr(String fileAddr) {
		this.fileAddr = fileAddr;
	}

	public String getFileAddr() {
		return fileAddr;
	}

	public void setSaveFile(int saveFile) {
		this.saveFile = saveFile;
	}

	public int getSaveFile() {
		return saveFile;
	}

}
