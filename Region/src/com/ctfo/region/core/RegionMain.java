package com.ctfo.region.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;


/*****************************************
 * <li>@描        述：工程入口			</li><br>
 * <li>@创  建  者：hushaung 		</li><br>
 * <li>@时        间：2013-6-15  下午3:12:58	</li><br>
 * 
 *****************************************/
public class RegionMain {
	protected static final Logger logger = LoggerFactory.getLogger(RegionMain.class); 
	/*****************************************
	 * <li>描        述：主函数 		</li><br>
	 * <li>参数： @param args 		</li><br>
	 * <li>时        间：2013-6-15  下午3:12:42	</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
		System.out.println("应用程序正在启动中 ... ...");
		logger.info("应用程序正在启动中 ... ...");
		@SuppressWarnings("unused")
		ApplicationContext context = new ClassPathXmlApplicationContext("spring-*.xml");   
		System.out.println("应用程序启动完成 !");
		logger.info("应用程序启动完成 !");
	}
	
}
