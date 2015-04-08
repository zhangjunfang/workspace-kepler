/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：驾驶员从业信息同步任务	(平台驾驶员纬度相关)
 * 
 *****************************************/
public class DriverListTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(DriverListTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/**	清理标记	*/
	private static boolean clearFlag = false;
	/** 最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();
	/*****************************************
	 * 
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		try {
			setName("DriverListTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("初始化驾驶员从业缓存同步任务异常:" + e.getMessage(), e); 
		}
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		long lastUtc = authUtc;
		int index = 0;
		int error = 0;
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
			Map<String, String> map =  new HashMap<String, String>();
			conn = this.oracle.getConnection();
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_driverListAll"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_driverListIncremental"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			long s1 = System.currentTimeMillis();
			while (rs.next()) {
				try {
					map.put(rs.getString("BUSSINESS_ID"), rs.getString("VAL")); 	// 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--DriverListTask--驾驶员从业信息同步任务 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--DriverListTask--驾驶员从业信息同步任务 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			long s2 = System.currentTimeMillis();
			jedis = this.redis.getJedisConnection();
			jedis.select(4);
			int mapSize = map.size();
			if(mapSize > 0){
				if(clearFlag){
					Set<String> oldKeys = jedis.hkeys("HD_DRIVER_LIST");
					if(oldKeys != null && oldKeys.size() > 0){
						int oldSize = oldKeys.size();
						Set<String> newKeys = map.keySet();
						oldKeys.removeAll(newKeys);
						logger.info("-----缓存中当前数量:[{}], 当前查询到的数量:[{}], 要删除的车辆数量:[{}]", oldSize, newKeys.size(), oldKeys.size());
						String[] fields = oldKeys.toArray(new String[oldKeys.size()]);
						if(fields != null && fields.length > 0){
							jedis.hdel("HD_DRIVER_LIST", fields);
						}
					}
					clearFlag = false;
				}
				jedis.hmset("HD_DRIVER_LIST", map);
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("--DriverListTask--驾驶员从业信息同步任务结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis处理耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s1 - start, s2 - s1,end - s2, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--DriverListTask--驾驶员从业信息同步任务异常:" + e.getMessage(), e);
		} finally {
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("--DriverListTask--驾驶员从业信息同步任务关闭资源异常:" + e.getMessage(), e);
			}
			authUtc = start - 60000;
		}
	}
}
