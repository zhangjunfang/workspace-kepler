/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.pool;

import java.util.NoSuchElementException;

import org.apache.commons.lang3.math.NumberUtils;
import org.apache.commons.pool.ObjectPool;
import org.apache.commons.pool.PoolableObjectFactory;
import org.apache.commons.pool.impl.StackObjectPool;
import org.apache.log4j.Logger;
import org.apache.zookeeper.ZooKeeper;
import org.storevm.toolkits.session.config.Configuration;

/**
 * ZK实例池管理器
 * @author  ocean
 * @version $Id: ZookeeperPoolManager.java, v 0.1 2012-4-1 下午05:17:07  ocean Exp $
 */
public class ZookeeperPoolManager {
    private static final Logger           LOGGER = Logger.getLogger(ZookeeperPoolManager.class);

    /** 单例 */
    protected static ZookeeperPoolManager instance;

    private ObjectPool<ZooKeeper>         pool;

    /**
     * 构造方法
     */
    protected ZookeeperPoolManager() {
    }

    /**
     * 返回单例的对象
     * 
     * @return
     */
    public static ZookeeperPoolManager getInstance() {
        if (instance == null) {
            instance = new ZookeeperPoolManager();
        }
        return instance;
    }

    /**
     * 初始化方法
     * 
     * @param config
     */
    public void init(Configuration config) {
        PoolableObjectFactory<ZooKeeper> factory = new ZookeeperPoolableObjectFactory(config);

        //初始化ZK对象池
        int maxIdle = NumberUtils.toInt(config.getString(Configuration.MAX_IDLE));
        int initIdleCapacity = NumberUtils
            .toInt(config.getString(Configuration.INIT_IDLE_CAPACITY));
        pool = new StackObjectPool<ZooKeeper>(factory, maxIdle, initIdleCapacity);
        //初始化池
        for (int i = 0; i < initIdleCapacity; i++) {
            try {
                pool.addObject();
            } catch (IllegalStateException ex) {
                LOGGER.error("初始化池发生异常。", ex);
            } catch (UnsupportedOperationException ex) {
                LOGGER.error("初始化池发生异常。", ex);
            } catch (Exception ex) {
                LOGGER.error("初始化池发生异常。", ex);
            }
        }
    }

    /**
     * 将ZK对象从对象池中取出
     * 
     * @return
     */
    public ZooKeeper borrowObject() {
        if (pool != null) {
            try {
                ZooKeeper zk = pool.borrowObject();
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("从ZK对象池中返回实例，zk.sessionId=" + zk.getSessionId());
                }
                return zk;
            } catch (NoSuchElementException ex) {
                LOGGER.error("出借ZK池化实例时发生异常：", ex);
            } catch (IllegalStateException ex) {
                LOGGER.error("出借ZK池化实例时发生异常：", ex);
            } catch (Exception e) {
                LOGGER.error("出借ZK池化实例时发生异常：", e);
            }
        }
        return null;
    }

    /**
     * 将ZK实例返回对象池
     * 
     * @param zk
     */
    public void returnObject(ZooKeeper zk) {
        if (pool != null && zk != null) {
            try {
                pool.returnObject(zk);
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("将ZK实例返回对象池中，zk.sessionId=" + zk.getSessionId());
                }
            } catch (Exception ex) {
                LOGGER.error("返回ZK池化实例时发生异常：", ex);
            }
        }
    }

    /**
     * 关闭对象池
     */
    public void close() {
        if (pool != null) {
            try {
                pool.close();
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("关闭ZK对象池完成");
                }
            } catch (Exception ex) {
                LOGGER.error("关闭ZK对象池时发生异常：", ex);
            }
        }
    }
}
