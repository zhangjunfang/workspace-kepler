/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.core FileSaveServiceMain.java	</li><br>
 * <li>时        间：2013-9-9  上午9:07:53	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import org.dom4j.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.dao.OracleConnectionPool;
import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.io.IoClient;
import com.ctfo.filesaveservice.parse.CommandParseThread;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.SystemUtil;
import com.ctfo.filesaveservice.util.TaskAdapter;


/*****************************************
 * <li>描        述：程序入口		
 * 
 *****************************************/
public class FileSaveServiceMain {
	private static final Logger logger = LoggerFactory.getLogger(FileSaveServiceMain.class);
	/** 定时执行任务 */
	public static ScheduledExecutorService service;
	/** Redis连接池	 */
	public static RedisConnectionPool redisPool;
	/** Redis连接池	 */
	public static OracleConnectionPool oraclePool;
	/** 通讯客户端	 */
	private static IoClient ioClient = null;
	/** 监听	 */
	private static ServerSocket listener = null;
	
	/*****************************************
	 * <li>描        述：程序主方法 		</li><br>
	 * <li>时        间：2013-9-9  上午9:07:53	</li><br>
	 * <li>参数： @param args			</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {

		try{
			SystemUtil.progress("0"); 
//			（一） 加载配置文件信息
			ConfigLoader.init(args);
			SystemUtil.progress("15"); 
			
//			（二）启动Oracle数据库连接池
			oraclePool = new OracleConnectionPool(ConfigLoader.oracleProperties);
			SystemUtil.progress("30"); 
			
//			（三）启动Redis缓存服务连接池
			redisPool = new RedisConnectionPool(ConfigLoader.redisProperties);
			SystemUtil.progress("40");
			
//			（四）启动任务线程 
			Element tasksElement = ConfigLoader.tasksElement; 
			List<?> tasks = tasksElement.elements("task");
			service = Executors.newScheduledThreadPool(tasks.size());
			for (Object element : tasks) {
				Element taskEm = (Element)element;
				//	任务启用标记
				if (!Boolean.parseBoolean(taskEm.attributeValue("enable"))) {
					continue;
				}
				Class<?> taskClass = Class.forName(taskEm.elementTextTrim("class"));
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setName(taskEm.attributeValue("name"));
				for (Object obj : taskEm.element("properties").elements("property")){
					String key = ((Element)obj).attributeValue("name");
					String value = ((Element)obj).getTextTrim();
					task.setConfig(key, value);
					
				}
				
				Element interval = taskEm.element("interval"); 			// 任务间隔参数
				String unit = interval.attributeValue("unit").toLowerCase();
				long initialDelay = Long.parseLong(interval.attributeValue("delay"));// 延迟执行时间
				long period = Long.parseLong(interval.getTextTrim());	//间隔时间
//				任务启动
				task.init();
				service.scheduleAtFixedRate(task, initialDelay, period, ConfigLoader.gerTimeUnit(unit));
			}
			SystemUtil.progress("50");
			
//			（五）启动数据处理线程
			CommandParseThread parse = new CommandParseThread();
			parse.start();
			SystemUtil.progress("70");
			
//			（六）启动通讯
			ioClient = new IoClient();
			ioClient.connect();  
			SystemUtil.progress("90"); 
			
//			（七）打开监听端口
			
			listener = new ServerSocket(ConfigLoader.managementPort, 1, InetAddress.getLocalHost());
			SystemUtil.progress("100");

			System.out.println("-FileSaveServiceMain--(ok)--NormalStart--应用程序启动完成 !");
			logger.info("-FileSaveServiceMain--(ok)--NormalStart--应用程序启动完成 !");
			
		}catch(Exception e){
			System.out.println("---(error)---应用程序启动异常!"+ e.getMessage() + " \r\n 程序退出！！！");
			logger.error("-FileSaveServiceMain--(error)-应用程序启动异常 !"+ e.getMessage() + " \r\n 程序退出！！！", e);
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
					logger.info("-FileSaveServiceMain-- is stopping.");  
					System.out.println("-FileSaveServiceMain- is stopping.");
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
