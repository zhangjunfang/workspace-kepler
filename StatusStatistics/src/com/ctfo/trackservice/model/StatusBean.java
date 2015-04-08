package com.ctfo.trackservice.model;

public class StatusBean {
	private int type = -1;
	private double min = -1;
	private double max = -1;
	private String voltageType ="";//蓄电池电压类型,跟车相关 12V 24V
	
	public int getType() {
		return type;
	}

	public void setType(int type) {
		this.type = type;
	}

	public double getMin() {
		return min;
	}

	public void setMin(double min) {
		this.min = min;
	}

	public double getMax() {
		return max;
	}

	public void setMax(double max) {
		this.max = max;
	}

	public String getVoltageType() {
		return voltageType;
	}

	public void setVoltageType(String voltageType) {
		this.voltageType = voltageType;
	}

}
