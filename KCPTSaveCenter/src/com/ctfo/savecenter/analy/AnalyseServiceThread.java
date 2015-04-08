package com.ctfo.savecenter.analy;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.addin.AddInManagerInit;
import com.ctfo.savecenter.beans.Message;

/**
 * 协议解析线程
 * 
 * @author yangyi
 * 
 */

public class AnalyseServiceThread extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(AnalyseServiceThread.class);
	// 异步数据报向量
	private ArrayBlockingQueue<Message> vPacket = new ArrayBlockingQueue<Message>(100000);
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	//线程编号
	private int id;
	private IAnalyseService iAnalyseService;
	public static boolean isRunning = true;

	public AnalyseServiceThread(int id, IAnalyseService iAnalyseService) {
		this.id = id;
		this.iAnalyseService = iAnalyseService;
	}

	public void addPacket(Message packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	
	@Override
	public void run() {
		Map<String, String> map = null;
		Message message;
		while (isRunning) {
			try {
				message = vPacket.take();
				map = iAnalyseService.dealPacket(message);
				if (map != null) {
					AddInManagerInit.getAddinManagerThread()[id].addPacket(map);
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime- tempTime) > 3000){
					logger.info("--AnalyseServiceThread:" + id + ", size:" + getPacketsSize() + ", 3秒处理数据:"+index+"条");
					index = 0;
					tempTime = currentTime;
				}
				index++;
			} catch (Exception e) {
				logger.error("线程" + id + "解析错误:" + e.getMessage());
			}
		}// End while
	}

}
