package com.ctfo.analy.io;

import java.util.concurrent.ArrayBlockingQueue;

import com.ctfo.analy.beans.MessageBean;


/**
 * 数据队列缓冲池
 * 
 * @author yangyi
 * 
 */
public class DataPool {

	// 接收数据量
	private static int receiveCount = 0;
	
	private static long startcount=0;
	private static long startdealcount=0;
	private static boolean startflag = false;

	// 数据接收队列
	//private static ArrayBlockingQueue<Message> receivePacket = new ArrayBlockingQueue<Message>(100000);
	
	// 数据发送队列
	private static ArrayBlockingQueue<MessageBean> receivePacket = new ArrayBlockingQueue<MessageBean>(10000);
	
	// 数据插件队列
	//private static ArrayBlockingQueue<Map<String, String>> addinPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	// 下行指令队列
	//private static ArrayBlockingQueue<Map<String, String>> commandPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
	// 位置指令队列
	//private static ArrayBlockingQueue<Map<String, String>> trackPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

 
	
 
	/**
	 * 获取数据量值
	 * 
	 * @return
	 */
	public static long getReceiveCountValue() {
		long temp = receiveCount;
		receiveCount = 0;
		return temp;

	}

	/**
	 * 添加数据量值
	 * 
	 * @param value
	 */
	public static void setCountValue() {
		receiveCount++;
	}

	public static long getStartcount() {
		return startcount;
	}

	public synchronized static void setStartcount() {
		startcount ++;
	}
	
	public static void setStartZero() {
		startcount =0;
	}

	public static boolean isStartflag() {
		return startflag;
	}

	public static void setStartflag(boolean tempstartflag) {
		 startflag = tempstartflag;
	}

	public static long getStartdealcount() {
		return startdealcount;
	}

	public synchronized static void setStartdealcount() {
	  startdealcount++;
	}
	
	public static void setStartDealZero() {
		startdealcount =0;
	}
//	/**
//	 * 插件队列是否为空
//	 */
//	public static boolean isAddinPacketEmpty() {
//		return addinPacket.isEmpty();
//	}
//	/**
//	 * 获取插件队列大小
//	 */
//	public static long getAddinPacketSize() {
//		return addinPacket.size();
//	}
//
//	/**
//	 * 获取插件队列值
//	 * 
//	 * @return
//	 * @throws InterruptedException 
//	 */
//	public static Map<String, String> getAddinPacketValue() throws InterruptedException {
//		return addinPacket.take();
//	}
//
//	/**
//	 * 添加插件队列值
//	 * 
//	 * @param value
//	 */
//	public static void setAddinPacketValue(Map<String, String> value) {
//		try {
//			addinPacket.put(value);
//		} catch (InterruptedException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//	}

	public static MessageBean getReceivePacket() {
			return receivePacket.poll();
		 
	}

	public static void setReceivePacket(MessageBean message) {
		DataPool.receivePacket.add(message);
	}
	
 


}
