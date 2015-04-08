package com.ocean.storm.study;

import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.base.BaseRichSpout;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;
import backtype.storm.utils.Utils;

import java.util.Map;
import java.util.Random;

public class RandomSentenceSpout extends BaseRichSpout {

	private static final long serialVersionUID = 929239623576073368L;
	/**
	 * 用来发射数据的工具类
	 */
	SpoutOutputCollector _collector;
	Random _rand;

	/**
	 * 这里初始化collector
	 */
	@Override
	@SuppressWarnings("rawtypes")
	public void open(Map conf, TopologyContext context,
			SpoutOutputCollector collector) {
		_collector = collector;
		_rand = new Random();
	}

	/**
	 * 该方法会在SpoutTracker类中被调用每调用一次就可以向storm集群中发射一条数据（一个tuple元组） 该方法会被不停的调用
	 */
	@Override
	public void nextTuple() {

		// 模拟等待100ms
		Utils.sleep(100);

		// 构造随机数据
		String[] sentences = new String[] { "the cow jumped over the moon",
				"an apple a day keeps the doctor away",
				"four score and seven years ago",
				"snow white and the seven dwarfs", "i am at two with nature" };
		String sentence = sentences[_rand.nextInt(sentences.length)];
		// 调用发射方法
		_collector.emit(new Values(sentence));
	}

	@Override
	public void ack(Object id) {
	}

	@Override
	public void fail(Object id) {
	}

	/**
	 * 这里定义字段id，该id在简单模式下没有用处，但在按照字段分组的模式下有很大的用处。 该declarer变量有很大作用，我们还可以调用
	 * declarer.declareStream(); 来定义stramId，该id可以用来定义 更加复杂的流拓扑结构
	 */
	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		declarer.declare(new Fields("word"));
	}

}