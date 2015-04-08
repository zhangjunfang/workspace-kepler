package com.ctfo.regionfileser.timer;

import org.apache.log4j.Logger;
import org.springframework.context.ApplicationContext;

import com.ctfo.regionfileser.filemanager.service.MemTbServiceviewManageServiceRmi;
import com.ctfo.regionfileser.util.SpringBUtils;

public class TbServiceTimer {
	private static final Logger logger = Logger.getLogger(TbServiceTimer.class);
	private ApplicationContext applicationContext = null;
	
	private MemTbServiceviewManageServiceRmi memTbServiceviewManageServiceRmi = null;
	
	public void timerTbService() {
		long start = System.currentTimeMillis();
		logger.info("开始执行定时器");
		applicationContext = SpringBUtils.getApplicationContext();
		
		memTbServiceviewManageServiceRmi = (MemTbServiceviewManageServiceRmi)applicationContext.getBean("memTbServiceviewManageServiceRmi");
		
		// 将轨迹数据写入文件
		memTbServiceviewManageServiceRmi.saveRegionFile();
		
		//memTbServiceviewManageServiceRmi.insertTbService();
		long end = System.currentTimeMillis();
		logger.info("定时器执行结束,耗时(ms):"+(end - start));
	}
	
}
