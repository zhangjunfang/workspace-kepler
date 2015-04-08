package com.ctfo.storage.dispatch.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.util.ConfigLoader;
import com.ctfo.storage.dispatch.util.SystemUtil;




public class DispatchMain {
	private static Logger log = LoggerFactory.getLogger(DispatchMain.class);
	/**
	 * 程序入口
	 * @param args
	 */
	public static void main(String[] args) {
		try{
			if (args.length < 3) {//参数不合法
				log.error("输入参数不合法，请输入形式如“-d src start”的参数。");
				return;
			}
			// 生成PID文件
			SystemUtil.generagePid();
			// 程序初始化
			ConfigLoader.init(args); 
			log.info("服务启动完成--ok!");
		}catch (Exception e){
			log.error("服务启动异常:" + e.getMessage() , e);
			log.error("系统退出...");
			System.exit(0);
		}
	}
}
