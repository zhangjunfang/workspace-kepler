/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import org.apache.zookeeper.data.Stat;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 
 * @author  ocean
 * @version $Id: RemoveDataHandler.java, v 0.1 2012-4-9 上午10:49:17  ocean Exp $
 */
public class RemoveDataHandler extends GetDataHandler {

    /**
     * @param id
     * @param key
     */
    public RemoveDataHandler(String id, String key) {
        super(id, key);
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.handler.GetDataHandler#handle()
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T handle() throws Exception {
        Object value = null;
        if (zookeeper != null) {
            String path = GROUP_NAME + NODE_SEP + id;

            // 检查指定的Session节点是否存在
            Stat stat = zookeeper.exists(path, false);
            if (stat != null) {
                //查找数据节点是否存在
                String dataPath = path + NODE_SEP + key;
                stat = zookeeper.exists(dataPath, false);
                if (stat != null) {
                    //得到数据节点的数据
                    byte[] data = zookeeper.getData(dataPath, false, null);
                    if (data != null) {
                        //反序列化
                        value = SerializationUtils.deserialize(data);
                    }
                    //删除节点
                    zookeeper.delete(dataPath, -1);
                }
            }
        }
        return (T) value;
    }

}
