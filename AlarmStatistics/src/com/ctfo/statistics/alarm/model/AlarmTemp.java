package com.ctfo.statistics.alarm.model;

import java.util.UUID;

public class AlarmTemp {
	/**	编号	*/
	private String alarmId;
	/**	告警编码	*/
	private String alarmCode;
	/**	告警开始UTC时间	*/
	private long startUtc;
	/**	最近一次告警位置里程	*/
	private int lastMileage;
	/**	告警总里程	*/
	private int alarmTotalMileage;
	/**	最高车速	*/
	private int maxSpeed;
	/**	车速总和	*/
	private int totalSpeed;
	/**	告警来源(1:终端上报; 2:平台分析)	*/
	private int alarmSource;
	/**	车速计数	*/
	private int speedIndex;
	/**	运行时长	*/
	private long runTatol;
	public AlarmTemp(){
		this.alarmId = UUID.randomUUID().toString().replace("-", ""); 
	}
	/**
	 * 获取[编号]值
	 */
	public String getAlarmId() {
		return alarmId;
	}
	/**
	 * 设置[编号] 值
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}
	/**
	 * 获取[告警编号]值
	 */
	public String getAlarmCode() {
		return alarmCode;
	}
	/**
	 * 设置[告警编号] 值
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	/**
	 * 获取[告警开始UTC时间]值
	 */
	public long getStartUtc() {
		return startUtc;
	}
	/**
	 * 设置[告警开始UTC时间] 值
	 */
	public void setStartUtc(long startUtc) {
		this.startUtc = startUtc;
	}
	/**
	 * 获取[最近一次告警位置里程]值
	 */
	public int getLastMileage() {
		return lastMileage;
	}
	/**
	 * 设置[最近一次告警位置里程] 值
	 */
	public void setLastMileage(int lastMileage) {
		this.lastMileage = lastMileage;
	}
	/**
	 * 获取[告警总里程]值
	 */
	public int getAlarmTotalMileage() {
		return alarmTotalMileage;
	}
	/**
	 * 设置[告警总里程] 值
	 */
	public void setAlarmTotalMileage(int totalMileage) {
		this.alarmTotalMileage += totalMileage;
	}
	/**
	 * 获取[最高车速]值
	 */
	public int getMaxSpeed() {
		return maxSpeed;
	}
	/**
	 * 设置[最高车速] 值
	 */
	public void setMaxSpeed(int maxSpeed) {
		this.maxSpeed = maxSpeed;
	}
	/**
	 * 获取[车速总和]值
	 */
	public int getTotalSpeed() {
		return totalSpeed;
	}
	/**
	 * 设置[车速总和] 值
	 */
	public void setTotalSpeed(int currentSpeed) {
		this.totalSpeed += currentSpeed;
		this.speedIndex++;
	}
	/**
	 * 获取[平均车速]值
	 */
	public int getAvgSpeed() {
		if(this.speedIndex > 1){
			return this.totalSpeed / this.speedIndex;
		}
		return this.totalSpeed;
	}
	/**
	 * 获取[告警来源(1:终端上报;2:平台分析)]值
	 */
	public int getAlarmSource() {
		return alarmSource;
	}
	/**
	 * 设置[告警来源(1:终端上报;2:平台分析)] 值
	 */
	public void setAlarmSource(int alarmSource) {
		this.alarmSource = alarmSource;
	}
	/**
	 * 获取[运行时长]值
	 */
	public long getRunTatol() {
		return runTatol;
	}
	/**
	 * 设置[运行时长] 值
	 */
	public void setRunTatol(long runTatol) {
		this.runTatol = runTatol;
	}
	
}
