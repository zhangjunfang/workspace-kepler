package com.caits.analysisserver.bean;

public class ThVehicleSpeedAnomalous {

	private String vid;

	private String speedForm;

	private String entId;

	private String vehicleNo;

	private long vssSpeedTotal;

	private long gpsSpeedTotal;

	private long count = 0;

	private long statDate;

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getSpeedForm() {
		return speedForm;
	}

	public void setSpeedForm(String speedForm) {
		this.speedForm = speedForm;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public long getVssSpeedAvg() {
		return vssSpeedTotal / count;
	}

	public void setVssGPSSpeedTotal(long vssSpeed, long gpsSpeed) {
		this.vssSpeedTotal += vssSpeed;
		this.gpsSpeedTotal += gpsSpeed;
		count++;
	}

	public long getGpsSpeedAvg() {
		return gpsSpeedTotal / count;
	}

	public long getCount() {
		return count;
	}

	public void setCount(long count) {
		this.count = count;
	}

	public long getStatDate() {
		return statDate;
	}

	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}

}
