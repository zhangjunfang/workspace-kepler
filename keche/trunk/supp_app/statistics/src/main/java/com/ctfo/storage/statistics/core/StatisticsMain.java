/**
 * 
 */
package com.ctfo.storage.statistics.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.statistics.util.ConfigLoader;
//import com.ctfo.storage.statistics.util.SystemUtils;
import com.ctfo.storage.statistics.util.SystemUtil;

/**
 * 统计服务
 */
public class StatisticsMain {
	private static Logger log = LoggerFactory.getLogger(StatisticsMain.class);
	
	public static void main(String[] args) {
		try{
			if (args.length < 3) {//参数不合法
				log.error("输入参数不合法，请输入形式如“-d conf start”的参数。");
				return;
			}
			// 加载配置文件信息
			ConfigLoader.getInstance().init(args);
			// 生成PID文件
			SystemUtil.generagePid();
			log.info("服务启动完成--ok!");
		}catch (Exception e){
			log.error("服务启动异常:" + e.getMessage() , e);
			log.error("系统退出...");
			System.exit(0);
		}
	}
}
