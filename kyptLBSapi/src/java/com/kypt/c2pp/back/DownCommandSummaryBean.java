package com.kypt.c2pp.back;

public class DownCommandSummaryBean {
	/**
	 * 下行消息命令字
	 */
	private String command;
	/**
	 * 下行消息ID
	 */
	private String seqId;
	
	/**
	 * 下行服务器地址
	 */
	private String address;
	
	/**
	 * 回复消息超时时间 unit:s 默认3s
	 */
	private int timeout=3;

	public String getSeqId() {
		return seqId;
	}

	public void setSeqId(String seqId) {
		this.seqId = seqId;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public int getTimeout() {
		return timeout;
	}

	public void setTimeout(int timeout) {
		this.timeout = timeout;
	}

	public String getCommand() {
		return command;
	}

	public void setCommand(String command) {
		this.command = command;
	}

}
