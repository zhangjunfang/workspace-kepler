package com.ctfo.syncservice.model;

import java.io.Serializable;

@SuppressWarnings("serial")
public class StatisticsVehicle implements Serializable {

	private String entId;

	/** 运营状态统计 */
	private Long statisticVehicle;

	public String getEntId() {
		return entId;
	}

	public void setEntId(String entId) {
		this.entId = entId;
	}

	public Long getStatisticVehicle() {
		return statisticVehicle;
	}

	public void setStatisticVehicle(Long statisticVehicle) {
		this.statisticVehicle = statisticVehicle;
	}

}
