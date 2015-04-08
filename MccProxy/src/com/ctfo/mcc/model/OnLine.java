package com.ctfo.mcc.model;

public class OnLine {
	/**	硬件识别码	*/
	private String macId;
	/**	信息服务器编号	*/
	private String msgId;
	/**	状态	*/
	private String status;
	
	public String getMacId() {
		return macId;
	}
	public void setMacId(String macId) {
		this.macId = macId;
	}
	public String getMsgId() {
		return msgId;
	}
	public void setMsgId(String msgId) {
		this.msgId = msgId;
	}
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	
}
