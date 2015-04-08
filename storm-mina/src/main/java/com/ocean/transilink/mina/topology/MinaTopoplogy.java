/**
 * 
 */
package com.ocean.transilink.mina.topology;

import java.io.Serializable;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.topology.TopologyBuilder;

import com.ocean.transilink.mina.bolt.MessageBolt;
import com.ocean.transilink.mina.spout.MinaSpout;

/**
 * @author ocean
 * @date 2015年1月1日
 */
public class MinaTopoplogy implements Serializable {

	private static final long serialVersionUID = -2796320246498967450L;

	public static void main(String[] args) {
		TopologyBuilder builder = new TopologyBuilder();
		builder.setSpout("start", new MinaSpout(), 4);
		builder.setBolt("end", new MessageBolt(), 8).shuffleGrouping("start");
		LocalCluster cluster = new LocalCluster();
		Config conf = new Config();
		conf.setDebug(false);
		conf.setNumWorkers(4);
		cluster.submitTopology("mina", conf, builder.createTopology());
	}
}
