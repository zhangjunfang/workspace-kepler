package com.ocean.storm.test;

import java.util.Map;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;

public class MyBolt implements IRichBolt {

	private static final long serialVersionUID = -2342415542544028873L;
	OutputCollector collector = null;
	@Override
	public void cleanup() {

	}
	int num = 0;
	String valueString = null;
	@Override
	public void execute(Tuple input) {
//		input.getValueByField("log");
//		input.getValue(0);
		try {
			valueString = input.getStringByField("log") ;
			
			if(valueString != null)
			{
				num ++ ;
				System.err.println("lines  :"+num +"   session_id:"+valueString.split("\t")[1]);
			}
			collector.ack(input);
			Thread.sleep(1000);
		} catch (Exception e) {
			collector.fail(input);
			e.printStackTrace();
		}
		
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {
		this.collector = collector ;
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

		declarer.declare(new Fields("")) ;
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {
		return null;
	}

}
