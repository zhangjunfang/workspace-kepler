package com.demo.bean;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.inside.msg.resp.DownTextResp;
import com.kypt.c2pp.inside.msg.resp.LocationReportResp;
import com.kypt.c2pp.inside.msg.resp.PhotoGraphResp;
import com.kypt.c2pp.inside.vo.AlarmVO;
import com.kypt.c2pp.inside.vo.CanVO;
import com.kypt.c2pp.inside.vo.LocInfoVO;
import com.kypt.c2pp.inside.vo.StateVO;
import com.kypt.c2pp.inside.vo.SwitchValueVO;
import com.kypt.c2pp.util.ValidationUtil.GENERAL_STATUS;

public class BuildUpCommand {

	private static Logger log = LoggerFactory.getLogger(BuildUpCommand.class);

	public static final String num3 = "3";

	public static final BuildUpCommand buildUpCommand = new BuildUpCommand();

	private BuildUpCommand() {
	}

	public static BuildUpCommand getInstance() {
		return buildUpCommand;
	}

	/**
	 * 拼装实时数据上传监管平台字符串
	 * 
	 * @param urt
	 * @return
	 * @throws ParseException 
	 */
	public LocationReportResp buildUpCommandString(Up_InfoContent urt) throws ParseException {
		StringBuilder sb = new StringBuilder();
		
		StateVO stateVO = new StateVO();
		
		if (urt.getFire_up_state() != null
				&& urt.getFire_up_state().equals("1")) {
			stateVO.setAccState(true);//acc状态 1 开=点火状态 
		}
		
		if (urt.getGps_valid() != null && urt.getGps_valid().equals("1")){
			stateVO.setGpsState(true);
		}
		
		AlarmVO alarmVO = new AlarmVO();
		if (urt.getSos() != null && !urt.getSos().equals("")) {
			alarmVO.setSosAlarm(true);
		}
		
		if (urt.getOverspeed_alert() != null
				&& urt.getOverspeed_alert().equals("2")) {
			alarmVO.setOverSpeedAlarm(true);
			alarmVO.setOverSpeedAreaType("2");
			alarmVO.setOverSpeedAreaId(urt.getRegion_id());
		}
		
		if (urt.getFatigue_alert() != null
				&& urt.getFatigue_alert().equals("2")) {
			alarmVO.setFatigueAlarm(true);
		}
		
		LocInfoVO locVO = new LocInfoVO();
		
		if (urt.getLatitude() != null && !urt.getLatitude().equals("")&& !urt.getLatitude().equals("FFFF")) {
			locVO.setLat(urt.getLatitude().trim());
		}
		
		if (urt.getLongitude() != null && !urt.getLongitude().equals("")&& !urt.getLongitude().equals("FFFF")) {
			locVO.setLon(urt.getLongitude().trim());
		}
		
		//高程取整
		if (urt.getElevation() != null && !urt.getElevation().equals("")&& !urt.getElevation().equals("FFFF")) {
			String elevation=urt.getElevation().trim();
			double d=Double.parseDouble(elevation);
			locVO.setElevation(""+Math.round(d));
		}else{
			locVO.setDirection("0");
		}
		
		//方向取整
		if (urt.getDirection() != null && !urt.getDirection().equals("")&& !urt.getDirection().equals("FFFF")) {
			String direction=urt.getDirection().trim();
			double d=Double.parseDouble(direction);
			locVO.setDirection(""+Math.round(d));
		}else{
			locVO.setDirection("0");
		}
		
		//速度乘10取整
		if (urt.getGps_speeding() != null && !urt.getGps_speeding().equals("")&& !urt.getGps_speeding().equals("FFFF")) {
			String gpsspeed=urt.getGps_speeding().trim();
			double d=Double.parseDouble(gpsspeed);
			d = d*10;
			locVO.setGpsSpeed(""+Math.round(d));
		}
		
		if (urt.getTerminal_time() != null
				&& !urt.getTerminal_time().equals("")) {
			DateFormat format = new SimpleDateFormat("yyMMddHHmmss");
//			System.out.println("da 1:"+urt.getTerminal_time().trim());
			Date dt = format.parse(urt.getTerminal_time().trim());
//			System.out.println("da 2:"+urt.getTerminal_time().trim());
//			System.out.println("da 2:"+dt);
			locVO.setTerminalTime(dt);
		}
		
		CanVO canVO = new CanVO();
		
		//里程乘10取整
		if (urt.getMileage() != null && !urt.getMileage().equals("")&& !urt.getMileage().equals("FFFF")) {
			String mileage=urt.getMileage().trim();
			double d=Double.parseDouble(mileage);
			d = d*10;
			canVO.setMileage(""+Math.round(d));
		}
		//油量
//		if (urt.getMileage() != null && !urt.getMileage().equals("")) {
//			canVO.setMileage(urt.getMileage().trim());
//		}
		
		//脉冲车速乘10取整
		if(urt.getSpeeding()!=null&&!urt.getSpeeding().equals("")&&!urt.getSpeeding().equals("FFFF")){
			String speed=urt.getSpeeding().trim();
			double d=Double.parseDouble(speed);
			d = d*10;
			canVO.setVehicleSpeed(""+Math.round(d));
		}
		
		//发动机转速 1bit=0.125rpm，0=0rpm
		if (urt.getEngine_rotate_speed() != null
				&& !urt.getEngine_rotate_speed().equals("")&& !urt.getEngine_rotate_speed().equals("FFFF")) {
			String rotate = urt.getEngine_rotate_speed().trim();
			double d=Double.parseDouble(rotate);
			d = d*(1000/125);
			canVO.setEngineRotateSpeed(""+Math.round(d));
		}
		
		//瞬时油耗，1bit=0.05L/h,0=0L/h
		if (urt.getOil_instant() != null
				&& !urt.getOil_instant().equals("")&& !urt.getOil_instant().equals("FFFF")) {
			String oilinstant = urt.getOil_instant().trim();
			double d=Double.parseDouble(oilinstant);
			d = d*(100/5);
			canVO.setOilInstant(""+Math.round(d));
		}
		
		//发动机扭矩百分比，1bit=1%，0=-125%
		if (urt.getE_torque() != null && !urt.getE_torque().equals("")&& !urt.getE_torque().equals("FFFF")) {
			String etorque = urt.getE_torque().trim();
			double d=Double.parseDouble(etorque);
			d = d+125;
			canVO.seteTorque(""+Math.round(d));
		}
		
		//油门踏板位置，1bit=0.4%，0=0%
		if(urt.getEcc_app()!=null&&!urt.getEcc_app().equals("")&&!urt.getEcc_app().equals("FFFF")){
			String oilinstant = urt.getOil_instant().trim();
			double d=Double.parseDouble(oilinstant);
			d = d*(10/4);
			canVO.setEecApp(""+Math.round(d));
		}
		
		//累计油耗，1bit=0.5L,0=0L
		if (urt.getOil_total() != null && !urt.getOil_total().equals("")&& !urt.getOil_total().equals("FFFF")) {
			String oilinstant = urt.getOil_instant().trim();
			double d=Double.parseDouble(oilinstant);
			d = d*(10/5);
			canVO.setOilTotal(""+Math.round(d));
		}
		
		//发动机运行时长
		
		//终端内置电池电压1bit=0.1V, 0=0V
		if (urt.getBattery_voltage() != null
				&& !urt.getBattery_voltage().equals("")&& !urt.getBattery_voltage().equals("FFFF")) {
			String batteryVoltage = urt.getBattery_voltage().trim();
			double d=Double.parseDouble(batteryVoltage);
			d = d*(10/1);
			canVO.setBatteryVoltage(""+Math.round(d));
		}
		
		//蓄电池电压1bit=0.1V, 0=0V
		if (urt.getExt_voltage() != null && !urt.getExt_voltage().equals("")&& !urt.getExt_voltage().equals("FFFF")) {
			String extVoltage = urt.getExt_voltage().trim();
			double d=Double.parseDouble(extVoltage);
			d = d*(10/1);
			canVO.setExtVoltage(""+Math.round(d));
		}
		
		//发动机水温1bit=1℃，0=-40℃
		if (urt.getE_water_temp() != null && !urt.getE_water_temp().equals("")&& !urt.getE_water_temp().equals("FFFF")) {
			String waterTemp = urt.getE_water_temp().trim();
			double d=Double.parseDouble(waterTemp);
			d = d+40;
			canVO.seteWaterTemp(""+Math.round(d));
		}
		
		//机油温度
		
		//机油压力1bit=4Kpa，0=0Kpa
		if (urt.getOil_pressure() != null && !urt.getOil_pressure().equals("")&& !urt.getOil_pressure().equals("FFFF")) {
			String oilPressure = urt.getOil_pressure().trim();
			double d=Double.parseDouble(oilPressure);
			d = d*(1/4);
			canVO.setOilPressure(""+Math.round(d));
		}
		
		//点火状态
		if (urt.getFire_up_state() != null
				&& urt.getFire_up_state().equals("1")) {
			
		}
		
		//供电状态
		if (urt.getPower_state() != null && !urt.getPower_state().equals("")) {
			
		}
		
		/*if(urt.getVin_speed()!=null&&!urt.getVin_speed().equals("")){
			canVO.setVehicleSpeed(urt.getVin_speed().trim());
		}*/
		
		//发动机运行时长
		
		//大气压力
		
		//进气温度
		
		
		SwitchValueVO switchVO = new SwitchValueVO();
		
		if (urt.getOn_off() != null && !urt.getOn_off().equals("")) {
			char onOff[] = urt.getOn_off().trim().toCharArray();
			
			if (onOff[7]=='1'){
				switchVO.setFogLampSignal(true);
			}
			
			if (onOff[6]=='1'){
				switchVO.setUpperBeamSignal(true);
			}
			
			if (onOff[5]=='1'){
				switchVO.setrSteeringLampSignal(true);
			}
			
			if (onOff[4]=='1'){
				switchVO.setHornSignal(true);
			}
			
			if (onOff[3]=='1'){
				switchVO.setlSteeringLampSignal(true);
			}
			
			if (onOff[2]=='1'){
				switchVO.setBackGearSignal(true);
			}
			
			if (onOff[1]=='1'){
				switchVO.setPassingLampSignal(true);
			}
			
			if (onOff[0]=='1'){
				switchVO.setBrakingSignal(true);
			}
			
			if (onOff[15]=='1'){
				switchVO.setSeriousBugAlarm(true);
			}
			
			if (onOff[14]=='1'){
				switchVO.setBrakeAirPressureAlarm(true);
			}
			
			if (onOff[13]=='1'){
				switchVO.setOilPressureAlarm(true);
			}
			
			
			/*if (onOff[12]=='1'){
				switchVO.setBackGearSignal(true);
			}*/
			
			if (onOff[11]=='1'){
				switchVO.setStageLowAlarm(true);
			}
			
			if (onOff[10]=='1'){
				switchVO.setBrakeShoeAlarm(true);
			}
			
			if (onOff[9]=='1'){
				switchVO.setAirFilterClogAlarm(true);
			}
			
			/*if (onOff[15]=='1'){
				switchVO.setBackGearSignal(true);
			}*/
			
			if (onOff[23]=='1'){
				switchVO.setOilAlarm(true);
			}
			
			if (onOff[22]=='1'){
				switchVO.setAirConditionerState(true);
			}
			
			if (onOff[21]=='1'){
				switchVO.setNeutralSignal(true);
			}
			
			/*if (onOff[19]=='1'){
				switchVO.setBackGearSignal(true);
			}
			
			if (onOff[20]=='1'){
				switchVO.setBackGearSignal(true);
			}*/
			
			if (onOff[18]=='1'){
				switchVO.setRetarderWorkingState(true);
			}
			
			if (onOff[17]=='1'){
				switchVO.setAbsWorkingState(true);
			}
			
			if (onOff[16]=='1'){
				switchVO.setHeaterWorkingState(true);
			}
			
			if (onOff[31]=='1'){
				switchVO.setDutchWorkingState(true);
			}
			
			if (onOff[30]=='1'){
				switchVO.setRetarderHtAlarm(true);
			}
			
			if (onOff[29]=='1'){
				switchVO.setEhousingHtAlarm(true);
			}
			
			if (onOff[28]=='1'){
				switchVO.setMwereBlockingAlarm(true);
			}
			
			if (onOff[27]=='1'){
				switchVO.setFuelBlockingAlarm(true);
			}
			
			if (onOff[26]=='1'){
				switchVO.setEoilTemperatureAlarm(true);
			}
			
		}
		
		locVO.setStateVO(stateVO);
		locVO.setAlarmVO(alarmVO);
		locVO.setCanVO(canVO);
		locVO.setSwitchValueVO(switchVO);
		
		LocationReportResp resp = new LocationReportResp();
		resp.setLocInfoVO(locVO);
		resp.setDeviceNo(urt.getCellphone());
		resp.setCommType("0");
		return resp;
	}
	
	/**
	 * 拼装监管平台下行短消息的回复信息
	 * @param urt
	 * @return
	 * @throws ParseException
	 */
	public DownTextResp buildDownTextRespString(String status,String respSeqId) throws ParseException {
		DownTextResp resp = new DownTextResp();
		resp.setSeq("");
		String macId = "";
		resp.setDeviceNo(macId==null?"":macId.substring(macId.indexOf("_")+1));
		resp.setCommType("0");
		if (status.equals("1")){
			resp.setStatus(GENERAL_STATUS.success);
		}else{
			resp.setStatus(GENERAL_STATUS.failure);
		}
		return resp;
	}
	
	/**
	 * 拼装监管平台下行短消息的回复信息
	 * @param urt
	 * @return
	 * @throws ParseException
	 */
	public PhotoGraphResp buildPhotoGraphRespString(String status,String sendCmdBean) throws ParseException {
		PhotoGraphResp resp = new PhotoGraphResp();
		resp.setSeq("");
		String macId = "";
		resp.setDeviceNo(macId==null?"":macId.substring(macId.indexOf("_")+1));
		resp.setCommType("0");
		if (status.equals("1")){
			resp.setStatus(GENERAL_STATUS.success);
		}else{
			resp.setStatus(GENERAL_STATUS.failure);
		}
		return resp;
	}
	
	
	
}

