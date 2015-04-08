/**
 * 
 */
package com.ocean.transilink.mina.bolt;

import java.io.Serializable;
import java.util.Map;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Tuple;

/**
 * @author ocean
 * @date 2015年1月1日
 */
public class MessageBolt implements Serializable, IRichBolt {

	private static final long serialVersionUID = -4868310317922471710L;

	
	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {
		

	}

	
	@Override
	public void execute(Tuple input) {

		System.err.println("-------MessageBolt----execute--------"+input.getString(0));

	}

	
	@Override
	public void cleanup() {
		

	}

	
	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		

	}


	@Override
	public Map<String, Object> getComponentConfiguration() {
		
		return null;
	}

}
