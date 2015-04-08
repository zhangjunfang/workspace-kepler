package com.caits.analysisserver.bean;

import com.caits.analysisserver.utils.CDate;

public class RunningBean {
	
	private String vid;
	private String startTime;
	private String stopTime;
	private long runningMileage;
	private long runningOil;
	private long maxSpeed;
	private long maxRotateSpeed;
	private long beginLon;
	private long beginLat;
	private long endLon;
	private long endLat;
	private long ecuRunningOil;
	private long metRunningOil;
	private String oilFlag;
	
	private boolean startFlag = false;
	private boolean stopFlag = false;
	
	private long startRunningMileage;//开始行驶里程
	private long startRunningOil;//开始油耗
	private long stopRunningMileage;//结束行驶里程
	private long stopRunningOil;//结束油耗
	
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getStartTime() {
		return startTime;
	}
	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}
	public String getStopTime() {
		return stopTime;
	}
	public void setStopTime(String stopTime) {
		this.stopTime = stopTime;
	}
	public long getRunningMileage() {
		return runningMileage;
	}
	public void addRunningMileage(long runningMileage) {
		this.runningMileage = runningMileage + this.runningMileage;
	}
	public long getRunningOil() {
		return runningOil;
	}
	public void addRunningOil(long runningOil) {
		this.runningOil = runningOil + this.runningOil;
	}
	public long getMaxSpeed() {
		return maxSpeed;
	}
	public void setMaxSpeed(long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}
	public long getMaxRotateSpeed() {
		return maxRotateSpeed;
	}
	public void setMaxRotateSpeed(long maxRotateSpeed) {
		this.maxRotateSpeed = maxRotateSpeed;
	}
	public long getBeginLon() {
		return beginLon;
	}
	public void setBeginLon(long beginLon) {
		this.beginLon = beginLon;
	}
	public long getBeginLat() {
		return beginLat;
	}
	public void setBeginLat(long beginLat) {
		this.beginLat = beginLat;
	}
	public long getEndLon() {
		return endLon;
	}
	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}
	public long getEndLat() {
		return endLat;
	}
	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}
	public boolean isStartFlag() {
		return startFlag;
	}
	public void setStartFlag(boolean startFlag) {
		this.startFlag = startFlag;
	}
	public boolean isStopFlag() {
		return stopFlag;
	}
	public void setStopFlag(boolean stopFlag) {
		this.stopFlag = stopFlag;
	}
	public long getStartRunningMileage() {
		return startRunningMileage;
	}
	public void setStartRunningMileage(long startRunningMileage) {
		this.startRunningMileage = startRunningMileage;
	}
	public long getStartRunningOil() {
		return startRunningOil;
	}
	public void setStartRunningOil(long startRunningOil) {
		this.startRunningOil = startRunningOil;
	}
	public long getStopRunningMileage() {
		return stopRunningMileage;
	}
	public void setStopRunningMileage(long stopRunningMileage) {
		this.stopRunningMileage = stopRunningMileage;
	}
	public long getStopRunningOil() {
		return stopRunningOil;
	}
	public void setStopRunningOil(long stopRunningOil) {
		this.stopRunningOil = stopRunningOil;
	}
	
	public String toString(){
		return "    time:"+this.getStartTime()+","+this.getStopTime()+","+(CDate.stringConvertUtc(this.getStopTime())-CDate.stringConvertUtc(this.getStartTime()))/1000+
				",loc:"+this.getBeginLon()+","+this.getBeginLat()+","+this.getEndLon()+","+this.getEndLat()+
				",mileage:"+this.getStartRunningMileage()+","+this.getStopRunningMileage()+
				",oil:"+this.getStartRunningOil()+","+this.getStopRunningOil()+
				",metoil:"+this.getMetRunningOil()+","+
				",ecuoil:"+this.getEcuRunningOil()+","+
				",max"+this.getMaxRotateSpeed()+","+this.getMaxSpeed()+"\r\n";
	}
	public long getEcuRunningOil() {
		return ecuRunningOil;
	}
	public void addEcuRunningOil(long ecuRunningOil) {
		this.ecuRunningOil = ecuRunningOil + this.ecuRunningOil;
	}
	public long getMetRunningOil() {
		return metRunningOil;
	}
	public void addMetRunningOil(long metRunningOil) {
		this.metRunningOil = metRunningOil + this.metRunningOil;
	}
	public String getOilFlag() {
		return oilFlag;
	}
	public void setOilFlag(String oilFlag) {
		this.oilFlag = oilFlag;
	}

}