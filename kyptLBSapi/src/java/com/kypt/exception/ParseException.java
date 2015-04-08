/*******************************************************************************
 * @(#)ParseException.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.exception;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午05:17:13
 */
public class ParseException extends Exception {

    /**
     * 
     */
    private static final long serialVersionUID = 4872329258589698333L;

    public ParseException() {
    }

    public ParseException(String message) {
        super(message);
    }

    public ParseException(String message, Throwable cause) {
        super(message, cause);
    }
}
