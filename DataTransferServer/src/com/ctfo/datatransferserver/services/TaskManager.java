package com.ctfo.datatransferserver.services;

import java.util.Vector;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import com.lingtu.xmlconf.XmlConf;

/**
 * 任务管理类
 * 
 * @author yangyi
 * 
 */
public class TaskManager {
	XmlConf config;

	public TaskManager(XmlConf config) {
		this.config = config;
	}

	/**
	 * 初始化数据传输任务
	 * 
	 * @param config
	 * @throws InstantiationException
	 * @throws IllegalAccessException
	 * @throws ClassNotFoundException
	 */
	@SuppressWarnings("unchecked")
	public void initTask() throws InstantiationException, IllegalAccessException, ClassNotFoundException {
		Vector<String> claddin = config.getSubConfigNames("TaskAddIns");
		int size = claddin.size();
		ScheduledExecutorService service = Executors.newScheduledThreadPool(size);
		for (int i = 0; i < size; i++) {
			String nodename = "TaskAddIns|" + i;
			String calssname = config.getStringValue(nodename + "|class");
			int delay = config.getIntValue(nodename + "|delay");
			int period = config.getIntValue(nodename + "|period");
			Class<TransferTask> clAnalyser = (Class<TransferTask>) Class.forName(calssname);
			TransferTask transferTask = (TransferTask) clAnalyser.newInstance();
			transferTask.initTask(config, nodename);
			service.scheduleWithFixedDelay(transferTask, delay, period, TimeUnit.SECONDS);
		}
	}
}
