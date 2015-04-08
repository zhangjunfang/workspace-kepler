package com.ctfo.mcc.action;

import java.io.IOException;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.mcc.dao.OracleConnectionPool;
import com.ctfo.mcc.dao.RedisConnectionPool;
import com.ctfo.mcc.io.IoClient;
import com.ctfo.mcc.mq.AreaQueue;
import com.ctfo.mcc.service.OracleService;
import com.ctfo.mcc.service.PushCommandRedisCache;
import com.ctfo.mcc.service.SendCacheCommand;
import com.ctfo.mcc.service.SendDispatchService;
import com.ctfo.mcc.service.SendQueue;
import com.ctfo.mcc.utils.ConfigLoader;
import com.ctfo.mcc.utils.TaskAdapter;
import com.ctfo.mcc.utils.TaskConfiger;


/**
 * @author Administrator
 *
 */
public class InitFilter implements Filter {
	private static Logger logger = LoggerFactory.getLogger(InitFilter.class);
	private static String encoding = "UTF-8";
	/** 过滤器配置文件	 */
	private FilterConfig filterConfig;
	/** 定时任务执行列表	 */
	public static ScheduledExecutorService service = null;
	/** 指令发送队列		 */
	public static SendQueue sendQueue;
	/** 缓存指令处理线程	 */
	public static SendCacheCommand sendCache;
	/** 通讯模块		 */
	public static IoClient ioClient;
	/** oralce接口	 */
	public static OracleService oracleService;
	/** 缓存指令处理线程	 */
	public static AreaQueue areaQueue;
	/** 调度指令处理线程	 */
	public static SendDispatchService sendDispatchService;
	/** 缓存指令过期秒数（默认7天）	 */
	private static int commandExpireSecond = 604800;
	
	
	/**
	 * 初始化连接池、定时任务、业务线程、通讯模块
	 */
	@Override
	public void init(FilterConfig config) throws ServletException {
		this.filterConfig = config;
		try {
//		1. 读取配置信息
			String encode = filterConfig.getInitParameter("encoding");
			if(encode != null){
				encoding = encode;
			}
			ConfigLoader.init(filterConfig.getInitParameter("configName"));
//		2. 初始化连接池
			OracleConnectionPool.init(ConfigLoader.oracleProperties);
			RedisConnectionPool.init(ConfigLoader.redisProperties);
			OracleService.init(); 
			
//		3. 启动定时任务
			service = Executors.newScheduledThreadPool(ConfigLoader.TASKS.size());
			for (TaskConfiger tc : ConfigLoader.TASKS) {
				Class<?> taskClass = Class.forName(tc.getImpClass());
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setName(tc.getName());
				task.setConf(tc.getConfig());
				task.init();
				long delay = Long.parseLong(tc.getDelay());
				String period = tc.getPeriod();
				service.scheduleAtFixedRate(task, delay, Long.parseLong(period), tc.getUnit());
			}
//		4. 启动业务模块
			sendQueue = new SendQueue(); // 数据发送队列线程
			sendQueue.init(); 
			
			sendCache = new SendCacheCommand(); // 指令发送线程
			sendCache.init();
			
			sendDispatchService = new SendDispatchService();
			sendDispatchService.init();
			
			areaQueue = new AreaQueue(); 	// 消息总线监听线程 - 围栏解绑、状态更新
			areaQueue.setMqUrl(ConfigLoader.appParam.get("mqUrl"));
			areaQueue.setMqName(ConfigLoader.appParam.get("mqName"));
			areaQueue.init(); 
			 
//			设置指令缓存过期时间
			int expireSecond = Integer.parseInt(ConfigLoader.appParam.get("commandCacheExpireSeconds"));
			if(expireSecond > 0){
				commandExpireSecond = expireSecond;
			}
			PushCommandRedisCache.setCache_time(commandExpireSecond); 
			
			
			
//		5. 启动通讯模块
			ioClient = new IoClient(ConfigLoader.msgProperties);
			ioClient.init();
			logger.info("初始化过滤器[InitFilter]启动完成!");
		} catch (Exception e) {
			 throw new ServletException(e); 
		}
		
	}
	@Override
	public void destroy() { 
		service.shutdown(); 
		ioClient.shutdown();
	}

	@Override
	public void doFilter(ServletRequest request, ServletResponse response,
			FilterChain chain) throws IOException, ServletException {
		request.setCharacterEncoding(encoding);
		response.setCharacterEncoding(encoding); 
//		编码过滤
		chain.doFilter(request, response);//看到这没，这只要是传递下一个Filter
	}
	
	public FilterConfig getFilterConfig() {
		return filterConfig;
	}

	public void setFilterConfig(FilterConfig filterConfig) {
		this.filterConfig = filterConfig;
	}
}
