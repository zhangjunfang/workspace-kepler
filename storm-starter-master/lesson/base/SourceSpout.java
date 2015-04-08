package base;

import java.util.Map;
import java.util.Queue;
import java.util.Random;
import java.util.concurrent.ConcurrentLinkedQueue;

import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichSpout;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

public class SourceSpout implements IRichSpout {

	/**
	 * 数据源Spout
	 */
	private static final long serialVersionUID = 1L;

	Queue<String> queue = new ConcurrentLinkedQueue<String>();

	SpoutOutputCollector collector = null;

	String str = null;

	@Override
	public void nextTuple() {
		if (queue.size() >= 0) {
			collector.emit(new Values(queue.poll()));
		}
		try {
			Thread.sleep(500);
		} catch (InterruptedException e) {

			e.printStackTrace();
		}

	}

	@SuppressWarnings("rawtypes")
	@Override
	public void open(Map conf, TopologyContext context,
			SpoutOutputCollector collector) {
		try {
			this.collector = collector;

			Random random = new Random();
			String[] hosts = { "www.taobao.com" };
			String[] session_id = { "ABYH6Y4V4SCVXTG6DPB4VH9U123",
					"XXYH6YCGFJYERTT834R52FDXV9U34",
					"BBYH61456FGHHJ7JL89RG5VV9UYU7",
					"CYYH6Y2345GHI899OFG4V9U567", "VVVYH6Y4V4SFXZ56JIPDPB4V678" };
			String[] time = { "2014-01-07 08:40:50", "2014-01-07 08:40:51",
					"2014-01-07 08:40:52", "2014-01-07 08:40:53",
					"2014-01-07 09:40:49", "2014-01-07 10:40:49",
					"2014-01-07 11:40:49", "2014-01-07 12:40:49" };

			for (int i = 0; i < 100; i++) {
				queue.add(hosts[0] + "\t" + session_id[random.nextInt(5)]
						+ "\t" + time[random.nextInt(8)]);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void close() {

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {

		declarer.declare(new Fields("log"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

	@Override
	public void ack(Object msgId) {

		System.out.println("spout ack:" + msgId.toString());
	}

	@Override
	public void activate() {

	}

	@Override
	public void deactivate() {

	}

	@Override
	public void fail(Object msgId) {

		System.out.println("spout fail:" + msgId.toString());
	}

}
