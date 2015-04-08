package com.ctfo.trackservice.task;

import java.util.Map;
import java.util.concurrent.Callable;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.model.FutureMapResult;
import com.ctfo.trackservice.service.OracleService;
/**
 *	车辆缓存同步线程
 *
 */
public class VehicleCacheTask implements Callable<FutureMapResult> {
	private static Logger log = LoggerFactory.getLogger(VehicleCacheTask.class);
	/**	方法	*/
	private int method;
	/**	最近查询时间	*/
	private long lastTime;
	/**	全量查询标记	*/
	private boolean allFlag;
	
	
	public VehicleCacheTask(int methodNum,long time, boolean flag){
		method = methodNum;
		lastTime = time;
		allFlag = flag;
	}
	@Override
	public FutureMapResult call() throws Exception {
		FutureMapResult result = new FutureMapResult();
		Map<String, ?> map = null;
		switch(method){
			case 0 :
				map = OracleService.vehicle3GInfoUpdate(lastTime, allFlag);
				result.setName("Vehicle3GInfo"); 
				result.setValue(map);
				break;
			case 1 :
				map = OracleService.updateVehicleInfo(lastTime, allFlag);
				result.setName("VehicleInfo"); 
				result.setValue(map);
				break;
			default :
				log.error("VehicleCacheTask - 初始化缓存任务异常!");
				break;
		} 
		return result;
	}
	
	@Override
	public String toString() {
		return String.valueOf(method); 
	}

}
