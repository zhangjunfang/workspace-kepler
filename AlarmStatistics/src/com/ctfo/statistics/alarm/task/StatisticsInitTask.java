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
public class StatisticsInitTask implements Callable<String> {
	private static Logger log = LoggerFactory.getLogger(StatisticsInitTask.class);
	/**	业务代码	*/
	private int method;
	/**	名称	*/
	private String name = "加载";
	/**	统计参数	*/
	private StatisticsParma parma;
	
	public StatisticsInitTask(int methodNum, StatisticsParma parmas){
		this.method = methodNum;
		this.parma = parmas;
	}
	@Override
	public String call() throws Exception {
		switch(method){
			case 0 :  //  报警父级编码获取
				OracleService.initAlarmParentCode();
				name += "报警父级编码";
				break;
			case 1 :  // 加载车辆信息
				OracleService.initVehicleInfo();
				name += "车辆信息";
				break;
			case 2 :  // 先加载限速设置，再加载超速轨迹 - 超速规则中需要用到限速设置
				OracleService.initSpeedLimitSettings();	// 限速设置
				OracleService.initOverspeedRules(parma);// 超速规则
				name += "限速设置";
				break;
			case 3 :  // 疲劳驾驶规则
				OracleService.initFatigueRules(parma);
				name += "疲劳驾驶规则";
				break;
			case 4 :  // 夜间非法运营规则 
				OracleService.initNightIllegalOperationsRules(parma);
				name += "夜间非法运营规则";
				break;
			case 5 :  
				OracleService.queryAlarmLevelSetting(parma);
				name += "告警级别设置";
				break;
			case 6 :  // 导入偷漏油告警（平台软告警）
				OracleService.importOilAlarm(parma);
				name += "偷漏油告警";
				break;
			case 7 :  // 导入离线告警（平台软告警）
				OracleService.importOfflineAlarm(parma);
				name += "离线告警";
				break;
			case 8 :  // 导入围栏告警（平台软告警）
				OracleService.importAreaAlarm(parma);
				name += "围栏告警";
				break;
			case 9 :  // 导入超速告警（平台软告警）
				OracleService.importOverspeedAlarm(parma);
				name += "超速告警";
				break;
			case 10 :  // 缓存驾驶员上下线记录
				OracleService.queryDriverOnoffline(parma);
				name += "驾驶员上下线记录";
				break;
			default :
				log.error("StatisticsInitTask - 初始化统计任务异常!");
				name += "任务异常";
				break;
		} 
		return name;
	}

}
