package com.ctfo.commandservice.util;

import static org.junit.Assert.*;

import org.junit.Test;

import com.ctfo.commandservice.model.OilInfo;

public class UtilsTest {

	@Test
	public void testGetBasicInfoByteArrayInt() {
		fail("Not yet implemented");
	}

	@Test
	public void testGetBasicInfoByteArray() {
		String base64Str = "AQAAAAAAAAAAAAAAAAAAFAcCEDgGBQA0gABkChQ=";
		byte[] buf = Base64_URl.base64DecodeToArray(base64Str);
		OilInfo result = new OilInfo();
		System.out.println(result.toString());
		
		if(buf[0] == 0x01){
//			result = Utils.getBasicInfo(base64Str);
		}
		System.out.println(result); 
	}

}
