package com.caits.analysisserver.database;

import java.util.concurrent.ConcurrentHashMap;

public class FilePool {
	private final static FilePool filePool = new FilePool();
	
	private int corssdays=0;
	
	public static FilePool getinstance(){
		return filePool;
	}
	
	private ConcurrentHashMap<String,String> fileMap = new ConcurrentHashMap<String,String>();
	
	/****
	 * 
	 * @param key 文件配置名称
	 * @param value 文件路径
	 */
	public void putFile(String key,String value){
		fileMap.put(key, value);
	}
	
	public String getFile(String key){
		if(fileMap.containsKey(key)){
			return fileMap.get(key);
		}
		return null;
	}
	
	public String getFile(Long statDateUtc,String key){
		Long currutc = System.currentTimeMillis();
		if ((currutc-statDateUtc)/(24*60*60*1000)>corssdays){
			key = "cross" + key;
		}
		if(fileMap.containsKey(key)){
			return fileMap.get(key);
		}
		return null;
	}

	public int getCorssdays() {
		return corssdays;
	}

	public void setCorssdays(int corssdays) {
		this.corssdays = corssdays;
	}
}
