package user_visit;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.tuple.Fields;
import base.SourceSpout;

public class UVTopo {

	/**
	 * @param args
	 */
	public static void main(String[] args) {

		TopologyBuilder builder = new TopologyBuilder();

		builder.setSpout("spout", new SourceSpout(), 1);
		builder.setBolt("FmtLogBolt", new FmtLogBolt(), 4).shuffleGrouping(
				"spout");
		builder.setBolt("sumBolt", new DeepVisitBolt(), 4).fieldsGrouping(
				"FmtLogBolt", new Fields("date", "session_id"));
		builder.setBolt("UvSum", new UVSumBolt(), 1).shuffleGrouping("sumBolt");

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
