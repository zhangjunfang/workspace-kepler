package com.ctfo.analy.util;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * 读取配置文件信息
 * @author LiangJian
 * 2012-10-13 16:56:51
 */
public enum PropertiesUtil {
	//枚举方式单例模式
	PROPERTIES;
	private PropertiesUtil(){}
	
	/**
	 * 获取配置文件中的值
	 * @param path 配置文件路径
	 * @param key 配置文件中的key
	 * @return
	 */
	public String read(String path,String key){
		//读取配置文件
		InputStream in = this.getClass().getClassLoader().getResourceAsStream(path);
		Properties p = new Properties();
		try {
			p.load(in);
		} catch (IOException e) {
			e.printStackTrace();
		}
		//取得配置文件中的值
		return p.getProperty(key);
	}
	
	public static void main(String[] args) {
		String value = PropertiesUtil.PROPERTIES.read("system.properties", "RGCService_IP");
		System.out.println(value);
	}
}
