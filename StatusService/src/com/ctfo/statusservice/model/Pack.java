package com.ctfo.statusservice.model;


/**
 *	轨迹对象
 */
public class Pack {
	/**	指令关键字	*/
	private String key;
	/**	指令序列号	*/
	private String seq;
	/**	硬件识别码	*/
	private String macid;
	/**	通道(通讯方式：0 服务端决定方式; 1 SMS方式; 2 GPRS方式 ; 3 WEBSERVICE; 4 其他)	*/
	private String channel;
	/**	指令类型(U_REPT:位置汇报; D_SETP	:终端参数设置; D_SNDM:下发短消息; D_ADDT:订阅; D_DELT:退订; D_CALL:点名)	*/
	private String type;
	/**	指令内容	*/
	private String content;
	/**	指令	*/
	private String command;
	/**	指令子类型	*/
	private String subType;
	/**	经度字符串	*/
	private String lonStr; 
	/**	纬度字符串	*/
	private String latStr; 
	/**	经度	*/
	private long lon; 
	/**	纬度	*/
	private long lat; 
	/**	地图经度	*/
	private long maplon; 
	/**	地图纬度		*/
	private long maplat; 
	/**	速度	*/
	private int speed;
	/**	gps速度	*/
	private int gpsSpeed; 
	/**	行驶记录仪速度(km/h)	*/
	private int vssSpeed; 
	/**	速度来源( 0:来自VSS ;1:来自GPS)  */
	private String speedSource; 
	/**	行驶记录仪速度字符串	*/
	private String vssSpeedStr;
	/**	GPS速度字符串	*/
	private String gpsSpeedStr;
	/**	GPS时间	*/
	private long gpsUtc; 
	/**	方向(度) 	5	*/
	private int direction; 
	/**	海拔(米)		6	*/
	private int elevation; 
	/**	基本信息状态位	*/
	private String baseStatus;
	/**	扩展信息状态位	*/
	private String extendedStatus;
	/**	定位状态	*/
	private int position;
	/**	总电状态	*/
	private int accStatus;
	/**	基本状态(经度、纬度异常:1; 速度异常:2; 时间异常:4; 方向异常:8;  )	*/
	private int status = 0;
	/**	里程，1/10km	 	9	*/
	private long mileage;
	/**	基本报警状态位	*/
	private String baseAlarm;
	/**	扩展报警状态位	*/
	private String extendedAlarm;
	/**	报警附加信息 (32)	*/
	private String alarmAdded;
	/**	是否有报警	*/
	private boolean alarm;
	/**	所有报警编号	*/
	private String allAlarm;
	/**	最近报警时间	*/
	private long alarmUtc;
	/**	速比	*/
	private double ratio;
	/**	档位rob	*/
	private int gears;
	/**	消息服务器编号	*/
	private String msgId;
	/**	总油量（单位：L）	213	*/
	private long oilTotal;
	/**	油量（单位：L）	24	*/
	private long oilMeasure;
	/**	发动机转速	*/
	private int engineSpeed; 
	/**	发动机扭矩	503	*/
	private long engineTorque;
	/**	瞬时油耗	*/
	private long oilInstant;
	/**	电池电压	507	*/
	private long batteryVoltage;
	/**	外部电压	506	*/
	private long externalVoltage;
	/**	冷却液温度	509	*/
	private long eWaterTemp;
	/**	大气压力	*/
	private long atmosphericPressure;
	/**	进气温度	*/
	private long inletTemperature;
	/**	发动机运行总时长	*/
	private long engineRunTotal;
	/**	累计油耗	*/
	private long cumulativeFuel;
	/**	油门踏板位置	*/
	private long throttlePedalPosition;
	/**	机油温度	*/
	private long oilTemperature;
	/**	机油压力	*/
	private long oilPressure;
	/**	精准油耗	*/
	private long preciseFuel;
	/**	锁车状态	*/
	private String lockStatus;
	/**	在线状态	*/
	private int onlineStatus;
	/**	车辆编号	*/
	private String vid;
	/**	手机号	*/
	private String phoneNumber;
	/**	厂商编号	*/
	private String oemCode;
	/**	车牌颜色	*/
	private String plateColor;
	/**	车牌号	*/
	private String plate;
	/**	终端编号	*/
	private String tid;
	/** 车架号 */
	private String vinCode;
	/**
	 * 
	 */
	public Pack() {
	}
	//-------------------------setter && getter-----------------------------------
	/**
	 * 获得指令关键字的值
	 * @return the key 指令关键字  
	 */
	public String getKey() {
		return key;
	}

	/**
	 * 设置指令关键字的值
	 * @param key 指令关键字  
	 */
	public void setKey(String key) {
		this.key = key;
	}

	/**
	 * 获得指令序列号的值
	 * @return the seq 指令序列号  
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * 设置指令序列号的值
	 * @param seq 指令序列号  
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

	/**
	 * 获得硬件识别码的值
	 * @return the macid 硬件识别码  
	 */
	public String getMacid() {
		return macid;
	}

	/**
	 * 设置硬件识别码的值
	 * @param macid 硬件识别码  
	 */
	public void setMacid(String macid) {
		this.macid = macid;
	}

	/**
	 * 获得通道(通讯方式：0服务端决定方式;1SMS方式;2GPRS方式;3WEBSERVICE;4其他)的值
	 * @return the channel 通道(通讯方式：0服务端决定方式;1SMS方式;2GPRS方式;3WEBSERVICE;4其他)  
	 */
	public String getChannel() {
		return channel;
	}

	/**
	 * 设置通道(通讯方式：0服务端决定方式;1SMS方式;2GPRS方式;3WEBSERVICE;4其他)的值
	 * @param channel 通道(通讯方式：0服务端决定方式;1SMS方式;2GPRS方式;3WEBSERVICE;4其他)  
	 */
	public void setChannel(String channel) {
		this.channel = channel;
	}

	/**
	 * 获得指令类型(U_REPT:位置汇报;D_SETP:终端参数设置;D_SNDM:下发短消息;D_ADDT:订阅;D_DELT:退订;D_CALL:点名)的值
	 * @return the type 指令类型(U_REPT:位置汇报;D_SETP:终端参数设置;D_SNDM:下发短消息;D_ADDT:订阅;D_DELT:退订;D_CALL:点名)  
	 */
	public String getType() {
		return type;
	}

	/**
	 * 设置指令类型(U_REPT:位置汇报;D_SETP:终端参数设置;D_SNDM:下发短消息;D_ADDT:订阅;D_DELT:退订;D_CALL:点名)的值
	 * @param type 指令类型(U_REPT:位置汇报;D_SETP:终端参数设置;D_SNDM:下发短消息;D_ADDT:订阅;D_DELT:退订;D_CALL:点名)  
	 */
	public void setType(String type) {
		this.type = type;
	}

	/**
	 * 获得指令内容的值
	 * @return the content 指令内容  
	 */
	public String getContent() {
		return content;
	}

	/**
	 * 设置指令内容的值
	 * @param content 指令内容  
	 */
	public void setContent(String content) {
		this.content = content;
	}

	/**
	 * 获得指令子类型的值
	 * @return the subType 指令子类型  
	 */
	public String getSubType() {
		return subType;
	}

	/**
	 * 设置指令子类型的值
	 * @param subType 指令子类型  
	 */
	public void setSubType(String subType) {
		this.subType = subType;
	}
	/**
	 * 获得经度字符串的值
	 * @return the lonStr 经度字符串  
	 */
	public String getLonStr() {
		return lonStr;
	}
	/**
	 * 设置经度字符串的值
	 * @param lonStr 经度字符串  
	 */
	public void setLonStr(String lonStr) {
		this.lonStr = lonStr;
	}
	/**
	 * 获得纬度字符串的值
	 * @return the latStr 纬度字符串  
	 */
	public String getLatStr() {
		return latStr;
	}
	/**
	 * 设置纬度字符串的值
	 * @param latStr 纬度字符串  
	 */
	public void setLatStr(String latStr) {
		this.latStr = latStr;
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
	 * 获得地图经度的值
	 * @return the maplon 地图经度  
	 */
	public long getMaplon() {
		return maplon;
	}

	/**
	 * 设置地图经度的值
	 * @param maplon 地图经度  
	 */
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}

	/**
	 * 获得地图纬度的值
	 * @return the maplat 地图纬度  
	 */
	public long getMaplat() {
		return maplat;
	}

	/**
	 * 设置地图纬度的值
	 * @param maplat 地图纬度  
	 */
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}

	/**
	 * 获得gps速度的值
	 * @return the gpsSpeed gps速度  
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}

	/**
	 * 设置gps速度的值
	 * @param gpsSpeed gps速度  
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}

	/**
	 * 获得行驶记录仪速度(kmh)的值
	 * @return the vssSpeed 行驶记录仪速度(kmh)  
	 */
	public int getVssSpeed() {
		return vssSpeed;
	}
	/**
	 * 设置行驶记录仪速度(kmh)的值
	 * @param vssSpeed 行驶记录仪速度(kmh)  
	 */
	public void setVssSpeed(int vssSpeed) {
		this.vssSpeed = vssSpeed;
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
	 * 获得速度来源(0:来自VSS;1:来自GPS)的值
	 * @return the speedSource 速度来源(0:来自VSS;1:来自GPS)  
	 */
	public String getSpeedSource() {
		return speedSource;
	}
	/**
	 * 设置速度来源(0:来自VSS;1:来自GPS)的值
	 * @param speedSource 速度来源(0:来自VSS;1:来自GPS)  
	 */
	public void setSpeedSource(String speedSource) {
		this.speedSource = speedSource;
	}
	/**
	 * 获得GPS时间的值
	 * @return the gpsUtc GPS时间  
	 */
	public long getGpsUtc() {
		return gpsUtc;
	}

	/**
	 * 设置GPS时间的值
	 * @param gpsUtc GPS时间  
	 */
	public void setGpsUtc(long gpsUtc) {
		this.gpsUtc = gpsUtc;
	}

	/**
	 * 获得方向(度)5的值
	 * @return the direction 方向(度)5  
	 */
	public int getDirection() {
		return direction;
	}

	/**
	 * 设置方向(度)5的值
	 * @param direction 方向(度)5  
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}

	/**
	 * 获得海拔(米)6的值
	 * @return the elevation 海拔(米)6  
	 */
	public int getElevation() {
		return elevation;
	}

	/**
	 * 设置海拔(米)6的值
	 * @param elevation 海拔(米)6  
	 */
	public void setElevation(int elevation) {
		this.elevation = elevation;
	}

	/**
	 * 获得基本信息状态位的值
	 * @return the baseStatus 基本信息状态位  
	 */
	public String getBaseStatus() {
		return baseStatus;
	}

	/**
	 * 设置基本信息状态位的值
	 * @param baseStatus 基本信息状态位  
	 */
	public void setBaseStatus(String baseStatus) {
		this.baseStatus = baseStatus;
	}

	/**
	 * 获得扩展信息状态位的值
	 * @return the extendedStatus 扩展信息状态位  
	 */
	public String getExtendedStatus() {
		return extendedStatus;
	}

	/**
	 * 设置扩展信息状态位的值
	 * @param extendedStatus 扩展信息状态位  
	 */
	public void setExtendedStatus(String extendedStatus) {
		this.extendedStatus = extendedStatus;
	}

	/**
	 * 获得定位状态的值
	 * @return the position 定位状态  
	 */
	public int getPosition() {
		return position;
	}

	/**
	 * 设置定位状态的值
	 * @param position 定位状态  
	 */
	public void setPosition(int position) {
		this.position = position;
	}

	/**
	 * 获得总电状态的值
	 * @return the accStatus 总电状态  
	 */
	public int getAccStatus() {
		return accStatus;
	}

	/**
	 * 设置总电状态的值
	 * @param accStatus 总电状态  
	 */
	public void setAccStatus(int accStatus) {
		this.accStatus = accStatus;
	}

	/**
	 * 获得基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;)的值
	 * @return the status 基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;)  
	 */
	public int getStatus() {
		return status;
	}

	/**
	 * 设置基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;)的值
	 * @param status 基本状态(经度、纬度异常:1;速度异常:2;时间异常:4;方向异常:8;)  
	 */
	public void setStatus(int status) {
		this.status = status;
	}

	/**
	 * 获得里程，110km9的值
	 * @return the mileage 里程，110km9  
	 */
	public long getMileage() {
		return mileage;
	}

	/**
	 * 设置里程，110km9的值
	 * @param mileage 里程，110km9  
	 */
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}

	/**
	 * 获得基本报警状态位的值
	 * @return the baseAlarm 基本报警状态位  
	 */
	public String getBaseAlarm() {
		return baseAlarm;
	}

	/**
	 * 设置基本报警状态位的值
	 * @param baseAlarm 基本报警状态位  
	 */
	public void setBaseAlarm(String baseAlarm) {
		this.baseAlarm = baseAlarm;
	}

	/**
	 * 获得扩展报警状态位的值
	 * @return the extendedAlarm 扩展报警状态位  
	 */
	public String getExtendedAlarm() {
		return extendedAlarm;
	}

	/**
	 * 设置扩展报警状态位的值
	 * @param extendedAlarm 扩展报警状态位  
	 */
	public void setExtendedAlarm(String extendedAlarm) {
		this.extendedAlarm = extendedAlarm;
	}

	/**
	 * 获得是否有报警的值
	 * @return the alarm 是否有报警  
	 */
	public boolean isAlarm() {
		return alarm;
	}

	/**
	 * 设置是否有报警的值
	 * @param alarm 是否有报警  
	 */
	public void setAlarm(boolean alarm) {
		this.alarm = alarm;
	}

	/**
	 * 获得所有报警编号的值
	 * @return the allAlarm 所有报警编号  
	 */
	public String getAllAlarm() {
		return allAlarm;
	}

	/**
	 * 设置所有报警编号的值
	 * @param allAlarm 所有报警编号  
	 */
	public void setAllAlarm(String allAlarm) {
		this.allAlarm = allAlarm;
	}

	/**
	 * 获得最近报警时间的值
	 * @return the alarmUtc 最近报警时间  
	 */
	public long getAlarmUtc() {
		return alarmUtc;
	}

	/**
	 * 设置最近报警时间的值
	 * @param alarmUtc 最近报警时间  
	 */
	public void setAlarmUtc(long alarmUtc) {
		this.alarmUtc = alarmUtc;
	}

	/**
	 * 获得速比的值
	 * @return the ratio 速比  
	 */
	public double getRatio() {
		return ratio;
	}

	/**
	 * 设置速比的值
	 * @param ratio 速比  
	 */
	public void setRatio(double ratio) {
		this.ratio = ratio;
	}

	/**
	 * 获得档位rob的值
	 * @return the gears 档位rob  
	 */
	public int getGears() {
		return gears;
	}

	/**
	 * 设置档位rob的值
	 * @param gears 档位rob  
	 */
	public void setGears(int gears) {
		this.gears = gears;
	}

	/**
	 * 获得消息服务器编号的值
	 * @return the msgId 消息服务器编号  
	 */
	public String getMsgId() {
		return msgId;
	}

	/**
	 * 设置消息服务器编号的值
	 * @param msgId 消息服务器编号  
	 */
	public void setMsgId(String msgId) {
		this.msgId = msgId;
	}

	/**
	 * 获得油量（单位：L）24的值
	 * @return the oilMeasure 油量（单位：L）24  
	 */
	public long getOilMeasure() {
		return oilMeasure;
	}

	/**
	 * 设置油量（单位：L）24的值
	 * @param oilMeasure 油量（单位：L）24  
	 */
	public void setOilMeasure(long oilMeasure) {
		this.oilMeasure = oilMeasure;
	}

	/**
	 * 获得发动机转速的值
	 * @return the engineSpeed 发动机转速  
	 */
	public int getEngineSpeed() {
		return engineSpeed;
	}

	/**
	 * 设置发动机转速的值
	 * @param engineSpeed 发动机转速  
	 */
	public void setEngineSpeed(int engineSpeed) {
		this.engineSpeed = engineSpeed;
	}

	/**
	 * 获得发动机扭矩503的值
	 * @return the engineTorque 发动机扭矩503  
	 */
	public long getEngineTorque() {
		return engineTorque;
	}

	/**
	 * 设置发动机扭矩503的值
	 * @param engineTorque 发动机扭矩503  
	 */
	public void setEngineTorque(long engineTorque) {
		this.engineTorque = engineTorque;
	}

	/**
	 * 获得瞬时油耗的值
	 * @return the oilInstant 瞬时油耗  
	 */
	public long getOilInstant() {
		return oilInstant;
	}

	/**
	 * 设置瞬时油耗的值
	 * @param oilInstant 瞬时油耗  
	 */
	public void setOilInstant(long oilInstant) {
		this.oilInstant = oilInstant;
	}

	/**
	 * 获得电池电压507的值
	 * @return the batteryVoltage 电池电压507  
	 */
	public long getBatteryVoltage() {
		return batteryVoltage;
	}

	/**
	 * 设置电池电压507的值
	 * @param batteryVoltage 电池电压507  
	 */
	public void setBatteryVoltage(long batteryVoltage) {
		this.batteryVoltage = batteryVoltage;
	}

	/**
	 * 获得外部电压506的值
	 * @return the externalVoltage 外部电压506  
	 */
	public long getExternalVoltage() {
		return externalVoltage;
	}

	/**
	 * 设置外部电压506的值
	 * @param externalVoltage 外部电压506  
	 */
	public void setExternalVoltage(long externalVoltage) {
		this.externalVoltage = externalVoltage;
	}

	/**
	 * 获得冷却液温度509的值
	 * @return the eWaterTemp 冷却液温度509  
	 */
	public long geteWaterTemp() {
		return eWaterTemp;
	}

	/**
	 * 设置冷却液温度509的值
	 * @param eWaterTemp 冷却液温度509  
	 */
	public void seteWaterTemp(long eWaterTemp) {
		this.eWaterTemp = eWaterTemp;
	}

	/**
	 * 获得大气压力的值
	 * @return the atmosphericPressure 大气压力  
	 */
	public long getAtmosphericPressure() {
		return atmosphericPressure;
	}

	/**
	 * 设置大气压力的值
	 * @param atmosphericPressure 大气压力  
	 */
	public void setAtmosphericPressure(long atmosphericPressure) {
		this.atmosphericPressure = atmosphericPressure;
	}

	/**
	 * 获得进气温度的值
	 * @return the inletTemperature 进气温度  
	 */
	public long getInletTemperature() {
		return inletTemperature;
	}

	/**
	 * 设置进气温度的值
	 * @param inletTemperature 进气温度  
	 */
	public void setInletTemperature(long inletTemperature) {
		this.inletTemperature = inletTemperature;
	}

	/**
	 * 获得发动机运行总时长的值
	 * @return the engineRunTotal 发动机运行总时长  
	 */
	public long getEngineRunTotal() {
		return engineRunTotal;
	}

	/**
	 * 设置发动机运行总时长的值
	 * @param engineRunTotal 发动机运行总时长  
	 */
	public void setEngineRunTotal(long engineRunTotal) {
		this.engineRunTotal = engineRunTotal;
	}

	/**
	 * 获得累计油耗的值
	 * @return the cumulativeFuel 累计油耗  
	 */
	public long getCumulativeFuel() {
		return cumulativeFuel;
	}

	/**
	 * 设置累计油耗的值
	 * @param cumulativeFuel 累计油耗  
	 */
	public void setCumulativeFuel(long cumulativeFuel) {
		this.cumulativeFuel = cumulativeFuel;
	}

	/**
	 * 获得油门踏板位置的值
	 * @return the throttlePedalPosition 油门踏板位置  
	 */
	public long getThrottlePedalPosition() {
		return throttlePedalPosition;
	}

	/**
	 * 设置油门踏板位置的值
	 * @param throttlePedalPosition 油门踏板位置  
	 */
	public void setThrottlePedalPosition(long throttlePedalPosition) {
		this.throttlePedalPosition = throttlePedalPosition;
	}

	/**
	 * 获得机油温度的值
	 * @return the oilTemperature 机油温度  
	 */
	public long getOilTemperature() {
		return oilTemperature;
	}

	/**
	 * 设置机油温度的值
	 * @param oilTemperature 机油温度  
	 */
	public void setOilTemperature(long oilTemperature) {
		this.oilTemperature = oilTemperature;
	}

	/**
	 * 获得机油压力的值
	 * @return the oilPressure 机油压力  
	 */
	public long getOilPressure() {
		return oilPressure;
	}

	/**
	 * 设置机油压力的值
	 * @param oilPressure 机油压力  
	 */
	public void setOilPressure(long oilPressure) {
		this.oilPressure = oilPressure;
	}

	/**
	 * 获得精准油耗的值
	 * @return the preciseFuel 精准油耗  
	 */
	public long getPreciseFuel() {
		return preciseFuel;
	}

	/**
	 * 设置精准油耗的值
	 * @param preciseFuel 精准油耗  
	 */
	public void setPreciseFuel(long preciseFuel) {
		this.preciseFuel = preciseFuel;
	}

	/**
	 * 获得锁车状态的值
	 * @return the lockStatus 锁车状态  
	 */
	public String getLockStatus() {
		return lockStatus;
	}

	/**
	 * 设置锁车状态的值
	 * @param lockStatus 锁车状态  
	 */
	public void setLockStatus(String lockStatus) {
		this.lockStatus = lockStatus;
	}

	/**
	 * 获得在线状态的值
	 * @return the onlineStatus 在线状态  
	 */
	public int getOnlineStatus() {
		return onlineStatus;
	}

	/**
	 * 设置在线状态的值
	 * @param onlineStatus 在线状态  
	 */
	public void setOnlineStatus(int onlineStatus) {
		this.onlineStatus = onlineStatus;
	}

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
	 * 获得手机号的值
	 * @return the phoneNumber 手机号  
	 */
	public String getPhoneNumber() {
		return phoneNumber;
	}

	/**
	 * 设置手机号的值
	 * @param phoneNumber 手机号  
	 */
	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}

	/**
	 * 获得厂商编号的值
	 * @return the oemCode 厂商编号  
	 */
	public String getOemCode() {
		return oemCode;
	}

	/**
	 * 设置厂商编号的值
	 * @param oemCode 厂商编号  
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}

	/**
	 * 获得车牌颜色的值
	 * @return the plateColor 车牌颜色  
	 */
	public String getPlateColor() {
		return plateColor;
	}

	/**
	 * 设置车牌颜色的值
	 * @param plateColor 车牌颜色  
	 */
	public void setPlateColor(String plateColor) {
		this.plateColor = plateColor;
	}

	/**
	 * 获得车牌号的值
	 * @return the plate 车牌号  
	 */
	public String getPlate() {
		return plate;
	}

	/**
	 * 设置车牌号的值
	 * @param plate 车牌号  
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}

	/**
	 * 获得终端编号的值
	 * @return the tid 终端编号  
	 */
	public String getTid() {
		return tid;
	}

	/**
	 * 设置终端编号的值
	 * @param tid 终端编号  
	 */
	public void setTid(String tid) {
		this.tid = tid;
	}
	/**
	 * 获得车架号的值
	 * @return the vinCode 车架号  
	 */
	public String getVinCode() {
		return vinCode;
	}
	/**
	 * 设置车架号的值
	 * @param vinCode 车架号  
	 */
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	/**
	 * 获得行驶记录仪速度字符串的值
	 * @return the vssSpeedStr 行驶记录仪速度字符串  
	 */
	public String getVssSpeedStr() {
		return vssSpeedStr;
	}
	/**
	 * 设置行驶记录仪速度字符串的值
	 * @param vssSpeedStr 行驶记录仪速度字符串  
	 */
	public void setVssSpeedStr(String vssSpeedStr) {
		this.vssSpeedStr = vssSpeedStr;
	}
	/**
	 * 获得GPS速度字符串的值
	 * @return the gpsSpeedStr GPS速度字符串  
	 */
	public String getGpsSpeedStr() {
		return gpsSpeedStr;
	}
	/**
	 * 设置GPS速度字符串的值
	 * @param gpsSpeedStr GPS速度字符串  
	 */
	public void setGpsSpeedStr(String gpsSpeedStr) {
		this.gpsSpeedStr = gpsSpeedStr;
	}
	/**
	 * 获得指令的值
	 * @return the command 指令  
	 */
	public String getCommand() {
		return command;
	}
	/**
	 * 设置指令的值
	 * @param command 指令  
	 */
	public void setCommand(String command) {
		this.command = command;
	}
	/**
	 * 获得总油量（单位：L）213的值
	 * @return the oilTotal 总油量（单位：L）213  
	 */
	public long getOilTotal() {
		return oilTotal;
	}
	/**
	 * 设置总油量（单位：L）213的值
	 * @param oilTotal 总油量（单位：L）213  
	 */
	public void setOilTotal(long oilTotal) {
		this.oilTotal = oilTotal;
	}
	/**
	 * 获得报警附加信息(32)的值
	 * @return the alarmAdded 报警附加信息(32)  
	 */
	public String getAlarmAdded() {
		return alarmAdded;
	}
	/**
	 * 设置报警附加信息(32)的值
	 * @param alarmAdded 报警附加信息(32)  
	 */
	public void setAlarmAdded(String alarmAdded) {
		this.alarmAdded = alarmAdded;
	}

}
