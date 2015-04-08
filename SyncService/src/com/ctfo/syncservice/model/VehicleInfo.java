package com.ctfo.syncservice.model;

import java.io.Serializable;

public class VehicleInfo implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	private String vin = "";
	
	private String plate = "";
	 
	private String color = "";
	
	private String orgCode = "?";
	
	private String manageOrgCode = "?";
	
	private String origin ="";
	
	private String type = "";
	
	private String option = "";
	
	private String transNo = "";
	
	private String businessScope ="";
	
	private String CertificateStart = "?";
	
	private String CertificateEnd = "?";
	/**吨位*/
	private String seatTon ="";
	/**发动机号*/
	private String motorNo = "";
	/**车主姓名*/
	private String owner = "";
	
	private String ownerTel = "";
	
	private String photoParam = "";
	
	private String vedioParam = "";

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
	 * @return the plate
	 */
	public String getPlate() {
		return plate;
	}

	/**
	 * @param plate the plate to set
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}

	/**
	 * @return the color
	 */
	public String getColor() {
		return color;
	}

	/**
	 * @param color the color to set
	 */
	public void setColor(String color) {
		this.color = color;
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
	 * @return the manageOrgCode
	 */
	public String getManageOrgCode() {
		return manageOrgCode;
	}

	/**
	 * @param manageOrgCode the manageOrgCode to set
	 */
	public void setManageOrgCode(String manageOrgCode) {
		this.manageOrgCode = manageOrgCode;
	}

	/**
	 * @return the origin
	 */
	public String getOrigin() {
		return origin;
	}

	/**
	 * @param origin the origin to set
	 */
	public void setOrigin(String origin) {
		this.origin = origin;
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

	/**
	 * @return the option
	 */
	public String getOption() {
		return option;
	}

	/**
	 * @param option the option to set
	 */
	public void setOption(String option) {
		this.option = option;
	}

	/**
	 * @return the transNo
	 */
	public String getTransNo() {
		return transNo;
	}

	/**
	 * @param transNo the transNo to set
	 */
	public void setTransNo(String transNo) {
		this.transNo = transNo;
	}

	/**
	 * @return the businessScope
	 */
	public String getBusinessScope() {
		return businessScope;
	}

	/**
	 * @param businessScope the businessScope to set
	 */
	public void setBusinessScope(String businessScope) {
		this.businessScope = businessScope;
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
	 * @return the seatTon
	 */
	public String getSeatTon() {
		return seatTon;
	}

	/**
	 * @param seatTon the seatTon to set
	 */
	public void setSeatTon(String seatTon) {
		this.seatTon = seatTon;
	}

	/**
	 * @return the owner
	 */
	public String getOwner() {
		return owner;
	}

	/**
	 * @param owner the owner to set
	 */
	public void setOwner(String owner) {
		this.owner = owner;
	}

	/**
	 * @return the ownerTel
	 */
	public String getOwnerTel() {
		return ownerTel;
	}

	/**
	 * @param ownerTel the ownerTel to set
	 */
	public void setOwnerTel(String ownerTel) {
		this.ownerTel = ownerTel;
	}

	/**
	 * @return the photoParam
	 */
	public String getPhotoParam() {
		return photoParam;
	}

	/**
	 * @param photoParam the photoParam to set
	 */
	public void setPhotoParam(String photoParam) {
		this.photoParam = photoParam;
	}

	/**
	 * @return the vedioParam
	 */
	public String getVedioParam() {
		return vedioParam;
	}

	/**
	 * @param vedioParam the vedioParam to set
	 */
	public void setVedioParam(String vedioParam) {
		this.vedioParam = vedioParam;
	}

	/**
	 * @return the motorNo
	 */
	public String getMotorNo() {
		return motorNo;
	}

	/**
	 * @param motorNo the motorNo to set
	 */
	public void setMotorNo(String motorNo) {
		this.motorNo = motorNo;
	}
	
	
}
