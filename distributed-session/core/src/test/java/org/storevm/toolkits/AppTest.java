package org.storevm.toolkits;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

import org.storevm.toolkits.session.config.Configuration;
import org.storevm.toolkits.session.pool.ZookeeperPoolManager;
import org.storevm.toolkits.session.zookeeper.DefaultZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.ZooKeeperClient;
import org.storevm.toolkits.session.zookeeper.handler.CreateGroupNodeHandler;
import org.storevm.toolkits.session.zookeeper.handler.CreateNodeHandler;

/**
 * Unit test for simple App.
 */
public class AppTest extends TestCase {
    /**
     * Create the test case
     *
     * @param testName name of the test case
     */
    public AppTest(String testName) {
        super(testName);
    }

    /**
     * @return the suite of tests being tested
     */
    public static Test suite() {
        return new TestSuite(AppTest.class);
    }

    /**
     * Rigourous Test :-)
     */
    public void testApp() throws Exception {
        final String GROUP_NAME = "/SESSIONS";

        //获取配置信息对象
        Configuration config = Configuration.getInstance();

        //初始化ZK实例池
        ZookeeperPoolManager.getInstance().init(config);

        //获取ZK客户端
        ZooKeeperClient client = DefaultZooKeeperClient.getInstance();

        long current = System.currentTimeMillis();
        client.execute(new CreateGroupNodeHandler());
        System.out.println("create group node success");

        String id = GROUP_NAME + "/" + "xxxxxxxx001";
        client.execute(new CreateNodeHandler(id, null));
        System.out.println("create node id:" + id);

        System.out.println("exec time:[" + (System.currentTimeMillis() - current) + "]");

        //关闭ZK实例池
        ZookeeperPoolManager.getInstance().close();
    }
}
