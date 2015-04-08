package com.ctfo.sas.service.bean;

import java.io.Serializable;

public class MessageResponse implements Serializable {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 3936121535797392827L;

	//接口 返回状态
	private String returnStatus;
	//接口 返回值
	private String returnValue;
	
	public String getReturnStatus() {
		return returnStatus;
	}
	public void setReturnStatus(String returnStatus) {
		this.returnStatus = returnStatus;
	}
	public String getReturnValue() {
		return returnValue;
	}
	public void setReturnValue(String returnValue) {
		this.returnValue = returnValue;
	}
	
}
