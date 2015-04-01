package com.ocean.distributedcache;

import com.ocean.BeanContext;
import com.ocean.CacheLocal;
import com.ocean.ParkLocal;

public class CachePutDemo {
	public static void putSmallCache(String[] keyArray) {
		ParkLocal pl = BeanContext.getPark();
		pl.create("cache", "keyArray", keyArray);
	}

	public static String[] putBigCache() {
		CacheLocal cc = BeanContext.getCache();
		String[] keyArray = new String[100];
		for (int i = 0; i < 100; i++)
			keyArray[i] = cc.add("key", "value" + i);
		return keyArray;
	}

	public static void main(String[] args) {
		String[] keyArray = putBigCache();
		putSmallCache(keyArray);
	}
}