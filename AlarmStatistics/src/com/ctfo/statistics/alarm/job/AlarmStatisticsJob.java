package com.ctfo.statistics.alarm.job;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.FutureTask;

import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.common.Cache;
import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.FileNameFilter;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.model.FutureResult;
import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.task.AlarmAnalysisTask;
import com.ctfo.statistics.alarm.task.StatisticsDeleteTask;
import com.ctfo.statistics.alarm.task.StatisticsInitTask;
import com.ctfo.statistics.alarm.task.StatisticsTask;

public class AlarmStatisticsJob implements Job {
	private static Logger log = LoggerFactory.getLogger(AlarmStatisticsJob.class);
	/**	轨迹路径	*/
	private String path;
	/**	业务处理线程数	*/
	private int analysisThreadSize;
	/**	统计状态显示间隔	*/
	private int statisticsStateInterval = 60000;
	/**	是否重跑	*/
	private boolean rerun;
	
	public AlarmStatisticsJob(){
		try {
			path = ConfigLoader.config.get("trackPath");
			rerun = Boolean.parseBoolean(ConfigLoader.config.get("rerun"));
			analysisThreadSize = Integer.parseInt(ConfigLoader.config.get("trackReaderSize"));
			statisticsStateInterval = Integer.parseInt(ConfigLoader.config.get("statisticsStateInterval"));
			log.info("告警统计任务启动 - 路径[{}], 处理线程[{}]个, 状态显示间隔[{}]ms", path, analysisThreadSize, statisticsStateInterval);
		} catch (Exception e) { 
			log.error("告警统计任务启动异常:" + e.getMessage(), e);
		}
	}
	
	public void execute(JobExecutionContext context) throws JobExecutionException {
		long begin = System.currentTimeMillis();
		long b1 = begin;
		long b2 = begin;
		long b3 = begin;
		long b4 = begin;
		try {
			if(rerun){
				log.info("重跑模式启动, 定时任务停止运行-|-|-|");
				return ;
			}
			log.info("定时任务开始运行->->->");
/** 1. 加载统计参数			*/
			StatisticsParma parma = getStatisticsParma();
			b1 = System.currentTimeMillis();
			log.info("【1】加载统计参数完成, 耗时[{}]", Utils.getTimeDesc(b1-begin));
/** 2. 删除统计数据				*/
			log.info("手动统计开始, 先删除[{}]统计元数据...",parma.getDateStr());
			deleteStatisticsData(parma); 
			b2 = System.currentTimeMillis();
			log.info("【2】删除统计数据完成, 耗时[{}]", Utils.getTimeDesc(b2-b1));
/** 3. 初始化				*/
			initialization(parma);
			b3 = System.currentTimeMillis();
			log.info("【3】初始化加载数据完成, 耗时[{}]", Utils.getTimeDesc(b3-b2));
/** 4. 分析				*/
			analysis(parma); 
			b4 = System.currentTimeMillis();
			log.info("【4】分析数据完成, 耗时[{}]", Utils.getTimeDesc(b4-b3));
/**	5. 统计		*/			
			statistics(parma);
			long over = System.currentTimeMillis();
			log.info("【5】统计数据完成 , 耗时[{}]", Utils.getTimeDesc(over-b4));
			
			log.info("- progress - [100%] - alarmOver - 执行完成, 总耗时:{}", Utils.getTimeDesc(over-begin));
		} catch (Exception e) {
			log.error("报警统计-读取轨迹文件线程异常::" + e.getMessage(), e);
		}
	}
	/**
	 * 轨迹文件分析
	 * @param parma
	 */
	@SuppressWarnings("unchecked")
	private void analysis(StatisticsParma parma) {
		try {
			int threadSize = analysisThreadSize;
			String trackPath = path + parma.getTrackDatePath();
			log.info("-------------Task starting ... FilePath:{}", trackPath);
//			初始化文件列表数组
			List<File>[] fileListArray = new ArrayList[analysisThreadSize];
			for(int i = 0; i < analysisThreadSize; i++){
				fileListArray[i] = new ArrayList<File>();
			}
			/** 任务列表	  */
			List<FutureTask<FutureResult>> futureList = new ArrayList<FutureTask<FutureResult>>();
			/** 线程池	  */
			ExecutorService exec =  Executors.newFixedThreadPool(threadSize);
			File file = new File(trackPath);
			if (file.isDirectory()) {
				File[] fileAlarm = file.listFiles(new FileNameFilter());// 获取目录下文件列表
				if (fileAlarm != null && fileAlarm.length > 0) {
					long start = System.currentTimeMillis();
					for (int i = 0; i < fileAlarm.length; i++) {
						log.debug("分发轨迹文件[{}]", fileAlarm[i].getPath());
						int mod = i % threadSize;
						fileListArray[mod].add(fileAlarm[i]);
					}
					// 将文件列表加入对应统计线程
					for (int i = 0; i < threadSize; i++) {
						// 创建对象
						FutureTask<FutureResult> task = new FutureTask<FutureResult>(new AlarmAnalysisTask(i, fileListArray[i], parma));
						// 添加到list,方便后面取得结果
						futureList.add(task);
						// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
						exec.submit(task);
					}
					long b3 = System.currentTimeMillis();
					log.info("读取轨迹文件目录[{}], 线程数据[{}], 文件数量[{}]条, 数据分发耗时[{}]ms", trackPath, threadSize, fileAlarm.length, Utils.getTimeDesc(b3 - start));
					Thread.sleep(statisticsStateInterval);
					/** 5. 获取处理结果 */
					Set<String> set = new HashSet<String>();
					boolean THREAD_STATE = true;
					while (THREAD_STATE) {
						try {
							for (FutureTask<FutureResult> result : futureList) {
								if (result.isDone()) {
									FutureResult fr = result.get();
									String futureName = fr.getName();
									if (!set.contains(futureName)) {
										log.info("业务线程[{}]执行完成!", futureName);
										set.add(futureName);
									}
								}
								if (set.size() == threadSize) {
									THREAD_STATE = false;
									break;
								}
							}
							Thread.sleep(statisticsStateInterval);
						} catch (Exception e) {
							log.error("报警统计-查询状态异常::" + e.getMessage(), e);
						}
					}
					long end = System.currentTimeMillis();
					log.info("- alarmAnalysis - 分析完成, 耗时:{}", Utils.getTimeDesc(end - b3));
				} else {
					log.error("读取轨迹文件异常:目录[{}]下文件数量小于1", trackPath);
				}
			} else {
				log.error("读取轨迹文件异常:目录[{}]不存在", trackPath);
			}
			exec.shutdown();// 关闭线程池
		} catch (Exception e) {
			log.error("分析轨迹文件异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 加载统计参数
	 * @return
	 */
	private StatisticsParma getStatisticsParma() {
		long start = System.currentTimeMillis();
		try {
			String initMode = "自动";
			String dateStr = null;
			SimpleDateFormat sdf = new SimpleDateFormat("/yyyy/MM/dd/");
			SimpleDateFormat format = new SimpleDateFormat("yyyyMMdd");
			SimpleDateFormat currentStrFormat = new SimpleDateFormat("yyyy-MM-dd");
			SimpleDateFormat dateFull = new SimpleDateFormat("yyyyMMddHHmmss");
			boolean manualOperation = Boolean.parseBoolean(ConfigLoader.config.get("manualOperation"));
			String statisticsDayStr = null;
			long startUtc = 0;
			long endUtc = 0;
			long yesterdayUtc = 0;
			long lastWeekStartUtc = 0;
			long lastWeekEndUtc = 0;
			long lastWeekUtc = 0;
			long lastMonthStartUtc = 0;
			long lastMonthEndUtc = 0;
			long lastMonthUtc = 0;
			long illegalDrivingStartUtc;
			long illegalDrivingEndUtc;
			String trackDatePath = null;
			// 手动处理设置日期
			if(manualOperation){ 
				dateStr = ConfigLoader.config.get("manualOperationDate");
				Date date = format.parse(dateStr);
				trackDatePath = sdf.format(date);
				statisticsDayStr = currentStrFormat.format(date);
				startUtc = dateFull.parse(dateStr + "000000").getTime();
				endUtc = dateFull.parse(dateStr + "235959").getTime();
				yesterdayUtc = dateFull.parse(dateStr + "120000").getTime();
				illegalDrivingStartUtc = dateFull.parse(dateStr + "020000").getTime();
				illegalDrivingEndUtc = dateFull.parse(dateStr + "050000").getTime();
				initMode = "手动";
			} else { // 自动处理上一天数据
				trackDatePath = Utils.getYesterdayDir();
				dateStr = Utils.getYesterdayStr();
				startUtc = dateFull.parse(dateStr + "000000").getTime();
				endUtc = dateFull.parse(dateStr + "235959").getTime();
				yesterdayUtc = dateFull.parse(dateStr + "120000").getTime();
				statisticsDayStr = currentStrFormat.format(yesterdayUtc);
				illegalDrivingStartUtc = dateFull.parse(dateStr + "020000").getTime();
				illegalDrivingEndUtc = dateFull.parse(dateStr + "050000").getTime();
				initMode = "自动";
			}
			StatisticsParma parma = new StatisticsParma();
			Calendar c = Calendar.getInstance();
			c.setTimeInMillis(yesterdayUtc + 86400000); // 使用统计当天的时间判断
			if (c.get(Calendar.DAY_OF_WEEK) == 2) { // 周一统计上一周的 （星期日是1）
				parma.setWeekStatistics(true);
				lastWeekStartUtc = Utils.getLastWeekStartUtc(yesterdayUtc);
				lastWeekEndUtc = Utils.getLastWeekEndUtc(yesterdayUtc);
				lastWeekUtc = Utils.getLastWeekUtc(yesterdayUtc);
				parma.setLastWeekUtc(lastWeekUtc);
				parma.setLastWeekStartUtc(lastWeekStartUtc);
				parma.setLastWeekEndUtc(lastWeekEndUtc); 
				parma.setWeek(Utils.getWeek(lastWeekUtc));
			} else {
				parma.setWeekStatistics(false);
			}
			if (c.get(Calendar.DAY_OF_MONTH) == 1) { // 1号统计上一月的
				parma.setMonthStatistics(true); 
				lastMonthStartUtc = Utils.getLastMonthStartUtc(dateStr);
				lastMonthEndUtc = Utils.getLastMonthEndUtc(dateStr); 
				lastMonthUtc = Utils.getLastMonthUtc(dateStr);
				parma.setLastMonthUtc(lastMonthUtc);
				parma.setLastMonthStartUtc(lastMonthStartUtc);
				parma.setLastMonthEndUtc(lastMonthEndUtc); 
			} else {
				parma.setMonthStatistics(false); 
			}
			parma.setStatisticsDayStr(statisticsDayStr);
			parma.setCurrentUtc(yesterdayUtc);
			parma.setStartUtc(startUtc);
			parma.setEndUtc(endUtc); 
			parma.setTrackDatePath(trackDatePath); 
			parma.setDateStr(dateStr);
			parma.setManualOperation(manualOperation);
			parma.setIllegalDrivingStartUtc(illegalDrivingStartUtc);
			parma.setIllegalDrivingEndUtc(illegalDrivingEndUtc); 
			long end = System.currentTimeMillis();
			log.info("初始化统计参数完成:【{}】, 统计日期[{}], 正午[{}], 开始[{}], 结束[{}], 目录[{}], 日期确认[{}], 耗时[{}]ms", initMode, statisticsDayStr, yesterdayUtc ,startUtc, endUtc, trackDatePath, dateStr, end-start);
			return parma;
		} catch (Exception e) {
			log.error("初始化统计参数异常:" + e.getMessage(), e);
			return null;
		}
	}



	/**
	 * 初始化
	 */
	private void initialization(StatisticsParma parma) {
		try {
//1. 		清空缓存
			Cache.clearAll();
			
//2. 		启动初始化任务线程池
			int threadSize = 11;
			/** 任务列表	  */
			List<FutureTask<String>> futureList = new ArrayList<FutureTask<String>>();
			/** 线程池	  */
			ExecutorService exec =  Executors.newFixedThreadPool(threadSize);
			// 将文件列表加入对应统计线程
			for (int i = 0; i < threadSize; i++) {
				// 创建对象
				FutureTask<String> task = new FutureTask<String>(new StatisticsInitTask(i, parma));
				// 添加到list,方便后面取得结果
				futureList.add(task);
				// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
				exec.submit(task);
			}
//3. 获取执行结果
			Set<String> set = new HashSet<String>();
			boolean THREAD_STATE = true;
			while (THREAD_STATE) {
				try {
					for (FutureTask<String> result : futureList) {
						if (result.isDone()) {
							String id = result.get();
							if (!set.contains(id)) {
								log.debug("初始化任务[{}]执行完成!", id);
								set.add(id);
							}
						}
						if (set.size() == threadSize) {
							THREAD_STATE = false;
							break;
						}
					}
					Thread.sleep(3);
				} catch (Exception e) {
					log.error("初始化-查询状态异常:" + e.getMessage(), e);
				}
			}
			exec.shutdown(); 
			futureList.clear(); 
		} catch (Exception e) {
			log.error("初始化异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 删除统计数据
	 * @param parma
	 */
	private void deleteStatisticsData(StatisticsParma parma) {
		try {
//1. 		启动初始化任务线程池
			int threadSize = 8;
			/** 任务列表	  */
			List<FutureTask<String>> futureList = new ArrayList<FutureTask<String>>();
			/** 线程池	  */
			ExecutorService exec =  Executors.newFixedThreadPool(threadSize);;
			// 将任务加入对应统计线程
			for (int i = 0; i < threadSize; i++) {
				// 创建对象
				FutureTask<String> task = new FutureTask<String>(new StatisticsDeleteTask(i, parma));
				// 添加到list,方便后面取得结果
				futureList.add(task);
				// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
				exec.submit(task);
			}
//2. 获取执行结果
			Set<String> set = new HashSet<String>();
			boolean THREAD_STATE = true;
			while (THREAD_STATE) {
				try {
					for (FutureTask<String> result : futureList) {
						if (result.isDone()) {
							String id = result.get();
							if (!set.contains(id)) {
								log.debug("初始化任务[{}]执行完成!", id);
								set.add(id);
							}
						}
						if (set.size() == threadSize) {
							THREAD_STATE = false;
							break;
						}
					}
					Thread.sleep(10);
				} catch (Exception e) {
					log.error("删除统计数据-查询状态异常:" + e.getMessage(), e);
				}
			}
			exec.shutdown(); 
			futureList.clear();
		} catch (Exception e) {
			log.error("删除统计数据异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 告警统计任务
	 * @param parma
	 */
	private void statistics(StatisticsParma parma) {
		/** 任务列表	  */
		List<FutureTask<String>> futureList = new ArrayList<FutureTask<String>>();
		int threadSize = 7;
		/** 线程池	  */
		ExecutorService exec =  Executors.newFixedThreadPool(threadSize);;
		// 将文件列表加入对应统计线程
		for (int i = 0; i < threadSize; i++) {
			// 创建对象
			FutureTask<String> task = new FutureTask<String>(new StatisticsTask(i, parma));
			// 添加到list,方便后面取得结果
			futureList.add(task);
			// 一个个提交给线程池，当然也可以一次性的提交给线程池，exec.invokeAll(list);
			exec.submit(task);
		}
		Set<String> set = new HashSet<String>();
		boolean THREAD_STATE = true;
		while (THREAD_STATE) {
			try {
				for (FutureTask<String> result : futureList) {
					if (result.isDone()) {
						String id = result.get();
						if (!set.contains(id)) {
							log.debug("告警任务[{}]执行完成!", id);
							set.add(id);
						}
					}
					if (set.size() == threadSize) {
						THREAD_STATE = false;
						break;
					}
				}
				Thread.sleep(3);
			} catch (Exception e) {
				log.error("报警统计-查询状态异常::" + e.getMessage(), e);
			}
		}
		exec.shutdown(); 
		futureList.clear(); 
	}

}
