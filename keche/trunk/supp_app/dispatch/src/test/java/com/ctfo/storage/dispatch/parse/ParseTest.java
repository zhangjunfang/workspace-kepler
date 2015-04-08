/**
 * 
 */
package com.ctfo.storage.dispatch.parse;

import static org.junit.Assert.*;

import org.junit.Test;

import com.ctfo.storage.dispatch.model.TbDvr3G;
import com.ctfo.storage.dispatch.parse.Parse;

/**
 * @author zjhl
 *
 */
public class ParseTest {

	/**
	 * 测试	运行
	 */
	@Test
	public void testRun() {
		Parse parse = new Parse();
		parse.start(); 
		TbDvr3G dvr = new TbDvr3G();
		dvr.setDvrMakerCode("E001");
//		dvr.setDvrserId(System.currentTimeMillis());
		dvr.setDvrSerIp("192.168.1.111");
		dvr.setDvrserName("haikang_test");
		dvr.setDvrSerPort("12345");
		parse.put("CAITS 0_1 E001_15249674417 0 U_REPT {TYPE:0,RET:0,1:72296421,2:18035508,20:0,21:0,210:-1,216:-1,218:0,24:2880,3:0,4:20140520/095229,5:15,500:0,503:-1,504:-1,520:1,550:2,551:0,552:9,553:0,554:513675,555:512027,6:9,7:0,8:2}");
		assertTrue(true); 
	}


}
