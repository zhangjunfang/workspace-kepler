package com.ctfo.analy;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Timer;

import org.apache.log4j.Logger;

import com.ctfo.analy.activemq.ActivemqInit;
import com.ctfo.analy.addin.AddInManagerInit;
import com.ctfo.analy.connpool.JDCConnectionInit;
import com.ctfo.analy.dao.CacheDataManager;
import com.ctfo.analy.dao.CustTipConfig;
import com.ctfo.analy.dao.LoadGPSInspectionConfig;
import com.ctfo.analy.dao.SQLPool;
import com.ctfo.analy.dao.SyncVehicleAlarmThread;
import com.ctfo.analy.io.IoInit;
import com.ctfo.analy.protocal.AnalyseServiceInit;
import com.ctfo.analy.util.CDate;
import com.lingtu.xmlconf.XmlConf;

 /**
  * 分析服务启动类
  * @author LiangJian
  * 2012-12-12 10:27:17
  */

public class DataAnalyMain {

	private static final Logger logger = Logger.getLogger(DataAnalyMain.class);

	/**
	 * 管理端口,用于接收和发送停止服务命令
	 */
	static int managePort;

	public static String gpsInspectionConfigFile = null;
	
	// 手动执行
	private static final String MANUALCOMMAND = "DataAnalyServer manual";
	/**
	 * 操作系统系统调用主函数
	 * 
	 * @param args
	 *            调用时给的参数字符串数组
	 */
	public static void main(String[] args) {
		String command = null;
		ServerSocket listener = null;
		String configFile = null;
		try {

			for (int i = 0; i < args.length; i++) {
				if (args[i].equalsIgnoreCase("-f")) {
					if (args.length == i + 1) {
						System.out.print("DataAnaly args error");
						printUsage();
						return;
					} else {
						configFile = args[++i];
					}
				}

				command = args[i];

			}
			if (command.equalsIgnoreCase("-VERSION")) {
				System.out.println("");
				return;
			}
			
			
			
			// 读取配置文件
			XmlConf conf = new XmlConf(configFile);
			// 读取配置文件SQL
			loadSQL(conf);
			// 从配置文件读取管理端口
			managePort = conf.getIntValue("ManagePort");
			
			 //获取redis告警缓存失效时间
			Constant.expiredSeconds = conf.getIntValue("expiredSeconds");

			// 从配置文件读取GPS巡检配置文件路径
			gpsInspectionConfigFile = conf.getStringValue("gpsInspectionConfigFile");
			
			
			 if (command.equals("-manual")) { // 外部访问接口
					String analType = "";
					String date = "";
					System.out.println("1.设置道路等级告警下发终端参数,0.退出");

					BufferedReader br = new BufferedReader(new InputStreamReader(
							System.in));
					boolean flag = false;

					StringBuffer msg = new StringBuffer("");
					do {
						if (flag) {
							System.out.println("1.设置道路等级告警下发终端参数,0.退出");
						}

						flag = true;
						System.out.print("请选择指令类型：");
						analType = br.readLine();
						if (!"1".equals(analType) && !"2".equals(analType)
								&& !"3".equals(analType)&& !"4".equals(analType) && !"0".equals(analType)) {
							analType = "";
							continue;
						} else if ("1".equals(analType)) {
							msg.append(analType);
							System.out.println("请确认道路等级告警是否下发终端:（1、下发 0、不下发）");
							date = br.readLine();

							if (!date.matches("[0-1]{1}")) {
								System.out.println("输入错误,请重新选择输入");
								msg.delete(0, msg.length());
								continue;
							}

							msg.append(":");
							msg.append(date);
							
							showManual(msg.toString());
							msg.delete(0, msg.length());
							break;
						}
						if ("0".equals(analType)) {
							break;
						}
					} while (true); // End while
					msg.delete(0, msg.length());
					msg = null;
					return;
				}
			
			
			// 打开管理端口
			try {
				listener = new ServerSocket(managePort, 1,InetAddress.getByName("127.0.0.1"));
			} catch (Exception e) {
				logger.error("打开管理端口时错误" + e.getMessage());
				System.out.println("不能启动轨迹存储服务，服务可能已经启动了");
				return;
			}
			
			//加载.properties文件 
//			String log4j = conf.getStringValue("log4j");
//			PropertyConfigurator.configure(log4j);


			// 初始化连接池
			JDCConnectionInit jDCConnectionInit = new JDCConnectionInit(conf);
			jDCConnectionInit.init();
			
			//GPS巡检缓存不通过MQ通知更新
			Timer timer = new Timer();
			LoadGPSInspectionConfig load = new LoadGPSInspectionConfig(0);
			load.run(); //初始化执行一次。
			timer.schedule(new LoadGPSInspectionConfig(1), CDate.getschedule("00:05"),1000*60*60*24); // 设置为每天00:05执行
			//timer.scheduleAtFixedRate(load, CDate.getschedule("00:00"),1000*60*60*24);
			
			// 同步数据库报警线程 - 初始化缓存
			SyncVehicleAlarmThread syncVehicleAlarmThread=new SyncVehicleAlarmThread(conf);
			syncVehicleAlarmThread.start();
			
			//企业超速、非法营运、疲劳驾驶告警设置缓存维护 每隔1分钟维护一次
			CacheDataManager cacheDataManager = new CacheDataManager();
			cacheDataManager.start();

			//启动mq客户端通道守护线程
			ActivemqInit activeMq = new ActivemqInit(conf);
			activeMq.init();
			
			
			//缓存定时更新第一次启动完成就启动插件类
//			if(syncVehicleAlarmThread.getCompleteNum()> 0){
			if(SyncVehicleAlarmThread.complete.take()){
				// 初始化分析类
				AnalyseServiceInit analyseServiceInit = new AnalyseServiceInit(conf);
				analyseServiceInit.init();
				
				// 启动插件管理线程
				AddInManagerInit addInManagerInit = new AddInManagerInit();
				addInManagerInit.init(conf);
	
				// 初始化通讯连接
				IoInit ioInit = new IoInit(conf);
				ioInit.init1();
			}
			
			/*ReadFileThread rt = new ReadFileThread();
			rt.start();*/

			System.out.println("DataAnaly started.");
			logger.info("分析服务" + Constant.VER + " 已经启动");
		} catch (Exception e) {	
			e.printStackTrace();
			logger.error("轨迹分析服务终止 ", e);
			System.out.println("Can not start DataAnaly. " + e);
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
				if (s.equals(Constant.COMMAND_SHUTDOWN))// 停止服务指令
				{
					in.close();
					listener.close();
					break;
				} else if (s.equals(Constant.COMMAND_STATUS))// 服务状态查询指令
				{
					PrintWriter out = new PrintWriter(sock.getOutputStream());

					in.close();
					out.close();
				} else if (s.matches(MANUALCOMMAND + ".*")) { // 手动执行统计
					String msg[] = s.split(":");
					if (msg.length == 3) {
						if ("1".equals(msg[1])) {
								if (msg[2]!=null&&!"".equals(msg[2])){
									CustTipConfig.getInstance().setSendToTerminal("1".equals(msg[2])?true:false);
								}
								CustTipConfig.getInstance().modDownMsgFile();

							printMsg(sock, " 道路等级告警参数" + msg[2] + " 设置成功",
									MANUALCOMMAND);
						} 
					} else {
						printMsg(sock, "1 is failed", MANUALCOMMAND);
					}
				}
				in.close();
			} catch (IOException e) {
				logger.error("创建socket出错。", e);
			}catch (Exception e) {
				logger.error("查询状态时出错 ", e);
			} finally {
				try {
					sock.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}

	}


	/******
	 * 初始化加载SQL
	 * @param conf
	 */
	public static void loadSQL(XmlConf conf){
		// 手机号对应报警设置Map
		SQLPool.getinstance().putSql("sql_queryAlarmVehicle", conf.getStringValue("database|sqlstatement|sql_queryAlarmVehicle"));

		// VID对应围栏报警设置Map
		SQLPool.getinstance().putSql("sql_queryAreaAlarm", conf.getStringValue("database|sqlstatement|sql_queryAreaAlarm"));

		// VID对应线路报警设置Map
		SQLPool.getinstance().putSql("sql_queryLineAlarm", conf.getStringValue("database|sqlstatement|sql_queryLineAlarm"));
		
		// VID对应道路等级报警设置Map
		SQLPool.getinstance().putSql("sql_queryRoadAlarm", conf.getStringValue("database|sqlstatement|sql_queryRoadAlarm"));
		
		//巡检SQL
		SQLPool.getinstance().putSql("sql_saveGpsInspection", conf.getStringValue("database|sqlstatement|sql_saveGpsInspection"));
		
		// 巡检车辆SQL
		SQLPool.getinstance().putSql("sql_gpsInsVehicle", conf.getStringValue("database|sqlstatement|sql_gpsInsVehicle"));
		
		// 加载增量车辆基本信息SQL
		SQLPool.getinstance().putSql("sql_queryNewVehicle", conf.getStringValue("database|sqlstatement|sql_queryNewVehicle"));
		
		// 加载已删除车辆基本信息SQL
		SQLPool.getinstance().putSql("sql_queryDelVehicle", conf.getStringValue("database|sqlstatement|sql_queryDelVehicle"));
		
		//查询企业车辆告警下发消息内容
		SQLPool.getinstance().putSql("sql_queryAlarmNotice", conf.getStringValue("database|sqlstatement|sql_queryAlarmNotice"));
		
	}
	
	public static void showManual(String command) {
		try {
			// 向管理端口发送服务状态查询指令
			Socket sock = null;
			try {
				sock = new Socket("127.0.0.1", managePort);
			} catch (Exception e) {
				logger.error(e);
				System.out.println("- DataAnaly is not running.");
				return;
			}
			System.out.println("Manual command :" + MANUALCOMMAND);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			BufferedReader in = new BufferedReader(new InputStreamReader(
					sock.getInputStream()));
			out.println(MANUALCOMMAND + ":" + command);
			out.flush();
			String info = in.readLine();

			if (!info.equals(MANUALCOMMAND)) {
				System.out.println("DataAnaly is not running.");
				in.close();
				out.close();
				sock.close();
				return;
			}

			info = in.readLine();
			while (info != null) {
				System.out.println(info);
				info = in.readLine();
			}

			in.close();
			out.close();
			sock.close();
		} catch (Exception e) {
			e.printStackTrace(System.err);
		}
	}
	
	/**************
	 * 向客户端发送消息
	 * 
	 * @param sock
	 * @param msg
	 * @param command
	 */
	private static void printMsg(Socket sock, String msg, String command) {
		PrintWriter out = null;
		try {
			out = new PrintWriter(sock.getOutputStream());
			out.println(command);
			out.flush();
			out.println(msg);
			out.flush();
			out.close();
		} catch (Exception e) {
			if (out != null) {
				out.close();
			}
			logger.error(e);
		}
	}
	
	public static void printUsage() {
		System.out
				.println("Usage: DataAnaly <-start|stop|manual|status|version> [-f configfile]");
	}

}
