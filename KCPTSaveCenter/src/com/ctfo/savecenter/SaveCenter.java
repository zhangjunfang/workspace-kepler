package com.ctfo.savecenter;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Map;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

//import org.apache.log4j.PropertyConfigurator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.addin.AddInManagerInit;
import com.ctfo.savecenter.analy.AnalyseServiceInit;
import com.ctfo.savecenter.connpool.JDCConnectionInit;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.ctfo.savecenter.dao.DaoInit;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.io.IoInit;
import com.ctfo.savecenter.io.SendMsgThread;
import com.lingtu.xmlconf.XmlConf;

/**
 * 存储服务启动类
 */

public class SaveCenter {

	private static final Logger logger = LoggerFactory.getLogger(SaveCenter.class);

	/**
	 * 管理端口,用于接收和发送停止服务命令
	 */
	private static int managePort;

	/**
	 * 操作系统系统调用主函数
	 * 
	 * @param args
	 *            调用时给的参数字符串数组
	 */
	public static void main(String[] args) {
		ServerSocket listener = null;
		String configFile = null;
		String command = null;
		Thread alarmActiveMq = null;
		try {

			for (int i = 0; i < args.length; i++) {
				if (args[i].equalsIgnoreCase("-f")) {
					if (args.length == i + 1) {
						System.out.print("SaveCenter args error");
						return;
					} else {
						configFile = args[++i];
					}
				}
				command = args[i];
			}
			// 读取配置文件
			XmlConf conf = new XmlConf(configFile);

			// 从配置文件读取管理端口
			managePort = conf.getIntValue("ManagePort");
			if (command.equalsIgnoreCase("-stop"))// 停止服务
			{
				shutdown();
				return;
			}
			else if (command.equalsIgnoreCase("-status"))// 状态查询
			{
				showStatus();
				return;
			}
			// 打开管理端口
			try {
//				listener = new ServerSocket(managePort, 1, InetAddress.getByName("127.0.0.1"));
				listener = new ServerSocket(managePort, 1, InetAddress.getLocalHost());
			} catch (Exception e) {
				logger.error("打开管理端口时错误" + e.getMessage());
				System.out.println("不能启动轨迹存储服务，服务可能已经启动了");
				return;
			}
//			String log4j = conf.getStringValue("log4j");
//			PropertyConfigurator.configure(log4j);//加载.properties文件 
			
//			创建远程诊断文件夹
			createRemoteDiagnosisDir();
			
			// 启动监控线程
			SaveCenterMonitor saveCenterMonitor = new SaveCenterMonitor();
			saveCenterMonitor.start();
			
			// 初始化连接池
			JDCConnectionInit jDCConnectionInit = new JDCConnectionInit(conf);
			jDCConnectionInit.init();

			// 初始化分析类
			AnalyseServiceInit analyseServiceInit = new AnalyseServiceInit(conf);
			analyseServiceInit.init();

			// 初始化缓存数据
			DaoInit daoInit = new DaoInit(conf);
			daoInit.init();
			
			//启动企业告警设置缓存维护任务 -- 程序启动后30分钟开始运行,间隔30分钟运行一次
			ScheduledExecutorService service = Executors.newScheduledThreadPool(1);
			Runnable orgAlarmSetting = new OrgAlarmSettingThread();
			service.scheduleAtFixedRate(orgAlarmSetting, Const.ORG_ALARM_UPDATE_TIME, Const.ORG_ALARM_UPDATE_TIME, TimeUnit.MILLISECONDS);
			
			
			// 启动插件管理线程
			AddInManagerInit addInManagerInit = new AddInManagerInit();
			addInManagerInit.init(conf);

			// 初始化通讯连接
			IoInit ioInit = new IoInit(conf);
			ioInit.init();

			// 启动缓存维护线程
			 MemoryManager memory=new MemoryManager();
			 memory.start();

			 SendMsgThread sendMsgThread=new SendMsgThread();
			 sendMsgThread.start();
			 
			 //启动MQ  2013-07-22 改为定时更新告警设置物化视图
			/* String mqUrl =conf.getStringValue("activeMQ|url");
			 String alarmMQ = conf.getStringValue("activeMQ|alarmQueue");
			 alarmActiveMq = new AlarmActiveMQ(mqUrl,alarmMQ);
			 alarmActiveMq.start();*/
			 
			
			 
			 System.out.println("SaveCenter started.");
			 logger.info("轨迹存储" + Constant.VER + " 已经启动");
		} catch (Exception e) {	
			logger.error("轨迹分析服务终止 "+ e.getMessage(), e);
			System.out.println("Can not start SaveCenter. " + e.getMessage());
			System.exit(0);
		}

		// 监听管理端口
		while (true) {
			Socket sock = null;
			try {
				sock = listener.accept();
				// 读取指令
				sock.setSoTimeout(5000);
				BufferedReader in = new BufferedReader(new InputStreamReader(
						sock.getInputStream()));
				String s = in.readLine();
				if (s.equals(Constant.SHUTDOWNCOMMAND))// 停止服务指令
				{
					
					System.out.println("+ SaveCenter is stopping.");
					if(null != alarmActiveMq){ // 关闭MQ连接
						alarmActiveMq.interrupt();
					}
					System.exit(0);
					break;
				} else if (s.equals(Constant.STATUSCOMMAND))// 服务状态查询指令
				{
					PrintWriter out = new PrintWriter(
							sock.getOutputStream());
					out.println(Constant.STATUSCOMMAND);
					out.flush();
					out.println("+ " + Constant.VER + " is running.");
					out.flush();
					out.println("Service is listening port " + managePort);
					out.println("Oracle connection size : " + OracleConnectionPool.listCacheInfos());					
					out.println("Vehicle cache count : " + TempMemory.getVehicleMapSize());
					out.flush();
					
					in.close();
					out.close();
				} else if (s.equals(Constant.QUEUECOMMAND))// 服务状态查询指令
				{
					DataOutputStream out = new DataOutputStream(sock.getOutputStream());    
					StringBuilder sb = new StringBuilder();
					for(Map.Entry<String,String> queue : TempMemory.queueStatus.entrySet()){
						sb.append(queue.getKey()).append(":").append(queue.getValue()).append(",");
					}
					out.writeUTF(sb.toString());
					out.flush();
					in.close();
					out.close();
				} else{
					in.close();
				}
			} catch (Exception e) {
				logger.error("查询状态时出错 "+ e.getMessage());
			} finally {
				try {
					sock.close();
				} catch (IOException e) {
					logger.error(Constant.SPACES,e);
				}
			}
		} // End while

	}
	
	/*****************************************
	 * <li>描        述：创建远程诊断文件夹 		</li><br>
	 * <li>时        间：2013-8-19  下午3:12:23	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	private static void createRemoteDiagnosisDir() {
		String currentDir = System.getProperty("user.dir");
		String separator = System.getProperty("file.separator");
		String remotediagnosisInfoDir = "";
		String remotediagnosisErrorDir = "";
		if(System.getProperty("os.name").toUpperCase().indexOf("WINDOWS") != -1){
			remotediagnosisInfoDir = currentDir + separator + "info" +separator;
			remotediagnosisErrorDir = currentDir + separator + "error" +separator;
		}else {
			remotediagnosisInfoDir = currentDir + separator + "info" +separator;
			remotediagnosisErrorDir = currentDir + separator + "error" +separator;
			
		}
		File infoFile = new File(remotediagnosisInfoDir);
		File errorFile = new File(remotediagnosisErrorDir);
		if(!infoFile.isDirectory()){
			infoFile.mkdir();
		}
		if(!errorFile.isDirectory()){
			errorFile.mkdir();
		}
	}

	/**
	 * 停止服务
	 */
	private static void shutdown() {
		try {
			// 向管理端口发送停止服务指令
//			Socket sock = new Socket("127.0.0.1", managePort);
			Socket sock = new Socket(InetAddress.getLocalHost(), managePort);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			out.println(Constant.SHUTDOWNCOMMAND);
			out.flush();
			out.close();
			sock.close();
		} catch (Exception e) {
			logger.error("- Can not stop SaveCenter. Service may be not running."+ e.getMessage());
		}
	}

	/**
	 * 显示当前服务状态
	 */
	public static void showStatus() {
		try {
			// 向管理端口发送服务状态查询指令
			Socket sock = null;
			try {
//				sock = new Socket("127.0.0.1", managePort);
				sock = new Socket(InetAddress.getLocalHost(), managePort);
			} catch (Exception e) {
				System.out.println("- SaveCenter is not running.");
				return;
			}
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			BufferedReader in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
			out.println(Constant.STATUSCOMMAND);
			out.flush();
			String info = in.readLine();
			if (!info.equals(Constant.STATUSCOMMAND)) {
				System.out.println("SaveCenter is not running.");
				in.close();
				out.close();
				sock.close();
				return;
			}
			info = in.readLine();
			while (info != null) {
				System.out.println(info);
				info = in.readLine();
			}// End while
			
			in.close();
			out.close();
			sock.close();
		} catch (Exception e) {
			e.printStackTrace(System.err);
		}
	}
}