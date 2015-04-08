/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：湖北驾驶员信息同步任务	(平台驾驶员纬度相关)
 * 
 *****************************************/
public class HubeiDriverTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(HubeiDriverTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
//	/**	最后处理天	*/
//	private static String lastDay = "";
//	/**	清理时间	*/
//	private static String clearTime = "01";
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
			setName("HubeiDriverTask");
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
////			判断当天
//			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
//			String currentStr = currentDate.substring(0, 8);
//			if(!currentStr.equals(lastDay)){ // 隔天
//				String hours = currentDate.substring(8);
//				if(hours.equals(clearTime)){ 
//					clearFlag = true;
//				}
//			}
			
			conn = this.oracle.getConnection();
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_hubeiDriver"));
			} else {
				String sql = conf.get("sql_hubeiDriverIncremental");
				ps = conn.prepareStatement(sql);
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
//				SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
//				logger.debug("当前时间:{}, SQL时间:{}, SQL:[{}], TIME:[{}]", sdf.format(new Date()), sdf.format(new Date(lastUtc)), sql, lastUtc);
			} 
			rs = ps.executeQuery();
			long s1 = System.currentTimeMillis();
//			hset  KCTX.HBINFO	手机号码	车牌颜色,车牌号码,车主姓名,联系电话,工作单位,运输行业编码
			Map<byte[],byte[]> map =  new HashMap<byte[],byte[]>();
			Set<String> newKeys = new HashSet<String>();
			Charset cs = Charset.forName("GBK");
			while (rs.next()) {
				try {
					String key = rs.getString("COMMADDR");
					String str = rs.getString("VAL");
					String transtype = rs.getString("TRANSTYPE_CODE");
					if(StringUtils.isNumeric(transtype)){
						int code = Integer.parseInt(transtype);
						str = str + code;
					} else { // 如果运输行业编码为空，默认为10（客运）
						str = str + "10";
					}
					CharBuffer cb = CharBuffer.wrap(str);
					ByteBuffer bbf = cs.encode(cb);
					byte[] bs= 	new byte[bbf.limit()];
					System.arraycopy(bbf.array(), 0, bs, 0, bbf.limit());
					map.put(key.getBytes(), bs); 
					newKeys.add(key);
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--HubeiDriverTask--同步湖北驾驶员信息 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--HubeiDriverTask--同步湖北驾驶员信息 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			if(map.size() > 0){
				if(clearFlag){
//					jedis.del("KCTX.HBINFO".getBytes());
//					lastDay = currentStr;
//					1. 获取历史缓存所有KEYS，存储到SET集合中
					Set<String> oldKeys = jedis.hkeys("KCTX.HBINFO");
					logger.debug("湖北驾驶员信息缓存历史记录数:{}", oldKeys.size()); 
					jedis.del("KCTX.HBINFO.OLD.TEMP");
					jedis.del("KCTX.HBINFO.NEW.TEMP");
					if(oldKeys != null && oldKeys.size() > 0){
						jedis.sadd("KCTX.HBINFO.OLD.TEMP", oldKeys.toArray(new String[oldKeys.size()])); 
					}
//					2. 获取当前缓存所有KEYS，对比出差集
//					Set<String> newKeys = map.keySet();
					logger.debug("湖北驾驶员信息缓存数据库当前记录数:{}", newKeys.size()); 
					if(newKeys != null && newKeys.size() > 0){
						jedis.sadd("KCTX.HBINFO.NEW.TEMP", newKeys.toArray(new String[newKeys.size()]));
					} 
					Set<String> diffKeys = jedis.sdiff("KCTX.HBINFO.OLD.TEMP","KCTX.HBINFO.NEW.TEMP");
					logger.debug("湖北驾驶员信息缓存可删除记录数:{}, 手机号列表:[{}]", diffKeys.size(), getKeys(diffKeys)); 
					if(diffKeys != null && diffKeys.size() > 0){
						String[] fields = diffKeys.toArray(new String[diffKeys.size()]);
						if(fields != null && fields.length > 0){
							jedis.hdel("KCTX.HBINFO", fields);
						}
					}
					clearFlag = false;
				}
				jedis.hmset("KCTX.HBINFO".getBytes(), map);
				jedis.del("KCTX.HBINFO.OLD.TEMP");
				jedis.del("KCTX.HBINFO.NEW.TEMP");
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("--HubeiDriverTask--同步湖北驾驶员信息结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s1 - start, end - s1, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--HubeiDriverTask--同步湖北驾驶员信息异常:" + e.getMessage(), e);
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
				logger.error("--HubeiDriverTask--同步湖北驾驶员信息关闭资源异常:" + e.getMessage(), e);
			}
		}
		authUtc = start - 60000;
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
}
