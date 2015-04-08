/**
 * 2014-6-4RegisterModel.java
 */
package com.ctfo.storage.command.model;

/**
 * RegisterModel
 * 注册
 * 
 * @author huangjincheng
 * 2014-6-4上午10:59:54
 * 
 */
public class RegisterModel {
	
	/** 终端型号*/
	private String oemCode ;
	
	/** 省域ID*/
	private String provinceId ;
	
	/** 市ID*/
	private String cityId ;
	
	/** 制造商ID*/
	private String manufacturerId ;
	
	/** 车牌号码*/
	private String vehicleNo ;
	
	/** 车牌颜色*/
	private String vehicleColor ;
	
	/** 入库时间*/
	private String utc ;
	
	/** 注册/注销结果（0成功，1车辆已注册，2数据库中无该车辆，3终端已被注册，4数据库中无终端）*/
	private String result ;
	
	/** 注销时间*/
	private String logoffUtc ;
	
	/** 手机号*/
	private String commaddr ;
	
	/** 终端ID*/
	private String tid ;

	/**
	 * 获取终端型号的值
	 * @return oemCode  
	 */
	public String getOemCode() {
		return oemCode;
	}

	/**
	 * 设置终端型号的值
	 * @param oemCode
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	/**
	 * 获取省域ID的值
	 * @return provinceId  
	 */
	public String getProvinceId() {
		return provinceId;
	}

	/**
	 * 设置省域ID的值
	 * @param provinceId
	 */
	public void setProvinceId(String provinceId) {
		this.provinceId = provinceId;
	}

	/**
	 * 获取市ID的值
	 * @return cityId  
	 */
	public String getCityId() {
		return cityId;
	}

	/**
	 * 设置市ID的值
	 * @param cityId
	 */
	public void setCityId(String cityId) {
		this.cityId = cityId;
	}

	/**
	 * 获取制造商ID的值
	 * @return manufacturerId  
	 */
	public String getManufacturerId() {
		return manufacturerId;
	}

	/**
	 * 设置制造商ID的值
	 * @param manufacturerId
	 */
	public void setManufacturerId(String manufacturerId) {
		this.manufacturerId = manufacturerId;
	}

	/**
	 * 获取车牌号码的值
	 * @return vehicleNo  
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}

	/**
	 * 设置车牌号码的值
	 * @param vehicleNo
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	/**
	 * 获取车牌颜色的值
	 * @return vehicleColor  
	 */
	public String getVehicleColor() {
		return vehicleColor;
	}

	/**
	 * 设置车牌颜色的值
	 * @param vehicleColor
	 */
	public void setVehicleColor(String vehicleColor) {
		this.vehicleColor = vehicleColor;
	}

	/**
	 * 获取入库时间的值
	 * @return utc  
	 */
	public String getUtc() {
		return utc;
	}

	/**
	 * 设置入库时间的值
	 * @param utc
	 */
	public void setUtc(String utc) {
		this.utc = utc;
	}

	/**
	 * 获取注册注销结果（0成功，1车辆已注册，2数据库中无该车辆，3终端已被注册，4数据库中无终端）的值
	 * @return result  
	 */
	public String getResult() {
		return result;
	}

	/**
	 * 设置注册注销结果（0成功，1车辆已注册，2数据库中无该车辆，3终端已被注册，4数据库中无终端）的值
	 * @param result
	 */
	public void setResult(String result) {
		this.result = result;
	}

	/**
	 * 获取注销时间的值
	 * @return logoffUtc  
	 */
	public String getLogoffUtc() {
		return logoffUtc;
	}

	/**
	 * 设置注销时间的值
	 * @param logoffUtc
	 */
	public void setLogoffUtc(String logoffUtc) {
		this.logoffUtc = logoffUtc;
	}

	/**
	 * 获取手机号的值
	 * @return commaddr  
	 */
	public String getCommaddr() {
		return commaddr;
	}

	/**
	 * 设置手机号的值
	 * @param commaddr
	 */
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	/**
	 * 获取终端ID的值
	 * @return tid  
	 */
	public String getTid() {
		return tid;
	}

	/**
	 * 设置终端ID的值
	 * @param tid
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}
	
	
	
}
