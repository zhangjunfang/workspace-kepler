/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper;

import org.apache.log4j.Logger;
import org.apache.zookeeper.ZooKeeper;

/**
 * 抽象实现
 * @author Administrator
 * @version $Id: AbstractZookeeperExecute.java, v 0.1 2012-4-8 下午6:16:01 Administrator Exp $
 */
public abstract class AbstractZookeeperHandler implements ZookeeperHandler {
    /** 日志 */
    protected static final Logger LOGGER = Logger.getLogger(ZookeeperHandler.class);

    /** ZK客户端 */
    protected ZooKeeper           zookeeper;

    /**
     * 节点ID
     */
    protected String              id;

    /**
     * 构造方法
     */
    public AbstractZookeeperHandler(String id) {
        this.id = id;
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.ZookeeperHandler#setZooKeeper(org.apache.zookeeper.ZooKeeper)
     */
    @Override
    public void setZooKeeper(ZooKeeper zookeeper) {
        this.zookeeper = zookeeper;
    }
}
