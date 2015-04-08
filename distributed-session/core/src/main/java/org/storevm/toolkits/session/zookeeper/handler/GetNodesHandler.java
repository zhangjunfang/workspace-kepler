/**
 * Storevm.com Inc.
 * Copyright (c) 2004-2012 All Rights Reserved.
 */
package org.storevm.toolkits.session.zookeeper.handler;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.zookeeper.data.Stat;
import org.storevm.toolkits.session.metadata.SessionMetaData;
import org.storevm.toolkits.utils.SerializationUtils;

/**
 * 返回所有节点Map的处理器
 * @author  ocean
 * @version $Id: GetNodesHandler.java, v 0.1 2012-4-9 上午10:52:37  ocean Exp $
 */
public class GetNodesHandler extends GetMetadataHandler {

    /**
     * @param id
     */
    public GetNodesHandler(String id) {
        super(id);
    }

    /** 
     * @see org.storevm.toolkits.session.zookeeper.ZookeeperHandler#handle()
     */
    @SuppressWarnings("unchecked")
	@Override
    public <T> T handle() throws Exception {
        Map<String, Object> nodeMap = new HashMap<String, Object>();
        if (zookeeper != null) {
            String path = GROUP_NAME + NODE_SEP + id;

            //获取元数据
            SessionMetaData metadata = super.handle();
            //如果不存在或是无效，则直接返回null
            if (metadata == null || !metadata.getValidate()) {
                return null;
            }
            //获取所有子节点
            List<String> nodes = zookeeper.getChildren(path, false);
            //存放数据
            for (String node : nodes) {
                String dataPath = path + NODE_SEP + node;
                Stat stat = zookeeper.exists(dataPath, false);
                //节点存在
                if (stat != null) {
                    //提取数据
                    byte[] data = zookeeper.getData(dataPath, false, null);
                    if (data != null) {
                        nodeMap.put(node, SerializationUtils.deserialize(data));
                    } else {
                        nodeMap.put(node, null);
                    }
                }
            }
        }
        return (T) nodeMap;
    }

}
