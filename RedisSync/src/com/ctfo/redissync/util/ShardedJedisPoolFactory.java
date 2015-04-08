package com.ctfo.redissync.util;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.JedisPoolConfig;
import redis.clients.jedis.JedisShardInfo;
import redis.clients.jedis.ShardedJedisPool;

import com.ctfo.redissync.vo.JedisConfigVo;

/**
 * Redis客户端构建工厂
 * @author jiangzhongming
 * @version 1.1 2012-6-26
 */
public class ShardedJedisPoolFactory {

	private ShardedJedisPoolFactory() {

	}

	private static final Logger logger = LoggerFactory.getLogger(ShardedJedisPoolFactory.class);
	private static ShardedJedisPool pool = null;
	
	public static ShardedJedisPool getPool() {
		return (null == pool) ? buildPool() : pool;
	}

	private static final ShardedJedisPool buildPool() {
		JedisConfigVo jcv = new JedisConfigVo();
		Properties p = null;
		try {
			p = ResourcesUtil.getResourceAsProperties(ConstantUtil.SYS_CONF_FILE);
			jcv.makeSelf(p);
		} catch (IOException e) {
			e.printStackTrace();
			logger.error("Redis Pool build error", e);
			return null;
		}
		JedisPoolConfig poolConfig = new JedisPoolConfig();
		poolConfig.setMaxActive(jcv.getMaxActive());
		poolConfig.setMaxIdle(jcv.getMaxIdel());
		poolConfig.setMaxWait(jcv.getMaxWait());
		poolConfig.setTestOnBorrow(jcv.isTestOnBorrow());
		
		if(logger.isInfoEnabled()) {
			logger.info("Redis Pool Configure: " + jcv);
		}
		pool = new ShardedJedisPool(poolConfig, getShardisInfos(p));

		return pool;
	}
	
	private static final List<JedisShardInfo> getShardisInfos(final Properties p) {
		String shards = p.getProperty("JD_servers");
		int timeout = Integer.valueOf(p.getProperty("JD_connTimeout", "5000").trim());
		List<JedisShardInfo> shardList = new ArrayList<JedisShardInfo>();
		if(null != shards && !shards.isEmpty()) {
			String[] s = shards.split(",");
			for(String server : s) {
				String[] ss = server.split(":");
				shardList.add(new JedisShardInfo(ss[0].trim(), Integer.valueOf(ss[1].trim()), timeout));
			}
			if(logger.isInfoEnabled()) {
				for(JedisShardInfo info : shardList) {
					logger.info("Redis Server List: " + info);
				}
			}
		}
		return shardList;
	}
	
	// test code
	public static void main(String[] args) throws IOException {
		for(JedisShardInfo j :  getShardisInfos(ResourcesUtil.getResourceAsProperties(ConstantUtil.SYS_CONF_FILE))) {
			logger.info(j.getHost());
			logger.info(String.valueOf(j.getTimeout()));
			logger.info(String.valueOf(j.getPort()));
			logger.info(j.toString());
		}
	}
}
