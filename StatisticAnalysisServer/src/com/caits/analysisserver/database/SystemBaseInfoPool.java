package com.caits.analysisserver.database;

import java.util.concurrent.ConcurrentHashMap;

import com.caits.analysisserver.bean.SystemBaseInfo;

public class SystemBaseInfoPool {
	private final static SystemBaseInfoPool systemBaseInfoPool = new SystemBaseInfoPool();
	
	private ConcurrentHashMap<String,SystemBaseInfo> baseInfoMap = new ConcurrentHashMap<String,SystemBaseInfo>();
	
	public static SystemBaseInfoPool getinstance(){
		return systemBaseInfoPool;
	}

	public SystemBaseInfo getBaseInfoMap(String key) {
		if(this.baseInfoMap.containsKey(key)){
			return this.baseInfoMap.get(key);
		}
		return null;
	}

	public void putBaseInfoMap(String key, SystemBaseInfo sysBaseInfo) {
		this.baseInfoMap.put(key, sysBaseInfo);
	}
}