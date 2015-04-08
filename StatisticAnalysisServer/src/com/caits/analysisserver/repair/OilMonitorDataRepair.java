package com.caits.analysisserver.repair;

import java.io.File;
import java.util.Calendar;
import java.util.Vector;

import oracle.jdbc.OracleConnection;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.LoadFile;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.services.OilService;

/**
 * 基础数据重跑任务
 * @author yujch
 *
 */
public class OilMonitorDataRepair extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(BasedataRepair.class);
	
	@SuppressWarnings("unused")
	private final String keyWord = "track";
	
	private long utc = 0;

	private String fileNameStr = "";//欲恢复数据的文件列表，多个文件用逗号隔开
	
	public OilMonitorDataRepair(){

	}
	
	/******
	 * 统计指定时间数据
	 */
	@Override
	public void run() {
		long startTime = System.currentTimeMillis();
		AnalysisDBAdapter dba = new AnalysisDBAdapter();
		Vector<File> fileList = null;
		OracleConnection dbCon = null;
		try {
			if(fileNameStr!=null&&!"".equals(fileNameStr)){
				String[] fileNames = fileNameStr.split(",");
				fileList = LoadFile.loadAssignFile(FilePool.getinstance().getFile(this.utc,"oilUrl"), this.utc,fileNames);
			}else{
				fileList = LoadFile.findFile(FilePool.getinstance().getFile(this.utc,"oilUrl"), this.utc);
			}
			dba.initDBAdapter();
			
			// 日统计文件 单项数据恢复用单线程做
			logger.info("OIL CHANGE daystat Repair job open!");
			for (int idx = 0; idx < fileList.size(); idx++) {
				File file = fileList.get(idx);
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				String vid = file.getName().replaceAll("\\.txt", "");
				OilService oilService = new OilService(dbCon,utc,vid);
				oilService.analysisOilRecords(file);
			}// End for
			
		} catch (InterruptedException e) {
			logger.error("统计线程休眠出错。", e);
		} catch (Exception e) {
			logger.error("统计主线程出错。",e);
		}finally{
			try{
			if(fileList != null && fileList.size() > 0){
				fileList.clear();
			}
			
			if (dbCon!=null){
				dbCon.close();
			}
			}catch(Exception ex){
				logger.error("将连接放回连接池出错：",ex);
			}
		}
		
		long endTime = System.currentTimeMillis();
		long costTime = (endTime - startTime);
		Calendar cl = Calendar.getInstance();
		cl.setTimeInMillis(this.utc);
	
 		logger.info("oilchange daystat Repair End " + cl.get(Calendar.YEAR) + "-" + (cl.get(Calendar.MONTH) +1) + "-" + cl.get(Calendar.DAY_OF_MONTH) + " use time:" + (costTime) / 1000 + "s");
	}


	/****
	 * 设置统计时间
	 */
	public void setTime(long utc) {
		this.utc = utc;
		
	}

	public void setFileNameStr(String fileNameStr) {
		this.fileNameStr = fileNameStr;
	}
	
	public void setDate(long date) {
		this.utc = date;
	}

}
