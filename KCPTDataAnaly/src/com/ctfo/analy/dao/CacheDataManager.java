package com.ctfo.analy.dao;

import java.util.Date;

import org.apache.log4j.Logger;

public class CacheDataManager extends Thread {
	
	private static final Logger logger = Logger
	.getLogger(CacheDataManager.class);
	
	public boolean isRunning = true;
	
	MonitorDBAdapter monitorDBAdapter=new MonitorDBAdapter();

	@Override
	public void run() {
		// TODO Auto-generated method stub
		logger.info("TB-同步数据库报警设置启动");
		long dateTime = System.currentTimeMillis();
		// while (isRunning) {
		try {
			//加载需要定时更新的缓存
			while (isRunning) {

				long updateIllegalOptionsStart = new Date().getTime();
				logger.debug("TB-定时更新车辆非法运营设置");
				monitorDBAdapter.updateIllegalOptionsAlarm();
				logger.debug("TB-定时更新车辆非法运营完成，耗时："+(new Date().getTime()-updateIllegalOptionsStart)+"ms");
				
				long overspeedAlarmCfgStart = new Date().getTime();
				logger.debug("TB-定时更新车辆超速设置");
				monitorDBAdapter.updateOverspeedAlarmCfg();
				logger.debug("TB-定时更新车辆超速设置完成，耗时："+(new Date().getTime()-overspeedAlarmCfgStart)+"ms");
				
				long fatigueAlarmCfgStart = new Date().getTime();
				logger.debug("TB-定时更新车辆疲劳驾驶设置");
				monitorDBAdapter.updateFatigueAlarmCfg();
				logger.debug("TB-定时更新车辆疲劳驾驶设置完成，耗时："+(new Date().getTime()-fatigueAlarmCfgStart)+"ms");

				Thread.sleep(60 * 1000);
			}

		} catch (Exception e) {
			logger.error("TB-定时同步数据错误" + e);
		}

	}

}
