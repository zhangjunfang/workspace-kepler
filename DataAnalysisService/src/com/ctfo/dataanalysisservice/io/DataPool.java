package com.ctfo.dataanalysisservice.io;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;

/**
 * 数据缓冲池
 * 
 * @author yiable
 * 
 */
public class DataPool {

	// MSG服务数据总接收队列
	public static ArrayBlockingQueue<Message> receivePackets = new ArrayBlockingQueue<Message>(
			100000);

	// MSG服务数据总发送队列
	public static ArrayBlockingQueue<String> sendPackets = new ArrayBlockingQueue<String>(
			100000);

	// 数据存储队列
	public static ArrayBlockingQueue<ThVehicleAlarm> saveDataPackets = new ArrayBlockingQueue<ThVehicleAlarm>(100000);

	// 业务分发线程类
	// public static BussinesDistributeThread[] bussinesDistributeThreadArray;

	public static long i;// 原始指令数
	public static long j;// 处理指令数
	//private static final Logger logger = Logger.getLogger(DataPool.class);

	/**
	 * 关键点队列，关键点队列是由待处理队列的数据分析后缓存本地，与待处理数据队列进行分析
	 */
	public static Map<String, VehicleMessage> keyPointPacket = new HashMap<String, VehicleMessage>();
	
	
	/**
	 * 获取接收队列大小
	 */
	public static long getReceivePacketSize() {
		return receivePackets.size();
	}

	/**
	 * 获取接收队列值
	 * 
	 * @return
	 */
	public static Message getReceivePacketValue() {

		try {
			return receivePackets.take();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();return null;
		}
	}

	/**
	 * 添加接收队列值
	 * 
	 * @param value
	 */
	public static void setReceivePacketValue(Message value) {
		// logger.info(System.currentTimeMillis()+"原始指令数"+i++);
		try {
			receivePackets.put(value);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/**
	 * 获取发送队列大小
	 */
	public static long getSendPacketSize() {
		return sendPackets.size();
	}

	/**
	 * 获取发送队列值
	 * 
	 * @return
	 */
	public static String getSendPacketValue() {
		return sendPackets.poll();
	}

	/**
	 * 添加发送队列值
	 * 
	 * @param value
	 */
	public static void setSendPacketValue(String value) {
		sendPackets.offer(value);
	}

	/**
	 * 获得存储队列数据
	 * 
	 * @return
	 * @throws InterruptedException
	 */
	public static ThVehicleAlarm getSaveDataPacketValue()
			throws InterruptedException {
		return saveDataPackets.take();
	}

	/**
	 * 添加存储队列值
	 * 
	 * @param saveDataPackets
	 * @throws InterruptedException
	 */
	public static void setSaveDataPacket(ThVehicleAlarm alarm)
			throws InterruptedException {
			saveDataPackets.put(alarm);
	}

	/**
	 * 获取存储队列值
	 * 
	 * @return
	 */
	public static long getSaveDataPacketSize() {
		return saveDataPackets.size();
	}

	public static void putKeyPointPacket(String key,VehicleMessage vm){
		keyPointPacket.put(key, vm);
	}
	
	public static VehicleMessage getKeyPointPacket(String key){
		if(keyPointPacket.containsKey(key)){
			return keyPointPacket.get(key);
		}
		return null;
	}
	
}
