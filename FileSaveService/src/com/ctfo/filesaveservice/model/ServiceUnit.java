
package com.ctfo.filesaveservice.model;

import java.io.Serializable;

/**
 * 车辆基础信息类，扩展插件使用
 */
public class ServiceUnit implements Serializable {

	private static final long serialVersionUID = 1L;
	/** 服务单元id */
    private String suid;
    /** 车辆号码 */
    private String vehicleno;
    /** 通讯地址 */
    private String macid;
    /** 颜色编号 */
    private String platecolorid;
    /** 新加车辆ID */
	private String vid = null; // 车辆ID
	/**终端编码*/
	private String teminalCode = null; //
	/** 终端ID*/
	private String tid; //
	/** 手机号 */
	private String commaddr;//
	/** oemcode */
	private String oemcode;
	/** oemcode */
	private String vinCode;
	
	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	public String getSuid() {
		return suid;
	}

	public void setSuid(String suid) {
		this.suid = suid;
	}

	public String getVehicleno() {
		return vehicleno;
	}

	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}

	public String getMacid() {
		return macid;
	}

	public void setMacid(String macid) {
		this.macid = macid;
	}

	public String getPlatecolorid() {
		return platecolorid;
	}

	public void setPlatecolorid(String platecolorid) {
		this.platecolorid = platecolorid;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public static long getSerialversionuid() {
		return serialVersionUID;
	}

	
	public String getTeminalCode() {
		return teminalCode;
	}

	public void setTeminalCode(String teminalCode) {
		this.teminalCode = teminalCode;
	}

	public String getTid() {
		return tid;
	}

	public void setTid(String tid) {
		this.tid = tid;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getCommaddr() {
		return commaddr;
	}

}
