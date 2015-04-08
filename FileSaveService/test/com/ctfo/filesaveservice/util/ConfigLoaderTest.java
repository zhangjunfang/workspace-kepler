package com.ctfo.filesaveservice.util;

import org.junit.Test;

public class ConfigLoaderTest {

	@SuppressWarnings("static-access")
	@Test
	public void testInit() {
		ConfigLoader configLoader = ConfigLoader.getInstance();
		try {
			configLoader.init(new String[]{"-d" ,"src", "start"});
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
