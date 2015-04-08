package com.ctfo.trackservice.task;

import java.io.BufferedReader;
import java.io.InputStreamReader;


import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.DateTools;



/**
 * 文件名：VehicleRunningMileageMonthTask.java
 * 功能：驾驶员月统计信息
 *
 * @author huangjincheng
 * 2014-10-27下午4:12:07
 * 
 */
public class VehicleRunningMileageMonthTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(VehicleRunningMileageMonthTask.class);
	/** 是否依赖*/
	public static Boolean dependFlag ;
	
	/**当日12点utc*/
	private String utc = ""; 
	
	private OracleService oracleService = new OracleService();
	
	/**今日*/
	private int day;
	@Override
	public void init() {
		
	}

	@Override
	public void execute() throws Exception {
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------【驾驶员月报表补录】输入您需要补跑的日期(yyyy/mm)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}")) {
					System.out.print("--------------------【驾驶员月报表补录】输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			utc = restoreTime;
			day = 1;  //重跑当前就为1号
			dependFlag = false;//重跑依赖关闭
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteRestoreDriverMonthInfo(utc);
			
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
						logger.info("---------------【油耗统计】-> 【驾驶员月报表】完成！日期：【{}】,耗时： 【{}】ms",utc,(System.currentTimeMillis()-startMonthTime));			
						break;
					} catch (Exception e) {
						logger.debug("驾驶员油耗统计月统计出错！",e);
					}
				}else {
					Thread.sleep(30000);
				}
			}else break;			
		}
		
	}
	
	/**
	 * 
	 * 驾驶员油耗月统计报表
	 */
	private void excuteStatMonthStatis(String utc) throws Exception{
		int beginYear = Integer.parseInt(utc.split("/")[0]);
		int endYear =Integer.parseInt(DateTools.getNextYearMonth(utc).split("/")[0]);
		int beginMonth = Integer.parseInt(utc.split("/")[1]);
		int endMonth = Integer.parseInt(DateTools.getNextYearMonth(utc).split("/")[1]);

		long beginTime = DateTools.getFirstDayOfMonth(beginYear, beginMonth).getTime();
		long endTime = DateTools.getFirstDayOfMonth(endYear, endMonth).getTime();
		OracleService oracleservice = new OracleService();
		oracleservice.saveDriverMonthInfo(beginTime, endTime, beginMonth+"", beginYear+"");
		
		
	}

	@Override
	public void isTimeRun() throws Exception {
		// TODO Auto-generated method stub
		
	}

}
