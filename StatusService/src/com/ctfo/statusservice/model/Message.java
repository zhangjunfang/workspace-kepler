package com.ctfo.statusservice.model;

public class Message {
	/**	指令	*/
	private String command;
	/**	消息服务器key	*/
	private String msgKey;
	
	public String getCommand() {
		return command;
	}
	public void setCommand(String command) {
		this.command = command;
	}
	public String getMsgKey() {
		return msgKey;
	}
	public void setMsgKey(String msgKey) {
		this.msgKey = msgKey;
	}
}
