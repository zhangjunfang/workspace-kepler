package com.ctfo.mcc.task;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.model.DispatchRules;
import com.ctfo.mcc.service.OracleService;
import com.ctfo.mcc.service.SendDispatchService;
import com.ctfo.mcc.utils.TaskAdapter;

/**
 * 定时发送调度信息规则查询服务
 * 
 */
public class DispatchRulesQuery extends TaskAdapter {
	private static Logger logger = LoggerFactory.getLogger(DispatchRulesQuery.class);

	private static SimpleDateFormat time = new SimpleDateFormat("HH:mm");
	
	@Override
	public void init() {
		logger.info("DispatchRulesQuery - 启动完成!"); 
	}

	@Override
	public void execute() {
		try {
			long start = System.currentTimeMillis();
			List<DispatchRules> list = OracleService.queryDispatchRules();
			int index = 0;
			int valid = 0;
			int expired = 0;
			if (list != null && !list.isEmpty()) {
				logger.debug("查询到有效规则:[{}]条, ", list.size());
				List<String> expiredId = new ArrayList<String>();
				for (DispatchRules rule : list) {
					if(logger.isDebugEnabled()){
						SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
						String oracleTime = sdf.format(new Date(rule.getOracleSysUtc()));
						String startTime = sdf.format(new Date(rule.getStartUtc()));
						String endTime = sdf.format(new Date(rule.getEndUtc()));
						String off = "不离线发送";
						if(rule.getIsOffline() == 0){
							off = "离线发送";
						}
						logger.debug("查询到{}创建的规则[{}], oracle当前时间[{}],规则开始时间[{}], 规则结束时间[{}] ,发送时间:[{}], {}, 发送周期[{}], 离线时间[{}], 信息显示类型:[{}]", rule.getCreateBy(),rule.getContent(), oracleTime, startTime, endTime,rule.getSendTime(), off, rule.getSendCycle(), rule.getOfflineCycle(), rule.getType());
					}
					index++;
					// 规则结束 - 返回并设置无效
					if (rule.getEndUtc() < rule.getOracleSysUtc()) {
						// 设置规则为无效
						expiredId.add(rule.getId());
						expired++;
						continue;
					}
					// 规则没有开始 - 返回
					if (rule.getStartUtc() > rule.getOracleSysUtc()) {
						continue;
					}
					// 判断今天是否在发送星期内
					if (inSendCycle(rule.getSendCycle())) {
						// 判断发送时间是否成立
						if (isSendTime(rule.getOracleSysUtc(), rule.getSendTime())) {
							SendDispatchService.put(rule);
							valid++;
						}
					}
				}
				if(!expiredId.isEmpty()){
					long startUpdate = System.currentTimeMillis();
					int result = OracleService.updateDispatchRules(expiredId);
					long endUpdate = System.currentTimeMillis();
					logger.info("批量更新定时调度规则无效状态结束, 更新数量[{}], 耗时:[{}]ms", result, endUpdate - startUpdate);
				}
			}
			long end = System.currentTimeMillis();
			logger.info("定时调度规则执行结束, 处理规则[{}]条, 有效规则[{}]条 , 过期规则[{}]条, 耗时:[{}]ms", index, valid, expired, end - start);
		} catch (Exception e) {
			logger.error("判断定时发送调度信息规则异常:" + e.getMessage(), e);
		}
		
	}
	private boolean isSendTime(long oracleSysTime, String sendTime) {
		String oracleSysTimeStr = time.format(new Date(oracleSysTime));
		if(sendTime == null || sendTime.length() < 1){
			return false;
		} else {
			return oracleSysTimeStr.equals(sendTime);
		}
	}


	/**
	 * 获取当前时间字符串
	 * @return
	 */
//	private String getCurrentTimeStr() {
//		return new SimpleDateFormat("HH:mm").format(new Date());
//	}

	/**
	 * 判断当天是否在发送周期内
	 * @param sendCycle
	 * @return
	 */
	@SuppressWarnings("static-access")
	private boolean inSendCycle(String sendCycle) {
		try {
			if(sendCycle == null || sendCycle.length() < 1){
				return false;
			}
			Calendar rightNow = Calendar.getInstance();
			int day = rightNow.get(rightNow.DAY_OF_WEEK);// 获取时间
			return sendCycle.contains(String.valueOf(day));
		} catch (Exception e) {
			logger.error("判断当天是否在发送周期内异常:" + e.getMessage(), e);
			return false;
		}
	}

	/**
	 * 获取小时分钟字符串表示
	 * 
	 * @param sysUtc
	 * @return
	 */
//	private String getSysTimeStr(long sysUtc) {
//		try {
//			long sysTime = System.currentTimeMillis();
//			// 判断时间合法性 - 数据库时间必须是10分钟内的时间，否则返回系统时间
//			if (sysUtc > (sysTime - 600000) && sysUtc < (sysTime + 600000)) {
//				return new SimpleDateFormat("HH:mm").format(new Date(sysUtc));
//			}
//			logger.warn("数据库时间[{}]与服务器时间[{}]差超过10分钟!", sysUtc, sysTime);
//			return new SimpleDateFormat("HH:mm").format(new Date());
//		} catch (Exception e) {
//			logger.error("获取小时分钟字符串表示异常:" + e.getMessage(), e);
//			return new SimpleDateFormat("HH:mm").format(new Date());
//		}
//	}




}
