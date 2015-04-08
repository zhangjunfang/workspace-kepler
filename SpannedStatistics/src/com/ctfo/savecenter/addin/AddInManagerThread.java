package com.ctfo.savecenter.addin;

import java.util.Hashtable;
import java.util.Map;
import java.util.Vector;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;

/**
 * Title: 插件管理类 Description: 管理轨迹分析线程，进行深入的轨迹分析
 */
public class AddInManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(AddInManagerThread.class);

	// 异步数据报向量
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	// 是否运行标志
	public boolean isRunning = true;

	public Hashtable<String, Vector<Vector<PacketAnalyser>>> taAnalyser = null;
	int threadId = 0;

	public AddInManagerThread(int threadId, Hashtable<String, Vector<Vector<PacketAnalyser>>> taAnalyser) {
		this.taAnalyser = taAnalyser;
		this.threadId = threadId;
	}

	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("",e);
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	/**
	 * 线程执行体
	 */
	public void run() {
		logger.info("插件管理应用启动");
		// 启动所有接收分析管理线程

		Map<String, String> map = null;

		int i = 0;
		Vector<Vector<PacketAnalyser>> vA;
		Vector<PacketAnalyser> am;
		String ptype;
		int thnum = 0;
		while (isRunning) {
			try {
				map = vPacket.take();

				ptype = map.get(Constant.PTYPE);

				vA = taAnalyser.get(ptype);
				if (vA == null || vA.size() == 0)
					continue;

				for (i = 0; i < vA.size(); i++) {
					am = vA.elementAt(i);
					thnum = (int)(Long.parseLong(map.get(Constant.VID)) % am.size());
					am.elementAt(thnum).addPacket(map);
				}// End while
				logger.debug("AddInManagerThread " + threadId + "," + vPacket.size());
			} catch (Exception e) {
				logger.error("插件管理线程出错."+ e.getMessage());
			}
		} // End While
		logger.info("插件管理服务停止");
	}
}
