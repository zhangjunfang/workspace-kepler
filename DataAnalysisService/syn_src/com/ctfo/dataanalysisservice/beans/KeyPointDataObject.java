package com.ctfo.dataanalysisservice.beans;

public class KeyPointDataObject extends BaseDataObject {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1L;
	/**
	 * 关键点ID
	 */
	private String keyPointId;
	/**
	 * 关键点范围
	 */
	private String keyPointArea;
	/**
	 * 到达时间
	 */
	private String reachTime;
	/**
	 * 离开时间
	 */
	private String leaveTime;
	
	// 是否开始判断
	private boolean isStart = false; 

	/**
	 * @return the keyPointId
	 */
	public String getKeyPointId() {
		return keyPointId;
	}

	/**
	 * @param keyPointId
	 *            the keyPointId to set
	 */
	public void setKeyPointId(String keyPointId) {
		this.keyPointId = keyPointId;
	}

	/**
	 * @return the keyPointArea
	 */
	public String getKeyPointArea() {
		return keyPointArea;
	}

	/**
	 * @param keyPointArea
	 *            the keyPointArea to set
	 */
	public void setKeyPointArea(String keyPointArea) {
		this.keyPointArea = keyPointArea;
	}

	/**
	 * @return the reachTime
	 */
	public String getReachTime() {
		return reachTime;
	}

	/**
	 * @param reachTime
	 *            the reachTime to set
	 */
	public void setReachTime(String reachTime) {
		this.reachTime = reachTime;
	}

	/**
	 * @return the leaveTime
	 */
	public String getLeaveTime() {
		return leaveTime;
	}

	/**
	 * @param leaveTime
	 *            the leaveTime to set
	 */
	public void setLeaveTime(String leaveTime) {
		this.leaveTime = leaveTime;
	}

	public boolean isStart() {
		return isStart;
	}

	public void setStart(boolean isStart) {
		this.isStart = isStart;
	}

}
