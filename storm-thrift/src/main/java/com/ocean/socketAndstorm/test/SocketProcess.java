package com.ocean.socketAndstorm.test;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Map;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.OutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.topology.base.BaseRichBolt;
import backtype.storm.topology.base.BaseRichSpout;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

/*
 * 
 *
 *  storm jar stormtest.jar socket.SocketProcess /home/hadoop/out_socket.txt true
 * 
 */

public class SocketProcess {
	public static class SocketSpout extends BaseRichSpout {
		private static Socket sock = null;
		private static BufferedReader in = null;
		private String str = null;
		private static final long serialVersionUID = 1L;
		private SpoutOutputCollector _collector;

		// 定义spout文件
		public SocketSpout() {

		}

		// 定义如何读取spout文件
		@SuppressWarnings("rawtypes")
		@Override
		public void open(Map conf, TopologyContext context,
				SpoutOutputCollector collector) {
			_collector = collector;
			try {
				sock = new Socket("127.0.0.1", 5678);
				in = new BufferedReader(new InputStreamReader(
						sock.getInputStream()));
			} catch (UnknownHostException e) {
				e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			}

		}

		// 获取下一个tuple的方法
		@Override
		public void nextTuple() {
			if (sock == null) {
				try {
					sock = new Socket("192.168.27.100", 5678);
					in = new BufferedReader(new InputStreamReader(
							sock.getInputStream()));
				} catch (UnknownHostException e) {
					e.printStackTrace();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}

			while (true) {

				try {
					str = in.readLine();
				} catch (IOException e) {
					e.printStackTrace();
				}
				System.out.println(str);
				_collector.emit(new Values(str));
				if (str.equals("end")) {
					break;
				}
			}

		}

		@Override
		public void declareOutputFields(OutputFieldsDeclarer declarer) {
			declarer.declare(new Fields("line"));
		}

	}

	public static class Process extends BaseRichBolt {

		/**
		 * 
		 */
		private static final long serialVersionUID = -7141978625012136249L;
		private String _outFile;
		private OutputCollector _collector;
		private BufferedWriter bw;

		public Process(String outFile) {

			this._outFile = outFile;

		}

		// 把输出结果保存到外部文件里面。
		@SuppressWarnings("rawtypes")
		@Override
		public void prepare(Map stormConf, TopologyContext context,
				OutputCollector collector) {
			this._collector = collector;
			File out = new File(_outFile);
			try {
				bw = new BufferedWriter(new OutputStreamWriter(
						new FileOutputStream(out, true)));
			} catch (IOException e1) {
				e1.printStackTrace();
			}
		}

		// blot计算单元，把tuple中的数据添加一个bkeep和回车。然后保存到outfile指定的文件中。
		@Override
		public void execute(Tuple input) {
			String line = input.getString(0);
			// System.out.println(line);
			// String[] str = line.split(_seperator);
			// System.out.println(str[2]);
			try {
				bw.write(line + ",bkeep" + "\n");
				bw.flush();
			} catch (IOException e) {
				e.printStackTrace();
			}
			_collector.emit(new Values(line));
		}

		@Override
		public void declareOutputFields(OutputFieldsDeclarer declarer) {
			declarer.declare(new Fields("line"));
		}

	}

	public static void main(String[] argv) throws AlreadyAliveException,
			InvalidTopologyException {

		String outFile = argv[0]; // 输出文件
		boolean distribute = Boolean.valueOf(argv[1]); // 本地模式还是集群模式
		TopologyBuilder builder = new TopologyBuilder(); // build一个topology
		builder.setSpout("spout", new SocketSpout(), 1); // 指定spout
		builder.setBolt("bolt", new Process(outFile), 1).shuffleGrouping(
				"spout"); // 指定bolt，包括bolt、process和grouping
		Config conf = new Config();
		if (distribute) {
			StormSubmitter.submitTopology("SocketProcess", conf,
					builder.createTopology());
		} else {
			LocalCluster cluster = new LocalCluster();
			cluster.submitTopology("SocketProcess", conf,
					builder.createTopology());
		}
	}
}