package com.ctfo.informationser.util;

import java.util.Map;

public class MonitorManagerBean {
	/**
	 * SEQ
	 */
	private String seq;
	/**
	 * MACID(车牌颜色_车牌号)
	 */
	private String macId;
	/**
	 * 通讯方式
	 */
	private String connType;
	/**
	 * 类型
	 */
	private Map<String, String> typeValue;

	/**
	 * 省域编码
	 */
	private String areaID;
	/**
	 * 车牌颜色_车牌号
	 */
	private String vehicle;
	/**
	 * 运营商
	 */
	private String accessCode;
	/**
	 * 业务类型
	 */
	private String type;

	/**
	 * @return the seq
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * @param seq
	 *            the seq to set
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

	/**
	 * @return the macId
	 */
	public String getMacId() {
		return macId;
	}

	/**
	 * @param macId
	 *            the macId to set
	 */
	public void setMacId(String macId) {
		this.macId = macId;
	}

	/**
	 * @return the connType
	 */
	public String getConnType() {
		return connType;
	}

	/**
	 * @param connType
	 *            the connType to set
	 */
	public void setConnType(String connType) {
		this.connType = connType;
	}

	/**
	 * @return the typeValue
	 */
	public Map<String, String> getTypeValue() {
		return typeValue;
	}

	/**
	 * @param typeValue
	 *            the typeValue to set
	 */
	public void setTypeValue(Map<String, String> typeValue) {
		this.typeValue = typeValue;
	}

	/**
	 * @return the areaID
	 */
	public String getAreaID() {
		return areaID;
	}

	/**
	 * @param areaID
	 *            the areaID to set
	 */
	public void setAreaID(String areaID) {
		this.areaID = areaID;
	}

	/**
	 * @return the vehicle
	 */
	public String getVehicle() {
		return vehicle;
	}

	/**
	 * @param vehicle
	 *            the vehicle to set
	 */
	public void setVehicle(String vehicle) {
		this.vehicle = vehicle;
	}

	/**
	 * @return the accessCode
	 */
	public String getAccessCode() {
		return accessCode;
	}

	/**
	 * @param accessCode
	 *            the accessCode to set
	 */
	public void setAccessCode(String accessCode) {
		this.accessCode = accessCode;
	}

	/**
	 * @return the type
	 */
	public String getType() {
		return type;
	}

	/**
	 * @param type the type to set
	 */
	public void setType(String type) {
		this.type = type;
	}


}
