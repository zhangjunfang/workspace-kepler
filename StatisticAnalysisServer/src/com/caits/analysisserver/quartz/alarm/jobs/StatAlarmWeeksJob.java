package com.caits.analysisserver.quartz.alarm.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.alarm.jobs.impl.StatAlarmWeekstatJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 告警按企业告警类别日统计
 * 运行频率：每日5点00分
 * @author yujch
 */
public class StatAlarmWeeksJob extends MyJob {
	private String jobName = "StatAlarmWeeksJob";
	
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
		int previousweek = CDate.getPreviousWeek();
		int month = CDate.getMonthOfYearByCurrentDate();
		int year = CDate.getCurrentYear();
		if (month==0&&previousweek>4){
			year = CDate.getPreviousYear();
		}
		
		StatAlarmWeekstatJobdetail vodJobDetail = new StatAlarmWeekstatJobdetail(year,previousweek);
		
		return vodJobDetail.executeStatRecorder();
	}

	
/*
	@Override
	public int executeEnd(int execFlag) {
		// TODO Auto-generated method stub
		return JobMonitor.getInstance().updateJobRunningMonitor("OrgAlarmDaystatJob", ""+execFlag, new Date());
	}*/
}



