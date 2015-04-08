package trident.functions;

import storm.trident.operation.BaseFunction;
import storm.trident.operation.TridentCollector;
import storm.trident.tuple.TridentTuple;
import backtype.storm.tuple.Values;

public class Split extends BaseFunction {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	String patton = null;

	public Split(String patton) {
		this.patton = patton;
	}

	@Override
	public void execute(TridentTuple tuple, TridentCollector collector) {
		String sentence = tuple.getString(0);
		for (String word : sentence.split(patton)) {
			collector.emit(new Values(word));
		}
	}
}
