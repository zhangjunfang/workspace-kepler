package com.ocean.transaction.topology;


import com.ocean.transaction.bolt.MyBaseTransactionalBolt;
import com.ocean.transaction.bolt.MyCommitter;
import com.ocean.transaction.spout.TransactionalSpout;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.transactional.TransactionalTopologyBuilder;

@SuppressWarnings("deprecation")
public class ITransactionalTopology {

	/**
	 * @param args
	 */
	public static void main(String[] args) {

		TransactionalTopologyBuilder builder = new TransactionalTopologyBuilder("ttbId","spoutid",new TransactionalSpout(10),1);
		builder.setBolt("bolt1", new MyBaseTransactionalBolt(),3).shuffleGrouping("spoutid");
		builder.setBolt("committer", new MyCommitter(),1).shuffleGrouping("bolt1") ;
		
		Config conf = new Config() ;
		conf.setDebug(false);

		if (args.length > 0) {
			try {
				StormSubmitter.submitTopology(args[0], conf, builder.buildTopology());
			} catch (AlreadyAliveException e) {
				e.printStackTrace();
			} catch (InvalidTopologyException e) {
				e.printStackTrace();
			}
		}else {
			LocalCluster localCluster = new LocalCluster();
			localCluster.submitTopology("mytopology", conf, builder.buildTopology());
		}
		
		
		
		
	}

}
