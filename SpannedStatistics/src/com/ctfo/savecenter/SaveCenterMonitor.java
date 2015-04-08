package com.ctfo.savecenter;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.addin.AddInManagerInit;
import com.ctfo.savecenter.analy.AnalyseServiceInit;
import com.ctfo.savecenter.io.DataPool;

/**
 * 队列监控线程
 * 
 * @author yangyi
 * 
 */
public class SaveCenterMonitor extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(SaveCenterMonitor.class);

	public static boolean isRunning = true;// 线程运行标志

	public SaveCenterMonitor() {

	}

	@Override
	public void run() {
		while (isRunning) {
			try {
				sleep(30000);
				logger.info("[30s接收:" + DataPool.getReceiveCountValue() + "],"
						+ AnalyseServiceInit.getThreadInfo() + ","+AddInManagerInit.getThreadInfo());

				
			} catch (Exception e) {
				logger.error("队列监控线程错误：" + e.getMessage());
			}
		}
	}

}
