package com.ctfo.datatransferserver;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.datatransferserver.beans.ServiceUnitBean;
import com.ctfo.datatransferserver.beans.VehiclePolymerizeBean;

/**
 * 车辆数据缓存
 * 
 * @author yangyi
 * 
 */
public class DataPool {
	// 车辆缓存
	private static Map<String, ServiceUnitBean> vehicleMap = new ConcurrentHashMap<String, ServiceUnitBean>();

	// 车辆增量变化缓存
	private static Map<String, VehiclePolymerizeBean> tempVehicleMap = new ConcurrentHashMap<String, VehiclePolymerizeBean>();
	
	// 车辆下线化缓存
	private static Map<String, ServiceUnitBean> tempOfflineVehicleMap = new ConcurrentHashMap<String, ServiceUnitBean>();

	public static Map<String, ServiceUnitBean> getVehicleMap() {
		return vehicleMap;
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
	public static ServiceUnitBean getVehicleMapValue(String key) {
		return vehicleMap.get(key);
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setVehicleMapValue(String key, ServiceUnitBean value) {
		vehicleMap.put(key, value);
	}

	/**
	 * 获取接收队列大小
	 */
	public static long getTempVehicleMapSize() {
		return tempVehicleMap.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static VehiclePolymerizeBean getTempVehicleMapValue(String key) {
		return tempVehicleMap.get(key);
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static Map<String, VehiclePolymerizeBean> getTempVehicleMap() {
		Map<String, VehiclePolymerizeBean> map;
		synchronized (tempVehicleMap) {
			map = new ConcurrentHashMap<String, VehiclePolymerizeBean>(
					tempVehicleMap);
			tempVehicleMap.clear();
		}
		return map;
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setTempVehicleMapValue(String key,
			VehiclePolymerizeBean value) {
		tempVehicleMap.put(key, value);
	}
	
	
	public static Map<String, ServiceUnitBean> getAllTempOfflineVehicleMap() {
		return tempOfflineVehicleMap;
	}

	public static ServiceUnitBean getTempOfflineVehicleMap(String key) {
		return tempOfflineVehicleMap.get(key);
	}

	public static void setTempOfflineVehicleMap( String key, ServiceUnitBean  serviceUnitBean) {
		tempOfflineVehicleMap.put(key,serviceUnitBean);
	}
	
	public static void clearTempOfflineVehicleMap() {
		tempOfflineVehicleMap.clear();
	}
}
