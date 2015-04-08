/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>时        间：2013-9-9  上午9:07:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.statusservice.io.IoClient;
import com.ctfo.statusservice.model.SystemConfig;
import com.ctfo.statusservice.model.TaskProperties;
import com.ctfo.statusservice.task.DriverTask;
import com.ctfo.statusservice.util.Constant;
import com.ctfo.statusservice.util.SystemUtil;



/*****************************************
 * <li>描        述：程序入口		
 * 
 *****************************************/
public class StatusServiceMain {
	private static final Logger logger = LoggerFactory.getLogger(StatusServiceMain.class);
	
	/** 定时任务执行列表 */
	static ScheduledExecutorService service = null;
	
	/*****************************************
	 * <li>描        述：程序主方法 		</li><br>
	 * <li>时        间：2013-9-9  上午9:07:53	</li><br>
	 * <li>参数： @param args			</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
		ServerSocket listener = null;
		try{
			System.out.println("状态服务应用程序正在启动中 ... ...");
			logger.info("状态服务应用程序正在启动中 ... ...");
			ApplicationContext context = new ClassPathXmlApplicationContext("spring-*.xml");
			logger.info("-StatusServiceMain--(1)-初始化完成");  
			System.out.println("-StatusServiceMain--(1)-初始化完成");
			
//			启动驾驶员缓存定时任务
			service = Executors.newScheduledThreadPool(1);
			DriverTask task = new DriverTask();
			TaskProperties driverPro = (TaskProperties) context.getBean("driverTaskProperties");
			task.setName("DriverTask");
			task.init();
			service.scheduleAtFixedRate(task, driverPro.getDelay(), driverPro.getPeriod(), driverPro.getTimeUnit());
			
//			报警消息发送线程
			Thread sendMsgThread = (Thread) context.getBean("sendMsgThread");
			sendMsgThread.start();
			logger.info("报警消息发送线程、分配线程(2)-初始化完成");  
			System.out.println("报警消息发送线程、分配线程(2)-初始化完成");
			
//			指令解析线程
			Thread commandParseThread = (Thread) context.getBean("commandParseThread");
			commandParseThread.start();
//			指令分配线程
			Thread allocationInstructionThread = (Thread) context.getBean("allocationInstructionThread");
			allocationInstructionThread.start();
			logger.info("指令解析、分配线程(3)-初始化完成");  
			System.out.println("指令解析、分配线程(3)-初始化完成");
			
//			定时器线程
			Thread entAlarmSettingSyncTask = (Thread) context.getBean("entAlarmSettingSyncTask");
			entAlarmSettingSyncTask.start();
			
			Thread syncUpdateOrAddVehicheCacheTask = (Thread) context.getBean("syncUpdateOrAddVehicheCacheTask");
			syncUpdateOrAddVehicheCacheTask.start();
			
			Thread vehicleClearUpdate = (Thread) context.getBean("vehicleClearUpdate");
			vehicleClearUpdate.start();
			
			
			logger.info("-StatusServiceMain--(4)-存储线程部分启动完成");  
			System.out.println("-StatusServiceMain--(4)-存储线程部分启动完成");
			
//			通讯部分
			try {
				IoClient ioClient = (IoClient) context.getBean("ioClient");
				ioClient.connect();
				logger.info("-StatusServiceMain--(5)-通讯部分启动完成");  
				System.out.println("-StatusServiceMain--(5)-通讯部分启动完成");
			} catch (InterruptedException e) {
				throw new Exception("初始化通讯服务异常:"+e.getMessage(),e);
			}
			// 打开管理端口
			try {
				SystemConfig systemConfig = (SystemConfig) context.getBean("systemConfig");
				Integer managementPort = systemConfig.getManagementPort();
				if(managementPort == null || managementPort == 0){
					throw new Exception("打开管理端口异常(原因:1.配置获取错误):"+ managementPort );
				}
				listener = new ServerSocket(managementPort, 1, InetAddress.getLocalHost());
				logger.info("-StatusServiceMain--(6)-打开管理端口["+managementPort+"]完成!");  
				System.out.println("-StatusServiceMain--(6)-打开管理端口["+managementPort+"]完成!");
			} catch (Exception e) {
				throw new Exception("打开管理端口异常(原因:1.端口被占用,2.服务已启动。):"+e.getMessage(),e);
			}
			
			SystemUtil.generagePid(); 
			
			logger.info("-StatusServiceMain-NormalStart-(ok)-应用程序启动完成 !");
			System.out.println("-StatusServiceMain-NormalStart-(ok)---应用程序启动完成 !");
		
		}catch(Exception e){
			System.out.println("---(error)---应用程序启动异常!"+ e.getMessage() + " \r\n 程序退出！！！");
			logger.error("-StatusServiceMain--(error)-应用程序启动异常 !"+ e.getMessage() + " \r\n 程序退出！！！", e);
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
					logger.info("-StatusServiceMain-- is stopping.");  
					System.out.println("-StatusServiceMain- is stopping.");
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
