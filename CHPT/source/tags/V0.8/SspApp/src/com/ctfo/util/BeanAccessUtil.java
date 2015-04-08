
package com.ctfo.util;


import java.beans.PropertyDescriptor;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.List;

import org.apache.commons.beanutils.BeanUtils;
import org.apache.commons.beanutils.ConvertUtils;
import org.apache.commons.beanutils.Converter;
import org.apache.commons.beanutils.DynaBean;
import org.apache.commons.beanutils.DynaClass;
import org.apache.commons.beanutils.DynaProperty;
import org.apache.commons.beanutils.PropertyUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.omg.CORBA.portable.ApplicationException;

import com.ctfo.local.exception.CtfoAppException;




public class BeanAccessUtil {
    private static Log log;
	private Integer int1;

	private int int2;

	public static void main(String[] args) {
		try {
			BeanAccessUtil dd = new BeanAccessUtil();
			BeanUtils.getProperty(dd, "testFld");
			/**
			 * java.util.Date myDate=Formater.toDate("2005-12-11"); java.sql.Date myDate1=new java.sql.Date(myDate.getTime()); BeanUtils.setProperty(dd,"testFld",myDate1);
			 * strTmp=BeanUtils.getProperty(dd,"testFld");
			 */
			BeanUtils.setProperty(dd, "int1", "1");
			BeanUtils.getProperty(dd, "int1");
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * 把一个bean的域值拷贝到另一个BEAN的域值。
	 * 
	 * @param toObj
	 * @param fromObj
	 * @throws ApplicationException
	 */
	public static void copyBeanProperties(Object toObj, Object fromObj) throws CtfoAppException {
		try {
			BeanUtils.copyProperties(toObj, fromObj);
			/**
			 * List fldNames=BeanAccessUtil.getFldNames(toObj.getClass()); if(fldNames==null){ return; } for(int i=0; i<fldNames.size(); i++){ Field myFld=(Field)fldNames.get(i);
			 * copyBeanProperty(toObj,fromObj,myFld); }
			 */
		} catch (Exception ex) {
			ex.printStackTrace();
			throw new CtfoAppException(ex);
		}
	}

	public static void clearBeanProperties(Object obj){

	    PropertyDescriptor[] origDescriptors
		= PropertyUtils.getPropertyDescriptors(obj);
	    for (int i = 0; i < origDescriptors.length; i++) {
			String name = origDescriptors[i].getName();
			if (!"class".equals(name)
			    && PropertyUtils.isWriteable(obj, name)) {
			    try {
			    	if(PropertyUtils.getPropertyType(obj, name).equals(boolean.class)){
						copyProperty(obj, name, false);
			    	}
			    	else if(PropertyUtils.getPropertyType(obj, name).equals(int.class)){
						copyProperty(obj, name, false);
			    	}
			    	else{
			    		copyProperty(obj, name, null);
			    	}
			    } catch (IllegalAccessException e) {
				} catch (InvocationTargetException e) {
				} catch (NoSuchMethodException e) {
				}
			}
	    }
	}

	public static void copyProperty(Object bean, String name, Object value) throws IllegalAccessException,
			InvocationTargetException {
		if (log.isTraceEnabled()) {
			StringBuffer sb = new StringBuffer("  copyProperty(");
			sb.append(bean);
			sb.append(", ");
			sb.append(name);
			sb.append(", ");
			if (value == null)
				sb.append("<NULL>");
			else if (value instanceof String)
				sb.append((String) value);
			else if (value instanceof String[]) {
				String[] values = (String[]) value;
				sb.append('[');
				for (int i = 0; i < values.length; i++) {
					if (i > 0)
						sb.append(',');
					sb.append(values[i]);
				}
				sb.append(']');
			} else
				sb.append(value.toString());
			sb.append(')');
			log.trace(sb.toString());
		}
		Object target = bean;
		int delim = name.lastIndexOf('.');
		if (delim >= 0) {
			try {
				target = PropertyUtils.getProperty(bean, name.substring(0, delim));
			} catch (NoSuchMethodException e) {
				return;
			}
			name = name.substring(delim + 1);
			if (log.isTraceEnabled()) {
				log.trace("    Target bean = " + target);
				log.trace("    Target name = " + name);
			}
		}
		String propName = null;
		Class<?> type = null;
		int index = -1;
		String key = null;
		propName = name;
		int i = propName.indexOf('[');
		if (i >= 0) {
			int k = propName.indexOf(']');
			try {
				index = Integer.parseInt(propName.substring(i + 1, k));
			} catch (NumberFormatException e) {
				/* empty */
			}
			propName = propName.substring(0, i);
		}
		int j = propName.indexOf('(');
		if (j >= 0) {
			int k = propName.indexOf(')');
			try {
				key = propName.substring(j + 1, k);
			} catch (IndexOutOfBoundsException e) {
				/* empty */
			}
			propName = propName.substring(0, j);
		}
		if (target instanceof DynaBean) {
			DynaClass dynaClass = ((DynaBean) target).getDynaClass();
			DynaProperty dynaProperty = dynaClass.getDynaProperty(propName);
			if (dynaProperty == null)
				return;
			type = dynaProperty.getType();
		} else {
			PropertyDescriptor descriptor = null;
			do {
				try {
					descriptor = PropertyUtils.getPropertyDescriptor(target, name);
					if (descriptor != null)
						break;
				} catch (NoSuchMethodException e) {
					/* empty */
				}
				return;
			} while (false);
			type = descriptor.getPropertyType();
			if (type == null) {
				if (log.isTraceEnabled())
					log.trace("    target type for property '" + propName + "' is null, so skipping ths setter");
				return;
			}
		}
		if (log.isTraceEnabled())
			log.trace("    target propName=" + propName + ", type=" + type + ", index=" + index + ", key=" + key);
		if (index >= 0) {
			Converter converter = ConvertUtils.lookup(type.getComponentType());
			if (converter != null) {
				log.trace("        USING CONVERTER " + converter);
				value = converter.convert(type, value);
			}
			try {
				PropertyUtils.setIndexedProperty(target, propName, index, value);
			} catch (NoSuchMethodException e) {
				throw new InvocationTargetException(e, "Cannot set " + propName);
			}
		} else if (key != null) {
			try {
				PropertyUtils.setMappedProperty(target, propName, key, value);
			} catch (NoSuchMethodException e) {
				throw new InvocationTargetException(e, "Cannot set " + propName);
			}
		} else {
			Converter converter = ConvertUtils.lookup(type);
			if (converter != null) {
				log.trace("        USING CONVERTER " + converter);
				value = converter.convert(type, value);
			}
			try {
				PropertyUtils.setSimpleProperty(target, propName, value);
			} catch (NoSuchMethodException e) {
				throw new InvocationTargetException(e, "Cannot set " + propName);
			}
		}
	}

	/**
	 * 将指定的字段的值从源对象中拷贝到目标对象
	 * 
	 * @param toObj
	 * @param fromObj
	 * @param fld
	 * @throws CtfoAppException
	 */
	public static void copyBeanProperty(Object toObj, Object fromObj, Field toObjFld) throws CtfoAppException {
		try {
			String fldValue = getPropertyValue(fromObj, toObjFld.getName());

			Class<?> fldType = toObjFld.getType();
			String strFldType = fldType.getName();
			if (strFldType.equalsIgnoreCase("java.util.Date")) {
				java.util.Date myDate = DateUtil.parse(fldValue);
				BeanUtils.setProperty(toObj, toObjFld.getName(), myDate);
			} else if (strFldType.equalsIgnoreCase("java.sql.Date")) {
				java.util.Date myDate = DateUtil.parse(fldValue);
				java.sql.Date myDate1 = new java.sql.Date(myDate.getTime());
				BeanUtils.setProperty(toObj, toObjFld.getName(), myDate1);
			}
		} catch (Exception ex) {
			ex.printStackTrace();
			throw new CtfoAppException(ex);
		}

	}
	
	public static Class<?> getProptType(Object obj,String fldName){
		PropertyDescriptor[] origDescriptors = PropertyUtils.getPropertyDescriptors(obj);
		for (int i = 0; i < origDescriptors.length; i++) {
			String name = origDescriptors[i].getName();
			if (name.equals(fldName)) {
				return origDescriptors[i].getPropertyType();
			}
		}
		return null;
	}

	/**
	 * 获利指定对象中指定字段名的值
	 * 
	 * @param obj
	 * @param fldName
	 * @return
	 * @throws CtfoAppException
	 */
	private static String getPropertyValue(Object obj, String fldName) throws CtfoAppException {
		try {
			String fldValue = BeanUtils.getProperty(obj, fldName);
			return fldValue;
		} catch (NoSuchMethodException ex) {
			return null;
		} catch (Exception ex) {
			//LogUtil.logInfo("ȡ" + obj.getClass().getName() + "��" + fldName + "����ֵ�������");
			ex.printStackTrace();
			throw new CtfoAppException(ex.getMessage());
		}
	}

	/**
	 * 获得指定类的所有字段列表 
	 * @param myClass
	 * @return
	 */
	public static List<Field> getFldNames(Class<?> myClass) {
		List<Field> result = new ArrayList<Field>();

		Class<?> superClass = myClass;
		while (superClass != null) {
			Field[] myFlds = superClass.getDeclaredFields();
			for (int i = 0; i < myFlds.length; i++) {
				result.add(myFlds[i]);
			}
			superClass = superClass.getSuperclass();
		}

		return result;
	}

	/**
	 * 得到字段的属性值，属性值以字符类型返回。 注：要想得到该属性的值，该字段必须要有一个在pulic范围的get方法。
	 * 
	 * @param myObj
	 *            要查找的字段的对象。
	 * @param fldName
	 *            字段的名称。
	 * @return 如果没有该字段或字段的属性值为NULL，则返回一个null。
	 */
	public static String getFieldValue(Object myObj, String fldName) throws CtfoAppException {
		try {
			String fldValue = BeanUtils.getProperty(myObj, fldName);

			return fldValue;
		} catch (NoSuchMethodException ex) {
			return null;
		} catch (Exception ex) {
			throw new CtfoAppException(ex.getMessage());
		}
	}

	/**
	* 开发人：宋帅杰
	* 开发日期: 2008-10-29  下午12:08:10
	* 功能描述: 将一个对象中的数组属性中的某一个值拷贝到目标对象的同名属性中
	* 			不支持基本数据类型
	* 方法的参数和返回值: 
	* @param to
	* @param from
	* @param index
	* @param proptName
	* @return
	*/
	public static boolean copyAryPropt(Object to, Object from, int index,String proptName) {
		boolean change = false;
		try {
			Object value = PropertyUtils.getSimpleProperty(from, proptName);
			if (value instanceof Object[]){
				Object[] ary = (Object[]) value;
				if(ary.length <= index || ObjEquals(PropertyUtils.getSimpleProperty(to, proptName),ary[index]))return false;
				BeanUtils.setProperty(to, proptName, ((Object[]) value)[index]);
				change=true;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return change;
	}
	/**
	 * @author ： 张波
	 * @since： 2013-7-9 上午9:04:36
	 * 功能描述：将一个对象中的所有数组属性中的某一个值拷贝到目标对象的同名属性中。
	 * 			不支持基本数据类型
	 * 方法的参数和返回值
	 * @param to
	 * @param from
	 * @param index
	 * @return
	 * boolean 
	 */
	public static boolean copyAryPropts(Object to, Object from, int index) {
		PropertyDescriptor[] origDescriptors = PropertyUtils.getPropertyDescriptors(to);
		boolean change = false;
		for (int i = 0; i < origDescriptors.length; i++) {
			String name = origDescriptors[i].getName();
			if (!"class".equals(name) && PropertyUtils.isReadable(from, name) && PropertyUtils.isWriteable(to, name)) {
				try {
					Object value = PropertyUtils.getSimpleProperty(from, name);
					if (value instanceof Object[]){
						Object[] ary = (Object[]) value;
						if(ary.length <= index || ObjEquals(PropertyUtils.getSimpleProperty(to, name),ary[index]))continue;
						BeanUtils.setProperty(to, name, ary[index]);
						change=true;
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
		return change;
	}
	
	/**
	* 开发人：宋帅杰
	* 开发日期: 2008-11-30  上午10:54:48
	* 功能描述: 将对象中的属性以及属性对应的属性值组成一个字符串，如 name=john&sex=male&,其中split1和split2分别为= &
	* 方法的参数和返回值: 
	* @param obj
	* @param split1
	* @param split2
	* @return
	*/
	public static String toJSon(Object obj, String split1, String split2) {
		PropertyDescriptor[] origDescriptors = PropertyUtils.getPropertyDescriptors(obj);
		StringBuffer res = new StringBuffer();
		for (int i = 0; i < origDescriptors.length; i++) {
			String name = origDescriptors[i].getName();
			if (!"class".equals(name) && PropertyUtils.isWriteable(obj, name)) {
				try {
					Object value = PropertyUtils.getSimpleProperty(obj, name);
					res.append(name).append(split1).append(value==null?"":value).append(split2);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
		return res.toString();
	}
	
	public static boolean isPropertyExist(Object obj,String proptName){
		if(proptName==null)return false;
		try {
			PropertyDescriptor[] origDescriptors = PropertyUtils.getPropertyDescriptors(obj);
			for (int i = 0; i < origDescriptors.length; i++) {
				if(proptName.equals(origDescriptors[i].getName()))
					return true;
			}
		} catch (Exception e) {
			return false;
		}
		return false;
	}

	
	/**
	 * 开发人 ： 张波
	 * 开发日期： 2013-7-9 下午9:08:19
	 * 功能描述：比较两个对象是否等价，空字符串和null等价
	 * 方法的参数和返回值
	 * @param obj1
	 * @param obj2
	 * @return
	 * boolean 
	 */
	public static boolean ObjEquals(Object obj1,Object obj2){
		if("".equals(obj1))obj1=null;
		if("".equals(obj2))obj2=null;
		if(obj1==null && obj2==null)return true;
		else if(obj1==null || obj2==null)return false;
		else return obj1.equals(obj2);
	}
	/**
	 * @param testFld
	 *            The testFld to set.
	 */
	public void setTestFld(java.sql.Date testFld) {
	}

	/**
	 * @return Returns the int1.
	 */
	public Integer getInt1() {
		return int1;
	}

	/**
	 * @param int1
	 *            The int1 to set.
	 */
	public void setInt1(Integer int1) {
		this.int1 = int1;
	}

	/**
	 * @return Returns the int2.
	 */
	public int getInt2() {
		return int2;
	}

	/**
	 * @param int2
	 *            The int2 to set.
	 */
	public void setInt2(int int2) {
		this.int2 = int2;
	}

    static {
	log = LogFactory.getLog
	       ("org.apache.commons.beanutils.BeanUtils");
    }
}
