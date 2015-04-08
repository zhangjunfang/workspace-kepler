/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import org.apache.commons.lang3.StringUtils;
import org.apache.zookeeper.CreateMode;
import org.apache.zookeeper.ZooDefs.Ids;
import org.apache.zookeeper.data.Stat;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.session.zookeeper.AbstractZookeeperHandler;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 创建ZK节点
 * @author Administrator
 * @version $Id: CreateNode.java, v 0.1 2012-4-8 下午6:14:57 Administrator Exp $
 */
public class CreateNodeHandler extends AbstractZookeeperHandler {
    /** 节点元数据 */
    private SessionMetaData metadata;

    /**
     * 
     * @param id
     * @param metadata
     */
    public CreateNodeHandler(String id, SessionMetaData metadata) {
        super(id);
        this.metadata = metadata;
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.AbstractZookeeperHandler#handle()
     */
    @Override
    public <T> T handle() throws Exception {
        if (zookeeper != null) {
            String path = id;
            if (!StringUtils.startsWithIgnoreCase(id, GROUP_NAME)) {
                path = GROUP_NAME + NODE_SEP + id;
            }
            // 检查节点是否存在
            Stat stat = zookeeper.exists(path, false);
            //stat为null表示无此节点，需要创建
            if (stat == null) {
                // 创建组件点
                byte[] arrData = null;
                if (metadata != null) {
                    arrData = SerializationUtils.serialize(metadata);
                }
                String createPath = zookeeper.create(path, arrData, Ids.OPEN_ACL_UNSAFE,
                    CreateMode.PERSISTENT);
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("创建节点完成:[" + createPath + "]");
                }
            } else {
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("组节点已存在，无需创建[" + path + "]");
                }
            }
        }
        return (T) null;
    }
}
