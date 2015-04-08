package com.caits.analysisserver.quartz;

import java.util.Date;

import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;

import com.caits.analysisserver.quartz.jobs.monitor.JobMonitor;
import com.ctfo.memcache.beans.JobInfo;

/**
 * 企业车辆告警处理情况日汇总 运行频率：每日0点10分
 * 
 * @author yujch
 */
public abstract class MyJob implements Job {
	/*
	 * 作业抽象类，系统中所有作业类的父类
	 * 
	 * @see org.quartz.Job#execute(org.quartz.JobExecutionContext)
	 */
	private Date triggerTime;
	private Date finishTime;
	
	private int waitTime = 10*60*1000;//10分钟
	
	private long lastUseTime = 0L;

	@Override
	public void execute(JobExecutionContext arg0) throws JobExecutionException {
		try {
			int readyFlag = 1;
			int execFlag = 0;
			readyFlag = executePrev();
			
			Date fireTime = arg0.getFireTime();
			Date nextFireTime = arg0.getNextFireTime();

			long intervalTime = nextFireTime.getTime() - fireTime.getTime();
			
			while (readyFlag == 2 && intervalTime > waitTime) {// 2表示等待状态
				Thread.sleep(waitTime);
				if (System.currentTimeMillis()>=(nextFireTime.getTime()-lastUseTime-waitTime)){
					break;
				}
				readyFlag = executePrev();
			}
			if (readyFlag != 2){
				triggerTime = new Date();
				if (readyFlag == 1) {
					execFlag = executeJob(arg0);
				}
				finishTime = new Date();

				executeEnd(execFlag);
			}
			
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public abstract String getJobName();

	// 作业执行前,默认返回1，表示需要执行作业
	public int executePrev() {
		JobInfo jobInfo = JobMonitor.getInstance().queryCurrentJob(getJobName());
		int flag = 1;
		if (jobInfo!=null){
			lastUseTime = jobInfo.getUseTime();
			String jobDepend = jobInfo.getJobDepend();
			
			JobMonitor.getInstance().updateJobRunningMonitor(getJobName(), null,
					null,null,lastUseTime);
			if (jobDepend!=null){
				flag = JobMonitor.getInstance().queryJobDependStatus(getJobName());
			}
		}
		return flag;
	}

	// 执行作业
	public abstract int executeJob(JobExecutionContext arg0)
			throws JobExecutionException;

	// 作业执行后
	public int executeEnd(int execFlag) {
		long useTime = finishTime.getTime() - triggerTime.getTime();
		if (lastUseTime>useTime){
			useTime = lastUseTime;
		}
		return JobMonitor.getInstance().updateJobRunningMonitor(getJobName(),
				"" + execFlag,triggerTime,finishTime,useTime);
	}

}
