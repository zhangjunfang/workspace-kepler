package com.ctfo.trackservice.handler;

import java.util.HashMap;
import java.util.Map;

import org.junit.Test;

import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.OracleProperties;

public class TrackAnalysisThreadTest {
//	ApplicationContext ac = null;
	OracleProperties oracleProperties = null;
	OracleService service = null;
	
	public TrackAnalysisThreadTest() throws Exception{
//		ac = new ClassPathXmlApplicationContext("spring-dataaccess.xml");
//		oracleProperties = (OracleProperties) ac.getBean("oracleProperties");
		service = new OracleService();
		service.initService();
		track = new TrackAnalysisThread(0, 1600, 0);  
	} 
	TrackAnalysisThread track = null;
	
	@Test
	public void testParseBaseInfo() {
		String str = "TYPE:0,RET:0,1:69807919,2:23986097,20:1610612736,24:0,3:10,4:20140227/153049,5:17190,500:0,6:36,7:10,8:786435,9:504";
		Map<String, String> dataMap = new HashMap<String, String>();
		String[] strArray = str.split(",");
		for(String s : strArray){
			String[] sArray = s.split(",");
			dataMap.put(sArray[0], sArray[1]);
		}
		track.parseBaseInfo(dataMap);
	}

}
