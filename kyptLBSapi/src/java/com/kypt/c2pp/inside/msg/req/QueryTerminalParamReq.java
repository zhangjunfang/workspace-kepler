package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;

/**
 * 监管平台查询终端参数指令
 */
public class QueryTerminalParamReq extends InsideMsg {

	public static final String COMMAND = "0x8104";

	private String seq;

	private String oemId;
	
	private String deviceNo;

	private String commType;

	private String[] params;
	
	public QueryTerminalParamReq(){
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

		if (msg.length == 6) {
			String str = msg[5];
			str = str.substring(str.indexOf(":") + 1, str.length() - 1);
			this.params = str.split("\\|");
		} else {
			this.params = null;
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

	public String getOemId() {
		return oemId;
	}

	public void setOemId(String oemId) {
		this.oemId = oemId;
	}

	public String getDeviceNo() {
		return deviceNo;
	}

	public void setDeviceNo(String deviceNo) {
		this.deviceNo = deviceNo;
	}

}
