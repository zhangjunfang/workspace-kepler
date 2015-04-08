package com.caits.analysisserver.quartz.jobs;

	import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.jobs.impl.OilConsumeMonthstatJobdetail;
import com.caits.analysisserver.utils.CDate;

	/**
	 * 燃油消耗月统计
	 * 运行频率：每月1日4点30分 统计生成上月数据
	 * @author yujch
	 */
	public class OilConsumeMonthstatJob extends MyJob {

		private String jobName = "OilConsumeMonthstatJob";
		
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
			int previousmonth = CDate.getPreviousMonth();
			int month = CDate.getMonthOfYearByCurrentDate();
			int year = CDate.getCurrentYear();
			if (previousmonth>month){
				year = CDate.getPreviousYear();
			}
			
			OilConsumeMonthstatJobdetail vodJobDetail = new OilConsumeMonthstatJobdetail(year,previousmonth);
			
			return vodJobDetail.executeStatRecorder();
		}

	}

