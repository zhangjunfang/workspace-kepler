package com.ctfo.util;

import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public class RemoteUtil {
	public static String map2Json(Map map) throws Exception {
		if (map != null) {
			StringBuffer sb = new StringBuffer("");
			Set keyIt = map.keySet();
			for (Iterator it = keyIt.iterator(); it.hasNext();) {
				Object key = it.next();
				sb.append("\"" + key + "\":");
				String strTmp = (map.get(key) != null) ? jsonCharFormat(String.valueOf(map.get(key))) : "";
				sb.append("\"" + strTmp + "\",");
			}

			sb.delete(sb.length() - 1, sb.length());
			return sb.toString();
		}
		return null;
	}

	public static String jsonCharFormat(String results) {
		char[] arrayOfChar1;
		StringBuilder sb = new StringBuilder();
		char[] chars = results.toCharArray();
		int j = (arrayOfChar1 = chars).length;
		for (int i = 0; i < j; ++i) {
			char c = arrayOfChar1[i];
			if (c == '\n') {
				sb.append("\\n");
			} else if (c == '\r') {
				sb.append("\\r");
			} else if (c == '"') {
				sb.append("\\\"");
			} else if (c == '\\') {
				sb.append("\\\\");
			} else if (c == '/') {
				sb.append("\\/");
			} else if (c == '\b') {
				sb.append("\\b");
			} else if (c == '\f') {
				sb.append("\\f");
			} else if (c == '\t') {
				sb.append("\\t");
			} else if (Character.isISOControl(c)) {
				char[] hex = "0123456789ABCDEF".toCharArray();
				sb.append("\\u");
				int n = c;
				for (int k = 0; k < 4; ++k) {
					int digit = (n & 0xF000) >> 12;
					sb.append(hex[digit]);
					n <<= 4;
				}
			} else {
				sb.append(c);
			}
		}
		return sb.toString();
	}
}
