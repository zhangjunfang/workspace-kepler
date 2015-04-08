/**
 * 
 */
package com.ocean.transilink.mina.spout;

import java.io.IOException;
import java.io.Serializable;
import java.net.InetSocketAddress;
import java.nio.charset.Charset;
import java.util.Map;
import java.util.concurrent.LinkedBlockingQueue;

import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSessionConfig;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.filter.codec.textline.LineDelimiter;
import org.apache.mina.filter.codec.textline.TextLineCodecFactory;
import org.apache.mina.transport.socket.nio.NioSocketAcceptor;

import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichSpout;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

/**
 * @author ocean
 * @date 2015年1月1日
 */
public class MinaSpout implements IRichSpout, Serializable {
	public static final LinkedBlockingQueue<String> blockingQueue = new LinkedBlockingQueue<>();
	private static final long serialVersionUID = -4136654451404867024L;
	/** 连接管理器 */
	private static NioSocketAcceptor acceptor = null;
	private SpoutOutputCollector collector = null;

	static {
		// 创建客户端连接器.
		acceptor = new NioSocketAcceptor();
		acceptor.getFilterChain().addLast(
				"encode",
				new ProtocolCodecFilter(new TextLineCodecFactory(Charset
						.forName("UTF-8"), LineDelimiter.WINDOWS.getValue(),
						LineDelimiter.WINDOWS.getValue())));
		IoSessionConfig isf = acceptor.getSessionConfig();
		isf.setIdleTime(IdleStatus.BOTH_IDLE, 10);
		// 添加处理器
		acceptor.setHandler(new IoHandler(blockingQueue));

		

		// 服务器端设置端口
		try {
			acceptor.bind(new InetSocketAddress("127.0.0.1", 4444));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void open(Map conf, TopologyContext context,
			SpoutOutputCollector collector) {

		this.collector = collector;

	}

	@Override
	public void close() {

	}

	@Override
	public void activate() {

	}

	@Override
	public void deactivate() {

	}

	@Override
	public void nextTuple() {
		try {
			collector.emit(new Values(blockingQueue.take()));
		} catch (InterruptedException e) {
			e.printStackTrace();
		}

	}

	@Override
	public void ack(Object msgId) {

	}

	@Override
	public void fail(Object msgId) {

	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		declarer.declare(new Fields("msg"));

	}

	@Override
	public Map<String, Object> getComponentConfiguration() {

		return null;
	}

}
