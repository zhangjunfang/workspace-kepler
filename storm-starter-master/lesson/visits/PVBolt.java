package visits;

import java.net.InetAddress;
import java.util.Map;

import org.apache.zookeeper.CreateMode;
import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.Watcher;
import org.apache.zookeeper.ZooKeeper;
import org.apache.zookeeper.ZooDefs.Ids;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Tuple;

public class PVBolt implements IRichBolt {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public static final String zk_path = "/lock/storm/pv";

	@Override
	public void cleanup() {

		try {
			zKeeper.close();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	String logString = null;
	String lockData = null;
	String session_id = null;
	ZooKeeper zKeeper = null;

	long Pv = 0;
	long beginTime = System.currentTimeMillis();
	long endTime = 0;

	@Override
	public void execute(Tuple input) {

		try {
			endTime = System.currentTimeMillis();
			logString = input.getString(0);
			if (logString != null) {
				session_id = logString.split("\t")[1];
				if (session_id != null) {
					Pv++;
				}
			}
			if (endTime - beginTime >= 5 * 1000) {
				System.err.println(lockData + " ======================== ");
				if (lockData.equals(new String(zKeeper.getData(zk_path, false,
						null)))) {

					System.err.println("pv ======================== " + Pv * 4);
				}
				beginTime = System.currentTimeMillis();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {

		try {
			zKeeper = new ZooKeeper("192.168.1.107:2181,192.168.1.108:2181",
					3000, new Watcher() {

						@Override
						public void process(WatchedEvent event) {
							System.out.println("event:" + event.getType());

						}
					});
			while (zKeeper.getState() != ZooKeeper.States.CONNECTED) {
				Thread.sleep(1000);
			}

			InetAddress address = InetAddress.getLocalHost();
			lockData = address.getHostAddress() + ":" + context.getThisTaskId();

			if (zKeeper.exists(zk_path, false) == null) {
				zKeeper.create(zk_path, lockData.getBytes(),
						Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);
			}

		} catch (Exception e) {
			try {
				zKeeper.close();
			} catch (InterruptedException e1) {
				e1.printStackTrace();
			}
		}

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
