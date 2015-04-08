package net.xulingbo.zookeeper.locks;

import net.xulingbo.zookeeper.TestMainClient;
import net.xulingbo.zookeeper.TestMainServer;
import org.apache.log4j.Logger;
import org.apache.zookeeper.CreateMode;
import org.apache.zookeeper.KeeperException;
import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.ZooDefs;
import org.apache.zookeeper.data.Stat;

import java.util.Arrays;
import java.util.List;

/**
 * locks
 * <p/>
 * Author By: ocean
 * Created Date: 2010-9-7 16:49:40
 */
public class Locks extends TestMainClient {
    public static final Logger logger = Logger.getLogger(Locks.class);
    String myZnode;
    
    public Locks(String connectString, String root) {
        super(connectString);
        this.root = root;
        if (zk != null) {
            try {
                Stat s = zk.exists(root, false);
                if (s == null) {
                    zk.create(root, new byte[0], ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                }
            } catch (KeeperException e) {
                logger.error(e);
            } catch (InterruptedException e) {
                logger.error(e);
            }
        }
    }
    void getLock() throws KeeperException, InterruptedException{
        List<String> list = zk.getChildren(root, false);
        String[] nodes = list.toArray(new String[list.size()]);
        Arrays.sort(nodes);
        if(myZnode.equals(root+"/"+nodes[0])){
            doAction();
        }
        else{
            waitForLock(nodes[0]);
        }
    }
    void check() throws InterruptedException, KeeperException {
        myZnode = zk.create(root + "/lock_" , new byte[0], ZooDefs.Ids.OPEN_ACL_UNSAFE,CreateMode.EPHEMERAL_SEQUENTIAL);
        getLock();
    }
    void waitForLock(String lower) throws InterruptedException, KeeperException {
        Stat stat = zk.exists(root + "/" + lower,true);
        if(stat != null){
            mutex.wait();
        }
        else{
            getLock();
        }
    }
    @Override
    public void process(WatchedEvent event) {
        if(event.getType() == Event.EventType.NodeDeleted){
            System.out.println("�õ�֪ͨ");
            super.process(event);
            doAction();
        }
    }
    /**
     * ִ����������
     */
    private void doAction(){
        System.out.println("ͬ�������Ѿ��õ�ͬ�������Կ�ʼִ�к����������");
    }

    public static void main(String[] args) {
        TestMainServer.start();
        String connectString = "localhost:"+TestMainServer.CLIENT_PORT;

        Locks lk = new Locks(connectString, "/locks");
        try {
            lk.check();
        } catch (InterruptedException e) {
            logger.error(e);
        } catch (KeeperException e) {
            logger.error(e);
        }
    }


}
