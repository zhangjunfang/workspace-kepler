package com.caits.analysisserver.quartz.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.jobs.impl.OilConsumeYearstatJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 燃油消耗年统计
 * 运行频率：每年1月1日5点 统计生成上年数据
 * @author yujch
 */
public class OilConsumeYearstatJob extends MyJob {

	private String jobName = "OilConsumeYearstatJob";
	
	@Override
	public String getJobName() {
		// TODO Auto-generated method stub
		return this.jobName;
	}
	/*
	 * 每年1月1日统计前一年数据
	 * 
	 * @see org.quartz.Job#execute(org.quartz.JobExecutionContext)
	 */
	@Override
	public int executeJob(JobExecutionContext arg0) throws JobExecutionException {
		// TODO Auto-generated method stub
		
		int year = CDate.getPreviousYear();

		OilConsumeYearstatJobdetail vodJobDetail = new OilConsumeYearstatJobdetail(year);
		
		return vodJobDetail.executeStatRecorder();
	}

}

