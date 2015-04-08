package com.ctfo.syn.kcpt_oracle2mysql;

import java.io.IOException;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.syn.kcpt.utils.SynPool;

import net.rubyeye.xmemcached.MemcachedClient;
import net.rubyeye.xmemcached.MemcachedClientBuilder;
import net.rubyeye.xmemcached.XMemcachedClientBuilder;
import net.rubyeye.xmemcached.command.KestrelCommandFactory;
import net.rubyeye.xmemcached.impl.KetamaMemcachedSessionLocator;
import net.rubyeye.xmemcached.utils.AddrUtil;

public class ConnectMemcachePool {
	private static ConcurrentHashMap<String,MemcachedClient> sqlMap = new ConcurrentHashMap<String,MemcachedClient>();
	private Logger logger = Logger.getLogger(ConnectMemcachePool.class);
	
	public ConnectMemcachePool(){
		
	}
	
	public void initMemcache(){
//		createMemcacheConnectionPool(SynPool.getinstance().getSql("memcacheaddr"));
//		createMemcacheConnectionPool(SynPool.getinstance().getSql("memcacheMainAddr"));
		createMemcacheConnectionPool(SynPool.getinstance().getSql("httpMemAddr"));
	}
	
	private void createMemcacheConnectionPool(String memcacheAddr){
		// memcache地址		
		MemcachedClientBuilder builder = null;
		MemcachedClient mcc = null;
		builder = new XMemcachedClientBuilder(AddrUtil.getAddresses(memcacheAddr));
		try {
			mcc = builder.build();
			mcc.setConnectTimeout(10 * 1000);
			mcc.setOpTimeout(10 * 1000);		
			
		} catch (IOException e) {
			logger.error("Connect to " + memcacheAddr + "fail.",e );
			System.exit(0);
		}
		sqlMap.put(memcacheAddr, mcc);
	}

	public static MemcachedClient getSqlMap(String memcacheAddr) {
		return sqlMap.get(memcacheAddr);
	}
}
