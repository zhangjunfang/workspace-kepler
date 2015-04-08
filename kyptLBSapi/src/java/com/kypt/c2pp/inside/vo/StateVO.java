package com.kypt.c2pp.inside.vo;

import org.apache.commons.lang.StringUtils;

import com.kypt.c2pp.util.Converser;

/*
 * 位置信息汇报     状态位定义信息
 */

public class StateVO {
	
	/**
	 * acc状态  TRUE:ACC开  FALSE:ACC关   默认：False
	 */
	private boolean accState=false;
	
	/**
	 * gps状态  TRUE:定位  FALSE:未定位
	 */
	private boolean gpsState=false;
	
	/**
	 * gps状态  TRUE:南纬  FALSE:北纬
	 */
	private boolean latState=false;
	
	/**
	 * 经度状态 TRUE:西经  FALSE:东经
	 */
	private boolean lonState=false;
	
	/**
	 * 运营|停运状态  TRUE:停运状态  FALSE:运营状态
	 */
	private boolean businessState=false;
	
	/**
	 * 坐标信息加密状态 TRUE:经纬度已经保密插件加密  FALSE:经纬度未经保密插件加密
	 */
	private boolean coorEncryptState=false;
	
	/**
	 * 油路状态 TRUE:车辆油路断开  FALSE:车辆油路正常
	 */
	private boolean oilAccessState=false;
	
	/**
	 * 电路状态  TRUE:车辆电路断开  FALSE:车辆电路正常
	 */
	private boolean circuitState=false;
	
	/**
	 * 车门锁状态 TRUE:车门加锁  FALSE:车门解锁
	 */
	private boolean doorLockState=false;
	
	/**
	 * 车门1开关状态 TRUE:车门1开启  FALSE:车门1关闭
	 */
	private boolean door1OpenState=false;
	
	/**
	 * 车门2开关状态 TRUE:车门2开启  FALSE:车门2关闭
	 */
	private boolean door2OpenState=false;
	
	/**
	 * 车门3开关状态 TRUE:车门3开启  FALSE:车门3关闭
	 */
	private boolean door3OpenState=false;
	
	/**
	 * 车门4开关状态 TRUE:车门4开启  FALSE:车门4关闭
	 */
	private boolean door4OpenState=false;

	public boolean isAccState() {
		return accState;
	}

	public void setAccState(boolean accState) {
		this.accState = accState;
	}

	public boolean isGpsState() {
		return gpsState;
	}

	public void setGpsState(boolean gpsState) {
		this.gpsState = gpsState;
	}

	public boolean isLatState() {
		return latState;
	}

	public void setLatState(boolean latState) {
		this.latState = latState;
	}

	public boolean isLonState() {
		return lonState;
	}

	public void setLonState(boolean lonState) {
		this.lonState = lonState;
	}

	public boolean isBusinessState() {
		return businessState;
	}

	public void setBusinessState(boolean businessState) {
		this.businessState = businessState;
	}

	public boolean isCoorEncryptState() {
		return coorEncryptState;
	}

	public void setCoorEncryptState(boolean coorEncryptState) {
		this.coorEncryptState = coorEncryptState;
	}

	public boolean isOilAccessState() {
		return oilAccessState;
	}

	public void setOilAccessState(boolean oilAccessState) {
		this.oilAccessState = oilAccessState;
	}

	public boolean isCircuitState() {
		return circuitState;
	}

	public void setCircuitState(boolean circuitState) {
		this.circuitState = circuitState;
	}

	public boolean isDoorLockState() {
		return doorLockState;
	}

	public void setDoorLockState(boolean doorLockState) {
		this.doorLockState = doorLockState;
	}

	public boolean isDoor1OpenState() {
		return door1OpenState;
	}

	public void setDoor1OpenState(boolean door1OpenState) {
		this.door1OpenState = door1OpenState;
	}

	public boolean isDoor2OpenState() {
		return door2OpenState;
	}

	public void setDoor2OpenState(boolean door2OpenState) {
		this.door2OpenState = door2OpenState;
	}

	public boolean isDoor3OpenState() {
		return door3OpenState;
	}

	public void setDoor3OpenState(boolean door3OpenState) {
		this.door3OpenState = door3OpenState;
	}

	public boolean isDoor4OpenState() {
		return door4OpenState;
	}

	public void setDoor4OpenState(boolean door4OpenState) {
		this.door4OpenState = door4OpenState;
	}
	
	public String toStringbak(){
		StringBuffer sb = new StringBuffer();
			if (this.accState){
				sb.append("2|");
			}else{
				sb.append("1|");
			}
			
			if (this.gpsState){
				sb.append("4|");
			}else{
				sb.append("3|");
			}
			
			if (this.latState){
				sb.append("6|");
			}else{
				sb.append("5|");
			}
			
			if (this.lonState){
				sb.append("8|");
			}else{
				sb.append("7|");
			}
			
			if (this.businessState){
				sb.append("10|");
			}else{
				sb.append("9|");
			}
			
			if (this.coorEncryptState){
				sb.append("12|");
			}else{
				sb.append("11|");
			}
			
			if (this.oilAccessState){
				sb.append("14|");
			}else{
				sb.append("13|");
			}
			
			if (this.circuitState){
				sb.append("16|");
			}else{
				sb.append("15|");
			}
			
			if (this.doorLockState){
				sb.append("18|");
			}else{
				sb.append("17|");
			}
			
			StringBuffer sb0 = new StringBuffer();
			
			if (this.door1OpenState){
				sb0.append("20|");
			}else{
				sb0.append("19|");
			}
			
			if (this.door2OpenState){
				sb0.append("22|");
			}else{
				sb0.append("21|");
			}
			
			if (this.door3OpenState){
				sb0.append("24|");
			}else{
				sb0.append("23|");
			}
			
			if (this.door4OpenState){
				sb0.append("26|");
			}else{
				sb0.append("25|");
			}
		String str = sb.toString();
		if (str.length()>1){
			str = "512:"+str.substring(0,str.length()-1)+",";
		}
		
		String str0 = sb0.toString();
		if (str0.length()>1){
			str0 = "502:"+str0.substring(0,str0.length()-1)+",";
		}
		
		if (this.accState){
			str0+="26:4,";
		}else{
			str0+="26:5,";
		} 
		
		
		return str+str0;
	}
	
	public String toString(){
		String stateStr = StringUtils.repeat("0", 32);
		if (this.accState){
			stateStr = Converser.replace(stateStr,0,'1');
		}
		if (this.gpsState){
			stateStr = Converser.replace(stateStr,1,'1');
		}
		if (this.latState){
			stateStr = Converser.replace(stateStr,2,'1');
		}
		if (this.lonState){
			stateStr = Converser.replace(stateStr,3,'1');
		}
		if (this.businessState){
			stateStr = Converser.replace(stateStr,4,'1');
		}
		if (this.coorEncryptState){
			stateStr = Converser.replace(stateStr,5,'1');
		}
		if (this.oilAccessState){
			stateStr = Converser.replace(stateStr,10,'1');
		}
		if (this.circuitState){
			stateStr = Converser.replace(stateStr,11,'1');
		}
		if (this.doorLockState){
			stateStr = Converser.replace(stateStr,12,'1');
		}
		if (this.door1OpenState){
			stateStr = Converser.replace(stateStr,13,'1');
		}
		if (this.door2OpenState){
			stateStr = Converser.replace(stateStr,14,'1');
		}
		if (this.door3OpenState){
			stateStr = Converser.replace(stateStr,15,'1');
		}
		if (this.door4OpenState){
			stateStr = Converser.replace(stateStr,16,'1');
		}
			
		int state = Integer.valueOf(stateStr,2);
		
		String str = "8:"+state+",";

		if (this.accState){
			str+="26:4,";
		}else{
			str+="26:5,";
		} 

		return str;
	}

}
