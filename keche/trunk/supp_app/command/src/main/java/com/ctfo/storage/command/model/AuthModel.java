/**
 * 2014-6-4AuthModel.java
 */
package com.ctfo.storage.command.model;

/**
 * AuthModel
 * 鉴权
 * 
 * @author huangjincheng
 * 2014-6-4上午11:00:02
 * 
 */
public class AuthModel {
	/** 手机号*/
	private String commaddr ;
	
	/** 鉴权码*/
	private String  akey ;
		
	/** 鉴权结果-1失败 0 成功*/
	private String result ;
	
	/** 鉴权序列号*/
	private String seq ;
	
	/** 记录时间*/
	private long utc ;
	
	/** 响应时间*/
	private long resultUtc ;
	
	/** 硬件识别码*/
	private String oemCode ;

	/**
	 * 获取手机号的值
	 * @return commaddr  
	 */
	public String getCommaddr() {
		return commaddr;
	}

	/**
	 * 设置手机号的值
	 * @param commaddr
	 */
	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	/**
	 * 获取鉴权码的值
	 * @return akey  
	 */
	public String getAkey() {
		return akey;
	}

	/**
	 * 设置鉴权码的值
	 * @param akey
	 */
	public void setAkey(String akey) {
		this.akey = akey;
	}

	/**
	 * 获取鉴权结果-1失败0成功的值
	 * @return result  
	 */
	public String getResult() {
		return result;
	}

	/**
	 * 设置鉴权结果-1失败0成功的值
	 * @param result
	 */
	public void setResult(String result) {
		this.result = result;
	}

	/**
	 * 获取鉴权序列号的值
	 * @return seq  
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * 设置鉴权序列号的值
	 * @param seq
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

	/**
	 * 获取记录时间的值
	 * @return utc  
	 */
	public long getUtc() {
		return utc;
	}

	/**
	 * 设置记录时间的值
	 * @param l
	 */
	public void setUtc(long l) {
		this.utc = l;
	}

	/**
	 * 获取响应时间的值
	 * @return resultUtc  
	 */
	public long getResultUtc() {
		return resultUtc;
	}

	/**
	 * 设置响应时间的值
	 * @param resultUtc
	 */
	public void setResultUtc(long resultUtc) {
		this.resultUtc = resultUtc;
	}

	/**
	 * 获取硬件识别码的值
	 * @return oemCode  
	 */
	public String getOemCode() {
		return oemCode;
	}

	/**
	 * 设置硬件识别码的值
	 * @param oemCode
	 */
	public void setOemCode(String oemCode) {
		this.oemCode = oemCode;
	}
	
	
	
}
