package com.ctfo.storage.media.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.util.ConfigLoader;
import com.ctfo.storage.media.util.SystemUtil;

public class MediaMain {
	private static Logger log = LoggerFactory.getLogger(MediaMain.class);
	
	
	public static void main(String[] args) {
		try{
			if (args.length < 3) {//参数不合法
				log.error("输入参数不合法，请输入形式如“-d conf start”的参数。");
				return;
			}
			// 加载配置文件信息
			ConfigLoader.init(args);
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
