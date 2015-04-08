package com.ctfo.trackservice.model;

public class VehicleAlarm {
	private String vid;
	
	private String alarmCode;//报警编码
	
	private String alarmType = null; //报警类型
	
	private int count = 0; //报警次数
	
	private long time = 0; //报警时间
	
	private long speedingOil = 0; // 报警下油耗
	
	private long speedingMileage =0; // 报警下行驶里程

	public long getSpeedingOil() {
		return speedingOil;
	}

	public void addSpeedingOil(long speedingOil) {
		if (speedingOil>0){
			this.speedingOil = speedingOil + this.speedingOil;
		}
	}

	public long getSpeedingMileage() {
		return speedingMileage;
	}

	public void addSpeedingMileage(Long speedingMileage) {
		if (speedingMileage>0){
			this.speedingMileage = speedingMileage + this.speedingMileage;
		}
	}

	public String getAlarmType() {
		return alarmType;
	}

	public void setAlarmType(String alarmType) {
		this.alarmType = alarmType;
	}

	public int getCount() {
		return count;
	}

	public void addCount(int count) {
		this.count = this.count + count;
	}

	public long getTime() {
		return time;
	}

	public void addTime(long time) {
		if (time>0){
			this.time = this.time + time;
		}
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getAlarmCode() {
		return alarmCode;
	}

	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
}
