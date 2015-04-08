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

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：深圳监管平台同步任务
 * 
 *****************************************/
public class ShenzhenInfoTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(ShenzhenInfoTask.class);
	/**	清理间隔(单位:分钟 ; 默认1小时)	*/
	private static long clearInterval = 60 * 60 * 1000;
	/**	最近处理时间		*/
	private static long lastTime = 0;
	/**	清理标记	*/
	private static boolean clearFlag = false;
	/** 最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();

	
	@Override
	public void init() {
		try {
			setName("ShenzhenInfoTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			//execute();
		} catch (Exception e) {
			logger.error("初始化深圳监管静态信息从业缓存同步任务异常:" + e.getMessage(), e); 
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
			conn = this.oracle.getConnection();
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_shenzhenInfo"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_shenzhenInfoIncremental"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			Map<byte[],byte[]> map =  new HashMap<byte[],byte[]>();
			Set<String> newKeys = new HashSet<String>();
			Charset cs = Charset.forName("GBK");
			while (rs.next()) {
				try {
					String key = notNull(rs.getString("COMMADDR"));
					String str = notNull(rs.getString("VAL"));

					CharBuffer cb = CharBuffer.wrap(str);
					ByteBuffer bbf = cs.encode(cb);
					byte[] bs= 	new byte[bbf.limit()];
					System.arraycopy(bbf.array(), 0, bs, 0, bbf.limit());
					map.put(key.getBytes(), bs);
					newKeys.add(key);
					index++;
				}catch (Exception e) {
					logger.error("--ShenzhenInfoTask--同步深圳平台信息 - 连接Oracle异常:" + e.getMessage(), e);
					map.clear();
					error++;
					continue;
				}
			}
			long s2 = System.currentTimeMillis();
			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			if(map.size() > 0){
				if(clearFlag){
//					jedis.del("KCTX.HBINFO".getBytes());
//					lastDay = currentStr;
//					1. 获取历史缓存所有KEYS，存储到SET集合中
					Set<String> oldKeys = jedis.hkeys("KCTX.SZINFO");
					logger.debug("深圳平台信息缓存历史记录数:{}", oldKeys.size()); 
					jedis.del("KCTX.SZINFO.OLD.TEMP");
					jedis.del("KCTX.SZINFO.NEW.TEMP");
					if(oldKeys != null && oldKeys.size() > 0){
						jedis.sadd("KCTX.SZINFO.OLD.TEMP", oldKeys.toArray(new String[oldKeys.size()])); 
					}
//					2. 获取当前缓存所有KEYS，对比出差集
//					Set<String> newKeys = map.keySet();
					logger.debug("深圳平台信息缓存数据库当前记录数:{}", newKeys.size()); 
					if(newKeys != null && newKeys.size() > 0){
						jedis.sadd("KCTX.SZINFO.NEW.TEMP", newKeys.toArray(new String[newKeys.size()]));
					} 
					Set<String> diffKeys = jedis.sdiff("KCTX.SZINFO.OLD.TEMP","KCTX.SZINFO.NEW.TEMP");
					logger.debug("深圳平台信息缓存可删除记录数:{}, 手机号列表:[{}]", diffKeys.size(), getKeys(diffKeys)); 
					if(diffKeys != null && diffKeys.size() > 0){
						String[] fields = diffKeys.toArray(new String[diffKeys.size()]);
						if(fields != null && fields.length > 0){
							jedis.hdel("KCTX.SZINFO", fields);
						}
					}
					clearFlag = false;
				}
				jedis.hmset("KCTX.SZINFO".getBytes(), map);
				jedis.del("KCTX.SZINFO.OLD.TEMP");
				jedis.del("KCTX.SZINFO.NEW.TEMP");
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("--ShenzhenInfoTask--同步深圳平台信息结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s2 - start, end - s2, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--ShenzhenInfoTask--同步深圳平台信息异常:" + e.getMessage(), e);
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
				logger.error("--ShenzhenInfoTask--同步深圳平台信息关闭资源异常:" + e.getMessage(), e);
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

