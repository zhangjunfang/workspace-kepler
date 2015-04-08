package com.ctfo.storage.util;

import java.io.UnsupportedEncodingException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： Tools 帮助类<br>
 * 描述： Tools 帮助类<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-24</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class Tools {
	public static final Logger logger = LoggerFactory.getLogger(Tools.class);
	public static String UNICODE = "unicode";
	public static String GBK = "gbk";
	public static String UTF8 = "utf8";
	public static String GB2312 = "gb2312";

	/**
	 * 判断协议格式是否正确
	 * 
	 * @param message
	 *            消息体
	 * @return
	 */
	public static boolean isRightDeviceCode(String message) {
		if (!message.toUpperCase().startsWith(Constant.LEFT_BRACKET) || !message.toUpperCase().endsWith(Constant.RIGHT_BRACKET)) {
			logger.debug("校验错误，非[开头]结尾！");
			return false;
		} else {
			String checkStr = message.substring(1, message.lastIndexOf(Constant.DOLLAR)); // 要校验的字符串
			String msgCode = message.substring(message.lastIndexOf(Constant.DOLLAR) + 1, message.length() - 1); // 校验码
			if (!msgCode.equals(Tools.getCheckCode(checkStr))) {
				logger.debug("校验错误，校验码错误！");
				return false;
			} else {
				logger.debug("校验成功！");
				return true;
			}
		}
	}

	/**
	 * 从输入参数开始，至输入参数结束，依次取每个字节和下一个字节的异或值(1字节8位)<br>
	 * 例如：12 33 45 68 90 AB CD EF 返回的结果就是 ：FA
	 * 
	 * @param str字符串
	 * @return str处理之后的校验码值。
	 */
	public static String getCheckCode(String str) {
		String messageCheck = Tools.getHzHexStr(str); // 将数据转为16进制字符串
		int l = messageCheck.length();
		int num = 0;
		for (int i = 0; i < l; i = i + 2) {
			num ^= Integer.parseInt(messageCheck.substring(i, i + 2), 16);
		}
		return String.valueOf(num);
	}

	/**
	 * 将汉字以UTF-8编码方式转换为16进制字符串。
	 * 
	 * @param hanzi
	 *            待转换的汉字字符串
	 * @return 返回改汉字字符串的16进制字符串
	 */
	public static String getHzHexStr(String hanzi) {
		return getHzHexStr(hanzi, UTF8);
	}

	/**
	 * 将汉字转换为16进制字符串。编码格式默认为GBK
	 * 
	 * @param hanzi
	 *            待转换的汉字字符串
	 * @param codeType
	 *            编码方式，取值GBK,UNICODE,GB2312,UTF8或其他。
	 * @return 返回改汉字字符串指定编码方式的16进制字符串，异常返回空。
	 */
	public static String getHzHexStr(String hanzi, String codeType) {
		String s = "";
		if (codeType != null) {
			try {
				s = bytesToHexStr(hanzi.getBytes(codeType));
			} catch (UnsupportedEncodingException e) {
				e.printStackTrace();
			}
		}

		return s;
	}

	/**
	 * 字节数组转换为十六进制字符串
	 * 
	 * @param b
	 *            字节数组
	 * @return 十六进制字符串
	 */
	public static String bytesToHexStr(byte[] b) {
		try {
			String hs = "";
			String stmp = "";
			for (int n = 0; n < b.length; n++) {
				stmp = (Integer.toHexString(b[n] & 0xFF));
				hs = stmp.length() == 1 ? hs + "0" + stmp : hs + stmp;
			}
			return hs.toUpperCase();
		} catch (Exception e) {
			logger.error(e.getMessage(), e);
			return null;
		}

	}

	/**
	 * 16进制字符串转byte数组
	 * 
	 * @param src
	 *            16进制字符串，如：12344A8B9C
	 * @return 返回的byte数组。
	 */
	public static byte[] hexStrToBytes(String src) {
		int m = 0;
		int n = 0;
		int l = src.length() / 2;
		byte[] ret = new byte[l];
		for (int i = 0; i < l; i++) {
			m = i * 2 + 1;
			n = m + 1;
			ret[i] = uniteBytes(src.substring(i * 2, m), src.substring(m, n));
		}
		return ret;
	}

	private static byte uniteBytes(String src0, String src1) {
		byte b0 = Byte.decode("0x" + src0).byteValue();
		b0 = (byte) (b0 << 4);
		byte b1 = Byte.decode("0x" + src1).byteValue();
		byte ret = (byte) (b0 | b1);
		return ret;
	}

	/**
	 * 
	 * 获取主消息类型
	 * 
	 * @param message
	 *            消息体
	 * @return
	 */
	public static String getMasterType(String message) {
		String masterType = message.substring(message.indexOf("$", 2), message.indexOf("$", 3));
		System.out.println(message.indexOf("$", 2));
		System.out.println(masterType);
		return masterType;
	}

	/**
	 * 由ASCII的16进制数获得ASCII值 例如：00 31 32 获得 null 1 2
	 * 
	 * @param ascHexStr
	 * @return
	 */
	public static String getASCIIByHex(String ascHexStr) {
		StringBuffer sb = new StringBuffer();
		int length = ascHexStr.length();
		if (length % 2 == 0) { // 整数长度
			for (int i = 0; i < length; i = i + 2) {
				int a = Integer.parseInt(ascHexStr.substring(i, i + 2), 16);
				if (a == 0) { // 过滤掉0
					sb.append("");
				} else {
					sb.append((char) a);
				}
			}
		}
		return sb.toString();
	}

	public static void main(String[] args) {
		System.out.println(Tools.getASCIIByHex("de3d"));
		System.out.println(isRightDeviceCode("[123$A$eyJUYlZlaGljbGUiOnsidmlkIjoiMTIzMiIsInZlaGljbGVObyI6Ir6pQTk4MjgxIn0sIm9wZXJUeXBlIjoiMDEifQ==$45]"));
		getMasterType("[123$num$A$A1$eyJUYlZlaGljbGUiOnsidmlkIjoiMTIzMiIsInZlaGljbGVObyI6Ir6pQTk4MjgxIn0sIm9wZXJUeXBlIjoiMDEifQ==$45]");
	}

}
