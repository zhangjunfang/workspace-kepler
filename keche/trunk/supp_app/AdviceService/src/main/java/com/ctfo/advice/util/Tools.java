package com.ctfo.advice.util;

import java.io.UnsupportedEncodingException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * Tools 帮助类，只提供基础的数十种操作方式，可以自己实现方法替代。<br>
 * 主要集中在以下几个方面:<br>
 * > 1:基本类型转换。<br>
 * > 2:字符串操作。<br>
 * > 3:16进制数操作。<br>
 * > 4:时间格式标准化操作。<br>
 * > 5:ip地址验证。<br>
 * > 6:成员变量i的自动增长。<br>
 * > 7:汉字的处理。<br>
 * > 8:base64 编码的处理。<br>
 * 
 * @author huangjincheng 2014-5-4下午04:12:04
 * 
 */
public class Tools {
	public static final String Base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
	static Logger log = LoggerFactory.getLogger(Tools.class);
	/** 验证ip */
	private static Pattern pattern = Pattern.compile("\\b((?!\\d\\d\\d)\\d+|1\\d\\d|2[0-4]\\d|25[0-5])\\.((?!\\d\\d\\d)\\d+|1\\d\\d|2[0-4]\\d|25[0-5])\\.((?!\\d\\d\\d)\\d+|1\\d\\d|2[0-4]\\d|25[0-5])\\.((?!\\d\\d\\d)\\d+|1\\d\\d|2[0-4]\\d|25[0-5])\\b");
	private static int i = 0;
	public static String UNICODE = "unicode";
	public static String GBK = "gbk";
	public static String UTF8 = "utf8";
	public static String GB2312 = "gb2312";

	/**
	 * 验证ip合法性
	 * 
	 * @param ip
	 * @return ip合法,返回true,否则,返回false
	 */
	public static boolean checkIP(String ip) {
		Matcher matchter = pattern.matcher(ip);
		return matchter.matches();
	}

	/**
	 * 获得唯一的命令标识符，范围[0到99999999]
	 * 
	 * @return 2011.12.21
	 */
	public synchronized static String getSeqId() {
		if (i < 99999999) {
			i++;
		} else {
			i = 0;
		}
		return fillNBitBefore("" + i, 6, "0");
	}

	/**
	 * char类型转换为为Int类型。
	 * 
	 * @param ch
	 *            char类型
	 * @return int 类型
	 */
	public static int charToInt(byte ch) {
		int val = 0;
		if (ch >= 0x30 && ch <= 0x39) {
			val = ch - 0x30;
		} else if (ch >= 0x41 && ch <= 0x46) {
			val = ch - 0x41 + 10;
		}
		return val;
	}

	/**
	 * 从输入参数开始，至输入参数结束，依次取每个字节和下一个字节的异或值(1字节8位)<br>
	 * 例如：12 33 45 68 90 AB CD EF 返回的结果就是 ：FA
	 * 
	 * @param str
	 *            16进制字符串。
	 * @return str处理之后的校验码值。
	 */
	public static String getCheckCode(String str) {
		String messageCheck = "";
		int l = str.length();
		int pre = Integer.parseInt(str.substring(0, 2), 16);
		String next = "";
		for (int i = 2; i < l; i = i + 2) {
			next = str.substring(i, i + 2);
			pre = pre ^ Integer.parseInt(next, 16);
		}
		messageCheck = Integer.toHexString(pre);
		return (messageCheck.length() == 1) ? "0" + messageCheck : messageCheck;
	}

	/**
	 * 转义算法
	 * 
	 * @param message
	 * @return
	 */
	public static String getTransferContent(String message) {
		String msgContent = "";
		msgContent = message.substring(2, message.length() - 4);
		msgContent = msgContent.toUpperCase().replace("5A", "5A02").replace("5B", "5A01").replace("5E", "5E02").replace("5E", "5E01");
		return msgContent;
	}

	/**
	 * 获得当前当地时间的指定格式:yyyyMMddHHmmss 例如当前时间是:2012年4月23日 23时34分23秒
	 * 返回是：20120423233423
	 * 
	 * @return 返回指定格式的时间字符串，否则返回 00000000000000 ；
	 */
	public static String getNowTime() {
		return getNowTimeByFomat("yyyyMMddHHmmss");
	}

	/**
	 * 获得当前当地时间的指定格式. 例如当前时间是:2012年4月23日 23时34分23秒 返回是：20120423233423
	 * 
	 * @param timePattern
	 *            指定返回字符串的格式，例如：yyyyMMddHHmmss
	 * @return 返回指定格式的时间字符串，否则返回 00000000000000 ；
	 */
	public static String getNowTimeByFomat(String timePattern) {
		String nowTime = "00000000000000";
		SimpleDateFormat sdf = new SimpleDateFormat(timePattern);
		nowTime = sdf.format(new Date());
		return nowTime;
	}

	/**
	 * 以指定格式(timePattern),获得指定时间(date)<br>
	 * 
	 * @param date
	 *            需要转换的时间
	 * @param timePattern
	 *            指定的格式.
	 * @return 返回指定格式的时间字符串;
	 */
	public static String getTimeByFormat(Date date, String timePattern) {
		String nowTime = "00000000000000";
		SimpleDateFormat sdf = new SimpleDateFormat(timePattern);
		nowTime = sdf.format(date);
		return nowTime;
	}

	/**
	 * 获取十六进制的二进制表示
	 * 
	 * @param hexStr
	 *            16进制字符串
	 * @return 二进制字符串
	 */
	public static String getBinaryStrByHexStr(String hexStr) {
		int l = hexStr.length();
		int b = Integer.parseInt(hexStr, 16);
		String binaryc = "";
		binaryc = Integer.toBinaryString(b);
		if (binaryc.length() != l * 4) {
			binaryc = fillNBitBefore(binaryc, l * 4, "0");
		}
		return binaryc;
	}

	/**
	 * 字符串前面补足规定长度的字符
	 * 
	 * @param s
	 *            需要补足位数的源字符串
	 * @param length
	 *            补足长度
	 * @param fillchar
	 *            填充的字符
	 * @return 指定长度补足指定字符后的字符串
	 */
	public static String fillNBitBefore(String s, int length, String fillchar) {
		for (int i = 0; i < length; i++) {
			if (s.length() == length) {
				break;
			}
			s = fillchar + s;
		}
		return s;
	}

	/**
	 * 字符串后面补足规定长度的字符
	 * 
	 * @param s
	 *            需要补足位数的源字符串
	 * @param length
	 *            补足长度
	 * @param fillchar
	 *            填充的字符
	 * @return 指定长度补足指定字符后的字符串
	 */
	public static String fillNBitAfter(String s, int length, String fillchar) {
		for (int i = 0; i < length; i++) {
			if (s.length() == length) {
				break;
			}
			s = s + fillchar;
		}
		return s;
	}

	/**
	 * 将汉字以GBK编码方式转换为16进制字符串。
	 * 
	 * @param hanzi
	 *            待转换的汉字字符串
	 * @return 返回改汉字字符串的16进制字符串
	 */
	public static String getHzHexStr(String hanzi) {
		return getHzHexStr(hanzi, GBK);
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
		String hs = "";
		String stmp = "";
		for (int n = 0; n < b.length; n++) {
			stmp = (Integer.toHexString(b[n] & 0xFF));
			hs = stmp.length() == 1 ? hs + "0" + stmp : hs + stmp;
		}
		return hs.toUpperCase();
	}

	/**
	 * 将16进制数转换为汉字，以GBK编码方式
	 * 
	 * @param hexStr
	 *            需要转换的16进制数
	 * @return 汉字。
	 */
	public static String getChinese(String hexStr) {
		return getChinese(hexStr, GBK);
	}

	/**
	 * 以指定的编码方式,将16进制数转换为汉字。
	 * 
	 * @param hexStr
	 * @param codeType
	 * @return 汉字
	 */
	public static String getChinese(String hexStr, String codeType) {
		String b = "";
		try {
			b = new String(hexStrToBytes(hexStr.trim()), codeType);
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return b;
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

	/**
	 * 2进制字符串转转16进制字符串
	 * 
	 * @param binaryStr
	 *            二进制字符串如00010001
	 * @return 返回的16进制数 。
	 */
	public static String getHexByBinary(String binaryStr) {
		String hexStr = "";
		hexStr = Integer.toHexString(Integer.valueOf(binaryStr, 2));
		return hexStr;
	}

	private static byte uniteBytes(String src0, String src1) {
		byte b0 = Byte.decode("0x" + src0).byteValue();
		b0 = (byte) (b0 << 4);
		byte b1 = Byte.decode("0x" + src1).byteValue();
		byte ret = (byte) (b0 | b1);
		return ret;
	}

	/**
	 * 将ASCII码字符串转换为16进制数字符串
	 * 
	 * @param asciiStr
	 *            ASCII码值 字符串
	 * @return 16进制字符串
	 */
	public static String asciiToHex(String asciiStr) {
		StringBuffer sb = new StringBuffer();
		char[] b = asciiStr.toCharArray();
		for (char c : b) {
			sb.append(Integer.toHexString((int) c));
		}
		return sb.toString();
	}

	/**
	 * 由ASCII的16进制数获得ASCII值 例如：00 31 32 获得 null 1 2
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

	/***
	 * 获得校验和,即获得输入的16进制字符串,每个字节相加得出的结果.
	 * 
	 * @param checkSum
	 *            需要校验和的16进制字符串
	 * @return 校验和,若有异常,则返回"00";
	 */
	public static String getCheckSum(String checkSum) {
		int l = checkSum.length();
		int pre = Integer.decode("0x" + checkSum.substring(0, 2));
		String next = "00";
		for (int i = 2; i < l; i = i + 2) {
			next = checkSum.substring(i, i + 2);
			pre = pre + Integer.decode("0x" + next);
		}
		next = Integer.toHexString(pre);
		return next;
	}

	/**
	 * 获得UTC时间字符串
	 * 
	 * @return 返回LINUX时间到此时此刻的毫秒数.
	 */
	public static long getUTC() {
		return System.currentTimeMillis();
	}

	/***
	 * 小端模式转大端模式 即低高字节模式转换为 高低字节模式
	 * 
	 * @param littleEnditon
	 *            模式的16进制字符串
	 * @return bigEndition 模式的16进制字符串
	 */
	public static String getBigEndtion(String littleEnditon) {
		StringBuffer sb = new StringBuffer();
		int l = littleEnditon.length();
		if (l % 2 == 0) {
			for (int i = l; i > 0; i = i - 2) {
				sb.append(littleEnditon.substring(i - 2, i));
			}
		}

		return sb.toString();
	}

	/**
	 * 根据 chars[] 数组获得 16进制字符串
	 * 
	 * @param chars
	 *            char数组
	 * @return 16进制字符串
	 */
	public static String char2HexString(char[] chars) {
		StringBuffer hex_string = new StringBuffer();
		int l = chars.length;
		for (int i = 0; i < l; i++) {
			hex_string.append(Tools.fillNBitBefore(Integer.toHexString(0xFF & chars[i]), 2, "0"));
		}

		return hex_string.toString();
	}

	/**
	 * 根据16进制字符串获得chars 数组
	 * 
	 * @param hexString
	 *            16进制字符串
	 * @return char数组
	 */
	public static char[] hex2chars(String hexString) {
		int l = hexString.length();
		char[] c_temp = new char[l / 2];
		for (int i = 0; i < l; i = i + 2) {
			c_temp[i / 2] = (char) Integer.parseInt(hexString.substring(i, i + 2), 16);

		}
		return c_temp;
	}

	public static String getValiCode2(String s) {
		String res = "";
		byte[] bs = hexStrToBytes(s);
		int sum = 0;
		for (byte b : bs) {
			sum += b;
		}
		res = Integer.toHexString(sum);
		for (int i = res.length(); i < 4; i++) {
			res = "0" + res;
		}
		return res;
	}

	/**
	 * Byte数组 转 Long
	 * 
	 * @param array
	 *            byte数组
	 * @return 64位长整型
	 */
	public static long bytesToUint(byte[] array) {
		return ((long) (array[3] & 0xff)) | ((long) (array[2] & 0xff)) << 8 | ((long) (array[1] & 0xff)) << 16 | ((long) (array[0] & 0xff)) << 24;
	}

	/**
	 * 将指定输入二进制字符串，转换为十进制字符串，并保留高自己为0的值。 例如 0000 00001 => 01
	 * 
	 * @param binaryString
	 * @return 十进制字符串
	 */
	public static String binary2SpecifiedLengthInt(String binaryString) {
		StringBuffer sb = new StringBuffer();
		int l = binaryString.length();
		String temp = "";
		if (l % 4 == 0) {
			for (int i = 0; i < l; i = i + 4) {
				temp = binaryString.substring(i, i + 4);
				sb.append(Integer.toHexString(Integer.parseInt(temp, 2)));
			}
		}
		return sb.toString();
	}

	/**
	 * 统计字符串中出现的指定字符的数量
	 * 
	 * @param str
	 *            指定字符串
	 * @param a
	 *            指定的字符
	 * @return 出现的次数。
	 */
	public static int count(String str, char a) {
		int c = 0;
		for (int i = 0; i < str.length(); i++) {
			if (a == str.charAt(i)) {
				c++;
			}

		}
		return c;
	}

	/**
	 * 把BCD码数组转为String
	 * 
	 * @param bytes
	 * @return String
	 */

	public static String bcd2Str(byte[] bytes) {
		StringBuffer temp = new StringBuffer(bytes.length * 2);

		for (int i = 0; i < bytes.length; i++) {
			temp.append((byte) ((bytes[i] & 0xf0) >>> 4));
			temp.append((byte) (bytes[i] & 0x0f));
		}
		// return
		// temp.toString().substring(0,1).equalsIgnoreCase("0")?temp.toString().substring(1):temp.toString();
		return temp.toString();
	}

	/**
	 * 把String转为BCD码
	 * 
	 * @param asc
	 * @return BCD码
	 */
	public static byte[] str2Bcd(String asc) {
		int len = asc.length();
		int mod = len % 2;

		if (mod != 0) {
			asc = "0" + asc;
			len = asc.length();
		}

		byte abt[] = new byte[len];
		if (len >= 2) {
			len = len / 2;
		}

		byte bbt[] = new byte[len];
		abt = asc.getBytes();
		int j, k;

		for (int p = 0; p < asc.length() / 2; p++) {
			if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9')) {
				j = abt[2 * p] - '0';
			} else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z')) {
				j = abt[2 * p] - 'a' + 0x0a;
			} else {
				j = abt[2 * p] - 'A' + 0x0a;
			}

			if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9')) {
				k = abt[2 * p + 1] - '0';
			} else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z')) {
				k = abt[2 * p + 1] - 'a' + 0x0a;
			} else {
				k = abt[2 * p + 1] - 'A' + 0x0a;
			}

			int a = (j << 4) + k;
			byte b = (byte) a;
			bbt[p] = b;
		}
		return bbt;
	}

	/**
	 * 获得当前时间的毫秒数,用作版本判断。
	 * 
	 * @deprecated
	 * @param time
	 *            符合格式：yyyyMMddHHmmss 的版本信息。
	 * @return 返回与UTC时间相隔的毫秒数。
	 */
	public static long getTimeLong(String time, String dataPattern) {
		long long_time = 0l;
		SimpleDateFormat out_time = new SimpleDateFormat(dataPattern);
		Date d = null;
		try {
			d = out_time.parse(time);
		} catch (ParseException e) {
			log.debug("版本信息格式不争取，格式：" + dataPattern + "，收到的版本：" + time);
			// new Exception("版本信息格式不争取，格式：yyyyMMddHHmmss，收到的版本："+time);
		}
		if (d != null) {
			long_time = d.getTime();
		}

		return long_time;

	}

	/**
	 * 判断协议格式是否正确
	 * 
	 * @param message
	 *            消息体
	 * @return
	 */
	public static boolean isRightDeviceCode(String message) {
		if (!message.startsWith("5B") || !message.endsWith("5D")) {
			log.debug("校验错误，非5B开头5D结尾！");
			return false;
		} else {
			String msgContent = getTransferContent(message);
			int sign_length = Integer.parseInt(msgContent.substring(32, 36), 16);
			int data_length = (msgContent.length() - 36) / 2;
			log.debug("sign_length" + sign_length);
			log.debug("data_length" + data_length);
			String msgCode = message.substring(message.length() - 4, message.length() - 2);
			if (sign_length != data_length) {
				log.debug("校验错误，数据长度不一致！");
				return false;
			} else if (!msgCode.equals(Tools.getCheckCode(msgContent))) {
				log.debug("校验错误，校验码错误！");
				return false;
			} else
				return true;
		}
	}

	/**
	 * 
	 * 获取主消息类型ID
	 * 
	 * @param message
	 *            消息体
	 * @return
	 */
	public static String getMasterType(String message) {
		String msgContent = getTransferContent(message);// 消息头+消息体
		return msgContent.substring(16, 20);
	}

	/**
	 * 
	 * 获取子消息类型ID
	 * 
	 * @param message
	 *            消息体
	 * @return
	 */
	public static String getSlaveType(String message) {
		String msgContent = getTransferContent(message);// 消息头+消息体
		return msgContent.substring(36, 40);
	}

	public static byte[] base64DecodeToArray(final String s) {
		if (s == null)
			return null;

		int len = s.length();
		if (len == 0)
			return new byte[0];
		if (len % 4 != 0) {
			throw new java.lang.IllegalArgumentException(s);
		}

		byte[] b = new byte[(len / 4) * 3];
		int i = 0, j = 0, e = 0, c, tmp;
		while (i < len) {
			c = Base64Chars.indexOf((int) s.charAt(i++));
			tmp = c << 18;
			c = Base64Chars.indexOf((int) s.charAt(i++));
			tmp |= c << 12;
			c = Base64Chars.indexOf((int) s.charAt(i++));
			if (c < 64) {
				tmp |= c << 6;
				c = Base64Chars.indexOf((int) s.charAt(i++));
				if (c < 64) {
					tmp |= c;
				} else {
					e = 1;
				}
			} else {
				e = 2;
				i++;
			}

			b[j + 2] = (byte) (tmp & 0xff);
			tmp >>= 8;
			b[j + 1] = (byte) (tmp & 0xff);
			tmp >>= 8;
			b[j + 0] = (byte) (tmp & 0xff);
			j += 3;
		}

		if (e != 0) {
			len = b.length - e;
			byte[] copy = new byte[len];
			System.arraycopy(b, 0, copy, 0, len);
			return copy;
		}

		return b;
	}

	/**
	 * Encoding a byte array to a string follow the Base64 regular.
	 * 
	 * @param s
	 *            byte array
	 * @return
	 */
	public static String base64EncodeFoArray(final byte[] s) {
		if (s == null)
			return null;
		if (s.length == 0)
			return "";

		StringBuffer buf = new StringBuffer();

		int b0, b1, b2, b3;
		int len = s.length;
		int i = 0;
		while (i < len) {
			byte tmp = s[i++];
			b0 = (tmp & 0xfc) >> 2;
			b1 = (tmp & 0x03) << 4;
			if (i < len) {
				tmp = s[i++];
				b1 |= (tmp & 0xf0) >> 4;
				b2 = (tmp & 0x0f) << 2;
				if (i < len) {
					tmp = s[i++];
					b2 |= (tmp & 0xc0) >> 6;
					b3 = tmp & 0x3f;
				} else {
					b3 = 64; // 1 byte "-" is supplement

				}
			} else {
				b2 = b3 = 64;// 2 bytes "-" are supplement

			}

			buf.append(Base64Chars.charAt(b0));
			buf.append(Base64Chars.charAt(b1));
			buf.append(Base64Chars.charAt(b2));
			buf.append(Base64Chars.charAt(b3));
		}

		return buf.toString();
	}

	/**
	 * string转BASE64
	 * 
	 * @param str
	 * @return
	 */
	public static String strToBase64(final String s) {
		if (s == null || s.length() == 0)
			return s;
		byte[] b = null;
		try {
			b = s.getBytes("GBK");
		} catch (java.io.UnsupportedEncodingException e) {
			e.printStackTrace();
			return s;
		}
		return base64EncodeFoArray(b);
	}

	/**
	 * BASE64转string
	 * 
	 * @param s
	 * @return
	 */
	public static String base64ToStr(String s) {
		byte[] b = base64DecodeToArray(s);
		if (b == null)
			return null;
		if (b.length == 0)
			return "";

		try {
			return new String(b, "GBK");
		} catch (java.io.UnsupportedEncodingException e) {
			e.printStackTrace();
			return null;
		}
	}

	public static void main(String[] args) {
		System.out.println(getCheckCode("31323334353637383930313233343536110012345678003211016875616E6700000000000000000000003132330000000000000000000000000000000000000000000000000000000000785d"));
		System.out.println(Tools.getASCIIByHex("6875616E670000000000000000000000"));
		System.out.println(strToBase64("123123333333333331312312333333333333333333333333333333333333333333333333333333333"));
	}

}
