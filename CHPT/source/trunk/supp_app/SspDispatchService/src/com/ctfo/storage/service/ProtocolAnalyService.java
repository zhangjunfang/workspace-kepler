package com.ctfo.storage.service;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.util.Base64_URl;

/**
 * ProtocolAnalyService
 * 
 * 
 * @author huangjincheng 2014-5-19下午05:10:54
 * 
 */
public class ProtocolAnalyService {

	private static final Logger logger = LoggerFactory.getLogger(ProtocolAnalyService.class);

	/**
	 * 
	 * 实现BasicAnaly接口 输入消息体message和需要得到的className，获取赋值后的实体bean
	 * 
	 * @param message
	 *            消息体全部
	 * @param packageName
	 *            bean包名
	 * @param className
	 *            bean类名
	 * @return Object
	 */
	public Object getTableFromControl(String message, String packageName, String className) {
		// 得到对象
		Class<?> c = null;
		try {
			c = Class.forName("com.ctfo.storage.model." + packageName + "." + className);
		} catch (ClassNotFoundException e) {
			logger.error(e.getMessage(), e);
		}
		Object obj;
		try {
			obj = c.newInstance();
		} catch (InstantiationException e) {
			logger.error("转换对象异常：" + e.getMessage());
		} catch (IllegalAccessException e) {
			logger.error("转换对象异常：" + e.getMessage());
		}
		// json字符串赋值对象
		String jsonStr = Base64_URl.base64Decode(message);
		// JSONObject jsonobject = JSONObject.fromObject(jsonStr);
		obj = JSON.parseObject(jsonStr, c);
		return obj;
	}

	/**
	 * 
	 * 实现BasicAnaly接口 输入消息体message和需要得到的className，获取赋值后的实体bean
	 * 
	 * @param message
	 *            消息体全部
	 * @param fileName
	 *            文件保存mongo的name
	 * @param className
	 *            bean类名
	 * @return Object
	 */
	public Object getFileTableFromControl(String message, String fileName, String className) {
		// 得到对象
		Class<?> c = null;
		try {
			c = Class.forName("com.ctfo.storage.model.file." + className);
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		Object obj;
		try {
			obj = c.newInstance();
		} catch (InstantiationException e) {
			logger.error("转换对象异常：" + e.getMessage());
		} catch (IllegalAccessException e) {
			logger.error("转换对象异常：" + e.getMessage());
		}
		// json字符串赋值对象
		String jsonStr = Base64_URl.base64Decode(message);
		// JSONObject jsonobject = JSONObject.fromObject(jsonStr);
		obj = JSON.parseObject(jsonStr, c);
		// 获取到方法对象
		Method setMongoUrl = null;
		try {
			setMongoUrl = c.getMethod("setMongourl", new Class[] { String.class });
			setMongoUrl.invoke(obj, fileName);
		} catch (NoSuchMethodException e) {
			e.printStackTrace();
		} catch (SecurityException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		} catch (IllegalArgumentException e) {
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			e.printStackTrace();
		}
		return obj;
	}
}
