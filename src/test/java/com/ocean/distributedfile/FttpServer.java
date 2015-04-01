package com.ocean.distributedfile;

import com.ocean.BeanContext;

public class FttpServer {
	public static void main(String[] args) {
		BeanContext.startFttpServer(args[0]);
	}
}