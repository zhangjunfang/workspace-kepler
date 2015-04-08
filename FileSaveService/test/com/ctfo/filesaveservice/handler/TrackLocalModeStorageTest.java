package com.ctfo.filesaveservice.handler;

import static org.junit.Assert.fail;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.model.Location;
import com.ctfo.filesaveservice.util.ConfigLoader;

public class TrackLocalModeStorageTest {
	private static final Logger logger = LoggerFactory.getLogger(TrackLocalModeStorageTest.class);
	
	public TrackLocalModeStorageTest() throws Exception{ 
		try {
			ConfigLoader.init(new String[]{"-d", "src", ""});
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	
	@Test
	public void testRun() {
		try {
			TrackLocalModeStorage tlm = new TrackLocalModeStorage(0);
//			tlm.setLocalModePath("d:/test1/test2/test3"); 
//			tlm.setBatchSize(72000); 
//			tlm.setSubmitTime(3000); 
			tlm.init();
			int lon = 72447154;
			int lat = 18471854;
			for (int i = 100000; i < 130000; i++) {
				for (int j = 0; j < 20; j++) {
					Location location = new Location();
					location.setVid(String.valueOf(i));
					location.setPath("d:/test1/test2/test3/LocalMode-" + j + ".txt");
					location.setContent((lon + j) + ":" + (lat + j)
							+ ":20140506/175741:0:0::,:72444558:18473217:0:607415:31404::11176:3::::2:880:::125:2052:0:-1:::0::::::1:-1:1399372543597");
					if(!tlm.offerDataMap(location)){
						logger.info("LocalMode Save Track Error:" +JSON.toJSONString(location)); 
					}
				}
			}
			Thread.sleep(50000); 
			tlm.getQueueSize();
		} catch (Exception e) {
			e.printStackTrace();
			fail("" + e.getMessage());
		}
	}

}
