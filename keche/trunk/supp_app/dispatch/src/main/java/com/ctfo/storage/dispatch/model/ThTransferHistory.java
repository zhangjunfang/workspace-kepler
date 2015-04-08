package com.ctfo.storage.dispatch.model;

import java.io.Serializable;



@SuppressWarnings("serial")
public class ThTransferHistory extends BaseModel implements Serializable {

	/** ID */
	private String id = "";

	/** 组ID(源组织ID;源车辆ID) */
	private String transferId = "";

	/** 转组类型(1:车辆转组;2:终端转车) */
	private Integer transferType =-1;

	/** 目标ID(目标组织ID) */
	private String goalId = "";
	
	/** 源ID(组织ID、车辆ID) */
	private String sourceId = "";

	/** 操作时间 */
	private Long opTime =-1l;

	/** 操作人ID */
	private String opId = "";

	/** 原企业名�?/
	private String sourceCorpName;
	
	/** 原车队名�?/
	private String sourceTeamName;
	
	/** 过户企业名称*/
	private String transferCorpName = "";
	
	public String getTransferCorpName() {
		return transferCorpName;
	}

	public void setTransferCorpName(String transferCorpName) {
		this.transferCorpName = transferCorpName;
	}

	/** 过户车队名称*/
	private String transferTeamName;
	
	/** 车牌号码 */
	private String vehicleNo;

	/** 车牌颜色 */
	private String plateColor;

	/** 车辆VIN */
	private String vinCode;

	/** 操作员登录名 */
	private String opLoginname;

	/** 操作员姓�?*/
	private String opName;
	
	/** 操作员所在组织名�?*/
	private String opEntName;

	/** 终端�?*/
	private String tmac;
	
	/** 原车车牌�?*/
	private String sourceVehicleNo;

	/** 原车车牌颜色 */
	private String sourcePlateColor;
	
	/** 转入车车牌号 */
	private String transferVehicleNo;

	/** 转入车车牌颜�?*/
	private String transferPlateColor;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getTransferId() {
		return transferId;
	}

	public void setTransferId(String transferId) {
		this.transferId = transferId;
	}

	public Integer getTransferType() {
		return transferType;
	}

	public void setTransferType(Integer transferType) {
		this.transferType = transferType;
	}

	public String getGoalId() {
		return goalId;
	}

	public void setGoalId(String goalId) {
		this.goalId = goalId;
	}

	public Long getOpTime() {
		return opTime;
	}

	public void setOpTime(Long opTime) {
		this.opTime = opTime;
	}

	public String getOpId() {
		return opId;
	}

	public void setOpId(String opId) {
		this.opId = opId;
	}

	public String getTransferTeamName() {
		return transferTeamName;
	}

	public void setTransferTeamName(String transferTeamName) {
		this.transferTeamName = transferTeamName;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getPlateColor() {
		return plateColor;
	}

	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public String getOpLoginname() {
		return opLoginname;
	}

	public void setOpLoginname(String opLoginname) {
		this.opLoginname = opLoginname;
	}

	public String getOpName() {
		return opName;
	}

	public void setOpName(String opName) {
		this.opName = opName;
	}

	public String getOpEntName() {
		return opEntName;
	}

	public void setOpEntName(String opEntName) {
		this.opEntName = opEntName;
	}

	public String getTmac() {
		return tmac;
	}

	public void setTmac(String tmac) {
		this.tmac = tmac;
	}

	public String getSourceVehicleNo() {
		return sourceVehicleNo;
	}

	public void setSourceVehicleNo(String sourceVehicleNo) {
		this.sourceVehicleNo = sourceVehicleNo;
	}

	public String getSourcePlateColor() {
		return sourcePlateColor;
	}

	public void setSourcePlateColor(String sourcePlateColor) {
		this.sourcePlateColor = sourcePlateColor;
	}

	public String getTransferVehicleNo() {
		return transferVehicleNo;
	}

	public void setTransferVehicleNo(String transferVehicleNo) {
		this.transferVehicleNo = transferVehicleNo;
	}

	public String getTransferPlateColor() {
		return transferPlateColor;
	}

	public void setTransferPlateColor(String transferPlateColor) {
		this.transferPlateColor = transferPlateColor;
	}

	/**
	 * 获取源ID(组织ID、车辆ID)的值
	 * @return sourceId  
	 */
	public String getSourceId() {
		return sourceId;
	}

	/**
	 * 设置源ID(组织ID、车辆ID)的值
	 * @param sourceId
	 */
	public void setSourceId(String sourceId) {
		this.sourceId = sourceId;
	}
	

}