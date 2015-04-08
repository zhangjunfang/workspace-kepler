/*******************************************************************************
 * @(#)BindRespProcessor.java 2008-11-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.processor.resp;

import com.kypt.c2pp.inside.msg.resp.BindResp;
import com.kypt.c2pp.inside.processor.AbstractInsideProcessor;
import com.kypt.c2pp.nio.SupCommService;
import com.kypt.exception.HandleException;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;
/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-24 下午03:30:48
 */
public final class BindRespProcessor extends AbstractInsideProcessor<BindResp, SupCommService> {

    private static final BindRespProcessor PROCESSOR = new BindRespProcessor();

    private BindRespProcessor() {
    }

    public static BindRespProcessor getInstance() {
        return PROCESSOR;
    }

    public BindResp parse(byte[] buf) throws ParseException {
        try {
            BindResp resp = new BindResp();
            super.parseHeader(buf, resp);
            return resp;
        } catch (Throwable t) {
            throw new ParseException("parse bind response failed.", t);
        }
    }

    public void valid(BindResp msg) throws InvalidMessageException {
        super.validHeader(msg);
    }

    public void handle(BindResp msg, SupCommService communicateService) throws HandleException {
    }

	public BindResp parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		return null;
	}

	public BindResp parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		return null;
	}
}
