package com.ocean.storm.study;

import java.util.HashMap;
import java.util.Map;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.testing.TestGlobalCount;
import backtype.storm.testing.TestWordCounter;
import backtype.storm.testing.TestWordSpout;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.tuple.Fields;
import backtype.storm.utils.Utils;

public class Example {

	public static void main(String[] args) throws Exception {
		stormLocal();

		// stormCluster();
	}

	/**
	 * Local
	 */

	private static void stormLocal() {

		// 实例化topologyBuilder类。
		TopologyBuilder builder = new TopologyBuilder();

		// 设置喷发节点并分配并发数，该并发数将会控制该对象在集群中的线程数。
		builder.setSpout("1", new TestWordSpout(true), 1);
		builder.setSpout("2", new TestWordSpout(true), 1);
		// 设置数据处理节点，并分配并发数。指定该几点接收喷发节点的策略为随机方式。
		builder.setBolt("3", new TestWordCounter(), 1)
				.fieldsGrouping("1", new Fields("word"))
				.fieldsGrouping("2", new Fields("word"));
		builder.setBolt("4", new TestGlobalCount()).globalGrouping("1");

		Map<String, Object> conf = new HashMap<String, Object>();

		conf.put(Config.TOPOLOGY_WORKERS, 4);
		conf.put(Config.TOPOLOGY_DEBUG, true);
		LocalCluster cluster = new LocalCluster();

		cluster.submitTopology("mytopology", conf, builder.createTopology());
		Utils.sleep(10000);
		cluster.shutdown();

	}

	/**
	 * Cluster
	 *
	 * @throws AlreadyAliveException
	 * @throws InvalidTopologyException
	 */
	public static void stormCluster() throws AlreadyAliveException,
			InvalidTopologyException {

		TopologyBuilder builder = new TopologyBuilder();

		builder.setSpout("1", new TestWordSpout(true), 5);
		builder.setSpout("2", new TestWordSpout(true), 3);
		builder.setBolt("3", new TestWordCounter(), 3)
				.fieldsGrouping("1", new Fields("word"))
				.fieldsGrouping("2", new Fields("word"));
		builder.setBolt("4", new TestGlobalCount()).globalGrouping("1");

		Map<String, Object> conf = new HashMap<String, Object>();
		conf.put(Config.TOPOLOGY_WORKERS, 4);

		StormSubmitter.submitTopology("mytopology", conf,
				builder.createTopology());

	}

}
