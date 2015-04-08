/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>时        间：2013-9-9  上午9:07:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.core;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.commandservice.io.IoClient;
import com.ctfo.commandservice.model.SystemConfig;
import com.ctfo.commandservice.util.ConfigLoader;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.commandservice.util.SystemUtil;

/*****************************************
 * <li>描 述：程序入口
 * 
 *****************************************/
public class CommandServiceMain {
	private static final Logger logger = LoggerFactory.getLogger(CommandServiceMain.class);

	/*****************************************
	 * <li>描 述：程序主方法</li><br>
	 * <li>时 间：2013-9-9 上午9:07:53</li><br>
	 * <li>参数： @param args</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
		ServerSocket listener = null;
		try {
			System.out.println("指令服务应用程序正在启动中 ... ...");
			logger.info("指令服务应用程序正在启动中 ... ...");
			SystemUtil.progress("15");

			// 初始化配置文件
			ConfigLoader.getInstance();
			ConfigLoader.init(args);

			Thread.sleep(2000);
			SystemUtil.progress("30");
			ApplicationContext context = new ClassPathXmlApplicationContext("spring-*.xml");
			logger.info("-CommandServiceMain--(1)-初始化完成");

			// 创建远程诊断文件夹
			createRemoteDiagnosisDir();

			SystemUtil.progress("45");
			Thread.sleep(2000);
			// 指令解析线程
			Thread commandParseThread = (Thread) context.getBean("commandParseThread");
			commandParseThread.start();

			SystemUtil.progress("60");
			Thread.sleep(2000);
			// 指令分配线程
			Thread allocationInstructionThread = (Thread) context.getBean("allocationInstructionThread");
			allocationInstructionThread.start();
			logger.info("-CommandServiceMain--(2)-指令解析、分配线程初始化完成");

			SystemUtil.progress("75");
			Thread.sleep(2000);
			IoClient fileSaveServiceClient = (IoClient) context.getBean("ioClient");
			fileSaveServiceClient.connect();
			logger.info("-CommandServiceMain--(3)-通讯部分启动完成");

			SystemUtil.progress("85");
			Thread.sleep(2000);
			// 打开管理端口
			SystemConfig systemConfig = (SystemConfig) context.getBean("systemConfig");
			Integer managementPort = systemConfig.getManagementPort();
			if (managementPort == null || managementPort == 0) {
				throw new Exception("打开管理端口异常(原因:1.配置获取错误):" + managementPort);
			}
			listener = new ServerSocket(managementPort, 1, InetAddress.getLocalHost());
			logger.info("-CommandServiceMain--(4)-打开管理端口[" + managementPort + "]完成!");

			System.out.println("---(ok)---应用程序启动完成 !");
			logger.info("-CommandServiceMain--(ok)-应用程序启动完成 !");

			SystemUtil.progress("100");
		} catch (Exception e) {
			 System.out.println("-CommandServiceMain--(error)---应用程序启动异常!"+e.getMessage() + " \r\n 程序退出！！！");
			logger.error("-CommandServiceMain--(error)-应用程序启动异常 !" + e.getMessage() + " \r\n 程序退出！！！", e);
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
				if (s.equals(Constant.SHUTDOWNCOMMAND)) {
					logger.info("-CommandServiceMain-- is stopping.");
					System.out.println("-CommandServiceMain- is stopping.");
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

	/*****************************************
	 * <li>描 述：创建远程诊断文件夹</li><br>
	 * <li>时 间：2013-8-19 下午3:12:23</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	private static void createRemoteDiagnosisDir() {
		String currentDir = System.getProperty("user.dir");
		String separator = System.getProperty("file.separator");
		String remotediagnosisInfoDir = "";
		String remotediagnosisErrorDir = "";
		if (System.getProperty("os.name").toUpperCase().indexOf("WINDOWS") != -1) {
			remotediagnosisInfoDir = currentDir + separator + "info" + separator;
			remotediagnosisErrorDir = currentDir + separator + "error" + separator;
		} else {
			remotediagnosisInfoDir = currentDir + separator + "info" + separator;
			remotediagnosisErrorDir = currentDir + separator + "error" + separator;

		}
		File infoFile = new File(remotediagnosisInfoDir);
		File errorFile = new File(remotediagnosisErrorDir);
		if (!infoFile.isDirectory()) {
			infoFile.mkdir();
		}
		if (!errorFile.isDirectory()) {
			errorFile.mkdir();
		}
	}
}
