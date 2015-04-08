package com.kypt.c2pp.inside.msg.req;

	import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;

	/**
	 * 监管平台发送的终端监听指令
	 */
	public class TerminalListenerSeq extends InsideMsg {

		public static final String COMMAND = "0x8400";

		private String seq;

		private String oemId;
		
		private String deviceNo;

		private String commType;

		private String paramType;

		private String phone;
		
		private String retry;

		public TerminalListenerSeq(){
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
			this.oemId = macId[0];
			this.deviceNo = macId[1];
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
					this.deviceNo=msgKV[1];
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

		public String getOemId() {
			return oemId;
		}

		public void setOemId(String oemId) {
			this.oemId = oemId;
		}

		public String getPhone() {
			return phone;
		}

		public void setPhone(String phone) {
			this.phone = phone;
		}

	}
