package com.ctfo.filesaveservice.util;

import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.filesaveservice.model.Driver;
/**
 *	本地驾驶员信息缓存
 *
 */
public class LocalDriverCacle {
	private static LocalDriverCacle instance = new LocalDriverCacle();
	/** 驾驶员缓存表   */
	private static Map<String, Driver> driverMap = new ConcurrentHashMap<String, Driver>(); 
	
	private LocalDriverCacle(){
	}
	/**
	 * 获取缓存实例
	 * @param timeout
	 * @return
	 */
	public static LocalDriverCacle getInstance() {
		if (instance == null) {
			instance = new LocalDriverCacle();
		}
		return instance;
	}
	/**
	 * 获取驾驶员信息
	 * @param vid
	 * @return
	 */
	public Driver getDriverInfo(String vid){
		return driverMap.get(vid); 
	}
	
	/**
	 * 将驾驶员信息加入缓存
	 * @param vid
	 * @param driver2
	 */
	public void put(String vid, Driver driver) {
		if(driver != null){
			driverMap.put(vid, driver);
		}
	}
	/**
	 * 批量缓存驾驶员信息
	 * @param m
	 */
	public void putAll(Map<String, Driver> m){
		driverMap.putAll(m);
	}
	public Set<String> getKeySet() {
		return driverMap.keySet();
	}
	public void remove(String key) {
		driverMap.remove(key);
	}
}
