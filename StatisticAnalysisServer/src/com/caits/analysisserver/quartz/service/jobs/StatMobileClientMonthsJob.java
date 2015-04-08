package com.caits.analysisserver.quartz.service.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.service.jobs.impl.StatMobileClientMonthsJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 产生手机客户端月分析数据
 * 运行频率：每月1日5点0分 統計生成上月数据
 * @author yujch
 */
public class StatMobileClientMonthsJob extends MyJob {

	private String jobName = "StatMobileClientMonthsJob";
	
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
		
		StatMobileClientMonthsJobdetail vodJobDetail = new StatMobileClientMonthsJobdetail(year,previousmonth);
		
		return vodJobDetail.executeStatRecorder();
	}

}
