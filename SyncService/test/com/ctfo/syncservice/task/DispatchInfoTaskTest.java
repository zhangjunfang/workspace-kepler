package com.ctfo.syncservice.task;

import java.util.Map;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.junit.Test;

import com.ctfo.syncservice.dao.OracleDataSource;
import com.ctfo.syncservice.dao.RedisDataSource;
import com.ctfo.syncservice.util.ConfigLoader;
import com.ctfo.syncservice.util.TaskConfiger;

public class DispatchInfoTaskTest {

	static DispatchInfoTask task = null;
	/** Oracle数据库客户端连接(池)管理器 */
	private static OracleDataSource oracle = null; 
	/** Redis缓存服务客户端连接(池)管理器 */
	private static RedisDataSource redis = null;
	
	@Test
	public static void main(String[] args) throws Exception{ 
		ConfigLoader.load("src/syncservice.xml");// 加载配置文件信息
		//构建Oracle数据库连接池管理器///////////////////////////////////////////
		Map<String, String> orcf = ConfigLoader.CONFIG_ORACLEDB; 
		oracle = OracleDataSource.getInstance();
		
		oracle.setUrl(orcf.get("url"));
		oracle.setUsername(orcf.get("username"));
		oracle.setPassword(orcf.get("password"));
		oracle.setInitialSize(Integer.parseInt(orcf.get("initialSize")));
		oracle.setMaxActive(Integer.parseInt(orcf.get("maxActive")));
		oracle.setMinIdle(Integer.parseInt(orcf.get("minIdle")));
		oracle.init();
		//构建Redis缓存服务连接池管理器///////////////////////////////////////////
		Map<String, String> rds = ConfigLoader.CONFIG_REDISCACHE;
		redis = new RedisDataSource();
		redis.setRedis_host("10.8.3.163");
		redis.setRedis_port(6379);
		redis.setRedis_password("kcpt");
		redis.setMaxActive(Integer.parseInt(rds.get("maxActive")));
		redis.setMaxIdle(Integer.parseInt(rds.get("maxIdle")));
		redis.setMaxWait(Integer.parseInt(rds.get("maxWait")));
		redis.setTimeOut(Integer.parseInt(rds.get("timeOut")));
		redis.init();
		
		//task = new SimBindCodeTask();
		//Map<String, String> conf = new HashMap<String, String>();
		//conf.put("clearTime", "03");
		//conf.get("sql_syncAll");
		//conf.get("sql_syncIncrements");
		ScheduledExecutorService service = Executors.newScheduledThreadPool(1);
		for (TaskConfiger tc : ConfigLoader.TASK_LIST) {
			if(tc.getName().startsWith("DispatchInfoTask")){
				Class<?> taskClass = Class.forName(tc.getImpClass());
				task = (DispatchInfoTask) taskClass.newInstance();
				task.setName(tc.getName());
				task.setConf(tc.getProperties());
				task.setOracle(oracle);
				task.setRedis(redis);
				service.scheduleWithFixedDelay(task, 1, 30, TimeUnit.SECONDS);
				break;
			}
		}
	}
}
