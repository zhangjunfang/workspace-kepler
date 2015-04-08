/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.task EntAlarmSettingSyncTask.java	</li><br>
 * <li>时        间：2013-9-27  下午2:30:46	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.task;

import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;


/*****************************************
 * <li>描        述:告警设置同步任务		
 * 
 *****************************************/
public class AlarmSettingsJob implements Job {
	private static final Logger logger = LoggerFactory.getLogger(AlarmSettingsJob.class);
	
	public AlarmSettingsJob() {
	}

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		try {
			OracleService.updateVehicleAlarmSetting();
		} catch (Exception e) {
			logger.error("告警设置同步任务执行异常:" + e.getMessage(), e);
		}
	}
	
}
