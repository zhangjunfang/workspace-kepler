/**
 * 
 */
package com.ctfo.commandservice.model;

/**
 * 自定义上传
 *
 */
public class CustomUpload {
	/**	指令序列号	*/
	private String seq;
	/**	报文类型	*/
	private String type;
	/**	报文内容	*/
	private String content;
	/**	时间	*/
	private long utc;
	/**	指令类型	*/
	private int commandType;
	/**
	 * 
	 */
	public CustomUpload() {
		this.utc = System.currentTimeMillis();
	}
	/**
	 * @return the 指令序列号
	 */
	public String getSeq() {
		return seq;
	}
	/**
	 * 设置指令序列号的值
	 * @param vid 指令序列号
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}
	/**
	 * @return the 报文类型
	 */
	public String getType() {
		return type;
	}
	/**
	 * 设置报文类型的值
	 * @param type 报文类型  
	 */
	public void setType(String type) {
		this.type = type;
	}
	/**
	 * @return the 报文内容
	 */
	public String getContent() {
		return content;
	}
	/**
	 * 设置报文内容的值
	 * @param content 报文内容  
	 */
	public void setContent(String content) {
		this.content = content;
	}
	/**
	 * @return the 时间
	 */
	public long getUtc() {
		return utc;
	}
	/**
	 * 设置时间的值
	 * @param utc 时间  
	 */
	public void setUtc(long utc) {
		this.utc = utc;
	}
	/**
	 * @return the 指令类型
	 */
	public int getCommandType() {
		return commandType;
	}
	/**
	 * 设置指令类型的值
	 * @param commandType 指令类型  
	 */
	public void setCommandType(int commandType) {
		this.commandType = commandType;
	}
	
}
