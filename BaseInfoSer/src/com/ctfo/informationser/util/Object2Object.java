package com.ctfo.informationser.util;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.math.BigDecimal;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;

import com.thoughtworks.xstream.XStream;
import com.thoughtworks.xstream.io.xml.DomDriver;

/**
 * 
 * 对象和对象相互转换类型。
 * 
 * @version 1.0
 * 
 * @author 张明
 * @since JDK1.6
 */
public class Object2Object {

	private static Log log = LogFactory.getLog(Object2Object.class);

	/**
	 * Map转Object 根据Map赋值Object
	 * 
	 * @param map
	 *            数据Map，key属性名，value属性值
	 * @param object
	 *            目标对象
	 * @return
	 */
	public static Object saveMap2Object(Map<String, Object> map, Object object) {
		Method[] methods = object.getClass().getDeclaredMethods();
		Class<?> classType = object.getClass();
		try {
			for (Entry<String, Object> entry : map.entrySet()) {
				String key = entry.getKey();
				Object value = entry.getValue();
				for (Method method : methods) {
					String methodName = method.getName();
					String getName = null;
					String setName = null;
					String name = null;
					if (methodName.startsWith("get")) {
						name = methodName.substring(3);
						getName = methodName;
						setName = "set" + name;
					} else if (methodName.startsWith("is")) {
						name = methodName.substring(2);
						getName = methodName;
						setName = "set" + name;
					} else if (null == name) {
						continue;
					}
					name = name.substring(0, 1).toLowerCase() + name.substring(1);
					if (key.equals(name)) {
						Method getMethod = classType.getMethod(getName, new Class[] {});
						Method setMethod = classType.getMethod(setName, new Class[] { getMethod.getReturnType() });
						setMethod.invoke(object, new Object[] { value });
					}
				}
			}
		} catch (IllegalArgumentException e) {
			log.error("parseElement IllegalArgumentException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			log.error("parseElement IllegalAccessException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			log.error("parseElement InvocationTargetException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (SecurityException e) {
			log.error("parseElement SecurityException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (NoSuchMethodException e) {
			log.error("parseElement NoSuchMethodException:" + e.getLocalizedMessage());
			e.printStackTrace();
		}
		return object;
	}

	/**
	 * Map转Object 根据Map赋值Object
	 * 
	 * @param map
	 *            数据Map，key属性名，value属性值
	 * @param object
	 *            目标对象
	 * @param equalMap
	 *            修改时的条件也要传入
	 * @return
	 */
	public static Object toObject(Map<String, Object> map, Object object, Map<String, Object> equalMap) {
		Method[] methods = object.getClass().getDeclaredMethods();
		try {
			for (Method method : methods) {
				String methodName = method.getName();
				// update中数据赋值给对象
				for (Entry<String, Object> entry : map.entrySet()) {
					String key = entry.getKey();
					Object value = entry.getValue();
					if (methodName.startsWith("set")) {
						String paramType = method.getParameterTypes()[0].getSimpleName();
						if (key.equalsIgnoreCase(methodName.substring(3))) {
							if ("Integer".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Integer.parseInt(value + "") });
							if ("short".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Short.parseShort(value + "") });
							if ("long".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Long.parseLong(value + "") });
							if ("BigDecimal".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { new BigDecimal(value + "") });
							if ("String".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { value });
							continue;
						}
					}
				}
				// equal中值赋给对象
				for (Entry<String, Object> entry : equalMap.entrySet()) {
					String key = entry.getKey();
					Object value = entry.getValue();
					if (methodName.startsWith("set")) {
						String paramType = method.getParameterTypes()[0].getSimpleName();
						if (key.equalsIgnoreCase(methodName.substring(3))) {
							if ("Integer".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Integer.parseInt(value + "") });
							if ("short".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Short.parseShort(value + "") });
							if ("long".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { Long.parseLong(value + "") });
							if ("BigDecimal".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { new BigDecimal(value + "") });
							if ("String".equalsIgnoreCase(paramType))
								method.invoke(object, new Object[] { value });
							continue;
						}
					}
				}
			}
		} catch (IllegalArgumentException e) {
			log.error("parseElement IllegalArgumentException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			log.error("parseElement IllegalAccessException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			log.error("parseElement InvocationTargetException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (SecurityException e) {
			log.error("parseElement SecurityException:" + e.getLocalizedMessage());
			e.printStackTrace();
		}
		return object;
	}

	/**
	 * Object转Map 根据Object赋值Map
	 * 
	 * @param object
	 *            数据对象
	 * @return
	 */
	public static Map<String, Object> saveObject2Map(Object object) {
		Map<String, Object> map = new HashMap<String, Object>();
		Method[] methods = object.getClass().getDeclaredMethods();
		try {
			for (Method method : methods) {
				String methodName = method.getName();
				String key = null;
				if (methodName.startsWith("get")) {
					key = methodName.substring(3);
				} else if (methodName.startsWith("is")) {
					key = methodName.substring(2);
				} else if (null == key) {
					continue;
				}
				key = key.substring(0, 1).toLowerCase() + key.substring(1);
				Object value = method.invoke(object, new Object[0]);
				map.put(key, value);
			}
		} catch (IllegalArgumentException e) {
			log.error("parseElement IllegalArgumentException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			log.error("parseElement IllegalAccessException:" + e.getLocalizedMessage());
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			log.error("parseElement InvocationTargetException:" + e.getLocalizedMessage());
			e.printStackTrace();
		}
		return map;
	}

	/**
	 * Element转Object
	 * 
	 * @param element
	 *            对象节点
	 * @return
	 */
	public static Object saveElement2Object(Element element) {
		return new XStream(new DomDriver()).fromXML(element.asXML());
	}

	/**
	 * Object转Element
	 * 
	 * @param object
	 *            数据对象
	 * @return
	 */
	public static Element saveObject2Element(Object object) {
		Element element = null;
		try {
			element = DocumentHelper.parseText(new XStream().toXML(object)).getRootElement();
		} catch (DocumentException e) {
			log.error("parseElement DocumentException:" + e.getLocalizedMessage());
			e.printStackTrace();
		}
		return element;
	}

	/**
	 * String转Object
	 * 
	 * @param xml
	 * @return
	 */
	public static Object saveString2Object(String xml) {
		return new XStream(new DomDriver()).fromXML(xml);
	}

	/**
	 * Object转String
	 * 
	 * @param object
	 * @return
	 */
	public static String saveObject2String(Object object) {
		Element element = null;
		try {
			element = DocumentHelper.parseText(new XStream().toXML(object)).getRootElement();
		} catch (DocumentException e) {
			log.error("parseElement DocumentException:" + e.getLocalizedMessage());
			e.printStackTrace();
		}
		if (element == null) {
			return null;
		}
		return element.asXML();
	}
}
