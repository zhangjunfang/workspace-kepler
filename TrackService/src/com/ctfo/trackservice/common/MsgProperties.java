package com.ctfo.trackservice.common;

public class MsgProperties {
	/**	编码格式	*/
	private String msgEncoding;
	/**	登录MSG用户名	*/
	private String msgUserName;
	/**	登录MSG密码	*/
	private String msgPassword;
	/**	MSG组编号	*/
	private String msgGroupId;
	/**	MSG组	*/
	private String msgGroup;
	/**	MSG服务器IP地址	*/
	private String msgHost;
	/**	MSG服务器端口	*/
	private int msgPort;
	/**	MSG服务器登录类型	*/
	private String loginType;
	/**	重连间隔	*/
	private long reConnectTime;
	/**	读空闲时间	*/
	private int readerIdle;
	/**	写空闲时间	*/
	private int writerIdle;
	/**	接收缓冲区的大小	*/
	private int receiveBufferSize;
	/**	发送缓冲区的大小	*/
	private int sendBufferSize;
	
	/**
	 * @return the 编码格式
	 */
	public String getMsgEncoding() {
		return msgEncoding;
	}
	/**
	 * @param 编码格式 the msgEncoding to set
	 */
	public void setMsgEncoding(String msgEncoding) {
		this.msgEncoding = msgEncoding;
	}
	/**
	 * 获得登录MSG用户名的值
	 * @return the msgUserName 登录MSG用户名  
	 */
	public String getMsgUserName() {
		return msgUserName;
	}
	/**
	 * 设置登录MSG用户名的值
	 * @param msgUserName 登录MSG用户名  
	 */
	public void setMsgUserName(String msgUserName) {
		this.msgUserName = msgUserName;
	}
	/**
	 * 获得登录MSG密码的值
	 * @return the msgPassword 登录MSG密码  
	 */
	public String getMsgPassword() {
		return msgPassword;
	}
	/**
	 * 设置登录MSG密码的值
	 * @param msgPassword 登录MSG密码  
	 */
	public void setMsgPassword(String msgPassword) {
		this.msgPassword = msgPassword;
	}
	/**
	 * 获得MSG组编号的值
	 * @return the msgGroupId MSG组编号  
	 */
	public String getMsgGroupId() {
		return msgGroupId;
	}
	/**
	 * 设置MSG组编号的值
	 * @param msgGroupId MSG组编号  
	 */
	public void setMsgGroupId(String msgGroupId) {
		this.msgGroupId = msgGroupId;
	}
	/**
	 * 获得MSG组的值
	 * @return the msgGroup MSG组  
	 */
	public String getMsgGroup() {
		return msgGroup;
	}
	/**
	 * 设置MSG组的值
	 * @param msgGroup MSG组  
	 */
	public void setMsgGroup(String msgGroup) {
		this.msgGroup = msgGroup;
	}
	/**
	 * 获得MSG服务器IP地址的值
	 * @return the msgHost MSG服务器IP地址  
	 */
	public String getMsgHost() {
		return msgHost;
	}
	/**
	 * 设置MSG服务器IP地址的值
	 * @param msgHost MSG服务器IP地址  
	 */
	public void setMsgHost(String msgHost) {
		this.msgHost = msgHost;
	}
	/**
	 * 获得MSG服务器端口的值
	 * @return the msgPort MSG服务器端口  
	 */
	public int getMsgPort() {
		return msgPort;
	}
	/**
	 * 设置MSG服务器端口的值
	 * @param msgPort MSG服务器端口  
	 */
	public void setMsgPort(int msgPort) {
		this.msgPort = msgPort;
	}
	/**
	 * 获得MSG服务器登录类型的值
	 * @return the loginType MSG服务器登录类型  
	 */
	public String getLoginType() {
		return loginType;
	}
	/**
	 * 设置MSG服务器登录类型的值
	 * @param loginType MSG服务器登录类型  
	 */
	public void setLoginType(String loginType) {
		this.loginType = loginType;
	}
	/**
	 * 获得重连间隔的值
	 * @return the reConnectTime 重连间隔  
	 */
	public long getReConnectTime() {
		return reConnectTime;
	}
	/**
	 * 设置重连间隔的值
	 * @param reConnectTime 重连间隔  
	 */
	public void setReConnectTime(long reConnectTime) {
		this.reConnectTime = reConnectTime;
	}
	/**
	 * 获得读空闲时间的值
	 * @return the readerIdle 读空闲时间  
	 */
	public int getReaderIdle() {
		return readerIdle;
	}
	/**
	 * 设置读空闲时间的值
	 * @param readerIdle 读空闲时间  
	 */
	public void setReaderIdle(int readerIdle) {
		this.readerIdle = readerIdle;
	}
	/**
	 * 获得写空闲时间的值
	 * @return the writerIdle 写空闲时间  
	 */
	public int getWriterIdle() {
		return writerIdle;
	}
	/**
	 * 设置写空闲时间的值
	 * @param writerIdle 写空闲时间  
	 */
	public void setWriterIdle(int writerIdle) {
		this.writerIdle = writerIdle;
	}
	/**
	 * @return the 接收缓冲区的大小
	 */
	public int getReceiveBufferSize() {
		return receiveBufferSize;
	}
	/**
	 * @param 接收缓冲区的大小 the receiveBufferSize to set
	 */
	public void setReceiveBufferSize(int receiveBufferSize) {
		this.receiveBufferSize = receiveBufferSize;
	}
	/**
	 * @return the 发送缓冲区的大小
	 */
	public int getSendBufferSize() {
		return sendBufferSize;
	}
	/**
	 * @param 发送缓冲区的大小 the sendBufferSize to set
	 */
	public void setSendBufferSize(int sendBufferSize) {
		this.sendBufferSize = sendBufferSize;
	}
	
}
