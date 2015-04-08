package com.ocean.transaction.spout;

import java.math.BigInteger;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import backtype.storm.coordination.BatchOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.transactional.ITransactionalSpout;
import backtype.storm.transactional.TransactionAttempt;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

public class TransactionalSpout implements ITransactionalSpout<Step> {

	private static final long serialVersionUID = -3536820885168697028L;

	/**
	 * 数据源
	 */
	 Map<Long, String> dbMap = null;
	public TransactionalSpout() {
		Random random = new Random();
		dbMap = new HashMap<Long, String>();
		String[] hosts = { "www.taobao.com" };
		String[] session_id = { "ABYH6Y4V4SCVXTG6DPB4VH9U123",
				"XXYH6YCGFJYERTT834R52FDXV9U34",
				"BBYH61456FGHHJ7JL89RG5VV9UYU7", "CYYH6Y2345GHI899OFG4V9U567",
				"VVVYH6Y4V4SFXZ56JIPDPB4V678" };
		String[] time = { "2014-01-07 08:40:50", "2014-01-07 08:40:51",
				"2014-01-07 08:40:52", "2014-01-07 08:40:53",
				"2014-01-07 09:40:49", "2014-01-07 10:40:49",
				"2014-01-07 11:40:49", "2014-01-07 12:40:49" };

		for (long i = 0; i < 100; i++) {
			dbMap.put(i, hosts[0] + "\t" + session_id[random.nextInt(5)] + "\t"
					+ time[random.nextInt(8)]);
		}
	}

	private int batchSize = 0;

	
	/**
	 * 
	 */
	public TransactionalSpout(int batchSize) {
		this();
		this.batchSize = batchSize;
		
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		declarer.declare(new Fields("tx", "log"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {
		return null;
	}

	@SuppressWarnings("rawtypes")
	@Override
	public ITransactionalSpout.Coordinator<Step> getCoordinator(Map conf,
			TopologyContext context) {
		return new ITransactionalSpout.Coordinator<Step>() {
			@Override
			public Step initializeTransaction(BigInteger txid, Step prevMetadata) {
				Step step = new Step();
				if (null == prevMetadata || prevMetadata.getBatchSize() == 0) {
					step.setStartPoint(0);
					step.setBatchSize(batchSize);
				} else {
					step.setBatchSize(batchSize);
					step.setStartPoint(prevMetadata.getBatchSize() + batchSize);
				}
				System.err.println("启动一个事务：" + step.toString() + " classId: "
						+ this + "  事务批次号：" + txid);
				return step;
			}

			/**
			 * 如果返回true 表示发射一个新事务 否则 跳过这个事务 如果你想在两个事务之间【执行失败或者需要重复执行的批次tuple】
			 * 需要在这里暂停
			 * */
			@Override
			public boolean isReady() {
				return true;
			}

			/***
			 * 释放任何相关的资源
			 */
			@Override
			public void close() {

			}

		};
	}

	@SuppressWarnings("rawtypes")
	@Override
	public ITransactionalSpout.Emitter<Step> getEmitter(Map conf,
			TopologyContext context) {

		return new Emitter<Step>() {
			/***
			 * 发射数据：直接调用ublic Step initializeTransaction(BigInteger txid, Step
			 * prevMetadata) 保证在多个task之间 事务 id
			 */
			@Override
			public void emitBatch(TransactionAttempt tx, Step coordinatorMeta,
					BatchOutputCollector collector) {
				long beginPoint = coordinatorMeta.getStartPoint();
				int num = coordinatorMeta.getBatchSize();

				for (long i = beginPoint; i < num + beginPoint; i++) {
					if (null == dbMap || dbMap.get(i) == null) {
						break;
					}
					collector.emit(new Values(tx, dbMap.get(i)));
				}

			}

			/***
			 * 释放任何相关的资源
			 */
			@Override
			public void close() {

			}

			/***
			 * 清理前一次事务id
			 */
			@Override
			public void cleanupBefore(BigInteger txid) {
				System.err.println(" cleanupBefore 清理前一次事务id:  " + txid);
			}
		};
	}

}
