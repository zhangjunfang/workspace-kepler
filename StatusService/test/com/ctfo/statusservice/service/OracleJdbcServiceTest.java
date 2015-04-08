package com.ctfo.statusservice.service;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.statusservice.model.AlarmEnd;
import com.ctfo.statusservice.model.AlarmStart;
import com.ctfo.statusservice.model.OracleProperties;

public class OracleJdbcServiceTest {
	ApplicationContext ac = null;
	OracleProperties oracleProperties = null;
	OracleJdbcService service = null;
	
	public OracleJdbcServiceTest(){
		ac = new ClassPathXmlApplicationContext("spring-dataaccess.xml");
		oracleProperties = (OracleProperties) ac.getBean("oracleProperties");
		service = new OracleJdbcService(oracleProperties);
	}
	
	@Test
	public void testOrgParentSync() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		os.orgParentSync();
	}
	
	@Test
	public void testInitAllVehilceCache() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		os.initAllVehilceCache();
	}
	
	@Test
	public void testUpdate3GPhotoVehicleInfo() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		os.update3GPhotoVehicleInfo();
	}
	/**
	 * 测试更新车辆增量信息
	 */
	@Test
	public void testUpdateVehilceCache() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		long time = System.currentTimeMillis() - 600000;
		time = 1390706915000l;
		os.updateVehilceCache(time);
	}
	
	@Test
	public void testSaveAlarmStart() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		AlarmStart alarmStart = new AlarmStart();
		alarmStart.setAlarmId(UUID.randomUUID().toString().replace("-", ""));
		alarmStart.setVid("12345");
		alarmStart.setUtc(System.currentTimeMillis());
		alarmStart.setLon(0l);
		alarmStart.setLat(0l);
		alarmStart.setMaplon(0l);
		alarmStart.setMaplat(0l);
		alarmStart.setElevation(0);
		alarmStart.setDirection(0);
		alarmStart.setGpsSpeed(0);
		alarmStart.setMileage(0l); 
		alarmStart.setOilTotal(0l);
		alarmStart.setAlarmCode("2");
		alarmStart.setSysUtc(System.currentTimeMillis());
		alarmStart.setAlarmStatus(1);
		alarmStart.setAlarmStartUtc(System.currentTimeMillis());
		alarmStart.setAlarmDriver("1");
		alarmStart.setPlate("测试数据");
		alarmStart.setAlarmLevel("1");
		alarmStart.setBaseStatus("0");
		alarmStart.setExtendStatus("0");
		alarmStart.setAlarmAdded("0");
		alarmStart.setTeamId("12345");
		alarmStart.setTeamName("测试车队");
		alarmStart.setEntId("67890");
		alarmStart.setEntName("测试组织");
		List<AlarmStart> list = new ArrayList<AlarmStart>();
		list.add(alarmStart);
		System.out.println("AlarmId:"+ alarmStart.getAlarmId()); 
		os.saveAlarmStart(list, 1);
	}
	@Test
	public void testSaveAlarmEnd() {
		OracleProperties op = (OracleProperties) ac.getBean("oracleProperties");
		OracleJdbcService os = new OracleJdbcService(op);
		AlarmEnd alarmEnd = new AlarmEnd();
		alarmEnd.setAlarmId("7917c813a00842218344a98332140ceb");
		alarmEnd.setLon(0l);
		alarmEnd.setLat(0l);
		alarmEnd.setMaplon(0l);
		alarmEnd.setMaplat(0l);
		alarmEnd.setElevation(0l);
		alarmEnd.setDirection(0);
		alarmEnd.setGpsSpeed(0);
		alarmEnd.setMileage(0l); 
		alarmEnd.setOilTotal(0l);
		alarmEnd.setSysUtc(System.currentTimeMillis());
		alarmEnd.setEndUtc(System.currentTimeMillis());
		alarmEnd.setAlarmAdded("报警附加信息");
		alarmEnd.setMaxRpm(1000);
		alarmEnd.setMaxSpeed(120);
		alarmEnd.setAverageSpeed(110);
		
		List<AlarmEnd> list = new ArrayList<AlarmEnd>();
		list.add(alarmEnd);
		System.out.println("AlarmId:"+ alarmEnd.getAlarmId()); 
		os.saveAlarmEnd(list, 1);
	}
}
