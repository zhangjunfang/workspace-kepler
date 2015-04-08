package com.ctfo.mcc.model;
/**
 *	MSG用户信息
 */
public class MsgUser {
	/**	登录MSG用户名	*/
	private String msgUserName;
	/**	登录MSG密码	*/
	private String msgPassword;
	/**	MSG组编号	*/
	private String msgGroupId;
	/**	MSG组	*/
	private String msgGroup;
	/**	MSG服务器IP地址	*/
	private String msgHost;
	/**	MSG服务器端口	*/
	private int msgPort;
	/**	MSG服务器登录类型	*/
	private String loginType;
	/**	重连间隔	*/
	private long reConnectTime;
	/**	读空闲时间	*/
	private int readerIdle;
	/**	写空闲时间	*/
	private int writerIdle;
	
	public String getMsgUserName() {
		return msgUserName;
	}
	public void setMsgUserName(String msgUserName) {
		this.msgUserName = msgUserName;
	}
	public String getMsgPassword() {
		return msgPassword;
	}
	public void setMsgPassword(String msgPassword) {
		this.msgPassword = msgPassword;
	}
	public String getMsgGroupId() {
		return msgGroupId;
	}
	public void setMsgGroupId(String msgGroupId) {
		this.msgGroupId = msgGroupId;
	}
	public String getMsgGroup() {
		return msgGroup;
	}
	public void setMsgGroup(String msgGroup) {
		this.msgGroup = msgGroup;
	}
	public String getMsgHost() {
		return msgHost;
	}
	public void setMsgHost(String msgHost) {
		this.msgHost = msgHost;
	}
	public int getMsgPort() {
		return msgPort;
	}
	public void setMsgPort(int msgPort) {
		this.msgPort = msgPort;
	}
	public String getLoginType() {
		return loginType;
	}
	public void setLoginType(String loginType) {
		this.loginType = loginType;
	}
	public long getReConnectTime() {
		return reConnectTime;
	}
	public void setReConnectTime(long reConnectTime) {
		this.reConnectTime = reConnectTime;
	}
	public int getReaderIdle() {
		return readerIdle;
	}
	public void setReaderIdle(int readerIdle) {
		this.readerIdle = readerIdle;
	}
	public int getWriterIdle() {
		return writerIdle;
	}
	public void setWriterIdle(int writerIdle) {
		this.writerIdle = writerIdle;
	}
	
}
