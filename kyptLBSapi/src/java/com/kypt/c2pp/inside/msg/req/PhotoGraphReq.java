package com.kypt.c2pp.inside.msg.req;

	import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;

	/**
	 * 监管平台发送的终端拍照指令
	 */
	public class PhotoGraphReq extends InsideMsg {

		public static final String COMMAND = "0x8801";

		private String seq;

		private String oemId;

		private String commType;

		private String paramType;

		private String deviceNo;
		
		private String retry;
		
		private String channelId;
		
		private String optType;
		
		private String saveTime;
		
		private String saveFlag;
		
		private String dpi;
		
		private String picQuality;
		
		private String luminance;
		
		private String contrast;
		
		private String saturation;
		
		private String chroma;

		public PhotoGraphReq(){
			super.setCommand(COMMAND);
		}

		public String getSeq() {
			return seq;
		}

		public void setSeq(String seq) {
			this.seq = seq;
		}

		public String getCommType() {
			return commType;
		}

		public void setCommType(String commType) {
			this.commType = commType;
		}

		public void setBody(String[] msg) {
			this.seq = msg[1];
			String macId[] = msg[2].split("_");
			this.oemId=macId[0];
			this.deviceNo=macId[1];
			this.commType = msg[3];

			String msgTemp[] = msg[5].substring(1, msg[5].length() - 1).split(",");

			for (int i = 0; i < msgTemp.length; i++) {
				String msgKV[] = msgTemp[i].split(":");
				if (msgKV[0].equals("TYPE")){
					this.paramType=msgKV[1];
					continue;
				}
				if (msgKV[0].equals("RETRY")){
					this.retry=msgKV[1];
					continue;
				}
				if (msgKV[0].equals("VALUE")){
					String photoStr[] = msgKV[1].split("\\|");
					if (photoStr.length==10){
						this.channelId=photoStr[0];
						this.optType=photoStr[1];
						this.saveTime=photoStr[2];
						this.saveFlag=photoStr[3];
						this.dpi=photoStr[4];
						this.picQuality=photoStr[5];
						this.luminance=photoStr[6];
						this.contrast=photoStr[7];
						this.saturation=photoStr[8];
						this.chroma=photoStr[9];
					}
					continue;
				}
			}

		}

		@Override
		public byte[] getBytes() throws UnsupportedEncodingException {

			String req = this.toString();
			if (this.getEncoding() != null && this.getEncoding().length() > 0) {
				return req.getBytes(this.getEncoding());
			} else {
				return req.getBytes();
			}

		}

		public String toString() {
			return "";

		}

		public String getParamType() {
			return paramType;
		}

		public void setParamType(String paramType) {
			this.paramType = paramType;
		}

		public String getDeviceNo() {
			return deviceNo;
		}

		public void setDeviceNo(String deviceNo) {
			this.deviceNo = deviceNo;
		}

		public String getRetry() {
			return retry;
		}

		public void setRetry(String retry) {
			this.retry = retry;
		}

		public String getChannelId() {
			return channelId;
		}

		public void setChannelId(String channelId) {
			this.channelId = channelId;
		}

		public String getOptType() {
			return optType;
		}

		public void setOptType(String optType) {
			this.optType = optType;
		}

		public String getSaveTime() {
			return saveTime;
		}

		public void setSaveTime(String saveTime) {
			this.saveTime = saveTime;
		}

		public String getSaveFlag() {
			return saveFlag;
		}

		public void setSaveFlag(String saveFlag) {
			this.saveFlag = saveFlag;
		}

		public String getDpi() {
			return dpi;
		}

		public void setDpi(String dpi) {
			this.dpi = dpi;
		}

		public String getPicQuality() {
			return picQuality;
		}

		public void setPicQuality(String picQuality) {
			this.picQuality = picQuality;
		}

		public String getLuminance() {
			return luminance;
		}

		public void setLuminance(String luminance) {
			this.luminance = luminance;
		}

		public String getContrast() {
			return contrast;
		}

		public void setContrast(String contrast) {
			this.contrast = contrast;
		}

		public String getSaturation() {
			return saturation;
		}

		public void setSaturation(String saturation) {
			this.saturation = saturation;
		}

		public String getChroma() {
			return chroma;
		}

		public void setChroma(String chroma) {
			this.chroma = chroma;
		}

		public String getOemId() {
			return oemId;
		}

		public void setOemId(String oemId) {
			this.oemId = oemId;
		}

	}
