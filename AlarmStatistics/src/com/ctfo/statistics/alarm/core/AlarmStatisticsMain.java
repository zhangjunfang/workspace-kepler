/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>时        间：2013-9-9  上午9:07:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statistics.alarm.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Calendar;
import java.util.List;
import java.util.concurrent.ScheduledExecutorService;

import org.quartz.CronTrigger;
import org.quartz.Scheduler;
import org.quartz.impl.StdSchedulerFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.JobInfo;
import com.ctfo.statistics.alarm.common.SystemUtil;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.dao.OracleConnectionPool;
import com.ctfo.statistics.alarm.dao.RedisConnectionPool;
import com.ctfo.statistics.alarm.service.OracleService;

/*****************************************
 * <li>描 述：程序入口
 * 
 *****************************************/
public class AlarmStatisticsMain {
	private static final Logger logger = LoggerFactory.getLogger(AlarmStatisticsMain.class);
	/** 定时任务执行列表 */
	public static ScheduledExecutorService service = null;
	/** 任务调度器 */
	private static Scheduler sched = null;
	/** 任务调度器工厂 */
	private static StdSchedulerFactory std = null;
	 
	private static ServerSocket listener = null;
	
	/*****************************************
	 * <li>描 述：程序主方法</li><br>
	 * <li>时 间：2013-9-9 上午9:07:53</li><br>
	 * <li>参数： @param args</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
	
		try {
			System.out.println("位置服务应用程序正在启动中 ... ...");
			logger.info("位置服务应用程序正在启动中 ... ...");

			// 1. 加载配置文件
			ConfigLoader.init(args);

			logger.info("-AlarmStatisticsMain--(1)-初始化完成");
			System.out.println("-AlarmStatisticsMain--(1)-初始化完成");

			// 2. 初始化连接池
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
			OracleService.init();

			// 3. 启动定时任务
			std = new StdSchedulerFactory();
			sched = std.getScheduler();
			sched.start();
			
			boolean manualOperation = Boolean.parseBoolean(ConfigLoader.config.get("manualOperation"));
			
			List<JobInfo> jobs = ConfigLoader.jobs;
			for (JobInfo job : jobs) {
				CronTrigger cron = job.getCronTrigger();
				if(manualOperation && (!job.getJobDetail().getName().equals("HeartbeatJob"))){
					String cronStr = getAfterSecondsExecution(10);
					cron.setCronExpression(cronStr);
					logger.info("任务[{}]手动执行, 执行规则[{}]",cron.getName(),cronStr);
				}
				sched.scheduleJob(job.getJobDetail(), cron);
			}

			// 4. 打开管理端口
			Integer port = Integer.parseInt(ConfigLoader.config.get("listenPort"));
			listener = new ServerSocket(port);

			SystemUtil.generagePid();

			logger.info("-AlarmStatisticsMain--(4)-打开管理端口[" + port + "]完成!");

			System.out.println("-AlarmStatisticsMain - started!");
			logger.info("-AlarmStatisticsMain - started!");

		} catch (Exception e) {
			System.out.println("---(error)---应用程序启动异常!" + e.getMessage() + " \r\n 程序退出！！！");
			logger.error("-AlarmStatisticsMain--(error)-应用程序启动异常 !" + e.getMessage() + " \r\n 程序退出！！！", e);
			// 程序退出
			System.exit(0);
		}
		// 监听管理端口
		while (true) {
			Socket sock = null;
			try {
				sock = listener.accept();
				// 读取指令
				sock.setSoTimeout(5000);
				BufferedReader in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
				String s = in.readLine();
				// 停止服务指令
				if (s.equals("shutdown")) {
					logger.info("-AlarmStatisticsMain-- is stopping.");
					System.out.println("-AlarmStatisticsMain- is stopping.");
					System.exit(0);
					break;
				}
			} catch (Exception e) {
				logger.error("查询状态时出错 " + e.getMessage(), e);
			} finally {
				try {
					sock.close();
				} catch (IOException e) {
					logger.error("监听管理端口异常:" + e.getMessage(), e);
				}
			}
		}
	}
	/**
	 * 获取秒数后的执行时间字符串
	 * @param seconds
	 * @return
	 */
	private static String getAfterSecondsExecution(int seconds) {
		 Calendar cl = Calendar.getInstance();
	     cl.add(Calendar.SECOND, seconds);
	     StringBuffer sb = new StringBuffer();
	     sb.append(cl.get(Calendar.SECOND)).append(" ");
	     sb.append(cl.get(Calendar.MINUTE)).append(" ");
	     sb.append(cl.get(Calendar.HOUR_OF_DAY));
	     sb.append(" * * ?");
		return sb.toString(); 
	}

}
