
package com.ctfo.statusservice.model;

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
    /**所属地域 */
    private String areacode;
    /** 车辆ID */
	private String vid ;
	/**终端编码*/
	private String teminalCode ; 
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
	/** 车队编号 */
	private String teamName;
	/** 车队编号 */
	private String entId;
	/** 车队编号 */
	private String entName;
	
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
	public String getAreacode() {
		return areacode;
	}
	public void setAreacode(String areacode) {
		this.areacode = areacode;
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
	public String getTeamId() {
		return teamId;
	}
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	public String getTeamName() {
		return teamName;
	}
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	public String getEntId() {
		return entId;
	}
	public void setEntId(String entId) {
		this.entId = entId;
	}
	public String getEntName() {
		return entName;
	}
	public void setEntName(String entName) {
		this.entName = entName;
	}
	
	
	
	@Override
	public String toString() {
		StringBuffer sb = new StringBuffer();
		/** 服务单元id */
		sb.append("suid=");
		sb.append(suid);
		/** 车辆号码 */
		sb.append(", vehicleno=");
		sb.append(vehicleno);
		/** 通讯地址 */
		sb.append(", macid=");
		sb.append(macid);
		/** 颜色编号 */
		sb.append(", plateColor=");
		sb.append(platecolorid);
		/** 所属地域 */
		sb.append(", areacode=");
		sb.append(areacode);
		/** 车辆ID */
		sb.append(", vid=");
		sb.append(vid);
		/** 终端编码 */
		sb.append(", teminalCode=");
		sb.append(teminalCode);
		/** 终端ID */
		sb.append(", tid=");
		sb.append(tid);
		/** 手机号 */
		sb.append(", PhoneNumber=");
		sb.append(commaddr);
		/** 厂商编号 */
		sb.append(", oemcode=");
		sb.append(oemcode);
		/** 车架号 */
		sb.append(", vinCode=");
		sb.append(vinCode);
		/** 车队编号 */
		sb.append(", teamId=");
		sb.append(teamId);
		/** 车队编号 */
		sb.append(", teamName=");
		sb.append(teamName);
		/** 车队编号 */
		sb.append(", entId=");
		sb.append(entId);
		/** 车队编号 */
		sb.append(", entName=");
		sb.append(entName);
		return sb.toString();
	}
	
}
