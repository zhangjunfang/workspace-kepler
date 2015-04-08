package WordCount;

import java.util.Map;

import backtype.storm.task.TopologyContext;
import backtype.storm.topology.BasicOutputCollector;
import backtype.storm.topology.FailedException;
import backtype.storm.topology.IBasicBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

public class MySplit implements IBasicBolt {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	String patton;

	public MySplit(String patton) {
		this.patton = patton;
	}

	@Override
	public void cleanup() {

	}

	@Override
	public void execute(Tuple input, BasicOutputCollector collector) {

		try {
			String sen = input.getString(0);
			if (sen != null) {
				for (String word : sen.split(patton)) {
					collector.emit(new Values(word));
				}
			}
		} catch (Exception e) {
			throw new FailedException("split fail!");
		}
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context) {

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		declarer.declare(new Fields("word"));

	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
