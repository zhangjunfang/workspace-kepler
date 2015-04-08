package com.demo.bean;

import java.util.List;

public class Up_InfoContent {

	public String msg_id;
	public String deal_state;
	public String vehicle_vin;
	public String cellphone;

	public String unit;
	
	public String utc_time;
	public String gps_valid;
	public String latitude;
	public String longitude;
	public String direction;
	public String gps_speeding;
	//上行实时信息-报警状态
	public String alarm_state;

	/**
	 * 上行参数信息
	 */
	public String msg_center;
	public String apn;
	public String server_ip;
	public String server_port;
	public String receive_time;

	public String time_answers;
	public String spacing_answers;
	public String keepalive_time;
	public String keepalive_overtime;
	public String stalled_time_answers;

	public String overspeed;
	public String overspeed_diff;
	public String overspeed_keep;
	public String driving_fatigue;
	public String driving_fatigue_diff;
	public String driving_fatigue_rest;

	public String index_property;
	public String pulse_per_second;
	public String engine_gear;
	public String vehicle_ln;
	public String vehicle_no;
	public String vehicle_sort;
	public String sleep_time;
	public String out1tiem;
	
	public String creater;
	public String create_time;
	public String modifier;
	public String modify_time;

	// 上行参数信息，属于哪种上传参数类型
	public String type;

	/**
	 * 上行车辆鉴权不通过
	 */
	public String sim;
	public String terminal_id;

	/**
	 * 上行图片信息
	 */
	public String sim_card_number;
	public String photo_time;
	public String channel_number;
	public String photo_file;
	public String operate_user_id;
	public String operate_time;
	public String deal_time;

	/**
	 * 上行实时信息
	 */
	public String msg_cmd;
	public String speeding;
	public String on_off;
	public String sos;
	public String overspeed_alert;
	public String fatigue_alert;
	public String gps_alert;
	public String show_speed_alert;
	public String driver_id;
	public String driver_license;
	public String terminal_time;
	public String engine_rotate_speed;
	public String fire_up_state;
	public String power_state;
	public String battery_voltage;
	public String gps_state;
	public String ext_voltage;
	public String img_process;
	public String mileage;
	public String oil_total;
	public String quad_id_type;
	public String route_info;
	public String e_water_temp;
	public String oil_pressure;
	public String oil_instant;
	public String e_torque;
	public String meg_resp_id;
	public String meg_id;
	public String meg_type;
	public String meg_info;
	public String torque_percent;
	public String elevation;
	public String ratio;
	public String gears;
	public String meg_seq;
	public String rapid_alert;

	public String ecc_app;
	public String pulse_mileage;
	//脉冲车速
	public String vin_speed;

	//终端处理状态
	public String state;
	//急加/减速开关量
	public String rapid;
	
	//区域id
	public String region_id;
	//告警确认时间
	public String confirm_time;
	//告警是否处理
	public String deal_flag;
	//sos报警类型ID
	public String sos_alarm_type_id;
	//超速报警类型ID
	public String overspeed_alarm_type_id;
	//疲劳驾驶报警类型ID
	public String fatigue_alarm_type_id;
	//gps超速报警类型ID
	public String gps_alarm_type_id;
	//急加/减速报警类型ID
	public String rapid_alarm_type_id;
	//区域超速告警id
	public String region_overspeed_alarm_type_id;
	//区域非法开关门告警id
	public String region_openclosedoor_alarm_type_id;
	//出区域告警id
	public String region_out_alarm_type_id;
	//入区域告警id
	public String region_in_alarm_type_id;
	//确认用户编码
	public String user_id;
	
	//前门开关信号
	public String frontdoor_on_off;
	//中门开关信号
	public String middledoor_on_off;

	//区域超速告警
	public String region_overspeed_alert;
	//区域开关门告警
	public String region_openclose_alert;
	//出区域报警
	public String region_out_alert;
	//入区域报警
	public String region_in_alert;
	
	
	
	//宇通杯上行历史秒数据
	//上报历史数据时间
	public String time;
	//上行的数据是否有效
	public String data_valid;
	//脉冲行驶里程 1m
	public String vehicle_distance;
	//脉冲车速 m/h
	public String vehicle_speed;
	//发动机转速, 缩小1000倍为真值
	public String engine_speed;
	//发动机扭矩
	public String engine_torque;
	//发动机冷却水水温
	public String engine_coolant_temperature;
	//油门踏板位置，缩小10倍为真值
	public String throttle_position;
	//瞬时油耗
	public String flowrate;
	//累计油耗
	public String totalconsumption;
	public String ytb_rapid;
	//latitude纬度
	public String ytb_latitude;
	//longitude经度
	public String ytb_longitude;
	//altitude海拔
	public String ytb_altitude;
	//speed车速
	public String speed;
	//week星期
	public String week;
	//time_of_week星期内时间
	public String time_of_week;
	//预留字段1
	public String reserved1;
	//预留字段2
	public String reserved2;
	//预留字段3
	public String reserved3;
	
	public String over_engine_speed;
	public String gear_unfit;
	public String egear_run;
	public String gear2_start;

	//车辆超速记录
	public String overspeed_start_time;
	public String overspeed_end_time;
	public String overspeed_driver_id;
	public String overspeed_type;
	public String overspeed_highspeed;
	public String overspeed_rpm;
	public String overspeed_latitude;
	public String overspeed_longitude;
	public String enterprise_id;
	public String organization_id;
	public String overspeed_onoff;
	public String overspeed_maxspeed;
	
	//登陆记录
	public String clw_login_time;
	public String clw_login_driverid;
	public String clw_login_cardlevel;
	public String clw_login_pass;
	public String clw_login_cardid;
	public String clw_login_driverlicense;
	
	//开关量变化
	public String clw_onoff_time;
	public String clw_onoff_aftervalue;
	public String clw_onoff_prevalue;
	public String clw_onoff_speed;
	public String clw_onoff_doorstate;
	
	//急加速记录
	public String clw_rapid_time;
	public String clw_rapid_latitude;
	public String clw_rapid_longitude;
	public String clw_rapid_speed;
	public String clw_rapid_heading;
	public String clw_rapid_onoff;
	public String clw_rapid_speed_dit;
	public String clw_rapid_onedata_time;

	@SuppressWarnings("unchecked")
	public List speedonofflist;
	
	//车联网1分钟数据
	public String clw_munite1_time;
	public String clw_munite1_totalfuelused;
	public String clw_munite1_fuellevel;
	public String clw_munite1_engineoilpressure;
	public String clw_munite1_engineoiltemperature;
	public String clw_munite1_enginecoolanttemperature;
	//车联网5分钟数据
	public String clw_munite5_time;
	public String clw_munite5_totalenginehours;
	public String clw_munite5_electricalpotentia;
	public String clw_munite5_barometricpressure;
	public String clw_munite5_airinlettemperature;
	
	//车联网秒数据
	public String clw_time;
	public String clw_latitude;
	public String clw_longitude;
	public String clw_speed;
	public String clw_heading;
	public String clw_fuelrate;
	public String clw_engine_speed;
	public String clw_engine_torque;
	public String clw_throttle_position;
	public String clw_vehicle_speed;
	
	
	
	//短信
	public String sms_state;
	public String send_take;
	public String src_id;
	public String tel;
	public String msg;
	
	public String getOut1tiem() {
		return out1tiem;
	}

	public void setOut1tiem(String out1tiem) {
		this.out1tiem = out1tiem;
	}

	public String getRapid_alert() {
		return rapid_alert;
	}

	public void setRapid_alert(String rapidAlert) {
		rapid_alert = rapidAlert;
	}

	public String getOverspeed_maxspeed() {
		return overspeed_maxspeed;
	}

	public void setOverspeed_maxspeed(String overspeedMaxspeed) {
		overspeed_maxspeed = overspeedMaxspeed;
	}

	public String getSend_take() {
		return send_take;
	}

	public void setSend_take(String sendTake) {
		send_take = sendTake;
	}

	public String getSrc_id() {
		return src_id;
	}

	public void setSrc_id(String srcId) {
		src_id = srcId;
	}

	public String getTel() {
		return tel;
	}

	public void setTel(String tel) {
		this.tel = tel;
	}

	public String getMsg() {
		return msg;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	public String getOverspeed_onoff() {
		return overspeed_onoff;
	}

	public void setOverspeed_onoff(String overspeedOnoff) {
		overspeed_onoff = overspeedOnoff;
	}

	public String getUnit() {
		return unit;
	}

	public void setUnit(String unit) {
		this.unit = unit;
	}

	public String getYtb_rapid() {
		return ytb_rapid;
	}

	public void setYtb_rapid(String ytbRapid) {
		ytb_rapid = ytbRapid;
	}

	public String getSms_state() {
		return sms_state;
	}

	public void setSms_state(String smsState) {
		sms_state = smsState;
	}

	public String getClw_rapid_onedata_time() {
		return clw_rapid_onedata_time;
	}

	public void setClw_rapid_onedata_time(String clwRapidOnedataTime) {
		clw_rapid_onedata_time = clwRapidOnedataTime;
	}

	public String getClw_rapid_speed_dit() {
		return clw_rapid_speed_dit;
	}

	public void setClw_rapid_speed_dit(String clwRapidSpeedDit) {
		clw_rapid_speed_dit = clwRapidSpeedDit;
	}
	
	public String getClw_rapid_onoff() {
		return clw_rapid_onoff;
	}

	public void setClw_rapid_onoff(String clwRapidOnoff) {
		clw_rapid_onoff = clwRapidOnoff;
	}

	public String getCreater() {
		return creater;
	}

	public void setCreater(String creater) {
		this.creater = creater;
	}

	@SuppressWarnings("unchecked")
	public List getSpeedonofflist() {
		return speedonofflist;
	}

	@SuppressWarnings("unchecked")
	public void setSpeedonofflist(List speedonofflist) {
		this.speedonofflist = speedonofflist;
	}

	public String getCreate_time() {
		return create_time;
	}

	public void setCreate_time(String createTime) {
		create_time = createTime;
	}

	public String getModifier() {
		return modifier;
	}

	public void setModifier(String modifier) {
		this.modifier = modifier;
	}

	public String getModify_time() {
		return modify_time;
	}

	public void setModify_time(String modifyTime) {
		modify_time = modifyTime;
	}

	public String getClw_rapid_time() {
		return clw_rapid_time;
	}

	public void setClw_rapid_time(String clwRapidTime) {
		clw_rapid_time = clwRapidTime;
	}

	public String getClw_rapid_latitude() {
		return clw_rapid_latitude;
	}

	public void setClw_rapid_latitude(String clwRapidLatitude) {
		clw_rapid_latitude = clwRapidLatitude;
	}

	public String getClw_rapid_longitude() {
		return clw_rapid_longitude;
	}

	public void setClw_rapid_longitude(String clwRapidLongitude) {
		clw_rapid_longitude = clwRapidLongitude;
	}

	public String getClw_rapid_speed() {
		return clw_rapid_speed;
	}

	public void setClw_rapid_speed(String clwRapidSpeed) {
		clw_rapid_speed = clwRapidSpeed;
	}

	public String getClw_rapid_heading() {
		return clw_rapid_heading;
	}

	public void setClw_rapid_heading(String clwRapidHeading) {
		clw_rapid_heading = clwRapidHeading;
	}	

	public String getClw_munite5_time() {
		return clw_munite5_time;
	}

	public void setClw_munite5_time(String clwMunite5Time) {
		clw_munite5_time = clwMunite5Time;
	}

	public String getClw_munite5_totalenginehours() {
		return clw_munite5_totalenginehours;
	}

	public void setClw_munite5_totalenginehours(String clwMunite5Totalenginehours) {
		clw_munite5_totalenginehours = clwMunite5Totalenginehours;
	}

	
	public String getClw_munite5_electricalpotentia() {
		return clw_munite5_electricalpotentia;
	}

	public void setClw_munite5_electricalpotentia(
			String clwMunite5Electricalpotentia) {
		clw_munite5_electricalpotentia = clwMunite5Electricalpotentia;
	}

	public String getClw_munite5_barometricpressure() {
		return clw_munite5_barometricpressure;
	}

	public void setClw_munite5_barometricpressure(
			String clwMunite5Barometricpressure) {
		clw_munite5_barometricpressure = clwMunite5Barometricpressure;
	}

	public String getClw_munite5_airinlettemperature() {
		return clw_munite5_airinlettemperature;
	}

	public void setClw_munite5_airinlettemperature(
			String clwMunite5Airinlettemperature) {
		clw_munite5_airinlettemperature = clwMunite5Airinlettemperature;
	}

	public String getReserved3() {
		return reserved3;
	}

	public void setReserved3(String reserved3) {
		this.reserved3 = reserved3;
	}

	public String getClw_munite1_time() {
		return clw_munite1_time;
	}

	public void setClw_munite1_time(String clwMunite1Time) {
		clw_munite1_time = clwMunite1Time;
	}

	public String getClw_munite1_totalfuelused() {
		return clw_munite1_totalfuelused;
	}

	public void setClw_munite1_totalfuelused(String clwMunite1Totalfuelused) {
		clw_munite1_totalfuelused = clwMunite1Totalfuelused;
	}

	public String getClw_munite1_fuellevel() {
		return clw_munite1_fuellevel;
	}

	public void setClw_munite1_fuellevel(String clwMunite1Fuellevel) {
		clw_munite1_fuellevel = clwMunite1Fuellevel;
	}

	public String getClw_munite1_engineoilpressure() {
		return clw_munite1_engineoilpressure;
	}

	public void setClw_munite1_engineoilpressure(String clwMunite1Engineoilpressure) {
		clw_munite1_engineoilpressure = clwMunite1Engineoilpressure;
	}

	public String getClw_munite1_engineoiltemperature() {
		return clw_munite1_engineoiltemperature;
	}

	public void setClw_munite1_engineoiltemperature(
			String clwMunite1Engineoiltemperature) {
		clw_munite1_engineoiltemperature = clwMunite1Engineoiltemperature;
	}

	public String getClw_munite1_enginecoolanttemperature() {
		return clw_munite1_enginecoolanttemperature;
	}

	public void setClw_munite1_enginecoolanttemperature(
			String clwMunite1Enginecoolanttemperature) {
		clw_munite1_enginecoolanttemperature = clwMunite1Enginecoolanttemperature;
	}

	public String getClw_onoff_time() {
		return clw_onoff_time;
	}

	public void setClw_onoff_time(String clwOnoffTime) {
		clw_onoff_time = clwOnoffTime;
	}

	public String getClw_onoff_aftervalue() {
		return clw_onoff_aftervalue;
	}

	public void setClw_onoff_aftervalue(String clwOnoffAftervalue) {
		clw_onoff_aftervalue = clwOnoffAftervalue;
	}

	public String getClw_onoff_prevalue() {
		return clw_onoff_prevalue;
	}

	public void setClw_onoff_prevalue(String clwOnoffPrevalue) {
		clw_onoff_prevalue = clwOnoffPrevalue;
	}

	public String getClw_onoff_speed() {
		return clw_onoff_speed;
	}

	public void setClw_onoff_speed(String clwOnoffSpeed) {
		clw_onoff_speed = clwOnoffSpeed;
	}

	public String getClw_onoff_doorstate() {
		return clw_onoff_doorstate;
	}

	public void setClw_onoff_doorstate(String clwOnoffDoorstate) {
		clw_onoff_doorstate = clwOnoffDoorstate;
	}

	public String getClw_login_time() {
		return clw_login_time;
	}

	public void setClw_login_time(String clwLoginTime) {
		clw_login_time = clwLoginTime;
	}

	public String getClw_login_driverid() {
		return clw_login_driverid;
	}

	public void setClw_login_driverid(String clwLoginDriverid) {
		clw_login_driverid = clwLoginDriverid;
	}

	public String getClw_login_cardlevel() {
		return clw_login_cardlevel;
	}

	public void setClw_login_cardlevel(String clwLoginCardlevel) {
		clw_login_cardlevel = clwLoginCardlevel;
	}

	public String getClw_login_pass() {
		return clw_login_pass;
	}

	public void setClw_login_pass(String clwLoginPass) {
		clw_login_pass = clwLoginPass;
	}

	public String getClw_login_cardid() {
		return clw_login_cardid;
	}

	public void setClw_login_cardid(String clwLoginCardid) {
		clw_login_cardid = clwLoginCardid;
	}

	public String getClw_login_driverlicense() {
		return clw_login_driverlicense;
	}

	public void setClw_login_driverlicense(String clwLoginDriverlicense) {
		clw_login_driverlicense = clwLoginDriverlicense;
	}

	public String getOverspeed_rpm() {
		return overspeed_rpm;
	}

	public void setOverspeed_rpm(String overspeedRpm) {
		overspeed_rpm = overspeedRpm;
	}

	public String getOverspeed_latitude() {
		return overspeed_latitude;
	}

	public void setOverspeed_latitude(String overspeedLatitude) {
		overspeed_latitude = overspeedLatitude;
	}

	public String getOverspeed_longitude() {
		return overspeed_longitude;
	}

	public void setOverspeed_longitude(String overspeedLongitude) {
		overspeed_longitude = overspeedLongitude;
	}

	public String getEnterprise_id() {
		return enterprise_id;
	}

	public void setEnterprise_id(String enterpriseId) {
		enterprise_id = enterpriseId;
	}

	public String getOrganization_id() {
		return organization_id;
	}

	public void setOrganization_id(String organizationId) {
		organization_id = organizationId;
	}
	
	public String getClw_time() {
		return clw_time;
	}

	public void setClw_time(String clwTime) {
		clw_time = clwTime;
	}

	public String getClw_latitude() {
		return clw_latitude;
	}

	public void setClw_latitude(String clwLatitude) {
		clw_latitude = clwLatitude;
	}

	public String getClw_longitude() {
		return clw_longitude;
	}

	public void setClw_longitude(String clwLongitude) {
		clw_longitude = clwLongitude;
	}

	public String getClw_speed() {
		return clw_speed;
	}

	public void setClw_speed(String clwSpeed) {
		clw_speed = clwSpeed;
	}

	public String getClw_heading() {
		return clw_heading;
	}

	public void setClw_heading(String clwHeading) {
		clw_heading = clwHeading;
	}

	public String getClw_fuelrate() {
		return clw_fuelrate;
	}

	public void setClw_fuelrate(String clwFuelrate) {
		clw_fuelrate = clwFuelrate;
	}

	public String getClw_engine_speed() {
		return clw_engine_speed;
	}

	public void setClw_engine_speed(String clwEngineSpeed) {
		clw_engine_speed = clwEngineSpeed;
	}

	public String getClw_engine_torque() {
		return clw_engine_torque;
	}

	public void setClw_engine_torque(String clwEngineTorque) {
		clw_engine_torque = clwEngineTorque;
	}

	public String getClw_throttle_position() {
		return clw_throttle_position;
	}

	public void setClw_throttle_position(String clwThrottlePosition) {
		clw_throttle_position = clwThrottlePosition;
	}

	public String getClw_vehicle_speed() {
		return clw_vehicle_speed;
	}

	public void setClw_vehicle_speed(String clwVehicleSpeed) {
		clw_vehicle_speed = clwVehicleSpeed;
	}

	public String getOverspeed_start_time() {
		return overspeed_start_time;
	}

	public void setOverspeed_start_time(String overspeedStartTime) {
		overspeed_start_time = overspeedStartTime;
	}

	public String getOverspeed_end_time() {
		return overspeed_end_time;
	}

	public void setOverspeed_end_time(String overspeedEndTime) {
		overspeed_end_time = overspeedEndTime;
	}

	public String getOverspeed_driver_id() {
		return overspeed_driver_id;
	}

	public void setOverspeed_driver_id(String overspeedDriverId) {
		overspeed_driver_id = overspeedDriverId;
	}

	public String getOverspeed_type() {
		return overspeed_type;
	}

	public void setOverspeed_type(String overspeedType) {
		overspeed_type = overspeedType;
	}

	public String getOverspeed_highspeed() {
		return overspeed_highspeed;
	}

	public void setOverspeed_highspeed(String overspeedHighspeed) {
		overspeed_highspeed = overspeedHighspeed;
	}

	public String on_off_value;
	
	public String getOn_off_value() {
		return on_off_value;
	}

	public void setOn_off_value(String onOffValue) {
		on_off_value = onOffValue;
	}

	public String getReserved1() {
		return reserved1;
	}

	public void setReserved1(String reserved1) {
		this.reserved1 = reserved1;
	}

	public String getReserved2() {
		return reserved2;
	}

	public void setReserved2(String reserved2) {
		this.reserved2 = reserved2;
	}

	public String getVehicle_distance() {
		return vehicle_distance;
	}

	public void setVehicle_distance(String vehicleDistance) {
		vehicle_distance = vehicleDistance;
	}

	public String getVehicle_speed() {
		return vehicle_speed;
	}

	public void setVehicle_speed(String vehicleSpeed) {
		vehicle_speed = vehicleSpeed;
	}

	public String getEngine_speed() {
		return engine_speed;
	}

	public void setEngine_speed(String engineSpeed) {
		engine_speed = engineSpeed;
	}

	public String getEngine_torque() {
		return engine_torque;
	}

	public void setEngine_torque(String engineTorque) {
		engine_torque = engineTorque;
	}

	public String getEngine_coolant_temperature() {
		return engine_coolant_temperature;
	}

	public void setEngine_coolant_temperature(String engineCoolantTemperature) {
		engine_coolant_temperature = engineCoolantTemperature;
	}

	public String getFlowrate() {
		return flowrate;
	}

	public void setFlowrate(String flowrate) {
		this.flowrate = flowrate;
	}

	public String getTotalconsumption() {
		return totalconsumption;
	}

	public void setTotalconsumption(String totalconsumption) {
		this.totalconsumption = totalconsumption;
	}

	public String getThrottle_position() {
		return throttle_position;
	}

	public void setThrottle_position(String throttlePosition) {
		throttle_position = throttlePosition;
	}

	public String getYtb_latitude() {
		return ytb_latitude;
	}

	public String getEcc_app() {
		return ecc_app;
	}

	public void setEcc_app(String eccApp) {
		ecc_app = eccApp;
	}

	public String getPulse_mileage() {
		return pulse_mileage;
	}

	public void setPulse_mileage(String pulseMileage) {
		pulse_mileage = pulseMileage;
	}

	public String getVin_speed() {
		return vin_speed;
	}

	public void setVin_speed(String vinSpeed) {
		vin_speed = vinSpeed;
	}

	public void setYtb_latitude(String ytbLatitude) {
		ytb_latitude = ytbLatitude;
	}

	public String getYtb_longitude() {
		return ytb_longitude;
	}

	public void setYtb_longitude(String ytbLongitude) {
		ytb_longitude = ytbLongitude;
	}

	public String getYtb_altitude() {
		return ytb_altitude;
	}

	public void setYtb_altitude(String ytbAltitude) {
		ytb_altitude = ytbAltitude;
	}

	public String getSpeed() {
		return speed;
	}

	public void setSpeed(String speed) {
		this.speed = speed;
	}

	public String getWeek() {
		return week;
	}

	public void setWeek(String week) {
		this.week = week;
	}

	public String getTime_of_week() {
		return time_of_week;
	}

	public void setTime_of_week(String timeOfWeek) {
		time_of_week = timeOfWeek;
	}

	public String getData_valid() {
		return data_valid;
	}

	public void setData_valid(String dataValid) {
		data_valid = dataValid;
	}

	public String getTime() {
		return time;
	}

	public void setTime(String time) {
		this.time = time;
	}

	public String getFrontdoor_on_off() {
		return frontdoor_on_off;
	}

	public void setFrontdoor_on_off(String frontdoorOnOff) {
		frontdoor_on_off = frontdoorOnOff;
	}

	public String getMiddledoor_on_off() {
		return middledoor_on_off;
	}

	public void setMiddledoor_on_off(String middledoorOnOff) {
		middledoor_on_off = middledoorOnOff;
	}
	
	public String getRegion_overspeed_alarm_type_id() {
		return region_overspeed_alarm_type_id;
	}

	public void setRegion_overspeed_alarm_type_id(
			String region_overspeed_alarm_type_id) {
		this.region_overspeed_alarm_type_id = region_overspeed_alarm_type_id;
	}

	public String getRegion_openclosedoor_alarm_type_id() {
		return region_openclosedoor_alarm_type_id;
	}

	public void setRegion_openclosedoor_alarm_type_id(
			String region_openclosedoor_alarm_type_id) {
		this.region_openclosedoor_alarm_type_id = region_openclosedoor_alarm_type_id;
	}

	public String getRegion_overspeed_alert() {
		return region_overspeed_alert;
	}

	public void setRegion_overspeed_alert(String region_overspeed_alert) {
		this.region_overspeed_alert = region_overspeed_alert;
	}

	public String getRegion_openclose_alert() {
		return region_openclose_alert;
	}

	public void setRegion_openclose_alert(String region_openclose_alert) {
		this.region_openclose_alert = region_openclose_alert;
	}

	public String getUser_id() {
		return user_id;
	}

	public void setUser_id(String userId) {
		user_id = userId;
	}

	public String getSos_alarm_type_id() {
		return sos_alarm_type_id;
	}

	public void setSos_alarm_type_id(String sosAlarmTypeId) {
		sos_alarm_type_id = sosAlarmTypeId;
	}

	public String getOverspeed_alarm_type_id() {
		return overspeed_alarm_type_id;
	}

	public void setOverspeed_alarm_type_id(String overspeedAlarmTypeId) {
		overspeed_alarm_type_id = overspeedAlarmTypeId;
	}

	public String getFatigue_alarm_type_id() {
		return fatigue_alarm_type_id;
	}

	public void setFatigue_alarm_type_id(String fatigueAlarmTypeId) {
		fatigue_alarm_type_id = fatigueAlarmTypeId;
	}

	public String getGps_alarm_type_id() {
		return gps_alarm_type_id;
	}

	public void setGps_alarm_type_id(String gpsAlarmTypeId) {
		gps_alarm_type_id = gpsAlarmTypeId;
	}

	//报警ID
	public String alarm_id;
	
	public String getAlarm_id() {
		return alarm_id;
	}

	public void setAlarm_id(String alarmId) {
		alarm_id = alarmId;
	}

	public String getDeal_flag() {
		return deal_flag;
	}

	public void setDeal_flag(String dealFlag) {
		deal_flag = dealFlag;
	}

	public String getConfirm_time() {
		return confirm_time;
	}

	public void setConfirm_time(String confirmTime) {
		confirm_time = confirmTime;
	}

	public String getRegion_id() {
		return region_id;
	}

	public void setRegion_id(String regionId) {
		region_id = regionId;
	}

	public String getRapid() {
		return rapid;
	}

	public void setRapid(String rapid) {
		this.rapid = rapid;
	}

	public String getRapid_alarm_type_id() {
		return rapid_alarm_type_id;
	}

	public void setRapid_alarm_type_id(String rapid_alarm_type_id) {
		this.rapid_alarm_type_id = rapid_alarm_type_id;
	}

	public String getState() {
		return state;
	}

	public void setState(String state) {
		this.state = state;
	}

	public String getMeg_seq() {
		return meg_seq;
	}

	public void setMeg_seq(String meg_seq) {
		this.meg_seq = meg_seq;
	}

	public String getTorque_percent() {
		return torque_percent;
	}

	public void setTorque_percent(String torque_percent) {
		this.torque_percent = torque_percent;
	}

	public String getElevation() {
		return elevation;
	}

	public void setElevation(String elevation) {
		this.elevation = elevation;
	}

	public String getRatio() {
		return ratio;
	}

	public void setRatio(String ratio) {
		this.ratio = ratio;
	}

	public String getGears() {
		return gears;
	}

	public void setGears(String gears) {
		this.gears = gears;
	}

	public String getMsg_id() {
		return msg_id;
	}

	public void setMsg_id(String msg_id) {
		this.msg_id = msg_id;
	}

	public String getDeal_state() {
		return deal_state;
	}

	public void setDeal_state(String deal_state) {
		this.deal_state = deal_state;
	}

	public String getVehicle_vin() {
		return vehicle_vin;
	}

	public void setVehicle_vin(String vehicle_vin) {
		this.vehicle_vin = vehicle_vin;
	}

	public String getUtc_time() {
		return utc_time;
	}

	public void setUtc_time(String utc_time) {
		this.utc_time = utc_time;
	}

	public String getGps_valid() {
		return gps_valid;
	}

	public void setGps_valid(String gps_valid) {
		this.gps_valid = gps_valid;
	}

	public String getLatitude() {
		return latitude;
	}

	public void setLatitude(String latitude) {
		this.latitude = latitude;
	}

	public String getLongitude() {
		return longitude;
	}

	public void setLongitude(String longitude) {
		this.longitude = longitude;
	}

	public String getDirection() {
		return direction;
	}

	public void setDirection(String direction) {
		this.direction = direction;
	}

	public String getGps_speeding() {
		return gps_speeding;
	}

	public void setGps_speeding(String gps_speeding) {
		this.gps_speeding = gps_speeding;
	}

	public String getMsg_center() {
		return msg_center;
	}

	public void setMsg_center(String msg_center) {
		this.msg_center = msg_center;
	}

	public String getApn() {
		return apn;
	}

	public void setApn(String apn) {
		this.apn = apn;
	}

	public String getServer_ip() {
		return server_ip;
	}

	public void setServer_ip(String server_ip) {
		this.server_ip = server_ip;
	}

	public String getServer_port() {
		return server_port;
	}

	public void setServer_port(String server_port) {
		this.server_port = server_port;
	}

	public String getReceive_time() {
		return receive_time;
	}

	public void setReceive_time(String receive_time) {
		this.receive_time = receive_time;
	}

	public String getTime_answers() {
		return time_answers;
	}

	public void setTime_answers(String time_answers) {
		this.time_answers = time_answers;
	}

	public String getSpacing_answers() {
		return spacing_answers;
	}

	public void setSpacing_answers(String spacing_answers) {
		this.spacing_answers = spacing_answers;
	}

	public String getKeepalive_time() {
		return keepalive_time;
	}

	public void setKeepalive_time(String keepalive_time) {
		this.keepalive_time = keepalive_time;
	}

	public String getKeepalive_overtime() {
		return keepalive_overtime;
	}

	public void setKeepalive_overtime(String keepalive_overtime) {
		this.keepalive_overtime = keepalive_overtime;
	}

	public String getStalled_time_answers() {
		return stalled_time_answers;
	}

	public void setStalled_time_answers(String stalled_time_answers) {
		this.stalled_time_answers = stalled_time_answers;
	}

	public String getOverspeed() {
		return overspeed;
	}

	public void setOverspeed(String overspeed) {
		this.overspeed = overspeed;
	}

	public String getOverspeed_diff() {
		return overspeed_diff;
	}

	public void setOverspeed_diff(String overspeed_diff) {
		this.overspeed_diff = overspeed_diff;
	}

	public String getOverspeed_keep() {
		return overspeed_keep;
	}

	public void setOverspeed_keep(String overspeed_keep) {
		this.overspeed_keep = overspeed_keep;
	}

	public String getDriving_fatigue() {
		return driving_fatigue;
	}

	public void setDriving_fatigue(String driving_fatigue) {
		this.driving_fatigue = driving_fatigue;
	}

	public String getDriving_fatigue_diff() {
		return driving_fatigue_diff;
	}

	public void setDriving_fatigue_diff(String driving_fatigue_diff) {
		this.driving_fatigue_diff = driving_fatigue_diff;
	}

	public String getDriving_fatigue_rest() {
		return driving_fatigue_rest;
	}

	public void setDriving_fatigue_rest(String driving_fatigue_rest) {
		this.driving_fatigue_rest = driving_fatigue_rest;
	}

	public String getIndex_property() {
		return index_property;
	}

	public void setIndex_property(String index_property) {
		this.index_property = index_property;
	}

	public String getPulse_per_second() {
		return pulse_per_second;
	}

	public void setPulse_per_second(String pulse_per_second) {
		this.pulse_per_second = pulse_per_second;
	}

	public String getEngine_gear() {
		return engine_gear;
	}

	public void setEngine_gear(String engine_gear) {
		this.engine_gear = engine_gear;
	}

	public String getVehicle_ln() {
		return vehicle_ln;
	}

	public void setVehicle_ln(String vehicle_ln) {
		this.vehicle_ln = vehicle_ln;
	}

	public String getVehicle_no() {
		return vehicle_no;
	}

	public void setVehicle_no(String vehicle_no) {
		this.vehicle_no = vehicle_no;
	}

	public String getVehicle_sort() {
		return vehicle_sort;
	}

	public void setVehicle_sort(String vehicle_sort) {
		this.vehicle_sort = vehicle_sort;
	}

	public String getSleep_time() {
		return sleep_time;
	}

	public void setSleep_time(String sleep_time) {
		this.sleep_time = sleep_time;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public String getSim() {
		return sim;
	}

	public void setSim(String sim) {
		this.sim = sim;
	}

	public String getTerminal_id() {
		return terminal_id;
	}

	public void setTerminal_id(String terminal_id) {
		this.terminal_id = terminal_id;
	}

	public String getSim_card_number() {
		return sim_card_number;
	}

	public void setSim_card_number(String sim_card_number) {
		this.sim_card_number = sim_card_number;
	}

	public String getPhoto_time() {
		return photo_time;
	}

	public void setPhoto_time(String photo_time) {
		this.photo_time = photo_time;
	}

	public String getChannel_number() {
		return channel_number;
	}

	public void setChannel_number(String channel_number) {
		this.channel_number = channel_number;
	}

	public String getPhoto_file() {
		return photo_file;
	}

	public void setPhoto_file(String photo_file) {
		this.photo_file = photo_file;
	}

	public String getOperate_user_id() {
		return operate_user_id;
	}

	public void setOperate_user_id(String operate_user_id) {
		this.operate_user_id = operate_user_id;
	}

	public String getOperate_time() {
		return operate_time;
	}

	public void setOperate_time(String operate_time) {
		this.operate_time = operate_time;
	}

	public String getDeal_time() {
		return deal_time;
	}

	public void setDeal_time(String deal_time) {
		this.deal_time = deal_time;
	}

	public String getMsg_cmd() {
		return msg_cmd;
	}

	public void setMsg_cmd(String msg_cmd) {
		this.msg_cmd = msg_cmd;
	}

	public String getSpeeding() {
		return speeding;
	}

	public void setSpeeding(String speeding) {
		this.speeding = speeding;
	}

	public String getOn_off() {
		return on_off;
	}

	public void setOn_off(String on_off) {
		this.on_off = on_off;
	}

	public String getSos() {
		return sos;
	}

	public void setSos(String sos) {
		this.sos = sos;
	}

	public String getOverspeed_alert() {
		return overspeed_alert;
	}

	public void setOverspeed_alert(String overspeed_alert) {
		this.overspeed_alert = overspeed_alert;
	}

	public String getFatigue_alert() {
		return fatigue_alert;
	}

	public void setFatigue_alert(String fatigue_alert) {
		this.fatigue_alert = fatigue_alert;
	}

	public String getGps_alert() {
		return gps_alert;
	}

	public void setGps_alert(String gps_alert) {
		this.gps_alert = gps_alert;
	}

	public String getShow_speed_alert() {
		return show_speed_alert;
	}

	public void setShow_speed_alert(String show_speed_alert) {
		this.show_speed_alert = show_speed_alert;
	}

	public String getDriver_id() {
		return driver_id;
	}

	public void setDriver_id(String driver_id) {
		this.driver_id = driver_id;
	}

	public String getDriver_license() {
		return driver_license;
	}

	public void setDriver_license(String driver_license) {
		this.driver_license = driver_license;
	}

	public String getTerminal_time() {
		return terminal_time;
	}

	public void setTerminal_time(String terminal_time) {
		this.terminal_time = terminal_time;
	}

	public String getEngine_rotate_speed() {
		return engine_rotate_speed;
	}

	public void setEngine_rotate_speed(String engine_rotate_speed) {
		this.engine_rotate_speed = engine_rotate_speed;
	}

	public String getFire_up_state() {
		return fire_up_state;
	}

	public void setFire_up_state(String fire_up_state) {
		this.fire_up_state = fire_up_state;
	}

	public String getPower_state() {
		return power_state;
	}

	public void setPower_state(String power_state) {
		this.power_state = power_state;
	}

	public String getBattery_voltage() {
		return battery_voltage;
	}

	public void setBattery_voltage(String battery_voltage) {
		this.battery_voltage = battery_voltage;
	}

	public String getGps_state() {
		return gps_state;
	}

	public void setGps_state(String gps_state) {
		this.gps_state = gps_state;
	}

	public String getExt_voltage() {
		return ext_voltage;
	}

	public void setExt_voltage(String ext_voltage) {
		this.ext_voltage = ext_voltage;
	}

	public String getImg_process() {
		return img_process;
	}

	public void setImg_process(String img_process) {
		this.img_process = img_process;
	}

	public String getMileage() {
		return mileage;
	}

	public void setMileage(String mileage) {
		this.mileage = mileage;
	}

	public String getOil_total() {
		return oil_total;
	}

	public void setOil_total(String oil_total) {
		this.oil_total = oil_total;
	}

	public String getQuad_id_type() {
		return quad_id_type;
	}

	public void setQuad_id_type(String quad_id_type) {
		this.quad_id_type = quad_id_type;
	}

	public String getRoute_info() {
		return route_info;
	}

	public void setRoute_info(String route_info) {
		this.route_info = route_info;
	}

	public String getE_water_temp() {
		return e_water_temp;
	}

	public void setE_water_temp(String e_water_temp) {
		this.e_water_temp = e_water_temp;
	}

	public String getOil_pressure() {
		return oil_pressure;
	}

	public void setOil_pressure(String oil_pressure) {
		this.oil_pressure = oil_pressure;
	}

	public String getOil_instant() {
		return oil_instant;
	}

	public void setOil_instant(String oil_instant) {
		this.oil_instant = oil_instant;
	}

	public String getE_torque() {
		return e_torque;
	}

	public void setE_torque(String e_torque) {
		this.e_torque = e_torque;
	}

	public String getMeg_resp_id() {
		return meg_resp_id;
	}

	public void setMeg_resp_id(String meg_resp_id) {
		this.meg_resp_id = meg_resp_id;
	}

	public String getMeg_id() {
		return meg_id;
	}

	public void setMeg_id(String meg_id) {
		this.meg_id = meg_id;
	}

	public String getMeg_type() {
		return meg_type;
	}

	public void setMeg_type(String meg_type) {
		this.meg_type = meg_type;
	}

	public String getMeg_info() {
		return meg_info;
	}

	public void setMeg_info(String meg_info) {
		this.meg_info = meg_info;
	}

	public String getAlarm_state() {
		return alarm_state;
	}

	public void setAlarm_state(String alarm_state) {
		this.alarm_state = alarm_state;
	}

	public String getRegion_out_alert() {
		return region_out_alert;
	}

	public void setRegion_out_alert(String region_out_alert) {
		this.region_out_alert = region_out_alert;
	}

	public String getRegion_in_alert() {
		return region_in_alert;
	}

	public void setRegion_in_alert(String region_in_alert) {
		this.region_in_alert = region_in_alert;
	}

	public String getRegion_out_alarm_type_id() {
		return region_out_alarm_type_id;
	}

	public void setRegion_out_alarm_type_id(String region_out_alarm_type_id) {
		this.region_out_alarm_type_id = region_out_alarm_type_id;
	}

	public String getRegion_in_alarm_type_id() {
		return region_in_alarm_type_id;
	}

	public void setRegion_in_alarm_type_id(String region_in_alarm_type_id) {
		this.region_in_alarm_type_id = region_in_alarm_type_id;
	}

	public String getOver_engine_speed() {
		return over_engine_speed;
	}

	public void setOver_engine_speed(String over_engine_speed) {
		this.over_engine_speed = over_engine_speed;
	}

	public String getGear_unfit() {
		return gear_unfit;
	}

	public void setGear_unfit(String gear_unfit) {
		this.gear_unfit = gear_unfit;
	}

	public String getEgear_run() {
		return egear_run;
	}

	public void setEgear_run(String egear_run) {
		this.egear_run = egear_run;
	}

	public String getGear2_start() {
		return gear2_start;
	}

	public void setGear2_start(String gear2_start) {
		this.gear2_start = gear2_start;
	}

	public String getCellphone() {
		return cellphone;
	}

	public void setCellphone(String cellphone) {
		this.cellphone = cellphone;
	}
}

