package com.ctfo.syncservice.model;

import java.io.Serializable;


/**
 * 路况信息
 */
public class RoadCondition implements Serializable {
	private static final long serialVersionUID = 1844541082964460532L;
	/** 省、直辖市编码  */
	private Long provinceCode;
	/**  时间  */
	private String roadConditionTime;
	/**  事件描述   */
	private String descriptions;

	public Long getProvinceCode() {
		return provinceCode;
	}

	public void setProvinceCode(Long provinceCode) {
		this.provinceCode = provinceCode;
	}

	public String getRoadConditionTime() {
		return roadConditionTime;
	}

	public void setRoadConditionTime(String roadConditionTime) {
		this.roadConditionTime = roadConditionTime;
	}

	public String getDescriptions() {
		return descriptions;
	}

	public void setDescriptions(String descriptions) {
		this.descriptions = descriptions;
	}

}
