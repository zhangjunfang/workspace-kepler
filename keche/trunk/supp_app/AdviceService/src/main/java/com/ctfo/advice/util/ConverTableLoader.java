package com.ctfo.advice.util;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.model.ConverTableEntity;

public class ConverTableLoader {
	private static Logger log = LoggerFactory.getLogger(ConverTableLoader.class);
	private final static ConverTableLoader converTableLoader = new ConverTableLoader();
	private Map<String,List<ConverTableEntity>> converTableMap = new HashMap<String,List<ConverTableEntity>>();

	private ConverTableLoader() {
		// 加载配置文件
		String path = ConverTableLoader.class.getResource("/").getPath().toString();
		log.info("path:" + path);
		parseConverTableXml(path);
	}
	
	/**
	 * 解析配置文件的信息并封装
	 * @param path
	 */
	@SuppressWarnings("unchecked")
	private void parseConverTableXml(String path){
		SAXReader reader = new SAXReader();
		try {
			Document document = reader.read(new File(path + File.separator + "conver_table.xml"));
			Element root = document.getRootElement();//获取xml的根configuration
			List<Element> convers = root.elements();//获取configuration下的conver列
			for(Element conver : convers){
				String field = conver.attributeValue("businessKey").toLowerCase();
				String tableName = conver.attributeValue("tableName").toLowerCase();
				ConverTableEntity converTable = new ConverTableEntity(field,conver.attributeValue("sql"),tableName);
				if(converTableMap.containsKey(field)){
					converTableMap.get(field).add(converTable);
				}else{
					List<ConverTableEntity> list = new ArrayList<ConverTableEntity>();
					list.add(converTable);
					converTableMap.put(field, list);
				}
				List<Element> columns = conver.elements();//获取conver下的column列
				for(Element column : columns){
					converTable.addColumn(column.attributeValue("name"));
				}
			}
		} catch (DocumentException e) {
			log.error(e.getMessage(), e);
			System.out.println(e.getMessage()); 
		}
	}
	
	/**
	 * 根据field的值，获取ConverTable
	 * @param field
	 * @return
	 */
	public List<ConverTableEntity> getConverTable(String field){
		return converTableMap.get(field.toLowerCase());
	}
	
	/**
	 * 获取实例
	 * @return
	 */
	public static ConverTableLoader getInstance(){
		return converTableLoader;
	}
	
	public static void main(String[] args) {
//		ConverTableLoader converTableLoader = ConverTableLoader.getInstance();
		/*ConverTableEntity converTable = converTableLoader.getConverTable("vid");
		System.out.println(converTable.getBusinessKey());
		System.out.println(converTable.getSqlKey());
		System.out.println(converTable.getColumns().size());*/
	}
}
