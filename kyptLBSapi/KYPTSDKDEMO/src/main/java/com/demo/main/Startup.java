package com.demo.main;

import org.apache.log4j.xml.DOMConfigurator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.demo.spring.SpringBootStrap;
import com.kypt.log.LogFormatter;

public class Startup {

	private final static String NAME = "Startup";

	private static Logger log = LoggerFactory.getLogger(Startup.class);
	private static String LOCATION = "classpath:main.xml";
	private static String MEMCACHE_LOCATION = "classpath:xmemcache.xml";

	private ClassPathXmlApplicationContext cx;

	protected Startup() {

	}

	private void loadLog4jConfig() {
		String log4jpath = System.getProperty("user.dir")
				+ System.getProperty("file.separator") + "config"
				+ System.getProperty("file.separator") + "log4j.xml";

		System.out.println("log4j文件路径：" + log4jpath);

		//DOMConfigurator.configure(log4jpath);
		System.out.println("log4j文件路径2：" + log4jpath);
	}

	void init() {
		loadLog4jConfig();
	}

	void start() {
		//加载spring文件
		SpringBootStrap.getInstance().setConfig(LOCATION);
		SpringBootStrap.getInstance().init();
		
		//模拟数据上传线程
		UpLocThread upthread = new UpLocThread();
		upthread.start();
		
		//模拟数据接收线程
		DownMsgView downthread = new DownMsgView();
		downthread.start();

	}

	

	public static void main(String[] args) {
		 try {
		 Startup startup = new Startup();
		 startup.init();
		 startup.start();
		            
		 System.out.println("中转系统启动成功！");
		
		 } catch (Throwable t) {
			 log.error(LogFormatter.formatMsg(NAME, "start failed."), t);
		 }

	}

}
