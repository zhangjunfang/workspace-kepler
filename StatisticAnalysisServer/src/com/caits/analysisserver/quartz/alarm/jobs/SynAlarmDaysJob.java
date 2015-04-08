package com.caits.analysisserver.quartz.alarm.jobs;

	import java.util.Date;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.alarm.jobs.impl.SynAlarmDaysJobdetail;
import com.caits.analysisserver.utils.CDate;

	/**
	 * 同步告警按企业告警级别统计告警信息同时更新redis缓存
	 * 运行频率：每日5点00分
	 * @author yujch
	 */
	public class SynAlarmDaysJob extends MyJob {
		private String jobName = "SynAlarmDaysJob";
		
		@Override
		public String getJobName() {
			// TODO Auto-generated method stub
			return this.jobName;
		}
		
	/*	@Override
		public int executePrev() {
			// TODO Auto-generated method stub
			return JobMonitor.getInstance().queryJobDependStatus("OrgAlarmDaystatJob");
		}*/

		/*
		 * 每日统计前一日数据
		 * 
		 * @see org.quartz.Job#execute(org.quartz.JobExecutionContext)
		 */
		@Override
		public int executeJob(JobExecutionContext arg0) throws JobExecutionException {
			// TODO Auto-generated method stub
			long yesterdayNoon = CDate.getYesDayYearMonthDay()+1000*60*60*12;//昨天中午日期时间
			Date dt = new Date();
			dt.setTime(yesterdayNoon);
			
			SynAlarmDaysJobdetail vodJobDetail = new SynAlarmDaysJobdetail();
			
			return vodJobDetail.executeStatRecorder();
		}

		
	/*
		@Override
		public int executeEnd(int execFlag) {
			// TODO Auto-generated method stub
			return JobMonitor.getInstance().updateJobRunningMonitor("OrgAlarmDaystatJob", ""+execFlag, new Date());
		}*/
	}


