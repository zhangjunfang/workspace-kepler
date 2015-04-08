package com.caits.analysisserver.quartz.jobs;

import java.util.Date;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationDaystatJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 车辆运营情况日汇总
 * 运行频率：每日0点10分
 * @author yujch
 */
public class OrgOperationDaystatJob extends MyJob {

	private String jobName = "OrgOperationDaystatJob";
	
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
		long yesterdayNoon = CDate.getYesDayYearMonthDay()+1000*60*60*12;//昨天中午日期时间
		Date dt = new Date();
		dt.setTime(yesterdayNoon);
		
		OrgOperationDaystatJobdetail vodJobDetail = new OrgOperationDaystatJobdetail(dt);
		
		return vodJobDetail.executeStatRecorder();
	}

}
