package com.ctfo.syn.kcpt_oracle2mysql;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.apache.log4j.Logger;

import com.ctfo.syn.activemq.FeedbackActiveMQ;
import com.ctfo.syn.activemq.OrgActiveMQ;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.httservice.HttpThread;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.oracle2memcache.SynMemPublishinfo;
import com.ctfo.syn.oracle2memcache.SynMemRoadCondition;
import com.ctfo.syn.oracle2memcache.SynMemVehicleSta;
import com.ctfo.syn.oracle2memcache.SynMemVehicleTop;

public class SynMain {
	private final static Logger logger = Logger.getLogger(SynMain.class); 
	private final long SYN_FINE_MIN = 5 * 60 * 1000;
	private final long SYN_TEN_MIN = 10 * 60 * 1000;
	private final long SYN_TWENTY_MIN = 20 * 60 * 1000;
	private final long SYN_FF_SEC = 45 * 1000;
	private final long SYN_ONE_HOUR = 60 * 60 * 1000;
	
	public SynMain(String arg) {
		loadDataBase(arg);
	}
	
	/**
	 * 初始化数据库连接池
	 * @param arg
	 */
	public void loadDataBase(String arg) {
		FileInputStream in = null;
		Properties property = null;
		try{
			in = new FileInputStream(arg);
			property = new Properties();
			property.load(in);
			String url = property.getProperty("oracle_jdbcUrl");
			String username = property.getProperty("oracle_user");
			String pwd = property.getProperty("oracle_password");
			int timeout = Integer.parseInt(property.getProperty("oracle_pool_dbreconnectwait"));
			int maxLimit = Integer.parseInt(property.getProperty("oracle_pool_maxlimit"));// 连接池中最大连接数
			int minLimit = Integer.parseInt(property.getProperty("oracle_pool_minlimit"));// 连接池中最小连接数
			int InitialLimit = Integer.parseInt(property.getProperty("oracle_pool_initiallimit")); // 初始化连接数 
			String propertyCheckInterval = property.getProperty("oracle_pool_propertyCheckInterval");
			// 初始ORACLE连接池
			OracleConnectionPool.initConnection(url, username, pwd, minLimit + "", maxLimit + "", InitialLimit + "", timeout + "", propertyCheckInterval);
			
			String redisHost = property.getProperty("redisHost");
			int redisPort = Integer.parseInt(property.getProperty("redisPort"));
			String redisPwd = property.getProperty("redisPwd");
			int redisMaxActive = Integer.parseInt(property.getProperty("redisMaxActive"));
			int redisMaxIdle = Integer.parseInt(property.getProperty("redisMaxIdle")); 
			int redisMaxWait = Integer.parseInt(property.getProperty("redisMaxWait"));
			int redisTimeout = Integer.parseInt(property.getProperty("redisTimeout")); 
			// 初始化REDIS连接池
//			RedisConnectionPool.initRedisConnectionPool(redisHost, redisPort, redisPwd,redisMaxActive,redisMaxIdle ,redisMaxWait,redisTimeout);
			RedisServer.initRedisService(redisHost, redisPort, redisPwd,redisMaxActive,redisMaxIdle ,redisMaxWait,redisTimeout);
			
			SynPool synPool = SynPool.getinstance();
			synPool.putSql("mysql_driverClass", property.getProperty("mysql_driverClass"));
			synPool.putSql("mysql_jdbcUrl", property.getProperty("mysql_jdbcUrl"));
			synPool.putSql("mysql_user", property.getProperty("mysql_user"));
			synPool.putSql("mysql_password", property.getProperty("mysql_password"));
			synPool.putSql("kcpt_oracle_syn_mysql", property.getProperty("kcpt_oracle_syn_mysql"));
			synPool.putSql("kcptsql_tbserviceview", property.getProperty("kcptsql_tbserviceview"));
			//synPool.putSql("kcptsql_thvehiclealarm", property.getProperty("kcptsql_thvehiclealarm"));
//			synPool.putSql("memcacheaddr", property.getProperty("memcacheaddr"));
//			synPool.putSql("memcacheMainAddr", property.getProperty("memcacheMainAddr"));
			synPool.putSql("oracle_memcache_org", property.getProperty("oracle_memcache_org"));
			synPool.putSql("oracle_memcache_feedback", property.getProperty("oracle_memcache_feedback"));
			synPool.putSql("provincecodes", property.getProperty("provincecodes"));
			synPool.putSql("RoadConditionsURLID", property.getProperty("RoadConditionsURLID"));
			synPool.putSql("oracle_memcache_org", property.getProperty("oracle_memcache_org"));
			synPool.putSql("oracle_memcache_sysPublish", property.getProperty("oracle_memcache_sysPublish"));
			synPool.putSql("oracle_memcache_orgPublish", property.getProperty("oracle_memcache_orgPublish"));

			synPool.putSql("oracle_memcache_network_vehicle", property.getProperty("oracle_memcache_network_vehicle"));
			synPool.putSql("oracle_memcache_operation_vehicle", property.getProperty("oracle_memcache_operation_vehicle"));
			synPool.putSql("oracle_memcache_online_vehicle", property.getProperty("oracle_memcache_online_vehicle"));
			synPool.putSql("oracle_memcache_driving_vehicle", property.getProperty("oracle_memcache_driving_vehicle"));
			
			synPool.putSql("oracle_memcache_vehicleTop", property.getProperty("oracle_memcache_vehicleTop"));
			synPool.putSql("oracle_memcache_vehicleTeamTop", property.getProperty("oracle_memcache_vehicleTeamTop"));
			synPool.putSql("httpMemAddr", property.getProperty("httpMemAddr"));
			synPool.putSql("sleepTime", property.getProperty("sleepTime"));
			synPool.putSql("oracle_organization", property.getProperty("oracle_organization"));
			synPool.putSql("mysql_organization", property.getProperty("mysql_organization"));
			synPool.putSql("oracle_vehicle", property.getProperty("oracle_vehicle"));
			synPool.putSql("mysql_vehicle", property.getProperty("mysql_vehicle"));
			
			synPool.putSql("kcptsql_driver", property.getProperty("kcptsql_driver"));
			synPool.putSql("kcpt_mysql_tb_vehicle", property.getProperty("kcpt_mysql_tb_vehicle"));
			synPool.putSql("kcpt_mysql_realTime_tb_vehicle", property.getProperty("kcpt_mysql_realTime_tb_vehicle"));
			
			synPool.putSql("activemqAddr", property.getProperty("activemqAddr"));
			synPool.putSql("orgAddr", property.getProperty("orgAddr"));
			synPool.putSql("feedbackAddr", property.getProperty("feedbackAddr"));
			
			logger.info("初始化ORACLE连接池成功!");
		}catch(Exception ex){
			logger.error("Loading " + arg + " to failed.",ex);
			
		}finally{
			property = null;
			if(in != null){
				try {
					in.close();
				} catch (IOException e) {
					logger.error(e);
				}
			}
		}
	}
	
	
	public static void main(String[] args){
		SynMain synMain = new SynMain(args[0]);
		synMain.run();
	}
	
	
	private void run(){
		SynDataSQL syndatasql = new SynDataSQL();
		syndatasql.syn(); // 初始化同步所有数据
		logger.info("休眠30S，启动同步服务");
		try {
			Thread.sleep(30 * 1000); // 休眠30S，启动同步服务
//			Thread.sleep(1 * 1000); // 休眠30S，启动同步服务
		} catch (InterruptedException e) {
			logger.error(e);
		} 
		ConnectMemcachePool connectMemcachePool = new ConnectMemcachePool();
		connectMemcachePool.initMemcache();
		
		ScheduledExecutorService service = Executors.newScheduledThreadPool(10);

		Runnable synData = new SynData();
		service.scheduleAtFixedRate(synData, 0, SYN_FINE_MIN, TimeUnit.MILLISECONDS);

		//启动同步MEMCACHE数据
		Runnable codeData = new SynGeneralCodeData();
		service.scheduleAtFixedRate(codeData, 0, SYN_FINE_MIN, TimeUnit.MILLISECONDS);

		// 启动同步MEMCACHE数据 - 信息反馈
//		Runnable feedback = new SynMemFeedback();
//		service.scheduleAtFixedRate(feedback, 0, SYN_FINE_MIN, TimeUnit.MILLISECONDS);

		 //启动同步MEMCACHE数据 - 路况信息
		SynMemRoadCondition road = new SynMemRoadCondition();
		service.scheduleAtFixedRate(road, 0, SYN_TEN_MIN, TimeUnit.MILLISECONDS);
		
		// 启动同步MEMCACHE数据 - 公告管理
		Runnable publishinfo = new SynMemPublishinfo();
		service.scheduleAtFixedRate(publishinfo, 0, SYN_TWENTY_MIN, TimeUnit.MILLISECONDS);
		
		// 启动同步MEMCACHE数据 - 车辆统计
		Runnable vehicleSta = new SynMemVehicleSta();
		service.scheduleAtFixedRate(vehicleSta, 0, SYN_FF_SEC, TimeUnit.MILLISECONDS);

		// 启动同步MEMCACHE数据 - 排行榜
		Runnable vehicleTop = new SynMemVehicleTop();
		service.scheduleAtFixedRate(vehicleTop, 0, SYN_ONE_HOUR, TimeUnit.MILLISECONDS);
		
//		消息总线地址
		String mqUrl = SynPool.getinstance().getSql("activemqAddr");
		//启动MQ - 企业频道
		String t_org = SynPool.getinstance().getSql("orgAddr");
		Thread orgActiveMq = new OrgActiveMQ(mqUrl,t_org);
		orgActiveMq.start();
		
		//启动MQ - 反馈频道
		String t_feedback = SynPool.getinstance().getSql("feedbackAddr");
		Thread feedbackActiveMq = new FeedbackActiveMQ(mqUrl,t_feedback);
		feedbackActiveMq.start();
		
		
		// 启动同步数据到HTTP服务的鉴权数据到Memcached
		Runnable http = new HttpThread();
		service.scheduleAtFixedRate(http, 0, SynPool.getinstance().getSql("sleepTime") == null ? 5 * 60 * 1000 : Long.parseLong(SynPool.getinstance().getSql("sleepTime")), TimeUnit.MILLISECONDS);
	}
}
