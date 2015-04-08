/**
 * 
 */
package com.ctfo.storage.process.util;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.service.MySqlService;

/**
 * 车辆缓存同步任务
 *
 */
public class VehicleCacheSyncTask implements Runnable {
	private static Logger log = LoggerFactory.getLogger(VehicleCacheSyncTask.class);
	
	private MySqlService mySqlService;
	
	public VehicleCacheSyncTask(String sql){
		mySqlService = new MySqlService();
		mySqlService.setSql_initVehicleCache(sql); 
	}
	
	public void run() {
		try {
			mySqlService.initVehicleCache();
		} catch (Exception e) {
			log.error("车辆缓存同步任务执行异常:" + e.getMessage(), e);
		}

	}

}
