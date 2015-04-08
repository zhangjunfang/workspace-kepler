package com.ctfo.advice.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.util.ConfigLoader;
import com.ctfo.advice.util.SystemUtil;


/**
 * 文件名：AdviceMain.java
 * 功能：
 *
 * @author root
 * 2014-8-12下午3:56:13
 * 
 */
public class AdviceMain {
	private static final Logger logger = LoggerFactory.getLogger(AdviceMain.class);
	public static void main(String[] args) {
		try{
		// -start
			if (args.length < 3) {//参数不合法
				//System.out.println("输入参数不合法，请输入形式如“-d src start”的参数。");
				logger.error("输入参数不合法，请输入形式如“-d src start”的参数。");
				return;
			}
			// 生成PID文件
			SystemUtil.generagePid();
			// 程序初始化
			ConfigLoader.init("");
		}catch (Exception e){
			logger.error("服务启动异常:" + e.getMessage() , e);
			logger.error("系统退出...");
 			System.exit(0);
		}
	}
}
