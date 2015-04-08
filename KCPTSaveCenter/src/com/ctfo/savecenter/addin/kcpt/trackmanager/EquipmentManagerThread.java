package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


import com.ctfo.savecenter.beans.EquipmentStatus;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;

public class EquipmentManagerThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(EquipmentManagerThread.class);
	//计数器
	private int index = 0 ;
	//计时器
	private long tempTime = System.currentTimeMillis();
	//线程编号
	private int nId = 0;
	// 异步数据报向量
	private ArrayBlockingQueue<EquipmentStatus> vPacket = new ArrayBlockingQueue<EquipmentStatus>(100000);

	private TrackManagerKcptDBAdapter oracleDB = null;

	public EquipmentManagerThread(TrackManagerKcptDBAdapter oracleDB, int nId) {
		this.oracleDB = oracleDB;
		this.nId = nId;
	}

	public void addPacket(EquipmentStatus packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}
	public void run() {
		while (TrackManagerKcptMainThread.isRunning) {
			try {
				EquipmentStatus eqStatus = vPacket.take();
				oracleDB.updateVehicleLineStatus(eqStatus);
				long currentTime = System.currentTimeMillis();
				if(currentTime - tempTime > 3000){
					logger.warn("equipment-:" + nId + ",size:" + vPacket.size() + ",3秒处理数据:"+index+"条");
					tempTime = currentTime;
					index = 0 ;
				}
				index++;
			} catch (Exception e) {
				logger.error("更新设备状态出错." + e.getMessage(),e);
			}
		}// End while
	}
}
