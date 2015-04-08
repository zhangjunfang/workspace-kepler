package com.ctfo.mileageservice.model;


/**
 * 文件名：DriverDetailBean.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-10-14下午6:40:40
 * 
 */
public class DriverDetailBean {
	private long id; // 一个车辆文件有多个驾驶员信息，记录该bean属于哪个文件，判断线程是否该结束
	private String detailId;
	
	private long statDate;
	
	private String vid;
	private VehicleMessageBean beginVmb;//驾驶起始点轨迹对象缓存
	private VehicleMessageBean endVmb;//驾驶结束点轨迹对象缓存
	
	private long   engineRotateTime;//发动机工作时长
	private long   runningTime;//行车时长
	private long   mileage;
	private long   runningOil;//行车总油耗
	private long   ecuOilWear;//ECU总油耗
	private long   ecuRunningOilWear;//ecu行车油耗
	private long   ecuIdlingOilwear;//ecu怠速油耗
	private long   metOilWear;//met总油耗
	private long   metRunningOilWear;//met行车油耗
	private long   metIdlingOilWear;//met怠速油耗
	private long   accCloseNum;//acc关次数
	private long   accCloseTime;//acc关时长
	private long   doorLockNum;
	private long   doorLockTime;
	private long   overspeedAlarm;
	private long   overspeedTime;
	private long   fatigueAlarm;
	private long   fatigueTime;
	private long   driverTimeoutTime;
	private long   stopTimoutNum;
	private long   stopTimoutTime;
	private long   inareaAlarm;
	private long   outareaAlarm;
	private long   inRouteNum;
	private long   outRouteNum;
	private long   routeRunDiffNum;
	private long   deviateRouteAlarm;
	private long   deviateRouteTime;
	private long   illegalFireNum;
	private long   illegalMoveNum;
	private long   cashAlarmNum;
	private long   cashAlarmTime;
	private long   overrpmAlarm;
	private long   overrpmTime;
	private long   gearWrongNum;
	private long   gearWrongTime;
	private long   gearGlideNum;
	private long   gearGlideTime;
	private long   urgentSpeedNum;
	private long   urgentSpeedTime;
	private long   urgentLowdownNum;
	private long   urgentLowdownTime;
	private long   longIdleNum;
	private long   longIdleTime;
	private long   economicRunTime;
	private long   areaOverspeedAlarm;
	private long   areaOverspeedTime;
	private long   areaDoorAlarm;
	private long   airconditionNum;//空调工作次数
	private long   airconditionTime;//空调工作时长
	private long   idlingAirNum;//怠速空调次数
	private long   idlingAirTime;//怠速空调时长
	private long   door1OpenNum;
	private long   areaOpendoorNum;
	private long   areaOpendoorTime;
	private long   overloadNum;
	private long   illegalStopNum;
	private long   illegalStopTime;
	private long   door2OpenNum;
	private long   door3OpenNum;
	private long   door4OpenNum;
	private long   gearImproper;
	private long   gearTime;
	private long   routeRunNum;
	private long   doorOpenNum;
	private long   overmanNum;
	private long   retarderWorkTime;
	private long   retarderWorkNum;
	private long   brakeTime;
	private long   brakeNum;
	private long   reverseGearNum;
	private long   reverseGearTime;
	private long   lowerBeamNum;
	private long   lowerBeamTime;
	private long   highBeamNum;
	private long   highBeamTime;
	private long   leftTurningSignalTime;
	private long   leftTurningSignalNum;
	private long   rightTurningSignalNum;
	private long   rightTurningSignalTime;
	private long   outlineLampTime;
	private long   outlineLampNum;
	private long   trumpetNum;
	private long   trumpetTime;
	private long   freePositionNum;
	private long   freePositionTime;
	private long   absWorkNum;
	private long   absWorkTime;
	private long   heatUpNum;
	private long   heatUpTime;
	private long   clutchTime;
	private long   clutchNum;
	private long   foglightNum;
	private long   foglightTime;
	private long   headCollideNum;  //前向碰撞
	private long   vehicleDeviateNum;	//车道偏离
	
	public String getDetailId() {
		return detailId;
	}
	public void setDetailId(String detailId) {
		this.detailId = detailId;
	}
	public long getStatDate() {
		return statDate;
	}
	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public VehicleMessageBean getBeginVmb() {
		return beginVmb;
	}
	public void setBeginVmb(VehicleMessageBean beginVmb) {
		this.beginVmb = beginVmb;
	}
	public VehicleMessageBean getEndVmb() {
		return endVmb;
	}
	public void setEndVmb(VehicleMessageBean endVmb) {
		this.endVmb = endVmb;
	}
	public long getEngineRotateTime() {
		return engineRotateTime;
	}
	public void setEngineRotateTime(long engineRotateTime) {
		this.engineRotateTime = engineRotateTime;
	}
	public long getRunningTime() {
		return runningTime;
	}
	public void setRunningTime(long runningTime) {
		this.runningTime = runningTime;
	}
	public long getRunningOil() {
		return runningOil;
	}
	public void setRunningOil(long runningOil) {
		this.runningOil = runningOil;
	}
	public long getEcuOilWear() {
		return ecuOilWear;
	}
	public void setEcuOilWear(long ecuOilWear) {
		this.ecuOilWear = ecuOilWear;
	}
	public long getEcuRunningOilWear() {
		return ecuRunningOilWear;
	}
	public void setEcuRunningOilWear(long ecuRunningOilWear) {
		this.ecuRunningOilWear = ecuRunningOilWear;
	}
	public long getEcuIdlingOilwear() {
		return ecuIdlingOilwear;
	}
	public void setEcuIdlingOilwear(long ecuIdlingOilwear) {
		this.ecuIdlingOilwear = ecuIdlingOilwear;
	}
	public long getMetOilWear() {
		return metOilWear;
	}
	public void setMetOilWear(long metOilWear) {
		this.metOilWear = metOilWear;
	}
	public long getMetRunningOilWear() {
		return metRunningOilWear;
	}
	public void setMetRunningOilWear(long metRunningOilWear) {
		this.metRunningOilWear = metRunningOilWear;
	}
	public long getMetIdlingOilWear() {
		return metIdlingOilWear;
	}
	public void setMetIdlingOilWear(long metIdlingOilWear) {
		this.metIdlingOilWear = metIdlingOilWear;
	}
	public long getAccCloseNum() {
		return accCloseNum;
	}
	public void setAccCloseNum(long accCloseNum) {
		this.accCloseNum = accCloseNum;
	}
	public long getAccCloseTime() {
		return accCloseTime;
	}
	public void setAccCloseTime(long accCloseTime) {
		this.accCloseTime = accCloseTime;
	}
	public long getDoorLockNum() {
		return doorLockNum;
	}
	public void setDoorLockNum(long doorLockNum) {
		this.doorLockNum = doorLockNum;
	}
	public long getDoorLockTime() {
		return doorLockTime;
	}
	public void setDoorLockTime(long doorLockTime) {
		this.doorLockTime = doorLockTime;
	}
	public long getOverspeedAlarm() {
		return overspeedAlarm;
	}
	public void setOverspeedAlarm(long overspeedAlarm) {
		this.overspeedAlarm = overspeedAlarm;
	}
	public long getOverspeedTime() {
		return overspeedTime;
	}
	public void setOverspeedTime(long overspeedTime) {
		this.overspeedTime = overspeedTime;
	}
	public long getFatigueAlarm() {
		return fatigueAlarm;
	}
	public void setFatigueAlarm(long fatigueAlarm) {
		this.fatigueAlarm = fatigueAlarm;
	}
	public long getFatigueTime() {
		return fatigueTime;
	}
	public void setFatigueTime(long fatigueTime) {
		this.fatigueTime = fatigueTime;
	}
	public long getDriverTimeoutTime() {
		return driverTimeoutTime;
	}
	public void setDriverTimeoutTime(long driverTimeoutTime) {
		this.driverTimeoutTime = driverTimeoutTime;
	}
	public long getStopTimoutNum() {
		return stopTimoutNum;
	}
	public void setStopTimoutNum(long stopTimoutNum) {
		this.stopTimoutNum = stopTimoutNum;
	}
	public long getStopTimoutTime() {
		return stopTimoutTime;
	}
	public void setStopTimoutTime(long stopTimoutTime) {
		this.stopTimoutTime = stopTimoutTime;
	}
	public long getInareaAlarm() {
		return inareaAlarm;
	}
	public void setInareaAlarm(long inareaAlarm) {
		this.inareaAlarm = inareaAlarm;
	}
	public long getOutareaAlarm() {
		return outareaAlarm;
	}
	public void setOutareaAlarm(long outareaAlarm) {
		this.outareaAlarm = outareaAlarm;
	}
	public long getInRouteNum() {
		return inRouteNum;
	}
	public void setInRouteNum(long inRouteNum) {
		this.inRouteNum = inRouteNum;
	}
	public long getOutRouteNum() {
		return outRouteNum;
	}
	public void setOutRouteNum(long outRouteNum) {
		this.outRouteNum = outRouteNum;
	}
	public long getRouteRunDiffNum() {
		return routeRunDiffNum;
	}
	public void setRouteRunDiffNum(long routeRunDiffNum) {
		this.routeRunDiffNum = routeRunDiffNum;
	}
	public long getDeviateRouteAlarm() {
		return deviateRouteAlarm;
	}
	public void setDeviateRouteAlarm(long deviateRouteAlarm) {
		this.deviateRouteAlarm = deviateRouteAlarm;
	}
	public long getDeviateRouteTime() {
		return deviateRouteTime;
	}
	public void setDeviateRouteTime(long deviateRouteTime) {
		this.deviateRouteTime = deviateRouteTime;
	}
	public long getIllegalFireNum() {
		return illegalFireNum;
	}
	public void setIllegalFireNum(long illegalFireNum) {
		this.illegalFireNum = illegalFireNum;
	}
	public long getIllegalMoveNum() {
		return illegalMoveNum;
	}
	public void setIllegalMoveNum(long illegalMoveNum) {
		this.illegalMoveNum = illegalMoveNum;
	}
	public long getCashAlarmNum() {
		return cashAlarmNum;
	}
	public void setCashAlarmNum(long cashAlarmNum) {
		this.cashAlarmNum = cashAlarmNum;
	}
	public long getCashAlarmTime() {
		return cashAlarmTime;
	}
	public void setCashAlarmTime(long cashAlarmTime) {
		this.cashAlarmTime = cashAlarmTime;
	}
	public long getOverrpmAlarm() {
		return overrpmAlarm;
	}
	public void setOverrpmAlarm(long overrpmAlarm) {
		this.overrpmAlarm = overrpmAlarm;
	}
	public long getOverrpmTime() {
		return overrpmTime;
	}
	public void setOverrpmTime(long overrpmTime) {
		this.overrpmTime = overrpmTime;
	}
	public long getGearWrongNum() {
		return gearWrongNum;
	}
	public void setGearWrongNum(long gearWrongNum) {
		this.gearWrongNum = gearWrongNum;
	}
	public long getGearWrongTime() {
		return gearWrongTime;
	}
	public void setGearWrongTime(long gearWrongTime) {
		this.gearWrongTime = gearWrongTime;
	}
	public long getGearGlideNum() {
		return gearGlideNum;
	}
	public void setGearGlideNum(long gearGlideNum) {
		this.gearGlideNum = gearGlideNum;
	}
	public long getGearGlideTime() {
		return gearGlideTime;
	}
	public void setGearGlideTime(long gearGlideTime) {
		this.gearGlideTime = gearGlideTime;
	}
	public long getUrgentSpeedNum() {
		return urgentSpeedNum;
	}
	public void setUrgentSpeedNum(long urgentSpeedNum) {
		this.urgentSpeedNum = urgentSpeedNum;
	}
	public long getUrgentSpeedTime() {
		return urgentSpeedTime;
	}
	public void setUrgentSpeedTime(long urgentSpeedTime) {
		this.urgentSpeedTime = urgentSpeedTime;
	}
	public long getUrgentLowdownNum() {
		return urgentLowdownNum;
	}
	public void setUrgentLowdownNum(long urgentLowdownNum) {
		this.urgentLowdownNum = urgentLowdownNum;
	}
	public long getUrgentLowdownTime() {
		return urgentLowdownTime;
	}
	public void setUrgentLowdownTime(long urgentLowdownTime) {
		this.urgentLowdownTime = urgentLowdownTime;
	}
	public long getLongIdleNum() {
		return longIdleNum;
	}
	public void setLongIdleNum(long longIdleNum) {
		this.longIdleNum = longIdleNum;
	}
	public long getLongIdleTime() {
		return longIdleTime;
	}
	public void setLongIdleTime(long longIdleTime) {
		this.longIdleTime = longIdleTime;
	}
	public long getEconomicRunTime() {
		return economicRunTime;
	}
	public void setEconomicRunTime(long economicRunTime) {
		this.economicRunTime = economicRunTime;
	}
	public long getAreaOverspeedAlarm() {
		return areaOverspeedAlarm;
	}
	public void setAreaOverspeedAlarm(long areaOverspeedAlarm) {
		this.areaOverspeedAlarm = areaOverspeedAlarm;
	}
	public long getAreaOverspeedTime() {
		return areaOverspeedTime;
	}
	public void setAreaOverspeedTime(long areaOverspeedTime) {
		this.areaOverspeedTime = areaOverspeedTime;
	}
	public long getAreaDoorAlarm() {
		return areaDoorAlarm;
	}
	public void setAreaDoorAlarm(long areaDoorAlarm) {
		this.areaDoorAlarm = areaDoorAlarm;
	}
	public long getAirconditionTime() {
		return airconditionTime;
	}
	public void setAirconditionTime(long airconditionTime) {
		this.airconditionTime = airconditionTime;
	}
	public long getDoor1OpenNum() {
		return door1OpenNum;
	}
	public void setDoor1OpenNum(long door1OpenNum) {
		this.door1OpenNum = door1OpenNum;
	}
	public long getAreaOpendoorNum() {
		return areaOpendoorNum;
	}
	public void setAreaOpendoorNum(long areaOpendoorNum) {
		this.areaOpendoorNum = areaOpendoorNum;
	}
	public long getAreaOpendoorTime() {
		return areaOpendoorTime;
	}
	public void setAreaOpendoorTime(long areaOpendoorTime) {
		this.areaOpendoorTime = areaOpendoorTime;
	}
	public long getOverloadNum() {
		return overloadNum;
	}
	public void setOverloadNum(long overloadNum) {
		this.overloadNum = overloadNum;
	}
	public long getIllegalStopNum() {
		return illegalStopNum;
	}
	public void setIllegalStopNum(long illegalStopNum) {
		this.illegalStopNum = illegalStopNum;
	}
	public long getIllegalStopTime() {
		return illegalStopTime;
	}
	public void setIllegalStopTime(long illegalStopTime) {
		this.illegalStopTime = illegalStopTime;
	}
	public long getDoor2OpenNum() {
		return door2OpenNum;
	}
	public void setDoor2OpenNum(long door2OpenNum) {
		this.door2OpenNum = door2OpenNum;
	}
	public long getDoor3OpenNum() {
		return door3OpenNum;
	}
	public void setDoor3OpenNum(long door3OpenNum) {
		this.door3OpenNum = door3OpenNum;
	}
	public long getDoor4OpenNum() {
		return door4OpenNum;
	}
	public void setDoor4OpenNum(long door4OpenNum) {
		this.door4OpenNum = door4OpenNum;
	}
	public long getGearImproper() {
		return gearImproper;
	}
	public void setGearImproper(long gearImproper) {
		this.gearImproper = gearImproper;
	}
	public long getGearTime() {
		return gearTime;
	}
	public void setGearTime(long gearTime) {
		this.gearTime = gearTime;
	}
	public long getRouteRunNum() {
		return routeRunNum;
	}
	public void setRouteRunNum(long routeRunNum) {
		this.routeRunNum = routeRunNum;
	}
	public long getDoorOpenNum() {
		return doorOpenNum;
	}
	public void setDoorOpenNum(long doorOpenNum) {
		this.doorOpenNum = doorOpenNum;
	}
	public long getOvermanNum() {
		return overmanNum;
	}
	public void setOvermanNum(long overmanNum) {
		this.overmanNum = overmanNum;
	}
	public long getRetarderWorkTime() {
		return retarderWorkTime;
	}
	public void setRetarderWorkTime(long retarderWorkTime) {
		this.retarderWorkTime = retarderWorkTime;
	}
	public long getRetarderWorkNum() {
		return retarderWorkNum;
	}
	public void setRetarderWorkNum(long retarderWorkNum) {
		this.retarderWorkNum = retarderWorkNum;
	}
	public long getBrakeTime() {
		return brakeTime;
	}
	public void setBrakeTime(long brakeTime) {
		this.brakeTime = brakeTime;
	}
	public long getBrakeNum() {
		return brakeNum;
	}
	public void setBrakeNum(long brakeNum) {
		this.brakeNum = brakeNum;
	}
	public long getReverseGearNum() {
		return reverseGearNum;
	}
	public void setReverseGearNum(long reverseGearNum) {
		this.reverseGearNum = reverseGearNum;
	}
	public long getReverseGearTime() {
		return reverseGearTime;
	}
	public void setReverseGearTime(long reverseGearTime) {
		this.reverseGearTime = reverseGearTime;
	}
	public long getLowerBeamNum() {
		return lowerBeamNum;
	}
	public void setLowerBeamNum(long lowerBeamNum) {
		this.lowerBeamNum = lowerBeamNum;
	}
	public long getLowerBeamTime() {
		return lowerBeamTime;
	}
	public void setLowerBeamTime(long lowerBeamTime) {
		this.lowerBeamTime = lowerBeamTime;
	}
	public long getHighBeamNum() {
		return highBeamNum;
	}
	public void setHighBeamNum(long highBeamNum) {
		this.highBeamNum = highBeamNum;
	}
	public long getHighBeamTime() {
		return highBeamTime;
	}
	public void setHighBeamTime(long highBeamTime) {
		this.highBeamTime = highBeamTime;
	}
	public long getLeftTurningSignalTime() {
		return leftTurningSignalTime;
	}
	public void setLeftTurningSignalTime(long leftTurningSignalTime) {
		this.leftTurningSignalTime = leftTurningSignalTime;
	}
	public long getLeftTurningSignalNum() {
		return leftTurningSignalNum;
	}
	public void setLeftTurningSignalNum(long leftTurningSignalNum) {
		this.leftTurningSignalNum = leftTurningSignalNum;
	}
	public long getRightTurningSignalNum() {
		return rightTurningSignalNum;
	}
	public void setRightTurningSignalNum(long rightTurningSignalNum) {
		this.rightTurningSignalNum = rightTurningSignalNum;
	}
	public long getRightTurningSignalTime() {
		return rightTurningSignalTime;
	}
	public void setRightTurningSignalTime(long rightTurningSignalTime) {
		this.rightTurningSignalTime = rightTurningSignalTime;
	}
	public long getOutlineLampTime() {
		return outlineLampTime;
	}
	public void setOutlineLampTime(long outlineLampTime) {
		this.outlineLampTime = outlineLampTime;
	}
	public long getOutlineLampNum() {
		return outlineLampNum;
	}
	public void setOutlineLampNum(long outlineLampNum) {
		this.outlineLampNum = outlineLampNum;
	}
	public long getTrumpetNum() {
		return trumpetNum;
	}
	public void setTrumpetNum(long trumpetNum) {
		this.trumpetNum = trumpetNum;
	}
	public long getTrumpetTime() {
		return trumpetTime;
	}
	public void setTrumpetTime(long trumpetTime) {
		this.trumpetTime = trumpetTime;
	}
	public long getAirconditionNum() {
		return airconditionNum;
	}
	public void setAirconditionNum(long airconditionNum) {
		this.airconditionNum = airconditionNum;
	}
	public long getFreePositionNum() {
		return freePositionNum;
	}
	public void setFreePositionNum(long freePositionNum) {
		this.freePositionNum = freePositionNum;
	}
	public long getFreePositionTime() {
		return freePositionTime;
	}
	public void setFreePositionTime(long freePositionTime) {
		this.freePositionTime = freePositionTime;
	}
	public long getAbsWorkNum() {
		return absWorkNum;
	}
	public void setAbsWorkNum(long absWorkNum) {
		this.absWorkNum = absWorkNum;
	}
	public long getAbsWorkTime() {
		return absWorkTime;
	}
	public void setAbsWorkTime(long absWorkTime) {
		this.absWorkTime = absWorkTime;
	}
	public long getHeatUpNum() {
		return heatUpNum;
	}
	public void setHeatUpNum(long heatUpNum) {
		this.heatUpNum = heatUpNum;
	}
	public long getHeatUpTime() {
		return heatUpTime;
	}
	public void setHeatUpTime(long heatUpTime) {
		this.heatUpTime = heatUpTime;
	}
	public long getClutchTime() {
		return clutchTime;
	}
	public void setClutchTime(long clutchTime) {
		this.clutchTime = clutchTime;
	}
	public long getClutchNum() {
		return clutchNum;
	}
	public void setClutchNum(long clutchNum) {
		this.clutchNum = clutchNum;
	}
	public long getFoglightNum() {
		return foglightNum;
	}
	public void setFoglightNum(long foglightNum) {
		this.foglightNum = foglightNum;
	}
	public long getFoglightTime() {
		return foglightTime;
	}
	public void setFoglightTime(long foglightTime) {
		this.foglightTime = foglightTime;
	}
	public long getMileage() {
		return mileage;
	}
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}
	public long getIdlingAirNum() {
		return idlingAirNum;
	}
	public void setIdlingAirNum(long idlingAirNum) {
		this.idlingAirNum = idlingAirNum;
	}
	public long getIdlingAirTime() {
		return idlingAirTime;
	}
	public void setIdlingAirTime(long idlingAirTime) {
		this.idlingAirTime = idlingAirTime;
	}
	public long getHeadCollideNum() {
		return headCollideNum;
	}
	public void setHeadCollideNum(long headCollideNum) {
		this.headCollideNum = headCollideNum;
	}
	public long getVehicleDeviateNum() {
		return vehicleDeviateNum;
	}
	public void setVehicleDeviateNum(long vehicleDeviateNum) {
		this.vehicleDeviateNum = vehicleDeviateNum;
	}
	
	public void addHeadCollideNum(long headCollide){
		this.headCollideNum += headCollide;
	}
	
	public void addVehicleDeviateNum(long vehicleDeviate){
		this.vehicleDeviateNum += vehicleDeviate;
	}
	
	public void addMileage(long mileage){
		this.mileage += mileage;
	}
	
	public void addEcuOilWear(long ecuoil){
		this.ecuOilWear += ecuoil;
	}
	
	public void addMetOilWear(long metoil){
		this.metOilWear += metoil;
	}
	
	public void addEngineTime(long time){
		this.engineRotateTime += time;
	}
	
	public void addAccNum(int num){
		this.accCloseNum += num;
	}
	
	public void addAccTime(long time){
		this.accCloseTime += time;
	}
	
	public void addRunningTime(long time){
		this.runningTime += time;
	}
	
	public void addEcuRunningOilWear(long oil){
		this.ecuRunningOilWear += oil;
	}
	
	public void addMetRunningOilWear(long oil){
		this.metRunningOilWear += oil;
	}
	
	/**
	 * 车门加锁状态数据
	 * @param time
	 */
	public void addDoorLock(long time){
		if (time>0){
			this.doorLockTime += time;
			this.doorLockNum += 1;
		}
		
	}
	
	public void addDoor1OpenNum(int num){
		if (num>0){
			this.door1OpenNum += num;
			this.doorOpenNum += num;
		}
	}
	
	public void addDoor2OpenNum(int num){
		if (num>0){
			this.door2OpenNum += num;
			this.doorOpenNum += num;
		}
	}
	
	public void addDoor3OpenNum(int num){
		if (num>0){
			this.door3OpenNum += num;
			this.doorOpenNum += num;
		}
	}
	
	public void addDoor4OpenNum(int num){
		if (num>0){
			this.door4OpenNum += num;
			this.doorOpenNum += num;
		}
	}
	
	/**
	 * 近光信号
	 * @param time
	 */
	public void addLowerBeam(long time){
		if (time>0){
			this.lowerBeamTime += time;
			this.lowerBeamNum += 1;
		}
	}
	
	/**
	 * 远光信号
	 * @param time
	 */
	public void addHighBeam(long time){
		if (time>0){
			this.highBeamTime += time;
			this.highBeamNum += 1;
		}
	}
	
	/**
	 * 右转信号
	 * @param time
	 */
	public void addRightTurningSignal(long time){
		if (time>0){
			this.rightTurningSignalTime += time;
			this.rightTurningSignalNum += 1;
		}
	}
	
	/**
	 * 左转信号
	 * @param time
	 */
	public void addLeftTurningSignal(long time){
		if (time>0){
			this.leftTurningSignalTime += time;
			this.leftTurningSignalNum += 1;
		}
	}
	
	/**
	 * 制动信号
	 * @param time
	 */
	public void addBrake(long time){
		if (time>0){
			this.brakeTime += time;
			this.brakeNum += 1;
		}
	}
	
	/**
	 * 倒档信号
	 * @param time
	 */
	public void addReverseGear(long time){
		if (time>0){
			this.reverseGearTime += time;
			this.reverseGearNum += 1;
		}
	}
	
	/**
	 * 雾灯信号
	 * @param time
	 */
	public void addFoglight(long time){
		if (time>0){
			this.foglightTime += time;
			this.foglightNum += 1;
		}
	}
	
	/**
	 * 示廊灯信号
	 * @param time
	 */
	public void addOutlineLamp(long time){
		if (time>0){
			this.outlineLampTime += time;
			this.outlineLampNum += 1;
		}
	}
	
	/**
	 * 喇叭信号
	 * @param time
	 */
	public void addTrumpet(long time){
		if (time>0){
			this.trumpetTime += time;
			this.trumpetNum += 1;
		}
	}
	
	/**
	 * 空调状态
	 * @param time
	 */
	public void addAircondition0(long time){
		if (time>0){
			this.airconditionTime += time;
			this.airconditionNum += 1;
		}
	}
	
	/**
	 * 空挡信号
	 * @param time
	 */
	public void addFreePosition(long time){
		if (time>0){
			this.freePositionTime += time;
			this.freePositionNum += 1;
		}
	}
	
	/**
	 * 缓速器工作
	 * @param time
	 */
	public void addRetarderWork(long time){
		if (time>0){
			this.retarderWorkTime += time;
			this.retarderWorkNum += 1;
		}
	}
	
	/**
	 * ABS工作
	 * @param time
	 */
	public void addAbsWork(long time){
		if (time>0){
			this.absWorkTime += time;
			this.absWorkNum += 1;
		}
	}
	
	/**
	 * 加热器工作
	 * @param time
	 */
	public void addHeatUp(long time){
		if (time>0){
			this.heatUpTime += time;
			this.heatUpNum += 1;
		}
	}
	
	/**
	 * 离合器工作
	 * @param time
	 */
	public void addClutch(long time){
		if (time>0){
			this.clutchTime += time;
			this.clutchNum += 1;
		}
	}
	
	/**
	 * 超速
	 * @param time
	 */
	public void addOverspeed(long time){
		if (time>0){
			this.overspeedTime += time;
			this.overspeedAlarm += 1;
		}
	}
	
	/**
	 * 疲劳驾驶
	 * @param time
	 */
	public void addFatigue(long time){
		if (time>0){
			this.fatigueTime += time;
			this.fatigueAlarm += 1;
		}
	}
	
	/**
	 * 当天累计驾驶超时
	 * @param time
	 */
	public void addDriverTimeoutTime(long time){
		if (time>0){
			this.driverTimeoutTime += time;
		}
	}
	
	public void addStopTimout(long time){
		if (time>0){
			this.stopTimoutTime += time;
			this.stopTimoutNum += 1;
		}
	}
	
	public void addIntoarea(int num){
		if (num>0){
			this.inareaAlarm += num;
		}
	}
	
	public void addOutarea(int num){
		if (num>0){
			this.outareaAlarm += num;
		}
	}
	
	public void addIntoRoute(int num){
		if (num>0){
			this.inRouteNum += num;
		}
	}
	
	public void addOutRoute(int num){
		if (num>0){
			this.outRouteNum += num;
		}
	}
	
	
	public void addRouteRunDiffNum(int num){
		if (num>0){
			this.routeRunDiffNum += num;
		}
	}
	
	public void addDeviateRoute(long time){
		if (time>0){
			this.deviateRouteTime += time;
			this.deviateRouteAlarm += 1;
		}
	}
	
	public void addIllegalFireNum(int num){
		if (num>0){
			this.illegalFireNum += num;
		}
	}
	
	public void addIllegalMoveNum(int num){
		if (num>0){
			this.illegalMoveNum += num;
		}
	}
	
	public void addCashAlarm(long time){
		if (time>0){
			this.cashAlarmTime += time;
			this.cashAlarmNum += 1;
		}
	}
	
	public void addOverrpm(long time){
		if (time>0){
			this.overrpmTime += time;
			this.overrpmAlarm += 1;
		}
	}
	
	public void addGearWrong(long time){
		if (time>0){
			this.gearWrongTime += time;
			this.gearWrongNum += 1;
		}
	}
	
	public void addGearGlide(long time){
		if (time>0){
			this.gearGlideTime += time;
			this.gearGlideNum += 1;
		}
	}
	
	public void addUrgentSpeed(long time){
		if (time>0){
			this.urgentSpeedTime += time;
			this.urgentSpeedNum += 1;
		}
	}
	
	public void addUrgentLowdown(long time){
		if (time>0){
			this.urgentLowdownTime += time;
			this.urgentLowdownNum += 1;
		}
	}
	
	public void addLongIdle(long time){
		if (time>0){
			this.longIdleTime += time;
			this.longIdleNum += 1;
		}
	}
	
	/**
	 * 空调工作
	 * @param time
	 */
	public void addAircondition(long time){
		if (time>0){
			this.airconditionTime += time;
			this.airconditionNum += 1;
		}
	}
	
	/**
	 * 怠速空调
	 * @param time
	 */
	public void addIdlingAir(long time){
		if (time>0){
			this.idlingAirTime += time;
			this.idlingAirNum += 1;
		}
	}
	
	/**
	 * 超经济区运行时间
	 * @param time
	 */
	public void addEconomicRunTime(long time){
		if (time>0){
			this.economicRunTime += time;
		}
	}
	
	/**
	 * 区域内超速
	 * @param time
	 */
	public void addAreaOverspeed(long time){
		if (time>0){
			this.areaOverspeedTime += time;
			this.areaOverspeedAlarm += 1;
		}
	}
	
	/**
	 * 区域内开门
	 * @param time
	 */
	public void addAreaOpendoor(long time){
		if (time>0){
			this.areaOpendoorTime += time;
			this.areaOpendoorNum += 1;
		}
	}
	/**
	 * @return the id
	 */
	public long getId() {
		return id;
	}
	/**
	 * @param id the id to set
	 */
	public void setId(long id) {
		this.id = id;
	}
	
	
}
