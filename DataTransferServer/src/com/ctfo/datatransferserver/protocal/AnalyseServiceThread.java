package com.ctfo.datatransferserver.protocal;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.DataPool;
import com.ctfo.datatransferserver.beans.VehiclePolymerizeBean;
import com.ctfo.datatransferserver.dao.ServiceUnitDao;

/**
 * 协议解析线程
 * 
 * @author yangyi
 * 
 */

public class AnalyseServiceThread extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(AnalyseServiceThread.class);
	// 异步数据报向量
	private ArrayBlockingQueue<String> vPacket = new ArrayBlockingQueue<String>(100000);

	private int id;
	private IAnalyseService iAnalyseService;
	public static boolean isRunning = true;
	ServiceUnitDao serviceUnitDao;

	public AnalyseServiceThread(int id, IAnalyseService iAnalyseService, ServiceUnitDao serviceUnitDao) {
		this.id = id;
		this.iAnalyseService = iAnalyseService;
		this.serviceUnitDao = serviceUnitDao;
	}

	public void addPacket(String packet) {

		vPacket.offer(packet);

	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void run() {
		VehiclePolymerizeBean vehiclePolymerizeBean = null;
		String message;
		while (isRunning) {
			try {
				if (!vPacket.isEmpty()) {
					message = vPacket.poll();
					vehiclePolymerizeBean = iAnalyseService.dealPacket(message, serviceUnitDao);
					if (vehiclePolymerizeBean != null) {
						DataPool.setTempVehicleMapValue(vehiclePolymerizeBean.getVid(), vehiclePolymerizeBean);
					}
				} else {
					sleep(1);
				}

			} catch (Exception e) {
				logger.error("线程" + id + "解析错误:" + e.getMessage());
			}
		}// End while
	}

}
