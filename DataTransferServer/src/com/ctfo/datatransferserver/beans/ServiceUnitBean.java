package com.ctfo.datatransferserver.beans;

import java.io.Serializable;

/**
 * 车辆基础信息类
 */
public class ServiceUnitBean implements Serializable {

	private static final long serialVersionUID = -90138930354921859L;

	private String suid; // 服务单元id
	private String vehicleno; // 车辆号码
	private String macid; // 通讯地址
	private String platecolorid; // 颜色编号
	private String areacode;// 所属地域
	private String vid; // 新加车辆ID
	private String transtypecode;// 行业
	private String teminalCode; // 终端编码
	private String tid; // 终端ID
	private String commaddr;// 手机号
	private String oemcode;// 车机类型码
	private String entid;// 组织结构ID
	
	private long lon;
	private long lat;
	private long utc;
	private int dir;//方向
	
	private String alarmcode;
	private int speed;
	private long alarmutc;
	private int isonline;
	private int state;

	
	public int getState() {
		return state;
	}
	public void setState(int state) {
		this.state = state;
	}

	public String getAlarmcode() {
		return alarmcode;
	}

	public void setAlarmcode(String alarmcode) {
		this.alarmcode = alarmcode;
	}

	public int getSpeed() {
		return speed;
	}

	public void setSpeed(int speed) {
		this.speed = speed;
	}

	public long getAlarmutc() {
		return alarmutc;
	}

	public void setAlarmutc(long alarmutc) {
		this.alarmutc = alarmutc;
	}

	public long getLon() {
		return lon;
	}

	public void setLon(long lon) {
		this.lon = lon;
	}

	public long getLat() {
		return lat;
	}

	public void setLat(long lat) {
		this.lat = lat;
	}

	public long getUtc() {
		return utc;
	}

	public void setUtc(long utc) {
		this.utc = utc;
	}

	public int getDir() {
		return dir;
	}

	public void setDir(int dir) {
		this.dir = dir;
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

	public String getTranstypecode() {
		return transtypecode;
	}

	public void setTranstypecode(String transtypecode) {
		this.transtypecode = transtypecode;
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
	public String getEntid() {
		return entid;
	}
	public void setEntid(String entid) {
		this.entid = entid;
	}

	@Override
	public String toString() {

		return "ServiceUnit[serialVersionUID=" + serialVersionUID + ",suid="
				+ suid + ",vehicleno=" + vehicleno + ",macid=" + macid
				+ ",platecolorid=" + platecolorid + ",areacode=" + areacode
				+ ",vid=" + vid + ",transtypecode=" + transtypecode
				+ ",teminalCode=" + teminalCode + ",tid=" + tid + ",commaddr="
				+ commaddr + ",oemcode=" + oemcode + ",entid=" + entid + "]";
	}

	public int getIsonline() {
		return isonline;
	}

	public void setIsonline(int isonline) {
		if(isonline == 1){
			state = 3;
		} 
		if (isonline == 0){
			state = 0;
		}
		this.isonline = isonline;
	}

	public Object getGpsDataTransferInfo() {
		StringBuffer sb = new StringBuffer();
		sb.append(vid).append(":");				// 车辆编号
		sb.append(vehicleno).append(":");		// 车牌号
		sb.append(999).append(":");				// 运输行业类型	
		sb.append(entid).append(":");			// 企业编号 (权限代码)
		sb.append(lon).append(":");				// 经度  1/600000度
		sb.append(lat).append(":");				// 纬度  1/600000度
		sb.append(utc/1000).append(":");				// 时间
		sb.append(platecolorid).append(":");	// 车牌颜色
		sb.append(-1).append(":");	    		// 报警状态	
		sb.append(dir).append(":");				// 方向   方位角,正北,顺时针0~359
		sb.append(state).append(":");			// 状态 状态( 离线:0; 报警:1; 停驶[<=5]:2 ;在线[<=70]:3; 预警[>70]:4; 满载:5; 半载:6; 空载：7)
		sb.append("0");							// 0：外省  1：本省
		return sb.toString();
	}

}
