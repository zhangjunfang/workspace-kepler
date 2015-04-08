package com.ctfo.analy.beans;

/**
 * 消息包封装
 * 
 * @author yangyi
 * 
 */
public class MessageBean {
	private String command;// 指令
	private String msgid;// 消息服务器id

	private String ip;// 消息服务器ip
	private Short port;// 消息服务器端口

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
