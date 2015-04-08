package com.ctfo.trackservice.task;

import java.util.Map;
import java.util.concurrent.Callable;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.model.FutureMapResult;
import com.ctfo.trackservice.service.OracleService;
/**
 * 告警设置同步线程
 *
 */
public class AlarmSettingsTask implements Callable<FutureMapResult> {
	private static Logger log = LoggerFactory.getLogger(AlarmSettingsTask.class);
	/**	方法	*/
	private int methodId;
	
	public AlarmSettingsTask(int method){
		methodId = method;
	}
	@Override
	public FutureMapResult call() throws Exception {
		FutureMapResult result = new FutureMapResult();
		Map<String, String> map = null;
		switch(methodId){
			case 0 :  // 告警设置车辆查询
				map = OracleService.alarmSettingVehicleQuery();
				result.setValue(map);
				result.setName("AlarmSettingVehicle"); 
				break;
			case 1 :  // 告警设置企业查询
				map = OracleService.alarmSettingEntQuery();
				result.setValue(map);
				result.setName("AlarmSettingEnt"); 
				break;
			default :
				log.error("AlarmSettingsTask - 初始化缓存任务异常!");
				break;
		}  
		
		return result;
	}

}
