package com.ctfo.informationser.util;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.List;
import java.util.Map;

import org.dom4j.Document;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;

import com.ctfo.informationser.monitoring.beans.VehicleInfo;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： MonitorConfigSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>Dec 6, 2011</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public class XMLParse {

	public static void main(String[] args) {
		String value=null;
		System.out.println(String.valueOf(value == null ? "" : value));

	}

	/**
	 * 
	 * @param map
	 * @param object
	 * @param id
	 * @return
	 */
	public static Document getResponse(Map<String, String> map, Object object, String id) {
		Document document = DocumentHelper.createDocument();
		Element element = getRoot(document, id);
		Element el = getResult(element);
		if (object instanceof List) {
			List<Object> list = (List<Object>) object;
			for (Object ob : list) {
				addElement(el, map, ob);
			}
			return document;
		} else {
			addElement(el, map, object);
			return document;
		}
	}

	/**
	 * 
	 * @param element
	 * @param keys
	 * @param object
	 */
	public static void addElement(Element element, Map<String, String> map, Object object) {
		Element el = element.addElement("Item");
		if (object instanceof Integer || object instanceof Boolean) {
			getItemResult(el, object);
			return;
		}
		if (object != null) {
			Object[] oArray = null;
			Class<?> ob = object.getClass();
			Method methods[] = ob.getMethods();
//			int count = 0;
			for (Method m : methods) {
				// 属性的get方法
				if (m.getName().startsWith("get")) {
					// 截取方法的属性名,拿这个属性去map中对比key
					String key = m.getName().substring(3);
					String keyName = String.valueOf(key.charAt(0)).toLowerCase().concat(key.substring(1));
					// 获取动态参数中存取的对象属性
					String value = map.get(keyName);
					if (value != null) {
						Object methodValue;
						try {
							methodValue = m.invoke(object, oArray);
							el.addAttribute(value, String.valueOf(methodValue == null ? "" : methodValue));
						} catch (IllegalArgumentException e) {
							e.printStackTrace();
						} catch (IllegalAccessException e) {
							e.printStackTrace();
						} catch (InvocationTargetException e) {
							e.printStackTrace();
						}
					} else {
						continue;
					}
				}
			}
		}
	}

	/**
	 * 通过反射获取Reponse XML 数据
	 * 
	 * @param keys
	 *            指定返回属性
	 * @param object
	 *            指定返回对象
	 * @return 返回XML对象
	 */
	public static Document getResponse(String[] keys, Object object, String id) {
		Document document = DocumentHelper.createDocument();
		Element element = getRoot(document, id);
		Element el = getResult(element);
		int i = 0;
		if (keys != null && keys.length != 0) {
			i = 1;
		}
		switch (i) {
		case 1:
			if (object instanceof List) {
				List<Object> list = (List<Object>) object;
				for (Object ob : list) {
					addElement(el, keys, ob);
				}
				return document;
			} else {
				addElement(el, keys, object);
				return document;
			}
		default:
			if (object instanceof List) {
				List<Object> list = (List<Object>) object;
				for (Object ob : list) {
					addElement(el, ob);
				}
			} else {
				addElement(el, object);
			}

			break;
		}
		return document;
	}

	/**
	 * 通过反射获取Reponse XML 数据
	 * 
	 * @param keys
	 *            指定返回属性
	 * @param object
	 *            指定返回对象
	 * @return 返回XML对象
	 */
	public static Document getResponse(String[] keys, Object object) {
		Document document = DocumentHelper.createDocument();
		Element element = getRoot(document);
		Element el = getResult(element);
		int i = 0;
		if (keys != null && keys.length != 0) {
			i = 1;
		}
		switch (i) {
		case 1:
			if (object instanceof List) {
				List<Object> list = (List<Object>) object;
				for (Object ob : list) {
					addElement(el, keys, ob);
				}
				return document;
			} else {
				addElement(el, keys, object);
				return document;
			}
		default:
			if (object instanceof List) {
				List<Object> list = (List<Object>) object;
				for (Object ob : list) {
					addElement(el, ob);
				}
			} else {
				addElement(el, object);
			}

			break;
		}
		return document;
	}

	/**
	 * 组装Response
	 * 
	 * @param document
	 *            Document
	 * @return Element
	 */
	public static Element getRoot(Document document, String id) {
		Element response = document.addElement("Response");
		response.addAttribute("id", id);
		return response;
	}

	/**
	 * 组装Response
	 * 
	 * @param document
	 *            Document
	 * @return Element
	 */
	public static Element getRoot(Document document) {
		Element response = document.addElement("Response");
		return response;
	}

	/**
	 * 组装Result
	 * 
	 * @param element
	 *            Element
	 * @return Element
	 */
	public static Element getResult(Element element) {
		return element.addElement("Result");
	}

	/**
	 * 组装参数
	 * 
	 * @param element
	 *            Elemnt
	 * @return Element
	 */
	public static Element getParam(Element element) {
		return element.addElement("Param");
	}

	/**
	 * 根据指定返回的对象组装XML
	 * 
	 * @param element
	 *            ELEMENT
	 * @param object
	 *            指定返回的对象
	 */
	public static void addElement(Element element, Object object) {
		Element el = element.addElement("Item");
		if (object instanceof Integer || object instanceof Boolean || object instanceof String) {
			getItemResult(el, object);
			return;
		}
		if (object != null) {
			Object[] oArray = null;
			Class<?> ob = object.getClass();
			Method methods[] = ob.getMethods();
//			int count = 0;
			for (Method m : methods) {
				if (m.getName().startsWith("get")) {
					try {
						Object value = m.invoke(object, oArray);
						el.addAttribute(m.getName().replace("get", ""), String.valueOf(value == null ? "" : value));
//						count = 0;
						break;
					} catch (IllegalArgumentException e) {
						e.printStackTrace();
					} catch (IllegalAccessException e) {
						e.printStackTrace();
					} catch (InvocationTargetException e) {
						e.printStackTrace();
					}
				}
			}
		} else {
			System.out.println("ERROR!The Object is null.");
		}
	}

	/**
	 * 根据指定的属性返回某个对象属性的数据
	 * 
	 * @param element
	 *            Element
	 * @param keys
	 *            指定返回的属性
	 * @param object
	 *            指定数据对象
	 */
	public static void addElement(Element element, String keys[], Object object) {

		Element el = element.addElement("Item");
		if (object instanceof Integer || object instanceof Boolean) {
			getItemResult(el, object);
			return;
		}
		Object[] oArray = null;
		for (String key : keys) {
			if (object != null) {
				Class<?> ob = object.getClass();
				String methodName = "get" + key;
				Method methods[] = ob.getMethods();
				int count = 0;
				for (Method m : methods) {
					if (m.getName().startsWith("get")) {
						if (methodName.equalsIgnoreCase(m.getName())) {
							try {
								Object value = m.invoke(object, oArray);
								el.addAttribute(key, String.valueOf(value == null ? "" : value));
								count = 0;
								break;
							} catch (IllegalArgumentException e) {
								e.printStackTrace();
							} catch (IllegalAccessException e) {
								e.printStackTrace();
							} catch (InvocationTargetException e) {
								e.printStackTrace();
							}
						} else {
							count++;
						}
					}
				}

				if (count != 0) {
					el.addAttribute(key, "");
				}
			} else {
				el.addAttribute(key, "");
			}

		}
	}

	public static Element getItemResult(Element element, Object object) {
		return element.addAttribute("result", String.valueOf(object));
	}
	/**
	 * 根据车辆列表返回请求结果
	 * @param map
	 * @return
	 */
	public static String getElementString(List<VehicleInfo> vehicleList, String key){
		StringBuffer sb = new StringBuffer("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Response id=\"");
		sb.append(key);
		sb.append("\"><Result><Item simMapping=");
		for(VehicleInfo vehicle : vehicleList){
			sb.append(vehicle.getCommaddr()).append(","); 
		}
		sb.append(" /></Result></Response>");
		return sb.toString();
	}
}
