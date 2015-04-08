package com.caits.analysisserver.quartz.visitlog.jobs;

import java.util.Date;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.visitlog.jobs.impl.AnalyserVisitlogDaysJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 访问记录按小时、日分析
 * 运行频率：每日4点10分
 * @author yujch
 */
public class AnalyserVisitlogDaysJob extends MyJob {

	@SuppressWarnings("unused")
	private static Logger logger = LoggerFactory.getLogger(AnalyserVisitlogDaysJob.class);
	
	private String jobName = "AnalyserVisitlogDaysJob";
	
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
		
		AnalyserVisitlogDaysJobdetail vodJobDetail = new AnalyserVisitlogDaysJobdetail(dt);
		
		return vodJobDetail.executeStatRecorder();
	}

	
/*
	@Override
	public int executeEnd(int execFlag) {
		// TODO Auto-generated method stub
		return JobMonitor.getInstance().updateJobRunningMonitor("OrgAlarmDaystatJob", ""+execFlag, new Date());
	}*/
}
