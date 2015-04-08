package com.kypt.c2pp.inside.vo;

import java.math.BigDecimal;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Calendar;

/*
 * 位置基本信息
 */
public class LocInfoVO {
	
	/**
	 * 报警信息
	 */
	private AlarmVO alarmVO=new AlarmVO();
	
	/**
	 * 状态信息
	 */
	private StateVO stateVO=new StateVO();
	
	/**
	 * 开关量信息
	 */
	private SwitchValueVO switchValueVO = new SwitchValueVO();
	
	/**
	 * can信息
	 */
	private CanVO canVO=new CanVO();
	
	/**
	 * 经度
	 */
	private String lon;
	
	/**
	 * 维度
	 */
	private String lat;
	
	/**
	 * 海拔高度 m
	 */
	private String elevation;
	
	/**
	 * 方向
	 */
	private String direction;
	
	/**
	 * GPS速度
	 */
	private String gpsSpeed;
	
	/**
	 * 终端采集时间
	 */
	private Date terminalTime;

	public AlarmVO getAlarmVO() {
		return alarmVO;
	}

	public void setAlarmVO(AlarmVO alarmVO) {
		this.alarmVO = alarmVO;
	}

	public StateVO getStateVO() {
		return stateVO;
	}

	public void setStateVO(StateVO stateVO) {
		this.stateVO = stateVO;
	}

	public CanVO getCanVO() {
		return canVO;
	}

	public void setCanVO(CanVO canVO) {
		this.canVO = canVO;
	}
	
	public SwitchValueVO getSwitchValueVO() {
		return switchValueVO;
	}

	public void setSwitchValueVO(SwitchValueVO switchValueVO) {
		this.switchValueVO = switchValueVO;
	}

	public String getLon() {
		return lon;
	}

	public void setLon(String lon) {
		this.lon = lon;
	}

	public String getLat() {
		return lat;
	}

	public void setLat(String lat) {
		this.lat = lat;
	}

	public String getElevation() {
		return elevation;
	}

	public void setElevation(String elevation) {
		this.elevation = elevation;
	}

	public String getDirection() {
		return direction;
	}

	public void setDirection(String direction) {
		this.direction = direction;
	}

	public String getGpsSpeed() {
		return gpsSpeed;
	}

	public void setGpsSpeed(String gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	public Date getTerminalTime() {
		return terminalTime;
	}

	public void setTerminalTime(Date terminalTime) {
		this.terminalTime = terminalTime;
	}
	
	
	
	public String toString(){
		StringBuffer sb=new StringBuffer();
		
		if (this.alarmVO!=null){
			sb.append(this.alarmVO.toString());
		}
		
		if (this.stateVO!=null){
			sb.append(this.stateVO.toString());
		}
		
		if (this.lat!=null&&this.lat.length()>0){
			BigDecimal dlat = new BigDecimal(this.lat);
			dlat=dlat.setScale(6, BigDecimal.ROUND_HALF_UP); 
			String str=dlat.multiply(new BigDecimal("600000")).toString();
			str = str.substring(0,str.indexOf("."));
			sb.append("2:"+str+",");
		}
		
		if (this.lon!=null&&this.lon.length()>0){
			BigDecimal dlon = new BigDecimal(this.lon);
			dlon=dlon.setScale(6, BigDecimal.ROUND_HALF_UP); 
			String str=dlon.multiply(new BigDecimal("600000")).toString();
			str = str.substring(0,str.indexOf("."));
			sb.append("1:"+str+",");
		}
		
		if (this.elevation!=null&&this.elevation.length()>0){
			sb.append("6:"+this.elevation+",");
		}
		
		if (this.gpsSpeed!=null&&this.gpsSpeed.length()>0){
			sb.append("3:"+this.gpsSpeed+",");
		}
		
		if (this.direction!=null&&this.direction.length()>0){
			sb.append("5:"+this.direction+",");
		}
		
		if (this.terminalTime!=null){
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd/HHmmss");
//			System.out.println("dt 3:"+this.terminalTime);
//			System.out.println("dt 3:"+this.terminalTime);
//			System.out.println("da 4:"+sdf.format(this.terminalTime));
			sb.append("4:"+sdf.format(this.terminalTime)+",");
		}
		
		if (this.canVO!=null){
			sb.append(this.canVO.toString());
		}
		
		if (this.switchValueVO!=null){
			sb.append(this.switchValueVO.toString());
		}
		
		String msg = sb.toString();
		
		return msg.substring(0,msg.length()-1);
	}

}
