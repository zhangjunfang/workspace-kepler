package com.ctfo.regionfileser.util;

import java.io.FileInputStream;
import java.util.Properties;

public class ProUtil {

	
	// 获取属性文件的值
	public static String getValue(String key){
		
		String value=null;
		
		if(StaticSession.perMap.containsKey(key)){
			
			value=StaticSession.perMap.get(key);
			
		}else{
			Properties pro = new Properties();
			try {
				
				pro.load(new FileInputStream("//jdbc.properties"));
				
			} catch (Exception e) {
				// TODO: handle exception
				e.printStackTrace();
			}
			

	        value= pro.getProperty(key);
	          
	        StaticSession.perMap.put(key, value);
			
		}
		
		return value;

	}
	
	// 获取属性文件的值
	public static String getGridValue(String key){
		
		String value=null;
		
		if(StaticSession.perMap.containsKey(key)){
			
			value=StaticSession.perMap.get(key);
			
		}else{
			Properties pro = new Properties();
			try {
				
				pro.load(new FileInputStream("src/system.properties"));
				
			} catch (Exception e) {
				// TODO: handle exception
				e.printStackTrace();
			}
			
	        value= pro.getProperty(key);
	          
	        StaticSession.perMap.put(key, value);
			
		}
		
		return value;

	}
}
