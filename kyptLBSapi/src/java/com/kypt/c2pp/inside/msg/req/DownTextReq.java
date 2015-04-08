package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.vo.FlagVO;
import com.kypt.c2pp.util.Base64;

/**
 * 监管平台设置终端参数指令
 */
public class DownTextReq extends InsideMsg {

	public static final String COMMAND = "0x8300";

	private String seq;

	private String oemId;
	
	private String deviceNo;

	private String commType;

	private FlagVO flagVO = new FlagVO();

	private String smsInfo;
	
	public DownTextReq(){
		this.setCommand(COMMAND);
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

	public void setBody(String[] msg) throws UnsupportedEncodingException {
		this.seq = msg[1];
		String macId[] = msg[2].split("_");
		this.oemId=macId[0];
		this.deviceNo=macId[1];
		this.commType = msg[3];

		String msgTemp[] = msg[5].substring(1, msg[5].length() - 1).split(",");

		for (int i = 0; i < msgTemp.length; i++) {
			String msgKV[] = msgTemp[i].split(":");

			if (msgKV[0].equals("1")) {
				this.flagVO.SetBody(msgKV[1]);
				continue;
			}
			if (msgKV[0].equals("2")) {
				this.smsInfo = msgKV[1];
				continue;
			}
		}

		this.smsInfo = Base64.decode(this.smsInfo,"GBK");

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

	public FlagVO getFlagVO() {
		return flagVO;
	}

	public void setFlagVO(FlagVO flagVO) {
		this.flagVO = flagVO;
	}

	public String getSmsInfo() {
		return smsInfo;
	}

	public void setSmsInfo(String smsInfo) {
		this.smsInfo = smsInfo;
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
