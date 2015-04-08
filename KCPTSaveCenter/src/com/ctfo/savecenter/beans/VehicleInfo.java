package com.ctfo.savecenter.beans;

public class VehicleInfo {
	private String vehicleNo = null; //车辆编码
	private String vinCode = null; //车架VIN
	private int entId = -1; // 企业ID
	private String entName = null; // 企业名称
	private int teamId = -1; // 车队ID
	private String teamName = null; // 车队名称
	private String innerCode = ""; // 车辆内部编码
	private String driverName = ""; // 司机名称
	private int driverId = -1; // 司机ID
	private  int vlineId = -1; // 线路ID
	private String vrBrandCode = "";//车辆品牌
	private String rear_axle_rate = ""; // 后桥速比
	private String tyre_r = ""; // 轮胎滚动半径

	public String getRear_axle_rate() {
		return rear_axle_rate;
	}

	public void setRear_axle_rate(String rearAxleRate) {
		rear_axle_rate = rearAxleRate;
	}

	public String getTyre_r() {
		return tyre_r;
	}

	public void setTyre_r(String tyreR) {
		tyre_r = tyreR;
	}

	public String getVrBrandCode() {
		return vrBrandCode;
	}

	public void setVrBrandCode(String vrBrandCode) {
		this.vrBrandCode = vrBrandCode;
	}

	public String getInnerCode() {
		return innerCode;
	}

	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}

	public String getDriverName() {
		return driverName;
	}

	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}

	public int getDriverId() {
		return driverId;
	}

	public void setDriverId(int driverId) {
		this.driverId = driverId;
	}

	public int getVlineId() {
		return vlineId;
	}

	public void setVlineId(int vlineId) {
		this.vlineId = vlineId;
	}

	public int getEntId() {
		return entId;
	}

	public void setEntId(int entId) {
		this.entId = entId;
	}

	public int getTeamId() {
		return teamId;
	}

	public void setTeamId(int teamId) {
		this.teamId = teamId;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	
	public String getEntName() {
		return entName;
	}

	public void setEntName(String entName) {
		this.entName = entName;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
}
