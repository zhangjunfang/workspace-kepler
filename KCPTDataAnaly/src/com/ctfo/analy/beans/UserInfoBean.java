package com.ctfo.analy.beans;

/**
 * 用户登录信息
 * 
 * @author yangyi
 * 
 */
public class UserInfoBean {

	private int msgServicePort;//消息服务器端口
	private String msgServiceAddr;//消息服务器IP

	private int reConnectTime;//重连时间
	private int connectStateTime;//连接状态时间

	private String logintype;//登录类型
	private String userid;//登录id
	private String password;//登录密码
 

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
 

}
