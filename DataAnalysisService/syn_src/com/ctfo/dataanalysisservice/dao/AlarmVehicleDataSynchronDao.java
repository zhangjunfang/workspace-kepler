package com.ctfo.dataanalysisservice.dao;

import java.util.List;

import com.ctfo.dataanalysisservice.beans.AlarmStationBean;
import com.ctfo.dataanalysisservice.beans.AlarmVehicleBean;
import com.ctfo.dataanalysisservice.beans.AreaDataObject;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.SectionsDataObject;

public interface AlarmVehicleDataSynchronDao {

	/**
	 * 获取时间关键点报警车辆的列表
	 * 
	 * @return
	 */
	public List<AlarmStationBean> getAlarmStationVehicleList();

	/**
	 * 获取报警车辆的列表
	 * 
	 * @return
	 */
	public List<AlarmVehicleBean> getAlarmVehicleList();

	/**
	 * 获取线路数据
	 * 
	 * @return 线路list
	 */
	public List<SectionsDataObject> getLineDataObject();

	/**
	 * 获取关键点数据
	 * 
	 * @return 返回关键点list
	 */
	public List<KeyPointDataObject> getKeyPointDataObject();

	/**
	 * 获取围栏数据
	 * 
	 * @return
	 */
	public List<AreaDataObject> getAreaDataObject();

	/**
	 * 获路段数据
	 * 
	 * @return
	 */
	public List<SectionsDataObject> getSectionDataObject();
}
