package visits;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichBolt;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Tuple;

public class PVSumBolt implements IRichBolt {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	Map<Long, Long> counts = new HashMap<Long, Long>();

	@Override
	public void cleanup() {

	}

	@Override
	public void execute(Tuple input) {

		long threadID = input.getLong(0);
		long pv = input.getLong(1);
		counts.put(threadID, pv);
		long word_sum = 0;
		// 获取总数，遍历counts 的values，进行sum
		Iterator<Long> i = counts.values().iterator();
		while (i.hasNext()) {
			word_sum += i.next();
		}

		System.err.println("pv_all = " + word_sum);
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map stormConf, TopologyContext context,
			OutputCollector collector) {

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
