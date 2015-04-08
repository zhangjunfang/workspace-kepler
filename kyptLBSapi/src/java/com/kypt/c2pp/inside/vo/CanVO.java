package com.kypt.c2pp.inside.vo;

import java.math.BigDecimal;

public class CanVO {
	
	/**
	 * 行驶里程：对应车上里程表读数 1/10Km
	 */
	private String mileage;
	
	/**
	 * 油量：对应车上油量表读数 1/10L
	 */
	private String oilMeasure;
	
	/**
	 * 发动机转速  
	 */
	private String engineRotateSpeed;
	
	/**
	 * 瞬时油耗
	 */
	private String oilInstant;
	
	/**
	 * 发动机扭矩
	 */
	private String eTorque;
	
	/**
	 * 油门踏板位置
	 */
	private String eecApp;
	
	/**
	 * 累计油耗
	 */
	private String oilTotal;
	
	/**
	 * 发动机运行总时长
	 */
	private String engineWorkingLong;
	
	/**
	 * 终端内置电池电压
	 */
	private String batteryVoltage;
	
	/**
	 * 蓄电池电压
	 */
	private String extVoltage;
	
	/**
	 * 发动机水温
	 */
	private String eWaterTemp;
	
	/**
	 * 机油温度
	 */
	private String oilTemperature;
	
	/**
	 * 发动机冷却液温度
	 */
	private String eCoolingTemperature;
	
	/**
	 * 进气温度
	 */
	private String airInflowTpr;
	
	/**
	 * 机油压力
	 */
	private String oilPressure;
	
	/**
	 * 大气压力
	 */
	private String airPressure;
	
	/**
	 * 脉冲车速  行驶记录功能获取的速度 1/10Km/h
	 */
	private String vehicleSpeed;
	
	/**
	 * 速比
	 */
	private String ratio;
	
	/**
	 * 档位
	 */
	private String gears;
	
	/**
	 * 其他信息（车厂扩展的）
	 */
	private String otherCan;

	public String getMileage() {
		return mileage;
	}

	public void setMileage(String mileage) {
		this.mileage = mileage;
	}

	public String getOilMeasure() {
		return oilMeasure;
	}

	public void setOilMeasure(String oilMeasure) {
		this.oilMeasure = oilMeasure;
	}

	public String getEngineRotateSpeed() {
		return engineRotateSpeed;
	}

	public void setEngineRotateSpeed(String engineRotateSpeed) {
		this.engineRotateSpeed = engineRotateSpeed;
	}

	public String getOilInstant() {
		return oilInstant;
	}

	public void setOilInstant(String oilInstant) {
		this.oilInstant = oilInstant;
	}

	public String geteTorque() {
		return eTorque;
	}

	public void seteTorque(String eTorque) {
		this.eTorque = eTorque;
	}

	public String getEecApp() {
		return eecApp;
	}

	public void setEecApp(String eecApp) {
		this.eecApp = eecApp;
	}

	public String getOilTotal() {
		return oilTotal;
	}

	public void setOilTotal(String oilTotal) {
		this.oilTotal = oilTotal;
	}

	public String getEngineWorkingLong() {
		return engineWorkingLong;
	}

	public void setEngineWorkingLong(String engineWorkingLong) {
		this.engineWorkingLong = engineWorkingLong;
	}

	public String getBatteryVoltage() {
		return batteryVoltage;
	}

	public void setBatteryVoltage(String batteryVoltage) {
		this.batteryVoltage = batteryVoltage;
	}

	public String getExtVoltage() {
		return extVoltage;
	}

	public void setExtVoltage(String extVoltage) {
		this.extVoltage = extVoltage;
	}

	public String geteWaterTemp() {
		return eWaterTemp;
	}

	public void seteWaterTemp(String eWaterTemp) {
		this.eWaterTemp = eWaterTemp;
	}

	public String getOilTemperature() {
		return oilTemperature;
	}

	public void setOilTemperature(String oilTemperature) {
		this.oilTemperature = oilTemperature;
	}

	public String geteCoolingTemperature() {
		return eCoolingTemperature;
	}

	public void seteCoolingTemperature(String eCoolingTemperature) {
		this.eCoolingTemperature = eCoolingTemperature;
	}

	public String getAirInflowTpr() {
		return airInflowTpr;
	}

	public void setAirInflowTpr(String airInflowTpr) {
		this.airInflowTpr = airInflowTpr;
	}

	public String getOilPressure() {
		return oilPressure;
	}

	public void setOilPressure(String oilPressure) {
		this.oilPressure = oilPressure;
	}

	public String getAirPressure() {
		return airPressure;
	}

	public void setAirPressure(String airPressure) {
		this.airPressure = airPressure;
	}
	
	public String getVehicleSpeed() {
		return vehicleSpeed;
	}

	public void setVehicleSpeed(String vehicleSpeed) {
		this.vehicleSpeed = vehicleSpeed;
	}

	public String getOtherCan() {
		return otherCan;
	}

	public void setOtherCan(String otherCan) {
		this.otherCan = otherCan;
	}
	
	
	public String toString(){
		StringBuffer sb=new StringBuffer();
		
		if (this.mileage!=null&&this.mileage.length()>0){
			sb.append("9:"+this.mileage+",");
		}
		
		if (this.oilMeasure!=null&&this.oilMeasure.length()>0){
			sb.append("209:"+this.oilMeasure+",");
		}
		
		if (this.engineRotateSpeed!=null&&this.engineRotateSpeed.length()>0){
			sb.append("210:"+this.engineRotateSpeed+",");
		}
		
		if (this.oilInstant!=null&&this.oilInstant.length()>0){
			sb.append("216:"+this.oilInstant+",");
		}
		
		if (this.eTorque!=null&&this.eTorque.length()>0){
			sb.append("503:"+this.eTorque+",");
		}
		
		if (this.eecApp!=null&&this.eecApp.length()>0){
			sb.append("504:"+this.eecApp+",");
		}
		
		if (this.oilTotal!=null&&this.oilTotal.length()>0){
			sb.append("213:"+this.oilTotal+",");
		}
		
		if (this.engineWorkingLong!=null&&this.engineWorkingLong.length()>0){
			sb.append("505:"+this.engineWorkingLong+",");
		}
		
		if (this.batteryVoltage!=null&&this.batteryVoltage.length()>0){
			sb.append("506:"+this.batteryVoltage+",");
		}
		
		if (this.extVoltage!=null&&this.extVoltage.length()>0){
			sb.append("507:"+this.extVoltage+",");
		}
		
		if (this.eWaterTemp!=null&&this.eWaterTemp.length()>0){
			sb.append("214:"+this.eWaterTemp+",");
		}
		
		if (this.oilTemperature!=null&&this.oilTemperature.length()>0){
			sb.append("508:"+this.oilTemperature+",");
		}
		
		if (this.eCoolingTemperature!=null&&this.eCoolingTemperature.length()>0){
			sb.append("509:"+this.eCoolingTemperature+",");
		}
		
		if (this.airInflowTpr!=null&&this.airInflowTpr.length()>0){
			sb.append("510:"+this.airInflowTpr+",");
		}
		
		if (this.oilPressure!=null&&this.oilPressure.length()>0){
			sb.append("215:"+this.oilPressure+",");
		}
		
		if (this.airPressure!=null&&this.airPressure.length()>0){
			sb.append("511:"+this.airPressure+",");
		}
		
		if (this.vehicleSpeed!=null&&this.vehicleSpeed.length()>0){
			sb.append("7:"+this.vehicleSpeed+",");
		}
		
		if (this.otherCan!=null&&this.otherCan.length()>0){
			sb.append("217:"+this.otherCan+",");
		}
		
		return sb.toString();
	}

	public String getRatio() {
		return ratio;
	}

	public void setRatio(String ratio) {
		this.ratio = ratio;
	}

	public String getGears() {
		return gears;
	}

	public void setGears(String gears) {
		this.gears = gears;
	}

}
