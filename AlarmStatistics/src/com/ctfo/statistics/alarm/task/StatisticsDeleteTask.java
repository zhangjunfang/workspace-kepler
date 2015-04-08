package com.ctfo.statistics.alarm.task;

import java.util.concurrent.Callable;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.service.OracleService;

/**
 * 统计数据删除任务
 *
 */
public class StatisticsDeleteTask implements Callable<String> {
	private static Logger log = LoggerFactory.getLogger(StatisticsDeleteTask.class);
	/**	业务代码	*/
	private int method;
	/**	名称	*/
	private String name = "删除";
	/**	统计参数	*/
	private StatisticsParma parma;
	
	public StatisticsDeleteTask(int methodNum, StatisticsParma parmas){
		this.method = methodNum;
		this.parma = parmas;
	}
	@Override
	public String call() throws Exception {
		switch(method){
			case 0 :  //  告警明细
				OracleService.deleteAlarmDetail(parma);
				name += "告警日明细";
				break;
			case 1 :  // 超速日统计
				OracleService.deleteOverspeedStatisticsDay(parma);
				name += "超速日统计";
				break;
			case 2 :  // 告警级别日统计
				OracleService.deleteAlarmLevelStatisticsDay(parma);
				name += "告警级别日统计";
				break;
			case 3 :  // 告警类型日统计
				OracleService.deleteAlarmTypeStatisticsDay(parma);
				name += "告警类型日统计";
				break;
			case 4 :  // 危险驾驶日统计
				OracleService.deleteDangerousDrivingStatisticsDay(parma);
				name += "危险驾驶日统计";
				break;
			case 5 :  // 运营违规日统计 
				OracleService.deleteViolationStatisticsDay(parma);
				name += "运营违规日统计";
				break;
//			case 6 :  // 车辆当天总里程 
//				OracleService.deleteLastTotalMileage();
//				name += "车辆当天总里程";
//				break;
			case 6 :  // 驾驶员运营违规统计 
				OracleService.deleteViolationStatisticsDriver(parma);
				name += "驾驶员运营违规统计";
				break;
			case 7 :  // 驾驶员危险驾驶统计 
				OracleService.deleteDangerousDrivingStatisticsDriver(parma);
				name += "驾驶员危险驾驶统计";
				break;
			default :
				log.error("StatisticsInitTask - 初始化统计任务异常!");
				name += "任务异常";
				break;
		} 
		return name;
	}

}
