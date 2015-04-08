package com.caits.analysisserver.addin.kcpt.addin.task;

import java.io.File;
import java.util.Calendar;
import java.util.Iterator;
import java.util.Set;
import java.util.Vector;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.addin.kcpt.statisticanalysis.AutoExtendAssessSetThread;
import com.caits.analysisserver.addin.kcpt.statisticanalysis.DaystatInit;
import com.caits.analysisserver.addin.kcpt.statisticanalysis.MainTainStatisticThread;
import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.LoadFile;
import com.caits.analysisserver.database.TaskPool;
import com.caits.analysisserver.quartz.jobs.monitor.JobMonitor;
import com.caits.analysisserver.utils.CDate;

public class FileTaskThread extends UnifiedTask {
	private static final Logger logger = LoggerFactory.getLogger(FileTaskThread.class);
	
	private long utc = 0;
	
	// 默认获取指定设置日期
	private boolean isUse = true;
	
	public FileTaskThread(){

	}
	
	/******
	 * 统计指定时间数据
	 */
	@Override
	public void run() {
		StaPool.isAnalyserFile = true;
 		if(!this.isUse){
 			this.utc = CDate.getYesDayYearMonthDay();
		}

		long startTime = System.currentTimeMillis();
		AnalysisDBAdapter dba = new AnalysisDBAdapter();
		Vector<File> fileList = null;
		try {
			DaystatInit statInit = new DaystatInit();
			
			statInit.initAnalyser();
			statInit.run();
			
			JobMonitor.getInstance().updateJobRunningMonitor("TrackFileAnalyse", null,CDate.convertUtc2Date(startTime),
					null,0);
			
			dba.initDBAdapter();
			dba.querySoftAlarmDetail(utc);
		
			ConcurrentHashMap<String,Vector<UnifiedFileDispatch>> task = TaskPool.getinstance().getTask(this.getClass().getSimpleName());
			
			fileList = LoadFile.findFile(FilePool.getinstance().getFile(this.utc,"trackfileurl"), this.utc);
			String[] arrTask = new String[task.size()];
			Set<String> taskSet = task.keySet();
			
			Iterator<String> taskIt = taskSet.iterator();
			int i =0;
			while(taskIt.hasNext()){
				String taskName = taskIt.next();
				Vector<UnifiedFileDispatch> childTask = task.get(taskName);
				Iterator<UnifiedFileDispatch> childIt = childTask.iterator();
				while(childIt.hasNext()){
					childIt.next().setTime(this.utc); // 设置指定日期时间
				}// End while
				
				arrTask[i] = taskName;
				i++;
			}// End while
			i--;
			
			// 日统计文件
			logger.info("开始统计报警统计,总线,日专属统计");
			for (int idx = 0; idx < fileList.size(); idx++) {
				File file = fileList.get(idx);
				for(String name : arrTask){
					task.get(name).get(idx % task.get(name).size()).addPacket(file);
				}// End for
			}// End for
			
			while(true){ // 等待日统计结束
				logger.debug("fileList.size:" + fileList.size() + ",StaPool.getCountListSize().size:" + StaPool.getCountListSize());
				if(fileList.size() == StaPool.getCountListSize()){ //判断是否日统计结束
					break;
				}else{
					//等待基表统计结束
					try {
						Thread.sleep(2 * 60 * 1000); 
					} catch (InterruptedException e) {
						logger.error("车辆统计出错.",e);
					}
				}
			}//End while
			long endTime0 = System.currentTimeMillis();
			//轨迹文件分析任务结束
			JobMonitor.getInstance().updateJobRunningMonitor("TrackFileAnalyse", "1",
					CDate.convertUtc2Date(startTime),CDate.convertUtc2Date(endTime0),endTime0 - startTime);
			
			// 更新总累计表
			/*SummaryVehicleDay summaryVehicleDay = new SummaryVehicleDay();
			summaryVehicleDay.setTime(this.utc);
			summaryVehicleDay.initAnalyser();
			summaryVehicleDay.run();*/
			
			//执行自动按周期延展考核设置线程
	        AutoExtendAssessSetThread autoExtendAssessSetThread = new AutoExtendAssessSetThread();
	        autoExtendAssessSetThread.initAnalyser();
	        autoExtendAssessSetThread.run();
			
			//启动评分统计分析线程
			/*GradeStatisticThread gradeStatisticThread = new GradeStatisticThread();
			gradeStatisticThread.initAnalyser();
			gradeStatisticThread.run();*/
			
			//安全维堡日统计		
			MainTainStatisticThread mainTainStatisticThread = new MainTainStatisticThread();
			mainTainStatisticThread.initAnalyser();
			mainTainStatisticThread.run();
			
	//		// 企业业务日统计
	//		AnalysisCorpThread corpThread = new AnalysisCorpThread();
	//		corpThread.initAnalyser();
	//		corpThread.run();
			
			//夸多天统计
			/*AnalysisStatisticCrossDaysThread croDaysThread = new AnalysisStatisticCrossDaysThread();
			croDaysThread.initAnalyser();
			if(!this.isUse){ //手动执行日，不执行月
				if(CDate.checkIsFirstDayMonth(this.utc + 24 * 60 * 60 * 1000)){
					croDaysThread.setMonthFlag(true);
				}
			}*/
//			if(CDate.checkIsFirstDayMonthYear(this.utc + 24 * 60 * 60 * 1000)){
//				croDaysThread.setYearFlag(true);
//			}
			//croDaysThread.statistic();
			
			// 任务结束通知报表同步服务同步数据
			/*SystemBaseInfo sys = SystemBaseInfoPool.getinstance().getBaseInfoMap("reportSystem");
			if(sys != null){
				if(sys.getIsLoad().equals("true")){ 
					try {
						String res = Utils.Post(sys.getValue());
						if(res.trim().matches("success!")){
							logger.info("Running synchronization services of report system to successful！URL:" + sys.getValue());
						}else{
							logger.info("Running synchronization services of report system to failed！URL:" + sys.getValue());
						}
					} catch (Exception e) {
						logger.error("通知报表同步服务出错.",e);
					}
				}
			}*/
			
			// 同步日、周报警数据啊
			/*SynIdxAlarm syn = new SynIdxAlarm();
			syn.run();*/
			
		} catch (InterruptedException e) {
			logger.error("统计线程休眠出错。", e);
		} catch (Exception e) {
			logger.error("统计主线程出错。",e);
		}finally{
			StaPool.isAnalyserFile = false;
			AnalysisDBAdapter.clearCollections();
			if(fileList != null && fileList.size() > 0){
				fileList.clear();
			}
		}
		
		long endTime = System.currentTimeMillis();
		long costTime = (endTime - startTime);
		Calendar cl = Calendar.getInstance();
		cl.setTimeInMillis(this.utc);
	//	System.out.println("统计分析 " + cl.get(Calendar.YEAR) + "-" + (cl.get(Calendar.MONTH) +1) + "-" + cl.get(Calendar.DAY_OF_MONTH) + " 数据花费时间:" + (costTime) / 1000 + "s");
 		logger.info("统计分析 " + cl.get(Calendar.YEAR) + "-" + (cl.get(Calendar.MONTH) +1) + "-" + cl.get(Calendar.DAY_OF_MONTH) + " 数据花费时间:" + (costTime) / 1000 + "s");
	}

	/*****
	 * 是否按设置的日期统计数据
	 */
	@Override
	public void isUsingSettingTime(boolean isUse) {
		this.isUse = isUse;
	}

	/****
	 * 设置指定日期
	 */
	@Override
	public void setDate(long date) {
		this.utc = date;
	}
}
