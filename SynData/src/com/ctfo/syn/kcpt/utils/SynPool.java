package com.ctfo.syn.kcpt.utils;

import java.util.concurrent.ConcurrentHashMap;



public class SynPool {
	private final static SynPool synPool = new SynPool();
	
	private ConcurrentHashMap<String,String> sqlMap = new ConcurrentHashMap<String,String>();
	
	public static SynPool getinstance(){
		return synPool;
	}
	
	/****
	 * 
	 * @param key 
	 * @param value
	 */
	public void putSql(String key,String value){
		sqlMap.put(key, value);
	}
	
	public String getSql(String key){
		if(sqlMap.containsKey(key)){
			return sqlMap.get(key);
		}
		return null;
	}
}
