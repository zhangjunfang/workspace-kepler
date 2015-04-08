package com.ctfo.filesaveservice.handler;

import java.io.File;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.util.ConfigLoader;

public class LocalBlindTest {
	private static final Logger logger = LoggerFactory.getLogger(LocalBlindTest.class);
	
	public LocalBlindTest() throws Exception{ 
		try {
			ConfigLoader.init(new String[]{"-d", "src", ""});
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	@Test
	public void testRun() {
		TrackStorage trackStorage = new TrackStorage(0,15000);
		LocalBlind lb = new LocalBlind(0, trackStorage);
		lb.start(); 
	}

	@Test
	public void testGetDirectory() {
		TrackStorage trackStorage = new TrackStorage(0,15000);
		LocalBlind lb = new LocalBlind(0, trackStorage);
		for(int i = 0; i < 10 ; i ++){
			logger.info("传入天数["+i+"]，得到目录:[" + lb.getDirectory(i, "D:\\test\\local\\track", "\\2014\\06\\19\\")+"]");
		}
	}

	@Test
	public void testGetProcessFileList() {
		TrackStorage trackStorage = new TrackStorage(0,15000);
		LocalBlind lb = new LocalBlind(0, trackStorage);
		File file = new File("D:\\test\\local\\track\\2014\\06\\19\\");
		String[] fileList = lb.getProcessFileList(file);
		for(String name : fileList){
			logger.info("文件列表:[" + name +"]");
		}
	}

	@Test
	public void testLocalDataBlind() {
		TrackStorage trackStorage = new TrackStorage(0,15000);
		LocalBlind lb = new LocalBlind(0, trackStorage);
		String directory = "D:\\test\\local\\track\\2014\\06\\19\\";
		File file = new File(directory);
		String[] fileList = lb.getProcessFileList(file);
		long s = System.currentTimeMillis();
		lb.localDataBlind(fileList, directory, "\\2014\\06\\19\\");
		logger.info("本地数据补传完成，耗时:[" + (System.currentTimeMillis() -s)+"]ms");
	}

}
