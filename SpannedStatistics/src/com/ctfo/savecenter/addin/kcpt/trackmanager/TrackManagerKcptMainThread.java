package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.addin.PacketAnalyser;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;
import com.lingtu.xmlconf.XmlConf;

public class TrackManagerKcptMainThread extends Thread implements PacketAnalyser {
	// 位置指令队列
	private ArrayBlockingQueue<Map<String, String>> trackPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	private static final Logger logger = LoggerFactory.getLogger(TrackManagerKcptMainThread.class);

	private int threadId = 0;
	// 运行标志
	public static boolean isRunning = true;
//	统计分析服务 
	private SpannedStatisticsThread spannedStatisticsThread; 
	
	private int count = 0;

	public void initAnalyser(int threadId, XmlConf config, String nodeName) throws Exception {
		this.threadId = threadId;
		// 文件存储线程数
		count = config.getIntValue(nodeName + "|count");
		TrackManagerKcptDBAdapter oracleDB = null;
		if (count != 0) {
			// 启动主线程
			start();
			oracleDB = new TrackManagerKcptDBAdapter();
			oracleDB.initDBAdapter(config, nodeName);
		}
		//初始化跨区统计存储线程
		spannedStatisticsThread = new SpannedStatisticsThread(oracleDB);
		spannedStatisticsThread.start();
	}

	public void addPacket(Map<String, String> packet) {
		try {
			trackPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("",e);
		}
	}

	public int getPacketsSize() {
		return trackPacket.size();
	}

	public void endAnalyser() {
		spannedStatisticsThread.interrupt();
	}

	public void run() {
		logger.info("监控位置主线程启动");
		Map<String, String> app = null;
		while (isRunning) {
			try// 依次向各个分析线程分发报文
			{
				app = trackPacket.take();
				if (app.get("TYPE").equals("5")) { // 上线通知
					continue;
				}
				// 处理盲区补传
				if (app.get("TYPE").equals("7")) {
					continue;
				}
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
				long maplon = -100;
				long maplat = -100;
				// 偏移
				Converter conver = new Converter();
				Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
				if (point != null) {
					maplon = Math.round(point.getX() * 600000);
					maplat = Math.round(point.getY() * 600000);
				} else {
					maplon = 0;
					maplat = 0;
				}
				app.put(Constant.MAPLON, maplon + "");
				app.put(Constant.MAPLAT, maplat + "");

				//跨域统计存储
				spannedStatisticsThread.addPacket(app);
				logger.debug(threadId + "，位置主线程" + trackPacket.size());
			} catch (Exception e) {
				logger.error("TrackManagerKcptMainThread : " + app.get(Constant.COMMAND) + e.getMessage());
			}
		}// End while
	}
}
