package com.kypt.c2pp.inside.msg;

import com.kypt.configuration.C2ppCfg;

/**
 * 应答对象超类
 */
public class InsideMsgResp extends InsideMsg {
	
	/**
	 * 终端厂家编码
	 */
	private String oemId;
	
	/**
	 * 手机号码
	 */
	private String deviceNo;
	
	/**
	 * 通信类型
	 */
	private String commType;
	
	/**
	 * 应答流水号
	 */
	private String respSeqId;
	
	public InsideMsgResp(){
		this.setEncoding(C2ppCfg.props.getProperty("superviseEncoding"));
		this.setOemId(C2ppCfg.props.getProperty("superviseOemId"));
	}

	
	public String getRespSeqId() {
		return respSeqId;
	}

	public void setRespSeqId(String respSeqId) {
		this.respSeqId = respSeqId;
	}

	public String getOemId() {
		return oemId;
	}

	public void setOemId(String oemId) {
		this.oemId = oemId;
	}

	public String getDeviceNo() {
		return deviceNo;
	}

	public void setDeviceNo(String deviceNo) {
		this.deviceNo = deviceNo;
	}

	public String getCommType() {
		return commType;
	}

	public void setCommType(String commType) {
		this.commType = commType;
	}

}