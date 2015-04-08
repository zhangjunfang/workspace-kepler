package com.ctfo.informationser.util;

/**
 * 根据环境变量获取目录结构地址
 * 
 * @author 王鹏
 * 
 */
public class GetEvn {

	public static String getPath() {
		String envPath = System.getenv("WEBAPP_HOME");
		if (envPath == null || envPath.equals("")) {
			return null;
		}
		return envPath.replace("\\", "/");
	}

	public static void main(String[] args) {
		System.out.println("WEBAPP_HOME:" + GetEvn.getPath());
	}
}
