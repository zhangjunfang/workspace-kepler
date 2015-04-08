package transaction1.daily;

import java.util.HashMap;
import java.util.Map;

import tools.DateFmt;
import backtype.storm.coordination.BatchOutputCollector;
import backtype.storm.coordination.IBatchBolt;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.transactional.TransactionAttempt;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

public class MyDailyBatchBolt implements IBatchBolt<TransactionAttempt> {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	Map<String, Integer> countMap = new HashMap<String, Integer>();
	BatchOutputCollector collector;
	Integer count = null;
	String today = null;
	TransactionAttempt tx = null;

	@Override
	public void execute(Tuple tuple) {

		String log = tuple.getString(1);
		tx = (TransactionAttempt) tuple.getValue(0);
		if (log != null && log.split("\\t").length >= 3) {
			today = DateFmt.getCountDate(log.split("\\t")[2],
					DateFmt.date_short);
			count = countMap.get(today);
			if (count == null) {
				count = 0;
			}
			count++;

			countMap.put(today, count);
		}
	}

	@Override
	public void finishBatch() {
		collector.emit(new Values(tx, today, count));
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map conf, TopologyContext context,
			BatchOutputCollector collector, TransactionAttempt id) {

		this.collector = collector;

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

		declarer.declare(new Fields("tx", "date", "count"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
