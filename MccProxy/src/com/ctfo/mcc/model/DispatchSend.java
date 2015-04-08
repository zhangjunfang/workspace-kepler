package com.ctfo.mcc.model;

public class DispatchSend {
	/**	在线状态	*/
	private boolean online;
	/**	指令内容	*/
	private String command;
	/**
	 * 获得在线状态的值
	 * @return the online 在线状态  
	 */
	public boolean isOnline() {
		return online;
	}
	/**
	 * 设置在线状态的值
	 * @param online 在线状态  
	 */
	public void setOnline(boolean online) {
		this.online = online;
	}
	/**
	 * 获得指令内容的值
	 * @return the command 指令内容  
	 */
	public String getCommand() {
		return command;
	}
	/**
	 * 设置指令内容的值
	 * @param command 指令内容  
	 */
	public void setCommand(String command) {
		this.command = command;
	}
	
	public String toString(){
		return new StringBuffer("online:[").append(online).append("], command:[").append(command).append("]").toString();
	}
}
