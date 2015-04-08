package com.ctfo.statistics.alarm.task;

import java.util.concurrent.Callable;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.service.OracleService;

/**
 * 统计任务
 *
 */
public class StatisticsTask implements Callable<String> {
	private static Logger log = LoggerFactory.getLogger(StatisticsTask.class);
	/**	业务代码	*/
	private int method;
	/**	名称	*/
	private String name = "统计";
	/**	统计参数	*/
	private StatisticsParma parma;
	
	public StatisticsTask(int methodNum, StatisticsParma parmas){
		this.method = methodNum;
		this.parma = parmas;
	}
	/**
	 * 线程执行方法
	 */
	@Override
	public String call() throws Exception {
		switch(method){
			case 0 :  // 统计超速告警到【车辆超速统计】
				statisticsOverspeed();
				name += "车辆超速";
				break;
			case 1 :  // 统计日周月告警级别【首页告警趋势图】
				statisticsAlarmLevel();
				name += "告警趋势图";
				break;
			case 2 :  // 统计告警大类【告警统计】
				statisticsAlarmTypeDay();
				name += "告警类型";
				break;
			case 3 :  // 统计危险驾驶【危险驾驶分析】
				statisticsDangerousDriving();
				name += "危险驾驶分析";
				break;
			case 4 :  // 统计运营违规【运营违规统计】
				statisticsOperationsViolationDay();
				name += "运营违规";
				break;
			case 5 :  // 统计驾驶员运营违规【驾驶员运营违规统计】
				statisticsOperationsViolationDriverDay();
				name += "驾驶员运营违规";
				break;
			case 6 :  // 统计驾驶员危险驾驶【驾驶员危险驾驶】
				statisticsDangerousDrivingDriver();
				name += "驾驶员危险驾驶";
				break;
			default :
				log.error("StatisticsTask - 初始化统计任务异常!");
				name += "任务异常";
				break;
		} 
		return name;
	}

	/**
	 * 超速告警统计
	 * TODO
	 */
	private void statisticsOverspeed() {
		try {
//			统计日统计
			OracleService.statisticsOverspeedDay(parma); 
//			进行月统计
			if(parma.isMonthStatistics()){
				OracleService.statisticsOverspeedMonth(parma);
			}
		} catch (Exception e) { 
			log.error("统计超速告警异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 告警级别统计【首页告警趋势图】
	 * TODO
	 */
	private void statisticsAlarmLevel() {
		try {
//			统计日统计
			OracleService.statisticsAlarmLevelDay(parma);
//			进行周统计
			if(parma.isWeekStatistics()){
				OracleService.statisticsAlarmLevelWeek(parma);
			}
//			进行月统计
			if(parma.isMonthStatistics()){
				OracleService.statisticsAlarmLevelMonth(parma);
			}
		} catch (Exception e) { 
			log.error("统计超速告警异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 告警类型统计【告警统计】模块
	 * TODO
	 */
	private void statisticsAlarmTypeDay() {
		try {
//			日统计
			OracleService.statisticsAlarmTypeDay(parma);
//			月统计
			if(parma.isMonthStatistics()){
				OracleService.statisticsAlarmTypeMonth(parma);
			}
		} catch (Exception e) {
			log.error("告警类型日统计异常:" + e.getMessage(), e); 
		}
	}
	/**
	 * 运营违规日统计
	 * TODO
	 */
	private void statisticsOperationsViolationDay() {
//		日统计
		OracleService.statisticsOperationsViolationDay(parma);
//		月统计
		if(parma.isMonthStatistics()){
			OracleService.statisticsOperationsViolationMonth(parma);
		}
	}
	/**
	 * 驾驶员运营违规统计
	 */
	private void statisticsOperationsViolationDriverDay() {
//		日统计
		OracleService.statisticsOperationsViolationDriverDay(parma);
//		月统计
		if(parma.isMonthStatistics()){
			OracleService.statisticsOperationsViolationDriverMonth(parma);
		}
	}
	/**
	 * 危险驾驶统计【危险驾驶统计】
	 * TODO
	 */
	private void statisticsDangerousDriving() {
//		日统计
		OracleService.statisticsDangerousDrivingDay(parma);
//		月统计
		if(parma.isMonthStatistics()){
			OracleService.statisticsDangerousDrivingMonth(parma);
		}
	}
	/**
	 * 驾驶员危险驾驶统计【驾驶员危险驾驶统计】
	 * TODO
	 */
	private void statisticsDangerousDrivingDriver() {
//		日统计
		OracleService.statisticsDangerousDrivingDriverDay(parma);
//		月统计
		if(parma.isMonthStatistics()){
			OracleService.statisticsDangerousDrivingDriverMonth(parma);
		}
	}
}
