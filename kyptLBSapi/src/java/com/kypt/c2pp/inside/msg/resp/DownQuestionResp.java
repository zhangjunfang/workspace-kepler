package com.kypt.c2pp.inside.msg.resp;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.configuration.C2ppCfg;

/**
 * 设置提问下发应答
 */
public class DownQuestionResp extends InsideMsgResp {

	public static final String COMMAND = "0x0302";

	private String answerId;
	
	public DownQuestionResp(){
		super.setCommand(COMMAND);
	}

	public String getAnswerId() {
		return answerId;
	}

	public void setAnswerId(String answerId) {
		this.answerId=answerId;
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
		return "CAITR " + this.getSeq() + " " + this.getOemId()+"_"+this.getDeviceNo()
				+ " " + this.getCommType() + " " + "U_REPT {TYPE:32,84:"+this.getRespSeqId()+",82:"+this.getAnswerId()+"}\r\n";

	}

}
