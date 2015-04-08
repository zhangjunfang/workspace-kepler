package com.caits.analysisserver.bean;

public class VehicleInfo {
	private String vid;
	private String vehicleNo = null; //车辆编码
	private String vinCode = null; //车架VIN
	private String entId; // 企业ID
	private String entName = null; // 企业名称
	private String teamId; // 车队ID
	private String teamName = null; // 车队名称
	private String innerCode = ""; // 车辆内部编码
	private String driverName = ""; // 司机名称
	private String driverId = null; // 司机ID
	private  String vlineId ; // 线路ID
	private String lineName = null; // 线路名称
	private String vrBrandCode = "";//车辆品牌
	private String commaddr = null;// 手机号码
	private int checkNum = 0; // 车辆鉴权次数
	
	private String rear_axle_rate;
	
	private String tyre_r;
	
	private String vehicleType = null; //车型
	
	private String oemCode;//终端厂家
	
	private String cfgFlag;//配置方案中是否选中精准油耗 1选中 0未选中
	
	private Long maxSpeed;//终端设置的超速阀值

	public String getVehicleType() {
		return vehicleType;
	}

	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

	public int getCheckNum() {
		return checkNum;
	}

	public void setCheckNum(int checkNum) {
		this.checkNum = checkNum;
	}

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

	public String getVlineId() {
		return vlineId;
	}

	public void setVlineId(String vlineId) {
		this.vlineId = vlineId;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getTeamId() {
		return teamId;
	}

	public void setTeamId(String teamId) {
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

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getLineName() {
		return lineName;
	}

	public void setLineName(String lineName) {
		this.lineName = lineName;
	}

	public String getDriverId() {
		return driverId;
	}

	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}

	public String getOemCode() {
		return oemCode;
	}

	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	public String getCfgFlag() {
		return cfgFlag;
	}

	public void setCfgFlag(String cfgFlag) {
		this.cfgFlag = cfgFlag;
	}

	public Long getMaxSpeed() {
		return maxSpeed;
	}

	public void setMaxSpeed(Long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}
	
}
