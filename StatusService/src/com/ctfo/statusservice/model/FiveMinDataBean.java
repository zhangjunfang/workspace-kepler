package com.ctfo.statusservice.model;

public class FiveMinDataBean {
	//大气压力
	private int airPressureStatus = 2; // 默认值为无效
	
	//冷却液温度
	private int eWaterStatus = 2; // 默认值为无效
	
	//蓄电池电压
	private int extVoltageStatus = 2; // 默认值为无效

	public int getAirPressureStatus() {
		return airPressureStatus;
	}

	public void setAirPressureStatus(int airPressureStatus) {
		this.airPressureStatus = airPressureStatus;
	}

	public int geteWaterStatus() {
		return eWaterStatus;
	}

	public void seteWaterStatus(int eWaterStatus) {
		this.eWaterStatus = eWaterStatus;
	}

	public int getExtVoltageStatus() {
		return extVoltageStatus;
	}

	public void setExtVoltageStatus(int extVoltageStatus) {
		this.extVoltageStatus = extVoltageStatus;
	}
}
