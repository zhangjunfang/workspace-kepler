package com.ctfo.informationser.monitoring.beans;

import com.ctfo.local.bean.ETB_Base;

public class ThPlatInfos extends ETB_Base{
	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1756002140781662483L;

	/**
	 * 表唯一标识
	 */
	private String pid;

	/**
	 * 信息内容
	 */
	private String messageContent;

	/**
	 * 信息标识，查岗或报文的ID
	 */
	private String messageId;

	/**
	 * 对象标识，运营商编码或企业码
	 */
	private String objectId;

	/**
	 * 对象类型，0下级平台所属单一平台，1当前连接的下级平台，2下级平台所属单一业户，3下级平台所有业户，4下级平台所有平台，5下级平台所有平台与业户，6下级平台所属所有政府监管平台，7下级平台所有企业监控平台，8下级平台所有经营性平台，9下级平台所有非经营性监控平台
	 */
	private Short objectType;

	/**
	 * 接收时间
	 */
	private Long utc;

	/**
	 * 操作类型 0查岗；1报文
	 */
	private Short opType;

	/**
	 * 回复结果
	 */
	private String result;

	/**
	 * 回复时间
	 */
	private Long resultutc;

	/**
	 * 操作员
	 */
	private String resultOp;
	/**
	 * 省域编码
	 */
	private String areaId;
	/**
	 * 业务标识
	 */
	private String seq;

	/**
	 * @return the pid
	 */
	public String getPid() {
		return pid;
	}

	/**
	 * @param pid
	 *            the pid to set
	 */
	public void setPid(String pid) {
		this.pid = pid;
	}

	/**
	 * @return the messageContent
	 */
	public String getMessageContent() {
		return messageContent;
	}

	/**
	 * @param messageContent
	 *            the messageContent to set
	 */
	public void setMessageContent(String messageContent) {
		this.messageContent = messageContent;
	}

	/**
	 * @return the messageId
	 */
	public String getMessageId() {
		return messageId;
	}

	/**
	 * @param messageId
	 *            the messageId to set
	 */
	public void setMessageId(String messageId) {
		this.messageId = messageId;
	}

	/**
	 * @return the objectId
	 */
	public String getObjectId() {
		return objectId;
	}

	/**
	 * @param objectId
	 *            the objectId to set
	 */
	public void setObjectId(String objectId) {
		this.objectId = objectId;
	}

	/**
	 * @return the objectType
	 */
	public Short getObjectType() {
		return objectType;
	}

	/**
	 * @param objectType
	 *            the objectType to set
	 */
	public void setObjectType(Short objectType) {
		this.objectType = objectType;
	}

	/**
	 * @return the utc
	 */
	public Long getUtc() {
		return utc;
	}

	/**
	 * @param utc
	 *            the utc to set
	 */
	public void setUtc(Long utc) {
		this.utc = utc;
	}

	/**
	 * @return the opType
	 */
	public Short getOpType() {
		return opType;
	}

	/**
	 * @param opType
	 *            the opType to set
	 */
	public void setOpType(Short opType) {
		this.opType = opType;
	}

	/**
	 * @return the result
	 */
	public String getResult() {
		return result;
	}

	/**
	 * @param result
	 *            the result to set
	 */
	public void setResult(String result) {
		this.result = result;
	}

	/**
	 * @return the resultutc
	 */
	public Long getResultutc() {
		return resultutc;
	}

	/**
	 * @param resultutc
	 *            the resultutc to set
	 */
	public void setResultutc(Long resultutc) {
		this.resultutc = resultutc;
	}

	/**
	 * @return the resultOp
	 */
	public String getResultOp() {
		return resultOp;
	}

	/**
	 * @param resultOp
	 *            the resultOp to set
	 */
	public void setResultOp(String resultOp) {
		this.resultOp = resultOp;
	}

	/**
	 * @return the areaId
	 */
	public String getAreaId() {
		return areaId;
	}

	/**
	 * @param areaId the areaId to set
	 */
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}

	/**
	 * @return the seq
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * @param seq the seq to set
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

}