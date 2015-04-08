package com.ctfo.commandservice.model;

/**
 *	平台信息
 */
public class PlatformMessage {
	/**	编号	*/
	private String id;
	/**	信息内容	*/
	private String content;
	/**	信息编号	*/
	private String messageId;
	/**	企业运营许可证号	*/
	private String operatingLicense;
	/**	企业运营类型	*/
	private int operatingType;
	/**	系统接收时间	*/
	private long utc;
	/**	操作类型	*/
	private int operateType;
	/**	回复结果	*/
	private String result;
	/**	回复时间	*/
	private long resultUtc;
	/**	操作员	*/
	private String operator;
	/**	省编码	*/
	private String areaId;
	/**	序列号		*/
	private String seq;
	
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getContent() {
		return content;
	}
	public void setContent(String content) {
		this.content = content;
	}
	public String getMessageId() {
		return messageId;
	}
	public void setMessageId(String messageId) {
		this.messageId = messageId;
	}
	public String getOperatingLicense() {
		return operatingLicense;
	}
	public void setOperatingLicense(String operatingLicense) {
		this.operatingLicense = operatingLicense;
	}
	public int getOperatingType() {
		return operatingType;
	}
	public void setOperatingType(int operatingType) {
		this.operatingType = operatingType;
	}
	public long getUtc() {
		return utc;
	}
	public void setUtc(long utc) {
		this.utc = utc;
	}
	public int getOperateType() {
		return operateType;
	}
	public void setOperateType(int operateType) {
		this.operateType = operateType;
	}
	public String getResult() {
		return result;
	}
	public void setResult(String result) {
		this.result = result;
	}
	public long getResultUtc() {
		return resultUtc;
	}
	public void setResultUtc(long resultUtc) {
		this.resultUtc = resultUtc;
	}
	public String getOperator() {
		return operator;
	}
	public void setOperator(String operator) {
		this.operator = operator;
	}
	public String getAreaId() {
		return areaId;
	}
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}
	public String getSeq() {
		return seq;
	}
	public void setSeq(String seq) {
		this.seq = seq;
	}
	
}
