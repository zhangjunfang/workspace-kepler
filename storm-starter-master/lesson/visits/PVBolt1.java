package visits;

import java.util.Map;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

public class PVBolt1 implements IRichBolt {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	OutputCollector collector = null;

	@Override
	public void cleanup() {

	}

	String logString = null;

	String session_id = null;

	long Pv = 0;

	@Override
	public void execute(Tuple input) {

		logString = input.getString(0);
		session_id = logString.split("\t")[1];
		if (session_id != null) {
			Pv++;
		}
		collector.emit(new Values(Thread.currentThread().getId(), Pv));
		System.err.println("threadid = " + Thread.currentThread().getId()
				+ "; pv=" + Pv);
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {

		this.collector = collector;
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

		declarer.declare(new Fields("threadId", "pv"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
