package com.ctfo.savecenter.beans;

public class Message {
	private String command;//指令
	private String msgid;//消息服务器id
	
	private String ip;//消息服务器ip
	private Short port;//消息服务器端口
 
	
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
	
	public String returnCommand(String command, String alarmid,
			String alarmcode, String status) {

		command=command.replace("CAITS", "CAITR");
		command=command.replace("}", ",130:"+alarmid+",131:"+alarmcode+",132:"+status+"} \r\n");
		return command;
	}

}
