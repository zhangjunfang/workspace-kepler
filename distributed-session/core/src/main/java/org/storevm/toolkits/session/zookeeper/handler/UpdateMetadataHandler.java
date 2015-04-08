/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import org.apache.zookeeper.ZooKeeper;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 更新元数据的处理器
 * @author  ocean
 * @version $Id: UpdateMetaDataHandler.java, v 0.1 2012-4-9 上午09:31:10  ocean Exp $
 */
public class UpdateMetadataHandler extends GetMetadataHandler {

    /**
     * @param nodeId
     */
    public UpdateMetadataHandler(String id) {
        super(id);
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.handler.GetMetadataHandler#handle()
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T handle() throws Exception {
        if (zookeeper != null) {
            //获取元数据
            SessionMetaData metadata = super.handle();
            if (metadata != null) {
                updateMetadata(metadata, zookeeper);
                return (T) metadata.getValidate();
            }
        }
        return (T) Boolean.FALSE;
    }

    /**
     * 更新节点数据
     * 
     * @param metadata
     * @param zk
     * @throws Exception
     */
    protected void updateMetadata(SessionMetaData metadata, ZooKeeper zk) throws Exception {
        if (metadata != null) {
            String id = metadata.getId();
            Long now = System.currentTimeMillis(); //当前时间
            //检查是否过期
            Long timeout = metadata.getLastAccessTm() + metadata.getMaxIdle(); //空闲时间
            //如果空闲时间小于当前时间，则表示Session超时
            if (timeout < now) {
                metadata.setValidate(false);
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("Session节点已超时[" + id + "]");
                }
            }
            //设置最后一次访问时间
            metadata.setLastAccessTm(now);
            //更新节点数据
            String path = GROUP_NAME + NODE_SEP + id;
            byte[] data = SerializationUtils.serialize(metadata);
            zk.setData(path, data, metadata.getVersion());
            if (LOGGER.isInfoEnabled()) {
                LOGGER.info("更新Session节点的元数据完成[" + path + "]");
            }
        }
    }
}
