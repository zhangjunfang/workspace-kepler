package com.caits.analysisserver.bean;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;



public class VehicleStatus{
	
	private String vid;
	
	private long statDate = 0;//统计日期
	
	private long maxSpeed = 0; // 本日最高车速
	
	private long maxRpm = 0; // 本日最高发动机转速
	
	private long mileage = 0; // 当日行驶里程
	
	private long accTime = 0; //ACC开时长
	
	private int accCount = 0; // ACC开次数
	
	private long engineTime = 0; //当日发动机运行时长
	
	private long costOil = 0; // 当日油耗

	private int countAlarm = 0; // 报警总数

	private int postCountAlarm = 0; // 报警总处理数

	private int overSpeed = 0; // 超速报警总数

	private int overSpeedOil = 0; // 本日超速下油耗

	private int overSpeedMileage = 0; // 本日超速下行驶里程
	
	private int door1 = 0; // 门1
	private int door2 = 0; // 门2
	private int door3 = 0; // 门3
	private int door4 = 0; // 其他门开启 
	
	private int countGPSValid = 0; // 定位量 定位有效数量
	private int countGPSInvalid = 0; //定位无效数量
	
	private int countLatLonInvalid = 0; //经纬度无效数量
	
	private long onOffLine = 0; // 车辆在线时长
	
	private int gpsTimeInvild = 0; // GPS时间无效数量
	
	private long runningTime = 0; // 行车时间
	private long idlingTime = 0; // 怠速时间
	
	private int waterTNum = 0; // 冷却液温度告警次数
	
	private long wateTTime = 0; // 冷却液温度告警时长
	
	private int batteryNum = 0; // 蓄电池电压告警
	
	private long batteryTime = 0; // 蓄电池电压告警时长
	
	private int areaOpenDoorNum = 0; // 区域内开关门次数
	
	private long areaOpenDoorTime = 0; // 区域内开门时长
	
	private int runCount = 0; // 行驶时间不足次数
	
	private int runDiffCount = 0;  // 行驶时间过长次数
	
	private long last_brake_shoe_time = 0; //制动蹄片磨损最近报警时间
	
	private long last_eoil_pressure_time = 0; // 机油压力最近报警时间
	
	private long last_e_water_temp_time = 0; // 冷却液温度最近报警时间
	
	private long last_trig_pressure_time = 0; //制动气压最近报警时间
	
	private long last_stage_low_alarm_time = 0; // 水位低最近报警时间
	
	private long last_retarder_ht_alarm_time = 0; // 缓速器高温最近报警时间
	
	private long last_ehouing_ht_alarm_time = 0; // 仓温最近报警时间
	
	private long last_battery_voltage_time = 0; // 蓄电池电压低最近报警时间
	
	private long last_fuel_blocking_alarm_time = 0; // 燃油滤清最近报警时间(燃油堵塞告警)
	
	private long runningOil = 0; // 行车油耗
	
	private long opening_door_ex_num = 0;
	
	private long precise_oil =0;
	
	private long metRunningOil =0;
	
	private long ecuOil =0;
	
	private long ecuRunningOil =0;
	
	private long point_milege = 0; // 最后一个点减第一个点计算里程
	
	private long gis_milege = 0; // GIS道路匹配计算里程
	
	private long point_oil = 0; // 最后一个点减第一个点计算油耗
	
	private Map<String, VehicleAlarm> alarmList = new HashMap<String,VehicleAlarm>();
	
	private Map<String,VehicleAlarm> stateCountMap =  new ConcurrentHashMap<String, VehicleAlarm>();
	
	public long getPrecise_oil() {
		return precise_oil;
	}

	public void addPrecise_oil(long preciseOil) {
		this.precise_oil = preciseOil + this.precise_oil;
	}

	
	public VehicleStatus(){
	}

	public int getAreaOpenDoorNum() {
		return areaOpenDoorNum;
	}
	
	public void setAreaOpenDoorNum(int areaOpenDoorNum) {
		this.areaOpenDoorNum = areaOpenDoorNum;
	}

	public void addAreaOpenDoorNum(int areaOpenDoorNum) {
		this.areaOpenDoorNum = areaOpenDoorNum + this.areaOpenDoorNum;
	}

	public long getAreaOpenDoorTime() {
		return areaOpenDoorTime;
	}
	
	public void setAreaOpenDoorTime(long areaOpenDoorTime) {
		this.areaOpenDoorTime = areaOpenDoorTime;
	}

	public void addAreaOpenDoorTime(long areaOpenDoorTime) {
		this.areaOpenDoorTime = areaOpenDoorTime + this.areaOpenDoorTime;
	}

	public int getBatteryNum() {
		return batteryNum;
	}

	public void addBatteryNum(int batteryNum) {
		this.batteryNum = batteryNum + this.batteryNum;
	}

	public long getBatteryTime() {
		return batteryTime;
	}

	public void addBatteryTime(long batteryTime) {
		this.batteryTime = batteryTime + this.batteryTime;
	}

	public int getWaterTNum() {
		return waterTNum;
	}

	public void addWaterTNum(int waterTNum) {
		this.waterTNum = waterTNum + this.waterTNum;
	}

	public long getWateTTime() {
		return wateTTime;
	}

	public void addWateTTime(long wateTTime) {
		this.wateTTime = wateTTime + this.wateTTime;
	}

	public int getGpsTimeInvild() {
		return gpsTimeInvild;
	}

	public void setGpsTimeInvild(int gpsTimeInvild) {
		this.gpsTimeInvild = gpsTimeInvild;
	}

	public long getIdlingTime() {
		return idlingTime;
	}

	public void setIdlingTime(long idlingTime) {
		this.idlingTime = idlingTime;
	}

	public long getOnOffLine() {
		return onOffLine;
	}

	public void setOnOffLine(long onOffLine) {
		this.onOffLine = onOffLine;
	}

	public int getCountLatLonInvalid() {
		return countLatLonInvalid;
	}

	public void addCountLatLonInvalid(int countLatLonInvalid) {
		this.countLatLonInvalid = countLatLonInvalid + this.countLatLonInvalid;
	}

	public int getCountGPSValid() {
		return countGPSValid;
	}

	public void addCountGPSValid(int countGPSValid) {
		this.countGPSValid = countGPSValid + this.countGPSValid;
	}

	public int getCountGPSInvalid() {
		return countGPSInvalid;
	}

	public void addCountGPSInvalid(int countGPSInvalid) {
		this.countGPSInvalid = countGPSInvalid + this.countGPSInvalid;
	}

	public int getDoor1() {
		return door1;
	}
	
	public void setDoor1(int door1) {
		this.door1 = door1;
	}

	public void addDoor1(int door1) {
		this.door1 = door1 + this.door1;
	}

	public int getDoor2() {
		return door2;
	}
	
	public void setDoor2(int door2) {
		this.door2 = door2;
	}

	public void addDoor2(int door2) {
		this.door2 = door2 + this.door2;
	}

	public int getDoor3() {
		return door3;
	}
	
	public void setDoor3(int door3) {
		this.door3 = door3;
	}

	public void addDoor3(int door3) {
		this.door3 = door3 + this.door3;
	}

	public int getDoor4() {
		return door4;
	}
	
	public void setDoor4(int door4) {
		this.door4 = door4;
	}

	public void addDoor4(int door4) {
		this.door4 = door4 + this.door4;
	}

	public int getCountAlarm() {
		return countAlarm;
	}

	public void addCountAlarm(int countAlarm) {
		this.countAlarm = countAlarm + this.countAlarm;
	}

	public int getPostCountAlarm() {
		return postCountAlarm;
	}

	public void setPostCountAlarm(int postCountAlarm) {
		this.postCountAlarm = postCountAlarm;
	}

	public int getOverSpeed() {
		return overSpeed;
	}

	public void addOverSpeed(int overSpeed) {
		this.overSpeed = overSpeed + this.overSpeed;
	}

	public int getOverSpeedOil() {
		return overSpeedOil;
	}

	public void addOverSpeedOil(int overSpeedOil) {
		this.overSpeedOil = overSpeedOil + this.overSpeedOil;
	}

	public int getOverSpeedMileage() {
		return overSpeedMileage;
	}

	public void addOverSpeedMileage(int overSpeedMileage) {
		this.overSpeedMileage = overSpeedMileage + this.overSpeedMileage;
	}

	public Map<String, VehicleAlarm> getAlarmList() {
		return alarmList;
	}

	public void setAlarmList(Map<String, VehicleAlarm> alarmList) {
		this.alarmList = alarmList;
	}

	public long getMaxSpeed() {
		return maxSpeed;
	}

	public void setMaxSpeed(long maxSpeed) {
		this.maxSpeed = maxSpeed;
	}

	public long getMaxRpm() {
		return maxRpm;
	}

	public void setMaxRpm(long maxRpm) {
		this.maxRpm = maxRpm;
	}

	public long getMileage() {
		return mileage;
	}

	public void addMileage(long mileage) {
		this.mileage = this.mileage + mileage;
	}

	public long getAccTime() {
		return accTime;
	}

	public void setAccTime(long accTime) {
		this.accTime = accTime;
	}

	public int getAccCount() {
		return accCount;
	}

	public void setAccCount(int accCount) {
		this.accCount = accCount;
	}

	public long getEngineTime() {
		return engineTime;
	}

	public void setEngineTime(long engineTime) {
		this.engineTime = engineTime;
	}

	public long getCostOil() {
		return costOil;
	}

	public void addCostOil(long costOil) {
		this.costOil = this.costOil + costOil;
	}

	public int getRunCount() {
		return runCount;
	}

	public void setRunCount(int runCount) {
		this.runCount = runCount;
	}

	public int getRunDiffCount() {
		return runDiffCount;
	}

	public void setRunDiffCount(int runDiffCount) {
		this.runDiffCount = runDiffCount;
	}

	public long getLast_brake_shoe_time() {
		return last_brake_shoe_time;
	}

	public void setLast_brake_shoe_time(long lastBrakeShoeTime) {
		last_brake_shoe_time = lastBrakeShoeTime;
	}

	public long getLast_eoil_pressure_time() {
		return last_eoil_pressure_time;
	}

	public void setLast_eoil_pressure_time(long lastEoilPressureTime) {
		last_eoil_pressure_time = lastEoilPressureTime;
	}

	public long getLast_e_water_temp_time() {
		return last_e_water_temp_time;
	}

	public void setLast_e_water_temp_time(long lastEWaterTempTime) {
		last_e_water_temp_time = lastEWaterTempTime;
	}

	public long getLast_trig_pressure_time() {
		return last_trig_pressure_time;
	}

	public void setLast_trig_pressure_time(long lastTrigPressureTime) {
		last_trig_pressure_time = lastTrigPressureTime;
	}

	public long getLast_stage_low_alarm_time() {
		return last_stage_low_alarm_time;
	}

	public void setLast_stage_low_alarm_time(long lastStageLowAlarmTime) {
		last_stage_low_alarm_time = lastStageLowAlarmTime;
	}

	public long getLast_retarder_ht_alarm_time() {
		return last_retarder_ht_alarm_time;
	}

	public void setLast_retarder_ht_alarm_time(long lastRetarderHtAlarmTime) {
		last_retarder_ht_alarm_time = lastRetarderHtAlarmTime;
	}

	public long getLast_ehouing_ht_alarm_time() {
		return last_ehouing_ht_alarm_time;
	}

	public void setLast_ehouing_ht_alarm_time(long lastEhouingHtAlarmTime) {
		last_ehouing_ht_alarm_time = lastEhouingHtAlarmTime;
	}

	public long getLast_battery_voltage_time() {
		return last_battery_voltage_time;
	}

	public void setLast_battery_voltage_time(long lastBatteryVoltageTime) {
		last_battery_voltage_time = lastBatteryVoltageTime;
	}

	public long getLast_fuel_blocking_alarm_time() {
		return last_fuel_blocking_alarm_time;
	}

	public void setLast_fuel_blocking_alarm_time(long lastFuelBlockingAlarmTime) {
		last_fuel_blocking_alarm_time = lastFuelBlockingAlarmTime;
	}

	public long getRunningOil() {
		return runningOil;
	}

	public void addRunningOil(long runningOil) {
		this.runningOil = runningOil + this.runningOil;
	}

	public long getOpening_door_ex_num() {
		return opening_door_ex_num;
	}

	public void setOpening_door_ex_num(long opening_door_ex_num) {
		this.opening_door_ex_num = opening_door_ex_num;
	}

	public long getPoint_milege() {
		return point_milege;
	}

	public void addPoint_milege(long pointMilege) {
		point_milege = this.point_milege + pointMilege;
	}

	public long getGis_milege() {
		return gis_milege;
	}

	public void addGis_milege(long gisMilege) {
		gis_milege = this.gis_milege + gisMilege;
	}

	public long getPoint_oil() {
		return point_oil;
	}

	public void addPoint_oil(long pointOil) {
		point_oil = this.point_oil + pointOil;
	}

	public long getMetRunningOil() {
		return metRunningOil;
	}

	public void addMetRunningOil(long metRunningOil) {
		this.metRunningOil = metRunningOil + this.metRunningOil;
	}

	public long getEcuOil() {
		return ecuOil;
	}

	public void addEcuOil(long ecuOil) {
		this.ecuOil = ecuOil + this.ecuOil;
	}

	public long getEcuRunningOil() {
		return ecuRunningOil;
	}

	public void addEcuRunningOil(long ecuRunningOil) {
		this.ecuRunningOil = ecuRunningOil + this.ecuRunningOil;
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

	public Map<String, VehicleAlarm> getStateCountMap() {
		return stateCountMap;
	}

	public void setStateCountMap(Map<String, VehicleAlarm> stateCountMap) {
		this.stateCountMap = stateCountMap;
	}

	public long getRunningTime() {
		return runningTime;
	}

	public void setRunningTime(long runningTime) {
		this.runningTime = runningTime;
	}
	
	
}
