package com.ctfo.syncservice.task;

import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
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

import com.ctfo.syncservice.util.TaskAdapter;

/**
 * 文件名：DispatchInfoTask.java
 * 功能：并网转发信息同步缓存任务
 *
 * @author huangjincheng
 * 2014-9-12上午10:17:15
 * 
 */
public class DispatchInfoTask extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(DispatchInfoTask.class);
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
			setName("DispatchInfoTask");
			long interval = Long.parseLong(conf.get("clearInterval"));
			if(interval != 0){
				clearInterval = interval * 60 * 1000;
			}
			execute();
		} catch (Exception e) {
			logger.error("初始化并网转发信息同步缓存任务异常:" + e.getMessage(), e); 
		}
	}

	@Override
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
			Map<String, Map<String, String>> map =  new HashMap<String,Map<String,String>>();			
			conn = this.oracle.getConnection();
			//-----------------------通道转发列表
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoAll"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoIncremental"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			long s1 = System.currentTimeMillis();
			while (rs.next()) {
				try {
					String key = "FORWARD.LIST."+notNull(rs.getString("platform_no"));
					String commaddr = notNull(rs.getString("commaddr"));
					String status = notNull(rs.getString("status"));
					if(map.containsKey(key)){
						map.get(key).put(commaddr, status);
					}else {
						Map<String,String> mapCh = new HashMap<String, String>();
						mapCh.put(commaddr, status);
						map.put(key, mapCh);
					}
					index++;
				}  catch (Exception e) {
					logger.error("--DispatchInfoTask--并网转发信息同步任务 - 读取oracle异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			System.err.println(map.size());
			//-----------------------808/809接入参数
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoAll_platform"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoIncremental_platform"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			while (rs.next()) {
				try {
					String key = "FORWARD.PLAT."+notNull(rs.getString("protocol_id"));
					String platform = notNull(rs.getString("platform_no"));
					String value = "";
					if("FORWARD.PLAT.JT808".equals(key)){
						value = notNull(rs.getString("ip_address"))+","+notNull(rs.getString("port"))+","+notNull(rs.getString("reg_auth"))+","+notNull(rs.getString("status"));
					}else if("FORWARD.PLAT.JT809".equals(key)){
						value = notNull(rs.getString("ip_address"))+","+notNull(rs.getString("port"))+","+notNull(rs.getString("user_name"))+","+notNull(rs.getString("password"))+","+notNull(rs.getString("access_code"))+",,"+notNull(rs.getString("status"));
					}else continue;									
					if(map.containsKey(key)){
						map.get(key).put(platform, value);
					}else {
						Map<String,String> mapCh = new HashMap<String, String>();
						mapCh.put(platform, value);
						map.put(key, mapCh);
					}
					index++;
				}  catch (Exception e) {
					logger.error("--DispatchInfoTask--并网转发信息同步任务 - 读取oracle异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			
			//-----------------------终端基础数据
			if(clearFlag){
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoAll_tmc"));
			} else {
				ps = conn.prepareStatement(conf.get("sql_dispatchInfoIncremental_tmc"));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
			}
			rs = ps.executeQuery();
			while (rs.next()) {
				try {
					String key = "FORWARD.INFO."+notNull(rs.getString("platform_no"));
					String commaddr = notNull(rs.getString("commaddr"));
					String value = notNull(rs.getString("commaddr"))+","+notNull(rs.getString("plate_color"))+","+getGbk(notNull(rs.getString("vehicle_no")))+","+notNull(rs.getString("oem_code"))+","+notNull(rs.getString("tmodel_code"))+","+notNull(rs.getString("tmac"));
					if(map.containsKey(key)){
						map.get(key).put(commaddr, value);
					}else {
						Map<String,String> mapCh = new HashMap<String, String>();
						mapCh.put(commaddr, value);
						map.put(key, mapCh);
					}
					index++;
				}  catch (Exception e) {
					logger.error("--DispatchInfoTask--并网转发信息同步任务 - 读取oracle异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			long s2 = System.currentTimeMillis();
			
			
			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			int mapSize = map.size();
			if(mapSize > 0){
				if(clearFlag){
					Set<String> oldKeys = jedis.keys("FORWARD.LIST.*");
					Iterator<String> it=oldKeys.iterator();  
					while(it.hasNext()){
						jedis.del(it.next());
					}
					clearFlag = false;
				}
				for(String key: map.keySet()){
					jedis.hmset(key, map.get(key));
				}		
				map.clear();
			}
			long end = System.currentTimeMillis();
			logger.info("--DispatchInfoTask--并网转发信息同步任务结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis处理耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s1 - start, s2 - s1,end - s2, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--DispatchInfoTask--并网转发信息同步任务异常:" + e.getMessage(), e);
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
				logger.error("--DispatchInfoTask--并网转发信息同步任务关闭资源异常:" + e.getMessage(), e);
			}
			authUtc = start - 60000;
		}
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
	
	public static String getGbk(String str) throws UnsupportedEncodingException{
		if(!"".equals(str)&& null!= str){
			Charset cs = Charset.forName("GBK");
			CharBuffer cb = CharBuffer.wrap(str);
			ByteBuffer bbf = cs.encode(cb);
			byte[] bs= 	new byte[bbf.limit()];
			System.arraycopy(bbf.array(), 0, bs, 0, bbf.limit());			
			return new String(bs);
		}
		return "";
	}
 
}
