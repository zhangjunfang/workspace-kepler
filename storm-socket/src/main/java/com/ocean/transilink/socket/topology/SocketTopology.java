/**
 * 
 */
package com.ocean.transilink.socket.topology;

import java.io.IOException;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.utils.Utils;

import com.ocean.transilink.socket.bolt.CommandBolt;
import com.ocean.transilink.socket.spout.SocketSpout;

/**
 * @author ocean
 * @date 2014年12月30日
 */
public class SocketTopology {

	/**
	 * @param args
	 * @throws IOException 
	 */
	public static void main(String[] args) throws IOException {
		//ServerSocket server = new ServerSocket(9999);
		TopologyBuilder builder = new TopologyBuilder();
		builder.setSpout("start", new SocketSpout(),4);
		builder.setBolt("end", new CommandBolt(),8).shuffleGrouping("start");
		LocalCluster cluster = new LocalCluster();
		Config conf = new Config();
		conf.setDebug(false);
		conf.setNumWorkers(4);
		cluster.submitTopology("mytopology", conf, builder.createTopology());
		Utils.sleep(10000);
		//cluster.shutdown();
	}

}
