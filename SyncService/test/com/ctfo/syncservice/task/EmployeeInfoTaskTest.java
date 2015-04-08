package com.ctfo.syncservice.task;

import java.util.Map;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.junit.Test;

import com.ctfo.syncservice.dao.OracleDataSource;
import com.ctfo.syncservice.util.ConfigLoader;
import com.ctfo.syncservice.util.TaskConfiger;

public class EmployeeInfoTaskTest {
	
	static EmployeeInfoTask task = null;
	/** Oracle数据库客户端连接(池)管理器 */
	private static OracleDataSource oracle = null; 
	
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
	
		
		//task = new SimBindCodeTask();
		//Map<String, String> conf = new HashMap<String, String>();
		//conf.put("clearTime", "03");
		//conf.get("sql_syncAll");
		//conf.get("sql_syncIncrements");
		ScheduledExecutorService service = Executors.newScheduledThreadPool(1);
		for (TaskConfiger tc : ConfigLoader.TASK_LIST) {
			if(tc.getName().startsWith("EmployeeInfoTask_byFile")){
				Class<?> taskClass = Class.forName(tc.getImpClass());
				task = (EmployeeInfoTask) taskClass.newInstance();
				task.setName(tc.getName());
				task.setConf(tc.getProperties());
				task.setOracle(oracle);
				service.scheduleWithFixedDelay(task, 1, 10, TimeUnit.SECONDS);
				break;
			}
		}
	}
}
