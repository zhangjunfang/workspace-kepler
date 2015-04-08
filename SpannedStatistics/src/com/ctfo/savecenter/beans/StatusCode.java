package com.ctfo.savecenter.beans;

public class StatusCode {
	private StatusBean terminalStatus = null; // 终端状态
	
	private StatusBean gpsStatus = null; // GPS状态
	
	private StatusBean eWater = null; // 冷却液温度
	
	private StatusBean extVoltage = null; // 蓄电池电压
	
	private StatusBean oilPressure = null; // 油压状态
	
	private StatusBean brakePressure = null; // 制动气压值
	
	private StatusBean brakepadFray = null; // 制动蹄片磨损
	
	private StatusBean oilAram = null; // 燃油告警
	
	private StatusBean absBug = null; // ABS故障状态
	
	private StatusBean coolantLevel = null; // 水位低状态
	
	private StatusBean airFilter = null; // 空滤堵塞
	
	private StatusBean mwereBlocking = null; // 机虑堵塞
	
	private StatusBean fuelBlocking = null; // 燃油堵塞
	
	private StatusBean eoilTemperature = null; // 机油温度
	
	private StatusBean retarerHt = null; // 缓速器高温
	
	private StatusBean ehousing = null; // 仓温高状态

	private StatusBean airPressure = null; //大气压力
	
	public StatusBean getTerminalStatus() {
		return terminalStatus;
	}

	public void setTerminalStatus(StatusBean terminalStatus) {
		this.terminalStatus = terminalStatus;
	}

	public StatusBean getGpsStatus() {
		return gpsStatus;
	}

	public void setGpsStatus(StatusBean gpsStatus) {
		this.gpsStatus = gpsStatus;
	}

	public StatusBean geteWater() {
		return eWater;
	}

	public void seteWater(StatusBean eWater) {
		this.eWater = eWater;
	}

	public StatusBean getExtVoltage() {
		return extVoltage;
	}

	public void setExtVoltage(StatusBean extVoltage) {
		this.extVoltage = extVoltage;
	}

	public StatusBean getOilPressure() {
		return oilPressure;
	}

	public void setOilPressure(StatusBean oilPressure) {
		this.oilPressure = oilPressure;
	}

	public StatusBean getBrakePressure() {
		return brakePressure;
	}

	public void setBrakePressure(StatusBean brakePressure) {
		this.brakePressure = brakePressure;
	}

	public StatusBean getBrakepadFray() {
		return brakepadFray;
	}

	public void setBrakepadFray(StatusBean brakepadFray) {
		this.brakepadFray = brakepadFray;
	}

	public StatusBean getOilAram() {
		return oilAram;
	}

	public void setOilAram(StatusBean oilAram) {
		this.oilAram = oilAram;
	}

	public StatusBean getAbsBug() {
		return absBug;
	}

	public void setAbsBug(StatusBean absBug) {
		this.absBug = absBug;
	}

	public StatusBean getCoolantLevel() {
		return coolantLevel;
	}

	public void setCoolantLevel(StatusBean coolantLevel) {
		this.coolantLevel = coolantLevel;
	}

	public StatusBean getAirFilter() {
		return airFilter;
	}

	public void setAirFilter(StatusBean airFilter) {
		this.airFilter = airFilter;
	}

	public StatusBean getMwereBlocking() {
		return mwereBlocking;
	}

	public void setMwereBlocking(StatusBean mwereBlocking) {
		this.mwereBlocking = mwereBlocking;
	}

	public StatusBean getFuelBlocking() {
		return fuelBlocking;
	}

	public void setFuelBlocking(StatusBean fuelBlocking) {
		this.fuelBlocking = fuelBlocking;
	}

	public StatusBean getEoilTemperature() {
		return eoilTemperature;
	}

	public void setEoilTemperature(StatusBean eoilTemperature) {
		this.eoilTemperature = eoilTemperature;
	}

	public StatusBean getRetarerHt() {
		return retarerHt;
	}

	public void setRetarerHt(StatusBean retarerHt) {
		this.retarerHt = retarerHt;
	}

	public StatusBean getEhousing() {
		return ehousing;
	}

	public void setEhousing(StatusBean ehousing) {
		this.ehousing = ehousing;
	}

	public StatusBean getAirPressure() {
		return airPressure;
	}

	public void setAirPressure(StatusBean airPressure) {
		this.airPressure = airPressure;
	}
}
