/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>时        间：2013-9-9  上午9:07:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.List;

import org.quartz.Scheduler;
import org.quartz.impl.StdSchedulerFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.common.JobInfo;
import com.ctfo.trackservice.common.Utils;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.RedisConnectionPool;
import com.ctfo.trackservice.io.IoClient;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.Constant;
import com.ctfo.trackservice.util.SystemUtil;



/*****************************************
 * <li>描        述：程序入口		
 * 
 *****************************************/
public class TrackServiceMain {
	private static final Logger logger = LoggerFactory.getLogger(TrackServiceMain.class);
	/** 通讯模块		 */
	public static IoClient ioClient;
	/** 任务调度器 */
	private static Scheduler sched = null;
	/** 任务调度器工厂 */
	private static StdSchedulerFactory std = null;
	
	private static ServerSocket listener = null;
	/*****************************************
	 * <li>描        述：程序主方法 		</li><br>
	 * <li>时        间：2013-9-9  上午9:07:53	</li><br>
	 * <li>参数： @param args			</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
		System.out.println("TrackService <init>");
		logger.info("TrackService <init>");
		
		try{
			// 1. 加载配置文件
			ConfigLoader.init(args);
			// 2. 初始化连接池
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
			OracleService.init();
			
			// 3. 启动定时任务
			std = new StdSchedulerFactory();
			sched = std.getScheduler();
			sched.start();
			List<JobInfo> jobs = ConfigLoader.jobs;
			for (JobInfo job : jobs) {
				sched.scheduleJob(job.getJobDetail(), job.getCronTrigger());
			}
			// 4. 打开管理端口
			Integer port = Integer.parseInt(ConfigLoader.config.get("listenPort"));
			listener = new ServerSocket(port, 1, InetAddress.getLocalHost());

			// 5. 启动通讯与业务线程
			ioClient = new IoClient(Utils.getMsgProperties(ConfigLoader.config));
			ioClient.init();
			
			SystemUtil.generagePid();
			System.out.println("TrackService <started>");
			logger.info("TrackService <started>");
		
		}catch(Exception e){
			System.out.println("TrackService Faile:"+ e.getMessage() + " \r\n 程序退出！！！");
			logger.error("TrackService Faile:"+ e.getMessage() + " \r\n 程序退出！！！", e);
//			程序退出
			System.exit(0);
		}
//		 监听管理端口
		while (true) {
			Socket sock = null;
			try {
				sock = listener.accept();
				// 读取指令
				sock.setSoTimeout(5000);
				BufferedReader in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
				String s = in.readLine();
				// 停止服务指令
				if (s.equals(Constant.SHUTDOWNCOMMAND)) {
					logger.info("-TrackServiceMain-- is stopping.");  
					System.out.println("-TrackServiceMain- is stopping.");
					System.exit(0);
					break;
				}
			} catch (Exception e) {
				logger.error("查询状态时出错 "+ e.getMessage(), e);
			} finally {
				try {
					sock.close();
				} catch (IOException e) {
					logger.error("监听管理端口异常:" + e.getMessage(), e);
				}
			}
		} 
	}
}
