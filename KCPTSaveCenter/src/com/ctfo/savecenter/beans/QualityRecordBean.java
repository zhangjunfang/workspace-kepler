package com.ctfo.savecenter.beans;
/**
 * 质检单 单项信息
 * @author yujch
 *
 */
public class QualityRecordBean {
	/** 记录ID */
	private String recordId;
	/** vid */
	private String vid;
	/** 手机号 */
	private String commaddr;
	/** VIN_CODE */
	private String vinCode;
	/** 上报时间 */
	private Long utc;
	/** 终端配置 */
	private String terminalConfig;
	/** 特征系数：车速、脉冲比 */
	private int speedPlus;
	/** gps状态 */
	private String gpsState;
	/** gprs强度 */
	private String gprsStrength;
	/** 检测项ID */
	private String paramId;
	/** 检测项值 */
	private String paramValue;
	
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public Long getUtc() {
		return utc;
	}
	public void setUtc(Long utc) {
		this.utc = utc;
	}
	public String getTerminalConfig() {
		return terminalConfig;
	}
	public void setTerminalConfig(String terminalConfig) {
		this.terminalConfig = terminalConfig;
	}
	public int getSpeedPlus() {
		return speedPlus;
	}
	public void setSpeedPlus(int speedPlus) {
		this.speedPlus = speedPlus;
	}
	public String getGpsState() {
		return gpsState;
	}
	public void setGpsState(String gpsState) {
		this.gpsState = gpsState;
	}
	public String getGprsStrength() {
		return gprsStrength;
	}
	public void setGprsStrength(String gprsStrength) {
		this.gprsStrength = gprsStrength;
	}
	public String getParamId() {
		return paramId;
	}
	public void setParamId(String paramId) {
		this.paramId = paramId;
	}
	public String getParamValue() {
		return paramValue;
	}
	public void setParamValue(String paramValue) {
		this.paramValue = paramValue;
	}
	public String getRecordId() {
		return recordId;
	}
	public void setRecordId(String recordId) {
		this.recordId = recordId;
	}
	public String getCommaddr() {
		return commaddr;
	}
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

}
