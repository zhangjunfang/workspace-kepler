package com.ctfo.commandservice.task;

import org.junit.Test;

import com.ctfo.commandservice.util.ConfigLoader;

public class VehicleCacheTaskTest {

	public VehicleCacheTaskTest(){
		
	}
	@SuppressWarnings("static-access")
	@Test
	public void testInit() {
		try {
			String[] args = new String[]{"-d","src","start"};
			ConfigLoader.getInstance().init(args);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
