package com.ctfo.savecenter.dao;


import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.savecenter.beans.AlarmTypeBean;
import com.ctfo.savecenter.beans.DriftLatLon;
import com.ctfo.savecenter.beans.FiveMinDataBean;
import com.ctfo.savecenter.beans.ServiceUnit;
import com.ctfo.savecenter.beans.StatusCode;

public class TempMemory {

	/** macid对应车辆最近报警表 */
	public static Map<String, Map<String, String>> vehicleStatusMap = new ConcurrentHashMap<String, Map<String, String>>();

	// 临时漂移数据
	private static Map<Long, DriftLatLon> driftLatLonMap = new ConcurrentHashMap<Long, DriftLatLon>();

	// 车辆缓存
	private static Map<String, ServiceUnit> vehicleMap = new ConcurrentHashMap<String, ServiceUnit>();

	/** 报警代码对应报警描述Map */
	private static Map<Integer, AlarmTypeBean> alarmtypeMap = new ConcurrentHashMap<Integer, AlarmTypeBean>();

	// 存储车辆状态对应参考值
	private static Map<Long, StatusCode> statusMap = new ConcurrentHashMap<Long, StatusCode>();

	// 存储车辆状态对应参考值
	private static Map<String, String> vehicleIsonlineMap = new ConcurrentHashMap<String, String>();

	/** 车辆报警缓存 */  // 存储车辆报警id
	private static Map<String, String> vehicleAlarmMap = new ConcurrentHashMap<String, String>();
	
	//缓存车辆最大转速
	public static Map<String, Long> vehicleMaxRpmMap = new ConcurrentHashMap<String, Long>();
	
	// 缓存五分钟数据
	private static Map<Long,FiveMinDataBean> fiveMap = new ConcurrentHashMap<Long,FiveMinDataBean>();
	
	// 车辆对应报警设置企业列表(key:vid,value:ent_id)
	public static Map<Long,Long> vidEntMap = new ConcurrentHashMap<Long,Long>();
	
	// 企业对应报警设置告警列表(key:vid,value:报警code列表)
	public static Map<Long,List<String>> entAlarmMap = new ConcurrentHashMap<Long,List<String>>();
	/**    队列状态         */
	public static  Map<String,String> queueStatus = new ConcurrentHashMap<String,String>();
	/**    处理状态         */
	public static  Map<String,Integer> processStatus = new ConcurrentHashMap<String,Integer>();
	
	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static String getVehicleAlarmMapValue(String key) {
		return vehicleAlarmMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setVehicleAlarmMapValue(String key, String value) {
		vehicleAlarmMap.put(key, value);
	}
	
	/***
	 * 判断是否包含当前key
	 * @param key
	 * @return
	 */
	public static boolean vehicleAlarmMapContain(String key){
		return vehicleAlarmMap.containsKey(key);
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static String getvehicleIsonlineMapValue(String key) {
		return vehicleIsonlineMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setvehicleIsonlineMapValue(String key, String value) {
		vehicleIsonlineMap.put(key, value);
	}

	/**
	 * 获取接收队列大小
	 */
	public static long getAlarmtypeMapSize() {
		return alarmtypeMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static AlarmTypeBean getAlarmtypeMapValue(int key) {
		return alarmtypeMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setAlarmtypeMapValue(int key, AlarmTypeBean alarmTypeBean) {
		alarmtypeMap.put(key, alarmTypeBean);
	}

	/**
	 * 获取接收队列大小
	 */
	public static long getStatusMapSize() {
		return statusMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static StatusCode getStatusMapValue(long key) {
		return statusMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setStatusMapValue(long key, StatusCode statusCode) {
		statusMap.put(key, statusCode);
	}

	/**
	 * 获取接收队列大小
	 */
	public static long getVehicleMapSize() {
		return vehicleMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static ServiceUnit getVehicleMapValue(String macid) {
		return vehicleMap.get(macid);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setVehicleMapValue(String macid, ServiceUnit serviceUnit) {
		vehicleMap.put(macid, serviceUnit);
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static Map<String, ServiceUnit> getVehicleMap() {
		return vehicleMap;
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static void deleteVehicleMapValue(String macid) {
		vehicleMap.remove(macid);
	}

	public static void initTempMemory() {
	}

	public static void putDriftLatLon(Long vid, DriftLatLon driftLatLon) {
		driftLatLonMap.put(vid, driftLatLon);
	}

	public static DriftLatLon getDriftLatLon(Long vid) {
		DriftLatLon driftLatLon = null;
		if (driftLatLonMap.containsKey(vid)) {
			driftLatLon = driftLatLonMap.get(vid);
		} else {
			driftLatLon = new DriftLatLon();
			driftLatLonMap.put(vid, driftLatLon);
		}
		return driftLatLon;
	}

	public static void removeDriftLatLon(Long vid) {
		if (driftLatLonMap.containsKey(vid)) {
			driftLatLonMap.remove(vid);
		}
	}
	
	// 存储大气压力缓存状态
	public static void addFiveAirPressureStatus(long vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.setAirPressureStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.setAirPressureStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	
	// 获取大气压力缓存状态,无任何缓存，返回无效状态2
	public static int getFiveAirPressureStatus(long vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.getAirPressureStatus();
		}else{
			return 2;
		}
	}
	
	// 存储冷却液温度缓存状态
	public static void addFiveEWaterStatus(long vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.seteWaterStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.seteWaterStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	
	// 获取冷却液温度缓存状态,无任何缓存，返回无效状态2
	public static int getFiveEWaterStatus(long vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.geteWaterStatus();
		}else{
			return 2;
		}
	}
	
	// 存储蓄电池电压缓存状态
	public static void addFiveExtVoltageStatus(long vid,int status){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			fiveMinData.setExtVoltageStatus(status);
		}else{
			fiveMinData = new FiveMinDataBean();
			fiveMinData.setExtVoltageStatus(status);
			fiveMap.put(vid, fiveMinData);
		}
	}
	
	// 获取蓄电池电压缓存状态 ,无任何缓存，返回无效状态2
	public static int getFiveExtVoltageStatus(long vid){
		FiveMinDataBean fiveMinData = fiveMap.get(vid);
		if(fiveMinData != null){
			return fiveMinData.getExtVoltageStatus();
		}else{
			return 2;
		}
	}

	/**
	 * 获取缓存的车辆最大转速
	 * 
	 * @return
	 */
	public static Long getVehicleMaxRpmMapValue(String key) {
		if(vehicleMaxRpmMap== null){
			return null;
		}
		return vehicleMaxRpmMap.get(key);
	}

	/**
	 * 添加车量最大转速缓存
	 * 
	 * @param value
	 */
	public static void setVehicleMaxRpmValue(String key, Long value) {
		vehicleMaxRpmMap.put(key, value);
	}
	
	/**
	 * 移除车辆最大转速缓存
	 * 
	 * @param value
	 */
	public static void removeVehicleMaxRpmValue(String key) {
		vehicleMaxRpmMap.remove(key);
	}
	
	/***
	 * 判断是否包含当前key
	 * @param key
	 * @return
	 */
	public static boolean vehicleMaxRpmMapContain(String key){
		return vehicleMaxRpmMap.containsKey(key);
	}
}
