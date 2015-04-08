package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.vo.MediaVO;

/**
 * 监管平台多媒体数据查询
 */
public class MediaDataQueryReq extends InsideMsg {

	public static final String COMMAND = "0x8802";

	private String seq;

	private String oemId;
	
	private String deviceNo;

	private String commType;

	private String paramType;

	private MediaVO mediaVO;
	
	public MediaDataQueryReq(){
		super.setCommand(COMMAND);
	}

	public String getSeq() {
		return seq;
	}

	public void setSeq(String seq) {
		this.seq = seq;
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

	public MediaVO getMediaVO() {
		return mediaVO;
	}

	public void setMediaVO(MediaVO mediaVO) {
		this.mediaVO = mediaVO;
	}

	public String getCommType() {
		return commType;
	}

	public void setCommType(String commType) {
		this.commType = commType;
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

	public void setBody(String[] msg) throws UnsupportedEncodingException {
		this.seq = msg[1];
		String macId[] = msg[2].split("_");
		this.oemId=macId[0];
		this.deviceNo=macId[1];
		this.commType = msg[3];

		String msgTemp[] = msg[5].substring(1, msg[5].length() - 1).split(",");

		for (int i = 0; i < msgTemp.length; i++) {
			String msgKV[] = msgTemp[i].split(":");

			if (msgKV[0].equals("TYPE")) {
				this.paramType = msgKV[1];
				continue;
			}

			if (msgKV[0].equals("1")) {
				this.mediaVO.setMediaType(msgKV[1]);
				continue;
			}

			if (msgKV[0].equals("2")) {
				this.mediaVO.setChannelId(msgKV[1]);
				continue;
			}

			if (msgKV[0].equals("3")) {
				this.mediaVO.setEventId(msgKV[1]);
				continue;
			}

			if (msgKV[0].equals("4")) {
				this.mediaVO.setBegin(msgKV[1]);
				continue;
			}

			if (msgKV[0].equals("5")) {
				this.mediaVO.setEnd(msgKV[1]);
				continue;
			}

			if (msgKV[0].equals("6")) {
				this.mediaVO.setDelFlag(msgKV[1]);
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

}
