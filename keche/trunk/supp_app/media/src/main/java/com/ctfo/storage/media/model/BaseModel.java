package com.ctfo.storage.media.model;

/**
 * ETB_Base
 * 
 * @author huangjincheng
 * 2014-5-8下午02:20:00
 * 
 */
public class BaseModel {
	/**	消息来源中心IP*/
	private String centerSourceIp = "";
	
	/** 主消息ID*/
	private String masterType = "";
	
	/** 子类型ID*/
	private String slaveType = "";
	
	/** 消息流水号*/
	private String messageRuuningNumber = "";
	
	/** 消息体长度*/
	private int messagelength = 0;
	
	/** 操作类型  0删除；1添加；2更新*/
	private int operateType = 0;
	
	/** 手机号*/
	private String phoneNum = "";

	/**
	 * 获取消息来源中心IP的值
	 * @return centerSourceIp  
	 */
	public String getCenterSourceIp() {
		return centerSourceIp;
	}

	/**
	 * 设置消息来源中心IP的值
	 * @param centerSourceIp
	 */
	public void setCenterSourceIp(String centerSourceIp) {
		this.centerSourceIp = centerSourceIp;
	}

	/**
	 * 获取主消息ID的值
	 * @return masterType  
	 */
	public String getMasterType() {
		return masterType;
	}

	/**
	 * 设置主消息ID的值
	 * @param masterType
	 */
	public void setMasterType(String masterType) {
		this.masterType = masterType;
	}

	/**
	 * 获取子类型ID的值
	 * @return slaveType  
	 */
	public String getSlaveType() {
		return slaveType;
	}

	/**
	 * 设置子类型ID的值
	 * @param slaveType
	 */
	public void setSlaveType(String slaveType) {
		this.slaveType = slaveType;
	}

	/**
	 * 获取消息流水号的值
	 * @return messageRuuningNumber  
	 */
	public String getMessageRuuningNumber() {
		return messageRuuningNumber;
	}

	/**
	 * 设置消息流水号的值
	 * @param messageRuuningNumber
	 */
	public void setMessageRuuningNumber(String messageRuuningNumber) {
		this.messageRuuningNumber = messageRuuningNumber;
	}

	/**
	 * 获取消息体长度的值
	 * @return messagelength  
	 */
	public int getMessagelength() {
		return messagelength;
	}

	/**
	 * 设置消息体长度的值
	 * @param messagelength
	 */
	public void setMessagelength(int messagelength) {
		this.messagelength = messagelength;
	}

	/**
	 * 获取操作类型0删除；1添加；2更新的值
	 * @return operateType  
	 */
	public int getOperateType() {
		return operateType;
	}

	/**
	 * 设置操作类型0删除；1添加；2更新的值
	 * @param operateType
	 */
	public void setOperateType(int operateType) {
		this.operateType = operateType;
	}

	/**
	 * 获取手机号的值
	 * @return phoneNum  
	 */
	public String getPhoneNum() {
		return phoneNum;
	}

	/**
	 * 设置手机号的值
	 * @param phoneNum
	 */
	public void setPhoneNum(String phoneNum) {
		this.phoneNum = phoneNum;
	}
	
	

	
}
