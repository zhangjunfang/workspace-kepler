package hbase.state;

import java.util.HashMap;
import java.util.Map;

import storm.trident.state.JSONNonTransactionalSerializer;
import storm.trident.state.JSONOpaqueSerializer;
import storm.trident.state.JSONTransactionalSerializer;
import storm.trident.state.Serializer;
import storm.trident.state.StateType;

@SuppressWarnings("rawtypes")
public class TridentConfig<T> extends TupleTableConfig {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private int stateCacheSize = 1000;

	private Serializer stateSerializer;

	public int getStateCacheSize() {
		return stateCacheSize;
	}

	public void setStateCacheSize(int stateCacheSize) {
		this.stateCacheSize = stateCacheSize;
	}

	public Serializer getStateSerializer() {
		return stateSerializer;
	}

	public void setStateSerializer(Serializer stateSerializer) {
		this.stateSerializer = stateSerializer;
	}

	public static final Map<StateType, Serializer> DEFAULT_SERIALIZES = new HashMap<StateType, Serializer>() {
		/**
		 * 
		 */
		private static final long serialVersionUID = 1122063963953675658L;

		{
			put(StateType.NON_TRANSACTIONAL,
					new JSONNonTransactionalSerializer());
			put(StateType.TRANSACTIONAL, new JSONTransactionalSerializer());
			put(StateType.OPAQUE, new JSONOpaqueSerializer());
		}

	};

	public TridentConfig(String table, String rowkeyField) {
		super(table, rowkeyField);
	}

	public TridentConfig(String table, String rowkeyField,
			String tupleTimestampField) {
		super(table, rowkeyField, tupleTimestampField);
	}
}
