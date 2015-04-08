package com.caits.analysisserver.bean;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;



public class StaPool {
	private static Map<String,Map<String, VehicleAlarm>>  alarmPool = new  HashMap<String,Map<String, VehicleAlarm>>();
	
	private static List<String> countList = new ArrayList<String>();
	
	/**
	 * 表示文件分析任务是否正在运行（true），如果正在运行则不能执行其他任务
	 * true 运行 false 未运行
	 */
	public static boolean isAnalyserFile=false;

	
	public static  Map<String, VehicleAlarm> getAlarm(String vid){
		synchronized(alarmPool){
			if(alarmPool.containsKey(vid)){
				return alarmPool.remove(vid);
			}
		}
		return null;
	}
	
	public static void addAlarm(String vid,  Map<String, VehicleAlarm> v){
		synchronized(alarmPool){
			alarmPool.put(vid, v);
		}
	}
	
	public static void addCountListVid(String vid){
		synchronized (countList) {
			countList.add(vid);
		}
	}
	
	public static int getCountListSize(){
		return countList.size();
	}
	
	public static void clearCountList(){
		countList.clear();
	}
	
}
