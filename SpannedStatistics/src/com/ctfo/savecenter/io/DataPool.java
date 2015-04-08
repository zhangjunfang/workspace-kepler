package com.ctfo.savecenter.io;

import java.util.Queue;
import java.util.concurrent.ArrayBlockingQueue;

import com.ctfo.savecenter.beans.Message;

/**
 * 数据队列缓冲池
 * 
 * @author yangyi
 * 
 */
public class DataPool {

	// 接收数据量
	private static int receiveCount = 0;

	// 数据发送队列
	private static Queue<Message> sendPacket =new ArrayBlockingQueue<Message>(100000);
	// 数据发送队列
	//private static Queue<Message> sendPacket = new ConcurrentLinkedQueue<Message>();
	
	// 数据插件队列
	//private static ArrayBlockingQueue<Map<String, String>> addinPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	// 下行指令队列
	//private static ArrayBlockingQueue<Map<String, String>> commandPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
	// 位置指令队列
	//private static ArrayBlockingQueue<Map<String, String>> trackPacket = new ArrayBlockingQueue<Map<String, String>>(100000);

	public static void addSendPacket(Message packet) {
		sendPacket.offer(packet);
	}    
 
	public static Message getSendPacket(){
		return sendPacket.poll();
	}
	
	public static Boolean isSendPacketEmpty(){
		return sendPacket.isEmpty();
	}
	
	
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
	
 


}
