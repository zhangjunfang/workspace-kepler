/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import java.io.Serializable;

import org.apache.zookeeper.CreateMode;
import org.apache.zookeeper.ZooDefs.Ids;
import org.apache.zookeeper.data.Stat;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 将数据放入节点的处理器
 * @author  ocean
 * @version $Id: PutDataHandler.java, v 0.1 2012-4-9 上午10:22:28  ocean Exp $
 */
public class PutDataHandler extends GetDataHandler {
    /** 存放节点的数据 */
    private Serializable data;

    /**
     * 
     * @param id
     * @param key
     * @param data
     */
    public PutDataHandler(String id, String key, Serializable data) {
        super(id, key);
        this.data = data;
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.handler.GetDataHandler#handle()
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T handle() throws Exception {
        if (zookeeper != null) {
            String path = GROUP_NAME + NODE_SEP + id;
            // 检查指定的Session节点是否存在
            Stat stat = zookeeper.exists(path, false);
            //如果节点存在则删除之
            if (stat != null) {
                //查找数据节点是否存在，不存在就创建一个
                String dataPath = path + NODE_SEP + key;
                stat = zookeeper.exists(dataPath, false);
                if (stat == null) {
                    //创建数据节点
                    zookeeper.create(dataPath, null, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                    if (LOGGER.isInfoEnabled()) {
                        LOGGER.info("创建数据节点完成[" + dataPath + "]");
                    }
                }
                //在节点上设置数据，所有数据必须可序列化
                if (data instanceof Serializable) {
                    int dataNodeVer = -1;
                    if (stat != null) {
                        //记录数据节点的版本
                        dataNodeVer = stat.getVersion();
                    }
                    byte[] arrData = SerializationUtils.serialize(data);
                    stat = zookeeper.setData(dataPath, arrData, dataNodeVer);
                    if (LOGGER.isInfoEnabled()) {
                        LOGGER.info("更新数据节点数据完成[" + dataPath + "][" + data + "]");
                    }
                    return (T) Boolean.TRUE;
                }
            }
        }
        return (T) Boolean.FALSE;
    }

}
