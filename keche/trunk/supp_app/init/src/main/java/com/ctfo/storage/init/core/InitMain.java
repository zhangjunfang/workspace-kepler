/**
 * 2014-5-22InitMain.java
 */
package com.ctfo.storage.init.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.init.util.ConfigLoader;

/**
 * InitMain
 * 
 * 
 * @author huangjincheng
 * 2014-5-22上午10:21:40
 * 
 */
public class InitMain {
	private static final Logger logger = LoggerFactory.getLogger(InitMain.class);
	public static void main(String[] args) {
		try{
		// -start
			if (args.length < 3) {//参数不合法
				//System.out.println("输入参数不合法，请输入形式如“-d src start”的参数。");
				logger.error("输入参数不合法，请输入形式如“-d src start”的参数。");
				return;
			}
			// 程序初始化
			ConfigLoader.init("");
		}catch (Exception e){
			logger.error("服务启动异常:" + e.getMessage() , e);
			logger.error("系统退出...");
 			System.exit(0);
		}
	}
}
