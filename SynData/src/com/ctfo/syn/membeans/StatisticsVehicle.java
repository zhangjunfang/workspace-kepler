package com.ctfo.syn.membeans;

import java.io.Serializable;

@SuppressWarnings("serial")
public class StatisticsVehicle implements Serializable {

	private Long entId;

	/** 运营状态统计 */
	private Long statisticVehicle;

	public Long getEntId() {
		return entId;
	}

	public void setEntId(Long entId) {
		this.entId = entId;
	}

	public Long getStatisticVehicle() {
		return statisticVehicle;
	}

	public void setStatisticVehicle(Long statisticVehicle) {
		this.statisticVehicle = statisticVehicle;
	}

}
