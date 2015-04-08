package com.ocean.transilink.socket.bolt;

import java.io.Serializable;
import java.util.Map;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;

/**
 * 
 * @author ocean
 * @date 2014年12月30日
 */
public class CommandBolt implements IRichBolt, Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3236827257405636176L;
	private OutputCollector collector = null;

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {
		this.collector = collector;
	}

	@Override
	public void execute(Tuple input) {
		String t = input.getString(0);
		System.err
				.println("-----CommandBolt---------execute(Tuple input)----------"
						+ t);
		this.collector.ack(input);
	}

	@Override
	public void cleanup() {
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		declarer.declare(new Fields("commandxxxx"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {
		return null;
	}

}
