package com.ctfo.storage.media.model;


public class Media{
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
	/** 位置信息汇报BYTE[28] */
	private String track = "";
	/** 多媒体数据包(数据) */
	private byte[] mediaData = null;
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
	 * @return 获取 位置信息汇报BYTE[28]
	 */
	public String getTrack() {
		return track;
	}
	/**
	 * 设置位置信息汇报BYTE[28]
	 * @param track 位置信息汇报BYTE[28] 
	 */
	public void setTrack(String track) {
		this.track = track;
	}
	/**
	 * @return 获取 多媒体数据包(数据)
	 */
	public byte[] getMediaData() {
		return mediaData;
	}
	/**
	 * 设置多媒体数据包(数据)
	 * @param mediaData 多媒体数据包(数据) 
	 */
	public void setMediaData(byte[] mediaData) {
		this.mediaData = mediaData;
	}
	
}
