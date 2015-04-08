package com.ctfo.analy.beans;

import java.io.Serializable;
/**
 * 设置报警判断
 * @author yangyi
 *
 */
public class AlarmBaseBean implements Serializable {
 
	private static final long serialVersionUID = 1L;
	private String vid;// 车辆ID
	private String commaddr;// 手机号码
	private String alarmtype;// 报警类型
	private Long updatetime;//更新时间
	
	private String vehicleno;//车牌号
	private String innerCode;//内部编号
	private String vinCode;//内部编号
	private String corpId;
	private String corpName;
	private String teamId;
	private String teamName;
	private String plateColor;//车牌颜色
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getCommaddr() {
		return commaddr;
	}
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}
	public String getAlarmtype() {
		return alarmtype;
	}
	public void setAlarmtype(String alarmtype) {
		this.alarmtype = alarmtype;
	}
	public String getVehicleno() {
		return vehicleno;
	}
	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}
	public Long getUpdatetime() {
		return updatetime;
	}
	public void setUpdatetime(Long updatetime) {
		this.updatetime = updatetime;
	}
	public String getInnerCode() {
		return innerCode;
	}
	public void setInnerCode(String innerCode) {
		this.innerCode = innerCode;
	}
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public String getCorpId() {
		return corpId;
	}
	public void setCorpId(String corpId) {
		this.corpId = corpId;
	}
	public String getCorpName() {
		return corpName;
	}
	public void setCorpName(String corpName) {
		this.corpName = corpName;
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
	public String getPlateColor() {
		return plateColor;
	}
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
 
}
