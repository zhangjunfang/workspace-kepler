package com.ocean.distributedcache;

import com.ocean.BeanContext;

public class CacheFacadeDemo {
	public static void main(String[] args) {
		BeanContext.startCacheFacade();
		System.out.println("CacheFacade is ok...");
	}
}