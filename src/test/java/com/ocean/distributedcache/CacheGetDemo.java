package com.ocean.distributedcache;

import com.ocean.BeanContext;
import com.ocean.CacheLocal;
import com.ocean.ParkLocal;

public class CacheGetDemo {
	public static String[] getSmallCache() {
		ParkLocal pl = BeanContext.getPark();
		return (String[]) pl.get("cache", "keyArray").toObject();
	}

	public static void getBigCache(String[] keyArray) {
		CacheLocal cc = BeanContext.getCache();
		for (String k : keyArray)
			System.out.println(cc.get(k, "key"));
	}

	public static void main(String[] args) {
		String[] keyArray = getSmallCache();
		getBigCache(keyArray);
	}
}