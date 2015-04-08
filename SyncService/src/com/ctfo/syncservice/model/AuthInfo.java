/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncService		</li><br>
 * <li>文件名称：com.ctfo.syncservice.model AuthInfo.java	</li><br>
 * <li>时        间：2013-12-2  上午10:41:15	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.model;

import java.io.Serializable;

/*****************************************
 * <li>描        述：鉴权对象		
 * 
 *****************************************/
public class AuthInfo implements Serializable{
	private static final long serialVersionUID = 1L;
	
	/** 鉴权码 */
	private String akey;
	/** 设备厂家类型编号 */
	private String oemcode;
	/** 车ID */
	private String vid;
	/** 车状态  */
	private String vehicleState;
	/**  车注册状态 */
	private String vehicleRegStatus;
	/** 车牌号  */
	private String vehicleNo;
	/** 车牌颜色  */
	private String plateColor;
	/**  终端号  */
	private String tid;
	/** 手机卡  */
	private String commaddr;
	/** 终端注册状态  */
	private String terRegStatus;
	/** 终端状态  */
	private String terState;
	/** 卡ID */
	private String sid;
	
	public String getAkey() {
		return akey;
	}
	public void setAkey(String akey) {
		this.akey = akey;
	}
	public String getOemcode() {
		return oemcode;
	}
	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getVehicleState() {
		return vehicleState;
	}
	public void setVehicleState(String vehicleState) {
		this.vehicleState = vehicleState;
	}
	public String getVehicleRegStatus() {
		return vehicleRegStatus;
	}
	public void setVehicleRegStatus(String vehicleRegStatus) {
		this.vehicleRegStatus = vehicleRegStatus;
	}
	public String getVehicleNo() {
		return vehicleNo;
	}
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}
	public String getPlateColor() {
		return plateColor;
	}
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
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
	public String getTerRegStatus() {
		return terRegStatus;
	}
	public void setTerRegStatus(String terRegStatus) {
		this.terRegStatus = terRegStatus;
	}
	public String getSid() {
		return sid;
	}
	public void setSid(String sid) {
		this.sid = sid;
	}
	public String getTerState() {
		return terState;
	}
	public void setTerState(String terState) {
		this.terState = terState;
	}
}
