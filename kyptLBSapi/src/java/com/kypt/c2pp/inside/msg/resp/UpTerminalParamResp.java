package com.kypt.c2pp.inside.msg.resp;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;

import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;
import com.kypt.c2pp.util.ValidationUtil;
import com.kypt.configuration.C2ppCfg;

public class UpTerminalParamResp extends InsideMsgResp {

	public static final String COMMAND = "D_GETP_UP";

	private int retry = 0;

	private HashMap hm;
	
	public UpTerminalParamResp(){
		super.setCommand(COMMAND);
	}

	public int getRetry() {
		return retry;
	}

	public void setRetry(int retry) {
		this.retry = retry;
	}

	public HashMap getHm() {
		return hm;
	}

	public void setHm(HashMap hm) throws Exception {
		if (ValidationUtil.validateTerminalParamId(hm)) {
			this.hm = hm;
		} else {
			throw new Exception("非法参数信息，请修改！");
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
		if (this.hm != null && this.hm.size() > 0) {
			return "CAITR " + this.getSeq() + " " + this.getOemId()+"_"+ this.getDeviceNo()
					+ " " + this.getCommType() + " " + "D_GETP {"
					+ InsideMsgUtils.convertHashMap2String(hm) + "}\r\n";
		} else {
			return "";
		}

	}

}
