package com.ctfo.analy.beans;

import java.util.ArrayList;
import java.util.List;

/**
 * 车辆限速比例设置对象
 * @author yujch
 *
 */
public class OverspeedAlarmCfgBean {
	private String vid;//车辆ID
	private String entId;//所属企业
	private String startTime;//非法运营开始时间
	private String endTime;// 非法运营结束时间
	private Long speedScale; //限速比例
	
	private List<AlarmTacticsSetBean> setList = new ArrayList<AlarmTacticsSetBean>();

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public String getStartTime() {
		return startTime;
	}

	public void setStartTime(String startTime) {
		this.startTime = startTime;
	}

	public String getEndTime() {
		return endTime;
	}

	public void setEndTime(String endTime) {
		this.endTime = endTime;
	}

	public Long getSpeedScale() {
		return speedScale;
	}

	public void setSpeedScale(Long speedScale) {
		this.speedScale = speedScale;
	}

	public List<AlarmTacticsSetBean> getSetList() {
		return setList;
	}

	public void setSetList(List<AlarmTacticsSetBean> setList) {
		this.setList = setList;
	}

}
