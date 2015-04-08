package com.ctfo.mgdbser.analy;

import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.mgdb.beans.Message;
import com.ctfo.mgdb.beans.Record;
import com.ctfo.mgdb.util.XmlConfUtil;
import com.ctfo.mgdbser.save.SaveManagerInit;

/**
 * 
 * 内部协议解析线程
 * @author huangjincheng
 *
 */
public class AnalyseServiceThread extends Thread {
	
	XmlConfUtil config;
	
	private static final Logger logger = Logger
			.getLogger(AnalyseServiceThread.class);
	// 异步数据报向量
	public ArrayBlockingQueue<Message> mPacket = new ArrayBlockingQueue<Message>(
			100000);

	private int id;
	private IAnalyseService iAnalyseService;
	public static boolean isRunning = true;

	public AnalyseServiceThread(int id, IAnalyseService iAnalyseService,XmlConfUtil config) {
		this.id = id;
		this.iAnalyseService = iAnalyseService;
		this.config = config;
	}

	public void addPacket(Message packet) {
		try {
			mPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(e);
		}
	}

	public int getPacketsSize() {
		return mPacket.size();
	}
	
	

	@Override
	public void run() {
		Record rPacket = null;
		Message message = null;
		SaveManagerInit ami = new SaveManagerInit(this.config);
		try {
			ami.init();
		} catch (Exception e1) {
			e1.printStackTrace();
		} 
		while (isRunning) {
			try {

				message = mPacket.take();
				rPacket = iAnalyseService.dealPacket(message);
				if (rPacket != null) {
					SaveManagerInit.getAddinManagerThread()[id].addPacket(rPacket);
				}
				
			} catch (Exception e) {
				logger.error("线程" + id + "解析错误:" + e.getMessage()+":"+message.getCommand());
			}
		}// End while
	}

}

