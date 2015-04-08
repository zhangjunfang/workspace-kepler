package com.caits.analysisserver.quartz.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationMonthstatJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 车辆运营情况日汇总
 * 运行频率：每月1日0点30分 統計生成上月数据
 * @author yujch
 */
public class OrgOperationMonthstatJob extends MyJob {

	private String jobName = "OrgOperationMonthstatJob";
	
	@Override
	public String getJobName() {
		// TODO Auto-generated method stub
		return this.jobName;
	}
	/*
	 * 每日统计前一日数据
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
		
		OrgOperationMonthstatJobdetail vodJobDetail = new OrgOperationMonthstatJobdetail(year,previousmonth);
		
		return vodJobDetail.executeStatRecorder();
	}

}
