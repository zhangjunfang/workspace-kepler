/**
 * 
 */
package com.ctfo.trackservice.task;

import static org.junit.Assert.fail;

import org.junit.Test;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.RedisConnectionPool;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.ConfigLoader;
import com.ctfo.trackservice.util.Utils;

/**
 * @author zjhl
 *
 */
public class AlarmSetCacheTaskTest {
	
	public static String taskName = "AlarmSetCacheTask";
	
	private TaskAdapter task = null;
	
	public AlarmSetCacheTaskTest() throws Exception{
		ConfigLoader.init(new String[]{"-d", "E:\\WorkSpace\\trank\\TrackService\\src\\", "start"}); 
		OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
		RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
		OracleService.init();
		for (TaskConfiger tc : ConfigLoader.tasks) {
			if(taskName.equals(tc.getName())){
				Class<?> taskClass = Class.forName(tc.getImpClass());
				task = (TaskAdapter) taskClass.newInstance();
				task.setName(tc.getName());
				task.setConfig(tc.getConfig());
			}
		}
	}
	
	@Test
	public void testInit() {
		try {
			task.init(); 
		} catch (Exception e) {
			fail("初始化异常:" + e.getMessage());
		}
	}

	@Test
	public void testExecute() {
		try {
			task.init(); 
			Thread.sleep(10000);
			task.execute();
		} catch (Exception e) {
			fail("执行异常:" + e.getMessage());
		}
	}

}
