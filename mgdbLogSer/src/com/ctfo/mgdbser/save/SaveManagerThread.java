package com.ctfo.mgdbser.save;

import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;


import com.ctfo.mgdb.beans.Record;
import com.ctfo.mgdb.conn.DbConn;
import com.ctfo.mgdb.conn.MongodbConn;

public class SaveManagerThread extends Thread{
	private static final Logger logger = Logger.getLogger(SaveManagerThread.class);
	
	// 异步数据报向量
	private ArrayBlockingQueue<Record> rPacket = new ArrayBlockingQueue<Record>(
			100000);

	// 是否运行标志
	public boolean isRunning = true;

	int threadId = 0;

	public SaveManagerThread(int threadId){
		this.threadId = threadId;
	}

	public void addPacket(Record rpacket) { 
		rPacket.offer(rpacket);
	}

	public int getPacketsSize() {
		return rPacket.size();
	}

	/**
	 * 线程执行体
	 */
	public void run() {
		
		DbConn conn = null;
		try {
			conn = new MongodbConn();
		} catch (Exception e) {
			logger.error("mongoDB连接错误:" + e.getMessage());
		}
		while (isRunning) {
			try {
				if(conn != null){
					Record packet = rPacket.take();
					if (rPacket != null) {
						conn.save(packet);		
					}
				}
				
			} catch (Exception e) {
				logger.error("mongoDB存储错误:" + e.getMessage());
			}
		}// End while
	}


	
	
}
