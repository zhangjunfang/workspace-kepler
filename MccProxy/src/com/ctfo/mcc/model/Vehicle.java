package com.ctfo.mcc.model;

public class Vehicle {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌颜色	*/
	private String plateColor;
	/**
	 * 获得车辆编号的值
	 * @return the vid 车辆编号  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获得车牌号的值
	 * @return the plate 车牌号  
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号的值
	 * @param plate 车牌号  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * 获得车牌颜色的值
	 * @return the plateColor 车牌颜色  
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌颜色的值
	 * @param plateColor 车牌颜色  
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	
}
