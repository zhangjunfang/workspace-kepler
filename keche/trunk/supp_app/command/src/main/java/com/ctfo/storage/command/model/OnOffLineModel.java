/**
 * 2014-6-4UpDownModel.java
 */
package com.ctfo.storage.command.model;

/**
 * UpDownModel
 * 上下线
 * 
 * @author huangjincheng
 * 2014-6-4上午10:55:23
 * 
 */
public class OnOffLineModel {
	/** 车辆编号*/
	private String vid ;
	
	/** 时间*/
	private long Utc ;
	
	/** 车牌号*/
	private String vehicleNo ;
	
	/** 上下线标志位*/
	private int code ;

	/**
	 * 获取车辆编号的值
	 * @return vid  
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置车辆编号的值
	 * @param vid
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获取utc的值
	 * @return utc  
	 */
	public long getUtc() {
		return Utc;
	}

	/**
	 * 设置utc的值
	 * @param utc
	 */
	public void setUtc(long utc) {
		Utc = utc;
	}

	/**
	 * 获取车牌号的值
	 * @return vehicleNo  
	 */
	public String getVehicleNo() {
		return vehicleNo;
	}

	/**
	 * 设置车牌号的值
	 * @param vehicleNo
	 */
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	/**
	 * 获取上下线标志位的值
	 * @return code  
	 */
	public int getCode() {
		return code;
	}

	/**
	 * 设置上下线标志位的值
	 * @param code
	 */
	public void setCode(int code) {
		this.code = code;
	}

	
	
}
