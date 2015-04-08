/**
 * 
 */
package com.ctfo.storage.process.model;

/**
 * 实时位置存储
 *
 */
public class RealTimeLocation {
	/**	车辆编号	*/
	private String vid;
	/**	车牌号	*/
	private String plate;
	/**	车牌颜色	*/
	private String plateColor;
	/**	手机号	*/
	private String phoneNumber;
	/**	终端号	*/
	private String tid;
	/**	终端型号	*/
	private String terminalType;
	/**	驾驶员名称	*/
	private String staffName;
	/**	组织名称	*/
	private String entName;
	/**	车队编号	*/
	private String entId;
	/**	车队名称	*/
	private String teamName;
	/**	车队编号	*/
	private String teamId;
	/**	实时数据状态(1:经纬度错误无法定位; 2:速度异常; 3:方向错误; 4:GPS时间错误)	*/
	private int status;
	/**	在线状态(1:在线; 0:离线)	*/
	private int online;
	/**	偏移经度	*/
	private long maplon;
	/**	偏移纬度	*/
	private long maplat;
	/**	速度来源标识	*/
	private int speedSource;
	/**	速度（1/10km/h 单位：米/小时）	*/
	private int speed;
	/**	行驶记录仪速度	*/
	private int vssSpeed;
	/**	海拔（单位：米）	*/
	private int elevation;
	/**	方向（0-359，正北为0，顺时针 单位：度）	*/
	private int direction;
	/**	GPS时间	*/
	private long gspTime;
	/**	SYS时间	*/
	private long sysTime;
	/**	里程	*/
	private long mileage;
	/**	厂商编号	*/
	private String oemCode;
	/**
	 * @return 获取 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return 获取 车牌颜色
	 */
	public String getPlateColor() {
		return plateColor;
	}
	/**
	 * 设置车牌颜色
	 * @param plateColor 车牌颜色 
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}
	/**
	 * @return 获取 手机号
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}
	/**
	 * 设置手机号
	 * @param phoneNumber 手机号 
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}
	/**
	 * @return 获取 终端号
	 */
	public String getTid() {
		return tid;
	}
	/**
	 * 设置终端号
	 * @param tid 终端号 
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}
	/**
	 * @return 获取 终端型号
	 */
	public String getTerminalType() {
		return terminalType;
	}
	/**
	 * 设置终端型号
	 * @param terminalType 终端型号 
	 */
	public void setTerminalType(String terminalType) {
		this.terminalType = terminalType;
	}
	/**
	 * @return 获取 驾驶员名称
	 */
	public String getStaffName() {
		return staffName;
	}
	/**
	 * 设置驾驶员名称
	 * @param driverName 驾驶员名称 
	 */
	public void setStaffName(String staffName) {
		this.staffName = staffName;
	}
	/**
	 * @return 获取 组织名称
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置组织名称
	 * @param entName 组织名称 
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * @return 获取 车队编号
	 */
	public String getEntId() {
		return entId;
	}
	/**
	 * 设置车队编号
	 * @param entId 车队编号 
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}
	/**
	 * @return 获取 车队名称
	 */
	public String getTeamName() {
		return teamName;
	}
	/**
	 * 设置车队名称
	 * @param teamName 车队名称 
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}
	/**
	 * @return 获取 车队编号
	 */
	public String getTeamId() {
		return teamId;
	}
	/**
	 * 设置车队编号
	 * @param teamId 车队编号 
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}
	/**
	 * @return 获取 实时数据状态(1:经纬度错误无法定位;2:速度异常;3:方向错误;4:GPS时间错误)
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * 设置实时数据状态(1:经纬度错误无法定位;2:速度异常;3:方向错误;4:GPS时间错误)
	 * @param status 实时数据状态(1:经纬度错误无法定位;2:速度异常;3:方向错误;4:GPS时间错误) 
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * @return 获取 在线状态(1:在线;0:离线)
	 */
	public int getOnline() {
		return online;
	}
	/**
	 * 设置在线状态(1:在线;0:离线)
	 * @param online 在线状态(1:在线;0:离线) 
	 */
	public void setOnline(int online) {
		this.online = online;
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
	 * @return 获取 速度来源标识
	 */
	public int getSpeedSource() {
		return speedSource;
	}
	/**
	 * 设置速度来源标识
	 * @param speedSource 速度来源标识 
	 */
	public void setSpeedSource(int speedSource) {
		this.speedSource = speedSource;
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
	 * @return 获取 行驶记录仪速度
	 */
	public int getVssSpeed() {
		return vssSpeed;
	}
	/**
	 * 设置行驶记录仪速度
	 * @param vssSpeed 行驶记录仪速度 
	 */
	public void setVssSpeed(int vssSpeed) {
		this.vssSpeed = vssSpeed;
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
	 * @return 获取 GPS时间
	 */
	public long getGspTime() {
		return gspTime;
	}
	/**
	 * 设置GPS时间
	 * @param gspTime GPS时间 
	 */
	public void setGspTime(long gspTime) {
		this.gspTime = gspTime;
	}
	/**
	 * @return 获取 SYS时间
	 */
	public long getSysTime() {
		return sysTime;
	}
	/**
	 * 设置SYS时间
	 * @param sysTime SYS时间 
	 */
	public void setSysTime(long sysTime) {
		this.sysTime = sysTime;
	}
	/**
	 * @return 获取 里程
	 */
	public long getMileage() {
		return mileage;
	}
	/**
	 * 设置里程
	 * @param mileage 里程 
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}
	/**
	 * @return 获取 厂商编号
	 */
	public String getOemCode() {
		return oemCode;
	}
	/**
	 * 设置厂商编号
	 * @param oemCode 厂商编号 
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
}
