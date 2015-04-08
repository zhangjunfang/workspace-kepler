/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.model AlarmStartInfo.java	</li><br>
 * <li>时        间：2013-12-4  下午5:25:21	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.model;

/*****************************************
 * <li>描        述：报警开始		
 * 
 *****************************************/
public class AlarmStart {
	/**	1.报警编号	*/
	private String alarmId;
	/**	2. 车辆编号	*/
	private String vid;
	/**	3. 时间	*/
	private long utc;
	/**	4. 经度	*/
	private long lon;
	/**	5. 纬度	*/
	private long lat;
	/**	6. 偏移经度	*/
	private long maplon;
	/**	7. 偏移纬度	*/
	private long maplat;
	/**	8. 海拔 	*/
	private int elevation;
	/**	9. 方向	*/
	private int direction;
	/**	10. gsp速度	*/
	private int gpsSpeed;
	/**	11.里程	*/
	private long mileage; 
	/**	12. 总油量	*/
	private long oilTotal;
	/**	13. 报警编号	*/
	private String alarmCode;
	/**	14。 系统时间	*/
	private long sysUtc;
	/**	15. 报警状态	*/
	private int alarmStatus;
	/**	16. 报警开始时间	*/
	private long alarmStartUtc;
	/**	17. 报警驾驶员	*/
	private String alarmDriver;
	/**	18. 车牌号	*/
	private String plate;
	/**	19. 报警级别	*/
	private String alarmLevel;
	/**	20. 基本状态	*/
	private String baseStatus;
	/**	21. 扩展状态	*/
	private String extendStatus;
	/**	22.报警附加信息	*/
	private String alarmAdded;
	/**	23. redis报警键	*/
	private String alarmKey;
	/**	24. redis报警值	*/
	private String alarmValue;
	/** 25. 车队编号 */
	private String teamId;
	/** 26. 车队编号 */
	private String teamName;
	/** 27. 车队编号 */
	private String entId;
	/** 28. 车队编号 */
	private String entName;
	/** 29. 驾驶员编号 */
	private String driverId;
	/** 30. 驾驶员信息来源 */
	private int driverSource;
	/** 31. 实时报警指令	 */
	private String realTimeAlarmCommand;
	/** 32. 监管报警指令	 */
	private String pccAlarmCommand;
	/**
	 * 获得1.报警编号的值
	 * @return the alarmId 1.报警编号  
	 */
	public String getAlarmId() {
		return alarmId;
	}

	/**
	 * 设置1.报警编号的值
	 * @param alarmId 1.报警编号  
	 */
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}

	/**
	 * 获得2.车辆编号的值
	 * @return the vid 2.车辆编号  
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置2.车辆编号的值
	 * @param vid 2.车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获得3.时间的值
	 * @return the utc 3.时间  
	 */
	public long getUtc() {
		return utc;
	}

	/**
	 * 设置3.时间的值
	 * @param utc 3.时间  
	 */
	public void setUtc(long utc) {
		this.utc = utc;
	}

	/**
	 * 获得4.经度的值
	 * @return the lon 4.经度  
	 */
	public long getLon() {
		return lon;
	}

	/**
	 * 设置4.经度的值
	 * @param lon 4.经度  
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}

	/**
	 * 获得5.纬度的值
	 * @return the lat 5.纬度  
	 */
	public long getLat() {
		return lat;
	}

	/**
	 * 设置5.纬度的值
	 * @param lat 5.纬度  
	 */
	public void setLat(long lat) {
		this.lat = lat;
	}

	/**
	 * 获得6.偏移经度的值
	 * @return the maplon 6.偏移经度  
	 */
	public long getMaplon() {
		return maplon;
	}

	/**
	 * 设置6.偏移经度的值
	 * @param maplon 6.偏移经度  
	 */
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}

	/**
	 * 获得7.偏移纬度的值
	 * @return the maplat 7.偏移纬度  
	 */
	public long getMaplat() {
		return maplat;
	}

	/**
	 * 设置7.偏移纬度的值
	 * @param maplat 7.偏移纬度  
	 */
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}

	/**
	 * 获得8.海拔的值
	 * @return the elevation 8.海拔  
	 */
	public int getElevation() {
		return elevation;
	}

	/**
	 * 设置8.海拔的值
	 * @param elevation 8.海拔  
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}

	/**
	 * 获得9.方向的值
	 * @return the direction 9.方向  
	 */
	public int getDirection() {
		return direction;
	}

	/**
	 * 设置9.方向的值
	 * @param direction 9.方向  
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}

	/**
	 * 获得10.gsp速度的值
	 * @return the gpsSpeed 10.gsp速度  
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}

	/**
	 * 设置10.gsp速度的值
	 * @param gpsSpeed 10.gsp速度  
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	/**
	 * 获得11.里程的值
	 * @return the mileage 11.里程  
	 */
	public long getMileage() {
		return mileage;
	}

	/**
	 * 设置11.里程的值
	 * @param mileage 11.里程  
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	/**
	 * 获得12.总油量的值
	 * @return the oilTotal 12.总油量  
	 */
	public long getOilTotal() {
		return oilTotal;
	}

	/**
	 * 设置12.总油量的值
	 * @param oilTotal 12.总油量  
	 */
	public void setOilTotal(long oilTotal) {
		this.oilTotal = oilTotal;
	}

	/**
	 * 获得13.报警编号的值
	 * @return the alarmCode 13.报警编号  
	 */
	public String getAlarmCode() {
		return alarmCode;
	}

	/**
	 * 设置13.报警编号的值
	 * @param alarmCode 13.报警编号  
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}

	/**
	 * 获得14。系统时间的值
	 * @return the sysUtc 14。系统时间  
	 */
	public long getSysUtc() {
		return sysUtc;
	}

	/**
	 * 设置14。系统时间的值
	 * @param sysUtc 14。系统时间  
	 */
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}

	/**
	 * 获得15.报警状态的值
	 * @return the alarmStatus 15.报警状态  
	 */
	public int getAlarmStatus() {
		return alarmStatus;
	}

	/**
	 * 设置15.报警状态的值
	 * @param alarmStatus 15.报警状态  
	 */
	public void setAlarmStatus(int alarmStatus) {
		this.alarmStatus = alarmStatus;
	}

	/**
	 * 获得16.报警开始时间的值
	 * @return the alarmStartUtc 16.报警开始时间  
	 */
	public long getAlarmStartUtc() {
		return alarmStartUtc;
	}

	/**
	 * 设置16.报警开始时间的值
	 * @param alarmStartUtc 16.报警开始时间  
	 */
	public void setAlarmStartUtc(long alarmStartUtc) {
		this.alarmStartUtc = alarmStartUtc;
	}

	/**
	 * 获得17.报警驾驶员的值
	 * @return the alarmDriver 17.报警驾驶员  
	 */
	public String getAlarmDriver() {
		return alarmDriver;
	}

	/**
	 * 设置17.报警驾驶员的值
	 * @param alarmDriver 17.报警驾驶员  
	 */
	public void setAlarmDriver(String alarmDriver) {
		this.alarmDriver = alarmDriver;
	}

	/**
	 * 获得18.车牌号的值
	 * @return the plate 18.车牌号  
	 */
	public String getPlate() {
		return plate;
	}

	/**
	 * 设置18.车牌号的值
	 * @param plate 18.车牌号  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}

	/**
	 * 获得19.报警级别的值
	 * @return the alarmLevel 19.报警级别  
	 */
	public String getAlarmLevel() {
		return alarmLevel;
	}

	/**
	 * 设置19.报警级别的值
	 * @param alarmLevel 19.报警级别  
	 */
	public void setAlarmLevel(String alarmLevel) {
		this.alarmLevel = alarmLevel;
	}

	/**
	 * 获得20.基本状态的值
	 * @return the baseStatus 20.基本状态  
	 */
	public String getBaseStatus() {
		return baseStatus;
	}

	/**
	 * 设置20.基本状态的值
	 * @param baseStatus 20.基本状态  
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	/**
	 * 获得21.扩展状态的值
	 * @return the extendStatus 21.扩展状态  
	 */
	public String getExtendStatus() {
		return extendStatus;
	}

	/**
	 * 设置21.扩展状态的值
	 * @param extendStatus 21.扩展状态  
	 */
	public void setExtendStatus(String extendStatus) {
		this.extendStatus = extendStatus;
	}

	/**
	 * 获得22.报警附加信息的值
	 * @return the alarmAdded 22.报警附加信息  
	 */
	public String getAlarmAdded() {
		return alarmAdded;
	}

	/**
	 * 设置22.报警附加信息的值
	 * @param alarmAdded 22.报警附加信息  
	 */
	public void setAlarmAdded(String alarmAdded) {
		this.alarmAdded = alarmAdded;
	}

	/**
	 * 获得23.redis报警键的值
	 * @return the alarmKey 23.redis报警键  
	 */
	public String getAlarmKey() {
		return alarmKey;
	}

	/**
	 * 设置23.redis报警键的值
	 * @param alarmKey 23.redis报警键  
	 */
	public void setAlarmKey(String alarmKey) {
		this.alarmKey = alarmKey;
	}

	/**
	 * 获得24.redis报警值的值
	 * @return the alarmValue 24.redis报警值  
	 */
	public String getAlarmValue() {
		return alarmValue;
	}

	/**
	 * 设置24.redis报警值的值
	 * @param alarmValue 24.redis报警值  
	 */
	public void setAlarmValue(String alarmValue) {
		this.alarmValue = alarmValue;
	}

	/**
	 * 获得25.车队编号的值
	 * @return the teamId 25.车队编号  
	 */
	public String getTeamId() {
		return teamId;
	}

	/**
	 * 设置25.车队编号的值
	 * @param teamId 25.车队编号  
	 */
	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}

	/**
	 * 获得26.车队编号的值
	 * @return the teamName 26.车队编号  
	 */
	public String getTeamName() {
		return teamName;
	}

	/**
	 * 设置26.车队编号的值
	 * @param teamName 26.车队编号  
	 */
	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	/**
	 * 获得27.车队编号的值
	 * @return the entId 27.车队编号  
	 */
	public String getEntId() {
		return entId;
	}

	/**
	 * 设置27.车队编号的值
	 * @param entId 27.车队编号  
	 */
	public void setEntId(String entId) {
		this.entId = entId;
	}

	/**
	 * 获得28.车队编号的值
	 * @return the entName 28.车队编号  
	 */
	public String getEntName() {
		return entName;
	}
	/**
	 * 设置28.车队编号的值
	 * @param entName 28.车队编号  
	 */
	public void setEntName(String entName) {
		this.entName = entName;
	}
	/**
	 * 获得29.驾驶员编号的值
	 * @return the driverId 29.驾驶员编号  
	 */
	public String getDriverId() {
		return driverId;
	}
	/**
	 * 设置29.驾驶员编号的值
	 * @param driverId 29.驾驶员编号  
	 */
	public void setDriverId(String driverId) {
		this.driverId = driverId;
	}
	/**
	 * 获得30.驾驶员信息来源的值
	 * @return the driverSource 30.驾驶员信息来源  
	 */
	public int getDriverSource() {
		return driverSource;
	}
	/**
	 * 设置30.驾驶员信息来源的值
	 * @param driverSource 30.驾驶员信息来源  
	 */
	public void setDriverSource(int driverSource) {
		this.driverSource = driverSource;
	}
	/**
	 * 获得31.实时报警指令的值
	 * @return the realTimeAlarmCommand 31.实时报警指令  
	 */
	public String getRealTimeAlarmCommand() {
		return realTimeAlarmCommand;
	}
	/**
	 * 设置31.实时报警指令的值
	 * @param realTimeAlarmCommand 31.实时报警指令  
	 */
	public void setRealTimeAlarmCommand(String realTimeAlarmCommand) {
		this.realTimeAlarmCommand = realTimeAlarmCommand;
	}
	/**
	 * 获得32.监管报警指令的值
	 * @return the pccAlarmCommand 32.监管报警指令  
	 */
	public String getPccAlarmCommand() {
		return pccAlarmCommand;
	}
	/**
	 * 设置32.监管报警指令的值
	 * @param pccAlarmCommand 32.监管报警指令  
	 */
	public void setPccAlarmCommand(String pccAlarmCommand) {
		this.pccAlarmCommand = pccAlarmCommand;
	}

	public String toString(ServiceUnit su, String entName) { 
//		报警开始UTC时间:车辆编号:车牌号:车牌颜色:告警类型:速度:经度:纬度:企业名称:报警编号:驾驶员编号：驾驶员信息来源:报警结束UTC时间
		StringBuffer sb = new StringBuffer();
		sb.append(this.alarmStartUtc).append(":");
		sb.append(su.getVid()).append(":");
		sb.append(su.getVehicleno()).append(":");
		sb.append(su.getPlatecolorid()).append(":");
		sb.append(this.alarmCode).append(":");
		sb.append(this.gpsSpeed).append(":");
		sb.append(this.maplon).append(":");
		sb.append(this.maplat).append(":");
		sb.append(entName).append(":");
		sb.append(this.alarmId).append(":"); 
		if(this.driverId != null && this.driverId.length() > 0){
			sb.append(this.driverId).append(":");
			sb.append(this.driverSource).append(":");
		} else {
			sb.append("::");
		}
		return sb.toString();
	}
}
