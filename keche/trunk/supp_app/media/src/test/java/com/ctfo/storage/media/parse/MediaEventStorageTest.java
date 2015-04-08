/**
 * 
 */
package com.ctfo.storage.media.parse;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.dao.HBaseDataSource;

/**
 * @author zjhl
 *
 */
public class MediaEventStorageTest {
	private static final Logger log = LoggerFactory.getLogger(MediaEventStorageTest.class);
	
	HBaseDataSource dataSource = null;
	
	public MediaEventStorageTest() throws Exception{ 
//		dataSource = HBaseDataSource.getInstance();
//		dataSource.setQuorum("zjhl1");
//		dataSource.setPort("2181");
//		dataSource.init();
	}
	/**
	 * 测试	多媒体时间存储
	 * @throws InterruptedException 
	 */
	@Test
	public void testRun() throws InterruptedException { 
		long s = System.currentTimeMillis();
		int index = 0;
		
//		MediaEventStorage storage = new MediaEventStorage();
//		storage.start();
//		for(int i = 0; i < 100; i++){
//			index++;
//			MediaEvent media = new MediaEvent();
//			media.setPlate("123456789");
//			media.setMediaType(0);
//			media.setChannelId(1);
//			media.setEventTime(System.currentTimeMillis());
//			media.setEventType(1);
//			media.setMediaFormat(0); 
//			media.setSysTime(System.currentTimeMillis()); 
//			media.setTakeSerial("" + System.currentTimeMillis());
//			storage.put(media);
//		}
//		Thread.sleep(5000);
		
		log.info("存储多媒体数据[{}]条完成，耗时[{}]ms", index,  System.currentTimeMillis() - s);
	}

}
