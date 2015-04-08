package com.ctfo.redissync.app;

import java.io.IOException;
import java.util.Properties;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.redissync.util.ConstantUtil;
import com.ctfo.redissync.util.ResourcesUtil;

/**
 * Redis同步程序，同步企业下所有车辆列表
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class NetTransformMain {

	private static final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(2);
	private static final Logger logger = LoggerFactory.getLogger(NetTransformMain.class);

	public static void main(String[] args) throws IOException {
		final Properties p = ResourcesUtil.getResourceAsProperties(ConstantUtil.SYS_CONF_FILE);
		long initialDelay = Long.parseLong(p.getProperty("TM_initalDelay", "1").trim());
		long delay = Long.parseLong(p.getProperty("TM_delay", "720").trim());

		if (logger.isInfoEnabled()) {
			logger.info("Server start suceessful~!!");
			logger.info("time configure: initialDelay: {} minutes, delay: {} minutes", initialDelay, delay);
		}
		// 开启定时器
		scheduler.scheduleWithFixedDelay(new NetTransformTask(), initialDelay, delay, TimeUnit.MINUTES);
	}
}
