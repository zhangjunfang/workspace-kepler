package com.ctfo.savecenter.util;

import java.io.InputStream;
import java.util.Date;
import java.util.Properties;

/**
 * <p>
 * Title: IOAgent
 * </p>
 * 
 * <p>
 * Description: ������
 * </p>
 * 
 * <p>
 * Copyright: Copyright (c) 2004
 * </p>
 * 
 * <p>
 * Company:
 * </p>
 * 
 * @author �쿡��
 * @version 1.0 ���� ϵͳ���õĺ���
 */
@SuppressWarnings("deprecation")
public class Tools {
	public Tools() {
	}

	private static String formattime(int uf) {
		if (uf > 9)
			return ("" + uf);
		else
			return ("0" + uf);
	}

	/**
	 * hexToString ��16������ʾ���ַ� ��: 0x35 ���"35"
	 * 
	 * @param b
	 *            byte
	 * @return byte
	 */
	public static String hexToString(byte b) {
		return Integer.toHexString((b & 0xf0) >> 4) + Integer.toHexString(b & 0x0f);
	}

	public static String hexToString(byte[] b) {
		String s = "";
		if (b == null)
			return "�����";
		for (int i = 0; i < b.length; i++)
			s = s + hexToString(b[i]);
		return s;
	}

	public static String hexToString(byte[] b, int start, int count) {
		String s = "";
		for (int i = start; i < start + count; i++)
			s = s + hexToString(b[i]);
		return s;
	}

	public static byte stringToHex(String s) {
		byte b0 = Byte.parseByte(s.substring(0, 1));
		byte b1 = Byte.parseByte(s.substring(1, 2));
		return (byte) ((b0 << 4) + b1);
	}

	public static byte stringHexToHex(String s) {
		byte b0 = Byte.parseByte(s.substring(0, 1), 16);
		byte b1 = Byte.parseByte(s.substring(1, 2), 16);
		return (byte) ((b0 << 4) + b1);
	}

	public static byte[] stringHexToBytes(String s) {
		byte[] t = new byte[s.length() / 2];
		for (int i = 0; i < s.length() / 2; i++) {
			t[i] = stringHexToHex(s.substring(i * 2, i * 2 + 2));
		}
		return t;
	}

	public static byte[] stringToBytes(String s, byte[] data, int start, int length) {
		byte[] content = s.getBytes();
		if (length < content.length)
			return null;
		for (int i = 0; i < content.length; i++) {
			data[start + i] = content[i];
		}
		return data;
	}

	public static byte[] stringToBytes(String s) {
		byte[] content = s.getBytes();
		byte[] data = new byte[content.length];
		for (int i = 0; i < content.length; i++) {
			data[i] = content[i];
		}
		return data;
	}

	public static String hexStringToString(String s) {
		// String s1;
		byte[] byteStr = new byte[s.length() / 2];
		for (int i = 0; i < s.length() / 2; i++) {
			byteStr[i] = stringHexToHex(s.substring(i * 2, i * 2 + 2));
		}
		String str1 = null;
		try {
			str1 = new String(byteStr, "GBK");
			str1 = str1.trim();
		} catch (Exception e) {
		}
		return str1;
	}

	/**
	 * byte2int byte����ת��Ϊ����
	 * 
	 * @param b
	 *            byte[]
	 * @return int
	 */
	public static long byte2int(byte b[]) {
		return Tools.hexToDecimal(b[0]) * 256 * 256 * 256 + Tools.hexToDecimal(b[1]) * 256 * 256 + Tools.hexToDecimal(b[2]) * 256 + Tools.hexToDecimal(b[3]);
	}

	public static int byte2short(byte b[]) {
		return b[1] & 0xff | (b[0] & 0xff) << 8;
	}

	/**
	 * hexToDecimal 16����ת��Ϊ10���� �ر���ڴ���0x80�ģ���Ҫ����
	 * 
	 * @param b
	 *            byte
	 * @return int
	 */
	public static int hexToDecimal(byte b) {
		if (b < 0)
			return 256 + b;
		else
			return b;
	}

	/**
	 * getDateString ��ʽ������ yyyy-mm-dd
	 * 
	 * @param time
	 *            long
	 * @return String
	 */

	public static String getDateString(long time) {
		StringBuffer s = new StringBuffer(19);
		s.setLength(0);
		Date d = new Date(time);
		int temp = d.getYear() + 1900;
		s.append(formattime(temp)).append('-');
		s.append(formattime(d.getMonth() + 1)).append('-');
		s.append(formattime(d.getDate()));
		s.setLength(10);
		return s.toString();
	}

	/**
	 * getTimeString ����ת���� Сʱ �� �� ��2410�� ת������ 4Сʱ10�� 2470 ת������
	 * 4Сʱ1��10��
	 * 
	 * @param second
	 *            int
	 * @return String
	 */
	public static String getTimeString(int second) {
		int other = second;
		String hh = "", mm = "", ss = "";
		if ((second / 3600) != 0) {
			hh = String.valueOf((int) (second / 3600)) + "Сʱ";
			other = second - ((int) (second / 3600)) * 3600;
		}
		if ((other / 60) != 0) {
			mm = String.valueOf((int) (other / 60)) + "��";
			other = other - ((int) (other / 60)) * 60;
		}
		if (other != 0) {
			ss = String.valueOf(other) + "��";
		}
		return hh + mm + ss;
	}

	/**
	 * getDateTimeString ��ʽ��ʱ�� yyyy-mm-dd hh:nn:ss
	 * 
	 * @param time
	 *            long
	 * @return String
	 */
	public static String getDateTimeString(long time) {
		StringBuffer s = new StringBuffer(19);
		s.setLength(0);
		Date d = new Date(time);
		int temp = d.getYear() + 1900;
		s.append(formattime(temp)).append('-');
		s.append(formattime(d.getMonth() + 1)).append('-');
		s.append(formattime(d.getDate())).append(' ');
		s.append(formattime(d.getHours())).append(':');
		s.append(formattime(d.getMinutes())).append(':');
		s.append(formattime(d.getSeconds()));
		s.setLength(19);
		return s.toString();
	}

	/**
	 * intToHex ��10����ת��Ϊ16�������ͣ��ر���Դ���0x80��Ҫ����
	 * 
	 * @param i
	 *            int
	 * @return byte
	 */
	public static byte intToHex(int i) {
		if (i > 0x80)
			return (byte) (i - 256);
		else
			return (byte) i;
	}

	/**
	 * splitStat ״̬�÷ָ����ʾ
	 * 
	 * @param total
	 *            String ԭ��ʾ�ַ�
	 * @param s
	 *            String ״̬��ʾ�ַ�
	 * @return String
	 */
	static public String splitStat(String total, String s) {
		if (s.length() > 0) {
			if (total.length() > 0)
				return total + "|" + s;
			else
				return s;
		}
		return total;
	}

	static public String delBlank(String s) {
		String[] tempString = s.split(" ");
		String result = "";
		for (int i = 0; i < tempString.length; i++) {
			result = result + tempString[i];
		}
		return result;
	}

	static public String delContralChar(String s) {
		// String[] tempString=s.split();
		byte[] buffer = s.getBytes();
		byte[] temp = new byte[1];
		String result = "";
		for (int i = 0; i < buffer.length; i++) {
			if (buffer[i] >= 0x30) {
				temp[0] = buffer[i];
				String tempS = new String(temp);
				result = result + tempS;
			}
		}
		return result;
	}

	static public Properties getFile() {
		Tools tools = new Tools();
		String propfile = "Conn.properties";
		InputStream is = tools.getClass().getResourceAsStream("/" + propfile);
		Properties Props = new Properties();
		try {
			Props.load(is);
		} catch (Exception e) {
			System.err.println("Can not read the properties file. " + "Make sure " + propfile + " is in the CLASSPATH");
			return null;
		}
		return Props;
	}

	static public String getFloatString(String s) {
		String result = "";
		for (int i = 0; i < s.length(); i++) {
			char c = s.charAt(i);
			if (((c >= '0') && (c <= '9')) || (c == '.')) {
				result = result + c;
			}
		}
		return result;
	}

	static public String getIntegerString(String s) {
		String result = "";
		for (int i = 0; i < s.length(); i++) {
			char c = s.charAt(i);
			if (((c >= '0') && (c <= '9'))) {
				result = result + c;
			}
		}
		return result;
	}

	public static String dec2Hex(int dec) {
		StringBuffer sb = new StringBuffer();
		// sb.append("0x");

		for (int i = 0; i < 8; i++) {
			int tmp = (dec >> (7 - i % 8) * 4) & 0x0f;

			if (tmp < 10) {
				sb.append(tmp);
			} else {
				sb.append((char) ('A' + (tmp - 10)));
			}

		}
		String sbString = sb.toString();
		for (int i = 0; i < sbString.length(); i++) {
			if (!sbString.substring(i, i + 1).equals("0")) {
				sbString = sbString.substring(i);
				break;
			}

		}
		return sbString;
	}

	/**
	 * 获取定位状态
	 * 
	 * @param tempStatus
	 *            位置基本信息状态位
	 * @return
	 */
	public static int getPositioning(String tempStatus) {
		if (tempStatus == null) {
			return 0;
		}
		String status = Long.toBinaryString(Long.parseLong(tempStatus));
		if (status.endsWith("11") || status.endsWith("10")) {
			return 1;
		} else {
			return 0;
		}
	}

	/**
	 * 获取ACC状态
	 * 
	 * @param string
	 * @return
	 */
	public static int getACCStatus(String tempStatus) {
		if (tempStatus == null) {
			return 0;
		}
		String status = Long.toBinaryString(Long.parseLong(tempStatus));
		if (status.endsWith("1")) {
			return 1;
		} else {
			return 0;
		}
	}
	// public static void main(String[] args) {
	// String str = "BEA9412D3838383838202020";
	// str = " sfsf sdf sdf 23 4 2 3 43";
	// // System.out.println(delBlank(str));
	// // System.out.println(hexStringToString(str));
	// }

}
