/**
 * 2014-6-4LogoutModel.java
 */
package com.ctfo.storage.command.model;

/**
 * LogoutModel
 * 注销
 * 
 * @author huangjincheng
 * 2014-6-4上午11:00:44
 * 
 */
public class LogoutModel {
	/** 车辆编号*/
	private String vid ;
	
	/** 注销结果-1 失败，0成功*/
	private String result = "0" ;
	
	/** SEQ标识*/
	private String seq ;
	
	/** 记录时间*/
	private long utc ;
	
	/** 响应时间*/
	private String resultUtc ;
	
	/** 硬件识别码*/
	private String oemCode ;

	/**
	 * 获取车辆编号的值
	 * @return vid  
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置车辆编号的值
	 * @param vid
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获取注销结果-1失败，0成功的值
	 * @return result  
	 */
	public String getResult() {
		return result;
	}

	/**
	 * 设置注销结果-1失败，0成功的值
	 * @param result
	 */
	public void setResult(String result) {
		this.result = result;
	}

	/**
	 * 获取SEQ标识的值
	 * @return seq  
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * 设置SEQ标识的值
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
	 * @param utc
	 */
	public void setUtc(long utc) {
		this.utc = utc;
	}

	/**
	 * 获取响应时间的值
	 * @return resultUtc  
	 */
	public String getResultUtc() {
		return resultUtc;
	}

	/**
	 * 设置响应时间的值
	 * @param resultUtc
	 */
	public void setResultUtc(String resultUtc) {
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
