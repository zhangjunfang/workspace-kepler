package drpc;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.LocalDRPC;
import backtype.storm.StormSubmitter;
import backtype.storm.drpc.DRPCSpout;
import backtype.storm.drpc.ReturnResults;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.topology.BasicOutputCollector;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.topology.base.BaseBasicBolt;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

public class ManualDRPC {
	public static class ExclamationBolt extends BaseBasicBolt {

		/**
	 * 
	 */
		private static final long serialVersionUID = -7471165532580755464L;

		@Override
		public void declareOutputFields(OutputFieldsDeclarer declarer) {
			declarer.declare(new Fields("result", "return-info"));
		}

		@Override
		public void execute(Tuple tuple, BasicOutputCollector collector) {
			String arg = tuple.getString(0);
			Object retInfo = tuple.getValue(1);
			collector.emit(new Values(arg + "!!!", retInfo));
		}

	}

	public static void main(String[] args) {
		TopologyBuilder builder = new TopologyBuilder();
		LocalDRPC drpc = new LocalDRPC();

		if (args.length > 0) {
			DRPCSpout spout = new DRPCSpout("exclamation");
			builder.setSpout("drpc", spout);
			builder.setBolt("exclaim", new ExclamationBolt(), 3)
					.shuffleGrouping("drpc");
			builder.setBolt("return", new ReturnResults(), 3).shuffleGrouping(
					"exclaim");

			Config conf = new Config();
			try {
				StormSubmitter.submitTopology(args[0], conf,
						builder.createTopology());
			} catch (AlreadyAliveException e) {
				e.printStackTrace();
			} catch (InvalidTopologyException e) {
				e.printStackTrace();
			}
		} else {
			DRPCSpout spout = new DRPCSpout("exclamation", drpc);
			builder.setSpout("drpc", spout);
			builder.setBolt("exclaim", new ExclamationBolt(), 3)
					.shuffleGrouping("drpc");
			builder.setBolt("return", new ReturnResults(), 3).shuffleGrouping(
					"exclaim");

			LocalCluster cluster = new LocalCluster();
			Config conf = new Config();
			cluster.submitTopology("exclaim", conf, builder.createTopology());
		}

		//
		//
		//
		// System.err.println(drpc.execute("exclamation", "aaa"));
		// System.err.println(drpc.execute("exclamation", "bbb"));

	}
}
