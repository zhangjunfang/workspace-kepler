package com.ctfo.analy.protocal;

import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.analy.addin.AddInManagerInit;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.VehicleMessageBean;

/**
 * 协议解析线程
 * 
 * @author yangyi
 * 
 */

public class AnalyseServiceThread extends Thread {

	private static final Logger logger = Logger.getLogger(AnalyseServiceThread.class);
	// 异步数据报向量
	private ArrayBlockingQueue<MessageBean> vPacket = new ArrayBlockingQueue<MessageBean>(100000);

	private int id;
	private IAnalyseService iAnalyseService;
	public static boolean isRunning = true;

	public AnalyseServiceThread(int id, IAnalyseService iAnalyseService) {
		this.id = id;
		this.iAnalyseService = iAnalyseService;
	}

	public void addPacket(MessageBean packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void run() {
		VehicleMessageBean vehicleMessage = null;
		MessageBean message;
		while (isRunning) {
			try {
				message = vPacket.take();
				vehicleMessage = iAnalyseService.dealPacket(message);
				if (vehicleMessage != null) {
					AddInManagerInit.getAddinManagerThread()[id].addPacket(vehicleMessage);
				}
			} catch (Exception e) {
				System.out.println(e.getMessage());
				logger.error("线程" + id + "解析错误:" + e.getMessage());
			}
		}// End while
	}

}
