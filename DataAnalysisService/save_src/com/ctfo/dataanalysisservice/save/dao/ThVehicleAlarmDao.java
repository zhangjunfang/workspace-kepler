package com.ctfo.dataanalysisservice.save.dao;

import java.util.List;

import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;

public interface ThVehicleAlarmDao {
	public boolean save(List<ThVehicleAlarm> alarm);

	public boolean update(List<ThVehicleAlarm> alarm);
}
