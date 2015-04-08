package com.ctfo.datatransferserver.beans;

import java.io.Serializable;

/**
 * 命令解析后车辆上报数据
 * 
 * @author yangyi
 * 
 */
public class VehiclePolymerizeBean implements Serializable {

	private static final long serialVersionUID = 5528583082446904163L;
	private String vid;// 车辆唯一标识
	private String vehicleno;// 车牌号
	private String transtypecode = "-1";// 行业编码
	private String nativetareacode;// 籍贯地编码
	private long lon;// 经度
	private long lat;// 纬度
	private long utc;// 定位时间
	private String platecolorid;// 车牌颜色
	private String isnative = "1";// 是否本地车
	private String alarmcode="0";// 报警编码
	private String isonline = "1";// 在线状态
	private String entid;// 组织结构ID
	private int dir;//方向
	private int speed;//速度
	private int state = 0;

	public int getState() {
		return state;
	}
	public void setState(int state) {
		this.state = state;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getVehicleno() {
		return vehicleno;
	}
	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}
	public String getTranstypecode() {
		return transtypecode;
	}
	public void setTranstypecode(String transtypecode) {
		this.transtypecode = transtypecode;
	}
	public String getNativetareacode() {
		return nativetareacode;
	}
	public void setNativetareacode(String nativetareacode) {
		this.nativetareacode = nativetareacode;
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
	public String getPlatecolorid() {
		return platecolorid;
	}
	public void setPlatecolorid(String platecolorid) {
		this.platecolorid = platecolorid;
	}
	public String getIsnative() {
		return isnative;
	}
	public void setIsnative(String isnative) {
		this.isnative = isnative;
	}
	public String getAlarmcode() {
		return alarmcode;
	}
	public void setAlarmcode(String alarmcode) {
		this.alarmcode = alarmcode;
	}
	public String getIsonline() {
		return isonline;
	}
	public void setIsonline(String isonline) {
		this.isonline = isonline;
	}
	public String getEntid() {
		return entid;
	}
	public void setEntid(String entid) {
		this.entid = entid;
	}
	public int getDir() {
		return dir;
	}
	public void setDir(int dir) {
		this.dir = dir;
	}
	public int getSpeed() {
		return speed;
	}
	public void setSpeed(int speed) {
		this.speed = speed;
	}
	/**
	 * 拼接GPS聚合信息
	 * @return
	 */
	public String getGpsDataTransferInfo(){
		if(speed == 0){
			state = 4;  //停驶 蓝色
		} else {
//			报警:1; 停驶[<=5]:2 ;在线[<=70]:3; 预警[>70]:4
//			0:离线-灰色 、 1:红色、 2:黄色 3:绿色 4:蓝色
			if(speed <= 5){
				state = 4; //停驶 蓝色
			} else if(speed <= 70){
				state = 3; //在线 绿色
			} else if(speed > 70){
				state = 1; //预警 红色
			} else {
				state = 3; //在线 绿色
			}
		}
		StringBuffer sb = new StringBuffer();
		sb.append(vid).append(":");				// 车辆编号
		sb.append(vehicleno).append(":");		// 车牌号
		sb.append(999).append(":");				// 运输行业类型	
		sb.append(entid).append(":");			// 企业编号 (权限代码)
		sb.append(lon).append(":");				// 经度  1/600000度
		sb.append(lat).append(":");				// 纬度  1/600000度
		sb.append(utc/1000).append(":");		// 时间(以秒为单位)
		sb.append(platecolorid).append(":");	// 车牌颜色
		sb.append(-1).append(":");	   	 		// 报警状态	
		sb.append(dir).append(":");				// 方向   方位角,正北,顺时针0~359
		sb.append(state).append(":");			// 状态 状态( 离线:0; 报警:1; 停驶[<=5]:2 ;在线[<=70]:3; 预警[>70]:4; 满载:5; 半载:6; 空载：7)
		sb.append("0");							// 0：外省  1：本省
		return sb.toString();				
	}

	
	@Override
	public String toString() {
		return "VehiclePolymerizeBean[vid=" + vid + ",vehicleno=" + vehicleno
				+ ",transtypecode=" + transtypecode + ",nativetareacode="
				+ nativetareacode + ",lon=" + lon + ",lat=" + lat + ",utc="
				+ utc + ",platecolorid=" + platecolorid + ",isnative="
				+ isnative + ",alarmcode=" + alarmcode + ",isonline="
				+ isonline + ",entid=" + entid + ",dir="+dir+"]";
	}
}
