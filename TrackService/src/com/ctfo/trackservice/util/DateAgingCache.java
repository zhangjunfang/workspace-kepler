package com.ctfo.trackservice.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class DateAgingCache {
	
	/** 车辆时效缓存表   */
	public static Map<String, Long> vehicleAgingMap = new ConcurrentHashMap<String, Long>();
	/**
	 * 获取上次时间
	 * @param vid
	 * @return
	 */
	public static Long getLastTime(String vid) {
		if(vehicleAgingMap.containsKey(vid)){
			return vehicleAgingMap.get(vid);
		} else {
			return null;
		}
	}
	/**
	 * 缓存当前时间
	 * @param vid
	 * @param currentTime
	 */
	public static void setCurrentTime(String vid, long currentTime){
		vehicleAgingMap.put(vid, currentTime);
	}
	
	/**
	 * 清空缓存
	 */
	public static void clear(){
		vehicleAgingMap.clear();
	}
}
