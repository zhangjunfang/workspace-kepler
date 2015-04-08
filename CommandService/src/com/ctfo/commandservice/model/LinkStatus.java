package com.ctfo.commandservice.model;
/**
 * 连接状态
 */
public class LinkStatus {
	/**	编号	*/
	private String id;
	/**	省域编号	*/
	private String areaId;
	/**	链接类型	*/
	private int likeType;
	/**	系统时间	*/
	private long utc;
	/**	上线时间	*/
	private long onlineUtc;
	/**	下线时间	*/
	private long offlineUtc;
	/**	接入码	*/
	private String accessCode;
	/**	通道码	*/
	private String channelCode;
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getAreaId() {
		return areaId;
	}
	public void setAreaId(String areaId) {
		this.areaId = areaId;
	}
	public int getLikeType() {
		return likeType;
	}
	public void setLikeType(int likeType) {
		this.likeType = likeType;
	}
	public long getUtc() {
		return utc;
	}
	public void setUtc(long utc) {
		this.utc = utc;
	}
	public long getOnlineUtc() {
		return onlineUtc;
	}
	public void setOnlineUtc(long onlineUtc) {
		this.onlineUtc = onlineUtc;
	}
	public long getOfflineUtc() {
		return offlineUtc;
	}
	public void setOfflineUtc(long offlineUtc) {
		this.offlineUtc = offlineUtc;
	}
	public String getAccessCode() {
		return accessCode;
	}
	public void setAccessCode(String accessCode) {
		this.accessCode = accessCode;
	}
	public String getChannelCode() {
		return channelCode;
	}
	public void setChannelCode(String channelCode) {
		this.channelCode = channelCode;
	}
	
}
