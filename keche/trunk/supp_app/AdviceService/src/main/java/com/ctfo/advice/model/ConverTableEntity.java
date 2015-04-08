package com.ctfo.advice.model;

import java.util.ArrayList;
import java.util.List;

/**
 * 用于封装conver_table.xml的属性数据
 * 
 * @author yinshuaiwei
 *
 */
public class ConverTableEntity {
	/**
	 * 配置文件中的field值
	 */
	private String businessKey;
	
	/**
	 * 配置文件中的sql值
	 */
	private String sqlKey;
	
	/**
	 * 配置文件中column的集合
	 */
	private List<String> columns;
	
	/**
	 * 配置文件中的tableName值
	 */
	private String tableName;
	
	public ConverTableEntity(){}
	
	public ConverTableEntity(String fileName,String sqlKey,String tableName){
		this(fileName,sqlKey);
		this.tableName = tableName;
	}
	
	public ConverTableEntity(String fileName,String sqlKey){
		this.businessKey = fileName;
		this.sqlKey = sqlKey;
	}
	
	public void addColumn(String column){
		if(null == columns){
			columns = new ArrayList<String>();
		}
		columns.add(column);
	}

	public String getBusinessKey() {
		return businessKey;
	}

	public void setBusinessKey(String fieldName) {
		this.businessKey = fieldName;
	}

	public String getSqlKey() {
		return sqlKey;
	}

	public void setSqlKey(String sqlKey) {
		this.sqlKey = sqlKey;
	}

	public List<String> getColumns() {
		return columns;
	}

	public String getTableName() {
		return tableName;
	}

	public void setTableName(String tableName) {
		this.tableName = tableName;
	}
}
