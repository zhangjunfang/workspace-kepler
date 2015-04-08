package com.ctfo.trackservice.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.task.TaskAdapter;
import com.ctfo.trackservice.task.TaskConfiger;
import com.ctfo.trackservice.util.ConfigLoader;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;
import com.ctfo.trackservice.util.Utils;


/**
 * 文件名：TrackServiceMain.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-10-13下午6:22:06
 * 
 */
public class StatusStatistics {
	private static final Logger logger = LoggerFactory.getLogger(StatusStatistics.class);
	
	/** 定时任务执行列表	 */
	public static ScheduledExecutorService service = null;
	
	private static OracleService oracleService = new OracleService();
	public static boolean result = false;

	//private static boolean flag = true; 
	//private static boolean flag_bak = true; 
	public static void main(String[] args) {

		try{
			logger.info("StatusStatistics-[{}]启动  ......",args[2]);
//			1. 加载配置文件
			ConfigLoader.init(args);
//          2.任务图 			
			Tools.showPic();
//			3. 初始化连接池 
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
//			4.加载xml的SQL配置
			OracleService.init();

//			5. 启动定时任务
			service = Executors.newScheduledThreadPool(ConfigLoader.tasks.size());
			if("start".equals(args[2])){
				for (TaskConfiger tc : ConfigLoader.tasks) {
					startTask(tc, args);
					result = true;		
				}			
				
			}else if("restore".equals(args[2])){
//				6.加载油箱油量监控车辆列表
				oracleService.loadOilMonitorVehicleListing();
//				7.加载车辆静态信息列表
				oracleService.loadVehicleInfo();
				System.out.print("---------------------输入您需要补跑的的任务序号:");			
				String className= "";
				String code = "";
				while(true){
					BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
					try {
						code = br.readLine();
					} catch (IOException e) {
						logger.error("读取失败！",e);
					}
					if(code.equals("1")){
						className= "VehicleRunningMileageTask";
						break;
					}else if(code.equals("2")){
						className= "VehicleRunningMileageMonthTask";
						break;
					}else if(code.equals("3")){
						className= "VehicleRunningTask";
						break;
					}else if(code.equals("4")){
						className= "VehicleRunningMonthTask";
						break;
					}else if(code.equals("5")){
						className= "OilWearTask";
						break;
					}else if(code.equals("6")){
						className= "VehicleReportTask";
						break;
					}else if(code.equals("7")){
						className = "TransportTask";
						break;
					}else if(code.equals("8")){
						className = "MaintenanceTask";
						break;
					}else if(code.equals("9")){
						className = "GradeStatisticTask";
						break;
					}else if(code.equals("10")){
						className = "AutoExtendAssessSetTask";
						break;
					}
					else {
						System.out.print("---------------------输入错误,请重新选择输入：");
					}								
				}
				for (TaskConfiger tc : ConfigLoader.tasks) {
					if(className.equals(tc.getImpClass().split("\\.")[4])){
						tc.setDelay("0");//补跑数据立即执行
						startTask(tc, args);
						result = true;
						break;
					}					
				}
				if(result == false){
					logger.info("---------------------请检查config.xml文件中该任务enable属性是否为true！");
					System.exit(0);
				}
				
			}else if("autoRestore".equals(args[2])){
				long start = DateTools.getTime(ConfigLoader.config.get("autoStartTime"));
				long end = DateTools.getTime(ConfigLoader.config.get("autoEndTime"));
				String className = ConfigLoader.config.get("autoTask");
				int length = (int) ((end-start)/(24 * 60 * 60 * 1000));
				for(int i = 0; i<= length ;i++){
					if(!"TransportTask".equals(className)){
//						6.加载油箱油量监控车辆列表
						oracleService.loadOilMonitorVehicleListing();
//						7.加载车辆静态信息列表
						oracleService.loadVehicleInfo();
					}
					long utc = start + (24 * 60 * 60 * 1000 * i);
					for (TaskConfiger tc : ConfigLoader.tasks) {
						if(className.equals(tc.getImpClass().split("\\.")[4])){
							autoStartTask(tc, args, utc);
							result = true;	
						}
					}
				}
				
				logger.info("-----------------------自动补跑任务结束！日期:【{}】-->【{}】",DateTools.getStringDate(start),DateTools.getStringDate(end));
				System.exit(0);
			}
				
		}catch(Exception e){
			logger.error("-StatusStatistics--(error)-应用程序启动异常 !",e);
			System.exit(0);
		}

	}
	
	/**
	 * 启动任务
	 * @param tc
	 * @param args
	 * @throws Exception
	 */
	public static void startTask(TaskConfiger tc,String[] args) throws Exception{
		Class<?> taskClass = Class.forName(tc.getImpClass());
		TaskAdapter task = (TaskAdapter) taskClass.newInstance();
		task.setType(args[2]);
		task.setThreadNum(tc.getThreadNum());
		task.setName(tc.getName());
		task.setConfig(tc.getConfig());
		task.init();
		long delay = Long.parseLong(tc.getDelay());
		service.scheduleAtFixedRate(task, delay, 24*3600, TimeUnit.SECONDS);
	}
	
	/**
	 * 启动任务
	 * @param tc
	 * @param args
	 * @throws Exception
	 */
	public static void autoStartTask(TaskConfiger tc,String[] args,long utc) throws Exception{
		Class<?> taskClass = Class.forName(tc.getImpClass());
		TaskAdapter task = (TaskAdapter) taskClass.newInstance();
		task.setType(args[2]);
		task.setThreadNum(tc.getThreadNum());
		task.setName(tc.getName());
		task.setConfig(tc.getConfig());
		task.setUtc(utc);
		task.init();
		task.execute();
		
	}
	
}
