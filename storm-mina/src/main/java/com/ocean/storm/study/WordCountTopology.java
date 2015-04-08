package com.ocean.storm.study;

import java.util.HashMap;
import java.util.Map;
 

import com.ocean.storm.study.RandomSentenceSpout;

import backtype.storm.Config;
import backtype.storm.LocalCluster;
import backtype.storm.StormSubmitter;
import backtype.storm.topology.BasicOutputCollector;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.TopologyBuilder;
import backtype.storm.topology.base.BaseBasicBolt;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Tuple;
import backtype.storm.tuple.Values;

/**
 * This topology demonstrates Storm's stream groupings and multilang
 * capabilities.
 */
public class WordCountTopology {
    public static class SplitSentence extends BaseBasicBolt {
       
		private static final long serialVersionUID = 146904577782804083L;


		public void execute(Tuple input, BasicOutputCollector collector) {
            try {
                String msg = input.getString(0);
                System.out.println(msg + "-------------------");
                if (msg != null) {
                    String[] s = msg.split(" ");
                    for (String string : s) {
                        collector.emit(new Values(string));
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
 
       
        public void declareOutputFields(OutputFieldsDeclarer declarer) {
            declarer.declare(new Fields("word"));
        }
    }
 
    public static class WordCount extends BaseBasicBolt {
       
		private static final long serialVersionUID = 9080948772140456741L;
		Map<String, Integer> counts = new HashMap<String, Integer>();
 
       
        public void execute(Tuple tuple, BasicOutputCollector collector) {
            String word = tuple.getString(0);
            Integer count = counts.get(word);
            if (count == null){
                count = 0;
            }
            count++;
            counts.put(word, count);
            collector.emit(new Values(word, count));
            System.err.println(word+"----------------------"+count);
        }
 
      
        public void declareOutputFields(OutputFieldsDeclarer declarer) {
            declarer.declare(new Fields("word", "count"));
        }
    }
 
    public static void main(String[] args) throws Exception {
 
        TopologyBuilder builder = new TopologyBuilder();
 
        builder.setSpout("spout", new RandomSentenceSpout(), 5);
 
        builder.setBolt("split", new SplitSentence(), 8).shuffleGrouping("spout");
        builder.setBolt("count", new WordCount(), 12).fieldsGrouping("split",new Fields("word"));
 
        Config conf = new Config();
        conf.setDebug(true);
 
        if (args != null && args.length > 0) {
        	/*设置该topology在storm集群中要抢占的资源slot数，一个slot对应这supervisor节点上的以个worker进程
            如果你分配的spot数超过了你的物理节点所拥有的worker数目的话，有可能提交不成功，加入你的集群上面已经有了
            一些topology而现在还剩下2个worker资源，如果你在代码里分配4个给你的topology的话，那么这个topology可以提交
            但是提交以后你会发现并没有运行。 而当你kill掉一些topology后释放了一些slot后你的这个topology就会恢复正常运行。
           */
            conf.setNumWorkers(3);
 
            StormSubmitter.submitTopology(args[0], conf,
                    builder.createTopology());
        } else {
            conf.setMaxTaskParallelism(3);
            //指定为本地模式运行
            LocalCluster cluster = new LocalCluster();
            cluster.submitTopology("word-count", conf, builder.createTopology());

            Thread.sleep(10000);
 
            cluster.shutdown();
        }
    }
}