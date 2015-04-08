package com.ctfo.common.util;

import java.text.DecimalFormat;
import java.util.UUID;

public class StringUtil {

	/**
	 * 随机生成32位UUID.
	 * 
	 * @return PID
	 */
	public static String genUUID() {
		return UUID.randomUUID().toString().replace("-", "");
	}

	/**
	 * 判断是否为空
	 * 
	 * @param arg
	 * @return boolean
	 */
	public static boolean isBlank(String arg) {
		return arg == null || "".equals(arg.trim());
	}

	/**
	 * 判断是否为非空
	 * 
	 * @param arg
	 * @return boolean
	 */
	public static boolean isNotBlank(String arg) {
		return !isBlank(arg);
	}

	/**
	 * 判断是否为非空 为空返回空字符串
	 * 
	 * @param str
	 * @return String
	 */
	public static String isBlankReturn(String str) {
		return isBlank(str) ? "" : str;
	}

	/**
	 * 判断是否为整形数字 方法
	 * 
	 * @param arg
	 * @return boolean
	 */
	public static boolean isInteger(String arg) {
		try {
			Integer.parseInt(arg);
		} catch (NumberFormatException e) {
			return false;
		}
		return true;
	}

	/**
	 * 判断是否为浮点数
	 * 
	 * @param arg
	 * @return boolean
	 */
	public static boolean isFloat(String arg) {
		try {
			Float.parseFloat(arg);
		} catch (NumberFormatException e) {
			return false;
		}
		return true;
	}

	/**
	 * 将字符串分割为数组
	 * 
	 * @param str
	 * @param split
	 * @return
	 */
	public static String[] split(String str, char split) {
		if (str == null) {
			return null;
		}
		int i = str.length();
		if (i == 0) {
			return new String[0];
		}
		return str.split("" + split);
	}

	/**
	 * 将字符串数组抓换成以“,”相隔的字符串,并根据isWrap参数确定是否每项都以“‘”包围
	 * 
	 * @param sourceStr
	 * @param isWrap
	 * @return
	 */
	public static String fromArrayToStr(String[] sourceStr, boolean isWrap) {
		String str = "";
		if (sourceStr != null && sourceStr.length > 0) {
			for (int i = 0; i < sourceStr.length - 1; i++) {
				if (isWrap)
					str = str + "'" + sourceStr[i] + "',";
				else
					str = str + sourceStr[i] + ",";
			}
			if (isWrap)
				str = str + "'" + sourceStr[sourceStr.length - 1] + "'";
			else
				str = str + sourceStr[sourceStr.length - 1];
		}
		return str;
	}

	/**
	 * 移除以某一种分隔符组成的字符串中的重复字符
	 * 
	 * @param sourceStr
	 * @param separator
	 * @return
	 */
	public static String removeRepStr(String sourceStr, String separator) {
		String[] sepStrs = sourceStr.split(separator);
		String reStr = ","; // 要返回的字符串
		for (int i = 0; i < sepStrs.length; i++) {
			if (StringUtil.isNotBlank(sepStrs[i]) && reStr.indexOf("," + sepStrs[i] + ",") < 0) {
				reStr = reStr + sepStrs[i] + ",";
			}
		}
		if (StringUtil.isNotBlank(reStr))
			reStr = reStr.substring(0, reStr.length() - 1);
		return reStr;
	}

	/**
	 * 按字节数截取字符串，汉字按2字节
	 * 
	 * @param str
	 * @param len
	 * @return
	 */
	public static String cutString(String str, int len) {
		len = len - 3;
		if (StringUtil.isBlank(str))
			return "";
		if (len + 3 > str.length() * 2)
			return str;
		int cnCount = 0;
		for (int i = 0; i < str.length(); i++) {
			if (str.charAt(i) > Byte.MAX_VALUE)
				cnCount += 2;
			else
				cnCount++;
			if (cnCount >= len) {
				if (str.length() - i <= 2) {
					return str;
				} else {
					return str.substring(0, i) + "...";
				}
			}
		}
		return str;
	}

	/**
	 * 将文件大小格式化成易读的字符串
	 * 
	 * @param fileSize
	 * @return
	 */
	public static String formatFileSize(long fileSize) {
		DecimalFormat formater = new DecimalFormat();
		formater.applyPattern("###.##");
		if (fileSize < 1024) {
			return fileSize + " B";
		} else if (fileSize < 1024 * 1024) {
			return formater.format(fileSize / 1024f) + " KB";
		} else if (fileSize < 1024 * 1024 * 1024) {
			return formater.format(fileSize / (1024 * 1024f)) + " MB";
		} else {
			return formater.format(fileSize / (1024 * 1024 * 1024f)) + " GB";
		}
	}

	/**
	 * 比较两个字符串内容是否相等，设定null=="" = true
	 * 
	 * @param arg1
	 * @param arg2
	 * @return
	 */
	public static boolean equals(String arg1, String arg2) {
		if ("".equals(arg1))
			arg1 = null;
		if ("".equals(arg2))
			arg2 = null;
		return equals((Object) arg1, (Object) arg2);
	}

	/**
	 * 比较两个对象是否相等
	 * 
	 * @param arg1
	 * @param arg2
	 * @return
	 */
	public static boolean equals(Object arg1, Object arg2) {
		if ("".equals(arg1))
			arg1 = null;
		if ("".equals(arg2))
			arg2 = null;

		if (arg1 == null && arg2 == null) {
			return true;
		} else if (arg1 == null || arg2 == null) {
			return false;
		} else {
			return arg1.equals(arg2);
		}
	}

	/**
	 * 将一个字符串数组以,分隔组成一个字符串
	 * 
	 * @param ary
	 * @return
	 */
	public static String join(String[] ary) {
		return join(ary, ",");
	}

	/**
	 * 将一个字符串数组以指定字符分隔组成一个字符串
	 * 
	 * @param ary
	 * @param split
	 * @return
	 */
	public static String join(String[] ary, String split) {
		if (ary == null || ary.length == 0) {
			return "";
		}
		if (split == null) {
			split = ",";
		}
		StringBuffer buff = new StringBuffer();
		for (String str : ary) {
			buff.append(split).append(str);
		}
		return buff.substring(split.length());
	}

	/**
	 * 转换字符串编码
	 * 
	 * @param src
	 * @param fromEncode
	 * @param toEncode
	 * @return
	 */
	public static String transEncode(String src, String fromEncode, String toEncode) {
		String des = null;
		if (src == null)
			return "";
		try {
			byte[] temp = src.getBytes(fromEncode);
			des = new String(temp, toEncode);
		} catch (Exception e) {
			// LogUtil.logInfo("-----------Error trans from "+fromEncode+" to "+toEncode+"!");
		}
		return des == null ? "" : des;
	}

	public static String IsoToGB2312(String src) {
		return transEncode(src, "ISO8859-1", "GB2312");
	}

	public static String IsoToUTF_16(String src) {
		return transEncode(src, "ISO8859-1", "UTF-16");
	}

	public static String GB2312ToIso(String src) {
		return transEncode(src, "GB2312", "ISO8859-1");
	}

	public final static String GBKToISO(String src) {
		return transEncode(src, "GBK", "ISO8859-1");
	}

	public final static String[] GBKToISO(String[] src) {
		if (src.length != 0) {
			String[] tmp = new String[src.length];
			for (int i = 0; i < src.length; i++) {
				tmp[i] = GBKToISO(src[i]);
			}
			return tmp;
		}
		return null;
	}

	/**
	 * 去掉字符串两端的空白，如果参数为null仍返回null
	 * 
	 * @param str
	 * @return
	 */
	public static String trim(String str) {
		if (str == null) {
			return str;
		} else {
			return str.trim();
		}
	}

	/**
	 * 将第一个字符大写
	 * 
	 * @param arg
	 * @return
	 */
	public static String caseStartChar(String arg) {
		if (isBlank(arg)) {
			return arg;
		}
		String strTmp = arg.substring(0, 1);
		strTmp = strTmp.toUpperCase();
		strTmp += arg.substring(1);
		return strTmp;
	}

	/**
	 * 转换html字符
	 * 
	 * @param inputs
	 * @return
	 */
	public static String escapeHTMLTags(String inputs) {
		if (StringUtil.isNotBlank(inputs))
			return inputs.replace("<", "&lt;").replace(">", "&gt;").replace("&", "&amp;");
		else
			return null;
	}

}
