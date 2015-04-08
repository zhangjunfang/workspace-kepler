/**
 * 
 */
package com.ctfo.storage.command.io;

import com.ctfo.storage.command.util.Tools;

/**
 * @author zjhl
 *
 */
public class Header {
	/**	消息来源	*/
	private int source;
	/**	消息目的地	*/
	private int destination;
	/**	消息类型	*/
	private String type;
	/**	消息流水号	*/
	private int serial;
	/**	消息体长度	*/
	private int length;
	
	public String toString(){
		StringBuffer sb = new StringBuffer();
//		消息来源
		sb.append(Tools.fillNBitBefore(Integer.toHexString(source), 8, "0"));
//		消息目的地
		sb.append(Tools.fillNBitBefore(Integer.toHexString(destination), 8, "0"));
//		消息类型
		sb.append(type);
//		消息流水号
		sb.append(Tools.fillNBitBefore(Integer.toHexString(serial), 8, "0"));
//		消息体长度
		sb.append(Tools.fillNBitBefore(Integer.toHexString(length), 8, "0"));
		return sb.toString();
	}
	
	/**
	 * @return 获取 消息来源
	 */
	public int getSource() {
		return source;
	}
	/**
	 * 设置消息来源
	 * @param source 消息来源 
	 */
	public void setSource(int source) {
		this.source = source;
	}
	/**
	 * @return 获取 消息目的地
	 */
	public int getDestination() {
		return destination;
	}
	/**
	 * 设置消息目的地
	 * @param destination 消息目的地 
	 */
	public void setDestination(int destination) {
		this.destination = destination;
	}
	/**
	 * @return 获取 消息类型
	 */
	public String getType() {
		return type;
	}
	/**
	 * 设置消息类型
	 * @param type 消息类型 
	 */
	public void setType(String type) {
		this.type = type;
	}
	/**
	 * @return 获取 消息流水号
	 */
	public int getSerial() {
		return serial;
	}
	/**
	 * 设置消息流水号
	 * @param serial 消息流水号 
	 */
	public void setSerial(int serial) {
		this.serial = serial;
	}
	/**
	 * @return 获取 消息体长度
	 */
	public int getLength() {
		return length;
	}
	/**
	 * 设置消息体长度
	 * @param length 消息体长度 
	 */
	public void setLength(int length) {
		this.length = length;
	}
	
}
