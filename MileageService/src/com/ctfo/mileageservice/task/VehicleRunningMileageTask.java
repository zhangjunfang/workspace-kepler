package com.ctfo.mileageservice.task;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mileageservice.dao.ThreadPool;
import com.ctfo.mileageservice.parse.DriverMileageUpdateThread;
import com.ctfo.mileageservice.parse.MileageAnalyThread;
import com.ctfo.mileageservice.parse.VehicleMileageUpdateThread;
import com.ctfo.mileageservice.service.OracleService;
import com.ctfo.mileageservice.util.DateTools;
import com.ctfo.mileageservice.util.LoadFile;
import com.ctfo.mileageservice.util.Tools;


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
	/**当日12点utc*/
	private Long utc = -1l; 
	
	private Boolean flag ;
	@Override
	public void init() {
		this.filePath = config.get("trackFilePath");	
	}

	@Override
	public void execute() throws Exception {		
		logger.info("--------------------【查询统计】->【行驶里程】统计开始!-------------------------");
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------输入您需要补跑的日期(yyyy/mm/dd)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.print("--------------------输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd hh/mm/ss");
			Date date = sdf.parse(restoreTime+" 00/00/00");
			utc = date.getTime();
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			OracleService.deleteRestoreInfo(utc+12*60*60*1000);			
		}else if("start".equals(this.type)){
			utc = DateTools.getYesDayYearMonthDay();
		}
		
		long startTime = System.currentTimeMillis();	
		flag = true;
		ThreadPool.init();
		for(int i = 0;i<this.threadNum;i++){
			MileageAnalyThread vehicleRunningAnalyThread = new MileageAnalyThread(i+1,this.threadNum,utc);
			//vehicleRunningAnalyThread.setThreadId();
			vehicleRunningAnalyThread.start();
			ThreadPool.addVehicleStatusAnalyPool(i, vehicleRunningAnalyThread);
		}
		//--------------------------------------
		long time = Long.parseLong(notNull(config.get("batchTime")));
		int count = Integer.parseInt(notNull(config.get("batchNum")));
		VehicleMileageUpdateThread vehicleRunningUpdateThread = new VehicleMileageUpdateThread(1,time,count);
		DriverMileageUpdateThread driverMileageUpdateThread  = new DriverMileageUpdateThread(2,time,count);
		ThreadPool.addVehicleStatusUpdatePool(1, vehicleRunningUpdateThread);
		ThreadPool.addVehicleStatusUpdatePool(2, driverMileageUpdateThread);
		
		vehicleRunningUpdateThread.start();
		driverMileageUpdateThread.start();
	
		
		//----------------------------------------------------------------
		
		Vector<File> fileList = LoadFile.findFile(filePath, utc);
		logger.info("--------------------轨迹文件加载完成！文件数：{}",fileList.size());
		ThreadPool.setTotalSize(fileList.size());
		for(int e=0;e<fileList.size();e++){
			ThreadPool.getAnalyPool().get(e % ThreadPool.getAnalyPool().size()).addPacket(fileList.get(e));
		}
		fileList.clear();
		while(flag){
			long fileSize = ThreadPool.getFileSize();
			logger.info("-------------------totalSize : {}, fileSize : {}",ThreadPool.getTotalSize(),fileSize);
			if(ThreadPool.getTotalSize() == fileSize){
				flag = false;
				closeThread();
				try {
					OracleService.updateVehicleStat(this.utc + 12*60*60*1000);
					logger.info("更新总里程成功！");
				} catch (SQLException e) {
					logger.debug("更新总里程表异常！",e);
				}
				logger.info("---------------【查询统计】->【行驶里程】完成！日期：【{}】,耗时： 【{}】s",Tools.getDate(utc),(System.currentTimeMillis()-startTime)/1000);			
			
			}else{
				//等待基表统计结束
				try {
					Thread.sleep(10 * 1000); 
				} catch (InterruptedException e) {
					logger.error("车辆统计出错.",e);
				}
			}
		}
		
	}

	private void closeThread() {	
/*		for(AbstractThread t : ThreadPool.getUpdatePool().values()){
			t.close();
		}*/
		ThreadPool.getUpdatePool().get(1).close();
		/*for(AbstractThread t : ThreadPool.getAnalyPool().values()){
			t.close();
		}*/
	}
	
	private String notNull(String str){
		if(null == str || "".equals(str)){
			return "0";
		}
		return str.trim();
		
	}
	
}
