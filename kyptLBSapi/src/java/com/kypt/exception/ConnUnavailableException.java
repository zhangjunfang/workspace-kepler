/*******************************************************************************
 * @(#)HandleException.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.exception;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午05:17:52
 */
public class ConnUnavailableException extends Exception {

	/**
     * 
     */
	private static final long serialVersionUID = 7171590345342903477L;

	public ConnUnavailableException() {
	}

	public ConnUnavailableException(String message) {
		super(message);
	}

	public ConnUnavailableException(String message, Throwable cause) {
		super(message, cause);
	}
}
