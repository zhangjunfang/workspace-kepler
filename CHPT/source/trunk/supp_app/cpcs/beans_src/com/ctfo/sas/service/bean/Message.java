package com.ctfo.sas.service.bean;

import java.io.Serializable;

public class Message implements Serializable{

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = -2683954107590073089L;
	
	//服务站sap代码
	private String serviceStationSap;
	//请求时间
	private String requestTime;
	//单据类型
	private String billType;
	//单据号
	private String billNumber;
	//操作类型
	private String opType;
	
	public String getServiceStationSap() {
		return serviceStationSap;
	}
	public void setServiceStationSap(String serviceStationSap) {
		this.serviceStationSap = serviceStationSap;
	}
	public String getRequestTime() {
		return requestTime;
	}
	public void setRequestTime(String requestTime) {
		this.requestTime = requestTime;
	}
	public String getBillType() {
		return billType;
	}
	public void setBillType(String billType) {
		this.billType = billType;
	}
	public String getBillNumber() {
		return billNumber;
	}
	public void setBillNumber(String billNumber) {
		this.billNumber = billNumber;
	}
	public String getOpType() {
		return opType;
	}
	public void setOpType(String opType) {
		this.opType = opType;
	}

	
}
