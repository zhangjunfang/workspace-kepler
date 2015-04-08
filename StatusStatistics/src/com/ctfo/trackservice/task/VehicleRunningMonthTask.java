package com.ctfo.trackservice.task;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.sql.SQLException;


import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.DateTools;


/**
 * 文件名：VehicleRunningMonthTask.java
 * 功能：车辆运行状态月统计
 *
 * @author huangjincheng
 * 2014-10-27下午6:16:51
 * 
 */
public class VehicleRunningMonthTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(VehicleRunningMonthTask.class);
	/** 是否依赖*/
	public static Boolean dependFlag ;
	
	/**当日12点utc*/
	private String utc = "";
	
	/** 今天日*/
	private int day;
	
	private OracleService oracleService = new OracleService();
	@Override
	public void init() {
		
	}

	@Override
	public void execute() {
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------【车辆运行统计月报表补录】输入您需要补跑的日期(yyyy/mm)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				try {
					restoreTime = br.readLine();
				} catch (IOException e) {
					logger.error("读取失败！",e);
				}
				if (!restoreTime.matches("\\d{4}/\\d{2}")) {
					System.out.print("--------------------【车辆运行统计月报表补录】输入错误,请重新选择输入：");
					continue;
				}else break;			
			}
			utc = restoreTime;
			day = 1;
			dependFlag = false; //重跑关闭依赖
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			try {
				oracleService.deleteRestoreMonthInfo(utc);
			} catch (SQLException e) {
				logger.error("--------------------车辆运行月统计删除失败!",e);
			}
			
		}else if("start".equals(this.type)){
			utc = DateTools.getPreYearMonth();
			dependFlag = true;
			day = DateTools.getCurrentDay();
		}
		while(true){
			if(day == 1){
				if(!dependFlag){	
					try {
						long startMonthTime = System.currentTimeMillis();
						excuteStatMonthStatis(utc);
						dependFlag = true;
						logger.info("---------------【机务管理】-> 【车辆运行统计月报表】,【能耗管理】-> 【节油驾驶分析月报表】完成！日期：【{}】,耗时： 【{}】ms",utc,(System.currentTimeMillis()-startMonthTime));			
						break;
					} catch (Exception e) {
						logger.debug("车辆运行月统计出错！",e);
					}
				}else {
					try {
						Thread.sleep(30000);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}else break;
		}
		
	}
	
	/**
	 * 
	 * 车辆运行状态月统计
	 */
	private void excuteStatMonthStatis(String utc) throws Exception{
		int beginYear = Integer.parseInt(utc.split("/")[0]);
		int endYear =Integer.parseInt(DateTools.getNextYearMonth(utc).split("/")[0]);
		int beginMonth = Integer.parseInt(utc.split("/")[1]);
		int endMonth = Integer.parseInt(DateTools.getNextYearMonth(utc).split("/")[1]);
		
		long beginTime = DateTools.getFirstDayOfMonth(beginYear, beginMonth).getTime();
		long endTime = DateTools.getFirstDayOfMonth(endYear, endMonth).getTime();
		OracleService oracleservice = new OracleService();
		oracleservice.saveStaMonthInfo(beginTime, endTime, beginMonth+"", beginYear+"");
		oracleservice.saveOilMonthInfo(beginTime, endTime, beginMonth+"", beginYear+"");
		
		
	}

	@Override
	public void isTimeRun() throws Exception {
		// TODO Auto-generated method stub
		
	}
}
