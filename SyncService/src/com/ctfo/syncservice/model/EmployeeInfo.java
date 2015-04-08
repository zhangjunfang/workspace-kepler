package com.ctfo.syncservice.model;

import java.io.Serializable;

public class EmployeeInfo implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	private String id = "";
	
	private String name = "";
	
	private String sex = "";
	
	private String orgCode = "";
	
	private String vin = "";
	/** 从业资格证号 */
	private String qNumber = "";
	
	private String qType ="";
	
	private String CertificateStart ="?";
	
	private String CertificateEnd = "?";
	
	private String tel = "";
	
	private String address = "";

	/**
	 * @return the id
	 */
	public String getId() {
		return id;
	}

	/**
	 * @param id the id to set
	 */
	public void setId(String id) {
		this.id = id;
	}

	/**
	 * @return the name
	 */
	public String getName() {
		return name;
	}

	/**
	 * @param name the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * @return the sex
	 */
	public String getSex() {
		return sex;
	}

	/**
	 * @param sex the sex to set
	 */
	public void setSex(String sex) {
		this.sex = sex;
	}

	/**
	 * @return the orgCode
	 */
	public String getOrgCode() {
		return orgCode;
	}

	/**
	 * @param orgCode the orgCode to set
	 */
	public void setOrgCode(String orgCode) {
		this.orgCode = orgCode;
	}

	/**
	 * @return the vin
	 */
	public String getVin() {
		return vin;
	}

	/**
	 * @param vin the vin to set
	 */
	public void setVin(String vin) {
		this.vin = vin;
	}

	/**
	 * @return the qNumber
	 */
	public String getqNumber() {
		return qNumber;
	}

	/**
	 * @param qNumber the qNumber to set
	 */
	public void setqNumber(String qNumber) {
		this.qNumber = qNumber;
	}

	/**
	 * @return the qType
	 */
	public String getqType() {
		return qType;
	}

	/**
	 * @param qType the qType to set
	 */
	public void setqType(String qType) {
		this.qType = qType;
	}

	/**
	 * @return the certificateStart
	 */
	public String getCertificateStart() {
		return CertificateStart;
	}

	/**
	 * @param certificateStart the certificateStart to set
	 */
	public void setCertificateStart(String certificateStart) {
		CertificateStart = certificateStart;
	}

	/**
	 * @return the certificateEnd
	 */
	public String getCertificateEnd() {
		return CertificateEnd;
	}

	/**
	 * @param certificateEnd the certificateEnd to set
	 */
	public void setCertificateEnd(String certificateEnd) {
		CertificateEnd = certificateEnd;
	}

	/**
	 * @return the tel
	 */
	public String getTel() {
		return tel;
	}

	/**
	 * @param tel the tel to set
	 */
	public void setTel(String tel) {
		this.tel = tel;
	}

	/**
	 * @return the address
	 */
	public String getAddress() {
		return address;
	}

	/**
	 * @param address the address to set
	 */
	public void setAddress(String address) {
		this.address = address;
	}
	
	
}
