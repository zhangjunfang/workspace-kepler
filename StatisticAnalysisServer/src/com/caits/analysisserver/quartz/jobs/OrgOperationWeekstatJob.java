package com.caits.analysisserver.quartz.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.MyJob;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationWeekstatJobdetail;
import com.caits.analysisserver.utils.CDate;

/**
 * 车辆运营情况周汇总
 * 运行频率：每周1日0点30分 統計生成上周数据
 * @author yujch
 */
public class OrgOperationWeekstatJob extends MyJob {

	private String jobName = "OrgOperationWeekstatJob";
	
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
		int previousweek = CDate.getPreviousWeek();
		int month = CDate.getMonthOfYearByCurrentDate();
		int year = CDate.getCurrentYear();
		if (month==0&&previousweek>4){
			year = CDate.getPreviousYear();
		}
		
		OrgOperationWeekstatJobdetail vodJobDetail = new OrgOperationWeekstatJobdetail(year,previousweek);
		
		return vodJobDetail.executeStatRecorder();
	}

}

