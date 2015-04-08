package com.kypt.c2pp.inside.msg.resp;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.inside.vo.LocInfoVO;

/**
 * 位置信息汇报对象
 * @author yujch
 *
 */
public class LocationReportResp extends InsideMsgResp {

	public static final String COMMAND = "0x0200";
	
	private LocInfoVO locInfoVO=new LocInfoVO();
	
	public LocationReportResp(){
		super.setCommand(COMMAND);
	}

	public LocInfoVO getLocInfoVO() {
		return locInfoVO;
	}

	public void setLocInfoVO(LocInfoVO locInfoVO) {
		this.locInfoVO = locInfoVO;
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
		return "CAITS 0_0 " +this.getOemId()+"_"+ this.getDeviceNo() + " " + this.getCommType()+" U_REPT {TYPE:0,"+this.locInfoVO.toString()+"}"
				+ "\r\n";
	}

}
