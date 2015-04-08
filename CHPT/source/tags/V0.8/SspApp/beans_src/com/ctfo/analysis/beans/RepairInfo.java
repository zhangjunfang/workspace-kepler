package com.ctfo.analysis.beans;

import java.io.Serializable;

public class RepairInfo implements Serializable{
	private static final long serialVersionUID = 3210440829116158330L;
	
	private String maintainId;//维修单统计ID
	private String comCode;//公司编码，关联字典表
	private String comName;//公司名称
	private String setbookName;//帐套名称
	private String province;//省份，关联字典码表
	private String city;//省份，关联字典码表
	private String county;//市
	private double repairCount;//维修单数量
	private double repairProject;//维修单项目
	private double repairSettlement;//维修结算金额
	private double manHourCost;//工时费用
	private double accessories;//配件数量
	private double accessoriesSettlement;//配件结算
	
	private String startTime;
	private String endTime;
	
	
	private String exportEndTime;


	public String getStartTime() {
		return startTime;
	}


	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}


	public String getEndTime() {
		return endTime;
	}


	public void setEndTime(String endTime) {
		this.endTime = endTime;
	}


	public static long getSerialversionuid() {
		return serialVersionUID;
	}


	public String getMaintainId() {
		return maintainId;
	}


	public void setMaintainId(String maintainId) {
		this.maintainId = maintainId;
	}


	public String getComCode() {
		return comCode;
	}


	public void setComCode(String comCode) {
		this.comCode = comCode;
	}


	public String getComName() {
		return comName;
	}


	public void setComName(String comName) {
		this.comName = comName;
	}


	public String getSetbookName() {
		return setbookName;
	}


	public void setSetbookName(String setbookName) {
		this.setbookName = setbookName;
	}


	public String getProvince() {
		return province;
	}


	public void setProvince(String province) {
		this.province = province;
	}


	public String getCity() {
		return city;
	}


	public void setCity(String city) {
		this.city = city;
	}


	public String getCounty() {
		return county;
	}


	public void setCounty(String county) {
		this.county = county;
	}


	public double getRepairCount() {
		return repairCount;
	}


	public void setRepairCount(double repairCount) {
		this.repairCount = repairCount;
	}


	public double getRepairProject() {
		return repairProject;
	}


	public void setRepairProject(double repairProject) {
		this.repairProject = repairProject;
	}


	public double getRepairSettlement() {
		return repairSettlement;
	}


	public void setRepairSettlement(double repairSettlement) {
		this.repairSettlement = repairSettlement;
	}


	public double getManHourCost() {
		return manHourCost;
	}


	public void setManHourCost(double manHourCost) {
		this.manHourCost = manHourCost;
	}


	public double getAccessories() {
		return accessories;
	}


	public void setAccessories(double accessories) {
		this.accessories = accessories;
	}


	public double getAccessoriesSettlement() {
		return accessoriesSettlement;
	}


	public void setAccessoriesSettlement(double accessoriesSettlement) {
		this.accessoriesSettlement = accessoriesSettlement;
	}


	public String getExportEndTime() {
		return exportEndTime;
	}


	public void setExportEndTime(String exportEndTime) {
		this.exportEndTime = exportEndTime;
	}
}
