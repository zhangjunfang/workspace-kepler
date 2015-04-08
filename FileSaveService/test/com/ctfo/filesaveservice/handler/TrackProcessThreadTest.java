package com.ctfo.filesaveservice.handler;

import static org.junit.Assert.*;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;

public class TrackProcessThreadTest {
	private static final Logger logger = LoggerFactory.getLogger(TrackProcessThreadTest.class);
	
	public TrackProcessThreadTest() throws Exception{ 
		try {
			ConfigLoader.init(new String[]{"-d", "src", ""});
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	
	@Test
	public void testRun() {
		try {
			TrackProcessThread t = new TrackProcessThread(0);
			t.start();  
			
			Map<String, String> map = new ConcurrentHashMap<String, String>();
			map.put("vid", "123"); // GPS时间2
			map.put(Constant.MAPLON, "72447166"); // 经度0
			map.put(Constant.MAPLAT, "18471866"); // 纬度1
			map.put("4", "20140616/123333"); // GPS时间2
			map.put("3", "23"); // GPS 速度3
			map.put("5", "12"); // 正北方向夹角4
			map.put("26", "3"); // 车辆状态5
			map.put(Constant.FILEALARMCODE, ",1,"); // 报警编码6
			map.put("1", "72447166"); // 经度7
			map.put("2", "18471866"); // 纬度8
			
			t.putDataMap(map);
			
			Thread.sleep(10000); 
			
		} catch (Exception e) {
			fail("Not yet implemented");
		}
	}

	@Test
	public void testProcessTrackFile() {
		try {
			Map<String, String> map = new ConcurrentHashMap<String, String>();
			map.put("vid", "123"); // GPS时间2
			map.put(Constant.MAPLON, "72447166"); // 经度0
			map.put(Constant.MAPLAT, "18471866"); // 纬度1
			map.put("4", "20140616/123333"); // GPS时间2
			map.put("3", "23"); // GPS 速度3
			map.put("5", "12"); // 正北方向夹角4
			map.put("26", "3"); // 车辆状态5
			map.put(Constant.FILEALARMCODE, ",1,"); // 报警编码6
			map.put("1", "72447166"); // 经度7
			map.put("2", "18471866"); // 纬度8
			map.put("speedfrom", "1"); // 
			TrackProcessThread t = new TrackProcessThread(0);
			Location location = t.processTrackFile(map);
			logger.info("path:{}, content:{}", location.getPath(), location.getContent());
			assertEquals(location.getVid(), "123");
			Thread.sleep(10000);
		} catch (Exception e) {
			fail("Not yet implemented");
		}
	}

	@Test
	public void testGetDateDirectory() {
		TrackProcessThread t = new TrackProcessThread(0);
		System.out.println(t.getDateDirectory("20140616/184211")); 
		assertEquals(t.getDateDirectory("20140616/184211"), "//2014//06//16//");
	}

}
