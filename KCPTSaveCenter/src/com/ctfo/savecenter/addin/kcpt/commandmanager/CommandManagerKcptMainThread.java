package com.ctfo.savecenter.addin.kcpt.commandmanager;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.addin.PacketAnalyser;
import com.ctfo.savecenter.dao.CommandManagerKcptDBAdapter;
import com.ctfo.savecenter.dao.CommandManagerRedisDBAdapter;
import com.lingtu.xmlconf.XmlConf;

/**
 * 控制指令线程
 * @author yangyi
 *
 */
public class CommandManagerKcptMainThread extends Thread implements PacketAnalyser {

	private static final Logger logger = LoggerFactory.getLogger(CommandManagerKcptMainThread.class);
	
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	int threadId;
	// 运行标志
	public static boolean isRunning = true;

	// 位置更新线程数组 
	private CommandManagerKcptThread commandManagerThread;
	
	private CommandManagerKcptDBAdapter oracleDba = null;
	
	private CommandManagerRedisDBAdapter redisDba = null;

	public void initAnalyser(int threadId, XmlConf config, String nodeName) throws Exception {
		this.threadId = threadId;
		// 文件存储线程数
		int count = config.getIntValue(nodeName + "|count");
		if (count != 0) {
			// 启动主线程
			start();
			oracleDba = new CommandManagerKcptDBAdapter();
			oracleDba.initDBAdapter(config, nodeName);
			redisDba = new CommandManagerRedisDBAdapter();
			// 启动位置更新线程
			commandManagerThread = new CommandManagerKcptThread(oracleDba,redisDba,threadId, config, nodeName);
			commandManagerThread.start();
		}
	}

	@Override
	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	public void endAnalyser() {
		commandManagerThread.interrupt();
	}
	/**  */
	@Override
	public void run() {
		logger.info("监控指令存储主线程启动");

		while (isRunning) {
			try {
				Map<String, String> cmdBean = vPacket.take();
				commandManagerThread.addPacket(cmdBean);
				
			} catch (InterruptedException e) {
				logger.error("分析线程分发报文线程出错"+ e.getMessage());
			}
		}// End while
		logger.debug("监控指令存储主线程停止");
	}
}
