package com.ctfo.trackservice.task;

import java.util.concurrent.Callable;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;

public class InitCacheTask implements Callable<Integer> {
	private static Logger log = LoggerFactory.getLogger(InitCacheTask.class);
	/**	方法	*/
	private int method;
	
	public InitCacheTask(int methodNum){
		method = methodNum;
	}
	@Override
	public Integer call() throws Exception {
		switch(method){
			case 0 :
				OracleService.updateVehicleAlarmSetting(); // 缓存车辆报警设置
				break;
			case 1 :  // 缓存父级组织编号
				OracleService.orgParentSync();  
				break;
			case 2 :  // 缓存所有车辆状态
				OracleService.cacheAllVehicleStatus();
				break;
			case 3 :  // 缓存所以车辆信息
				OracleService.vehicleInfoUpdate(0, true);
				break;
			case 4 :  // 缓存线路站点绑定信息
				OracleService.queryLineStationBind();
				break;
			default :
				log.error("InitCacheTask - 初始化缓存任务异常!");
				break;
		} 
		return method;
	}

}
