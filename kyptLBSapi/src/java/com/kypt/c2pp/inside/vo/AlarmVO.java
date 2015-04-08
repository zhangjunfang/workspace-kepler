package com.kypt.c2pp.inside.vo;

import org.apache.commons.lang.StringUtils;

import com.kypt.c2pp.util.Converser;

public class AlarmVO {
	
	/**
	 * 紧急报警，触动报警开关后触发
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean sosAlarm=false;
	
	/**
	 * 超速报警
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean overSpeedAlarm=false;
	
	/**
	 * 疲劳驾驶报警
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean fatigueAlarm=false;
	
	/**
	 * 预警
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean forecastAlarm=false;
	
	/**
	 * GNSS模块发生故障
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean gnssBugAlarm=false;
	
	/**
	 * GNSS天线未接或被剪断
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean gnssUnantenanAlarm=false;
	
	/**
	 * GNSS天线短路
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean antenanShortoutAlarm=false;
	
	/**
	 * 终端主电源欠压
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean mpowerUndervoltageAlarm=false;
	
	/**
	 * 终端主电源掉电
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean mpowerDownAlarm=false;
	
	/**
	 * 终端LCD或显示器故障
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean lcdBugAlarm=false;
	
	/**
	 * 终端TIS模块故障
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean tisBugAlarm=false;
	
	/**
	 * 终端摄像头故障
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean cameraBugAlarm=false;
	
	/**
	 * 当天累计驾驶超时
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean driverTimeoutAlarm=false;
	
	/**
	 * 超时停车
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean stopTimeoutAlarm=false;
	
	/**
	 * 进出区域
	 * true:报警  false:正常
	 * default:false
	 */
	//private boolean inoutAreaAlarm=false;
	
	/**
	 * 进出路线
	 * true:报警  false:正常
	 * default:false
	 */
	//private boolean inoutLineAlarm=false;
	
	/**
	 * 路段行驶时间过长
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean routeRunTooLongAlarm=false;
	
	/**
	 * 路段行驶时间不足
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean routeRunShortageAlarm=false;
	
	/**
	 * 路线偏离告警
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean deviateRouteAlarm=false;
	
	/**
	 * 车辆VSS故障
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean vssBugAlarm=false;
	
	/**
	 * 车辆油量异常
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean oilMassUnUsualAlarm=false;
	
	/**
	 * 车辆被盗（通过车辆防盗器）
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean vehicleBestolenAlarm=false;
	
	/**
	 * 车辆非法点火
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean illegalFireAlarm=false;
	
	/**
	 * 车辆非法位移
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean illegalMoveAlarm=false;
	
	/**
	 * 碰撞侧翻报警
	 * true:报警  false:正常
	 * default:false
	 */
	private boolean cashAlarm=false;
	
	/**
	 * 进区域路线告警
	 */
	private boolean inAreaLineAlarm=false;
	
	/**
	 * 出区域路线告警
	 */
	private boolean outAreaLineAlarm=false;
	
	/**
	 * 电子围栏位置类型 1圆形区域 2矩形区域 3多边形区域 4路段
	 * 当发生进出区告警时需附加此信息
	 */
	private String areaType;
	
	/**
	 * 电子围栏区域或线路ID  
	 * 当发生进出区告警时需附加此信息
	 */
	private String areaId;
	
	/**
	 * 超速区域位置类型 0无特定区域 1圆形区域 2矩形区域 3多边形区域 4路段
	 * 当发生超速告警时附加此信息
	 */
	private String overSpeedAreaType;
	
	/**
	 * 超速区域或路段ID
	 * 当发生超速告警时附加此信息
	 */
	private String overSpeedAreaId;
	
	/**
	 * 路段行驶时间不足或过长路段ID
	 * 当发生路段行驶时间不足或过长告警时附加此信息
	 */
	private String routeRunDiffAreaId;
	
	/**
	 * 路段行驶时间不足或过长 行驶时间
	 * 当发生路段行驶时间不足或过长告警时附加此信息
	 */
	private String routeRunTime;
	
	
	

	public boolean isSosAlarm() {
		return sosAlarm;
	}

	public void setSosAlarm(boolean sosAlarm) {
		this.sosAlarm = sosAlarm;
	}

	public boolean isOverSpeedAlarm() {
		return overSpeedAlarm;
	}

	public void setOverSpeedAlarm(boolean overSpeedAlarm) {
		this.overSpeedAlarm = overSpeedAlarm;
	}

	public boolean isFatigueAlarm() {
		return fatigueAlarm;
	}

	public void setFatigueAlarm(boolean fatigueAlarm) {
		this.fatigueAlarm = fatigueAlarm;
	}

	public boolean isForecastAlarm() {
		return forecastAlarm;
	}

	public void setForecastAlarm(boolean forecastAlarm) {
		this.forecastAlarm = forecastAlarm;
	}

	public boolean isGnssBugAlarm() {
		return gnssBugAlarm;
	}

	public void setGnssBugAlarm(boolean gnssBugAlarm) {
		this.gnssBugAlarm = gnssBugAlarm;
	}

	public boolean isGnssUnantenanAlarm() {
		return gnssUnantenanAlarm;
	}

	public void setGnssUnantenanAlarm(boolean gnssUnantenanAlarm) {
		this.gnssUnantenanAlarm = gnssUnantenanAlarm;
	}

	public boolean isAntenanShortoutAlarm() {
		return antenanShortoutAlarm;
	}

	public void setAntenanShortoutAlarm(boolean antenanShortoutAlarm) {
		this.antenanShortoutAlarm = antenanShortoutAlarm;
	}

	public boolean isMpowerUndervoltageAlarm() {
		return mpowerUndervoltageAlarm;
	}

	public void setMpowerUndervoltageAlarm(boolean mpowerUndervoltageAlarm) {
		this.mpowerUndervoltageAlarm = mpowerUndervoltageAlarm;
	}

	public boolean isMpowerDownAlarm() {
		return mpowerDownAlarm;
	}

	public void setMpowerDownAlarm(boolean mpowerDownAlarm) {
		this.mpowerDownAlarm = mpowerDownAlarm;
	}

	public boolean isLcdBugAlarm() {
		return lcdBugAlarm;
	}

	public void setLcdBugAlarm(boolean lcdBugAlarm) {
		this.lcdBugAlarm = lcdBugAlarm;
	}

	public boolean isTisBugAlarm() {
		return tisBugAlarm;
	}

	public void setTisBugAlarm(boolean tisBugAlarm) {
		this.tisBugAlarm = tisBugAlarm;
	}

	public boolean isCameraBugAlarm() {
		return cameraBugAlarm;
	}

	public void setCameraBugAlarm(boolean cameraBugAlarm) {
		this.cameraBugAlarm = cameraBugAlarm;
	}

	public boolean isDriverTimeoutAlarm() {
		return driverTimeoutAlarm;
	}

	public void setDriverTimeoutAlarm(boolean driverTimeoutAlarm) {
		this.driverTimeoutAlarm = driverTimeoutAlarm;
	}

	public boolean isStopTimeoutAlarm() {
		return stopTimeoutAlarm;
	}

	public void setStopTimeoutAlarm(boolean stopTimeoutAlarm) {
		this.stopTimeoutAlarm = stopTimeoutAlarm;
	}

	/*public boolean isInoutAreaAlarm() {
		return inoutAreaAlarm;
	}

	public void setInoutAreaAlarm(boolean inoutAreaAlarm) {
		this.inoutAreaAlarm = inoutAreaAlarm;
	}

	public boolean isInoutLineAlarm() {
		return inoutLineAlarm;
	}

	public void setInoutLineAlarm(boolean inoutLineAlarm) {
		this.inoutLineAlarm = inoutLineAlarm;
	}*/

	public boolean isDeviateRouteAlarm() {
		return deviateRouteAlarm;
	}

	public void setDeviateRouteAlarm(boolean deviateRouteAlarm) {
		this.deviateRouteAlarm = deviateRouteAlarm;
	}

	public boolean isVssBugAlarm() {
		return vssBugAlarm;
	}

	public void setVssBugAlarm(boolean vssBugAlarm) {
		this.vssBugAlarm = vssBugAlarm;
	}

	public boolean isOilMassUnUsualAlarm() {
		return oilMassUnUsualAlarm;
	}

	public void setOilMassUnUsualAlarm(boolean oilMassUnUsualAlarm) {
		this.oilMassUnUsualAlarm = oilMassUnUsualAlarm;
	}

	public boolean isVehicleBestolenAlarm() {
		return vehicleBestolenAlarm;
	}

	public void setVehicleBestolenAlarm(boolean vehicleBestolenAlarm) {
		this.vehicleBestolenAlarm = vehicleBestolenAlarm;
	}

	public boolean isIllegalFireAlarm() {
		return illegalFireAlarm;
	}

	public void setIllegalFireAlarm(boolean illegalFireAlarm) {
		this.illegalFireAlarm = illegalFireAlarm;
	}

	public boolean isIllegalMoveAlarm() {
		return illegalMoveAlarm;
	}

	public void setIllegalMoveAlarm(boolean illegalMoveAlarm) {
		this.illegalMoveAlarm = illegalMoveAlarm;
	}

	public boolean isCashAlarm() {
		return cashAlarm;
	}

	public void setCashAlarm(boolean cashAlarm) {
		this.cashAlarm = cashAlarm;
	}
	
	public boolean isRouteRunTooLongAlarm() {
		return routeRunTooLongAlarm;
	}

	public void setRouteRunTooLongAlarm(boolean routeRunTooLongAlarm) {
		this.routeRunTooLongAlarm = routeRunTooLongAlarm;
	}

	public boolean isRouteRunShortageAlarm() {
		return routeRunShortageAlarm;
	}

	public void setRouteRunShortageAlarm(boolean routeRunShortageAlarm) {
		this.routeRunShortageAlarm = routeRunShortageAlarm;
	}

	public boolean isInAreaLineAlarm() {
		return inAreaLineAlarm;
	}

	public void setInAreaLineAlarm(boolean inAreaLineAlarm) {
		this.inAreaLineAlarm = inAreaLineAlarm;
	}

	public boolean isOutAreaLineAlarm() {
		return outAreaLineAlarm;
	}

	public void setOutAreaLineAlarm(boolean outAreaLineAlarm) {
		this.outAreaLineAlarm = outAreaLineAlarm;
	}

	public String getAreaType() {
		return areaType;
	}

	public void setAreaType(String areaType) {
		this.areaType = areaType;
	}

	public String getAreaId() {
		return areaId;
	}

	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}

	public String getOverSpeedAreaType() {
		return overSpeedAreaType;
	}

	public void setOverSpeedAreaType(String overSpeedAreaType) {
		this.overSpeedAreaType = overSpeedAreaType;
	}

	public String getOverSpeedAreaId() {
		return overSpeedAreaId;
	}

	public void setOverSpeedAreaId(String overSpeedAreaId) {
		this.overSpeedAreaId = overSpeedAreaId;
	}

	public String getRouteRunDiffAreaId() {
		return routeRunDiffAreaId;
	}

	public void setRouteRunDiffAreaId(String routeRunDiffAreaId) {
		this.routeRunDiffAreaId = routeRunDiffAreaId;
	}

	public String getRouteRunTime() {
		return routeRunTime;
	}

	public void setRouteRunTime(String routeRunTime) {
		this.routeRunTime = routeRunTime;
	}
	
	public String toStringBak(){
		StringBuffer sb=new StringBuffer();
		
		if (this.sosAlarm){
			sb.append("20:0,");
		}
		
		if (this.overSpeedAlarm){
			sb.append("20:41,");
			sb.append("32:"+this.overSpeedAreaType+"|"+this.overSpeedAreaId+"|2,");
		}else{
			sb.append("20:76,");
		}
		
		if (this.fatigueAlarm){
			sb.append("20:10,");
		}else{
			sb.append("20:67,");
		}
		
		if (this.forecastAlarm){
			sb.append("20:4,");
		}
		
		if (this.gnssBugAlarm){
			sb.append("20:5,");
		}else{
			sb.append("20:62,");
		}
		
		if (this.gnssUnantenanAlarm){
			sb.append("20:6,");
		}else{
			sb.append("20:63,");
		}
		
		if (this.antenanShortoutAlarm){
			sb.append("20:14,");
		}else{
			sb.append("20:68,");
		}
		
		if (this.mpowerUndervoltageAlarm){
			sb.append("20:7,");
		}else{
			sb.append("20:64,");
		}
		
		if (this.mpowerDownAlarm){
			sb.append("20:8,");
		}else{
			sb.append("20:65,");
		}
		
		if (this.lcdBugAlarm){
			sb.append("20:9,");
		}else{
			sb.append("20:66,");
		}
		
		if (this.tisBugAlarm){
			sb.append("20:16,");
		}else{
			sb.append("20:69,");
		}
		
		if (this.cameraBugAlarm){
			sb.append("20:17,");
		}else{
			sb.append("20:70,");
		}
		
		if (this.driverTimeoutAlarm){
			sb.append("20:18,");
		}else{
			sb.append("20:71,");
		}
		
		if (this.stopTimeoutAlarm){
			sb.append("20:43,");
		}else{
			sb.append("20:78,");
		}
		
		if (this.routeRunShortageAlarm){
			sb.append("20:22,");
			sb.append("32:4|"+this.routeRunDiffAreaId+"|4|"+this.routeRunTime+",");
		}
		
		if (this.routeRunTooLongAlarm){
			sb.append("20:22 ");
			sb.append("32:4|"+this.routeRunDiffAreaId+"|5|"+this.routeRunTime+",");
		}
		
		
		if (this.deviateRouteAlarm){
			sb.append("20:210,");
		}else{
			sb.append("20:213,");
		}
		
		if (this.vssBugAlarm){
			sb.append("20:24,");
		}else{
			sb.append("20:73,");
		}
		
		if (this.oilMassUnUsualAlarm){
			sb.append("20:25,");
		}else{
			sb.append("20:74,");
		}
		
		if (this.vehicleBestolenAlarm){
			sb.append("20:26,");
		}else{
			sb.append("20:75,");
		}
		
		if (this.illegalFireAlarm){
			sb.append("20:31,");
		}
		
		if (this.illegalMoveAlarm){
			sb.append("20:28,");
		}
		
		if (this.cashAlarm){
			sb.append("20:23,");
		}else{
			sb.append("20:72,");
		}
		
		if (this.inAreaLineAlarm){
			sb.append("20:200,");
			sb.append("32:"+this.areaType+"|"+this.areaId+"|0,");
		}
		
		if (this.outAreaLineAlarm){
			sb.append("20:201,");
			sb.append("32:"+this.areaType+"|"+this.areaId+"|1,");
		}
		
		
		return sb.toString();
	}
	
	public String toString(){
		StringBuffer sb = new StringBuffer();
		String alarmStr = StringUtils.repeat("0", 32);
		if (this.sosAlarm){
			alarmStr = Converser.replace(alarmStr,0,'1');
		}
		
		if (this.overSpeedAlarm){
			alarmStr = Converser.replace(alarmStr,1,'1');
			if ("0".equals(this.overSpeedAreaType)){
				sb.append("31:"+this.overSpeedAreaType+"|,");
			}else{
				sb.append("31:"+this.overSpeedAreaType+"|"+this.overSpeedAreaId+",");
			}
			
		}
		
		if (this.fatigueAlarm){
			alarmStr = Converser.replace(alarmStr,2,'1');
		}
		
		if (this.forecastAlarm){
			alarmStr = Converser.replace(alarmStr,3,'1');
		}
		
		if (this.gnssBugAlarm){
			alarmStr = Converser.replace(alarmStr,4,'1');
		}
		
		if (this.gnssUnantenanAlarm){
			alarmStr = Converser.replace(alarmStr,5,'1');
		}
		
		if (this.antenanShortoutAlarm){
			alarmStr = Converser.replace(alarmStr,6,'1');
		}
		
		if (this.mpowerUndervoltageAlarm){
			alarmStr = Converser.replace(alarmStr,7,'1');;
		}
		
		if (this.mpowerDownAlarm){
			alarmStr = Converser.replace(alarmStr,8,'1');
		}
		
		if (this.lcdBugAlarm){
			alarmStr = Converser.replace(alarmStr,9,'1');
		}
		
		if (this.tisBugAlarm){
			alarmStr = Converser.replace(alarmStr,10,'1');
		}
		
		if (this.cameraBugAlarm){
			alarmStr = Converser.replace(alarmStr,11,'1');
		}
		
		if (this.driverTimeoutAlarm){
			alarmStr = Converser.replace(alarmStr,18,'1');
		}
		
		if (this.stopTimeoutAlarm){
			alarmStr = Converser.replace(alarmStr,19,'1');
		}
		
		if (this.routeRunShortageAlarm){
			alarmStr = Converser.replace(alarmStr,22,'1');
			sb.append("35:"+this.routeRunDiffAreaId+"|"+this.routeRunTime+"|0,");
		}
		
		if (this.routeRunTooLongAlarm){
			alarmStr = Converser.replace(alarmStr,22,'1');
			sb.append("35:"+this.routeRunDiffAreaId+"|"+this.routeRunTime+"|1,");
		}
		
		
		if (this.deviateRouteAlarm){
			alarmStr = Converser.replace(alarmStr,23,'1');
		}
		
		if (this.vssBugAlarm){
			alarmStr = Converser.replace(alarmStr,24,'1');
		}
		
		if (this.oilMassUnUsualAlarm){
			alarmStr = Converser.replace(alarmStr,25,'1');
		}
		
		if (this.vehicleBestolenAlarm){
			alarmStr = Converser.replace(alarmStr,26,'1');
		}
		
		if (this.illegalFireAlarm){
			alarmStr = Converser.replace(alarmStr,27,'1');
		}
		
		if (this.illegalMoveAlarm){
			alarmStr = Converser.replace(alarmStr,28,'1');
		}
		
		if (this.cashAlarm){
			alarmStr = Converser.replace(alarmStr,29,'1');
		}
		
		if (this.inAreaLineAlarm){
			if (!"4".equals(this.areaType)){
				alarmStr = Converser.replace(alarmStr,20,'1');
			}else{
				alarmStr = Converser.replace(alarmStr,21,'1');
			}
			sb.append("32:"+this.areaType+"|"+this.areaId+"|0,");
		}
		
		if (this.outAreaLineAlarm){
			if (!"4".equals(this.areaType)){
				alarmStr = Converser.replace(alarmStr,20,'1');
			}else{
				alarmStr = Converser.replace(alarmStr,21,'1');
			}
			sb.append("32:"+this.areaType+"|"+this.areaId+"|1,");
		}
		
		return "20:"+Integer.valueOf(alarmStr,2)+","+sb.toString();
	}

}
