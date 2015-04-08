/**
 * 
 */
package com.ctfo.commandservice.model;

/**
 * @author zjhl
 *
 */
public class CustomIssued {
	/**	编号	*/
	private String id;
	/**	序列号	*/
	private String seq;
	/**	车辆编号	*/
	private String vid;
	/**	状态	*/
	private int status;
	/**	创建人编号	*/
	private String createId;
	/**	创建时间	*/
	private long createUtc;
	/**
	 * @return the 编号
	 */
	public String getId() {
		return id;
	}
	/**
	 * 设置编号的值
	 * @param id 编号  
	 */
	public void setId(String id) {
		this.id = id;
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
	/**
	 * @return the 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * @return the 状态(-1:等待回应，0:发送成功，2:发送失败，4:车辆不在线)
	 */
	public int getStatus() {
		return status;
	}
	/**
	 * 设置状态的值(-1:等待回应，0:发送成功，2:发送失败，4:车辆不在线)
	 * @param statue 状态  
	 */
	public void setStatus(int status) {
		this.status = status;
	}
	/**
	 * @return the 创建人编号
	 */
	public String getCreateId() {
		return createId;
	}
	/**
	 * 设置创建人编号的值
	 * @param createId 创建人编号  
	 */
	public void setCreateId(String createId) {
		this.createId = createId;
	}
	/**
	 * @return the 创建时间
	 */
	public long getCreateUtc() {
		return createUtc;
	}
	/**
	 * 设置创建时间的值
	 * @param createUtc 创建时间  
	 */
	public void setCreateUtc(long createUtc) {
		this.createUtc = createUtc;
	}
	
}
