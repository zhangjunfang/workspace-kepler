package net.xulingbo.zookeeper.synchronizing;


import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.List;
import net.xulingbo.zookeeper.TestMainClient;
import net.xulingbo.zookeeper.TestMainServer;
import org.apache.log4j.Logger;
import org.apache.zookeeper.CreateMode;
import org.apache.zookeeper.KeeperException;
import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.ZooDefs.Ids;
import org.apache.zookeeper.data.Stat;

/**
 * Synchronizing
 * <p/>
 * Author By: ocean
 * Created Date: 2010-9-3 15:09:50
 */
public class Synchronizing extends TestMainClient {
    int size;
    String name;
    public static final Logger logger = Logger.getLogger(Synchronizing.class);

    /**
     * ���캯��
     *
     * @param connectString ����������
     * @param root ��Ŀ¼
     * @param size ���д�С
     */
    Synchronizing(String connectString, String root, int size) {
        super(connectString);
        this.root = root;
        this.size = size;

        if (zk != null) {
            try {
                Stat s = zk.exists(root, false);
                if (s == null) {
                    zk.create(root, new byte[0], Ids.OPEN_ACL_UNSAFE,CreateMode.PERSISTENT);
                }
            } catch (KeeperException e) {
                logger.error(e);
            } catch (InterruptedException e) {
                logger.error(e);
            }
        }
        try {
            name = new String(InetAddress.getLocalHost().getCanonicalHostName().toString());
        } catch (UnknownHostException e) {
            logger.error(e);
        }

    }

    /**
     * �������
     *
     * @return
     * @throws KeeperException
     * @throws InterruptedException
     */

    void addQueue() throws KeeperException, InterruptedException{
        zk.exists(root + "/start",true);
        zk.create(root + "/" + name, new byte[0], Ids.OPEN_ACL_UNSAFE,CreateMode.EPHEMERAL_SEQUENTIAL);
        synchronized (mutex) {
            List<String> list = zk.getChildren(root, false);
            if (list.size() < size) {
                mutex.wait();
            } else {
                zk.create(root + "/start", new byte[0], Ids.OPEN_ACL_UNSAFE,CreateMode.PERSISTENT);
            }
        }
    }

    @Override
    public void process(WatchedEvent event) {
        if(event.getPath().equals(root + "/start") && event.getType() == Event.EventType.NodeCreated){
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

    public static void main(String args[]) {
        //����Server
        TestMainServer.start();
        String connectString = "localhost:"+TestMainServer.CLIENT_PORT;
        int size = 1; 
        Synchronizing b = new Synchronizing(connectString, "/synchronizing", size);
        try{
            b.addQueue();
        } catch (KeeperException e){
            logger.error(e);
        } catch (InterruptedException e){
            logger.error(e);
        }
    }
}

