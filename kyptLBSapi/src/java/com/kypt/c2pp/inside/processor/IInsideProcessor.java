/*******************************************************************************
 * @(#)IInsideProcessor.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.processor;

import com.kypt.exception.HandleException;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;
import com.kypt.nio.client.ICommunicateService;
import com.kypt.c2pp.inside.msg.InsideMsg;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午06:08:10
 */
public interface IInsideProcessor<E extends InsideMsg, V extends ICommunicateService> {

	/**
	 * check whether the msg received is valid
	 * 
	 * @param msg
	 * @throws InvalidMessageException
	 */
	void valid(E msg) throws InvalidMessageException;

	/**
	 * parse the message received
	 * 
	 * @param buf
	 * @return
	 * @throws ParseException
	 * @throws com.kypt.exception.ParseException
	 */
	E parse(byte[] buf) throws ParseException,
			com.kypt.exception.ParseException;

	/**
	 * parse the message received
	 * 
	 * @param msg
	 * @return
	 * @throws ParseException
	 */
	E parse(String msg) throws ParseException;

	/**
	 * parse the message received
	 * 
	 * @param msg
	 *            []
	 * @return
	 * @throws ParseException
	 */
	E parse(String msg[]) throws ParseException;

	/**
	 * handle the message received
	 * 
	 * @param msg
	 * @param session
	 * @throws HandleException
	 */
	void handle(E msg, V communicateService) throws HandleException;
}
