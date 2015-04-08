package lesson;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.topology.TopologyBuilder;

public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {

		TopologyBuilder builder = new TopologyBuilder();

		builder.setSpout("spout", new MySpout(), 1);
		builder.setBolt("bolt", new MyBolt(), 1).setNumTasks(2)
				.shuffleGrouping("spout");
		// builder.setBolt("bolt", new MyBolt(), 2).fieldsGrouping("spout", new
		// Fields("log"));
		// builder.setBolt("bolt", new MyBolt(), 2).globalGrouping("spout");

		// Map conf = new HashMap();
		// conf.put(Config.TOPOLOGY_WORKERS, 4);
		Config conf = new Config();
		conf.setDebug(true);

		if (args.length > 0) {
			try {
				StormSubmitter.submitTopology(args[0], conf,
						builder.createTopology());
			} catch (AlreadyAliveException e) {
				e.printStackTrace();
			} catch (InvalidTopologyException e) {
				e.printStackTrace();
			}
		} else {
			LocalCluster localCluster = new LocalCluster();
			localCluster.submitTopology("mytopology", conf,
					builder.createTopology());
		}

	}

}
