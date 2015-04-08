/**
 * 
 */
package com.ctfo.advice.util;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

import org.apache.commons.lang3.SystemUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * 文件名：SystemUtil.java
 * 功能：
 *
 * @author root
 * 2014-8-12下午4:46:08
 * 
 */
public class SystemUtil {
	private static Logger log = LoggerFactory.getLogger(SystemUtil.class);

	private SystemUtil() {

	}
	/**
	 * 生成PID文件
	 * @throws Exception 
	 */
	public static String generagePid() throws Exception {
		String processName = null;
		String pid = null;
		String filePath = null;
		FileWriter fw = null;
		try {
			processName = java.lang.management.ManagementFactory.getRuntimeMXBean().getName();
			pid = processName.split("@")[0];
			filePath = SystemUtils.getUserDir() + System.getProperty("file.separator") + "PID";
			File file = new File(filePath);
			fw = new FileWriter(file);
			if (!file.exists()) {
				file.mkdirs();
			}
			fw.write(pid);
			fw.close();
			log.info("PID文件创建完成:" + filePath);
			return pid;
		} catch (Exception e) {
			log.error("生成PID文件异常, 路径:" + filePath, e);
			throw new Exception("生成PID文件异常, 路径:" + filePath, e); 
		} finally {
			try {
				if (fw != null) {
					fw.close();
				}
			} catch (IOException e) {
				log.error("生成PID文件关闭资源异常, 路径:" + filePath, e);
			}
		}
	}
}
