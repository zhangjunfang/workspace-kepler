package com.kypt.c2pp.inside.vo;

import org.apache.commons.lang.StringUtils;

import com.kypt.c2pp.util.Converser;

public class SwitchValueVO {
	
	/**
	 * 严重故障	标志维持至报警条件解除
	 */
	private boolean  seriousBugAlarm=false;
	
	/**
	 * 1：制动气压报警	标志维持至报警条件解除
	 */
	private boolean brakeAirPressureAlarm=false;

	/**
	 * 1：油压报警	标志维持至报警条件解除
	 */
	private boolean oilPressureAlarm=false;
		
	/**
	 * 1：水位低报警	标志维持至报警条件解除
	 */
	private boolean stageLowAlarm=false;
		
	/**
	 * 1：制动蹄片磨损报警	标志维持至报警条件解除
	 */
	private boolean brakeShoeAlarm=false;
		
	/**
	 * 1：空滤堵塞报警	标志维持至报警条件解除
	 */
	private boolean airFilterClogAlarm=false;
		
	/**
	 * 1：缓速器高温报警信号	标志维持至报警条件解除
	 */
	private boolean retarderHtAlarm=false;
		
	/**
	 * 1：仓温报警信号	标志维持至报警条件解除
	 */
	private boolean ehousingHtAlarm=false;
		
	/**
	 * 1：机滤堵塞信号	标志维持至报警条件解除
	 */
	private boolean mwereBlockingAlarm=false;
		
	/**
	 * 1：燃油堵塞信号	标志维持至报警条件解除
	 */
	private boolean fuelBlockingAlarm=false;
		
	/**
	 * 1：机油温度报警信号	标志维持至报警条件解除
	 */
	private boolean eoilTemperatureAlarm=false;
		
	/**
	 * 1：燃油警告	标志维持至报警条件解除
	 */
	private boolean oilAlarm=false;
		
	/**
	 * 1：空档滑行告警	标志维持至报警条件解除
	 */
	private boolean gearGlideAlarm=false;
		
	/**
	 * 1：超长怠速告警	标志维持至报警条件解除
	 */
	private boolean longIdleAlarm=false;
		
	/**
	 * 1：怠速空调告警	标志维持至报警条件解除
	 */
	private boolean airConditionAlarm=false;
		
	/**
	 * 1：发动机超转告警	标志维持至报警条件解除
	 */
	private boolean overRpmAlarm=false;
		
	/**
	 * 1：急加速报警	标志维持至报警条件解除
	 */
	private boolean urgentSpeedAlarm=false;
		
	/**
	 * 1：急减速报警	标志维持至报警条件解除
	 */
	private boolean urgentLowdownAlarm=false;
	
	/**
	 * 1：门开报警	标志维持至报警条件解除
	 */
	private boolean doorOpenAlarm=false;
	/**
	 * 1：冷却液温度过高报警	标志维持至报警条件解除
	 */
	private boolean coolantHighTemperatureAlarm=false;
	/**
	 * 1：蓄电池电压报警	标志维持至报警条件解除
	 */
	private boolean batteryVoltageAlarm=false;
	/**
	 * 1：ABS故障报警	标志维持至报警条件解除
	 */
	private boolean absBugAlarm=false;

	/**
	 * 1：近光灯信号
	 */
	private boolean passingLampSignal=false;
	
	/**
	 * 1：远光灯信号
	 */
	private boolean upperBeamSignal=false;
	
	/**
	 * 1：右转向灯信号
	 */
	private boolean rSteeringLampSignal=false;
	
	/**
	 * 1：左转向灯信号
	 */
	private boolean lSteeringLampSignal=false;
	
	/**
	 * 1：制动信号
	 */
	private boolean brakingSignal=false;
	
	/**
	 * 1：倒档信号
	 */
	private boolean backGearSignal=false;
	
	/**
	 * 1：雾灯信号
	 */
	private boolean fogLampSignal=false;
	
	/**
	 * 1：示廓灯
	 */
	private boolean makerLampSignal=false;
	
	/**
	 * 1：喇叭信号
	 */
	private boolean hornSignal=false;

	/**
	 * 1：空调状态
	 */
	private boolean airConditionerState=false;
	
	/**
	 * 1：空挡信号
	 */
	private boolean neutralSignal=false;
	
	/**
	 * 1：缓速器工作
	 */
	private boolean retarderWorkingState=false;
	
	/**
	 * 1：ABS工作
	 */
	private boolean absWorkingState=false;
	
	/**
	 * 1：加热器工作
	 */
	private boolean heaterWorkingState=false;
	
	/**
	 * 1：离合器状态
	 */
	private boolean dutchWorkingState=false;

	/**
	 * 是否严重故障
	 * @return
	 */
	public boolean isSeriousBugAlarm() {
		return seriousBugAlarm;
	}

	/**
	 * 严重故障
	 * @return
	 */
	public void setSeriousBugAlarm(boolean seriousBugAlarm) {
		this.seriousBugAlarm = seriousBugAlarm;
	}

	public boolean isBrakeAirPressureAlarm() {
		return brakeAirPressureAlarm;
	}

	public void setBrakeAirPressureAlarm(boolean brakeAirPressureAlarm) {
		this.brakeAirPressureAlarm = brakeAirPressureAlarm;
	}

	public boolean isOilPressureAlarm() {
		return oilPressureAlarm;
	}

	public void setOilPressureAlarm(boolean oilPressureAlarm) {
		this.oilPressureAlarm = oilPressureAlarm;
	}

	public boolean isStageLowAlarm() {
		return stageLowAlarm;
	}

	public void setStageLowAlarm(boolean stageLowAlarm) {
		this.stageLowAlarm = stageLowAlarm;
	}

	public boolean isBrakeShoeAlarm() {
		return brakeShoeAlarm;
	}

	public void setBrakeShoeAlarm(boolean brakeShoeAlarm) {
		this.brakeShoeAlarm = brakeShoeAlarm;
	}

	public boolean isAirFilterClogAlarm() {
		return airFilterClogAlarm;
	}

	public void setAirFilterClogAlarm(boolean airFilterClogAlarm) {
		this.airFilterClogAlarm = airFilterClogAlarm;
	}

	public boolean isRetarderHtAlarm() {
		return retarderHtAlarm;
	}

	public void setRetarderHtAlarm(boolean retarderHtAlarm) {
		this.retarderHtAlarm = retarderHtAlarm;
	}

	public boolean isEhousingHtAlarm() {
		return ehousingHtAlarm;
	}

	public void setEhousingHtAlarm(boolean ehousingHtAlarm) {
		this.ehousingHtAlarm = ehousingHtAlarm;
	}

	public boolean isMwereBlockingAlarm() {
		return mwereBlockingAlarm;
	}

	public void setMwereBlockingAlarm(boolean mwereBlockingAlarm) {
		this.mwereBlockingAlarm = mwereBlockingAlarm;
	}

	public boolean isFuelBlockingAlarm() {
		return fuelBlockingAlarm;
	}

	public void setFuelBlockingAlarm(boolean fuelBlockingAlarm) {
		this.fuelBlockingAlarm = fuelBlockingAlarm;
	}

	public boolean isEoilTemperatureAlarm() {
		return eoilTemperatureAlarm;
	}

	public void setEoilTemperatureAlarm(boolean eoilTemperatureAlarm) {
		this.eoilTemperatureAlarm = eoilTemperatureAlarm;
	}

	public boolean isOilAlarm() {
		return oilAlarm;
	}

	public void setOilAlarm(boolean oilAlarm) {
		this.oilAlarm = oilAlarm;
	}

	public boolean isGearGlideAlarm() {
		return gearGlideAlarm;
	}

	public void setGearGlideAlarm(boolean gearGlideAlarm) {
		this.gearGlideAlarm = gearGlideAlarm;
	}

	public boolean isLongIdleAlarm() {
		return longIdleAlarm;
	}

	public void setLongIdleAlarm(boolean longIdleAlarm) {
		this.longIdleAlarm = longIdleAlarm;
	}

	public boolean isAirConditionAlarm() {
		return airConditionAlarm;
	}

	public void setAirConditionAlarm(boolean airConditionAlarm) {
		this.airConditionAlarm = airConditionAlarm;
	}

	public boolean isOverRpmAlarm() {
		return overRpmAlarm;
	}

	public void setOverRpmAlarm(boolean overRpmAlarm) {
		this.overRpmAlarm = overRpmAlarm;
	}

	public boolean isUrgentSpeedAlarm() {
		return urgentSpeedAlarm;
	}

	public void setUrgentSpeedAlarm(boolean urgentSpeedAlarm) {
		this.urgentSpeedAlarm = urgentSpeedAlarm;
	}

	public boolean isUrgentLowdownAlarm() {
		return urgentLowdownAlarm;
	}

	public void setUrgentLowdownAlarm(boolean urgentLowdownAlarm) {
		this.urgentLowdownAlarm = urgentLowdownAlarm;
	}
	
	public boolean isDoorOpenAlarm() {
		return doorOpenAlarm;
	}

	public void setDoorOpenAlarm(boolean doorOpenAlarm) {
		this.doorOpenAlarm = doorOpenAlarm;
	}

	public boolean isCoolantHighTemperatureAlarm() {
		return coolantHighTemperatureAlarm;
	}

	public void setCoolantHighTemperatureAlarm(boolean coolantHighTemperatureAlarm) {
		this.coolantHighTemperatureAlarm = coolantHighTemperatureAlarm;
	}

	public boolean isBatteryVoltageAlarm() {
		return batteryVoltageAlarm;
	}

	public void setBatteryVoltageAlarm(boolean batteryVoltageAlarm) {
		this.batteryVoltageAlarm = batteryVoltageAlarm;
	}

	public boolean isAbsBugAlarm() {
		return absBugAlarm;
	}

	public void setAbsBugAlarm(boolean absBugAlarm) {
		this.absBugAlarm = absBugAlarm;
	}

	public boolean isRSteeringLampSignal() {
		return rSteeringLampSignal;
	}

	public void setRSteeringLampSignal(boolean steeringLampSignal) {
		rSteeringLampSignal = steeringLampSignal;
	}

	public boolean isLSteeringLampSignal() {
		return lSteeringLampSignal;
	}

	public void setLSteeringLampSignal(boolean steeringLampSignal) {
		lSteeringLampSignal = steeringLampSignal;
	}

	public boolean isPassingLampSignal() {
		return passingLampSignal;
	}

	public void setPassingLampSignal(boolean passingLampSignal) {
		this.passingLampSignal = passingLampSignal;
	}

	public boolean isUpperBeamSignal() {
		return upperBeamSignal;
	}

	public void setUpperBeamSignal(boolean upperBeamSignal) {
		this.upperBeamSignal = upperBeamSignal;
	}

	public boolean isrSteeringLampSignal() {
		return rSteeringLampSignal;
	}

	public void setrSteeringLampSignal(boolean rSteeringLampSignal) {
		this.rSteeringLampSignal = rSteeringLampSignal;
	}

	public boolean islSteeringLampSignal() {
		return lSteeringLampSignal;
	}

	public void setlSteeringLampSignal(boolean lSteeringLampSignal) {
		this.lSteeringLampSignal = lSteeringLampSignal;
	}

	public boolean isBrakingSignal() {
		return brakingSignal;
	}

	public void setBrakingSignal(boolean brakingSignal) {
		this.brakingSignal = brakingSignal;
	}

	public boolean isBackGearSignal() {
		return backGearSignal;
	}

	public void setBackGearSignal(boolean backGearSignal) {
		this.backGearSignal = backGearSignal;
	}

	public boolean isFogLampSignal() {
		return fogLampSignal;
	}

	public void setFogLampSignal(boolean fogLampSignal) {
		this.fogLampSignal = fogLampSignal;
	}

	public boolean isMakerLampSignal() {
		return makerLampSignal;
	}

	public void setMakerLampSignal(boolean makerLampSignal) {
		this.makerLampSignal = makerLampSignal;
	}

	public boolean isHornSignal() {
		return hornSignal;
	}

	public void setHornSignal(boolean hornSignal) {
		this.hornSignal = hornSignal;
	}

	public boolean isAirConditionerState() {
		return airConditionerState;
	}

	public void setAirConditionerState(boolean airConditionerState) {
		this.airConditionerState = airConditionerState;
	}

	public boolean isNeutralSignal() {
		return neutralSignal;
	}

	public void setNeutralSignal(boolean neutralSignal) {
		this.neutralSignal = neutralSignal;
	}

	public boolean isRetarderWorkingState() {
		return retarderWorkingState;
	}

	public void setRetarderWorkingState(boolean retarderWorkingState) {
		this.retarderWorkingState = retarderWorkingState;
	}

	public boolean isAbsWorkingState() {
		return absWorkingState;
	}

	public void setAbsWorkingState(boolean absWorkingState) {
		this.absWorkingState = absWorkingState;
	}

	public boolean isHeaterWorkingState() {
		return heaterWorkingState;
	}

	public void setHeaterWorkingState(boolean heaterWorkingState) {
		this.heaterWorkingState = heaterWorkingState;
	}

	public boolean isDutchWorkingState() {
		return dutchWorkingState;
	}

	public void setDutchWorkingState(boolean dutchWorkingState) {
		this.dutchWorkingState = dutchWorkingState;
	}
	
	/**
	 * 初始化扩展报警信息
	 * @param alarmStr  按808扩展协议顺序排列的扩展报警信息
	 *                  如：00001111100011
	 */
	public void setExtAlarm(String alarmStr){
		
	}
	
	/**
	 * 初始化扩展状态信息
	 * @param alarmStr  按808扩展协议顺序排列的扩展报警信息
	 *                  如：00001111100011
	 */
	public void setExtState(String stateStr){
		
	}
	
	public String toString(){
		String extendAlarmStr = StringUtils.repeat("0", 32);
		
		if (this.seriousBugAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,0,'1');
		}
		
		if (this.brakeAirPressureAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,1,'1');
		}
		
		if (this.oilPressureAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,2,'1');
		}
		
		if (this.stageLowAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,3,'1');
		}
		
		if (this.brakeShoeAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,4,'1');
		}
		
		if (this.airFilterClogAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,5,'1');
		}
		
		if (this.retarderHtAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,6,'1');
		}
		
		if (this.ehousingHtAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,7,'1');
		}
		
		if (this.mwereBlockingAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,8,'1');
		}
		
		if (this.fuelBlockingAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,9,'1');
		}
		
		if (this.eoilTemperatureAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,10,'1');
		}
		
		if (this.oilAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,11,'1');
		}
		
		if (this.gearGlideAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,12,'1');
		}
		
		if (this.longIdleAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,13,'1');
		}
		
		if (this.airConditionAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,14,'1');
		}
		
		if (this.overRpmAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,15,'1');
		}
		
		if (this.urgentSpeedAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,16,'1');
		}
		
		if (this.urgentLowdownAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,17,'1');
		}
		
		if (this.doorOpenAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,18,'1');
		}
		
		if (this.coolantHighTemperatureAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,19,'1');
		}
		
		if (this.batteryVoltageAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,20,'1');
		}
		
		if (this.absBugAlarm){
			extendAlarmStr = Converser.replace(extendAlarmStr,21,'1');
		}
		
		
		String extendStateStr = StringUtils.repeat("0", 32);
		
		if (this.passingLampSignal){
			extendStateStr = Converser.replace(extendStateStr,0,'1');
		}
		
		if (this.upperBeamSignal){
			extendStateStr = Converser.replace(extendStateStr,1,'1');
		}
		
		if (this.rSteeringLampSignal){
			extendStateStr = Converser.replace(extendStateStr,2,'1');
		}
		
		if (this.lSteeringLampSignal){
			extendStateStr = Converser.replace(extendStateStr,3,'1');
		}
		
		if (this.brakingSignal){
			extendStateStr = Converser.replace(extendStateStr,4,'1');
		}
		
		if (this.backGearSignal){
			extendStateStr = Converser.replace(extendStateStr,5,'1');
		}
		
		if (this.fogLampSignal){
			extendStateStr = Converser.replace(extendStateStr,6,'1');
		}
		
		if (this.makerLampSignal){
			extendStateStr = Converser.replace(extendStateStr,7,'1');
		}
		
		if (this.hornSignal){
			extendStateStr = Converser.replace(extendStateStr,8,'1');
		}
		
		if (this.airConditionerState){
			extendStateStr = Converser.replace(extendStateStr,9,'1');
		}
		
		if (this.neutralSignal){
			extendStateStr = Converser.replace(extendStateStr,10,'1');
		}
		
		if (this.retarderWorkingState){
			extendStateStr = Converser.replace(extendStateStr,11,'1');
		}
		
		if (this.absWorkingState){
			extendStateStr = Converser.replace(extendStateStr,12,'1');
		}
		
		if (this.heaterWorkingState){
			extendStateStr = Converser.replace(extendStateStr,13,'1');
		}
		
		if (this.dutchWorkingState){
			extendStateStr = Converser.replace(extendStateStr,14,'1');
		}
		
		String str = "21:"+Integer.valueOf(extendAlarmStr,2)+",502:"+Integer.valueOf(extendStateStr,2)+",";

		
		return str;
	}

	


}
