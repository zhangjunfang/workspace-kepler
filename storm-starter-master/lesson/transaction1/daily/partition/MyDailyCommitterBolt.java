package transaction1.daily.partition;

import java.math.BigInteger;
import java.util.HashMap;
import java.util.Map;

import backtype.storm.coordination.BatchOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.base.BaseTransactionalBolt;
import backtype.storm.transactional.ICommitter;
import backtype.storm.transactional.TransactionAttempt;
import backtype.storm.tuple.Tuple;

public class MyDailyCommitterBolt extends BaseTransactionalBolt implements
		ICommitter {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	public static final String GLOBAL_KEY = "GLOBAL_KEY";
	public static Map<String, DbValue> dbMap = new HashMap<String, DbValue>();

	Map<String, Integer> countMap = new HashMap<String, Integer>();
	TransactionAttempt id;
	BatchOutputCollector collector;
	String today = null;

	@Override
	public void execute(Tuple tuple) {

		today = tuple.getString(1);
		Integer count = tuple.getInteger(2);
		id = (TransactionAttempt) tuple.getValue(0);

		if (today != null && count != null) {
			Integer batchCount = countMap.get(today);
			if (batchCount == null) {
				batchCount = 0;
			}
			batchCount += count;
			countMap.put(today, batchCount);
		}
	}

	@Override
	public void finishBatch() {

		if (countMap.size() > 0) {
			DbValue value = dbMap.get(GLOBAL_KEY);
			DbValue newValue;
			if (value == null || !value.txid.equals(id.getTransactionId())) {
				// 更新数据库
				newValue = new DbValue();
				newValue.txid = id.getTransactionId();
				newValue.dateStr = today;
				if (value == null) {
					newValue.count = countMap.get(today);
				} else {
					newValue.count = value.count + countMap.get("2014-01-07");
				}
				dbMap.put(GLOBAL_KEY, newValue);
			} else {
				newValue = value;
			}
		}

		System.out.println("total==========================:"
				+ dbMap.get(GLOBAL_KEY).count);
		// collector.emit(tuple)
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void prepare(Map conf, TopologyContext context,
			BatchOutputCollector collector, TransactionAttempt id) {

		this.id = id;
		this.collector = collector;
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

	}

	public static class DbValue {
		BigInteger txid;
		int count = 0;
		String dateStr;
	}

}
