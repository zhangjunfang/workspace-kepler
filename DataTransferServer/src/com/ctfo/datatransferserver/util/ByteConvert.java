package com.ctfo.datatransferserver.util;

/**
 * 字节转换工具类
 * 
 * @author yangyi
 * 
 */
public class ByteConvert {

	public static int int2byteArray(int i, byte[] ba, int pos) {
		if (ba.length < pos + 4)
			return pos;

		ba[pos] = (byte) (i >>> 24 & 0xFF);
		ba[(pos + 1)] = (byte) (i >>> 16 & 0xFF);
		ba[(pos + 2)] = (byte) (i >>> 8 & 0xFF);
		ba[(pos + 3)] = (byte) (i & 0xFF);
		return (pos + 4);
	}

	public static int short2byteArray(short s, byte[] ba, int pos) {
		if (ba.length <= pos + 2)
			return pos;
		ba[pos] = (byte) (s >>> 8 & 0xFF);
		ba[(pos + 1)] = (byte) (s & 0xFF);
		return (pos + 2);
	}

	/**
	 * 把字节数组转换成16进制字符串
	 * 
	 * @param bArray
	 * @return
	 */
	public static String bytesToHexString(byte[] bArray) {
		StringBuffer sb = new StringBuffer(bArray.length);
		String sTemp;
		for (int i = 0; i < bArray.length; i++) {
			sTemp = Integer.toHexString(0xFF & bArray[i]);
			if (sTemp.length() < 2)
				sb.append(0);
			sb.append(sTemp.toUpperCase());
		}
		return sb.toString();
	}
}
