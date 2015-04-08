package com.ctfo.commandservice.model;
/**
 * 油量基本信息
 * @author zjhl
 *
 */
public class OilBase {
	/** 经度	*/
	private long lon;
	/** 纬度	*/
	private long lat;
	/** 海拔、高度	*/
	private int elevation ;
	/** 速度	*/
	private int speed ;
	/** 方向	*/
	private int direction ;
	/** 时间	*/
	private String time;
	/** 偷漏油状态(0:油位正常 ; 1:偷油量提示 ; 2:加油提示 ; 3:偷油告警 ; 4:软件版本号 ; 5:参数设置查询 ;	)		*/
	private int status;
	/** 指令类型	*/
	private int commandType;
	/**	油箱液位	*/
	private int fuelLevel;
	/**	变动油量	*/
	private int oilChange;
	/**	燃油余量	*/
	private int oilAllowance;
	/**
	 * 获得经度的值
	 * @return the lon 经度  
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * 设置经度的值
	 * @param lon 经度  
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * 获得纬度的值
	 * @return the lat 纬度  
	 */
	public long getLat() {
		return lat;
	}
	/**
	 * 设置纬度的值
	 * @param lat 纬度  
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}
	/**
	 * 获得海拔、高度的值
	 * @return the elevation 海拔、高度  
	 */
	public int getElevation() {
		return elevation;
	}
	/**
	 * 设置海拔、高度的值
	 * @param elevation 海拔、高度  
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}
	/**
	 * 获得速度的值
	 * @return the speed 速度  
	 */
	public int getSpeed() {
		return speed;
	}
	/**
	 * 设置速度的值
	 * @param speed 速度  
	 */
	public void setSpeed(int speed) {
		this.speed = speed;
	}
	/**
	 * 获得方向的值
	 * @return the direction 方向  
	 */
	public int getDirection() {
		return direction;
	}
	/**
	 * 设置方向的值
	 * @param direction 方向  
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	/**
	 * 获得时间的值
	 * @return the time 时间  
	 */
	public String getTime() {
		return time;
	}
	/**
	 * 设置时间的值
	 * @param time 时间  
	 */
	public void setTime(String time) {
		this.time = time;
	}
	/**
	 * 获得偷漏油状态的值(0:油位正常 ; 1:偷油量提示 ; 2:加油提示 ; 3:偷油告警 ; 4:软件版本号 ; 5:参数设置查询 ;	)	
	 * @return the status 偷漏油状态  
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * 设置偷漏油状态的值 (0:油位正常 ; 1:偷油量提示 ; 2:加油提示 ; 3:偷油告警 ; 4:软件版本号 ; 5:参数设置查询 ;	)	
	 * @param status 偷漏油状态  
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * 获得指令类型的值
	 * @return the commandType 指令类型  
	 */
	public int getCommandType() {
		return commandType;
	}
	/**
	 * 设置指令类型的值
	 * @param commandType 指令类型  
	 */
	public void setCommandType(int commandType) {
		this.commandType = commandType;
	}
	/**
	 * 获得油箱液位的值
	 * @return the fuelLevel 油箱液位  
	 */
	public int getFuelLevel() {
		return fuelLevel;
	}
	/**
	 * 设置油箱液位的值
	 * @param fuelLevel 油箱液位  
	 */
	public void setFuelLevel(int fuelLevel) {
		this.fuelLevel = fuelLevel;
	}
	/**
	 * 获得变动油量的值
	 * @return the oilChange 变动油量  
	 */
	public int getOilChange() {
		return oilChange;
	}
	/**
	 * 设置变动油量的值
	 * @param oilChange 变动油量  
	 */
	public void setOilChange(int oilChange) {
		this.oilChange = oilChange;
	}
	/**
	 * 获得燃油余量的值
	 * @return the oilAllowance 燃油余量  
	 */
	public int getOilAllowance() {
		return oilAllowance;
	}
	/**
	 * 设置燃油余量的值
	 * @param oilAllowance 燃油余量  
	 */
	public void setOilAllowance(int oilAllowance) {
		this.oilAllowance = oilAllowance;
	}
	
}
