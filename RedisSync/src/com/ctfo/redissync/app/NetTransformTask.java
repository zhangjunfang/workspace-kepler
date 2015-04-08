package com.ctfo.redissync.app;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import javax.sql.DataSource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.ShardedJedis;

import com.ctfo.redissync.db.DBCPPoolManager;
import com.ctfo.redissync.util.ConstantUtil;
import com.ctfo.redissync.util.ResourcesUtil;
import com.ctfo.redissync.util.ShardedJedisPoolFactory;
import com.ctfo.redissync.vo.OracleDbVO;

/**
 * 转网数据同步
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class NetTransformTask implements Runnable {
	private static final Logger logger = LoggerFactory.getLogger(NetTransformTask.class);

	private static final String SQL_CAR_INFO= "select (v.plate_color || '_' || v.vehicle_no ) a ,(t.OEM_CODE || '_' || s.commaddr) b from tr_serviceunit u,tb_vehicle v,tb_terminal t,tb_sim s where u.vid=v.vid and u.tid=t.tid and u.sid=s.sid";
	
	private static final String HEAD_KEY="mppas.%s";
	
	private static DataSource source = null;

	@Override
	public void run() {
		long t = System.currentTimeMillis();
		
		try {
			if (null == source) {
				OracleDbVO ovo = new OracleDbVO();
				ovo.makeSelf(ResourcesUtil.getResourceAsProperties(ConstantUtil.SYS_CONF_FILE));
				source = DBCPPoolManager.getOracleDbPool(ovo.getIp(), ovo.getPort(), ovo.getUid(), ovo.getUser(), ovo.getPwd());
				if (logger.isInfoEnabled()) {
					logger.info(ovo.toString());
				}
			}

			if(rushCache()) {
				if(logger.isInfoEnabled())
					logger.info("update Redis Cache successful, use Time: {}ms", (System.currentTimeMillis()-t));
			} else {
				if(logger.isInfoEnabled())
					logger.info("update Redis Cache failures");
			}
			
		} catch (Exception e) {
			e.printStackTrace();
			logger.info("update Redis Cache exception", e);
		} 
		
	}

	private boolean rushCache() {
		PreparedStatement ps = null;
		ResultSet rs = null;
		Connection conn = null;
		try {
			conn = source.getConnection();
			ps = conn.prepareStatement(SQL_CAR_INFO);
			rs = ps.executeQuery();
			return save(rs);
		//	getTest(rs);
		} catch (SQLException e) {
			e.printStackTrace();
			return false;
		} finally {
			colseRSC(ps,rs,conn);
		}
		
	}
	
	/**
	 * 关闭数据库资源
	 * @param ps
	 * @param rs
	 * @param conn
	 */
	private void colseRSC(final PreparedStatement ps,  final ResultSet rs,  final Connection conn) {
		try {
			if (rs != null) rs.close();
			if (ps != null) ps.close();
			if (conn != null) conn.close();
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("rsc close ", e);
		}
	}
	
	// 构建RedisKey
	private final String getKey(final String key) {
		return String.format(HEAD_KEY, key);
	}

	/**
	 * 存储到Redis
	 */
	private boolean save(ResultSet rs) throws SQLException {
		ShardedJedis shard = ShardedJedisPoolFactory.getPool().getResource();
		try {
			int i=0;
			while(rs.next()) {
				String key = rs.getString("a");
				String value = rs.getString("b");
				if (logger.isDebugEnabled()) {
					logger.debug(String.format("save: %s == %s", key, value));
				}
				if(!key.isEmpty()) {
					shard.set(getKey(key), value);
					i++;
				}
			}
			if(logger.isInfoEnabled())
				logger.info("update NUM=" + i);
		} catch (Exception e) {
			e.printStackTrace();
			return false;
		} finally {
			ShardedJedisPoolFactory.getPool().returnResource(shard);
		}
		return true;
	}
	
	// 测试代码
	@SuppressWarnings("unused")
	private void getTest(ResultSet rs) {
		ShardedJedis shard = ShardedJedisPoolFactory.getPool().getResource();
		try {
			int i=0;
			while(rs.next()) {
				String key = rs.getString("a");
				String value = rs.getString("b");
				if (logger.isDebugEnabled()) {
					logger.debug(String.format("test: %s = %s = %s", key, value, shard.get(getKey(key))));
				}
				i++;
			}
			if(logger.isInfoEnabled())
				logger.info("update NUM=" + i);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			ShardedJedisPoolFactory.getPool().returnResource(shard);
		}
	}
}
