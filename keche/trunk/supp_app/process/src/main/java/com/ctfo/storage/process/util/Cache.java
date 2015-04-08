/**
 * 
 */
package com.ctfo.storage.process.util;

import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.storage.process.model.AlarmCache;
import com.ctfo.storage.process.model.Vehicle;

/**
 * @author zjhl
 *
 */
public class Cache {
	/**hbase缓冲区缓存 key：tablename  value：缓冲区put数目*/
	private static Map<String, Integer> tableMap = new ConcurrentHashMap<String, Integer>();
	
	/**
	 * 获取tableMap的值
	 * @return tableMap  
	 */
	public static Map<String, Integer> getTableMap() {
		return tableMap;
	}
	/**
	 * 设置tableMap的值
	 * @param tableMap
	 */
	public static void setTableMap(Map<String, Integer> tableMap) {
		Cache.tableMap = tableMap;
	}
	private static Map<String, Vehicle> vehicleMap = new ConcurrentHashMap<String, Vehicle>();
	
	public static Vehicle getVehicle(String phoneNumber){
		return vehicleMap.get(phoneNumber);
	}
	public static void setAllVehicle(Map<String, Vehicle> map){
		vehicleMap.putAll(map); 
	}
	/** 车辆报警缓存 */
	public static Map<String, List<AlarmCache>> alarmCacheMap = new ConcurrentHashMap<String, List<AlarmCache>>();

	/**
	 * 获取vehicleMap的值
	 * @return vehicleMap  
	 */
	public static Map<String, Vehicle> getVehicleMap() {
		return vehicleMap;
	}
	/**
	 * 设置vehicleMap的值
	 * @param vehicleMap
	 */
	public static void setVehicleMap(Map<String, Vehicle> vehicleMap) {
		Cache.vehicleMap = vehicleMap;
	}
	/**
	 * 获取alarmCacheMap的值
	 * @return alarmCacheMap  
	 */
	public static Map<String, List<AlarmCache>> getAlarmCacheMap() {
		return alarmCacheMap;
	}
	/**
	 * 设置alarmCacheMap的值
	 * @param alarmCacheMap
	 */
	public static void setAlarmCacheMap(Map<String, List<AlarmCache>> alarmCacheMap) {
		Cache.alarmCacheMap = alarmCacheMap;
	}
    
	

}
