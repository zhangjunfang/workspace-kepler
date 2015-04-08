/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper;

import org.apache.zookeeper.ZooKeeper;

/**
 * ZK客户端操作接口
 * @author Administrator
 * @version $Id: ZookeeperOperate.java, v 0.1 2012-4-8 下午6:07:55 Administrator Exp $
 */
public interface ZookeeperHandler {
    /** ZK组节点名称 */
    public static final String GROUP_NAME = "/SESSIONS";

    public static final String NODE_SEP   = "/";

    /**
     * 执行具体操作
     * 
     * @throws Exception
     */
    public <T> T handle() throws Exception;

    /**
     * 设置ZK客户端对象
     * 
     * @param zookeeper ZK客户端
     */
    public void setZooKeeper(ZooKeeper zookeeper);
}
