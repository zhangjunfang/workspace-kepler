
package com.ctfo.savecenter.beans;

import java.io.Serializable;

/**
 * 车辆基础信息类，扩展插件使用
 */
public class ServiceUnit implements Serializable {

	private static final long serialVersionUID = 1L;

	/** 服务单元id */
    private long suid;
 
    /** 车辆号码 */
    private String vehicleno;
    
    /** 通讯地址 */
    private String macid;
    
    /** 颜色编号 */
    private String platecolorid;
    
    /**所属地域 */
    private long areacode;
    
	private Long vid = null; // 新加车辆ID
	private String transtypecode;
	
	private String teminalCode = null; //终端编码
	
	private long tid = -1; //终端ID
	
	private String commaddr;//手机号
	
	private Double rearaxlerate;//后桥速比
	private Double tyrer ;//轮胎滚动半径
	
	private String oemcode;

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	public Double getRearaxlerate() {
		return rearaxlerate;
	}

	public void setRearaxlerate(Double rearaxlerate) {
		this.rearaxlerate = rearaxlerate;
	}

	public Double getTyrer() {
		return tyrer;
	}

	public void setTyrer(Double tyrer) {
		this.tyrer = tyrer;
	}

	public String getTranstypecode() {
		return transtypecode;
	}

	public void setTranstypecode(String transtypecode) {
		this.transtypecode = transtypecode;
	}

	public long getSuid() {
		return suid;
	}

	public void setSuid(long suid) {
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

	public long getAreacode() {
		return areacode;
	}

	public void setAreacode(long areacode) {
		this.areacode = areacode;
	}

	public Long getVid() {
		return vid;
	}

	public void setVid(Long vid) {
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

	public long getTid() {
		return tid;
	}

	public void setTid(long tid) {
		this.tid = tid;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getCommaddr() {
		return commaddr;
	}

}
