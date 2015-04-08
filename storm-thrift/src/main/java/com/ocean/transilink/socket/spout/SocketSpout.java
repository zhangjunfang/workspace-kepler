/**
 * 
 */
package com.ocean.transilink.socket.spout;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.Serializable;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadLocalRandom;

import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.IRichSpout;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

/**
 * 
 * @author ocean
 * @date 2014年12月30日
 */
public class SocketSpout implements IRichSpout, Serializable {

	private static final long serialVersionUID = -2911770877156424415L;
	private static ServerSocket serverSocket;
	private ExecutorService executorService;// 线程池
	private final int POOL_SIZE = 10;// 单个CPU线程池大小
	private SpoutOutputCollector collector = null;
	static {
		try {
			serverSocket = new ServerSocket(9999);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public SocketSpout() {
		super();

	}

	@SuppressWarnings("rawtypes")
	@Override
	public void open(Map conf, TopologyContext context,
			SpoutOutputCollector collector) {
		this.collector = collector;
		this.executorService = Executors.newFixedThreadPool(Runtime
				.getRuntime().availableProcessors() * POOL_SIZE);
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
		while (true) {
			Socket socket = null;
			try {
				// 接收客户连接,只要客户进行了连接,就会触发accept();从而建立连接
				while (null == serverSocket) {
				}
				socket = serverSocket.accept();
				executorService.execute(new Handler(socket));

			} catch (Exception e) {
				e.printStackTrace();
			}
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
		declarer.declare(new Fields("command"));
	}

	@Override
	public Map<String, Object> getComponentConfiguration() {
		return null;
	}

	public class Handler implements Runnable {
		private Socket socket;

		public Handler(Socket socket) {
			this.socket = socket;
		}

		public PrintWriter getWriter(Socket socket) throws IOException {
			OutputStream socketOut = socket.getOutputStream();
			return new PrintWriter(socketOut, true);
		}

		public BufferedReader getReader(Socket socket) throws IOException {
			InputStream socketIn = socket.getInputStream();
			return new BufferedReader(new InputStreamReader(socketIn));
		}

		public String echo(String msg) {
			return "echo:" + msg;
		}

		@Override
		public void run() {
			try {
				BufferedReader br = getReader(socket);
				PrintWriter pw = getWriter(socket);
				String msg = null;
				while ((msg = br.readLine()) != null) {
					System.err.println(msg);
					pw.println(echo("----------" + msg));
					if (msg.equals("bye")) {
						break;
					}
					ThreadLocalRandom random = ThreadLocalRandom.current();
					int temp = random.nextInt();
					collector.emit(new Values(msg), temp);
				}
			} catch (IOException e) {
				e.printStackTrace();
			} finally {
				try {
					if (socket != null) {
						socket.close();
					}
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}
}
