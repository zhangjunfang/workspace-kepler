/**
 * 
 */
package com.ctfo.storage.statistics.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.ctfo.storage.statistics.model.Vehicle;


/**
 * @author zjhl
 *
 */
public class Cache {
	private static Map<String, Vehicle> vehicleMap = new ConcurrentHashMap<String, Vehicle>();
	
	public static Vehicle getVehicle(String phoneNumber){
		return vehicleMap.get(phoneNumber);
	}
	public static void setAllVehicle(Map<String, Vehicle> map){
		vehicleMap.putAll(map); 
	}
	

}
