package com.ctfo.dataanalysisservice;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;

import com.ctfo.dataanalysisservice.addin.KeyPointTimer;
import com.ctfo.dataanalysisservice.beans.UserInfo;
import com.ctfo.dataanalysisservice.io.ConfigServer;
import com.ctfo.dataanalysisservice.service.DataDistributeThread;
import com.ctfo.dataanalysisservice.service.DataSaveThread;
import com.ctfo.dataanalysisservice.service.ReceiveThread;
import com.ctfo.dataanalysisservice.service.impl.AlarmVehicleDataSynchronServiceImpl;
import com.ctfo.dataanalysisservice.util.FileUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据分析服务程序入口 （平台软报警） 主线程
 * 
 * @author yangjian
 * 
 */
public class DataAnalysisServiceMain extends Thread {

	private static final Logger logger = Logger
			.getLogger(DataAnalysisServiceMain.class);

	/**
	 * 配置
	 */
	public static XmlConf config;

	/**
	 * 线程运行标志
	 */
	public boolean isRunning = true;

	public static int threadCount;

	/**
	 * 程序入口
	 */
	public static void main(String[] args) {
		// 输入管理命令
		String command = null;

		// 管理端口
		int managePort;
		// 系统配置文件
		String configFile = null;
		// 管理服务
		ServerSocket serverListener = null;

		try {

			// 得到控制台输入字符串
			for (int i = 0; i < args.length; i++) {

				if (args[i].equalsIgnoreCase("-f")) {
					if (args.length == i + 1) {
						// 输出使用方法
						System.out.println(Constant.SYSTEM_HELPER);
						return;
					} else {
						configFile = args[++i];
					}
				}
				command = args[i];
			}

			// 没有输入配置文件名则加载默认项
			if (configFile == null) {
				configFile = FileUtil.getFileInClassPath(Constant.CONFIG_XML);
			}
			if (configFile == null) {
				System.out.println(Constant.LOAD_CONFIG_ERROR);
				return;
			}

			// 加载系统配置文件
			config = new XmlConf(configFile);

			// 初始化数据库中设置报警数据
			AlarmVehicleDataSynchronServiceImpl alarm = new AlarmVehicleDataSynchronServiceImpl();
			alarm.start();

			/*
			 * 测试 ThvehicleAlarmDaoImpl tb=new ThvehicleAlarmDaoImpl();
			 * 
			 * List<ThVehicleAlarm> data = new ArrayList<ThVehicleAlarm>();
			 * for(int i=0;i<10;i++){ ThVehicleAlarm thVehicleAlarm = new
			 * ThVehicleAlarm(); thVehicleAlarm.setVid(13l+i);
			 * thVehicleAlarm.setAlarmCode("1"); thVehicleAlarm.setUtc(12l);
			 * thVehicleAlarm.setMaplat(12l); thVehicleAlarm.setMaplon(12l);
			 * thVehicleAlarm.setEndGpsSpeed(12l); data.add(thVehicleAlarm); }
			 * tb.save(data);
			 */

			// Log4j配置
			PropertyConfigurator.configure(config.getLog4jProperties("log4j"));

			// 从配置文件读取管理端口
			managePort = config.getIntValue("ManagePort");

			// 执行用户输入命令
			executeCommand(command, managePort);

			// 打开管理端口
			try {

				serverListener = new ServerSocket(managePort, 1,
						InetAddress.getByName("127.0.0.1"));
			} catch (Exception e) {
				System.out.println("DataAnalysisService管理端口启动异常,服务可能已启动！" + e);
				return;
			}

			// 启动服务
			startService(serverListener);

		} catch (Exception e) {

			if (serverListener != null)
				System.out.println("DataAnalysisService服务异常终止！ " + e);
			else
				System.out.println("Can not start DataAnalysisService. " + e);
			
			e.printStackTrace();
		}

		// 终止虚拟机
		System.exit(0);

	}

	/**
	 * 程序主线程运行
	 */

	public void run() {

		try {

			String manageFlag = config.getStringValue("ManageFlag");
			//判断关键点时间容差
			long  keyPointTimeTolerance = config.getIntValue("KeyPointTimeTolerance");
			PermeterInit.setKeyPointTimeTolerance(keyPointTimeTolerance);
			
			KeyPointTimer kyTimer = new KeyPointTimer();
			kyTimer.start();
			
			if (manageFlag != null && Constant.MANAGEFLAG.equals(manageFlag)) {
				// 多msg处理
				String msg;
				String nodeName;
				int i = 1;

				while (true) {
					nodeName = "msgServiceManage|msg" + i;
					msg = config.getStringValue("msgServiceManage|msg" + i);
					// 取不到msg配置则跳出循环
					if (msg == null) {
						break;
					}
					UserInfo userInfo = new UserInfo();
					userInfo.setMsgServicePort(config.getIntValue(nodeName
							+ "|msgServicePort"));
					userInfo.setMsgServiceAddr(config.getStringValue(nodeName
							+ "|msgServiceAddr"));
					userInfo.setReConnectTime(config.getIntValue(nodeName
							+ "|reConnectTime"));
					userInfo.setConnectStateTime(config.getIntValue(nodeName
							+ "|connectStateTime"));
					userInfo.setLogintype(config.getStringValue(nodeName
							+ "|logintype"));
					userInfo.setUserid(config.getStringValue(nodeName
							+ "|userid"));
					userInfo.setPassword(config.getStringValue(nodeName
							+ "|password"));

					// 启动报文接收线程
					ReceiveThread receiveThread = new ReceiveThread(userInfo);
					receiveThread.start();

					i++;

				}
			} else {

				// 初始化节点管理器
				ConfigServer c = new ConfigServer();
				c.init(config);
			}

			sleep(5);
			// 默认报文分发处理线程数
			int analyseThreadCount = config
					.getIntValue("DataDistributeThreadCount");
			int m = 0;

			// 启动报文分发处理线程
			while (m < analyseThreadCount) {
				DataDistributeThread ataDistribute = new DataDistributeThread();
				ataDistribute.start();
				m++;
			}

			// 启动业务分发处理线程缓存池
			BussinesDistributeThreadPool.init();

			// 数据存储线程数
			int dataSaveThreadCount = config.getIntValue("DataSaveThreadCount");
			int n = 0;

			// 启动数据存储线程组
			while (n < dataSaveThreadCount) {
				DataSaveThread dataSaveThread = new DataSaveThread();
				dataSaveThread.start();
				n++;
			}

//			MonitorThread monitor = new MonitorThread();
//			monitor.start();
			System.out.println("DataAnalysisService started.");
			logger.info("DataAnalysisService started.");
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("Can not start DataAnalysisService.初始化出错.", e);
			System.exit(1);
		}
	}

	/**
	 * 启动服务
	 * 
	 * @throws IOException
	 */
	private static void startService(ServerSocket serverListener)
			throws IOException {

		// 主线程
		DataAnalysisServiceMain mainThread = null;

		Socket sock = null;

		mainThread = new DataAnalysisServiceMain();// 主线程
		logger.info(Constant.ver);

		// 启动主线程
		mainThread.start();
		logger.info("DataAnalysisService服务启动成功！");

		// 监听管理端口
		try {

			while (true) {
				sock = serverListener.accept();
				// SOCKET 读取超时值
				sock.setSoTimeout(Constant.MAIN_SOCKET_TIMEOUT);

				BufferedReader in = new BufferedReader(new InputStreamReader(
						sock.getInputStream()));
				String readStr = in.readLine();

				// 停止服务指令
				if (readStr.equals(Constant.COMMAND_SHUTDOWN)) {

					logger.info("DataAnalysisService收到停止命令：" + readStr);
					in.close();
					serverListener.close();
					if (mainThread != null) {
						mainThread.isRunning = false;
						mainThread.interrupt();
						// 等待子线程结束
						try {
							Thread.sleep(500);
						} catch (Exception e) {
							logger.error("停止服务时出错 ", e);
						}
					}

					logger.info("DataAnalysisService服务停止！");
					break;

					// 服务状态查询指令
				} else if (readStr.equals(Constant.COMMAND_STATUS)) {
					PrintWriter out = new PrintWriter(sock.getOutputStream());
					out.println(Constant.COMMAND_STATUS);
					out.flush();
					out.println("+ " + Constant.ver + " is running.");
					out.flush();
					int listenport = 0;
					if (listenport > 0)
						out.println("Udp listener is listening on port "
								+ listenport + ".");
					else
						out.println("Udp listener is not alive.");
					out.flush();

					in.close();
					out.close();
				} else
					in.close();
			}

		} catch (Exception e) {
			logger.error("查询状态时出错 ", e);
		} finally {

			sock.close();
		}
	}

	/**
	 * 执行输入命令
	 * 
	 * @param command
	 */
	private static void executeCommand(String command, int managePort) {

		if (command != null) {
			// 停止服务
			if (command.equalsIgnoreCase(Constant.SERVICE_STOP)) {
				shutdown(managePort);
				return;
				// 状态查询
			} else if (command.equalsIgnoreCase(Constant.SERVICE_STATUS)) {
				showStatus(managePort);
				return;
			}
		}
	}

	/**
	 * 停止服务
	 */
	private static void shutdown(int managePort) {
		try {
			// 向管理端口发送停止服务指令
			Socket sock = new Socket("127.0.0.1", managePort);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			out.println(Constant.COMMAND_SHUTDOWN);
			out.flush();
			out.close();
			sock.close();
			logger.info("+ DataAnalysisService Stopped.");
		} catch (Exception e) {
			logger.error(
					"- Can not stop DataAnalysisService. Service may be not running.",
					e);
		}
	}

	/**
	 * 显示当前服务状态
	 */
	private static void showStatus(int managePort) {
		try {
			// 向管理端口发送服务状态查询指令
			Socket sock = null;
			PrintWriter out = null;
			BufferedReader in = null;
			String info = null;
			try {
				sock = new Socket("127.0.0.1", managePort);
			} catch (Exception e) {
				System.out.println("- DataAnalysisService is not running.");
				return;
			}
			out = new PrintWriter(sock.getOutputStream());
			in = new BufferedReader(
					new InputStreamReader(sock.getInputStream()));
			out.println(Constant.COMMAND_STATUS);
			out.flush();

			info = in.readLine();
			if (!info.equals(Constant.COMMAND_STATUS)) {
				logger.info("DataAnalysisService is not running.");
				in.close();
				out.close();
				sock.close();
				return;
			}
			info = in.readLine();
			while (info != null) {
				logger.info(info);
				info = in.readLine();
			}// End while

			in.close();
			out.close();
			sock.close();

		} catch (Exception e) {
			logger.error(e);
		}
	}
}
