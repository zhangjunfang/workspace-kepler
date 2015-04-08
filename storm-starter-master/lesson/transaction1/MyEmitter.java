package transaction1;

import java.math.BigInteger;
import java.util.Map;

import backtype.storm.coordination.BatchOutputCollector;
import backtype.storm.transactional.ITransactionalSpout.Emitter;
import backtype.storm.transactional.TransactionAttempt;
import backtype.storm.tuple.Values;

public class MyEmitter implements Emitter<MyMata> {

	Map<Long, String> dbMap = null;

	public MyEmitter(Map<Long, String> dbMap) {
		this.dbMap = dbMap;
	}

	@Override
	public void cleanupBefore(BigInteger txid) {

	}

	@Override
	public void close() {

	}

	@Override
	public void emitBatch(TransactionAttempt tx, MyMata coordinatorMeta,
			BatchOutputCollector collector) {

		long beginPoint = coordinatorMeta.getBeginPoint();
		int num = coordinatorMeta.getNum();

		for (long i = beginPoint; i < num + beginPoint; i++) {
			if (dbMap.get(i) == null) {
				break;
			}
			collector.emit(new Values(tx, dbMap.get(i)));
		}
	}

}
