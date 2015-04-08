package com.ctfo.common.util;

import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-5-26</td>
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
@SuppressWarnings("all")
public class JsonUtil {

	public static String getJsonforGrid(String total, List<Object> list) throws Exception {
		if ((list != null) && (list.size() > 0)) {
			StringBuffer sb = new StringBuffer();
			sb.append("{\"Rows\":[");

			sb.append(list2Json(list));
			sb.append("],\"Total\":\"" + total + "\"}");

			return sb.toString();
		}

		return "{\"Rows\":[],\"Total\":\"0\"}";
	}

	private static String list2Json(List<Object> list) throws Exception {
		StringBuffer sb = new StringBuffer("");
		if ((list != null) && (list.size() > 0)) {
			for (Iterator localIterator = list.iterator(); localIterator.hasNext();) {
				Object o = localIterator.next();
				if (o != null) {
					sb.append(obj2Json(o));
				}
			}
			sb.delete(sb.length() - 1, sb.length());
		}
		return sb.toString();
	}

	private static String obj2Json(Object o) throws Exception {
		Map map = getObjectMap(o);

		StringBuffer sb = new StringBuffer("{");
		Iterator it = map.keySet().iterator();

		while (it.hasNext()) {
			Object key = it.next();

			if (map.get(key) instanceof String) {
				sb.append("\"" + key + "\":");
				String strTmp = (map.get(key) != null) ? jsonCharFormat(String.valueOf(map.get(key))) : "";
				sb.append("\"" + strTmp + "\",");
			} else if (map.get(key) instanceof Character) {
				sb.append("\"" + key + "\":");
				sb.append("\"" + map.get(key) + "\",");
			} else if (map.get(key) instanceof Date) {
				try {
					if ((map.get(key) == null) || ("".equals(map.get(key)))) {
						sb.append("\"" + key + "\":");
						sb.append(",");
					} else {
						sb.append("\"" + key + "\":");
						sb.append("\"" + DateUtil.format((Date) map.get(key), "yyyy-MM-dd HH:mm") + "\",");
					}

				} catch (Exception e) {
					sb.append("\"" + key + "\":");
					sb.append("\"\",");
				}
			} else if (map.get(key) instanceof List) {
				sb.append("\"" + key + "\":");

				sb.append("[");
				sb.append(list2Json((List) map.get(key)));
				sb.append("],");
			} else if (map.get(key) instanceof Object[]) {
				sb.append("\"" + key + "\":");

				sb.append("[");
				sb.append(array2Json((Object[]) map.get(key)));
				sb.append("],");
			} else if (map.get(key) instanceof Map) {
				sb.append("\"" + key + "\":");
				sb.append("{");
				sb.append(map2Json((Map) map.get(key)));
				sb.append("},");
			} else if (map.get(key) == null) {
				sb.append("\"" + key + "\":");
				sb.append("\"\",");
			} else {
				sb.append("\"" + key + "\":");
				sb.append("\"" + map.get(key) + "\",");
			}

		}

		if (sb.length() > 0) {
			sb.delete(sb.length() - 1, sb.length());
		}

		sb.append("},");

		return sb.toString();
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

	private static String array2Json(Object[] objArray) throws Exception {
		Object[] arrayOfObject;
		StringBuffer sb = new StringBuffer("");
		int j = (arrayOfObject = objArray).length;
		for (int i = 0; i < j; ++i) {
			Object o = arrayOfObject[i];
			if (o != null)
				sb.append(obj2Json(o));
		}

		if (sb.length() > 0)
			sb.delete(sb.length() - 1, sb.length());

		return sb.toString();
	}

	private static String map2Json(Map map) throws Exception {
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

	private static Map<String, Object> getObjectMap(Object o) throws Exception {
		Map map = new HashMap();

		Class c = o.getClass();
		setValue(map, o, c);

		if (c.getSuperclass() != null) {
			setValue(map, o, c.getSuperclass());
		}

		return map;
	}

	private static void setValue(Map map, Object o, Class<? extends Object> ownerClass) throws Exception {
		Object value = null;
		Field[] f = ownerClass.getDeclaredFields();
		for (int i = 0; i < f.length; ++i) {
			if ((f[i].getName() != null) && (!(f[i].getName().equals("serialVersionUID")))) {
				if ((f[i].getAnnotations() != null) && (f[i].getAnnotations().length > 0)) {
					Annotation[] arrayOfAnnotation;
					int j = (arrayOfAnnotation = f[i].getAnnotations()).length;
					for (int k = 0; k < j; ++k) {
						Annotation ann = arrayOfAnnotation[k];

						/*
						 * if (ann instanceof AnnotationJson) { if (!(((AnnotationJson)ann).ex())) { value = getMethodValue(o, f[i].getName(), ownerClass);
						 * 
						 * map.put(f[i].getName(), value); } } else {
						 */
						value = getMethodValue(o, f[i].getName(), ownerClass);

						map.put(f[i].getName(), value);
						// }
					}
				} else {
					value = getMethodValue(o, f[i].getName(), ownerClass);

					map.put(f[i].getName(), value);
				}
			}
		}
	}

	private static Object getMethodValue(Object owner, String methodName, Class<? extends Object> ownerClass) throws Exception {
		methodName = methodName.substring(0, 1).toUpperCase() + methodName.substring(1);
		Method method = null;
		try {
			method = ownerClass.getMethod("get" + methodName, new Class[0]);
		} catch (SecurityException localSecurityException) {
		} catch (NoSuchMethodException e) {
			return "";
		}
		return method.invoke(owner, new Object[0]);
	}

}
