package com.kypt.c2pp.back;

import java.util.LinkedList;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.inside.msg.InsideMsgResp;

/**
 * 底层为每个服务器保存一个消息队列，队列key为服务器IP
 * @author yujch
 *
 */
public class SupCommandQueueMap extends ConcurrentHashMap<String,BlockingQueue> {
	
	private static final Logger log = LoggerFactory.getLogger(SupCommandQueueMap.class);

    private static final SupCommandQueueMap supUpCommandQueueMap = new SupCommandQueueMap();
    
    private LinkedList<BlockingQueue<InsideMsgResp>> queuelist = new LinkedList<BlockingQueue<InsideMsgResp>>();

    public static SupCommandQueueMap getInstance() {
        return supUpCommandQueueMap;
    }
    
	@Override
	public BlockingQueue<InsideMsgResp> get(Object key) {
		// TODO Auto-generated method stub
		return super.get(key);
	}

	@Override
	public boolean isEmpty() {
		// TODO Auto-generated method stub
		return super.isEmpty();
	}

	@Override
	public BlockingQueue<InsideMsgResp> put(String key, BlockingQueue value) {
		// TODO Auto-generated method stub
		return super.put(key, value);
	}

	@Override
	public BlockingQueue<InsideMsgResp> remove(Object key) {
		// TODO Auto-generated method stub
		return super.remove(key);
	}
    
}
