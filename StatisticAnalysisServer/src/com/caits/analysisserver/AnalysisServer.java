package com.caits.analysisserver;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Iterator;
import java.util.List;
import java.util.StringTokenizer;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.DispatchThread;
import com.caits.analysisserver.bean.SystemBaseInfo;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.manual.ManualCommand;
import com.caits.analysisserver.manual.ManualExecute;
import com.caits.analysisserver.utils.LoadXml;
import com.ctfo.redis.pool.JedisConnectionPool;


//import org.apache.log4j.PropertyConfigurator;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.dom4j.Document;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： GradeStaistic <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 */
public class AnalysisServer extends Thread {

	// Log4j Category
	public static Logger logger = LoggerFactory.getLogger(AnalysisServer.class);

	private static final String CONFIGXMLFILE = "StatisticsCenter.xml";

	// 停止服务命令
	private static final String SHUTDOWNCOMMAND = "AnalysisServer shutdown";

	// 状态查询指令
	private static final String STATUSCOMMAND = "AnalysisServer status";

	// 手动执行
	private static final String MANUALCOMMAND = "AnalysisServer manual";

	// service version no.
	private static final String VERSION = "KCPT-AnalysisServer-1.0.0.0-2012.06.06";

	// 管理端口,用于接收和发送停止服务命令
	private static int managePort = 7230;

	private static Document document = null;

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		Long mainStartTime = System.currentTimeMillis();
		String command = null;
		String configFile = null;
		AnalysisServer mainThread = null;
		ServerSocket listener = null;
		for (int i = 0; i < args.length; i++) {
			if (args[i].equalsIgnoreCase("-f")) {
				if (args.length == i + 1) {
					printUsage();
					return;
				} else {
					configFile = args[++i];
				}
			}
			command = args[i];
		}// End for

		if (command.equalsIgnoreCase("-VERSION")) {
			System.out.println(VERSION);
			return;
		}

		// 读取XML配置文件
		if (configFile == null)
			configFile = getFileInClassPath(CONFIGXMLFILE);
		if (configFile == null) {
			System.out.println("Can not find the config file StatisticsCenter.xml in all classpath!");
			return;
		}

		try {
			// 从配置文件读取管理端口

			SAXReader saxReader = new SAXReader();
			File in = new File(configFile);
			document = saxReader.read(in);
			Element rootElt = document.getRootElement();

			// Log4j配置 TODO
//			PropertyConfigurator.configure(getFileInClassPath(rootElt.element("log4j").attributeValue("resource")));
			// 打开管理端口
			// 加载管理端口
			String isload = rootElt.element("ManagePort").attributeValue("isload");
			if (isload.equals("true")) {
				SystemBaseInfo sys = new SystemBaseInfo();
				managePort = Integer.parseInt(rootElt.element("ManagePort").elementTextTrim("value"));
				sys.setIsLoad("true");
				sys.setValue(rootElt.element("ManagePort").elementTextTrim("value"));
				SystemBaseInfoPool.getinstance().putBaseInfoMap("ManagePort",sys);
			}

			if (command.equalsIgnoreCase("-stop")) {// 停止服务
				shutdown();
				return;
			} else if (command.equalsIgnoreCase("-status"))// 状态查询
			{
				showStatus();
				return;
			} else if (command.equals("-manual")) { // 手动执行月、日
				ManualCommand mc = new ManualCommand();
				String msg = mc.generateCommand();
				if (msg==null||"".equals(msg)){
					System.out.println("未生成有效指令，请重新生成！");
				}else{
					showManual(msg);
				}
				return;
			}

			listener = new ServerSocket(managePort, 1,InetAddress.getByName("127.0.0.1"));

			// 加载SQL语句
			loadXml(rootElt.element("sql"));

			// 加载参数值
			loadInitParams(rootElt.element("params"));

			// 加载文件配置路径
			// 报警文件路径
			loadFileUrl(rootElt.element("alarmfileurl"));

			// 轨迹文件路径
			loadFileUrl(rootElt.element("trackfileurl"));

			// 驾驶行为事件路径
			loadFileUrl(rootElt.element("eventfileurl"));

			// 油量监控路径
			loadFileUrl(rootElt.element("oilUrl"));
			
			// 发动机负荷率
			loadFileUrl(rootElt.element("eloaddistFileUrl"));
			
			//加载跨天文件路径
			//加载跨天数
			getCrossDays(rootElt.element("crossdays"));
			// 报警文件路径
			loadFileUrl(rootElt.element("crossalarmfileurl"));

			// 轨迹文件路径
			loadFileUrl(rootElt.element("crosstrackfileurl"));

			// 驾驶行为事件路径
			loadFileUrl(rootElt.element("crosseventfileurl"));

			// 油量监控路径
			loadFileUrl(rootElt.element("crossoilUrl"));
			
			// 发动机负荷率
			loadFileUrl(rootElt.element("crosseloaddistFileUrl"));

			// 加载数据库XML，并且初始化数据库连接池
			loadXml(rootElt.element("database"));

			// 加载插件
			loadXml(rootElt.element("addins"));

			String redisHost = rootElt.element("redisHost").attributeValue("value");
			int redisPort = Integer.valueOf(rootElt.element("redisPort").attributeValue("value"));
			String redisPwd = rootElt.element("redisPwd").attributeValue("value");
			int redisMaxActive = Integer.valueOf(rootElt.element("redisMaxActive").attributeValue("value"));
			int redisMaxIdle = Integer.valueOf(rootElt.element("redisMaxIdle").attributeValue("value"));
			long redisMaxWait = Long.valueOf(rootElt.element("redisMaxWait").attributeValue("value"));
			int redisTimeout = Integer.valueOf(rootElt.element("redisTimeout").attributeValue("value"));
//			初始化缓存
			//RedisServer.initRedisService(redisHost, redisPort, redisPwd, redisMaxActive, redisMaxIdle, redisMaxWait, redisTimeout);
			JedisConnectionPool.initRedisConnectionPool(redisHost, redisPort,redisPwd,redisMaxActive ,redisMaxIdle ,redisMaxWait,redisTimeout);
			// 启动任务调度插件
			QuartzPlugIn example = new QuartzPlugIn();
			example.run();

		} catch (Exception e) {
			logger.error("打开管理端口时错误.", e);
			logger.info("不能启动统计分析服务，服务可能已经启动了");
			return;
		}
		// 启动主线程
		mainThread = new AnalysisServer();
		mainThread.run();
		Long mainEndtime= System.currentTimeMillis();
		//增加自动跑
		//FileTaskThread fileTaskThread2 = new FileTaskThread();
		//fileTaskThread.setDate(CDate.yearMonthDayConvertUtc(msg[2]));
	    //fileTaskThread2.isUsingSettingTime(false);
	    //fileTaskThread2.run();

		System.out.println("StatisAnalysis server has run!");
		System.out.println("统计分析启动时长：" +(mainEndtime-mainStartTime)/1000+"s");
		logger.info("统计分析启动时长：" +(mainEndtime-mainStartTime)/1000+"s");
		System.out.println("Listening port : " + managePort);
		// 监听管理端口
		while (true) {
			Socket sock;
			BufferedReader in = null;
			try {
				sock = listener.accept();
				// 读取指令
				sock.setSoTimeout(5000);
				in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
				String s = in.readLine();
				if (s.equals(SHUTDOWNCOMMAND))// 停止服务指令
				{
					System.exit(0);
					printMsg(sock, " 统计服务停止 成功", SHUTDOWNCOMMAND);
					break;
				} else if (s.equals(STATUSCOMMAND))// 服务状态查询指令
				{
					StringBuffer msg = new StringBuffer("");
					msg.append("+ " + VERSION + " is running.");
					msg.append("\n\r");
					msg.append("Oracle Connection size: " + OracleConnectionPool.listCacheInfos());
					printMsg(sock, msg.toString(), STATUSCOMMAND);
					msg.delete(0, msg.length());
				} else if (s.matches(MANUALCOMMAND + ".*")) { // 手动执行统计
					ManualExecute me = new ManualExecute();
					String[] result = me.execute(s);
					
					if (result!=null&&result.length==2){
						printMsg(sock, result[1],	MANUALCOMMAND);
					}else{
						printMsg(sock, "手动执行失败！",	MANUALCOMMAND);
					}
				}
				in.close();
			} catch (IOException e) {
				logger.error("创建socket出错。", e);
			} catch (Exception e) {
				logger.warn("查询状态时出错 " + e.getMessage());
			} finally {
				if (in != null) {
					try {
						in.close();
					} catch (IOException e1) {
						in = null;
					}
				}
			}
		} // End while
	}

	/*******
	 * 初始化加载参数值
	 * 
	 * @param param
	 */
	@SuppressWarnings("unchecked")
	private static void loadInitParams(Element param) {
		List<Element> list = param.elements("param");
		Iterator<Element> eltIt = list.iterator();
		while (eltIt.hasNext()) {
			Element elt = eltIt.next();
			SystemBaseInfo sys = new SystemBaseInfo();
			sys.setIsLoad(elt.attributeValue("isload"));
			sys.setValue(elt.elementTextTrim("value"));
			SystemBaseInfoPool.getinstance().putBaseInfoMap(elt.attributeValue("name"), sys);
		}// End while
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
			logger.error(e.getMessage(), e);
		}
	}

	@SuppressWarnings("unchecked")
	private static void loadXml(Element nodeElt) throws Exception {
		LoadXml loadXml = new LoadXml();
		List<Element> sqlList = nodeElt.elements("import");
		Iterator<Element> sqlIt = sqlList.iterator();
		String nodeType = nodeElt.getName();
		while (sqlIt.hasNext()) {
			Element resourceElt = sqlIt.next();
			String xmlPath = resourceElt.attributeValue("resource");
			loadXml.loadResource(nodeType, getFileInClassPath(xmlPath));
		}// End while
	}

	private static void loadFileUrl(Element node) throws Exception {
		String ky = node.getName();
		String value = node.elementTextTrim("value");
		if (!new File(value).exists()) {
			throw new Exception("找不到" + value + "文件夹");
		}
		FilePool.getinstance().putFile(ky, value);
	}
	
	private static void getCrossDays(Element node) throws Exception {
		String value = getNodeValue(node);
		if (value==null||!isNumeric(value)){
			logger.debug("没有找到跨天参数，默认跨天数为0，将使用默认目录。");
			value = "0";
		}
		FilePool.getinstance().setCorssdays(Integer.parseInt(value));
	}
	
	private static String getNodeValue(Element node) throws Exception {
//		String ky = node.getName();
		String value = node.elementTextTrim("value");
		if (value==null) {
			throw new Exception("找不到" + value + "文件夹");
		}
		return value;
	}
	
	public static boolean isNumeric(String str){
		  if(str.matches("\\d*")){
		   return true;
		  }else{
		   return false;
		  }
		 }


	public void run() {
		DispatchThread dispatchThread = new DispatchThread();
		dispatchThread.start();
	}

	/**
	 * 停止服务
	 */
	private static void shutdown() {
		try {
			System.out.println("shut down:" + SHUTDOWNCOMMAND);
			// 向管理端口发送停止服务指令
			Socket sock = new Socket("127.0.0.1", managePort);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			out.println(SHUTDOWNCOMMAND);
			out.flush();
			out.close();
			sock.close();
			System.out.println("+ StatisticCenter Stopped.");
		} catch (Exception e) {
			System.out.println("- Can not stop StatisticCenter. Service may be not running.");
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
				sock = new Socket("127.0.0.1", managePort);
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
				System.out.println("- StatisticCenter is not running.");
				return;
			}
			System.out.println("show Status:" + STATUSCOMMAND);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			BufferedReader in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
			out.println(STATUSCOMMAND);
			out.flush();
			String info = in.readLine();

			if (!info.equals(STATUSCOMMAND)) {
				System.out.println("StatisticCenter is not running.");
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
			logger.error(e.getMessage(), e);
		}
	}

	public static void showManual(String command) {
		try {
			// 向管理端口发送服务状态查询指令
			Socket sock = null;
			try {
				sock = new Socket("127.0.0.1", managePort);
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
				System.out.println("- StatisticCenter is not running.");
				return;
			}
			System.out.println("Manual command :" + MANUALCOMMAND);
			PrintWriter out = new PrintWriter(sock.getOutputStream());
			BufferedReader in = new BufferedReader(new InputStreamReader(sock.getInputStream()));
			out.println(MANUALCOMMAND + ":" + command);
			out.flush();
			String info = in.readLine();

			if (!info.equals(MANUALCOMMAND)) {
				System.out.println("StatisticCenter is not running.");
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

	/**
	 * 从当前路径和classpath中搜索指定文件名的文件
	 * 
	 * @param filename
	 *            要搜索的文件名,不能包含路径
	 * @return 包含完整路径的文件名,如果没有搜索到则返回的是null
	 */
	public static String getFileInClassPath(String filename) {
		FileInputStream fin = null;
		// 搜索当前路径
		try {
			fin = new FileInputStream(filename);
			fin.close();
			return filename;
		} catch (Exception e) {
		}
		// 获得系统变量classpath值
		String classpath = System.getProperty("java.class.path");
		StringTokenizer stk = new StringTokenizer(classpath, File.pathSeparator);
		String path = null;
		// 依次搜索classpath
		while (stk.hasMoreTokens()) {
			path = stk.nextToken();
			if (!path.endsWith(File.separator))
				path += File.separator;
			try {
				fin = new FileInputStream(path + filename);
				fin.close();
				// 找到文件则返回
				return path + filename;
			} catch (Exception e) {
			}
		}
		return null;
	}

	public static void printUsage() {
		System.out.println("Usage: AnalysisServer <-start|stop|manual|status|version> [-f configfile]");
	}
}
