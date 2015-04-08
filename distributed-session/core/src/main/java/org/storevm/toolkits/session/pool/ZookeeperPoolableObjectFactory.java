/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.pool;

import org.apache.commons.lang3.math.NumberUtils;
import org.apache.commons.pool.PoolableObjectFactory;
import org.apache.log4j.Logger;
import org.apache.zookeeper.ZooKeeper;
import org.apache.zookeeper.ZooKeeper.States;
import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.helper.ConnectionWatcher;

/**
 * Zookeeper实例对象池，由于一个Zookeeper实例持有一个Socket连接，所以将Zookeeper实例池化避免实例化过程中的消耗
 * @author  ocean
 * @version $Id: ZookeeperPoolableObjectFactory.java, v 0.1 2012-4-1 下午03:52:05  ocean Exp $
 */
public class ZookeeperPoolableObjectFactory implements PoolableObjectFactory<ZooKeeper> {
    private static final Logger LOGGER = Logger.getLogger(ZookeeperPoolableObjectFactory.class);

    /** 配置信息对象 */
    private Configuration       config;

    /**
     * 构造方法
     * @param config
     */
    public ZookeeperPoolableObjectFactory(Configuration config) {
        this.config = config;
    }

    @Override
    public ZooKeeper makeObject() throws Exception {
        //返回一个新的zk实例
        ConnectionWatcher cw = new ConnectionWatcher();

        //连接服务端
        String servers = config.getString(Configuration.SERVERS);
        int timeout = NumberUtils.toInt(config.getString(Configuration.TIMEOUT));
        ZooKeeper zk = cw.connection(servers, timeout);
        if (zk != null) {
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("实例化ZK客户端对象，zk.sessionId=" + zk.getSessionId());
            }
        } else {
            LOGGER.warn("实例化ZK客户端对象失败");
        }
        return zk;
    }

    @Override
    public void destroyObject(ZooKeeper obj) throws Exception {
        if (obj != null) {
            obj.close();
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("ZK客户端对象被关闭，zk.sessionId=" + obj.getSessionId());
            }
        }
    }

    @Override
    public boolean validateObject(ZooKeeper obj) {
        if (obj != null && obj.getState() == States.CONNECTED) {
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("ZK客户端对象验证通过，zk.sessionId=" + obj.getSessionId());
            }
            return true;
        }
        if (LOGGER.isInfoEnabled()) {
            LOGGER.info("ZK客户端对象验证不通过，zk.sessionId=" + obj.getSessionId());
        }
        return false;
    }

    @Override
    public void activateObject(ZooKeeper obj) throws Exception {
    }

    @Override
    public void passivateObject(ZooKeeper obj) throws Exception {
    }

}
