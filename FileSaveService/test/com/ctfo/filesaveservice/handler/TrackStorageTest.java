package com.ctfo.filesaveservice.handler;

import static org.junit.Assert.*;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;

public class TrackStorageTest {
	private static final Logger logger = LoggerFactory.getLogger(TrackStorageTest.class);
	
	public TrackStorageTest() throws Exception{ 
		try {
			ConfigLoader.init(new String[]{"-d", "src", ""});
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	
	@Test
	public void testRun() {
		try {
			TrackStorage ts = new TrackStorage(0,15000);
//			ts.setSubmitTime(3000); 
			ts.start();
			
			int lon = 72447154;
			int lat = 18471854;
			int index = 1;
			int temp = 0;
			for (int i = 100000; i < 100010; i++) {
				for (int j = 0; j < 2; j++) {
					index++;
					Location location = new Location();
					location.setVid(String.valueOf(j));
					temp++;
					if(temp % 3 == 0){
						location.setPath("d:/test1/test2/test3/temp/" + j + ".txt");
					} else {
						location.setPath("d:/test1/test2/test3/" + j + ".txt");
					}
					
					location.setContent((lon + index) + ":" + (lat + index)
							+ ":20140506/175741:0:0::,:72444558:18473217:0:607415:31404::11176:3::::2:880:::125:2052:0:-1:::0::::::1:-1:1399372543597\r\n");
					if(!ts.offerDataMap(location)){
						logger.info("LocalMode Save Track Error:" +JSON.toJSONString(location)); 
					}
					Thread.sleep(1); 
				}
			}
			Thread.sleep(10000); 
			
		} catch (Exception e) {
			fail("Not yet implemented");
		}
	}

	@Test
	public void testSaveLocation() {
		fail("Not yet implemented");
	}

	@Test
	public void testCacheLocation() {
		fail("Not yet implemented");
	}

	@Test
	public void testWriteToDisk() {
		fail("Not yet implemented");
	}

	@Test
	public void testCreateAndCacheFile() {
		fail("Not yet implemented");
	}

}
