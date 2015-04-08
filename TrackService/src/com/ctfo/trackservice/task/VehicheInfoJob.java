package com.ctfo.trackservice.task;

import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.service.OracleService;


/*****************************************
 * <li>描        述：车辆信息缓存任务		
 * 
 *****************************************/
public class VehicheInfoJob implements Job {
	private static final Logger logger = LoggerFactory.getLogger(VehicheInfoJob.class);
	/**	最近增量同步时间	*/
	private static long lastTime = System.currentTimeMillis();
	/**	最近全量同步时间	*/
	private static long lastAllSyncTime = System.currentTimeMillis();
	/**	最近全量同步时间 - 默认5分钟	*/
	private static long allSyncInterval = 5 * 60 * 1000;
	/**	同步偏移时间 - 默认1分钟	*/
	private static long offset = 1 * 60 * 1000;
	
	public VehicheInfoJob() {
		try {
			long all = Long.parseLong(ConfigLoader.config.get("vehicheInfoAll"));
			long offsets = Long.parseLong(ConfigLoader.config.get("vehicheInfoOffset"));
			allSyncInterval = all * 60 * 1000;
			offset = offsets * 60 * 1000;
			logger.info("车辆信息缓存任务启动完成 - 全量间隔[{}]分钟, 偏移[{}]分钟", all, offsets);
		} catch (Exception e) {
			logger.error("车辆信息缓存任务启动异常:" + e.getMessage(), e);
		}
	}

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		try {
			long currentTime = System.currentTimeMillis();
			if(currentTime - lastAllSyncTime > allSyncInterval){
//			全量同步
				OracleService.vehicleInfoUpdate(0, true);
				lastAllSyncTime = currentTime;
				lastTime = currentTime - offset;
			} else {
//			增量同步
				OracleService.vehicleInfoUpdate(lastTime, false);
				lastTime = currentTime - offset;
			}
		} catch (Exception e) {
			logger.error("车辆信息同步任务执行异常:" + e.getMessage(), e);
		}
	}
}
