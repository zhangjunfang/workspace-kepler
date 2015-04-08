package com.ctfo.mileageservice.core;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mileageservice.dao.OracleConnectionPool;
import com.ctfo.mileageservice.service.OracleService;
import com.ctfo.mileageservice.task.TaskAdapter;
import com.ctfo.mileageservice.task.TaskConfiger;
import com.ctfo.mileageservice.util.ConfigLoader;
import com.ctfo.mileageservice.util.Utils;


/**
 * 文件名：MileageServiceMain.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-10-13下午6:22:06
 * 
 */
public class MileageServiceMain {
	private static final Logger logger = LoggerFactory.getLogger(MileageServiceMain.class);
	
	/** 定时任务执行列表	 */
	public static ScheduledExecutorService service = null;
	
	public static void main(String[] args) {

		try{
			logger.info("MileageService-[{}]启动  ......",args[2]);
//			1. 加载配置文件
			ConfigLoader.init(args);
			
//			2. 初始化连接池 
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			OracleService.init();
//			4. 启动定时任务
			service = Executors.newScheduledThreadPool(ConfigLoader.tasks.size());
			for (TaskConfiger tc : ConfigLoader.tasks) {
				Class<?> taskClass = Class.forName(tc.getImpClass());
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setType(args[2]);
				task.setThreadNum(tc.getThreadNum());
				task.setName(tc.getName());
				task.setConfig(tc.getConfig());
				task.init();
				long delay = Long.parseLong(tc.getDelay());
				String period = tc.getPeriod();
				service.scheduleAtFixedRate(task, delay, Long.parseLong(period), tc.getUnit());
			}
				
		}catch(Exception e){
			logger.error("-MileageServiceMain--(error)-应用程序启动异常 !"+ e.getMessage());
			System.exit(0);
		}

	}
}
