/*******************************************************************************
 * @(#)UpLoadDataReq.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg.req;

import com.kypt.c2pp.inside.msg.InsideMsg;

public class UpLoadDataReq extends InsideMsg {

    public static final String COMMAND = "0011";

    public static final String STATUS = "0000";

    public static final int TERMINALIDSIZE = 20;

    public static final int PACKETLENSIZE = 8;

    private String terminalId;

    private String packetLen;

    private String packetContent;
    
    public UpLoadDataReq(){
		super.setCommand(COMMAND);
	}

    public void setTerminalId(String terminalId) {
        this.terminalId = (terminalId == null || terminalId.equals("")) ? null : terminalId.trim();
    }

    public String getTerminalId() {
        return terminalId;
    }

    public void setPacketLen(String packetLen) {
        this.packetLen = (packetLen == null || packetLen.equals("")) ? null : packetLen.trim();
    }

    public String getPacketLen() {
        return packetLen;
    }

    public void setPacketContent(String packetContent) {
        this.packetContent = (packetContent == null || packetContent.equals("")) ? null
                : packetContent.trim();
    }

    public String getPacketContent() {
        return packetContent;
    }
}
