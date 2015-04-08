package com.ctfo.datatransferserver;

import java.net.InetAddress;
import java.net.ServerSocket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.connpool.JDCConnectionInit;
import com.ctfo.datatransferserver.io.IoClient;
import com.ctfo.datatransferserver.protocal.AnalyseServiceInit;
import com.ctfo.datatransferserver.services.TaskManager;
import com.lingtu.xmlconf.XmlConf;

/**
 * 分析服务启动类
 * 
 * @author yangyi
 * 
 */

public class DataTransferMain {
	private static final Logger logger = LoggerFactory.getLogger(DataTransferMain.class);

	/**
	 * 管理端口,用于接收和发送停止服务命令
	 */
	static int managePort;

	/**
	 * 操作系统系统调用主函数
	 * 
	 * @param args
	 *            调用时给的参数字符串数组
	 */
	@SuppressWarnings("unused")
	public static void main(String[] args) {
		ServerSocket listener = null;
		String configFile = null;
		try {

			for (int i = 0; i < args.length; i++) {
				if (args[i].equalsIgnoreCase("-f")) {
					if (args.length == i + 1) {
						System.out.print("DataAnaly args error");
						return;
					} else {
						configFile = args[++i];
					}
				}
			}
			// 读取配置文件
			XmlConf conf = new XmlConf(configFile);

			// 从配置文件读取管理端口
			managePort = conf.getIntValue("ManagePort");

			// 打开管理端口
			try {
				listener = new ServerSocket(managePort, 1, InetAddress.getByName("127.0.0.1"));
			} catch (Exception e) {
				logger.error("打开管理端口时错误" + e.getMessage());
				System.out.println("不能启动轨迹存储服务，服务可能已经启动了");
				return;
			}

			// 初始化连接池
			JDCConnectionInit jDCConnectionInit = new JDCConnectionInit(conf);
			jDCConnectionInit.init();

			// 初始化任务
			TaskManager taskManager = new TaskManager(conf);
			taskManager.initTask();

			// 初始化分析类
			AnalyseServiceInit analyseServiceInit = new AnalyseServiceInit(conf);
			analyseServiceInit.init();

			// 初始化通讯连接
			// IoInit ioInit = new IoInit(conf);
			// ioInit.init();
			IoClient ioClient = new IoClient(conf);
			ioClient.connect();

			System.out.println("服务启动完成 - DataTransfer started!");
			logger.info("服务启动完成 - DataTransfer started!");
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("数据传输服务终止 ", e);
			System.out.println("Can not start DataTransfer. " + e);
			System.exit(0);
		}

	}

}
