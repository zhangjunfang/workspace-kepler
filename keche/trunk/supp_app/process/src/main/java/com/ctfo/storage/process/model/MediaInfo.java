package com.ctfo.storage.process.model;

import java.io.Serializable;

public class MediaInfo implements Serializable {
	private static final long serialVersionUID = 1L;
	/**	多媒体编号	*/
	private String mediaId;
	/**	车辆编号	*/
	private String plate;
	/**	车辆类型	*/
	private String vehicleType;
	/**	手机号	*/
	private String phoneNumber;
	/**	多媒体类型 (0:图像，1：音频，2：视频)	*/
	private int mediaType;
	/**	多媒体格式 (0:JPEG 1:TIF; 2:MP3; 3:WAV; 4:WMV)	*/
	private int mediaFormat;
	/**	事件类型编码 	0 平台下发 1 定时动作 2 抢劫报警 3 碰撞侧翻报警触发; 4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里 */
	private int eventType;
	/**	多媒体上传时间	*/
	private long eventUpTime;
	/**	多媒体访问地址	*/
	private String mediaUrl;
	/**	通道编号	*/
	private String channel;
	/**	多媒体数据大小（单位：字节）	*/
	private int mediaSize;
	/**	多媒体规格(1:320x240, 2:640x480, 3:800x600, 4:1024x768)	*/
	private int ImageUnit;
	/**	采样频率	*/
	private String sampleRate;
	/**	经度	*/
	private long lon;
	/**	纬度	*/
	private long lat;
	/**	偏移经度	*/
	private long maplon;
	/**	偏移纬度	*/
	private long maplat;
	/**	海拔（单位：米）	*/
	private int elevation;
	/**	方向（单位：度）	*/
	private int direction;
	/**	GPS速度（单位：米/小时）	*/
	private int gpsSpeed;
	/**	状态信息, 多值用逗号分隔	*/
	private String statusCode;
	/**	报警信息，多值用逗号分隔	*/
	private String alarmCode;
	/**	系统时间	*/
	private long sysTime;
	/**	是否超载(0 否 1 是)	*/
	private int isOverload;
	/**	事件状态（0 成功1 失败 2执行中）	*/
	private int eventStatus;
	/**	有效标记 1:有效 0:无效 默认为1	*/
	private int enableFlag;
	/**	指令流水号	*/
	private String seq;
	/**	发送人编号	*/
	private String sendUserId;
	/**	多媒体数据ID（合规新加)	*/
	private String mediaDataId;
	/**	事件触发时间	*/
	private long eventTriggerTime;
	/**	已读标识（0:未读，1:已读）	*/
	private int readFlag;
	/**	超员人数	*/
	private int overloadNum;
	/**	标记超员者编号	*/
	private long overloadById;
	/**	标记超员时间	*/
	private long overloadTime;
	/**	设备类型(0:2G;1:3G)	*/
	private int devType;
	/**	备注	*/
	private String memo;
	/**
	 * @return 获取 多媒体编号
	 */
	public String getMediaId() {
		return mediaId;
	}
	/**
	 * 设置多媒体编号
	 * @param mediaId 多媒体编号 
	 */
	public void setMediaId(String mediaId) {
		this.mediaId = mediaId;
	}
	/**
	 * @return 获取 车辆编号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车辆编号
	 * @param plate 车辆编号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
	/**
	 * @return 获取 车辆类型
	 */
	public String getVehicleType() {
		return vehicleType;
	}
	/**
	 * 设置车辆类型
	 * @param vehicleType 车辆类型 
	 */
	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
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
	 * @return 获取 多媒体类型(0:图像，1：音频，2：视频)
	 */
	public int getMediaType() {
		return mediaType;
	}
	/**
	 * 设置多媒体类型(0:图像，1：音频，2：视频)
	 * @param mediaType 多媒体类型(0:图像，1：音频，2：视频) 
	 */
	public void setMediaType(int mediaType) {
		this.mediaType = mediaType;
	}
	/**
	 * @return 获取 多媒体格式(0:JPEG1:TIF;2:MP3;3:WAV;4:WMV)
	 */
	public int getMediaFormat() {
		return mediaFormat;
	}
	/**
	 * 设置多媒体格式(0:JPEG1:TIF;2:MP3;3:WAV;4:WMV)
	 * @param mediaFormat 多媒体格式(0:JPEG1:TIF;2:MP3;3:WAV;4:WMV) 
	 */
	public void setMediaFormat(int mediaFormat) {
		this.mediaFormat = mediaFormat;
	}
	/**
	 * @return 获取 事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发;4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里
	 */
	public int getEventType() {
		return eventType;
	}
	/**
	 * 设置事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发;4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里
	 * @param eventType 事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发;4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里 
	 */
	public void setEventType(int eventType) {
		this.eventType = eventType;
	}
	/**
	 * @return 获取 多媒体上传时间
	 */
	public long getEventUpTime() {
		return eventUpTime;
	}
	/**
	 * 设置多媒体上传时间
	 * @param eventUpTime 多媒体上传时间 
	 */
	public void setEventUpTime(long eventUpTime) {
		this.eventUpTime = eventUpTime;
	}
	/**
	 * @return 获取 多媒体访问地址
	 */
	public String getMediaUrl() {
		return mediaUrl;
	}
	/**
	 * 设置多媒体访问地址
	 * @param mediaUrl 多媒体访问地址 
	 */
	public void setMediaUrl(String mediaUrl) {
		this.mediaUrl = mediaUrl;
	}
	/**
	 * @return 获取 通道编号
	 */
	public String getChannel() {
		return channel;
	}
	/**
	 * 设置通道编号
	 * @param channel 通道编号 
	 */
	public void setChannel(String channel) {
		this.channel = channel;
	}
	/**
	 * @return 获取 多媒体数据大小（单位：字节）
	 */
	public int getMediaSize() {
		return mediaSize;
	}
	/**
	 * 设置多媒体数据大小（单位：字节）
	 * @param mediaSize 多媒体数据大小（单位：字节） 
	 */
	public void setMediaSize(int mediaSize) {
		this.mediaSize = mediaSize;
	}
	/**
	 * @return 获取 imageUnit
	 */
	public int getImageUnit() {
		return ImageUnit;
	}
	/**
	 * 设置imageUnit
	 * @param imageUnit imageUnit 
	 */
	public void setImageUnit(int imageUnit) {
		ImageUnit = imageUnit;
	}
	/**
	 * @return 获取 采样频率
	 */
	public String getSampleRate() {
		return sampleRate;
	}
	/**
	 * 设置采样频率
	 * @param sampleRate 采样频率 
	 */
	public void setSampleRate(String sampleRate) {
		this.sampleRate = sampleRate;
	}
	/**
	 * @return 获取 经度
	 */
	public long getLon() {
		return lon;
	}
	/**
	 * 设置经度
	 * @param lon 经度 
	 */
	public void setLon(long lon) {
		this.lon = lon;
	}
	/**
	 * @return 获取 纬度
	 */
	public long getLat() {
		return lat;
	}
	/**
	 * 设置纬度
	 * @param lat 纬度 
	 */
	public void setLat(long lat) {
		this.lat = lat;
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
	 * @return 获取 方向（单位：度）
	 */
	public int getDirection() {
		return direction;
	}
	/**
	 * 设置方向（单位：度）
	 * @param direction 方向（单位：度） 
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	/**
	 * @return 获取 GPS速度（单位：米小时）
	 */
	public int getGpsSpeed() {
		return gpsSpeed;
	}
	/**
	 * 设置GPS速度（单位：米小时）
	 * @param gpsSpeed GPS速度（单位：米小时） 
	 */
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}
	/**
	 * @return 获取 状态信息多值用逗号分隔
	 */
	public String getStatusCode() {
		return statusCode;
	}
	/**
	 * 设置状态信息多值用逗号分隔
	 * @param statusCode 状态信息多值用逗号分隔 
	 */
	public void setStatusCode(String statusCode) {
		this.statusCode = statusCode;
	}
	/**
	 * @return 获取 报警信息，多值用逗号分隔
	 */
	public String getAlarmCode() {
		return alarmCode;
	}
	/**
	 * 设置报警信息，多值用逗号分隔
	 * @param alarmCode 报警信息，多值用逗号分隔 
	 */
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	/**
	 * @return 获取 系统时间
	 */
	public long getSysTime() {
		return sysTime;
	}
	/**
	 * 设置系统时间
	 * @param sysTime 系统时间 
	 */
	public void setSysTime(long sysTime) {
		this.sysTime = sysTime;
	}
	/**
	 * @return 获取 是否超载(0否1是)
	 */
	public int getIsOverload() {
		return isOverload;
	}
	/**
	 * 设置是否超载(0否1是)
	 * @param isOverload 是否超载(0否1是) 
	 */
	public void setIsOverload(int isOverload) {
		this.isOverload = isOverload;
	}
	/**
	 * @return 获取 事件状态（0成功1失败2执行中）
	 */
	public int getEventStatus() {
		return eventStatus;
	}
	/**
	 * 设置事件状态（0成功1失败2执行中）
	 * @param eventStatus 事件状态（0成功1失败2执行中） 
	 */
	public void setEventStatus(int eventStatus) {
		this.eventStatus = eventStatus;
	}
	/**
	 * @return 获取 有效标记1:有效0:无效默认为1
	 */
	public int getEnableFlag() {
		return enableFlag;
	}
	/**
	 * 设置有效标记1:有效0:无效默认为1
	 * @param enableFlag 有效标记1:有效0:无效默认为1 
	 */
	public void setEnableFlag(int enableFlag) {
		this.enableFlag = enableFlag;
	}
	/**
	 * @return 获取 指令流水号
	 */
	public String getSeq() {
		return seq;
	}
	/**
	 * 设置指令流水号
	 * @param seq 指令流水号 
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}
	/**
	 * @return 获取 发送人编号
	 */
	public String getSendUserId() {
		return sendUserId;
	}
	/**
	 * 设置发送人编号
	 * @param sendUserId 发送人编号 
	 */
	public void setSendUserId(String sendUserId) {
		this.sendUserId = sendUserId;
	}
	/**
	 * @return 获取 多媒体数据ID（合规新加)
	 */
	public String getMediaDataId() {
		return mediaDataId;
	}
	/**
	 * 设置多媒体数据ID（合规新加)
	 * @param mediaDataId 多媒体数据ID（合规新加) 
	 */
	public void setMediaDataId(String mediaDataId) {
		this.mediaDataId = mediaDataId;
	}
	/**
	 * @return 获取 事件触发时间
	 */
	public long getEventTriggerTime() {
		return eventTriggerTime;
	}
	/**
	 * 设置事件触发时间
	 * @param eventTriggerTime 事件触发时间 
	 */
	public void setEventTriggerTime(long eventTriggerTime) {
		this.eventTriggerTime = eventTriggerTime;
	}
	/**
	 * @return 获取 已读标识（0:未读，1:已读）
	 */
	public int getReadFlag() {
		return readFlag;
	}
	/**
	 * 设置已读标识（0:未读，1:已读）
	 * @param readFlag 已读标识（0:未读，1:已读） 
	 */
	public void setReadFlag(int readFlag) {
		this.readFlag = readFlag;
	}
	/**
	 * @return 获取 超员人数
	 */
	public int getOverloadNum() {
		return overloadNum;
	}
	/**
	 * 设置超员人数
	 * @param overloadNum 超员人数 
	 */
	public void setOverloadNum(int overloadNum) {
		this.overloadNum = overloadNum;
	}
	/**
	 * @return 获取 标记超员者编号
	 */
	public long getOverloadById() {
		return overloadById;
	}
	/**
	 * 设置标记超员者编号
	 * @param overloadById 标记超员者编号 
	 */
	public void setOverloadById(long overloadById) {
		this.overloadById = overloadById;
	}
	/**
	 * @return 获取 标记超员时间
	 */
	public long getOverloadTime() {
		return overloadTime;
	}
	/**
	 * 设置标记超员时间
	 * @param overloadTime 标记超员时间 
	 */
	public void setOverloadTime(long overloadTime) {
		this.overloadTime = overloadTime;
	}
	/**
	 * @return 获取 设备类型(0:2G;1:3G)
	 */
	public int getDevType() {
		return devType;
	}
	/**
	 * 设置设备类型(0:2G;1:3G)
	 * @param devType 设备类型(0:2G;1:3G) 
	 */
	public void setDevType(int devType) {
		this.devType = devType;
	}
	/**
	 * @return 获取 备注
	 */
	public String getMemo() {
		return memo;
	}
	/**
	 * 设置备注
	 * @param memo 备注 
	 */
	public void setMemo(String memo) {
		this.memo = memo;
	}
}
