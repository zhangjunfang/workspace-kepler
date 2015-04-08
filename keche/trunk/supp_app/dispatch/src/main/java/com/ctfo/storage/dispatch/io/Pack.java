/**
 * 
 */
package com.ctfo.storage.dispatch.io;

/**
 * @author zjhl
 *
 */
public class Pack {
	/**	消息来源	*/
	private int source;
	/**	消息目的地	*/
	private int destination;
	/**	消息类型	*/
	private short type;
	/**	消息流水号	*/
	private int serial;
	/**	消息体长度	*/
	private int length;
	/**	包体数据	*/
	private byte[] data;
	/**	验证码	*/
	private byte verifyCode;
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
	public short getType() {
		return type;
	}
	/**
	 * 设置消息类型
	 * @param type 消息类型 
	 */
	public void setType(short type) {
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
	/**
	 * @return 获取 包体数据
	 */
	public byte[] getData() {
		return data;
	}
	/**
	 * 设置包体数据
	 * @param data 包体数据 
	 */
	public void setData(byte[] data) {
		this.data = data;
	}
	/**
	 * @return 获取 验证码
	 */
	public byte getVerifyCode() {
		return verifyCode;
	}
	/**
	 * 设置验证码
	 * @param verifyCode 验证码 
	 */
	public void setVerifyCode(byte verifyCode) {
		this.verifyCode = verifyCode;
	}
	
}
