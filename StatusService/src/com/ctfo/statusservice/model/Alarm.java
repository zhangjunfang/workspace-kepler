package com.ctfo.statusservice.model;

public class Alarm {
	/**	报警编号		*/
	private String id;
	/**	报警开始时间		*/
	private long startUtc;
	/**	缓存redis报警编号		*/
	private String cacheAlarmId;
	/**	最大车速		*/
	private int maxSpeed;
	/**	速度总和		*/
	private int speedSum;
	/**	计速器		*/
	private int speedometer = 0;
	/**	平均车速		*/
	private int averageSpeed;
	/**	发动机最大车速		*/
	private int maxRpm;
	
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public long getStartUtc() {
		return startUtc;
	}
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}
	public String getCacheAlarmId() {
		return cacheAlarmId;
	}
	public void setCacheAlarmId(String cacheAlarmId) {
		this.cacheAlarmId = cacheAlarmId;
	}
	public int getMaxSpeed() {
		return maxSpeed;
	}
	public void setMaxSpeed(int maxSpeed) {
		this.maxSpeed = maxSpeed;
	}
	public int getSpeedSum() {
		return speedSum;
	}
	public void setSpeedSum(int speedSum) {
		if(this.speedSum < Integer.MAX_VALUE){
			this.speedSum += speedSum;
			this.speedometer += 1;
		}
	}
	public int getSpeedometer() {
		return speedometer;
	}
	public void setSpeedometer(int speedometer) {
		this.speedometer = speedometer;
	}
	public int getAverageSpeed() {
		if(this.speedometer <= 0 || this.speedSum <= 0){
			return 0;
		} else {
			return this.speedSum/this.speedometer;
		}
	}
	public void setAverageSpeed(int averageSpeed) {
		this.averageSpeed = averageSpeed;
	}
	public int getMaxRpm() {
		return maxRpm;
	}
	public void setMaxRpm(int maxRpm) {
		this.maxRpm = maxRpm;
	}
	public String toString(){
		StringBuffer sb = new StringBuffer();
		sb.append("id=").append(this.id).append(" , ");
		sb.append("startUtc=").append(this.startUtc).append(" , ");
		sb.append("cacheAlarmId=").append(this.cacheAlarmId).append(" , ");
		sb.append("maxSpeed=").append(this.maxSpeed).append(" , ");
		sb.append("speedSum=").append(this.speedSum).append(" , ");
		sb.append("averageSpeed=").append(this.averageSpeed).append(" , ");
		sb.append("maxRpm=").append(this.maxRpm);
		return sb.toString();
	}
}
