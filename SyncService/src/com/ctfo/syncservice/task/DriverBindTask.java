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
 * <li>描 述：驾驶员绑定缓存同步任务	(平台驾驶员纬度相关)
 * 
 *****************************************/
public class DriverBindTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(DriverBindTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/**	清理标记	*/
	private static boolean clearFlag = false;
	/** 最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();
	
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		try {
			setName("DriverBindTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("初始化驾驶员绑定缓存同步任务异常:" + e.getMessage(), e); 
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
//			判断当天
//			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
//			String currentStr = currentDate.substring(0, 8);
//			if(!currentStr.equals(lastDay)){ // 隔天
//				String hours = currentDate.substring(8);
//				if(hours.equals(clearTime)){ 
//					clearFlag = true;
//				}
//			}
			Map<String, String> map =  new HashMap<String, String>();
			conn = this.oracle.getConnection();
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_driverInfo"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_driverInfoIncremental"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			long s1 = System.currentTimeMillis();
			StringBuffer sb = new StringBuffer(128);
			while (rs.next()) {
				try {
					sb.setLength(0); 
					String vid = notNull(rs.getString("VID"));
					sb.append(vid).append(":");  			// 车辆编号
					sb.append(notNull(rs.getString("STAFF_ID"))).append(":");		// 驾驶员唯一编号
					sb.append(notNull(rs.getString("STAFF_NAME"))).append(":");		// 驾驶员姓名
					sb.append(notNull(rs.getString("SEX"))).append(":");			// 性别
					sb.append(notNull(rs.getString("MOBILEPHONE"))).append(":");	// 手机号码
					sb.append(notNull(rs.getString("CARD_ID"))).append(":");		// 身份证号
					sb.append(notNull(rs.getString("BUSSINESS_ID"))).append(":");	// 资格证号
					sb.append(notNull(rs.getString("DRIVERCARD_DEP"))).append(":");	// 发证机关
					sb.append(notNull(rs.getString("ADDRESS"))).append(":");		// 联系地址
					sb.append(notNull(rs.getString("ARCHIVENO"))).append(":");		// 档案编号
					sb.append("0");
					map.put(vid, sb.toString()); 	// 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--DriverBindTask--驾驶员绑定缓存同步任务 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--DriverBindTask--驾驶员绑定缓存同步任务 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			long s2 = System.currentTimeMillis();
			jedis = this.redis.getJedisConnection();
			jedis.select(4);
			if(map.size() > 0){
				if(clearFlag){  // 清理已经删除或者解绑的车辆信息（垃圾信息清理）
//					1. 获取历史缓存所有KEYS，存储到SET集合中
					Set<String> oldKeys = jedis.hkeys("HD_DRIVER_BIND");
					logger.debug("驾驶员绑定缓存历史记录数:{}", oldKeys.size()); 
					jedis.del("SD_OLD_DRIVER_BIND");
					jedis.del("SD_NEW_DRIVER_BIND");
					if(oldKeys != null && oldKeys.size() > 0){
						jedis.sadd("SD_OLD_DRIVER_BIND", oldKeys.toArray(new String[oldKeys.size()])); 
					}
//					2. 获取当前缓存所有KEYS，对比出差集
					Set<String> newKeys = map.keySet();
					logger.debug("驾驶员绑定缓存数据库当前记录数:{}", newKeys.size()); 
					if(newKeys != null && newKeys.size() > 0){
						jedis.sadd("SD_NEW_DRIVER_BIND", newKeys.toArray(new String[newKeys.size()]));
					} 
					Set<String> diffKeys = jedis.sdiff("SD_OLD_DRIVER_BIND","SD_NEW_DRIVER_BIND");
					logger.debug("驾驶员绑定缓存可删除记录数:{}, 手机号列表:[{}]", diffKeys.size(), getKeys(diffKeys)); 
					if(diffKeys != null && diffKeys.size() > 0){
						String[] fields = diffKeys.toArray(new String[diffKeys.size()]);
						if(fields != null && fields.length > 0){
							jedis.hdel("HD_DRIVER_BIND", fields);
						}
					}
					clearFlag = false;
				}
				jedis.hmset("HD_DRIVER_BIND", map);
				jedis.del("SD_OLD_DRIVER_BIND");
				jedis.del("SD_NEW_DRIVER_BIND");
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("--DriverBindTask--驾驶员绑定缓存同步任务结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis处理耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s1 - start, s2 - s1,end - s2, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--DriverBindTask--驾驶员绑定缓存同步任务异常:" + e.getMessage(), e);
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
				logger.error("--DriverInfoTask--驾驶员绑定缓存同步任务关闭资源异常:" + e.getMessage(), e);
			}
		}
		authUtc = start - 60000;
	}
	/**
	 * 获取Keys字符串
	 * @param diffKeys
	 * @return
	 */
	private Object getKeys(Set<String> diffKeys) {
		StringBuffer sb = new StringBuffer();
		for(String key : diffKeys){
			sb.append(key).append(";");
		}
		return sb.toString();
	}

	/**
	 * 获得非null的字符串
	 * @param str
	 * @return
	 */
	public static String notNull(String str){
		if(str != null){
			return str;
		}
		return "";
	}
	
}
