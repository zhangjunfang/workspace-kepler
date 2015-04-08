/*******************************************************************************
 * @(#)Cm.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;



/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午05:02:36
 */
public class InsideMsg {

    /**
     * the whole message size(byte)
     */
    public static final int MSGLENSIZE = 8;

    /**
     * message command size(byte)
     */
    public static final int COMMANDSIZE = 4;

    /**
     * message status code size(byte)
     */
    public static final int STATUSCODESIZE = 4;

    /**
     * message sequence size(byte)
     */
    public static final int SEQSIZE = 4;

    /**
     * message header length(byte)
     */
    public static final int MSGHEADERSIZE = 20;

    /**
     * the whole message length
     */
    private String msgLength;

    /**
     * message command
     */
    private String command;

    /**
     * message status code
     */
    private String statusCode;

    /**
     * message sequence
     */
    private String seq;
    
    private String encoding="UTF-8";

    /**
     * @param msgLength The msgLength to set.
     */
    public void setMsgLength(String msgLength) {
        this.msgLength = (msgLength == null || msgLength.equals("")) ? null : InsideMsgUtils
                .formatMsgLen(Integer.parseInt(msgLength));
    }

    /**
     * @return Returns the msgLength.
     */
    public String getMsgLength() {
        return msgLength;
    }

    /**
     * @param command The command to set.
     */
    public void setCommand(String command) {
        this.command = (command == null || command.equals("")) ? null : command.trim();
    }

    /**
     * @return Returns the command.
     */
    public String getCommand() {
        return command;
    }

    /**
     * @param statusCode The statusCode to set.
     */
    public void setStatusCode(String statusCode) {
        this.statusCode = (statusCode == null || statusCode.equals("")) ? null : InsideMsgUtils
                .formatStatusCode(statusCode);
    }

    /**
     * @return Returns the statusCode.
     */
    public String getStatusCode() {
        return statusCode;
    }

    /**
     * @param seq The seq to set.
     */
    public void setSeq(String seq) {
        this.seq = (seq == null || seq.equals("")) ? null : InsideMsgUtils.formatSeq(seq);
    }

    /**
     * @return Returns the seq.
     */
    public String getSeq() {
        return seq;
    }
    
	public String getEncoding() {
		return encoding;
	}

	public void setEncoding(String encoding) {
		this.encoding = encoding;
	}

    /**
     * @return the msg header byte array
     * @throws UnsupportedEncodingException 
     */
    public byte[] getBytes() throws UnsupportedEncodingException {
        int location = 0;
        byte[] buf = new byte[MSGHEADERSIZE];
        System.arraycopy(this.getMsgLength().getBytes(), 0, buf, location, MSGLENSIZE);
        location += MSGLENSIZE;
        System.arraycopy(this.getCommand().getBytes(), 0, buf, location, COMMANDSIZE);
        location += COMMANDSIZE;
        System.arraycopy(this.getStatusCode().getBytes(), 0, buf, location, STATUSCODESIZE);
        location += STATUSCODESIZE;
        System.arraycopy(this.getSeq().getBytes(), 0, buf, location, SEQSIZE);
        return buf;
    }

}
