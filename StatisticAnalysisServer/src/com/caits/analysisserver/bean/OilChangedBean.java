package com.caits.analysisserver.bean;

public class OilChangedBean {
	private String changed_type = "00"; //油量变化状态 注：默认为油位正常
	
	private int addOil = 0; //加油量
	
	private int decreaseOil = 0; //减少量
	
	private int usedOil = 0; //消耗量

	public String getChanged_type() {
		return changed_type;
	}

	public void setChanged_type(String changedType) {
		changed_type = changedType;
	}

	public int getAddOil() {
		return addOil;
	}

	public void addAddOil(int addOil) {
		this.addOil = addOil + this.addOil;
	}

	public int getDecreaseOil() {
		return decreaseOil;
	}

	public void addDecreaseOil(int decreaseOil) {
		this.decreaseOil = decreaseOil + this.decreaseOil;
	}

	public int getUsedOil() {
		return usedOil;
	}

	public void addUsedOil(int usedOil) {
		this.usedOil = usedOil + this.usedOil;
	}
}
