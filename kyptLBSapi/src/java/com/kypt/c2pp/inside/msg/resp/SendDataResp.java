package com.kypt.c2pp.inside.msg.resp;

import com.kypt.c2pp.inside.msg.InsideMsgResp;

public class SendDataResp extends InsideMsgResp {
	public static final String COMMAND = "1010";

	public static final String STATUS = "0000";
	
	public SendDataResp(){
		super.setCommand(COMMAND);
	}
}
