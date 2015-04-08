package com.kypt.c2pp.inside.vo;

import java.util.Date;

import com.kypt.c2pp.util.ValidationUtil.EVENT_ENTRY;

public class MediaVO {
	
	private String mediaId;
	
	private String mediaType;
	
	private String mediaMode;
	
	private String eventId;
	
	private String channelId;
	
	private String mediaUri;
	
	private String delFlag;
	
	private String begin;
	
	private String end;
	
	private byte[] media;

	public String getMediaId() {
		return mediaId;
	}

	public void setMediaId(String mediaId) {
		this.mediaId = mediaId;
	}

	public String getMediaType() {
		return mediaType;
	}

	public void setMediaType(String mediaType) {
		this.mediaType = mediaType;
	}

	public String getMediaMode() {
		return mediaMode;
	}

	public void setMediaMode(String mediaMode) {
		this.mediaMode = mediaMode;
	}

	public String getEventId() {
		return eventId;
	}

	public void setEventId(EVENT_ENTRY entry) {
		switch(entry){
		case platsend:
			this.eventId = "0";
		case timingaction:
			this.eventId = "1";
		case robalarm:
			this.eventId = "2";
		case collisionalarm:
			this.eventId = "3";
		}
		
	}

	public String getChannelId() {
		return channelId;
	}

	public void setChannelId(String channelId) {
		this.channelId = channelId;
	}

	public String getMediaUri() {
		return mediaUri;
	}

	public void setMediaUri(String mediaUri) {
		this.mediaUri = mediaUri;
	}

	public byte[] getMedia() {
		return media;
	}

	public void setMedia(byte[] media) {
		this.media = media;
	}
	
	public String getString(){
		StringBuffer sb=new StringBuffer();
		sb.append(this.mediaId+"|"+this.mediaType+"|"+this.mediaMode+"|"+this.eventId+"|"+this.channelId+"|"+this.mediaUri);
		return sb.toString();
	}

	public String getDelFlag() {
		return delFlag;
	}

	public void setDelFlag(String delFlag) {
		this.delFlag = delFlag;
	}

	public void setEventId(String eventId) {
		this.eventId = eventId;
	}

	public String getBegin() {
		return begin;
	}

	public void setBegin(String begin) {
		this.begin = begin;
	}

	public String getEnd() {
		return end;
	}

	public void setEnd(String end) {
		this.end = end;
	}
	

}
