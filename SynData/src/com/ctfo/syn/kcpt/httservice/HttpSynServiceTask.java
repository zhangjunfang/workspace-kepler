package com.ctfo.syn.kcpt.httservice;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeoutException;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

/**
 * <p>
 * Title: SynData
 * </p>
 * <p>
 * Description: SynData
 * </p>
 * <p>
 * Copyright: Copyright (c) 2004
 * </p>
 * <p>
 * Company:北京中交兴路信息科技有限公司
 * </p>
 * 
 * @author wuqiangjun
 * @version 1.1 2012-4-5
 */
public class HttpSynServiceTask {

	private final static Log log = LogFactory.getLog(HttpSynServiceTask.class);

	public static long memLastUpdateTime = 0;// 同步数据到Memcached中最后一次的UTC时间

	private Config config;

	/**
	 * @return the config
	 */
	public Config getConfig() {
		return config;
	}

	/**
	 * @param config
	 *            the config to set
	 */
	public void setConfig(Config config) {
		this.config = config;
	}

	public HttpSynServiceTask(Config config) {
		this.config = config;
	}

	/**
	 * 同步数据到Memcached中
	 * 
	 * @return
	 */
	public int getSynData() {
		long updateTime = HttpSynServiceTask.memLastUpdateTime;

		String sql = "SELECT T.AUTH_CODE, T.OEM_CODE,SS.COMMADDR FROM TB_TERMINAL T, TB_SIM SS, TR_SERVICEUNIT S WHERE T.TID = S.TID AND S.SID = SS.SID  AND SS.ENABLE_FLAG != 0 AND T.REG_STATUS != -1 and t.reg_status=0 ";

		if (updateTime > 0)// 第一次加载的时候，加载全部, 以后的话要对比上次更新时间
		{
			sql = sql + " AND (T.Update_Time > " + updateTime
					+ " or SS.Update_Time > " + updateTime
					+ "  or S.Update_Time > " + updateTime + " )";
		}

		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		List<VehicleInfo> list = new ArrayList<VehicleInfo>();
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql);
			rs = ps.executeQuery();
			
			while (rs.next()) {
				VehicleInfo info = new VehicleInfo();
				info.setAkey(rs.getObject("AUTH_CODE") == null ? "" : String
						.valueOf(rs.getObject("AUTH_CODE")));
				info.setOemcode(rs.getObject("OEM_CODE") == null ? "" : String
						.valueOf(rs.getObject("OEM_CODE")));
				info.setCommaddr(rs.getObject("COMMADDR") == null ? "" : String
						.valueOf(rs.getObject("COMMADDR")));
				log.info("auth_code:" + info.getCommaddr());
				list.add(info);
			} // End while
			
			addMemCached(list);
			HttpSynServiceTask.memLastUpdateTime = System.currentTimeMillis();// 更新缓存最后一次的修改时间
			int count = list.size();
			return count;
		} catch (SQLException e) {
			log.error("鉴权同步到cached中出错.",e);
		} finally {
			if(rs != null){
				try {
					rs.close();
				} catch (SQLException e) {
					log.error(e);
				}
			}
			
			if(ps != null){
				try {
					ps.close();
				} catch (SQLException e) {
					log.equals(e);
				}
			}
			
			if( conn != null){
				try {
					conn.close();
				} catch (SQLException e) {
					log.error(e);
				}
			}
			
			// 清空list
			if(list != null && list.size() > 0){
				list.clear();
				list = null;
			}
		}
		return 0;
	}

	/**
	 * 将数据放入Mem中
	 * 
	 * @param info
	 *            数据对象
	 */
	private void addMemCached(List<VehicleInfo> info) {
//		MemcachedClient client = null;
		JedisUnifiedStorageService jedis = null;
		try {
			jedis = RedisServer.getJedisServiceInstance();
//			client = MemManager.getMemcachedClient(config);
//			client.setOpTimeout(100000);
			for (VehicleInfo v : info) {
				log.debug("同步数据 " + v.getCommaddr() + " ");
				// client.delete("PCC_" + v.getCommaddr());
//				client.set("PCC_" + v.getCommaddr(), 0, v);
				jedis.saveVehicleAuthentication("PCC_" + v.getCommaddr(), RedisJsonUtil.objectToJson(v));
			}// End for
			log.info("此次共同步数据[" + info.size() + "]条.");
		} catch (TimeoutException e) {
			log.error("鉴权同步到cached中出错.",e);
		} catch (InterruptedException e) {
			log.error("鉴权同步到cached中出错.",e);
		} catch (Exception e) {
			log.error("鉴权同步到cached中出错.",e);
		} 
//		finally {
//			try {
//				client.shutdown();
//			} catch (IOException e) {
//				log.error("鉴权同步到cached中出错.",e);
//			}
//		}

	}
}
