package com.ctfo.syncservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.syncservice.util.TaskAdapter;

public class DispatchPlatformTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(DispatchPlatformTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/**	清理标记	*/
	private static boolean clearFlag = false;

	@Override
	public void init() {
		try {
			setName("DispatchPlatformTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
		} catch (Exception e) {
			logger.error("初始化并网转发第三方平台信息同步缓存任务异常:" + e.getMessage(), e); 
		}
	}

	@Override
	public void execute() {
		long start = System.currentTimeMillis();

		Connection conn = null;
		PreparedStatement ps = null;
		
		ResultSet rs = null;
		Jedis jedis = null;
		
		try {
//			判断清理时间间隔
			if((start - lastTime) > clearInterval){
				clearFlag = true;
				lastTime = System.currentTimeMillis();
			}
			
			if(clearFlag){
				Map<String,Long> oracleMap = new HashMap<String,Long>();
				conn = this.oracle.getConnection();
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoAll"));
				rs = ps.executeQuery();
				while(rs.next()){
					oracleMap.put(rs.getString("platform_no"),rs.getLong("update_time"));
				}
				
				Map<String, String> redisMap = new HashMap<String,String>();
				jedis = this.redis.getJedisConnection();
				jedis.select(8);
				redisMap = jedis.hgetAll("FORWARD.PLAT");
				Set<String> keys = redisMap.keySet();
				Iterator<String> it=keys.iterator();
				int count = 0;
				int count2 = 0;
				while(it.hasNext()){
					String key  = it.next();
					String[] arr = redisMap.get(key).split(",");
					long updateTime = Long.parseLong(arr[4]);
					if(oracleMap.containsKey(key) && updateTime > oracleMap.get(key)){
						ps = conn.prepareStatement(conf.get("sql_updateInfo"));					
						//ps.setString(2, arr[0]);
						//ps.setString(3, arr[1]);
						ps.setString(1, arr[2]);
						ps.setString(2, arr[3]);
						ps.setLong(3, Long.parseLong(arr[4]));
						ps.setString(4, key);
						ps.execute();
						count++;
					}else if(!oracleMap.containsKey(key)){
						ps = conn.prepareStatement(conf.get("sql_insertInfo"));
						ps.setString(1,GeneratorPK.instance().getPKString());
						ps.setString(2, key);
						ps.setString(3, arr[0]);
						ps.setString(4, arr[1]);
						ps.setString(5, arr[2]);
						ps.setString(6, arr[3]);
						ps.setLong(7, Long.parseLong(arr[4]));
						ps.execute();
						count2++;
					}
				}
				logger.info("并网转发第三方平台信息同步缓存完成！,新增【{}】条,更新【{}】条",count2,count);
				
			}
		}catch (Exception e) {
			logger.error("并网转发第三方平台信息同步缓存任务异常:" + e.getMessage(), e); 
		}finally{
			try {
				if(jedis != null){
					this.redis.returnJedisConnection(jedis);
				}
				
				if(conn!=null){
					conn.close();
				}
				if(rs!=null){
					rs.close();
				}
				if(ps!=null){
					ps.close();
				}
			}catch (SQLException e) {
				logger.error("并网转发第三方平台信息关闭连接异常:" + e.getMessage(), e); 
			}
		}
		
	}
	
}
