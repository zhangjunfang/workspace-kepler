package com.ocean.transaction.bolt;

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
/**
 * 
 * @author ocean
 * date：2015年1月23日
 * description：批量提交数据
 */
public class MyCommitter extends BaseTransactionalBolt implements ICommitter {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	public static final String GLOBAL_KEY = "GLOBAL_KEY";
	public static Map<String, DbValue> dbMap = new HashMap<String, DbValue>();
    int  sum = 0;
	TransactionAttempt id;
	BatchOutputCollector collector;

	@Override
	public void execute(Tuple tuple) {
		sum += tuple.getInteger(1);
	}
    
	@Override
	public void finishBatch() {

		DbValue value = dbMap.get(GLOBAL_KEY);
		DbValue newValue;
		if (value == null || !value.txid.equals(id.getTransactionId())) {
			// 更新数据库
			newValue = new DbValue();
			newValue.txid = id.getTransactionId();
			if (value == null) {
				newValue.count = sum;
			} else {
				newValue.count = value.count + sum;
			}
			dbMap.put(GLOBAL_KEY, newValue);
		} else {
			newValue = value;
		}
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
   /**
    * 
    * @author ocean
    * date：2015年1月23日
    * description： 模拟数据存储设备【数据库。平面文件等】
    */
	public static class DbValue {
		BigInteger txid;
		int count = 0;
	}

}