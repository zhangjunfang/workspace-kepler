/**
 * 
 */
package com.ctfo.storage.media.model;

/**
 * @author zjhl
 *
 */
public class Vehicle {
	/**	手机号	*/
	private String phoneNumber;
	/**	车辆编号	*/
	private String plateId;
	/**	车牌号	*/
	private String plate;
	/**	车辆类型	*/
	private String vehicleType;
	/**
	 * @return 获取 手机号
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号
	 * @param phoneNumber 手机号 
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	/**
	 * @return 获取 车辆编号
	 */
	public String getPlateId() {
		return plateId;
	}
	/**
	 * 设置车辆编号
	 * @param plateId 车辆编号 
	 */
	public void setPlateId(String plateId) {
		this.plateId = plateId;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return 获取 车辆类型
	 */
	public String getVehicleType() {
		return vehicleType;
	}
	/**
	 * 设置车辆类型
	 * @param vehicleType 车辆类型 
	 */
	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}
	
}
