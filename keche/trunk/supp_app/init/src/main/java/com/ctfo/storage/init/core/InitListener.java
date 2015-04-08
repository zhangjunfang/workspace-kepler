package com.ctfo.storage.init.core;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.init.util.ConfigLoader;
import com.ctfo.storage.init.util.SystemUtil;


/**
 * @author zjhl
 *
 */
public class InitListener implements ServletContextListener {
	private static Logger log = LoggerFactory.getLogger(InitListener.class);
	
	/**
	 * 程序启动
	 */
	public void contextInitialized(ServletContextEvent arg0) {
		// 生成PID文件
		String pid = null;
		try {
			pid = SystemUtil.generagePid(); 
			String initparam = arg0.getServletContext().getInitParameter("test");
			log.info("InitService init ... - 初始化服务启动, 程序进程号:[{}], 程序开始加载...启动参数{}",  pid, initparam);
			// 程序初始化
			ConfigLoader.init(initparam); 
	
			log.info("InitService inited！ - 初始化完成！");
		} catch (Exception e) {
			log.error("InitService init Error:" + e.getMessage(), e);
		}
	}
	/**  
	 * 程序退出
	 */
	public void contextDestroyed(ServletContextEvent arg0) {
		log.info("InitService stop! - 初始化服务停止!");
	}
}
