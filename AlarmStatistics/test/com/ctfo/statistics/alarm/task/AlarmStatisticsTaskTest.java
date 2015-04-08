package com.ctfo.statistics.alarm.task;

import static org.junit.Assert.*;

import org.junit.Test;

import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.job.AlarmStatisticsJob;

public class AlarmStatisticsTaskTest {
	
	public AlarmStatisticsTaskTest(){
		try {
			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace/trank/AlarmStatistics/src/config.xml", "E:/WorkSpace/trank/AlarmStatistics/src/system.properties"});
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	@Test
	public void testExecute() {
		try {
			Thread.sleep(7000); 
			AlarmStatisticsJob at = new AlarmStatisticsJob();
			at.execute(null); 
			Thread.sleep(20000); 
		} catch (Exception e) {
			e.printStackTrace();
			fail("Not yet implemented");
		}
	}

}
