package com.caits.analysisserver.bean;

import java.util.TimerTask;

public class Task{
	private String time = null; // 任务指定时间
	
	private TimerTask task = null; //任务

	public String getTime() {
		return time;
	}

	public void setTime(String time) {
		this.time = time;
	}

	public TimerTask getTask() {
		return task;
	}

	public void setTask(TimerTask task) {
		this.task = task;
	}
	
	
}
