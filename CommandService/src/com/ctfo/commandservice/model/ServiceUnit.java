
package com.ctfo.commandservice.model;

import java.io.Serializable;

/**
 * 车辆基础信息
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
    /**	车辆编号 */
	private String vid ;  
	/**终端编码*/
	private String teminalCode; 
	/** 终端ID*/
	private String tid; 
	/** 手机号 */
	private String commaddr; 
	/** 厂商编号 */
	private String oemcode;
	/** 车架号 */
	private String vinCode;
	/** 车队编号 */
	private String teamId;
	/** 车辆类型 */
	private String vehicleType;
	
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
	public String getCommaddr() {
		return commaddr;
	}
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}
	public String getOemcode() {
		return oemcode;
	}
	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public String getTeamId() {
		return teamId;
	}
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	public String getVehicleType() {
		return vehicleType;
	}
	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

}
