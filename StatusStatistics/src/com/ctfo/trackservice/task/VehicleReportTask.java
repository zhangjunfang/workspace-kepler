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
import com.ctfo.trackservice.parse.VehicleReportAnalyThread;
import com.ctfo.trackservice.parse.VehicleReportCoolThread;
import com.ctfo.trackservice.parse.VehicleReportGsThread;
import com.ctfo.trackservice.parse.VehicleReportOilThread;
import com.ctfo.trackservice.parse.VehicleReportRotateThread;
import com.ctfo.trackservice.parse.VehicleReportSpeedThread;
import com.ctfo.trackservice.parse.VehicleReportTempairThread;
import com.ctfo.trackservice.parse.VehicleReportVolThread;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;

/**
 * 文件名：VehicleReportTask.java
 * 功能：单车分析报表
 *
 * @author huangjincheng
 * 2014-10-29下午3:05:50
 * 
 */
public class VehicleReportTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(VehicleReportTask.class);
	/**轨迹文件路径*/
	private String filePath ="";
	/**当日12点utc*/
	private Long utc = -1l; 
	private OracleService oracleService = new OracleService();
	private Boolean flag ;
	@Override
	public void init() {
		this.filePath = config.get("trackFilePath");
		//this.utc = DateTools.getYesDayYearMonthDay();
		//execute();
	}
	/**
	 * 执行主方法
	 * @throws Exception 
	 */
	public void execute() throws Exception {		
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------【单车分析报告】输入您需要补跑的日期(yyyy/mm/dd)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.print("--------------------【单车分析报告】输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd hh/mm/ss");
			Date date = sdf.parse(restoreTime+" 00/00/00");
			utc = date.getTime();
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteReportInfo(utc+12*60*60*1000);
		}else if("start".equals(this.type)){
			utc = DateTools.getYesDayYearMonthDay();
			ThreadPool.isOilOver = false;
			isTimeRun();
		}
		logger.info("--------------------【单车分析报告】任务开始！-------------------------");
		long startTime = System.currentTimeMillis();  
		flag = true;
		//ThreadPool.init();
		for(int i = 0;i<this.threadNum;i++){
			VehicleReportAnalyThread vehicleReportAnalyThread = new VehicleReportAnalyThread(i+1,this.threadNum,utc);
			//vehicleRunningAnalyThread.setThreadId();
			vehicleReportAnalyThread.start();
			ThreadPool.addVehicleReportAnalyPool(i, vehicleReportAnalyThread);
		}
		long time = Long.parseLong(notNull(config.get("batchTime")));
		int count = Integer.parseInt(notNull(config.get("batchNum")));
		
		VehicleReportCoolThread coolThread = new VehicleReportCoolThread(1,time,count);
		VehicleReportGsThread gsThread = new VehicleReportGsThread(1,time,count);
		VehicleReportOilThread oilThread = new VehicleReportOilThread(1,time,count);
		VehicleReportRotateThread rotateThread = new VehicleReportRotateThread(1,time,count);
		VehicleReportSpeedThread speedThread = new VehicleReportSpeedThread(1,time,count);
		VehicleReportTempairThread tempairThread = new VehicleReportTempairThread(1,time,count);
		VehicleReportVolThread volThread = new VehicleReportVolThread(1,time,count);
		
		ThreadPool.addVehicleReportUpdatePool(1, coolThread);
		ThreadPool.addVehicleReportUpdatePool(2, gsThread);
		ThreadPool.addVehicleReportUpdatePool(3, oilThread);
		ThreadPool.addVehicleReportUpdatePool(4, rotateThread);
		ThreadPool.addVehicleReportUpdatePool(5, speedThread);
		ThreadPool.addVehicleReportUpdatePool(6, tempairThread);
		ThreadPool.addVehicleReportUpdatePool(7, volThread);
		
		coolThread.start();
		gsThread.start();
		oilThread.start();
		rotateThread.start();
		speedThread.start();
		tempairThread.start();
		volThread.start();
		
		//----------------------------------------------------------------
		
		Vector<File> fileList = LoadFile.findFile(filePath, utc);
		logger.info("--------------------轨迹文件加载完成！文件数：[{}]",fileList.size());
		ThreadPool.setReportSize(fileList.size());
		ThreadPool.setCurrentReportsize(0);
		for(int e=0;e<fileList.size();e++){
			ThreadPool.getVehicleReportAnalyPool().get(e % ThreadPool.getVehicleReportAnalyPool().size()).addPacket(fileList.get(e));
		}
		fileList.clear();
		while(flag){
			long currentReportSize = ThreadPool.getCurrentReportsize();
			logger.info("-------------------ReportSize : {} * 7 = {}, currentReportSize : {}",ThreadPool.getReportSize(),ThreadPool.getReportSize()*7,currentReportSize);
			if(ThreadPool.getReportSize()*7 == currentReportSize){
				flag = false;
				closeThread();
				logger.info("---------------单车分析报告日统计完成！日期：【{}】,耗时： 【{}】s",Tools.getDate(utc),(System.currentTimeMillis()-startTime)/1000);			
				ThreadPool.isReportOver = true;
			}else{
				//等待基表统计结束
				try {
					Thread.sleep(30 * 1000); 
				} catch (InterruptedException e) {
					logger.error("单车分析报告统计出错.",e);
				}
			}
		}
		
	}
	
	/**
	 * 关闭线程
	 */
	private void closeThread() {	
		for(AbstractThread t : ThreadPool.getVehicleReportUpdatePool().values()){
			t.close();
		}
		for(AbstractThread t : ThreadPool.getVehicleReportAnalyPool().values()){
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
			if(ThreadPool.isOilOver){
				f = false;
			}else{
				Thread.sleep(3000);
			}
		}
		
		
	}

}

