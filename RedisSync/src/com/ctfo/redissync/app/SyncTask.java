package com.ctfo.redissync.app;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

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
 * 定时任务对象，1，查询所有企业，2，通过企业Id，更新Redis中车辆列表
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class SyncTask implements Runnable {
	private static final Logger logger = LoggerFactory.getLogger(SyncTask.class);

	private static final String SQL_CORN_IDS = "select ent_id from TB_ORGANIZATION WHERE ENABLE_FLAG = 1 and ENT_STATE != '0' ";
	private static final String SQL_CARD_IDS = "select (t.oem_code || '_' || s.commaddr) id from tb_vehicle v, tb_sim s, tr_serviceunit tr, tb_terminal t where v.ent_id in (select ent_id from TB_ORGANIZATION ton WHERE ton.ENABLE_FLAG = 1 and ton.ENT_STATE != '0' start with ton.ENT_ID = ? connect by prior ton.ENT_ID = ton.parent_id) and s.sid = tr.sid and t.tid = tr.tid and tr.vid = v.vid";
	
	private static final String HEAD_KEY="pushserver.";
	
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

			for (long ls : getCornIds()) {
				 rushCache(ls);
			}
		} catch (Exception e) {
			e.printStackTrace();
			logger.info("update Redis Cache failures", e);
		} 
		
		if(logger.isInfoEnabled())
			logger.info("use Time: {}ms", (System.currentTimeMillis()-t));
	}

	/**
	 * 获得企业ID列表
	 * @return
	 */
	private List<Long> getCornIds() {
		PreparedStatement ps = null;
		ResultSet rs = null;
		Connection conn = null;
		List<Long> ids = new ArrayList<Long>();
		try {
			conn = source.getConnection();
			ps = conn.prepareStatement(SQL_CORN_IDS);
			rs = ps.executeQuery();
			while (rs.next()) {
				ids.add(rs.getLong(1));
			}
		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			colseRSC(ps,rs,conn);
		}
		return ids;
	}

	private void rushCache(long cornId) {
		PreparedStatement ps = null;
		ResultSet rs = null;
		Connection conn = null;
		try {
			conn = source.getConnection();
			ps = conn.prepareStatement(SQL_CARD_IDS);
			ps.setLong(1, cornId);
			rs = ps.executeQuery();
			save(cornId, rs);
			
		//		getTest(cornId);  // 测试代码
		} catch (SQLException e) {
			e.printStackTrace();
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
	private final String getKey(final long cornId) {
		return HEAD_KEY+cornId;
	}

	/**
	 * 存储到Redis
	 */
	private void save(final long cornId, ResultSet rs) throws SQLException {
		ShardedJedis shard = ShardedJedisPoolFactory.getPool().getResource();
		try {
			shard.del(getKey(cornId)); // 先删除存在的Key，再进行更新
			while (rs.next()) {
				shard.lpush(getKey(cornId), rs.getString(1));
				if(logger.isDebugEnabled()) {
					logger.debug("cornid:{} == {}", cornId, rs.getString(1));
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			ShardedJedisPoolFactory.getPool().returnResource(shard);
		}
	}
	
	// 测试代码
	@SuppressWarnings("unused")
	private void getTest(final long cornId) {
		ShardedJedis shard = ShardedJedisPoolFactory.getPool().getResource();
				if(logger.isInfoEnabled()) {
					for(String s : shard.lrange(getKey(cornId), 0, -1))
						logger.info("cornid:{} == {}", cornId, s);
				}
			ShardedJedisPoolFactory.getPool().returnResource(shard);
	}
}
