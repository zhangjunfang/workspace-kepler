package com.ctfo.sectionspeed.client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

import org.apache.thrift.protocol.TBinaryProtocol;
import org.apache.thrift.transport.TSocket;
import org.apache.thrift.transport.TTransport;
import org.apache.thrift.transport.TTransportException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.sectionspeed.thrift.SectionSpeed;
import com.ctfo.sectionspeed.util.ResourcesUtil;

public enum ClientManager {
	INSTANCE;
	
	private static final Logger logger = LoggerFactory.getLogger(ClientManager.class);
	private static ConsistentHash<TTransport> hashTransport= null;
	
	public SectionSpeed.Client getClient() throws TTransportException {
		if(hashTransport == null) {
			initManager();
		}
		TTransport ht = hashTransport.getPrimary(String.valueOf(Math.random()));
		if(!ht.isOpen()) {
			ht.open();
			if(logger.isInfoEnabled()) {
				logger.info("建立连接: " + ht.isOpen());
			}
		}
		
		return new SectionSpeed.Client(new TBinaryProtocol(ht));
	}
	
	private final static void initManager() {
		long l = System.currentTimeMillis();
		hashTransport = new ConsistentHash<TTransport>(buildClient4Config(), 160);
		logger.info("初始化Hash环，用时: " +(System.currentTimeMillis()-l) + " ms");
	}
	
	private static List<TTransport> buildClient4Config() {
		List<TTransport> shardList = new ArrayList<TTransport>();
		try {
			Properties p = ResourcesUtil.getResourceAsProperties("system.properties");
			String shards = p.getProperty("SV_servers");
			int timeout = Integer.valueOf(p.getProperty("SV_connTimeout", "5000").trim());
			if(null != shards && !shards.isEmpty()) {
				String[] s = shards.split(",");
				for(String server : s) {
					String[] ss = server.split(":");
					shardList.add(new TSocket(ss[0].trim(), Integer.valueOf(ss[1].trim()), timeout));
				}
			}
			if(logger.isInfoEnabled()) {
				logger.info(shards);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		return shardList;
	}

}
