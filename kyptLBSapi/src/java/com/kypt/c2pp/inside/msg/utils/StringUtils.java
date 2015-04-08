package com.kypt.c2pp.inside.msg.utils;

import java.text.MessageFormat;

/**
 * <p>
 * Title: Scalper Program
 * </p>
 * <p>
 * Description:
 * </p>
 * <p>
 * Copyright: Copyright (c) 2004
 * </p>
 * <p>
 * Company: Neusoft MID
 * </p>
 * 
 * @author Huang Wen Jun
 * @version 5.0
 */

public class StringUtils {
	public static String addPrefix(String sSource, String sAdd, int iDigital) {
		String sTemp = sSource.trim();
		for (int i = 1; i <= iDigital - sTemp.trim().length(); i++) {
			sSource = sAdd + sSource;
		}
		return sSource;
	}

	public static String addPostposition(String sSource, String sAdd,
			int iDigital) {
		String sTemp = sSource.trim();
		for (int i = 1; i <= iDigital - sTemp.trim().length(); i++) {
			sSource = sSource + sAdd;
		}
		return sSource;
	}

	public static String replaceString(String sMessage, Object[] variables) {
		MessageFormat formatter = new MessageFormat(sMessage);
		String sReturn = formatter.format(variables);
		return sReturn;
	}

	public static String getInteger(String sDouble) {
		int iPostion = sDouble.indexOf(".");
		return sDouble.substring(0, iPostion);
	}

	@SuppressWarnings("static-access")
	public static String getFormatKey(long lNumber) {
		Long objLong = new Long(lNumber);
		String sNumber = objLong.toHexString(lNumber);
		@SuppressWarnings("unused")
		int j;
		String sTemp = "";

		for (int i = 0; i < 8 - sNumber.length(); i++) {
			sTemp = sTemp + "0";
		}

		sTemp = sTemp.trim() + sNumber;
		sTemp = sTemp.toUpperCase();
		sNumber = sTemp.substring(0, 4) + "-" + sTemp.substring(4, 8);
		return sNumber;
	}

	@SuppressWarnings("static-access")
	public static String getFormatString(String sSource) {
		long lSum = 0;

		for (int i = 0; i < sSource.length(); i++) {
			lSum += (byte) (sSource.charAt(i));
		}

		Long objLong = new Long(lSum);
		String sNumber = objLong.toHexString(lSum);

		String sTemp = "";

		for (int i = 0; i < 4 - sNumber.length(); i++) {
			sTemp = sTemp + "0";
		}
		sTemp = sTemp.trim() + sNumber;
		sTemp = sTemp.toUpperCase();

		return sTemp;
	}
}
