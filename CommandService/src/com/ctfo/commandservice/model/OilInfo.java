package com.ctfo.commandservice.model;
/**
 *	燃油信息
 * @author zjhl
 *
 */
public class OilInfo {
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
	/** 偷漏油状态(0=油位正常 ; 1=偷油量提示 ; 2=加油提示 ; 3=偷油告警 ; 4=软件版本号 ; 5=参数设置查询 ;	)		*/
	private String status;
	/** 指令类型	*/
	private int commandType;
	/**	油箱液位	*/
	private int fuelLevel;
	/**	变动油量	*/
	private int oilChange;
	/**	当前油量	*/
	private int oilAllowance;
	/** 油量容量（标定）	*/
	private int oilCalibration;
	/** 加油前后落差	*/
	private int gap;
	/** 加油阀值（门限）	*/
	private int refuelThreshold;
	/** 偷油阀值（门限）	*/
	private int stealThreshold;
	/** 序列号	*/
	private String seq;

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

	/**
	 * 获得偷漏油状态(0=油位正常;1=偷油量提示;2=加油提示;3=偷油告警;4=软件版本号;5=参数设置查询;)的值
	 * @return the status 偷漏油状态(0=油位正常;1=偷油量提示;2=加油提示;3=偷油告警;4=软件版本号;5=参数设置查询;)  
	 */
	public String getStatus() {
		return status;
	}

	/**
	 * 设置偷漏油状态(0=油位正常;1=偷油量提示;2=加油提示;3=偷油告警;4=软件版本号;5=参数设置查询;)的值
	 * @param status 偷漏油状态(0=油位正常;1=偷油量提示;2=加油提示;3=偷油告警;4=软件版本号;5=参数设置查询;)  
	 */
	public void setStatus(String status) {
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
	 * 获得当前油量的值
	 * @return the oilAllowance 当前油量
	 */
	public int getOilAllowance() {
		return oilAllowance;
	}

	/**
	 * 设置当前油量的值
	 * @param oilAllowance 当前油量  
	 */
	public void setOilAllowance(int oilAllowance) {
		this.oilAllowance = oilAllowance;
	}

	/**
	 * 获得油量容量（标定）的值
	 * @return the oilCalibration 油量容量（标定）  
	 */
	public int getOilCalibration() {
		return oilCalibration;
	}

	/**
	 * 设置油量容量（标定）的值
	 * @param oilCalibration 油量容量（标定）  
	 */
	public void setOilCalibration(int oilCalibration) {
		this.oilCalibration = oilCalibration;
	}

	/**
	 * 获得加油前后落差的值
	 * @return the gap 加油前后落差  
	 */
	public int getGap() {
		return gap;
	}

	/**
	 * 设置加油前后落差的值
	 * @param gap 加油前后落差  
	 */
	public void setGap(int gap) {
		this.gap = gap;
	}

	/**
	 * 获得加油阀值（门限）的值
	 * @return the refuelThreshold 加油阀值（门限）  
	 */
	public int getRefuelThreshold() {
		return refuelThreshold;
	}

	/**
	 * 设置加油阀值（门限）的值
	 * @param refuelThreshold 加油阀值（门限）  
	 */
	public void setRefuelThreshold(int refuelThreshold) {
		this.refuelThreshold = refuelThreshold;
	}

	/**
	 * 获得偷油阀值（门限）的值
	 * @return the stealThreshold 偷油阀值（门限）  
	 */
	public int getStealThreshold() {
		return stealThreshold;
	}

	/**
	 * 设置偷油阀值（门限）的值
	 * @param stealThreshold 偷油阀值（门限）  
	 */
	public void setStealThreshold(int stealThreshold) {
		this.stealThreshold = stealThreshold;
	}

	/**
	 * 获得序列号的值
	 * @return the seq 序列号  
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * 设置序列号的值
	 * @param seq 序列号  
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

	public String toString() {
		StringBuffer sb = new StringBuffer();
		sb.append("commandType=").append(commandType);
		sb.append(",fuelLevel=").append(fuelLevel);
		sb.append(",lat=").append(lat);
		sb.append(",lon=").append(lon);
		sb.append(",elevation=").append(elevation);
		sb.append(",speed=").append(speed);
		sb.append(",direction=").append(direction);
		sb.append(",time=").append(time);
		sb.append(",status=").append(status);
		sb.append(",commandType=").append(commandType);
		sb.append("vid=").append(vid);
		sb.append(",seq=").append(seq);
		sb.append(",oilCalibration=").append(oilCalibration);
		sb.append(",gap=").append(gap);
		sb.append(",refuelThreshold=").append(refuelThreshold);
		sb.append(",stealThreshold=").append(stealThreshold);
		return sb.toString();
	}
}
