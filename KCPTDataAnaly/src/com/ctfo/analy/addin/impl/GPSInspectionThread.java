package com.ctfo.analy.addin.impl;


import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.GPSInspectionConfig;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.dao.GPSInspectionAdapter;
import com.ctfo.analy.util.CDate;
import com.lingtu.xmlconf.XmlConf;

public class GPSInspectionThread extends Thread implements PacketAnalyser{
	
	private static final Logger logger = Logger.getLogger(GPSInspectionThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);
	
	private GPSInspectionAdapter gpsInspectionAdapter = null; 
	
	// 线程ID
	private int threadId = 0;
	
	@Override
	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			logger.error(e);
		}
	}
	
	public void run(){
		while(true){
			try {
				logger.debug(threadId+"，GPSInspection主线程开始处理" + vPacket.size());
				VehicleMessageBean vehicleMessage = vPacket.take(); 
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
					checkGPSInspection(vehicleMessage);
				}
				logger.debug(threadId+"，GPSInspection主线程处理结束" + vPacket.size());
			} catch (InterruptedException e) {
				logger.error(e);
			}
		}
	}
	
	/****
	 * 对该点进行巡检
	 * @param vehicleMessage
	 */
	private void checkGPSInspection(VehicleMessageBean vehicleMessage){
		try{
		String cmddr = vehicleMessage.getCommanddr();
		GPSInspectionConfig gpsConfig = TempMemory.getGPSInspectionConfig(cmddr);
		if(null == gpsConfig){ // 如果没有GPS巡检配置则丢掉该车消息
			return;
		}
		
		logger.info("ThreadId of GPSInspection: " + threadId + " Commanddr ==>" + vehicleMessage.getCommanddr());
		
		long sysTime = System.currentTimeMillis();
		
		// 判断该点所属巡检区间是否已经巡检成功
		if(gpsConfig.getTimeFlag() + CDate.getCurrentDayYearMonthDay() < sysTime ){
			if(vehicleMessage.getMaplat() > 0 && vehicleMessage.getMaplon() > 0){
				if(gpsConfig.checkGPSInspection(sysTime)){
					gpsInspectionAdapter.saveGPSInspectionRecord(vehicleMessage);
				}
			}
		}
		}catch(Exception ex){
			logger.debug("GPS巡检过程中出错："+ex);
		}
	}

	@Override
	public void endAnalyser() {
		
	}

	@Override
	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void initAnalyser(int nId, XmlConf config, String nodeName) throws Exception {
		this.threadId = nId;
		gpsInspectionAdapter =  new GPSInspectionAdapter();
		start();
	}

}
