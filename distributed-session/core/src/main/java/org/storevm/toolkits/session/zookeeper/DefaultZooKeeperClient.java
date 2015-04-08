/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper;

import org.apache.log4j.Logger;
import org.apache.zookeeper.KeeperException;
import org.apache.zookeeper.ZooKeeper;
import org.storevm.toolkits.session.pool.ZookeeperPoolManager;

/**
 * 默认ZK客户端处理实现
 * @author Administrator
 * @version $Id: ZooKeeperClientImpl.java, v 0.1 2012-4-8 下午6:41:19 Administrator Exp $
 */
public class DefaultZooKeeperClient implements ZooKeeperClient {
    /** 日志 */
    private static final Logger    LOGGER = Logger.getLogger(ZooKeeperClient.class);

    /** 单例对象 */
    private static ZooKeeperClient instance;

    /** ZK对象池 */
    private ZookeeperPoolManager   pool;

    /**
     * 构造方法
     */
    protected DefaultZooKeeperClient() {
        if (pool == null) {
            pool = ZookeeperPoolManager.getInstance();
        }
    }

    /**
     * 返回单例方法
     * 
     * @return
     */
    public static ZooKeeperClient getInstance() {
        if (instance == null) {
            instance = new DefaultZooKeeperClient();
        }
        return instance;
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.ZooKeeperClient#execute(org.storevm.toolkits.session.zookeeper.ZookeeperHandler)
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T execute(ZookeeperHandler handler) throws Exception {
        //从池中获取ZK对象 
        ZooKeeper zk = pool.borrowObject();
        if (zk != null) {
            try {
                handler.setZooKeeper(zk);
                return (T) handler.handle();
            } catch (KeeperException ex) {
                LOGGER.error("执行ZK节点操作时发生异常: ", ex);
            } catch (InterruptedException ex) {
                LOGGER.error("执行ZK节点操作时发生异常: ", ex);
            } finally {
                //将ZK对象返回对象池中
                pool.returnObject(zk);
            }
        }
        return (T) null;
    }

}
