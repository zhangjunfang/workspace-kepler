/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper;

/**
 * ZK客户端接口定义
 * @author Administrator
 * @version $Id: ZooKeeperManager.java, v 0.1 2012-4-8 上午9:23:20 Administrator Exp $
 */
public interface ZooKeeperClient {

    /**
     * 
     * 
     * @param execute
     * @return
     * @throws Exception
     */
    public <T> T execute(ZookeeperHandler handler) throws Exception;
}
