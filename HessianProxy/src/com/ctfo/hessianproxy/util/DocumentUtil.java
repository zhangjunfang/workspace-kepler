package com.ctfo.hessianproxy.util;

import java.util.ArrayList;
import java.util.List;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;

public class DocumentUtil {

	/**
	 * Xml转Document
	 * 
	 * @return
	 */
	public static Document toDocument(String xml) {
		Document document = null;
		if (null != xml) {
			try {
				document = DocumentHelper.parseText(xml);
			} catch (DocumentException e) {
				e.printStackTrace();
			}
		}
		return document;
	}

	/**
	 * 创建Document根节点
	 * 
	 * @return
	 */
	public static Element setDocument(String name) {
		Document document = DocumentHelper.createDocument();
		if (null != name) {
			document.addElement(name);
		}
		return document.getRootElement();
	}

	/**
	 * 创建Element
	 * 
	 * @param element
	 *            父节点
	 * @param name
	 *            节点名称
	 * @return
	 */
	public static Element setResultElement(Element element, String name) {
		Element resultElement = null;
		if (null != name) {
			resultElement = element.addElement(name);
		}
		return resultElement;
	}

	/**
	 * 创建Attribute
	 * 
	 * @param element
	 *            父节点
	 * @param name
	 *            属性名
	 * @param value
	 *            属性值
	 * @return
	 */
	public static Element setAttribute(Element element, String name, Object value) {
		if (null != name && !"".equals(name)) {
			element.addAttribute(name, value != null ? String.valueOf(value) : "");
		}
		return element;
	}

	/**
	 * 获取Document根节点
	 * 
	 * @param document
	 * @return
	 */
	public static Element getRootElement(Document document) {
		return document.getRootElement();
	}

	/**
	 * 获取Element集合
	 * 
	 * @param element
	 * @param name
	 *            节点名称
	 * @return
	 */
	@SuppressWarnings("unchecked")
	public static List<Element> getElementList(Element element, String name) {
		List<Element> elementList = new ArrayList<Element>();
		if (null != name) {
			elementList = element.elements(name);
		} else {
			elementList = element.elements();
		}
		return elementList;
	}

	/**
	 * 获取属性值
	 * 
	 * @param element
	 * @param name
	 *            属性名称
	 * @return
	 */
	public static String getAttribute(Element element, String name) {
		return element.attributeValue(name);
	}

	public static void main(String[] args) {
		Element root = setDocument("Xml");
		Element element1 = setResultElement(root, "Element");
		setAttribute(element1, "name1", "");
		setAttribute(element1, "total1", "");
		Element resultEElement1 = setResultElement(element1, "Item");
		setAttribute(resultEElement1, "name1-1", null);

		System.out.println(root.asXML());
	}
}
