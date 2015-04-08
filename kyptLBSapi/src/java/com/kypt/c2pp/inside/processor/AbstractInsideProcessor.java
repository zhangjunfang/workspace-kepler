package com.kypt.c2pp.inside.processor;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;
import com.kypt.nio.client.ICommunicateService;

public abstract class AbstractInsideProcessor<E extends InsideMsg, V extends ICommunicateService>
		implements IInsideProcessor<E, V> {

	public int parseHeader(byte[] buf, InsideMsg msg) throws ParseException {
		try {
			int location = 0;
			msg.setMsgLength(new String(buf, location, InsideMsg.MSGLENSIZE));
			location += InsideMsg.MSGLENSIZE;
			msg.setCommand(new String(buf, location, InsideMsg.COMMANDSIZE));
			location += InsideMsg.COMMANDSIZE;
			msg.setStatusCode(new String(buf, location,
					InsideMsg.STATUSCODESIZE));
			location += InsideMsg.STATUSCODESIZE;
			msg.setSeq(new String(buf, location, InsideMsg.SEQSIZE));
			location += InsideMsg.SEQSIZE;
			// System.out.println(msg.getMsgLength()+","+msg.getCommand()+","+msg.getStatusCode()+","+msg.getSeq());
			return location;
		} catch (Throwable t) {
			throw new ParseException("parse message header failed.", t);
		}

	}

	public void validHeader(E msg) throws InvalidMessageException {
		if (msg.getMsgLength() == null) {
			throw new InvalidMessageException("msgLength is invalid.");
		} else if (msg.getCommand() == null
				|| !InsideMsgUtils.checkMsgCommand(msg.getCommand())) {
			throw new InvalidMessageException("command is invalid.");
		} else if (msg.getStatusCode() == null
				|| !InsideMsgUtils.checkStatusCode(msg.getStatusCode())) {
			throw new InvalidMessageException("statusCode is invalid.");
		} else if (msg.getSeq() == null
				|| !InsideMsgUtils.checkSeq(Integer.parseInt(msg.getSeq()))) {
			throw new InvalidMessageException("sequence is invalid.");
		} else {
			return;
		}
	}
}
