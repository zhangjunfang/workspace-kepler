package com.caits.analysisserver.quartz.alarm.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.alarm.jobs.impl.StatAlarmMonthJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 车辆运营情况日汇总
 * 运行频率：每月1日0点30分 統計生成上月数据
 * @author yujch
 */
public class StatAlarmMonthJob extends MyJob {


	private String jobName = "StatAlarmMonthJob";
	
	@Override
	public String getJobName() {
		// TODO Auto-generated method stub
		return this.jobName;
	}
	/*
	 * 每月1日统计前一月数据
	 * 
	 * @see org.quartz.Job#execute(org.quartz.JobExecutionContext)
	 */
	@Override
	public int executeJob(JobExecutionContext arg0) throws JobExecutionException {
		// TODO Auto-generated method stub
		int previousmonth = CDate.getPreviousMonth()-1;
		int month = CDate.getMonthOfYearByCurrentDate();
		int year = CDate.getCurrentYear();
		if (previousmonth>month){
			year = CDate.getPreviousYear();
		}
		
		StatAlarmMonthJobdetail vodJobDetail = new StatAlarmMonthJobdetail(year,previousmonth);
		
		return vodJobDetail.executeStatRecorder();
	}

}
