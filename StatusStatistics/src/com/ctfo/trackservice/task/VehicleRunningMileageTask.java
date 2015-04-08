package com.ctfo.trackservice.task;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.io.LoadFile;
import com.ctfo.trackservice.parse.AbstractThread;
import com.ctfo.trackservice.parse.DriverMileageUpdateThread;
import com.ctfo.trackservice.parse.MileageAnalyThread;
import com.ctfo.trackservice.parse.VehicleMileageUpdateThread;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;




/**
 * 文件名：VehicleRunningMileageTask.java
 * 功能：行驶里程统计
 *
 * @author huangjincheng
 * 2014-10-15上午9:24:31
 * 
 */
public class VehicleRunningMileageTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(VehicleRunningMileageTask.class);
	/**轨迹文件路径*/
	private String filePath ="";
	
	/**油量文件路径*/
	private String oilPath ="";
	
	/**事件文件路径*/
	private String eventPath ="";
	
	/**当日12点utc*/
	//private Long utc = -1l; 
	
	private Boolean flag ;
	
	private OracleService oracleService = new OracleService();
	/**是否更新总里程,默认更新*/
	private Boolean isUpdateMileage  = true;
	@Override          
	public void init() {
		this.filePath = config.get("trackFilePath");
		this.oilPath = config.get("oilFilePath");
		this.eventPath = config.get("eventFilePath");
	}

	@Override
	public void execute() throws Exception {		
		if("restore".equals(this.type)){
			isUpdateMileage = false;//补跑行驶里程不更新总里程表
			String restoreTime = "";
			System.out.print("--------------------【行驶里程日统计】输入您需要补跑的日期(yyyy/mm/dd)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.print("--------------------【行驶里程日统计】输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd hh/mm/ss");
			Date date = sdf.parse(restoreTime+" 00/00/00");
			utc = date.getTime();
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteRestoreInfo(utc+12*60*60*1000);
			//OilWearTask中的加载油量文件集合，为使计算传感器油耗
			Vector<File> fileList = LoadFile.findFile(oilPath, utc);		
			logger.info("--------------------油量文件加载完成！文件数：[{}]",fileList.size());
			ThreadPool.oilFileMap.clear();
			for(int e=0;e<fileList.size();e++){
				//设置油耗文件缓存，为查询传感器油耗准备		
				ThreadPool.oilFileMap.put(fileList.get(e).getName().replaceAll("\\.txt", ""), fileList.get(e));
			}
			oracleService.loadingOilMap(utc);
		}else if("start".equals(this.type)){
			utc = DateTools.getYesDayYearMonthDay();
			//设置false状态，保持下一场继续依赖执行
			ThreadPool.isReportOver = false;
			isTimeRun();
			
		}else if("autoRestore".equals(this.type)){
			logger.info("--------------------【行驶里程日统计】自动补跑启动,时间:{}",DateTools.getStringDate(utc));
			isUpdateMileage = false;//补跑行驶里程不更新总里程表
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteRestoreInfo(utc+12*60*60*1000);
			//OilWearTask中的加载油量文件集合，为使计算传感器油耗
			Vector<File> fileList = LoadFile.findFile(oilPath, utc);		
			logger.info("--------------------油量文件加载完成！文件数：[{}]",fileList.size());
			ThreadPool.oilFileMap.clear();
			for(int e=0;e<fileList.size();e++){
				//设置油耗文件缓存，为查询传感器油耗准备		
				ThreadPool.oilFileMap.put(fileList.get(e).getName().replaceAll("\\.txt", ""), fileList.get(e));
			}
			oracleService.loadingOilMap(utc);
		}
		
		
		logger.info("--------------------【行驶里程日统计】统计开始!-------------------------");
		long startTime = System.currentTimeMillis();
		flag = true;
		//ThreadPool.init();
		for(int i = 0;i<this.threadNum;i++){
			MileageAnalyThread vehicleRunningAnalyThread = new MileageAnalyThread(i+1,this.threadNum,utc);
			//vehicleRunningAnalyThread.setThreadId();
			vehicleRunningAnalyThread.start();
			ThreadPool.addMileageAnalyPool(i, vehicleRunningAnalyThread);
		}
		//--------------------------------------
		long time = Long.parseLong(notNull(config.get("batchTime")));
		int count = Integer.parseInt(notNull(config.get("batchNum")));
		VehicleMileageUpdateThread vehicleRunningUpdateThread = new VehicleMileageUpdateThread(1,time,count,isUpdateMileage);
		DriverMileageUpdateThread driverMileageUpdateThread  = new DriverMileageUpdateThread(2,time,count);
		ThreadPool.addVehicleStatusUpdatePool(4, vehicleRunningUpdateThread);
		ThreadPool.addVehicleStatusUpdatePool(5, driverMileageUpdateThread);
		
		vehicleRunningUpdateThread.start();
		driverMileageUpdateThread.start();
	
		
		//----------------------------------------------------------------
		loadingEventFileList();
		Vector<File> fileList = LoadFile.findFile(filePath, utc);
		logger.info("--------------------轨迹文件加载完成！文件数：[{}]",fileList.size());
		ThreadPool.setMileageSize(fileList.size());
		ThreadPool.setCurrentMileageSize(0);
		for(int e=0;e<fileList.size();e++){
			ThreadPool.getMileageAnalyPool().get(e % ThreadPool.getMileageAnalyPool().size()).addPacket(fileList.get(e));
		}
		fileList.clear();
		while(flag){
			long currentMileageSize = ThreadPool.getCurrentMileageSize();
			logger.info("-------------------mileageSize : {}, currentMileageSize : {}",ThreadPool.getMileageSize(),currentMileageSize);
			if(ThreadPool.getMileageSize() == currentMileageSize){
				flag = false;
				closeThread();
				//OracleService.updateVehicleStat(this.utc + 12*60*60*1000);
				//logger.info("---------------更新总里程成功！");
			
				logger.info("-------------【查询统计】->【行驶里程】,【油耗统计】-> 【驾驶员】完成！日期：【{}】,耗时：【{}】s",Tools.getDate(utc),(System.currentTimeMillis()-startTime)/1000);			
				//开始月统计
				VehicleRunningMileageMonthTask.dependFlag = false;
			}else{
				//等待基表统计结束
				try {
					Thread.sleep(30 * 1000);
				} catch (InterruptedException e) {
					logger.error("车辆统计出错.",e);
				}
			}
		}
		
	}
	
	private void loadingEventFileList() {
		Vector<File> eventfileList = LoadFile.findFile(eventPath, utc);
		logger.info("--------------------事件文件加载完成！文件数：[{}]",eventfileList.size());
		ThreadPool.eventFileMap.clear();
		for(int e=0;e<eventfileList.size();e++){
			//设置事件文件缓存，为查询超经济区运行时长准备		
			ThreadPool.eventFileMap.put(eventfileList.get(e).getName().replaceAll("\\.txt", ""), eventfileList.get(e));
		}
		
	}
	
	

	private void closeThread() {	
/*		for(AbstractThread t : ThreadPool.getUpdatePool().values()){
			t.close();
		}*/
		ThreadPool.getUpdatePool().get(4).close();
		for(AbstractThread t : ThreadPool.getMileageAnalyPool().values()){
			t.close();
		}
	}
	
	private String notNull(String str){
		if(null == str || "".equals(str)){
			return "0";
		}
		return str.trim();
		
	}

	@Override
	public void isTimeRun() throws Exception {
		Boolean f = true;
		while(f){
			if(ThreadPool.isReportOver){
				f = false;
				
    		}else{
				Thread.sleep(60000);
			}
		}
		
	}
	
}
