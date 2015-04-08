/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import org.apache.commons.lang3.time.DateFormatUtils;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 超时处理器
 * @author  ocean
 * @version $Id: TimeoutHandler.java, v 0.1 2012-4-9 上午09:44:31  ocean Exp $
 */
public class TimeoutHandler extends GetMetadataHandler {

    /**
     * @param id
     */
    public TimeoutHandler(String id) {
        super(id);
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.ZookeeperHandler#handle()
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T handle() throws Exception {
        if (zookeeper != null) {
            String path = GROUP_NAME + NODE_SEP + id;
            //获取元数据
            SessionMetaData metadata = super.handle();
            //如果不存在，则直接返回true
            if (metadata == null) {
                return (T) Boolean.TRUE;
            }
            //如果节点失效，直接返回
            if (!metadata.getValidate()) {
                return (T) Boolean.TRUE;
            } else {
                //检查节点是否超时
                Long now = System.currentTimeMillis(); //当前时间
                //检查是否过期
                Long timeout = metadata.getLastAccessTm() + metadata.getMaxIdle(); //空闲时间
                //如果空闲时间小于当前时间，则表示Session超时
                if (timeout < now) {
                    metadata.setValidate(false);
                    //更新节点数据
                    byte[] data = SerializationUtils.serialize(metadata);
                    zookeeper.setData(path, data, metadata.getVersion());
                }
                String timeoutStr = DateFormatUtils.format(timeout, "yyyy-MM-dd HH:mm");
                if (LOGGER.isInfoEnabled()) {
                    LOGGER.info("session超时检查:[" + timeoutStr + "]");
                }
            }
        }
        return (T) Boolean.FALSE;
    }

}
