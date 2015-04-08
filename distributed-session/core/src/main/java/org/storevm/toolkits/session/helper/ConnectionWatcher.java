/**
 * 
 */
package org.storevm.toolkits.session.helper;

import java.io.IOException;
import java.util.concurrent.CountDownLatch;

import org.apache.log4j.Logger;
import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.Watcher;
import org.apache.zookeeper.Watcher.Event.KeeperState;
import org.apache.zookeeper.ZooKeeper;

/**
 * 连接ZooKeeper服务器的watcher实现
 * 
 * @author  ocean
 * 
 */
public class ConnectionWatcher implements Watcher {
    private static final int SESSION_TIMEOUT = 5000;
    private CountDownLatch   signal          = new CountDownLatch(1);
    private Logger           log             = Logger.getLogger(getClass());

    /**
     * 连接ZK客户端
     * @throws IOException
     * @throws InterruptedException
     */
    public ZooKeeper connection(String servers) {
        return connection(servers, SESSION_TIMEOUT);
    }

    /**
     * 连接ZK客户端
     * 
     * @param servers
     * @param sessionTimeout
     * @return
     */
    public ZooKeeper connection(String servers, int sessionTimeout) {
        ZooKeeper zk;
        try {
            zk = new ZooKeeper(servers, sessionTimeout, this);
            signal.await();
            return zk;
        } catch (IOException e) {
            log.error(e);
        } catch (InterruptedException e) {
            log.error(e);
        }
        return null;
    }

    /*
     * (non-Javadoc)
     * 
     * @see
     * org.apache.zookeeper.Watcher#process(org.apache.zookeeper.WatchedEvent)
     */
    public void process(WatchedEvent event) {
        KeeperState state = event.getState();
        if (state == KeeperState.SyncConnected) {
            signal.countDown();
        }
    }
}
