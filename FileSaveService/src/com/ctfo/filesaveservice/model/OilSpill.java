package com.ctfo.filesaveservice.model;

public class OilSpill {
	/**	偷油发生后的油量值	*/
	private int beferOilSpillOil;
	/**	偷油发生前上上次有油量变化的里程值	*/
	private int mileage;
	/** 车辆编号	*/
	private String vid;
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
	/** 油量变化信息	*/
	private OilInfo oilInfo;
	/**
	 * 获得偷油发生后的油量值的值
	 * @return the beferOilSpillOil 偷油发生后的油量值  
	 */
	public int getBeferOilSpillOil() {
		return beferOilSpillOil;
	}
	/**
	 * 设置偷油发生后的油量值的值
	 * @param beferOilSpillOil 偷油发生后的油量值  
	 */
	public void setBeferOilSpillOil(int beferOilSpillOil) {
		this.beferOilSpillOil = beferOilSpillOil;
	}
	/**
	 * 获得偷油发生前上上次有油量变化的里程值的值
	 * @return the mileage 偷油发生前上上次有油量变化的里程值  
	 */
	public int getMileage() {
		return mileage;
	}
	/**
	 * 设置偷油发生前上上次有油量变化的里程值的值
	 * @param mileage 偷油发生前上上次有油量变化的里程值  
	 */
	public void setMileage(int mileage) {
		this.mileage = mileage;
	}
	/**
	 * 获得车辆编号的值
	 * @return the vid 车辆编号  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
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
	public OilInfo getOilInfo() {
		return oilInfo;
	}
	public void setOilInfo(OilInfo oilInfo) {
		this.oilInfo = oilInfo;
	}
	
}
