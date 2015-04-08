package com.caits.analysisserver.database;

import java.util.concurrent.ConcurrentHashMap;

public class SQLPool {
	
	private final static SQLPool sqlPool = new SQLPool();
	
	public static SQLPool getinstance(){
		return sqlPool;
	}
	
	private ConcurrentHashMap<String,String> sqlMap = new ConcurrentHashMap<String,String>();
	
	/****
	 * 
	 * @param key SQL名称
	 * @param value SQL
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
