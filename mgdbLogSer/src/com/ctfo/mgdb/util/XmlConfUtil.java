
package com.ctfo.mgdb.util;

import java.io.File;
import java.util.Iterator;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;



/**
 * 
 * xml解析
 * @author huangjincheng
 *
 */
public class XmlConfUtil {
	
	public XmlConfUtil(){}
	
	public String getStringValue(String xmlpath) throws DocumentException{
		String result =null;
		String[] xmlp  = xmlpath.split("\\|");		
		SAXReader saxReader = new SAXReader();
		Document document = saxReader.read(new File("mgdbService.xml"));
		Element root = document.getRootElement();
		//Attribute ageAttr = root.getName();
		
	
		for(Iterator<?> iter1 = root.elementIterator();iter1.hasNext();){
			Element element1 = (Element) iter1.next();
			if(element1.getName() == "item"){
				if(element1.attribute("name").getValue().equals(xmlp[0])){
					for(Iterator<?> iter2 = element1.elementIterator();iter2.hasNext();){
						Element element2 = (Element) iter2.next();
						if(element2.getName() == "item"){
							if(element2.attribute("name").getValue().equals(xmlp[1])){
								for(Iterator<?> iter3 = element2.elementIterator();iter3.hasNext();){
									Element element3 = (Element) iter3.next();
									if(element3.getName() == "value"){
										result = element3.getText();
										break;
									}
								}
							}
							
						}
					}
				}
			
			}
		}
	/*	if(xmlRecur(root,"item","name").equals(xmlp[0])){
			Element element = (Element)root.elementIterator().next();
			if(xmlRecur(element,"item","name").equals(xmlp[1])){
				System.out.println(element.attribute("name").getValue());
			}
		};
		*/
		
		return result;
		
	}
	
/*	public static String xmlRecur(Element element,String name, String attribute){
		String res = "";
		for(Iterator iter = element.elementIterator();iter.hasNext();){
			if(element.getName() .equals(name)){
				res =  element.attribute(attribute).getValue();
			}
		}
		return res;
	}*/
	
	
	public static void main(String[] args) throws DocumentException {
		System.out.println(new XmlConfUtil().getStringValue(("msgServiceManage|msgServiceAddr")));
	}
	
}
