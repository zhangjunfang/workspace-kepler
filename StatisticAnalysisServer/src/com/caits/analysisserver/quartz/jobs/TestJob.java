package com.caits.analysisserver.quartz.jobs;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.quartz.MyJob;

public class TestJob extends MyJob{
	
	private static final Logger logger = LoggerFactory
	.getLogger(TestJob.class);
	
	private String jobName="TestJob";

	@Override
	public String getJobName() {
		// TODO Auto-generated method stub
		return jobName;
	}

	@Override
	public int executeJob(JobExecutionContext arg0)
			throws JobExecutionException {
		// TODO Auto-generated method stub
		logger.info("TestJob 开始执行");
		return 1;
	}

}
