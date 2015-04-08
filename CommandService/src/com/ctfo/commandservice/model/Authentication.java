package com.ctfo.commandservice.model;

public class Authentication {
	/**	主键	*/
	private String uuid;
	/**	手机号	*/
	private String phoneNumber;
	/**	鉴权码	*/
	private String authKey;
	/**	结果	*/
	private String result;
	/**	序号	*/
	private String seq;
	/**	系统时间	*/
	private long sysUtc;
	/**	鉴权记录时间	*/
	private long resultUtc;
	/**	厂商编号	*/
	private String oemCode;
	
	
	public String getUuid() {
		return uuid;
	}
	public void setUuid(String uuid) {
		this.uuid = uuid;
	}
	public String getPhoneNumber() {
		return phoneNumber;
	}
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	public String getAuthKey() {
		return authKey;
	}
	public void setAuthKey(String authKey) {
		this.authKey = authKey;
	}
	public String getResult() {
		return result;
	}
	public void setResult(String result) {
		this.result = result;
	}
	public String getSeq() {
		return seq;
	}
	public void setSeq(String seq) {
		this.seq = seq;
	}
	public long getSysUtc() {
		return sysUtc;
	}
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	public long getResultUtc() {
		return resultUtc;
	}
	public void setResultUtc(long resultUtc) {
		this.resultUtc = resultUtc;
	}
	public String getOemCode() {
		return oemCode;
	}
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
	
}
