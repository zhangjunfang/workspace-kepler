package com.ctfo.portalmng.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 路况信息
 * 
 * @author xuehui
 * 
 */
@SuppressWarnings("serial")
public class RoadCondition extends ETB_Base {

	/**
	 * 省、直辖市编码
	 */
	private Long provinceCode;

	/**
	 * 时间
	 */
	private String roadConditionTime;

	/**
	 * 事件描述
	 */
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
