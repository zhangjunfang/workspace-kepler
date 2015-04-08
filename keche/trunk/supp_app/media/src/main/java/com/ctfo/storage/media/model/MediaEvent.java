/**
 * 
 */
package com.ctfo.storage.media.model;

/**
 * 多媒体事件
 *
 */
public class MediaEvent {
	/**	车辆编号	*/
	private String plateId;
	/**	车牌号	*/
	private String plate;
	/**	多媒体编号	*/
	private String mediaId;
	/**	多媒体类型 0:图像，1：音频，2：视频	*/
	private int mediaType;
	/**	多媒体格式 0 JPEG 1: TIF; 2: MP3; 3: WAV; 4: WMV	*/
	private int mediaFormat;
	/**	事件类型编码 	0 平台下发 1 定时动作 2 抢劫报警 3 碰撞侧翻报警触发	*/
	private int eventType;
	/**	通道标识	*/
	private int channelId;
	/**	采集序号	*/
	private String takeSerial;
	/**	事件触发时间	*/
	private long eventTime;
	/**	系统时间	*/
	private long sysTime;
	
	/**
	 * @return 获取 车辆编号
	 */
	public String getPlateId() {
		return plateId;
	}
	/**
	 * 设置车辆编号
	 * @param plateId 车辆编号 
	 */
	public void setPlateId(String plateId) {
		this.plateId = plateId;
	}
	/**
	 * @return 获取 车牌号
	 */
	public String getPlate() {
		return plate;
	}
	/**
	 * 设置车牌号
	 * @param plate 车牌号 
	 */
	public void setPlate(String plate) {
		this.plate = plate;
	}
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
	 * @return 获取 多媒体类型0:图像，1：音频，2：视频
	 */
	public int getMediaType() {
		return mediaType;
	}
	/**
	 * 设置多媒体类型0:图像，1：音频，2：视频
	 * @param mediaType 多媒体类型0:图像，1：音频，2：视频 
	 */
	public void setMediaType(int mediaType) {
		this.mediaType = mediaType;
	}
	/**
	 * @return 获取 多媒体格式0JPEG1:TIF;2:MP3;3:WAV;4:WMV
	 */
	public int getMediaFormat() {
		return mediaFormat;
	}
	/**
	 * 设置多媒体格式0JPEG1:TIF;2:MP3;3:WAV;4:WMV
	 * @param mediaFormat 多媒体格式0JPEG1:TIF;2:MP3;3:WAV;4:WMV 
	 */
	public void setMediaFormat(int mediaFormat) {
		this.mediaFormat = mediaFormat;
	}
	/**
	 * @return 获取 事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发
	 */
	public int getEventType() {
		return eventType;
	}
	/**
	 * 设置事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发
	 * @param eventType 事件类型编码0平台下发1定时动作2抢劫报警3碰撞侧翻报警触发 
	 */
	public void setEventType(int eventType) {
		this.eventType = eventType;
	}
	/**
	 * @return 获取 通道标识
	 */
	public int getChannelId() {
		return channelId;
	}
	/**
	 * 设置通道标识
	 * @param channelId 通道标识 
	 */
	public void setChannelId(int channelId) {
		this.channelId = channelId;
	}
	/**
	 * @return 获取 采集序号
	 */
	public String getTakeSerial() {
		return takeSerial;
	}
	/**
	 * 设置采集序号
	 * @param takeSerial 采集序号 
	 */
	public void setTakeSerial(String takeSerial) {
		this.takeSerial = takeSerial;
	}
	/**
	 * @return 获取 事件触发时间
	 */
	public long getEventTime() {
		return eventTime;
	}
	/**
	 * 设置事件触发时间
	 * @param eventTime 事件触发时间 
	 */
	public void setEventTime(long eventTime) {
		this.eventTime = eventTime;
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
	
}
