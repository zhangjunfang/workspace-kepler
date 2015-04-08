package com.ctfo.storage.media.model;

import org.apache.mina.core.session.IoSession;

/**
 * ResponseModel
 * 通用应答
 * 
 * 
 * @author huangjincheng
 * 2014-6-12下午03:09:45
 * 
 */
public class ResponseModel {
	/** 连接session*/
	private IoSession session ;
	/** 接收源数据*/
	private String sourceStr ;
	/** 应答指令*/
	private String command = "C001";
	/** 长度*/
	private String length = "00000005";
	/**
	 * 获取连接session的值
	 * @return 连接session  
	 */
	public IoSession getSession() {
		return session;
	}
	/**
	 * 设置连接session的值
	 * @param 连接session
	 */
	public void setSession(IoSession session) {
		this.session = session;
	}
	/**
	 * 获取接收源数据的值
	 * @return sourceStr  
	 */
	public String getSourceStr() {
		return sourceStr;
	}
	/**
	 * 设置接收源数据的值
	 * @param sourceStr
	 */
	public void setSourceStr(String sourceStr) {
		this.sourceStr = sourceStr;
	}
	/**
	 * 获取应答指令的值
	 * @return command  
	 */
	public String getCommand() {
		return command;
	}
	/**
	 * 设置应答指令的值
	 * @param command
	 */
	public void setCommand(String command) {
		this.command = command;
	}
	/**
	 * 获取长度的值
	 * @return length  
	 */
	public String getLength() {
		return length;
	}
	/**
	 * 设置长度的值
	 * @param length
	 */
	public void setLength(String length) {
		this.length = length;
	}
	
	
	
	
}
