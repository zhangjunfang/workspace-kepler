package com.ctfo.mgdb.beans;

/**
 * @author huangjincheng
 *
 */
public class Message {
	private int enable_flag;//0正常，1错误
	private String command;//指令
	private String msgid;//消息服务器id
	
	private String ip;//消息服务器ip
	private Short port;//消息服务器端口
 
	
	
	
	public int getEnable_flag() {
		return enable_flag;
	}
	public void setEnable_flag(int enable_flag) {
		this.enable_flag = enable_flag;
	}
	public String getIp() {
		return ip;
	}
	public void setIp(String ip) {
		this.ip = ip;
	}
	public Short getPort() {
		return port;
	}
	public void setPort(Short port) {
		this.port = port;
	}
	public String getCommand() {
		return command;
	}
	public void setCommand(String command) {
		this.command = command;
	}
	public String getMsgid() {
		return msgid;
	}
	public void setMsgid(String msgid) {
		this.msgid = msgid;
	}
	
	

}

