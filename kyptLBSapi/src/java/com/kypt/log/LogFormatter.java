package com.kypt.log;

/*******************************************************************************
 * @(#)LogFormatter.java 2010-3-4
 *
 * Copyright 2010 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/

/**
 * @author <a href="mailto:zhangmq@neusoft.com">zhangmeiqiu </a>
 * @version $Revision 1.1 $ 2010-3-4 下午07:50:06
 */
public class LogFormatter {

	public static String formatMsg(String module, String information) {
		return "<" + module + ">" + information;
	}

	public static String formatMsg(String module, String key, String info) {
		return "<" + module + ">" + "<" + key + ">" + info;
	}

}
