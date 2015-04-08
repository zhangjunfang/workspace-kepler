package com.ctfo.advice.model;

/**
 * 此类的目的是用于封装从mq获取的广播信息
 * @author yinshuaiwei
 *
 */
public class MqEntity {
	/**
	 * 操作类型：增加
	 */
	public static final String OPERATER_TYPE_ADD = "ADD";
	/**
	 * 操作类型：修改
	 */
	public static final String OPERATER_TYPE_UPDATA = "UPDATA";
	/**
	 * 操作类型：删除
	 */
	public static final String OPERATER_TYPE_DELETE = "DELETE";
	
	/**
	 * 从mq中取出的数据的操作类型
	 */
	private String operateType;
	/**
	 * 从mq中取出的数据的字段属性
	 */
	private String businessKey;
	/**
	 * 从mq中取出的数据的字段值
	 */
	private String value;
	
	public MqEntity(){
		
	}
	
	public MqEntity(String  operateType,String column,String columnValue){
		this.operateType = operateType;
		this.businessKey = column;
		this.value = columnValue;
	}

	public String getOperateType() {
		return operateType;
	}

	public void setOperateType(String operateType) {
		this.operateType = operateType;
	}

	public String getBusinessKey() {
		return businessKey;
	}

	public void setBusinessKey(String column) {
		this.businessKey = column;
	}

	public String getValue() {
		return value;
	}

	public void setValue(String columnValue) {
		this.value = columnValue;
	}
}
