package hbase.state;

import java.util.Random;

import storm.trident.TridentTopology;
import storm.trident.operation.builtin.Count;
import storm.trident.state.StateFactory;
import storm.trident.testing.FixedBatchSpout;
import trident.functions.MySplit;
import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.LocalDRPC;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.StormTopology;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

public class TridentPVTopo {

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public static StormTopology buildTopology(LocalDRPC drpc) {
		Random random = new Random();
		String[] hosts = { "www.taobao.com" };
		String[] session_id = { "ABYH6Y4V4SCVXTG6DPB4VH9U123",
				"XXYH6YCGFJYERTT834R52FDXV9U34",
				"BBYH61456FGHHJ7JL89RG5VV9UYU7", "CYYH6Y2345GHI899OFG4V9U567",
				"VVVYH6Y4V4SFXZ56JIPDPB4V678" };
		String[] time = { "2014-01-07 08:40:50", "2014-01-07 08:40:51",
				"2014-01-09 08:40:52", "2014-01-09 08:40:53",
				"2014-01-07 09:40:49", "2014-01-08 10:40:49",
				"2014-01-08 11:40:49", "2014-01-09 12:40:49" };

		FixedBatchSpout spout = new FixedBatchSpout(new Fields("eachLog"), 3,
				new Values(hosts[0] + "\t" + session_id[random.nextInt(5)]
						+ "\t" + time[random.nextInt(8)]), new Values(hosts[0]
						+ "\t" + session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]), new Values(hosts[0] + "\t"
						+ session_id[random.nextInt(5)] + "\t"
						+ time[random.nextInt(8)]));

		spout.setCycle(true);

		TridentConfig config = new TridentConfig("hbase_state", "rowkey");

		StateFactory state = HBaseAggregateState.transactional(config);

		TridentTopology topology = new TridentTopology();
		topology.newStream("spout1", spout)
				.each(new Fields("eachLog"), new MySplit("\t"),
						new Fields("date", "cf", "pv_count", "session_id"))
				.project(new Fields("date", "cf", "pv_count"))
				.groupBy(new Fields("date", "cf", "pv_count"))
				.persistentAggregate(state, new Count(), new Fields("PV"));

		return topology.build();
	}

	public static void main(String[] args) throws Exception {
		Config conf = new Config();
		conf.setMaxSpoutPending(20);
		if (args.length == 0) {
			LocalCluster cluster = new LocalCluster();
			cluster.submitTopology("wordCounter", conf, buildTopology(null));
		} else {
			conf.setNumWorkers(3);
			StormSubmitter.submitTopology(args[0], conf, buildTopology(null));
		}
	}
}
