package com.ctfo.storage.process.model;

import java.io.Serializable;

public class Media extends BaseModel implements Serializable {
	private static final long serialVersionUID = 1L;
	/** 文件名 		*/
	private String fileName = "";
	/** 多媒体ID 	*/
	private String mediaId = "";
	/** 多媒体类型 	*/
	private String mediaType = "";
	/** 多媒体格式编码 	*/
	private String mediaFormatCode = "";
	/** 事件项编码 	*/
	private String eventCode = "";
	/** 通道ID  */
	private String ChannelId = "";
	/** 位置信息汇报BYTE[28] */
	private String locationData = "";
	/** 多媒体数据包(图片数据) */
	private byte[] mediaData = null;
	
	
	/**
	 * 获取多媒体ID的值
	 * @return mediaId  
	 */
	public String getMediaId() {
		return mediaId;
	}
	/**
	 * 设置多媒体ID的值
	 * @param mediaId
	 */
	public void setMediaId(String mediaId) {
		this.mediaId = mediaId;
	}
	/**
	 * 获取多媒体类型的值
	 * @return mediaType  
	 */
	public String getMediaType() {
		return mediaType;
	}
	/**
	 * 设置多媒体类型的值
	 * @param mediaType
	 */
	public void setMediaType(String mediaType) {
		this.mediaType = mediaType;
	}
	/**
	 * 获取多媒体格式编码的值
	 * @return mediaFormatCode  
	 */
	public String getMediaFormatCode() {
		return mediaFormatCode;
	}
	/**
	 * 设置多媒体格式编码的值
	 * @param mediaFormatCode
	 */
	public void setMediaFormatCode(String mediaFormatCode) {
		this.mediaFormatCode = mediaFormatCode;
	}
	/**
	 * 获取事件项编码的值
	 * @return eventCode  
	 */
	public String getEventCode() {
		return eventCode;
	}
	/**
	 * 设置事件项编码的值
	 * @param eventCode
	 */
	public void setEventCode(String eventCode) {
		this.eventCode = eventCode;
	}
	/**
	 * 获取channelId的值
	 * @return channelId  
	 */
	public String getChannelId() {
		return ChannelId;
	}
	/**
	 * 设置channelId的值
	 * @param channelId
	 */
	public void setChannelId(String channelId) {
		ChannelId = channelId;
	}
	/**
	 * 获取位置信息汇报BYTE[28]的值
	 * @return locationData  
	 */
	public String getLocationData() {
		return locationData;
	}
	/**
	 * 设置位置信息汇报BYTE[28]的值
	 * @param locationData
	 */
	public void setLocationData(String locationData) {
		this.locationData = locationData;
	}
	/**
	 * 获取多媒体数据包(图片数据)的值
	 * @return mediaData  
	 */
	public byte[] getMediaData() {
		return mediaData;
	}
	/**
	 * 设置多媒体数据包(图片数据)的值
	 * @param mediaData
	 */
	public void setMediaData(byte[] mediaData) {
		this.mediaData = mediaData;
	}
	/**
	 * 获取文件名的值
	 * @return fileName  
	 */
	public String getFileName() {
		return fileName;
	}
	/**
	 * 设置文件名的值
	 * @param fileName
	 */
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}
	
	

}
