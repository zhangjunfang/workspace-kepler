package com.ctfo.informationser.util;

/**
 * 
 * bo对象属性拷贝formbean，formbean对象属性拷贝到bo.
 * 
 * @version 1.0
 * 
 * @author 王鹏
 * @since JDK1.6
 */

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;

@SuppressWarnings({ "rawtypes", "unused", "unchecked" })
public class BeanUtil {

	/**
	 * 定制拷贝Bean对象方法
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 * @param copyNull
	 *            是否拷贝空对象
	 * @param fieldNames
	 *            指定拷贝的对象属性名称
	 * @param include
	 *            名称列表是否是白名单
	 */
	public static void copyFields(Object src, Object des, boolean copyNull, String[] fieldNames, boolean include) {
		Class desClass = des.getClass();
		Arrays.sort(fieldNames);

		Method[] methods = src.getClass().getMethods();
		for (int i = 0; i < methods.length; i++) {
			Method method = methods[i];
			String methodName = method.getName();

			if (methodName.startsWith("get") && !methodName.equals("getClass")) {
				String attrName = methodName.substring(3, 4).toLowerCase() + methodName.substring(4, methodName.length());
				boolean inAttr = Arrays.binarySearch(fieldNames, attrName) >= 0 ? true : false;
				try {
					if (inAttr == include) {

						Class returnType = method.getReturnType();
						Object returnValue = method.invoke(src, new Object[] {});
						if (!copyNull && returnValue == null) {
							continue;
						}

						String invokeMethodName = "set" + methodName.substring(3, methodName.length());
						Method invokeMethod = desClass.getMethod(invokeMethodName, new Class[] { returnType });
						invokeMethod.invoke(des, new Object[] { returnValue });
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
	}

	/**
	 * 定制拷贝Bean对象方法
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 * @param copyNull
	 *            是否拷贝空对象
	 * @param srcFieldNames
	 *            指定拷贝的对象属性名
	 * @param desFieldNames
	 *            指定拷贝的对象属性名称
	 */
	public static void copyFields(Object src, Object des, boolean copyNull, String[] srcFieldNames, String[] desFieldNames) {
		Class<?> cArray = null;
		for (int i = 0; i < srcFieldNames.length; i++) {
			String fieldName = srcFieldNames[i];
			String desFieldName = desFieldNames[i];
			String methodName = "get" + fieldName.substring(0, 1).toUpperCase() + fieldName.substring(1, fieldName.length());
			try {
				Method method = src.getClass().getMethod(methodName, cArray);
				Class returnType = method.getReturnType();
				Object returnValue = method.invoke(src, new Object[] {});
				if (!copyNull && returnValue == null) {
					continue;
				}
				String setInvokeMethodName = "set" + (desFieldName.substring(0, 1).toUpperCase() + desFieldName.substring(1, desFieldName.length()));
				String getInvokeMethodName = "get" + (desFieldName.substring(0, 1).toUpperCase() + desFieldName.substring(1, desFieldName.length()));
				Method getInvokeMethod = des.getClass().getMethod(getInvokeMethodName, cArray);
				Class desReturnType = getInvokeMethod.getReturnType();

				Method invokeMethod = des.getClass().getMethod(setInvokeMethodName, new Class[] { desReturnType });
				invokeMethod.invoke(des, new Object[] { returnValue.getClass().getName().equals(desReturnType.getName()) ? returnValue : desReturnType.cast(returnValue) });
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	/**
	 * 拷贝Bean对象 源与目的属性名相同
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 * @param copyNull
	 *            是否拷贝为Null对象
	 */
	public static void copyFields(Object src, Object des, boolean copyNull) {
		final String get = "get";
		final String set = "set";
		final String is = "is";

		Class srcClass = src.getClass();
		Class desClass = des.getClass();

		Method[] methods = srcClass.getMethods();
		for (int i = 0; i < methods.length; i++) {
			Method method = methods[i];
			String methodName = method.getName();
			if ((methodName.startsWith(get) && !methodName.equals("getClass")) || methodName.startsWith(is)) {
				String invokeMethodName = set + methodName.substring(3);

				try {
					Method invokeMethod = desClass.getMethod(invokeMethodName, new Class[] { method.getReturnType() });
					Object result = method.invoke(src, new Object[] {});

					if (!copyNull && result == null) {
						continue;
					}
					invokeMethod.invoke(des, new Object[] { result });
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
	}

	/**
	 * 拷贝Bean对象 源与目的属性名相同
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 * @param copyNull
	 *            是否拷贝为Null对象
	 * @param copyEmpty
	 *            是否拷贝为空对象
	 */
	public static void copyFields(Object src, Object des, boolean copyNull, boolean copyEmpty) {
		final String get = "get";
		final String set = "set";
		final String is = "is";

		Class srcClass = src.getClass();
		Class desClass = des.getClass();

		Method[] methods = srcClass.getMethods();
		for (int i = 0; i < methods.length; i++) {
			Method method = methods[i];
			String methodName = method.getName();

			if ((methodName.startsWith(get) && !methodName.equals("getClass")) || methodName.startsWith(set) || methodName.startsWith(is)) {
				String invokerMethodName = set + methodName.substring(3);

				try {
					Method invokerMethod = desClass.getMethod(invokerMethodName, new Class[] { method.getReturnType() });
					Object result = method.invoke(src, new Object[] {});

					if (!copyNull && result == null) {
						continue;
					}
					if (!copyEmpty && result.toString().trim().equals("")) {
						continue;
					}

					invokerMethod.invoke(des, new Object[] { result });
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
	}

	/**
	 * 拷贝Bean对象 属性名称相同 类型一致 就会拷贝 会拷贝空;
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 */
	public static void copyFields(Object src, Object des) {
		final String get = "get";
		final String set = "set";
		final String is = "is";

		Class srcClass = src.getClass();
		Class desClass = des.getClass();
		try {
			Object[] oArray = null;
			Method[] methods = srcClass.getMethods();
			Method[] methoddes = desClass.getMethods();
			for (int i = 0; i < methods.length; i++) {
				Method method = methods[i];
				String methodName = method.getName();
				if ((methodName.startsWith(get) && !methodName.equals("getClass"))) {
					String invkemethod = set + methodName.substring(3);
					for (Method method2 : methoddes) {
						// 找到名称相同的方法
						if (method2.getName().equals(invkemethod)) {
							// 类型一致
							if (method.getReturnType().getName().equals(method2.getParameterTypes()[0].getName())) {
								Object object = method.invoke(src, oArray);
								if (object != null) {
									method2.invoke(des, object);
								}
							}

						}
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * 批量拷贝Bean对象
	 * 
	 * @param src
	 *            源端Bean对象
	 * @param des
	 *            目的端Bean对象
	 * @param copyNull
	 *            是否拷贝为Null对象
	 */
	public static List copyFields(List src, Class des, boolean copyNull) {
		List des_objs = new ArrayList();
		Iterator iter = src.iterator();
		for (; iter.hasNext();) {
			try {
				Object odes = des.newInstance();
				Object obj = iter.next();
				copyFields(obj, des, copyNull);
				des_objs.add(odes);
			} catch (Exception e) {
				System.out.println("copyFields 362 " + e.getMessage());
			}

		}
		return des_objs;
	}

	public static List copyFields(List src, Class des, boolean copyNull, boolean copyEmpty) {
		List des_objs = new ArrayList();
		Iterator iter = src.iterator();
		for (; iter.hasNext();) {
			try {
				Object odes = des.newInstance();
				Object obj = iter.next();
				copyFields(obj, des, copyNull, copyEmpty);
				des_objs.add(odes);
			} catch (Exception e) {
				System.out.println("copyFields 362 " + e.getMessage());
			}
		}
		return des_objs;
	}

	public static List copyFields(List src, Class des, boolean copyNull, String[] fieldNames, boolean include) {
		List des_objs = new ArrayList();
		Iterator iter = src.iterator();
		for (; iter.hasNext();) {

			try {
				Object odes = des.newInstance();
				Object obj = iter.next();
				copyFields(obj, des, copyNull, fieldNames, include);
				des_objs.add(odes);
			} catch (Exception e) {
				System.out.println("copyFields 362 " + e.getMessage());
			}

		}
		return des_objs;
	}

	/**
	 * 获取指定对象的指定方法返回值
	 * 
	 * @param object
	 *            指定期望调用的对象
	 * @param name
	 *            指定期望调用的对象方法
	 * @return Object 返回值
	 */
	protected static Object getValue(Object object, String name) {
		try {
			Method method = object.getClass().getMethod("get" + formatCase(name), new Class[] {});
			Object result = method.invoke(object, new Object[] {});
			if (result != null) {
				return result;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * 格式化方法名，将首字母设置为大写
	 * 
	 * @param name
	 *            方法名称
	 * @return String 格式化后的方法名称
	 */
	protected static String formatCase(String name) {
		StringBuffer buffer = new StringBuffer(name.toLowerCase());
		char c = Character.toUpperCase(name.charAt(0));

		buffer.deleteCharAt(0);
		buffer.insert(0, c);

		return buffer.toString();
	}

}
