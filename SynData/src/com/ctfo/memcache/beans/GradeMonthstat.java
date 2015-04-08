package com.ctfo.memcache.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 首页车辆车队排行榜
 */
@SuppressWarnings({ "serial", "unchecked" })
public class GradeMonthstat extends ETB_Base implements Comparable {

	/** 序号 */
	private Integer seqId;

	/** 车辆id */
	private String vid;

	/** 车牌号 */
	private String vehicleNo;

	/** 车队id */
	private Long teamId;

	/** 车队名称 */
	private String teamName;

	/** 总数 */
	private Long allScoreSum;

	/** 上升:1 持平:0 下降:-1 */
	private int sign;

	/**
	 * compareTo
	 */
	public int compareTo(Object object) {
		return this.seqId - ((GradeMonthstat) object).getSeqId();
	}

	public Integer getSeqId() {
		return seqId;
	}

	public void setSeqId(Integer seqId) {
		this.seqId = seqId;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public Long getTeamId() {
		return teamId;
	}

	public void setTeamId(Long teamId) {
		this.teamId = teamId;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	public Long getAllScoreSum() {
		return allScoreSum;
	}

	public void setAllScoreSum(Long allScoreSum) {
		this.allScoreSum = allScoreSum;
	}

	public int getSign() {
		return sign;
	}

	public void setSign(int sign) {
		this.sign = sign;
	}

}
