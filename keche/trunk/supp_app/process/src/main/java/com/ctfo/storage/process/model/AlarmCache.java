/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * @author zjhl
 *
 */
public class AlarmCache {
	/**	报警编码	*/
	private String alarmCode;
	/**	报警编号	*/
	private String alarmId;
	/**	报警开始时间	*/
	private long startTime;
	/**
	 * @return 获取 报警编码
	 */
	public String getAlarmCode() {
		return alarmCode;
	}
	/**
	 * 设置报警编码
	 * @param alarmCode 报警编码 
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	/**
	 * @return 获取 报警编号
	 */
	public String getAlarmId() {
		return alarmId;
	}
	/**
	 * 设置报警编号
	 * @param alarmId 报警编号 
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}
	/**
	 * @return 获取 报警开始时间
	 */
	public long getStartTime() {
		return startTime;
	}
	/**
	 * 设置报警开始时间
	 * @param startTime 报警开始时间 
	 */
	public void setStartTime(long startTime) {
		this.startTime = startTime;
	}
	
}
