/**
 * 
 */
package com.ctfo.commandservice.model;

/**
 * @author zjhl
 *
 */
public class Custom {
	/**	过期时间	*/
	private long outTime;
	/**	序列号	*/
	private String seq;
	/**
	 * @return the 过期时间
	 */
	public long getOutTime() {
		return outTime;
	}
	/**
	 * 设置过期时间的值
	 * @param outTime 过期时间  
	 */
	public void setOutTime(long outTime) {
		this.outTime = outTime;
	}
	/**
	 * @return the 序列号
	 */
	public String getSeq() {
		return seq;
	}
	/**
	 * 设置序列号的值
	 * @param seq 序列号  
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}
	
}
