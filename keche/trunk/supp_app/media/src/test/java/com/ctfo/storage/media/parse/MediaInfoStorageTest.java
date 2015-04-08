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
public class MediaInfoStorageTest {
	private static final Logger log = LoggerFactory.getLogger(MediaInfoStorageTest.class);
	
	HBaseDataSource dataSource = null;
	
	public MediaInfoStorageTest() throws Exception{ 
//		dataSource = HBaseDataSource.getInstance();
//		dataSource.setQuorum("zjhl1");
//		dataSource.setPort("2181");
//		dataSource.init();
	}
	/**
	 * 测试	运行状态
	 * @throws InterruptedException 
	 */
	@Test
	public void testRun() throws InterruptedException {
		long s = System.currentTimeMillis();
		int index = 0;
		
//		MediaInfoStorage storage = new MediaInfoStorage();
//		storage.start();
//		for(int i = 0; i < 100; i++){
//			index++;
//			MediaInfo media = new MediaInfo();
//			media.setMediaId(UUID.randomUUID().toString());
//			media.setPlate("123456789");
//			media.setVehicleType("2");
//			media.setPhoneNumber("13311331133");
//			media.setMediaType(0);
//			media.setMediaFormat(0);
//			media.setEventType(1);
//			media.setEventUpTime(System.currentTimeMillis());
//			media.setMediaUrl("http://192.168.100.52/pics/test.jpg");
//			media.setChannelId(1); 
//			media.setMediaSize(12345);
//			media.setImageUnit(4);
//			media.setSampleRate("44KMz");
//			media.setLon(65332432);
//			media.setLat(76311111);
//			media.setMaplon(1111111);
//			media.setMaplat(7654321);
//			media.setElevation(500);
//			media.setDirection(90);
//			media.setGpsSpeed(90);
//			media.setStatusCode(",1,2,3,");
//			media.setAlarmCode(",0,");
//			media.setSysTime(System.currentTimeMillis()); 
//			media.setIsOverload(0);
//			media.setEventStatus(0);
//			media.setEnableFlag(1);
//			media.setSeq("" + System.nanoTime());
//			media.setSendUserId("12345");
//			media.setMediaDataId(54321);
//			media.setEventTriggerTime(System.currentTimeMillis());
//			media.setReadFlag(1);
//			media.setOverloadNum(10);
//			media.setOverloadById(22222);
//			media.setOverloadTime(System.currentTimeMillis());
//			media.setDevType(1);
//			media.setMemo("备注信息"); 
//			storage.put(media);
//		}
//		Thread.sleep(5000);
		
		log.info("存储多媒体数据[{}]条完成，耗时[{}]ms", index,  System.currentTimeMillis() - s);
	}

}
