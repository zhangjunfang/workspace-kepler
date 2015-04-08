package com.ctfo.syncservice.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Calendar;
import java.util.Map;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.syncservice.dao.OracleDataSource;
import com.ctfo.syncservice.dao.RedisDataSource;
import com.ctfo.syncservice.util.ConfigLoader;
import com.ctfo.syncservice.util.SystemUtil;
import com.ctfo.syncservice.util.TaskAdapter;
import com.ctfo.syncservice.util.TaskConfiger;

public class SyncServiceMain {
	private static Logger log = LoggerFactory.getLogger(SyncServiceMain.class);
	
	public static boolean RUNNING = false;
	/** 服务状态管理线程 */
//	private static ManagerThread managerThread = null;
	/** 系统管理器监听绑定IP */
//	public static String SYS_LISTENHOST = "localhost";
	/** 系统管理器监听端口 */
	public static int SYS_LISTENPORT = 8450;
	/** 系统版本 */
	public static String SYS_VERSION = "SyncService-2.0.0.0-20131216";
	
	/** Oracle数据库客户端连接(池)管理器 */
	private static OracleDataSource oracle = null; 
	
	/** Redis缓存服务客户端连接(池)管理器 */
	private static RedisDataSource redis = null;
	
	/** 定时任务执行列表 */
	private static ScheduledExecutorService service = null;
	/** 应用监听	 */
	private static ServerSocket listener = null;
	/**
	 * 获得下一次的执行时间
	 * @param currentDate 当前时间
	 * @param priod 日期模式 1:月   2:周   3:日
	 * @param hour 小时
	 * @param minute 分钟
	 * @param second 秒
	 * @return 下一次执行压缩的日期
	 */
	public static Calendar getNextExecuteTime(Calendar currentDate, int hour, int minute, int second) {
		int curHour = currentDate.get(Calendar.HOUR_OF_DAY);
		int curMinute = currentDate.get(Calendar.MINUTE);
		int curSecond = currentDate.get(Calendar.SECOND);
		//当前时间是否大于执行的时间
		boolean islater = isCurLargerSet(curHour, curMinute, curSecond, hour, minute, second);
		Calendar newdate = Calendar.getInstance();
		if (islater) {//当前时间大于设定时间，第二天执行
			newdate.add(Calendar.DAY_OF_MONTH, 1);
		}
		newdate.set(Calendar.HOUR_OF_DAY, hour);
		newdate.set(Calendar.MINUTE, minute);
		newdate.set(Calendar.SECOND, second);
		
		return newdate;
	}
	
	/**
	 * 当前时间是否大于设置的时间
	 * @param ch 当前时
	 * @param cm 当前分
	 * @param cs 当前秒
	 * @param sh 设置时
	 * @param sm 设置分
	 * @param ss 设置秒
	 * @return true:大于  false:不大于
	 */
	public static boolean isCurLargerSet(int ch, int cm, int cs, int sh, int sm, int ss) {
		boolean res = false;
		if (ch > sh) {//当前时大于设置时
			res = true;
		} else if (ch == sh) {//当前时等于设置时
			if (cm > sm) {//当前分大于设置分
				res = true;
			} else if (cm == sm) {//当前分等于设置分
				if (cs > ss) {//当前秒大于设置秒
					res = true;
				}
			}
		}
		return res;
	}
	/*****************************************
	 * <li>描        述：服务停止 		</li><br>
	 * <li>时        间：2013-12-4  下午2:22:16	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public static void stop() {
		RUNNING = false;
		try {
			service.shutdown();// 停止服务
			oracle.destroy();
			redis.destroy();
			log.warn("服务停止！");
		} catch (Exception e) {
			System.exit(-1);
		}
	}
	public static void main(String[] args) {
		try{
			//-f syncservice.xml start
			if (args.length < 3) {//参数不合法
				System.out.println("输入参数不合法，请输入形式如“-f syncservice.xml start”的参数。");
				log.error("输入参数不合法，请输入形式如“-f syncservice.xml start”的参数。");
				return;
			}
			
			ConfigLoader.load(args[1]);// 加载配置文件信息
			

			
			//构建Oracle数据库连接池管理器///////////////////////////////////////////
			Map<String, String> orcf = ConfigLoader.CONFIG_ORACLEDB; 
			oracle = OracleDataSource.getInstance();
			oracle.setUrl(orcf.get("url"));
			oracle.setUsername(orcf.get("username"));
			oracle.setPassword(orcf.get("password"));
			oracle.setInitialSize(Integer.parseInt(orcf.get("initialSize")));
			oracle.setMaxActive(Integer.parseInt(orcf.get("maxActive")));
			oracle.setMinIdle(Integer.parseInt(orcf.get("minIdle")));
			oracle.setMaxWait(Long.parseLong(orcf.get("maxWait")));
			oracle.setRemoveAbandoned(Boolean.parseBoolean(orcf.get("removeAbandoned")));
			oracle.init();
			
			//构建Redis缓存服务连接池管理器///////////////////////////////////////////
			Map<String, String> rds = ConfigLoader.CONFIG_REDISCACHE;
			redis = new RedisDataSource();
			redis.setRedis_host(rds.get("host"));
			redis.setRedis_port(Integer.parseInt(rds.get("port")));
			redis.setRedis_password(rds.get("pass"));
			redis.setMaxActive(Integer.parseInt(rds.get("maxActive")));
			redis.setMaxIdle(Integer.parseInt(rds.get("maxIdle")));
			redis.setMaxWait(Integer.parseInt(rds.get("maxWait")));
			redis.setTimeOut(Integer.parseInt(rds.get("timeOut")));
			redis.init();
			
			//构建任务执行列表///////////////////////////////////////////
			service = Executors.newScheduledThreadPool(ConfigLoader.TASK_LIST.size());
			for (TaskConfiger tc : ConfigLoader.TASK_LIST) {
				Class<?> taskClass = Class.forName(tc.getImpClass());
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setName(tc.getName());
				task.setConf(tc.getProperties());
				task.setOracle(oracle);
				task.setRedis(redis);
				task.init();
				long delay = Long.parseLong(tc.getDelay());
				String period = tc.getPeriod();
				service.scheduleAtFixedRate(task, delay, Long.parseLong(period), tc.getUnit());
			}
			RUNNING = true;
			
//			启动管理端口
			// 加载系统参数
			Map<String, String> sycf = ConfigLoader.CONFIG_SYS_PARAM;
//			SYS_LISTENHOST = sycf.get("managehost").toLowerCase();
			SYS_LISTENPORT = Integer.parseInt(sycf.get("manageport"));
			listener = new ServerSocket(SYS_LISTENPORT);
			
//			//指令处理模块//////////////////////////////////////////////////////////////////////////////
//			String command = args[2];
//			boolean isrunning = true;
//			try {
//				Socket socket = new Socket(SYS_LISTENHOST, SYS_LISTENPORT);
//				socket.setSoTimeout(180 * 1000);
//				BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
//				BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
//				bw.write(command + "\r\n");
//				bw.flush();
//				if ("stop".equals(command)) System.out.println("正在停止服务，不超过3分钟，请耐心等候...");
//				String resp = "";
//				resp = br.readLine();
//				System.out.println(resp);
//				socket.close();
//			} catch (UnknownHostException e) {
//				System.out.println("系统异常，无法创建Socket");
//			} catch (IOException e) {
//				if ("start".equals(command)) {
//					isrunning = false;//标识为没有启动
//				} else if ("stop".equals(command)) {
//					System.out.println("等待服务应答超时，请手动结束进程或跟踪日志等待服务自动停止！");
//				} else if ("clear".equals(command)) {
//					System.out.println("存储分析服务开始清理内存...");
//				}else {
//					System.out.println("存储分析服务没有启动。");
//				}
//			}
//			if (isrunning) {
//				System.exit(0);
//			}
//			
//			managerThread = new ManagerThread(SYS_LISTENPORT, SYS_VERSION);
//			managerThread.start();
			
			SystemUtil.generagePid(); 
			
			log.info("服务启动完成--ok!");  
			System.out.println("服务启动完成--ok!");
		}catch (Exception e){
			log.error("服务启动异常:" + e.getMessage() , e);
			log.error("系统退出...");
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
				if (s.equals("stop")) {
					log.info("-SyncService-- is stopping.");  
					System.out.println("-SyncService- is stopping.");
					System.exit(0);
					break;
				}
			} catch (Exception e) {
				log.error("查询状态时出错 "+ e.getMessage(), e);
			} finally {
				try {
					sock.close();
				} catch (IOException e) {
					log.error("监听管理端口异常:" + e.getMessage(), e);
				}
			}
		} 
	}
}
