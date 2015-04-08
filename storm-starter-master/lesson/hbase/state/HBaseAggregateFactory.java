package hbase.state;

import java.util.Map;

import storm.trident.state.State;
import storm.trident.state.StateFactory;
import storm.trident.state.StateType;
import storm.trident.state.map.CachedMap;
import storm.trident.state.map.MapState;
import storm.trident.state.map.NonTransactionalMap;
import storm.trident.state.map.OpaqueMap;
import storm.trident.state.map.SnapshottableMap;
import storm.trident.state.map.TransactionalMap;
import backtype.storm.task.IMetricsContext;
import backtype.storm.tuple.Values;

@SuppressWarnings({ "rawtypes", "unchecked" })
public class HBaseAggregateFactory implements StateFactory {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private StateType type;
	private TridentConfig config;

	@Override
	public State makeState(Map conf, IMetricsContext metrics,
			int partitionIndex, int numPartitions) {

		HBaseAggregateState state = new HBaseAggregateState(config);
		CachedMap c = new CachedMap(state, config.getStateCacheSize());

		MapState ms = null;
		if (type == StateType.NON_TRANSACTIONAL) {
			ms = NonTransactionalMap.build(c);

		} else if (type == StateType.OPAQUE) {
			ms = OpaqueMap.build(c);
		} else if (type == StateType.TRANSACTIONAL) {
			ms = TransactionalMap.build(c);
		}
		return new SnapshottableMap(ms, new Values("$GLOBAL$"));
	}

	public HBaseAggregateFactory(TridentConfig config, StateType type) {
		this.config = config;
		this.type = type;

		if (config.getStateSerializer() == null) {
			config.setStateSerializer(TridentConfig.DEFAULT_SERIALIZES
					.get(type));
		}
	}
}
