package com.ctfo.util;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

public class RedisJsonUtil {

	/**
	 * Object转Json
	 * 
	 * @param object
	 *            Object
	 * @return
	 */
	public static String objectToJson(Object object) {
		String value = null;
		if (null != object) {
			Gson gson = new Gson();
			value = gson.toJson(object);
		}
		return value;
	}

	/**
	 * Json转Object
	 * 
	 * @param json
	 *            Json
	 * @param typeToken
	 *            数据类型转换器(new TypeToken<数据类型>(){})
	 * @return
	 */
	public static Object jsonToObject(String json, TypeToken<?> typeToken) {
		Object value = null;
		if (null != json && !"".equals(json) && null != typeToken) {
			Gson gson = new Gson();
			value = gson.fromJson(json, typeToken.getType());
		}
		return value;
	}
}
