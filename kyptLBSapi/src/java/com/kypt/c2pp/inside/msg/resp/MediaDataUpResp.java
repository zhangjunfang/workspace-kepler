package com.kypt.c2pp.inside.msg.resp;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;
import com.kypt.c2pp.inside.vo.MediaVO;
import com.kypt.configuration.C2ppCfg;

public class MediaDataUpResp extends InsideMsgResp {

	public static final String COMMAND = "0x0801_0";

	private MediaVO mediaVO = new MediaVO();
	
	public MediaDataUpResp(){
		super.setCommand(COMMAND);
	}

	public MediaVO getMediaVO() {
		return mediaVO;
	}

	public void setMediaVO(MediaVO mediaVO) {
		this.mediaVO = mediaVO;
	}

	@Override
	public byte[] getBytes() throws UnsupportedEncodingException {

		String req = this.toString();
		if (C2ppCfg.props.getProperty("superviseEncoding") != null
				&& C2ppCfg.props.getProperty("superviseEncoding").length() > 0) {
			return req.getBytes(C2ppCfg.props.getProperty("superviseEncoding"));
		} else {
			return req.getBytes();
		}

	}

	public String toString() {
		return "CAITS " + InsideMsgUtils.getCommandSeq() + " " + this.getOemId()+"_"+ this.getDeviceNo()
				+ " " + this.getCommType() + " D_REQD {TYPE:2,"
				+ this.mediaVO.toString() + "}" + "\r\n";
	}

}
