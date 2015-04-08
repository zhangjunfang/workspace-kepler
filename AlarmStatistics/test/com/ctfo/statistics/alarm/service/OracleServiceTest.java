package com.ctfo.statistics.alarm.service;

import org.junit.Test;

import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.dao.OracleConnectionPool;
import com.ctfo.statistics.alarm.dao.RedisConnectionPool;
import com.ctfo.statistics.alarm.model.StatisticsParma;

public class OracleServiceTest {
	public OracleServiceTest(){
		try {
			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace/trank/AlarmStatistics/src/config.xml", "E:/WorkSpace/trank/AlarmStatistics/src/system.properties"});
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
			OracleService.init();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	@Test
	public void testQueryAlarmLevelSetting(){
		try {
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11-10");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			OracleService.queryAlarmLevelSetting(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 告警日趋势
	 */
	@Test
	public void testStatisticsAlarmLevelDay(){
		try { 
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11-10");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			OracleService.statisticsAlarmLevelDay(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 告警周趋势
	 */
	@Test
	public void testStatisticsAlarmLevelWeek(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			parma.setLastWeekUtc(1412913600000l);
			parma.setLastWeekStartUtc(1412870400000l);
			parma.setLastWeekEndUtc(1412956799000l);
			parma.setWeekStatistics(true);  
			parma.setWeek(11);
			parma.setYear(2014);
			OracleService.statisticsAlarmLevelWeek(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 告警月趋势
	 */
	@Test
	public void testStatisticsAlarmLevelMonth(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			parma.setLastMonthUtc(1412913600000l);
			parma.setLastMonthStartUtc(1412870400000l);
			parma.setLastMonthEndUtc(1412956799000l);
			parma.setMonthStatistics(true);  
			parma.setMonth(11);
			parma.setYear(2014);
			OracleService.statisticsAlarmLevelMonth(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 超速告警日统计
	 */
	@Test
	public void testStatisticsOverspeedDay(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			OracleService.statisticsOverspeedDay(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 超速告警日统计
	 */
	@Test
	public void testStatisticsOverspeedMonth(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			parma.setLastMonthUtc(1412913600000l);
			parma.setLastMonthStartUtc(1412870400000l);
			parma.setLastMonthEndUtc(1412956799000l);
			parma.setMonthStatistics(true);  
			parma.setMonth(11);
			parma.setYear(2014);
			OracleService.statisticsOverspeedMonth(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 告警类型日统计
	 */
	@Test
	public void testStatisticsAlarmTypeDay(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			OracleService.statisticsAlarmTypeDay(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 告警类型月统计
	 */
	@Test
	public void testStatisticsAlarmTypeMonth(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			parma.setLastMonthUtc(1412913600000l);
			parma.setLastMonthStartUtc(1412870400000l);
			parma.setLastMonthEndUtc(1412956799000l);
			parma.setMonthStatistics(true);  
			parma.setMonth(11);
			parma.setYear(2014);
			OracleService.statisticsAlarmTypeMonth(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 运营违规日统计
	 */
	@Test
	public void testStatisticsOperationsViolationDay(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			OracleService.statisticsOperationsViolationDay(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	/**
	 * 运营违规月统计
	 */
	@Test
	public void testStatisticsOperationsViolationMonth(){
		try {  
			StatisticsParma parma = new StatisticsParma();
			parma.setStatisticsDayStr("2014-11");
			parma.setCurrentUtc(1412913600000l);
			parma.setStartUtc(1412870400000l);
			parma.setEndUtc(1412956799000l); 
			parma.setLastMonthUtc(1412913600000l);
			parma.setLastMonthStartUtc(1412870400000l);
			parma.setLastMonthEndUtc(1412956799000l);
			parma.setMonthStatistics(true);  
			parma.setMonth(11);
			parma.setYear(2014);
			OracleService.statisticsOperationsViolationMonth(parma);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
