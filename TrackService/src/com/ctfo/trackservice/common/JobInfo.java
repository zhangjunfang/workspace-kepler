package com.ctfo.trackservice.common;

import org.quartz.CronTrigger;
import org.quartz.JobDetail;

public class JobInfo {
	/**	任务明细	*/
	private JobDetail jobDetail;
	/**	任务触发器	*/
    private CronTrigger cronTrigger;
	/**
	 * @return the 任务明细
	 */
	public JobDetail getJobDetail() {
		return jobDetail;
	}
	/**
	 * @param 任务明细 the jobDetail to set
	 */
	public void setJobDetail(JobDetail jobDetail) {
		this.jobDetail = jobDetail;
	}
	/**
	 * @return the 任务触发器
	 */
	public CronTrigger getCronTrigger() {
		return cronTrigger;
	}
	/**
	 * @param 任务触发器 the cronTrigger to set
	 */
	public void setCronTrigger(CronTrigger cronTrigger) {
		this.cronTrigger = cronTrigger;
	}

    
}
