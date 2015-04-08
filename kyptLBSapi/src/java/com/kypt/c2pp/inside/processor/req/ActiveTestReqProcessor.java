/*******************************************************************************
 * @(#)ActiveTestRequestProcessor.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.processor.req;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.inside.msg.InsideMsgFactory;
import com.kypt.c2pp.inside.msg.req.ActiveTestReq;
import com.kypt.c2pp.inside.processor.AbstractInsideProcessor;
import com.kypt.c2pp.nio.SupCommService;
import com.kypt.exception.HandleException;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;
import com.kypt.log.LogFormatter;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午05:24:18
 */
public final class ActiveTestReqProcessor extends
		AbstractInsideProcessor<ActiveTestReq, SupCommService> {

	private Logger log = LoggerFactory.getLogger(ActiveTestReqProcessor.class);

	public static final ActiveTestReqProcessor PROCESSOR = new ActiveTestReqProcessor();

	private ActiveTestReqProcessor() {
	}

	public static ActiveTestReqProcessor getInstance() {
		return PROCESSOR;
	}

	public ActiveTestReq parse(byte[] buf) throws ParseException {
		try {
			ActiveTestReq req = new ActiveTestReq();
			super.parseHeader(buf, req);
			return req;
		} catch (Throwable t) {
			throw new ParseException("parse active test requset failed.", t);
		}
	}

	public void valid(ActiveTestReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(ActiveTestReq msg, SupCommService nioService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("ActiveTestReqProcessor",
					"receive a active test request."));
			nioService.send(InsideMsgFactory.createActiveTestResp(msg.getSeq())
					.getBytes());
			log.info(LogFormatter.formatMsg("ActiveTestReqProcessor",
					"send a active test response."));
		} catch (Throwable t) {
			throw new HandleException("handle active test request failed.", t);
		}
	}

	public ActiveTestReq parse(String msg)
			throws com.kypt.exception.ParseException {
		// TODO Auto-generated method stub
		return null;
	}

	public ActiveTestReq parse(String[] msg)
			throws com.kypt.exception.ParseException {
		// TODO Auto-generated method stub
		return null;
	}
}
