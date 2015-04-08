/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * @author zjhl
 *
 */
public class Track {
	/**	报警标志位	*/
	private String alarm;
	/**	状态位	*/
	private String status;
	/**	经度	*/
	private long lon;
	/**	纬度	*/
	private long lat;
	/**	偏移经度	*/
	private long maplon;
	/**	偏移纬度	*/
	private long maplat;
	/**	海拔（单位：米）	*/
	private int elevation;
	/**	方向（0-359，正北为0，顺时针 单位：度）	*/
	private int direction;
	/**	速度（1/10km/h 单位：米/小时）	*/
	private int speed;
	/**	BCD时间（格式:yyMMddHHmmss）	*/
	private String bcdTime;
	/**	UTC时间	*/
	private long utcTime;
	/**
	 * @return 获取 报警标志位
	 */
	public String getAlarm() {
		return alarm;
	}
	/**
	 * 设置报警标志位
	 * @param alarm 报警标志位 
	 */
	public void setAlarm(String alarm) {
		this.alarm = alarm;
	}
	/**
	 * @return 获取 状态位
	 */
	public String getStatus() {
		return status;
	}
	/**
	 * 设置状态位
	 * @param status 状态位 
	 */
	public void setStatus(String status) {
		this.status = status;
	}
	/**
	 * @return 获取 经度
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * 设置经度
	 * @param lon 经度 
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * @return 获取 纬度
	 */
	public long getLat() {
		return lat;
	}
	/**
	 * 设置纬度
	 * @param lat 纬度 
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}
	/**
	 * @return 获取 偏移经度
	 */
	public long getMaplon() {
		return maplon;
	}
	/**
	 * 设置偏移经度
	 * @param maplon 偏移经度 
	 */
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}
	/**
	 * @return 获取 偏移纬度
	 */
	public long getMaplat() {
		return maplat;
	}
	/**
	 * 设置偏移纬度
	 * @param maplat 偏移纬度 
	 */
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}
	/**
	 * @return 获取 海拔（单位：米）
	 */
	public int getElevation() {
		return elevation;
	}
	/**
	 * 设置海拔（单位：米）
	 * @param elevation 海拔（单位：米） 
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}
	/**
	 * @return 获取 方向（0-359，正北为0，顺时针单位：度）
	 */
	public int getDirection() {
		return direction;
	}
	/**
	 * 设置方向（0-359，正北为0，顺时针单位：度）
	 * @param direction 方向（0-359，正北为0，顺时针单位：度） 
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	/**
	 * @return 获取 速度（110kmh单位：米小时）
	 */
	public int getSpeed() {
		return speed;
	}
	/**
	 * 设置速度（110kmh单位：米小时）
	 * @param speed 速度（110kmh单位：米小时） 
	 */
	public void setSpeed(int speed) {
		this.speed = speed;
	}
	/**
	 * @return 获取 BCD时间（格式:yyMMddHHmmss）
	 */
	public String getBcdTime() {
		return bcdTime;
	}
	/**
	 * 设置BCD时间（格式:yyMMddHHmmss）
	 * @param bcdTime BCD时间（格式:yyMMddHHmmss） 
	 */
	public void setBcdTime(String bcdTime) {
		this.bcdTime = bcdTime;
	}
	/**
	 * @return 获取 UTC时间
	 */
	public long getUtcTime() {
		return utcTime;
	}
	/**
	 * 设置UTC时间
	 * @param utcTime UTC时间 
	 */
	public void setUtcTime(long utcTime) {
		this.utcTime = utcTime;
	}
	public String toString(){
		StringBuffer sb = new StringBuffer();
		sb.append("报警编号:").append(this.alarm);
		sb.append(", 时间:").append(this.bcdTime);
		sb.append(", 方向:").append(this.direction);
		sb.append(", 海拔:").append(this.elevation);
		sb.append(", 纬度:").append(this.lat);
		sb.append(", 经度:").append(this.lon);
		sb.append(", 速度:").append(this.speed);
		sb.append(", 状态:").append(this.status);
		return sb.toString();
	}
}
